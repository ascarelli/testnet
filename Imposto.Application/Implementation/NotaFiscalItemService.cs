using Imposto.Core.Data.Implementation;
using Imposto.Domain.Entity;
using Imposto.Infra;
using System.Collections.Generic;
using System.Linq;

namespace Imposto.Core.Service
{
    public class NotaFiscalItemService
    {
        public NotaFiscalItemService()
        {
        }
        
        public NotaFiscalItem CalcularICMS(Pedido pedido, NotaFiscalItem notaFiscalItem)
        {
            //ICMS
            notaFiscalItem.TipoIcms = "10";
            notaFiscalItem.AliquotaIcms = 0.17;

            if (pedido.EstadoDestino == pedido.EstadoOrigem)
            {
                notaFiscalItem.TipoIcms = "60";
                notaFiscalItem.AliquotaIcms = 0.18;
            }

            notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;

            return notaFiscalItem;
        }
        public NotaFiscalItem CalcularCFOP(Pedido pedido, PedidoItem pedidoItem, NotaFiscalItem notaFiscalItem)
        {
            IEnumerable<NotaFiscalCFOP> cfops = new List<NotaFiscalCFOP>();
            using (var context = new TesteImpostoContext())
            {
                var notaFiscalCfopRepository = new NotaFiscalCfopRepository(context);
                cfops = notaFiscalCfopRepository.GetAll();
                notaFiscalItem.Cfop = cfops.FirstOrDefault(x => x.EstadoOrigem == pedido.EstadoOrigem && x.EstadoDestino == pedido.EstadoDestino)?.ValorCFOP.ToString();

                if (notaFiscalItem.Cfop == "6.009")
                    notaFiscalItem.BaseIcms = pedidoItem.ValorItemPedido * 0.90; //redução de base
                else
                    notaFiscalItem.BaseIcms = pedidoItem.ValorItemPedido;
            }

            return notaFiscalItem;
        }
        public NotaFiscalItem CalcularIPI(PedidoItem pedidoItem, NotaFiscalItem notaFiscalItem)
        {
            notaFiscalItem.BaseIPI = pedidoItem.ValorItemPedido;
            notaFiscalItem.AliquotaIPI = 10;
            if (pedidoItem.Brinde)
                notaFiscalItem.AliquotaIPI = 0;
            notaFiscalItem.ValorIPI = (notaFiscalItem.AliquotaIPI / 100) * notaFiscalItem.BaseIPI;

            return notaFiscalItem;
        }
    }
}
