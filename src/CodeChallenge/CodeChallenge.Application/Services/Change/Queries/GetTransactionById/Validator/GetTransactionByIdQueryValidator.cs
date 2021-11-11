using FluentValidation;

namespace CodeChallenge.Application.Services.Change.Queries.GetTransactionById.Validator
{
    public class GetTransactionByIdQueryValidator : AbstractValidator<GetTransactionByIdQuery>
    {
        public GetTransactionByIdQueryValidator()
        {
            // Id da Transação
            RuleFor(m => m.Id)
                .NotNull().WithMessage("O Id da transação é obrigatório")
                .NotEmpty().WithMessage("O Id da transação é obrigatório")
                .GreaterThan(0).WithMessage("O Id tem de ser maior que 0!");
        }
    }
}
