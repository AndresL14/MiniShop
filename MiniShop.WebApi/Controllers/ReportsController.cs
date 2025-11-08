using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniShop.Infrastructure.Persistence;

namespace MiniShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ReportsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("sales")]
        public IActionResult GetSalesReport([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var sales = _db.Sales
                .Include(s => s.Items)
                .Include(s => s.User)
                .Where(s => s.Date >= from && s.Date <= to)
                .Select(s => new
                {
                    s.Id,
                    s.Date,
                    User = s.User!.Username,
                    Total = s.Items.Sum(i => i.Subtotal),
                    ItemsCount = s.Items.Count
                })
                .OrderBy(s => s.Date)
                .ToList();

            var totalGeneral = sales.Sum(s => s.Total);

            return Ok(new
            {
                from,
                to,
                totalSales = sales.Count,
                totalGeneral,
                sales
            });
        }
    }
}
