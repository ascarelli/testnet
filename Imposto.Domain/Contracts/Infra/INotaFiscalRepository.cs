using Imposto.Core.Core;
using Imposto.Domain.Entity;

namespace Imposto.Domain.Contracts.Infra
{
    public interface INotaFiscalRepository : IBaseRepository<NotaFiscal>
    {
        int NextNumber();
    }
}
