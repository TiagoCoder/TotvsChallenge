using MediatR;

namespace CodeChallenge.Application.Services.Change.Commands.CreateTransaction
{
    /// <summary>
    /// Objecto de entrada na invocação do evento/command 'CreateTransaction'.
    /// Geralmente contem os dados que vêm do controlador e que são necessários à execução do evento/query.
    /// Ao herdar o IRequest faz match da consulta à resposta para o respetivo Handler com a interface IRequestHandler.
    /// </summary>
    public class CreateTransactionCommand : IRequest<CreateTransactionResponse>
    {
        public CreateTransactionCommand() { }

        /// <summary>
        /// Guarda o valor total da transação
        /// </summary>
        /// <example>€200.00</example>
        public decimal TotalValue { get; set; }
        /// <summary>
        /// Guarda o valor entregue
        /// </summary>
        /// <example>220.00</example>
        public decimal ValueGiven { get; set; }
    }
}
