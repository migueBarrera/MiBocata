using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiBocataAPI.DB;
using Models;
using MiBocataAPI.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using MiBocataAPI.Helpers;

namespace MiBocataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : MBControllerBase
    {
        public ProductsController(IConfiguration configuration, MBDBContext context)
            : base(configuration, context)
        {
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _context.Product.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public Task<IActionResult> PutProduct(int id, Product product)
        {
            return UpdateProducts(id, product);
        }

        // POST: api/Products
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            //TODO comprobar que el producto el id de store es el del token
            _context.Product.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpPost]
        [Route("{id}/image")]
        public async Task<IActionResult> PostUploadImage(int id, IFormFile file)
        {
            if (file == null)
            {
                return BadRequest();
            }

            if (!ProductExists(id))
            {
                return BadRequest();
            }

            var url = await ImageHelper.UploadImage("products", file);

            if (string.IsNullOrWhiteSpace(url))
            {
                return BadRequest();
            }

            var product = await _context.Product.FindAsync(id);
            product.Image = url;
            await UpdateProducts(id, product);
            return Ok(url);
        }

        private async Task<IActionResult> UpdateProducts(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(string id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
