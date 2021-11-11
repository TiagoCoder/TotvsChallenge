using CodeChallenge.Application.Entities.Messages.Response;
using CodeChallenge.Application.Entities.Transaction;

namespace CodeChallenge.Application.Services.Change.Commands.CreateTransaction
{
    /// <summary>
    /// Objecto de resposta ao command 'CreateTransaction' gerada pelo MediatR.
    /// </summary>
    public record CreateTransactionResponse : Response<TransactionDTO>
    {
        public CreateTransactionResponse() { }
    }
}
