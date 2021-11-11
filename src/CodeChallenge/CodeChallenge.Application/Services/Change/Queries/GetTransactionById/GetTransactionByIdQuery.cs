using MediatR;

namespace CodeChallenge.Application.Services.Change.Queries.GetTransactionById
{
    /// <summary>
    /// Objecto de entrada na invocação do evento/query 'GetTransactionById'.
    /// Geralmente contem os dados que vêm do controlador e que são necessários à execução do evento/query.
    /// Ao herdar o IRequest faz match da consulta à resposta para o respetivo Handler com a interface IRequestHandler.
    /// </summary>
    public class GetTransactionByIdQuery : IRequest<GetTransactionByIdResponse>
    {
        /// <summary>
        /// O Id pelo qual pesquisar
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        public GetTransactionByIdQuery() { }
    }
}
