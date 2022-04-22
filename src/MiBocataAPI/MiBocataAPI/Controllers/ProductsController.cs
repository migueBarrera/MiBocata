using Mibocata.Infrastructure.Data.Models.Mappers;
using Models.Responses;

namespace MiBocataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : MBControllerBase
{
    public ProductsController(
        IConfiguration configuration, 
        MBDBContext context)
        : base(configuration, context)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
    {
        return await _context.Product.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductsResponse>> GetProduct(string id)
    {
        var product = await _context.Product.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(ProductMapper.Parse(product));
    }

    [HttpPut("{id}")]
    public Task<IActionResult> PutProduct(int id, Product product)
    {
        return UpdateProducts(id, product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductsResponse>> PostProduct(ProductCreateRequest request)
    {
        //TODO comprobar que el producto el id de store es el del token
        var product = ProductMapper.Parse(request);
        _context.Product.Add(product);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (ProductExists(request.Id))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetProduct", new { id = product.Id }, ProductMapper.Parse(product));
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
