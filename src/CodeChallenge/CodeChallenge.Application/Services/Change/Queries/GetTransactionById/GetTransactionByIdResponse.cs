using CodeChallenge.Application.Entities.Messages.Response;
using CodeChallenge.Application.Entities.Transaction;

namespace CodeChallenge.Application.Services.Change.Queries.GetTransactionById
{
    public record GetTransactionByIdResponse : Response<TransactionDTO>
    {
        public GetTransactionByIdResponse() { }
    }
}
