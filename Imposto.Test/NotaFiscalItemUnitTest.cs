using Imposto.Core.Service;
using Imposto.Domain.Entity;
using Imposto.Infra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Imposto.Test
{
    [TestClass]
    public class NotaFiscalItemUnitTest
    {
        TesteImpostoContext _ctx;
        NotaFiscalItemService _notaFiscalItemService;

        [TestInitialize]
        public void Initialize()
        {
            _ctx = new TesteImpostoContext();
            _notaFiscalItemService = new NotaFiscalItemService();
        }

        [TestMethod]
        public void CalcularCFOP()
        {
            var ok = true;
            var pedido = TestHelper.GetPedido_SP_RJ();

            try
            {
                foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
                {
                    NotaFiscalItem notaFiscalItem = new NotaFiscalItem
                    {
                        NomeProduto = itemPedido.NomeProduto,
                        CodigoProduto = itemPedido.CodigoProduto
                    };

                    _notaFiscalItemService.CalcularCFOP(pedido, itemPedido, notaFiscalItem);
                }
            }
            catch (Exception)
            {
                ok = false;
            }

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void CalcularIPI()
        {
            var ok = true;
            var pedido = TestHelper.GetPedido_SP_RJ();

            try
            {
                foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
                {
                    NotaFiscalItem notaFiscalItem = new NotaFiscalItem
                    {
                        NomeProduto = itemPedido.NomeProduto,
                        CodigoProduto = itemPedido.CodigoProduto
                    };

                    _notaFiscalItemService.CalcularIPI(itemPedido, notaFiscalItem);
                }
            }
            catch (Exception)
            {
                ok = false;
            }

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void CalcularICMS()
        {
            var ok = true;
            var pedido = TestHelper.GetPedido_SP_RJ();

            try
            {
                foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
                {
                    NotaFiscalItem notaFiscalItem = new NotaFiscalItem
                    {
                        NomeProduto = itemPedido.NomeProduto,
                        CodigoProduto = itemPedido.CodigoProduto
                    };

                    _notaFiscalItemService.CalcularICMS(pedido, notaFiscalItem);
                }
            }
            catch (Exception)
            {
                ok = false;
            }

            Assert.IsTrue(ok);
        }
    }
}
