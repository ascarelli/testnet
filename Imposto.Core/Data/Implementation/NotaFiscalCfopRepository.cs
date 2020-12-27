using Imposto.Core.Core;
using Imposto.Core.Data.Contract;
using Imposto.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Imposto.Core.Data.Implementation
{
    public class NotaFiscalCfopRepository : BaseRepository<NotaFiscalCFOP, TesteImpostoContext>, INotaFiscalCfopRepository
    {
        public NotaFiscalCfopRepository(TesteImpostoContext context) : base(context)
        {

        }

        public override IEnumerable<NotaFiscalCFOP> GetAll()
        {
            return context.Database.SqlQuery<NotaFiscalCFOP>("exec P_NOTA_FISCAL_CFOP_VALORES").ToList();
        }
    }
}
