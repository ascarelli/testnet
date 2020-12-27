using Imposto.Core.Data.Implementation;
using Imposto.Core.Service;
using Imposto.Domain.Contracts.Infra;
using Imposto.Infra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Imposto.Test
{
    [TestClass]
    public class NotaFiscalUnitTest
    {
        TesteImpostoContext _ctx;
        INotaFiscalRepository _notaFiscalRepository;
        NotaFiscalService _notaFiscalService;

        [TestInitialize]
        public void Initialize()
        {
            _ctx = new TesteImpostoContext();
            _notaFiscalService = new NotaFiscalService();
            _notaFiscalRepository = new NotaFiscalRepository(_ctx);
        }

        [TestMethod]
        public void GetNextNumberNF()
        {
            var nextNumber = _notaFiscalRepository.NextNumber();
            Assert.IsTrue(nextNumber > 0);
        }

        [TestMethod]
        public void GerarNotaFiscal_SP_RJ()
        {
            bool ok = true;
            var pedido = TestHelper.GetPedido_SP_RJ();

            try
            {
                _notaFiscalService.GerarNotaFiscal(pedido);
            }
            catch (Exception)
            {
                ok = false;
            }

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void EmitirNotaFiscal_SP_RJ()
        {
            var pedido = TestHelper.GetPedido_SP_RJ();
            var nextnumber = _notaFiscalRepository.NextNumber();

            var result = _notaFiscalService.EmitirNotaFiscal(pedido, nextnumber);

            Assert.IsTrue(result?.Serie > 0);
        }

        [TestMethod]
        public void EmitirNotaFiscalXML_SP_RJ()
        {
            var notaFiscal = TestHelper.GetNotaFiscal_SP_RJ();

            var result = _notaFiscalService.EmitirNotaFiscalXML(notaFiscal);

            Assert.IsTrue(result);
        }

        
    }
}
