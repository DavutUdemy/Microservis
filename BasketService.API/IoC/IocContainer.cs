using BasketService.API.Infrastructure.Repository;
using BasketService.API.IntegrationEvents.Events;
using BasketService.API.Model;
using BasketService.API.Services;
using EventBus;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BasketService.API.IoC
{
    public static class IocContainer
    {
        public static void ConfigureIOC(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, RedisBasketRepository>();
            services.AddTransient<IIdentityService, IdentityService>(); ;
            services.AddTransient<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>(); ;
           // services.AddTransient<IRabbitMQPersistentConnection, DefaultRabbitMQPersistentConnection>();
          //  services.AddSingleton<IRabbitMQPersistentConnection, DefaultRabbitMQPersistentConnection>(); ;

            services.AddTransient<OrderCreatedIntegrationEventHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }
    }
}
