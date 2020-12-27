using Imposto.Core.Core;
using Imposto.Core.Data.Contract;
using Imposto.Core.Domain;
using System.Linq;

namespace Imposto.Core.Data.Implementation
{
    public class NotaFiscalItemRepository : BaseRepository<NotaFiscalItem, TesteImpostoContext>, INotaFiscalItemRepository
    {
        public NotaFiscalItemRepository(TesteImpostoContext context) : base(context)
        {

        }

        public override ProcResult Add(NotaFiscalItem entity)
        {
            var proc = "EXEC [dbo].[P_NOTA_FISCAL_ITEM] " +
                   $"@pId = '{entity.Id}'" +
                   $",@pIdNotaFiscal = '{entity.IdNotaFiscal}'" +
                   $",@pCfop = '{entity.Cfop}'" +
                   $",@pTipoIcms = '{entity.TipoIcms}'" +
                   $",@pBaseIcms = '{entity.BaseIcms}'" +
                   $",@pAliquotaIcms = '{entity.AliquotaIcms}'" +
                   $",@pValorIcms = '{entity.ValorIcms}'" +
                   $",@pBaseIPI = '{entity.BaseIPI}'" +
                   $",@pAliquotaIPI = '{entity.AliquotaIPI}'" +
                   $",@pValorIPI = '{entity.ValorIPI}'" +
                   $",@pNomeProduto = '{entity.NomeProduto}'" +                   
                   $",@pCodigoProduto = '{entity.CodigoProduto}'" +
                   $",@pDesconto = '{entity.Desconto}'";

            return context.Database.SqlQuery<ProcResult>(proc).FirstOrDefault();
        }
    }
}
