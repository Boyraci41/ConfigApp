using ConfigManager.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigManager.Extensions
{
    public static class CacheManagerExtensions
    {
        public static void CacheManagerRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options => { options.Configuration = configuration["RedisCacheUrl"]; });
            services.AddScoped<IConfigManager, Concrete.ConfigManager>();
        }
    }
}
