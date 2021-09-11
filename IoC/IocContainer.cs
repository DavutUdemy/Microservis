using Core.Utilities.Security.JWT;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.DB;
using Service;

namespace IoC
{
    public static class IocContainer
    {
        public static void ConfigureIOC(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserManager>();
            services.AddTransient<IAuthService, AuthManager>(); 
            services.AddTransient<ITokenHelper, JwtHelper>();
            ;

            services.AddDbContext<OnionDBContext>();
        }
    }

}