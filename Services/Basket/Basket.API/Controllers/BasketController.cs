using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IMediator _mediator;
        public BasketController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("GetBasket/{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasketByUserName(string userName)
        {
            var query = new GetBasketByUserNameQuery(userName);
            var basket = await _mediator.Send(query);
            return Ok(basket);
        }

        [HttpPost("CreateBasket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket([FromBody] CreateShoppingCartCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpDelete("DeleteBasket/{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Unit>> DeleteBasketByUserName(string userName)
        {
            var command = new DeleteBasketByUserNameCommand(userName);
            var res = await _mediator.Send(command);
            return Ok(Unit.Value);
        }



    }
}
