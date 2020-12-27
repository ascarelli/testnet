using Imposto.Core.Data.Implementation;
using Imposto.Domain.Contracts.Infra;
using Imposto.Infra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Imposto.Test
{
    [TestClass]
    public class NotaFiscalUnitCfopTest
    {
        TesteImpostoContext _ctx;
        INotaFiscalCfopRepository _notaFiscalRepository;

        [TestInitialize]
        public void Initialize()
        {
            _ctx = new TesteImpostoContext();
            _notaFiscalRepository = new NotaFiscalCfopRepository(_ctx);
        }

        [TestMethod]
        public void GetAll()
        {
            var result = _notaFiscalRepository.GetAll();
            Assert.IsTrue(result.Any()) ;
        }
    }
}
