using Imposto.Core.Service;
using Imposto.Domain.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        public FormImposto()
        {
            InitializeComponent();
            dataGridViewPedidos.AutoGenerateColumns = true;                       
            dataGridViewPedidos.DataSource = GetTablePedidos();
            SetDatagridviewColumns();
            cbxEstadoOrigem.DataSource = GetEstados();
            cbxEstadoDestino.DataSource = GetEstados();
            ResizeColumns();
        }

        private void ResizeColumns()
        {
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }   
        }
        private object GetTablePedidos()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));
            return table;
        }
        private bool NotaFiscalIsValid()
        {
            if(string.IsNullOrWhiteSpace(textBoxNomeCliente.Text))
            {
                MessageBox.Show("Necessário digitar o nome do cliente");
                return false;
            }

            return true;
        }
        private void Clear()
        {
            dataGridViewPedidos.DataSource = GetTablePedidos();
            textBoxNomeCliente.Clear();
            cbxEstadoOrigem.SelectedIndex = 0;
            cbxEstadoDestino.SelectedIndex = 0;
        }
        private List<EstadoViewModel> GetEstados()
        {            
            var jsonString = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "//estados.json");
            var estados = JsonConvert.DeserializeObject<List<EstadoViewModel>>(jsonString);
            return estados;
        }
        private void SetDatagridviewColumns()
        {
            DataGridViewButtonColumn bcol = new DataGridViewButtonColumn
            {
                Text = "Deletar",
                Name = "OpDeletar",
                HeaderText = "",
                UseColumnTextForButtonValue = true
            };

            dataGridViewPedidos.Columns.Add(bcol);
            dataGridViewPedidos.Columns["OpDeletar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
        }

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (NotaFiscalIsValid())
            {
                NotaFiscalService service = new NotaFiscalService();

                Pedido pedido = new Pedido
                {
                    EstadoOrigem = cbxEstadoOrigem.SelectedValue.ToString(),
                    EstadoDestino = cbxEstadoDestino.SelectedValue.ToString(),
                    NomeCliente = textBoxNomeCliente.Text
                };

                DataTable table = (DataTable)dataGridViewPedidos.DataSource;

                foreach (DataRow row in table.Rows)
                {
                    Boolean.TryParse(row["Brinde"].ToString(), out bool Brinde);
                    Double.TryParse(row["Valor"].ToString(), out double Valor);

                    pedido.ItensDoPedido.Add(
                        new PedidoItem()
                        {
                            Brinde = Brinde,
                            CodigoProduto = row["Codigo do produto"].ToString(),
                            NomeProduto = row["Nome do produto"].ToString(),
                            ValorItemPedido = Valor
                        });
                }

                service.GerarNotaFiscal(pedido);
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Operação efetuada com sucesso");
                Clear();
            }
        }
        private void dataGridViewPedidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv != null)
            {
                DataGridViewButtonCell b = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                if(b?.Value?.ToString() == "Deletar")
                {
                    dataGridViewPedidos.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
        private void dataGridViewPedidos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewPedidos.Columns["Valor"].Index &&
                !double.TryParse(Convert.ToString(e.FormattedValue), out double i))
            {
                e.Cancel = true;
                MessageBox.Show("Precisar ser um valor numérico. Ex: 25 - 25.60 - 26.66");
            }
        }
    }

    public class EstadoViewModel
    {
        public string ID { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
    }
}
