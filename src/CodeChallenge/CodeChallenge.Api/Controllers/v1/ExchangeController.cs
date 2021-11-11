using CodeChallenge.Application.Entities.Transaction;
using CodeChallenge.Application.Helpers;
using CodeChallenge.Application.Services.Change.Commands.CreateTransaction;
using CodeChallenge.Application.Services.Change.Queries.GetTransactionById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CodeChallenge.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/exchange")]
    public class ExchangeController : Controller
    {
        private readonly IMediator _mediator;
        public ExchangeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region CreateTransaction
        /// <summary>
        /// Cria uma nova transaction
        /// </summary>
        /// <param name="createTransactionCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Retorna a informação da transaction</response>
        /// <response code="400">Erro inesperado</response>
        [HttpPost("getChange")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> CreateTransaction([FromBody] CreateTransactionCommand createTransactionCommand, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(createTransactionCommand, cancellationToken);

            // Transforma a propriedade result numa string
            var formattedResponse = ResponseMessageFormatterHelper.FormatResponse(response.Result);

            return Ok(formattedResponse);
        }
        #endregion

        #region GetTransactionById

        /// <summary>
        /// Devolve a informação de uma transação
        /// </summary>
        /// <param name="id" example="2">Id da Transação</param>
        /// <param name="cancellationToken"></param> 
        /// <response code="200">Retorna a informação da transação</response>
        /// <response code="404">Transação não encontrada</response>
        /// <response code="400">Erro inesperado</response>
        [HttpGet("/byId/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(TransactionDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetTransactionByIdQuery { Id = id };
            var response = await _mediator.Send(query, cancellationToken);

            return Ok(response);
        }
        #endregion
    }
}
