using CodeChallenge.Application.Helpers;
using CodeChallenge.Application.Services.Change.Commands.CreateTransaction;
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
        //// GET: ExchangeController
        //public async Task<ActionResult> GetBills(CancellationToken cancellationToken)
        //{
        //    return View();
        //}

        //// GET: ExchangeController/Details/5
        //public async Task<ActionResult> GetCoins(CancellationToken cancellationToken)
        //{
        //    return View();
        //}

        //// GET: ExchangeController/Create
        //public async Task<ActionResult> GetTransactionById([FromRoute] int id, CancellationToken cancellationToken)
        //{
        //    return View();
        //}

        /// <summary>
        /// Cria uma nova transaction
        /// </summary>
        /// <param name="createTransactionCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Retorna a informação da transaction</response>
        /// <response code="400">Erro inesperado</response>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> CreateTransaction([FromBody] CreateTransactionCommand createTransactionCommand, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(createTransactionCommand, cancellationToken);

            var formattedResponse = ResponseMessageFormatterHelper.FormatResponse(response.Result);

            return Ok(formattedResponse);
        }

        //// POST: ExchangeController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> CreateBill([FromBody] CreateBillCommand createBillCommand, CancellationToken cancellationToken)
        //{
        //        var response = await _mediator.Send(createBillCommand, cancellationToken);
        //        return Ok(response);
        //}

        //// POST: ExchangeController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> CreateCoin([FromBody] CreateCoinCommand createCoinCommand, CancellationToken cancellationToken)
        //{
        //        var response = await _mediator.Send(createCoinCommand, cancellationToken);

        //        return Ok(response);
        //}
    }
}
