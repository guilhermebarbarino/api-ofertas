using Ofertas.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofertas.Application
{
    public interface IOfertaService
    {
        Task<IEnumerable<Oferta>> GetAllAsync();
        Task<Oferta> GetByIdAsync(Guid id);
        Task AddAsync(Oferta oferta);
        Task UpdateAsync(Oferta oferta);
        Task DeleteAsync(Guid id);
    }
}
