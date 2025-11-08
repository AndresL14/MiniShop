using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Domain.Entities;
using MiniShop.Infrastructure.Persistence;
using MiniShop.WebApi.Models.DTOs;

namespace MiniShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ProductsController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        // GET /api/products?page=1&pageSize=10&search=texto
        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var query = _db.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.Name.Contains(search));

            var totalItems = query.Count();
            var items = query
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    ImageUrl = p.ImageUrl
                })
                .ToList();

            return Ok(new
            {
                page,
                pageSize,
                totalItems,
                totalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                items
            });
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromForm] ProductCreateRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };

            if (request.Image != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Image.FileName)}";
                var filePath = Path.Combine(_env.WebRootPath, "images", fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                request.Image.CopyTo(stream);
                product.ImageUrl = $"/images/{fileName}";
            }

            _db.Products.Add(product);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromForm] ProductUpdateRequest request)
        {
            var product = _db.Products.Find(id);
            if (product == null)
                return NotFound();

            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;

            if (request.Image != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Image.FileName)}";
                var filePath = Path.Combine(_env.WebRootPath, "images", fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                request.Image.CopyTo(stream);
                product.ImageUrl = $"/images/{fileName}";
            }

            _db.SaveChanges();
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
                return NotFound();

            _db.Products.Remove(product);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
