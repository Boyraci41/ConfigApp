using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigManager.Abstract
{
    public  interface IConfigManager
    {
        Task<T> GetConfigValue<T>(string cacheKey);
        Task SetConfig(string key, object value);
        Task RemoveConfig(string key);

    }
}
