using Imposto.Core.Data;
using Imposto.Core.Data.Contract;
using Imposto.Core.Data.Implementation;
using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        public NotaFiscalService()
        {
            
        }
        public void GerarNotaFiscal(Domain.Pedido pedido)
        {
            using (var context = new TesteImpostoContext())
            {
                INotaFiscalRepository notaFiscalRepository = new NotaFiscalRepository(context);
                var numeroNotaFiscal = notaFiscalRepository.NextNumber();
                NotaFiscal notaFiscal = this.EmitirNotaFiscal(pedido, numeroNotaFiscal);
                if (this.EmitirNotaFiscalXML(notaFiscal))
                {
                    var resultNotaFiscal = notaFiscalRepository.Add(notaFiscal);
                    if (resultNotaFiscal?.Id > 0)
                    {
                        var notaFiscalItemRepository = new NotaFiscalItemRepository(context);
                        foreach (var itemNotaFiscal in notaFiscal.ItensDaNotaFiscal)
                        {
                            itemNotaFiscal.IdNotaFiscal = resultNotaFiscal.Id;
                            notaFiscalItemRepository.Add(itemNotaFiscal);
                        }
                    }
                }
            }
        }
        private NotaFiscal EmitirNotaFiscal(Pedido pedido, int numeroNotaFiscal)
        {
            NotaFiscalItemService notaFiscalItemService = new NotaFiscalItemService();

            NotaFiscal notaFiscal = new NotaFiscal
            {
                NumeroNotaFiscal = numeroNotaFiscal,
                Serie = new Random().Next(Int32.MaxValue),
                NomeCliente = pedido.NomeCliente,
                EstadoDestino = pedido.EstadoDestino,
                EstadoOrigem = pedido.EstadoOrigem
            };

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem
                {
                    NomeProduto = itemPedido.NomeProduto,
                    CodigoProduto = itemPedido.CodigoProduto
                };

                notaFiscalItemService.CalcularCFOP(notaFiscal, ref notaFiscalItem, itemPedido);

                notaFiscalItemService.CalcularICMS(notaFiscal, ref notaFiscalItem);

                if (itemPedido.Brinde)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                    notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                }

                if (notaFiscal.EstadoDestino == "SP")
                    notaFiscalItem.Desconto = 10;

                notaFiscalItemService.CalcularIPI(ref notaFiscalItem, itemPedido);

                notaFiscal.ItensDaNotaFiscal.Add(notaFiscalItem);
            }

            return notaFiscal;
        }
        private bool EmitirNotaFiscalXML(NotaFiscal notaFiscal)
        {
            var folderInfra = ConfigurationManager.AppSettings["folder-infra-notafiscal"];
            var folder = $"{folderInfra}{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}\\";
            var path = folder + $"NF_{notaFiscal.NomeCliente}_{notaFiscal.Serie}_{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}.xml";

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            FileStream file = File.Create(path);

            XmlSerializer xmlSerializer = new XmlSerializer(notaFiscal.GetType());
            xmlSerializer.Serialize(file, notaFiscal);

            file.Close();

            return true;
        }
    }
}
