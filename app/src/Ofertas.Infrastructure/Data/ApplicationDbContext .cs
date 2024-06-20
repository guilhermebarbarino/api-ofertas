using Microsoft.EntityFrameworkCore;
using Ofertas.Domain.Entidades;
using System.Collections.Generic;

namespace Ofertas.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Oferta> Ofertas { get; set; }
    }
}
