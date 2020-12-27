using Imposto.Domain.Entity;

namespace Imposto.Test
{
    public static class TestHelper
    {

        public static Pedido GetPedido_SP_RJ()
        {
            var pedido = new Pedido
            {
                EstadoDestino = "RJ",
                EstadoOrigem = "SP",
                NomeCliente = "Magalu"
            };

            pedido.ItensDoPedido.Add(new PedidoItem
            {
                Brinde = true,
                CodigoProduto = "blah1",
                NomeProduto = "Tenis",
                ValorItemPedido = 75.50
            });

            pedido.ItensDoPedido.Add(new PedidoItem
            {
                Brinde = true,
                CodigoProduto = "blah2",
                NomeProduto = "Tenis 2",
                ValorItemPedido = 185.50
            });

            return pedido;
        }

        public static NotaFiscal GetNotaFiscal_SP_RJ()
        {
            NotaFiscal notaFiscal = new NotaFiscal
            {
                EstadoOrigem = "SP",
                EstadoDestino = "RJ",
                Id = 1010,
                NomeCliente = "Magalu",
                NumeroNotaFiscal = 54894,
                Serie = 1317415536
            };

            notaFiscal.ItensDaNotaFiscal.Add(new NotaFiscalItem
            {
                AliquotaIcms = 0.6,
                AliquotaIPI = 0.6,
                BaseIcms = 0.6,
                BaseIPI = 0.6,
                Cfop = "0.1",
                CodigoProduto = "Blah1",
                Desconto = 0,
                Id = 111,
                IdNotaFiscal = 10,
                NomeProduto = "Tenis blah",
                TipoIcms = "60",
                ValorIcms = 1.5,
                ValorIPI = 2.5
            });

            notaFiscal.ItensDaNotaFiscal.Add(new NotaFiscalItem
            {
                AliquotaIcms = 0.6,
                AliquotaIPI = 0.6,
                BaseIcms = 0.6,
                BaseIPI = 0.6,
                Cfop = "0.1",
                CodigoProduto = "Blah2",
                Desconto = 0,
                Id = 112,
                IdNotaFiscal = 10,
                NomeProduto = "Tenis blah2",
                TipoIcms = "60",
                ValorIcms = 1.5,
                ValorIPI = 2.5
            });

            return notaFiscal;
        }
    }
}
