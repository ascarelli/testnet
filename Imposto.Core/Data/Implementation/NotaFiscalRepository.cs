using Imposto.Core.Core;
using Imposto.Core.Data.Contract;
using Imposto.Core.Domain;
using System.Linq;

namespace Imposto.Core.Data.Implementation
{
    public class NotaFiscalRepository : BaseRepository<NotaFiscal, TesteImpostoContext>, INotaFiscalRepository
    {
        public NotaFiscalRepository(TesteImpostoContext context) : base(context)
        {

        }

        public override ProcResult Add(NotaFiscal entity)
        {
            var proc = "EXEC [dbo].[P_NOTA_FISCAL] " +
                   $"@pId = '{entity.Id}'" +
                   $",@pNumeroNotaFiscal = '{entity.NumeroNotaFiscal}'" +
                   $",@pSerie = '{entity.Serie}'" +
                   $",@pNomeCliente = '{entity.NomeCliente}'" +
                   $",@pEstadoDestino = '{entity.EstadoDestino}'" +
                   $",@pEstadoOrigem = '{entity.EstadoOrigem}'";

            return context.Database.SqlQuery<ProcResult>(proc).FirstOrDefault();
        }

        public int NextNumber()
        {
            return context.Database.SqlQuery<int>("EXEC [dbo].[P_NOTA_FISCAL_NUMERO]").FirstOrDefault();
        }
    }
}
