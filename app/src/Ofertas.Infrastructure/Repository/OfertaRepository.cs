using Microsoft.EntityFrameworkCore;
using Ofertas.Domain.Entidades;
using Ofertas.Infrastructure.Data;
using Ofertas.Infrastructure.interfaces;

namespace Ofertas.Infrastructure.Repositories
{
    public class OfertaRepository : IOfertaRepository
    {
        private readonly ApplicationDbContext _context;

        public OfertaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Oferta>> GetAllAsync()
        {
            return await _context.Ofertas.ToListAsync();
        }


        public async Task AddAsync(Oferta oferta)
        {
            await _context.Ofertas.AddAsync(oferta);
        }

        public async Task UpdateAsync(Oferta oferta)
        {
            _context.Ofertas.Update(oferta);
        }

      

        public async Task<Oferta> GetByIdAsync(Guid id)
        {
            return await _context.Ofertas.FindAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var oferta = await _context.Ofertas.FindAsync(id);
            if (oferta != null)
            {
                _context.Ofertas.Remove(oferta);
            }
        }
    }
}
