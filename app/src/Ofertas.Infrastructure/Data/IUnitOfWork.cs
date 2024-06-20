using Ofertas.Infrastructure.interfaces;
using System.Threading.Tasks;

namespace Ofertas.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        IOfertaRepository Ofertas { get; }
        Task<int> CommitAsync();
    }
}
