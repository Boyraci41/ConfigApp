using System.Threading.Tasks;

namespace ConfigApp.Services.Abstract
{
    public interface IConfigService
    {
        Task<T> GetConfig<T>(string key);
        Task SetConfig(string key, object value);
        Task RemoveConfig(string key);
    }
}
