using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ShopBridge.Common;
using ShopBridge.DataLayer;

namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public ShopBridgeDbContext db => HttpContext.RequestServices.GetService<ShopBridgeDbContext>();
        public Configuration Config => HttpContext.RequestServices.GetService<Configuration>();
    }
}
