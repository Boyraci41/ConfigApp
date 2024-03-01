using ConfigApp.Services.Abstract;
using ConfigManager.Abstract;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigApp.Services.Concrete
{
    public class ConfigService : IConfigService
    {
        private readonly IConfigManager _configManager;
        public ConfigService(IConfigManager configManager)
        {
            _configManager = configManager;
        }

        public async Task<T> GetConfig<T>(string key)
        {
            var value = await _configManager.GetConfigValue<T>(key);
            return value == null ? default(T) : value;
        }

        public async Task RemoveConfig(string key)
        {
            await _configManager.RemoveConfig(key);
        }


        public async Task SetConfig(string key, object value)
        {
            var strValue = JsonSerializer.Serialize(value);
            await _configManager.SetConfig(key,strValue);
        }

       
    }
}
