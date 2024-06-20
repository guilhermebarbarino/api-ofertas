using Ofertas.Domain.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ofertas.Infrastructure.interfaces
{
    public interface IOfertaRepository
    {
        Task<IEnumerable<Oferta>> GetAllAsync();
        Task<Oferta> GetByIdAsync(Guid id);
        Task AddAsync(Oferta oferta);
        Task UpdateAsync(Oferta oferta);
        Task DeleteAsync(Guid id);
    }
}
