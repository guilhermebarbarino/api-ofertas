using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Ofertas.Application;
using Ofertas.Application.Services;
using Ofertas.Domain.Entidades;

namespace Ofertas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfertasController : ControllerBase
    {
        private readonly IOfertaService _ofertaService;
        private readonly IMemoryCache _cache;
        private const string OfertasCacheKey = "OfertasCache";

        public OfertasController(IOfertaService ofertaService, IMemoryCache cache)
        {
            _ofertaService = ofertaService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IEnumerable<Oferta>> Get()
        {
            if (!_cache.TryGetValue(OfertasCacheKey, out IEnumerable<Oferta> ofertas))
            {
                ofertas = await _ofertaService.GetAllAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(OfertasCacheKey, ofertas, cacheOptions);
            }

            return ofertas;
        }

        [HttpGet("{id}")]
        public async Task<Oferta> Get(Guid id)
        {
            return await _ofertaService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Oferta oferta)
        {
            await _ofertaService.AddAsync(oferta);
            _cache.Remove(OfertasCacheKey);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Oferta oferta)
        {
            if (id != oferta.Id)
            {
                return BadRequest();
            }

            await _ofertaService.UpdateAsync(oferta);
            _cache.Remove(OfertasCacheKey);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _ofertaService.DeleteAsync(id);
            _cache.Remove(OfertasCacheKey);
            return Ok();
        }
    }
}
