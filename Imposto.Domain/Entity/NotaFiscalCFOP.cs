using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Domain.Entity
{
    public class NotaFiscalCFOP
    {
        public int Id { get; set; }
        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }
        public decimal ValorCFOP { get; set; }
    }
}
