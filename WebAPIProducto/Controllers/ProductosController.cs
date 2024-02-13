using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks; 
using WebAPIProducto.Models;
using WebAPIProducto.Data;

namespace MyApp.Namespace
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ILogger<ProductosController> _logger; 
        private readonly DataContext _context;

        public ProductosController(ILogger<ProductosController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetProductos")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetProducto")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }
            return producto;
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> Post(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetProducto", new { id = producto.Id}, producto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }
            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Producto>> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return producto;
        }
    }
}
