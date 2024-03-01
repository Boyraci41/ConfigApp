using ConfigApp.Models;
using ConfigApp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace ConfigApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _configService;

        public ConfigController(IConfigService configService)
        {
            _configService = configService;
        }

        [HttpGet]
        [Route("GetConfig")]
        public async Task<IActionResult> Get(string key)
        {
            var value = await _configService.GetConfig<string>(key);
            return Ok(value);
        }

        [HttpPost]
        [Route("SetConfig")]
        public async Task<IActionResult> Set(SetConfigModel model)
        {
            await _configService.SetConfig(model.Key,model.Value);
            return Ok();
        }


        [HttpDelete]
        [Route("RemoveConfig")]
        public async Task<IActionResult> Remove(string key)
        {
            await _configService.RemoveConfig(key);
            return Ok();
        }





    }
}
