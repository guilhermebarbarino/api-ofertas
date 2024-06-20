using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Ofertas.Application;
using Ofertas.Application.Automapper;
using Ofertas.Application.Services;
using Ofertas.Application.Validators;
using Ofertas.Domain.Entidades;
using Ofertas.Infrastructure.Data;
using Ofertas.Infrastructure.interfaces;
using Ofertas.Infrastructure.Repositories;

namespace Ofertas.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryDb"));

            services.AddMemoryCache();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOfertaService,OfertaService>();
            services.AddScoped<IOfertaRepository,OfertaRepository>();
            services.AddTransient<IValidator<Oferta>, OfertaValidator>();

            services.AddMemoryCache();

            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<OfertaValidator>());

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ofertas API V1"));
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            InitializeDatabase(app.ApplicationServices);

        }
        private void InitializeDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (context.Database.EnsureCreated())
            {
                context.Ofertas.AddRange(new List<Oferta>
                {
                    new Oferta { Titulo = "Oferta 1", Descricao = "Descrição da Oferta 1", Preco = 100.0M, DataCriacao = DateTime.Now },
                    new Oferta { Titulo = "Oferta 2", Descricao = "Descrição da Oferta 2", Preco = 200.0M, DataCriacao = DateTime.Now },
                    new Oferta { Titulo = "Oferta 3", Descricao = "Descrição da Oferta 3", Preco = 300.0M, DataCriacao = DateTime.Now }
                });

                context.SaveChanges();
            }
        }
    }
}
