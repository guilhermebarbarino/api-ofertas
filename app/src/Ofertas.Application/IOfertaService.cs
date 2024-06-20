using Ofertas.Application.ViewModels;
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
        Task<IEnumerable<OfertaResponse>> GetAllAsync();
        Task<OfertaResponse> GetByIdAsync(Guid id);
        Task AddAsync(OfertaRequest oferta);
        Task UpdateAsync(OfertaRequest oferta);
        Task DeleteAsync(Guid id);
    }
}
