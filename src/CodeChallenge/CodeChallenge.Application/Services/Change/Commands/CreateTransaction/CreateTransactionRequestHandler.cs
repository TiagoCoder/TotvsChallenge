using AutoMapper;
using CodeChallenge.Application.Common.Interfaces;
using CodeChallenge.Application.Entities.Transaction;
using CodeChallenge.Application.Helpers;
using CodeChallenge.Domain.Entities;
using CodeChallenge.Domain.Enumerations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CodeChallenge.Application.Services.Change.Commands.CreateTransaction
{
    public class CreateTransactionRequestHandler : IRequestHandler<CreateTransactionCommand, CreateTransactionResponse>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public CreateTransactionRequestHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<CreateTransactionResponse> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
        {
            if(command.ValueGiven - command.TotalValue <= 0.00M)
            {
                return new CreateTransactionResponse
                {
                    Success = false,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Error = "Não foi possível criar a transação. O valor recebido é inferior ou igual ao valor pedido."
                };
            }

            var billsFromDB = await _applicationDbContext
                .Bills
                .AsNoTracking()
                .Select(m => m.Value)
                .ToArrayAsync();

            var coinsFromDB = await _applicationDbContext
                .Coins
                .AsNoTracking()
                .Select(m => m.Value)
                .ToArrayAsync();

            var change = DynamicChangeHelper.GetChange(billsFromDB, (command.ValueGiven - command.TotalValue), PaymentTypes.BILL);

            decimal bills = 0.00M;

            foreach(var value in change)
            {
                bills += value.Value * value.Quantity;
            }

            decimal coinsAmount = (command.ValueGiven - command.TotalValue) - bills;

            if (coinsAmount > 0.00M)
                change.AddRange(DynamicChangeHelper.GetChange(coinsFromDB, coinsAmount, PaymentTypes.COIN));

            var newTransactionObj = new Transaction()
            {
                TotalValue = command.TotalValue,
                ValueGiven = command.ValueGiven,
                TransactionDate = DateTime.UtcNow
            };

            foreach (var transactionDetail in change)
                newTransactionObj.TransactionDetail.Add(item: _mapper.Map<TransactionDetail>(transactionDetail));

            var result = await _applicationDbContext.InsertAsync(newTransactionObj, cancellationToken);
            if (result > 0)
                return new CreateTransactionResponse
                {
                    Success = true,
                    HttpStatusCode = HttpStatusCode.OK,
                    Result = _mapper.Map<TransactionDTO>(newTransactionObj)
                };
            return new CreateTransactionResponse
            {
                Success = false,
                HttpStatusCode = HttpStatusCode.BadRequest,
                Error = "Não foi possível criar a transação. Algo correu mal."
            };
        }
    }
}
