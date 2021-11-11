using AutoMapper;
using CodeChallenge.Application.Common.Interfaces;
using CodeChallenge.Application.Entities.Transaction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CodeChallenge.Application.Services.Change.Queries.GetTransactionById
{
    /// <summary>
    /// Query executada pelo MediatR para invocar o evento/query 'GetTransactionById'.
    /// </summary>
    public class GetTransactionByIdRequestHandler : IRequestHandler<GetTransactionByIdQuery, GetTransactionByIdResponse>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetTransactionByIdRequestHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<GetTransactionByIdResponse> Handle(GetTransactionByIdQuery query, CancellationToken cancellationToken)
        {
            var transactionObj = await _applicationDbContext
                 .Transactions
                 .Include(t => t.TransactionDetail)
                 .Where(t => t.Id == query.Id)
                 .AsNoTracking()
                 .FirstOrDefaultAsync();

            if (transactionObj != null && transactionObj.TransactionDetail.Count() != 0)
            {
                return new GetTransactionByIdResponse
                {
                    Success = true,
                    HttpStatusCode = HttpStatusCode.OK,
                    Result = _mapper.Map<TransactionDTO>(transactionObj)
                };
            }

            return new GetTransactionByIdResponse
            {
                Success = false,
                HttpStatusCode = HttpStatusCode.BadRequest,
                Error = $"Não foi possível encontrar a transação com o Id: {query.Id}. Algo correu mal."
            };
        }
    }
}
