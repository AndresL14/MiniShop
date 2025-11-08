using Microsoft.AspNetCore.Mvc;

namespace MiniShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping() => Ok(new { service = "products", status = "ok" });
    }
}
