using MediatR;

namespace CodeChallenge.Application.Services.Change.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<CreateTransactionResponse>
    {
        /// <summary>
        /// Nome da Empresa
        /// </summary>
        /// <example>CODIPOR - ASSOCIAÇÃO PORTUGUESA DE IDENTIFICAÇÃO E CODIFICAÇÃO DE PRODUTOS</example>
        public decimal TotalValue { get; set; }

        public decimal ValueGiven { get; set; }
    }
}
