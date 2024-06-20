using Ofertas.Infrastructure.interfaces;
using Ofertas.Infrastructure.Repositories;

namespace Ofertas.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private OfertaRepository _ofertaRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IOfertaRepository Ofertas => _ofertaRepository ??= new OfertaRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
