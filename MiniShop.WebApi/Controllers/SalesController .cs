using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniShop.Domain.Entities;
using MiniShop.Infrastructure.Persistence;
using MiniShop.WebApi.Models.DTOs;

namespace MiniShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public SalesController(AppDbContext db)
        {
            _db = db;
        }

        // POST /api/sales
        [HttpPost]
        public IActionResult CreateSale([FromBody] SaleCreateRequest request)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (userIdClaim == null)
                return Unauthorized("Token inválido o expirado.");

            var userId = int.Parse(userIdClaim);

            if (request.Items.Count == 0)
                return BadRequest("Debe incluir al menos un producto.");

            using var transaction = _db.Database.BeginTransaction();

            var sale = new Sale
            {
                UserId = userId,
                Date = DateTime.UtcNow
            };

            decimal total = 0;
            foreach (var item in request.Items)
            {
                var product = _db.Products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product == null)
                    return BadRequest($"El producto con ID {item.ProductId} no existe.");

                if (item.Quantity <= 0)
                    return BadRequest($"La cantidad para el producto {product.Name} debe ser mayor que cero.");

                if (product.Stock < item.Quantity)
                    return BadRequest($"Stock insuficiente para el producto '{product.Name}'. " +
                                      $"Disponible: {product.Stock}, solicitado: {item.Quantity}.");

                // Crear el ítem de venta
                var saleItem = new SaleItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    Subtotal = product.Price * item.Quantity
                };

                total += saleItem.Subtotal;
                sale.Items.Add(saleItem);

                // Descontar stock
                product.Stock -= item.Quantity;
            }

            _db.Sales.Add(sale);
            _db.SaveChanges();
            transaction.Commit();

            return Ok(new
            {
                message = "Venta registrada correctamente",
                saleId = sale.Id,
                total
            });
        }

        // GET /api/sales
        [HttpGet]
        public IActionResult GetAll()
        {
            var sales = _db.Sales
                .Include(s => s.User)
                .Include(s => s.Items)
                .ThenInclude(i => i.Product)
                .OrderByDescending(s => s.Date)
                .Take(20)
                .Select(s => new SaleResponse
                {
                    Id = s.Id,
                    Date = s.Date,
                    Username = s.User!.Username,
                    Total = s.Items.Sum(i => i.Subtotal),
                    Items = s.Items.Select(i => new SaleItemResponse
                    {
                        ProductId = i.ProductId,
                        ProductName = i.Product!.Name,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        Subtotal = i.Subtotal
                    }).ToList()
                })
                .ToList();

            return Ok(sales);
        }

        // GET /api/sales/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var sale = _db.Sales
                .Include(s => s.User)
                .Include(s => s.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(s => s.Id == id);

            if (sale == null)
                return NotFound();

            var response = new SaleResponse
            {
                Id = sale.Id,
                Date = sale.Date,
                Username = sale.User!.Username,
                Total = sale.Items.Sum(i => i.Subtotal),
                Items = sale.Items.Select(i => new SaleItemResponse
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product!.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Subtotal = i.Subtotal
                }).ToList()
            };

            return Ok(response);
        }
    }
}
