using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BasketService.API.IntegrationEvents.Events;
using BasketService.API.Model;
using BasketService.API.Models;
using BasketService.API.Services;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BasketService.API.Controllers
{
    [Route("api/v1/basket")]
    [ApiController]
    [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IIdentityService _identityService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<BasketController> _logger;

        public BasketController(
            ILogger<BasketController> logger,
            IBasketRepository repository,
            IIdentityService identityService,
            IEventBus eventBus)
        {
            _logger = logger;
            _repository = repository;
            _identityService = identityService;
            _eventBus = eventBus;
        }

        [HttpGet("get")]
        public string getall()
        {
            return "ddk";
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync(string id)
        {
            var basket = await _repository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync([FromBody] CustomerBasket value)
        {
            return Ok(await _repository.UpdateBasketAsync(value));
        }

        [Route("checkout")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CheckoutAsync([FromBody] BasketCheckout basketCheckout, [FromHeader(Name = "x-requestid")] string requestId)
        {

            var userId = basketCheckout.Buyer;

            basketCheckout.RequestId = (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty) ?
                guid : basketCheckout.RequestId;

            var basket = await _repository.GetBasketAsync(userId);

            if (basket == null)
            {
                return BadRequest();
            }

            var userName = _identityService.GetUserIdentity();

            var eventMessage = new OrderCreatedIntegrationEvent(userId, userName, basketCheckout.City, basketCheckout.Street,
                basketCheckout.State, basketCheckout.Country, basketCheckout.ZipCode, basketCheckout.CardNumber, basketCheckout.CardHolderName,
                basketCheckout.CardExpiration, basketCheckout.CardSecurityNumber, basketCheckout.CardTypeId, basketCheckout.Buyer, basketCheckout.RequestId, basket);

            // Once basket is checkout, sends an integration event to
            // ordering.api to convert basket to order and proceeds with
            // order creation process
            try
            {
                _eventBus.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName}", eventMessage.Id);

                throw;
            }

            return Accepted();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteBasketByIdAsync(string id)
        {
            await _repository.DeleteBasketAsync(id);
        }
    }
}
