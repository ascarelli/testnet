using Imposto.Core.Data;
using Imposto.Core.Data.Contract;
using Imposto.Core.Data.Implementation;
using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Imposto.Core.Service
{
    public class NotaFiscalItemService
    {
        public NotaFiscalItemService()
        {
        }
        
        public void CalcularICMS(NotaFiscal notaFiscal, ref NotaFiscalItem notaFiscalItem)
        {
            //ICMS
            notaFiscalItem.TipoIcms = "10";
            notaFiscalItem.AliquotaIcms = 0.17;

            if (notaFiscal.EstadoDestino == notaFiscal.EstadoOrigem)
            {
                notaFiscalItem.TipoIcms = "60";
                notaFiscalItem.AliquotaIcms = 0.18;
            }

            notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
        }
        public void CalcularCFOP(NotaFiscal notaFiscal, ref NotaFiscalItem notaFiscalItem, PedidoItem pedidoItem)
        {
            IEnumerable<NotaFiscalCFOP> cfops = new List<NotaFiscalCFOP>();
            using (var context = new TesteImpostoContext())
            {
                var notaFiscalCfopRepository = new NotaFiscalCfopRepository(context);
                cfops = notaFiscalCfopRepository.GetAll();
                notaFiscalItem.Cfop = cfops.FirstOrDefault(x => x.EstadoOrigem == notaFiscal.EstadoOrigem && x.EstadoDestino == notaFiscal.EstadoDestino)?.ValorCFOP.ToString();

                if (notaFiscalItem.Cfop == "6.009")
                    notaFiscalItem.BaseIcms = pedidoItem.ValorItemPedido * 0.90; //redução de base
                else
                    notaFiscalItem.BaseIcms = pedidoItem.ValorItemPedido;
            }
        }
        public void CalcularIPI(ref NotaFiscalItem notaFiscalItem, PedidoItem pedidoItem)
        {
            notaFiscalItem.BaseIPI = pedidoItem.ValorItemPedido;
            notaFiscalItem.AliquotaIPI = 10;
            if (pedidoItem.Brinde)
                notaFiscalItem.AliquotaIPI = 0;
            notaFiscalItem.ValorIPI = (notaFiscalItem.AliquotaIPI / 100) * notaFiscalItem.BaseIPI;
        }
    }
}
