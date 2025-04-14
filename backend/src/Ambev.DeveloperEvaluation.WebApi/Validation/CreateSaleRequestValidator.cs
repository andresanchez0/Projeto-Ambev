using Ambev.DeveloperEvaluation.Common.DTOs;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Validation
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("Número da venda é obrigatório.");
            RuleFor(x => x.SaleDate).NotEmpty().WithMessage("Data da venda é obrigatória.");
            RuleFor(x => x.Customer).NotEmpty().WithMessage("Cliente é obrigatório.");
            RuleFor(x => x.Branch).NotEmpty().WithMessage("Filial é obrigatória.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("É necessário ao menos um item na venda.")
                .Must(items => items.All(i => i.Quantity <= 20))
                .WithMessage("Não é permitido vender mais de 20 unidades do mesmo produto.");

            RuleForEach(x => x.Items).SetValidator(new CreateSaleItemRequestValidator());
        }
    }

    public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
    {
        public CreateSaleItemRequestValidator()
        {
            RuleFor(i => i.Product).NotEmpty().WithMessage("Produto é obrigatório.");
            RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("Quantidade deve ser maior que 0.");
            RuleFor(i => i.UnitPrice).GreaterThan(0).WithMessage("Preço unitário deve ser maior que 0.");
        }
    }
}
