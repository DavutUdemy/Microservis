using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace BasketService.API.Extensions
{
    public static class RedisRegistration
    {
        public static ConnectionMultiplexer ConfigureRedis(this IServiceProvider services, IConfiguration configuration)
        {
            var redisConf = ConfigurationOptions.Parse(configuration["RedisSettings:ConnectionString"], true);
            redisConf.ResolveDns = true;
            return  ConnectionMultiplexer.Connect(redisConf);
        }
    }
}