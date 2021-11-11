using FluentValidation;

namespace CodeChallenge.Application.Services.Change.Commands.CreateTransaction.Validator
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            // Valor da Compra
            RuleFor(m => m.TotalValue)
                .NotNull().WithMessage("Campo obrigatório!")
                .NotEmpty().WithMessage("Campo obrigatório!")
                .GreaterThan(0.00M).WithMessage("Valor tem de ser positivo!")
               .ScalePrecision(2, 5).WithMessage("Valor inválido. Apenas 3 valores inteiros e 2 valores decimais permitidos.");

            // Valor Pago
            RuleFor(m => m.ValueGiven)
                .NotNull().WithMessage("Campo obrigatório!")
                .NotEmpty().WithMessage("Campo obrigatório!")
                .GreaterThan(m => m.TotalValue).WithMessage("Valor a receber tem de ser mais alto que valor a pagar!")
                .ScalePrecision(2, 5).WithMessage("Valor inválido. Apenas 3 valores inteiros e 2 valores decimais permitidos.");
        }
    }
}
