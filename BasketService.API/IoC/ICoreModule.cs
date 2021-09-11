using Microsoft.Extensions.DependencyInjection;

namespace BasketService.API.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection);
    }

}