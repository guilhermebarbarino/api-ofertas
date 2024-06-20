using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Ofertas.Application;
using Ofertas.Application.Services;
using Ofertas.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<OfertaResponse>> Get()
        {
            if (!_cache.TryGetValue(OfertasCacheKey, out IEnumerable<OfertaResponse> ofertas))
            {
                ofertas = await _ofertaService.GetAllAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(OfertasCacheKey, ofertas, cacheOptions);
            }

            return ofertas;
        }

        [HttpGet("{id}")]
        public async Task<OfertaResponse> Get(Guid id)
        {
            return await _ofertaService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OfertaRequest ofertaRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _ofertaService.AddAsync(ofertaRequest);
            _cache.Remove(OfertasCacheKey);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] OfertaRequest ofertaRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oferta = await _ofertaService.GetByIdAsync(id);
            if (oferta == null)
            {
                return NotFound();
            }

            await _ofertaService.UpdateAsync(ofertaRequest);
            _cache.Remove(OfertasCacheKey);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var oferta = await _ofertaService.GetByIdAsync(id);
            if (oferta == null)
            {
                return NotFound();
            }

            await _ofertaService.DeleteAsync(id);
            _cache.Remove(OfertasCacheKey);
            return Ok();
        }
    }
}
