using FluentValidation;
using Ofertas.Domain.Entidades;

namespace Ofertas.Application.Validators
{
    public class OfertaValidator : AbstractValidator<Oferta>
    {
        public OfertaValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("O título é obrigatório.");
            RuleFor(x => x.Descricao).NotEmpty().WithMessage("A descrição é obrigatória.");
            RuleFor(x => x.Preco).GreaterThan(0).WithMessage("O preço deve ser maior que zero.");
        }
    }
}
