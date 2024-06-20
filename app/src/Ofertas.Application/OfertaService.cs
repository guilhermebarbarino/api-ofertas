using FluentValidation;
using Ofertas.Domain.Entidades;
using Ofertas.Infrastructure.Data;

namespace Ofertas.Application.Services
{
    public class OfertaService : IOfertaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Oferta> _validator;

        public OfertaService(IUnitOfWork unitOfWork, IValidator<Oferta> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<IEnumerable<Oferta>> GetAllAsync()
        {
            return await _unitOfWork.Ofertas.GetAllAsync();
        }

        public async Task<Oferta> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Ofertas.GetByIdAsync(id);
        }

        public async Task AddAsync(Oferta oferta)
        {
            await _validator.ValidateAndThrowAsync(oferta);
            await _unitOfWork.Ofertas.AddAsync(oferta);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Oferta oferta)
        {
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
