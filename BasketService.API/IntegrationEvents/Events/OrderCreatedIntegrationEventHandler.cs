using System;
using System.Threading.Tasks;
using BasketService.API.Model;
using EventBus.Abstractions;
using Microsoft.Extensions.Logging;

namespace BasketService.API.IntegrationEvents.Events
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly IBasketRepository _repository;
        private readonly ILogger<OrderCreatedIntegrationEvent> _logger;

        public OrderCreatedIntegrationEventHandler(
            IBasketRepository repository,
            ILogger<OrderCreatedIntegrationEvent> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
           
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id,@event);

                await _repository.DeleteBasketAsync(@event.UserId.ToString());
            }
        }
    
}