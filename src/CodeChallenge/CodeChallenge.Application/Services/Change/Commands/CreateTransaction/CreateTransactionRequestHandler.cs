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
    /// <summary>
    /// Comando executado pelo MediatR para invocar o evento/command 'CreateTransaction'.
    /// </summary>
    public class CreateTransactionRequestHandler : IRequestHandler<CreateTransactionCommand, CreateTransactionResponse>
    {
        #region Properties
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CreateTransactionRequestHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<CreateTransactionResponse> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
        {
            // Valida se existe troco
            if(command.ValueGiven - command.TotalValue <= 0.00M)
            {
                return new CreateTransactionResponse
                {
                    Success = false,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Error = "Não foi possível criar a transação. O valor recebido é inferior ou igual ao valor pedido."
                };
            }

            // Recebe os valores do tipo notas da Base de dados
            var billsFromDB = await _applicationDbContext
                .Bills
                .AsNoTracking()
                .Select(m => m.Value)
                .ToArrayAsync();

            // Recebe os valores do tipo moedas da Base de dados
            var coinsFromDB = await _applicationDbContext
                .Coins
                .AsNoTracking()
                .Select(m => m.Value)
                .ToArrayAsync();

            // Executa o algoritmo que calcula o troco para o primeiro set de valores, neste caso, notas
            var change = DynamicChangeHelper.GetChange(billsFromDB, (command.ValueGiven - command.TotalValue), PaymentTypes.BILL);

            // Inicializa uma variável que irá guardar o troco em notas
            decimal bills = 0.00M;

            // Calcula o valor devolvido como troco de notas
            foreach(var value in change)
            {
                bills += value.Value * value.Quantity;
            }

            // Guarda o troco a devolver em moedas
            decimal coinsAmount = (command.ValueGiven - command.TotalValue) - bills;

            // Se este montante for superior a 0, executa o algoritmo que calcula os valores a receber em moedas
            if (coinsAmount > 0.00M)
                change.AddRange(DynamicChangeHelper.GetChange(coinsFromDB, coinsAmount, PaymentTypes.COIN));

            // Cria um novo objeto do tipo Transaction e guarda os valores nas respetivas variáveis
            var newTransactionObj = new Transaction()
            {
                TotalValue = command.TotalValue,
                ValueGiven = command.ValueGiven,
                TransactionDate = DateTime.UtcNow
            };

            // Guarda os detalhes na propriedade Detalhes do objeto Pai
            foreach (var transactionDetail in change)
                newTransactionObj.TransactionDetail.Add(item: _mapper.Map<TransactionDetail>(transactionDetail));

            // Insere a transação na Base de dados e retorna um objeto do tipo TransactionDTO se for bem sucedido
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
        #endregion
    }
}
