using Imposto.Core.Core;
using Imposto.Core.Domain;

namespace Imposto.Core.Data.Contract
{
    public interface INotaFiscalRepository : IBaseRepository<NotaFiscal>
    {
        int NextNumber();
    }
}
