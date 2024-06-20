using AutoMapper;
using FluentValidation;
using Ofertas.Application.ViewModels;
using Ofertas.Domain.Entidades;
using Ofertas.Infrastructure.Data;

namespace Ofertas.Application.Services
{
    public class OfertaService : IOfertaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Oferta> _validator;
        private readonly IMapper _mapper;

        public OfertaService(IUnitOfWork unitOfWork, IValidator<Oferta> validator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OfertaResponse>> GetAllAsync()
        {
            var ofertas = await _unitOfWork.Ofertas.GetAllAsync();
            return _mapper.Map<IEnumerable<OfertaResponse>>(ofertas);
        }

        public async Task<OfertaResponse> GetByIdAsync(Guid id)
        {
            var oferta = await _unitOfWork.Ofertas.GetByIdAsync(id);
            return _mapper.Map<OfertaResponse>(oferta);
        }

        public async Task AddAsync(OfertaRequest ofertaDto)
        {
            var oferta = _mapper.Map<Oferta>(ofertaDto);
            await _validator.ValidateAndThrowAsync(oferta);
            await _unitOfWork.Ofertas.AddAsync(oferta);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(OfertaRequest ofertaDto)
        {
            var oferta = _mapper.Map<Oferta>(ofertaDto);
            await _validator.ValidateAndThrowAsync(oferta);
            await _unitOfWork.Ofertas.UpdateAsync(oferta);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Ofertas.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
    }
}
