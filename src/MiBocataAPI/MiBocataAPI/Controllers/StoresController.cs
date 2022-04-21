namespace MiBocataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoresController : MBControllerBase
{
    public StoresController(IConfiguration configuration, MBDBContext context)
        : base(configuration, context)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Store>>> GetStore()
    {
        return await _context.Store
                             .Include(s => s.Products)
                             .Include(s => s.StoreLocation)
                             .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Store>> GetStore(int id)
    {
        var store = await _context.Store
                                  .Include(s => s.Products)
                                  .Include(s => s.StoreLocation)
                                  .Where(s => s.Id == id)
                                  .SingleAsync();

        if (store == null)
        {
            return NotFound();
        }

        return store;
    }

    [HttpGet("{id}/orders")]
    public async Task<ActionResult<IEnumerable<Order>>> GetStoreOrders(int id)
    {
        return await _context.Order
                                .Where(o => o.StoreId == id)
                                .Include(s => s.OrderProducts)
                                .Include(s => s.Client)
                                .Include(s => s.Store)
                                .ToListAsync();
    }
    
    [HttpGet("{id}/products")]
    public async Task<ActionResult<IEnumerable<Product>>> GetStoreProducts(int id)
    {
        return await _context.Product
                                .Where(o => o.StoreId == id)
                                .ToListAsync();
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutStore(int id, Store store)
    {
        if (id != store.Id)
        {
            return BadRequest();
        }

        _context.Entry(store).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StoreExists(id))
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

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Store>> PostStore(Store store)
    {
        var idEmpleado = 0;
        var currentUser = HttpContext.User;
        if (currentUser.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
        {
            idEmpleado = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
        var user = _context.Shopkeeper.Where(a => a.Id == idEmpleado).Single();

        _context.Store.Add(store);
        await _context.SaveChangesAsync();

        user.IdStore = store.Id;
        _context.Shopkeeper.Update(user);

        await _context.SaveChangesAsync();


        return CreatedAtAction("GetStore", new { id = store.Id }, store);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Store>> DeleteStore(int id)
    {
        var store = await _context.Store.FindAsync(id);
        if (store == null)
        {
            return NotFound();
        }

        _context.Store.Remove(store);
        await _context.SaveChangesAsync();

        return store;
    }

    private bool StoreExists(int id)
    {
        return _context.Store.Any(e => e.Id == id);
    }
}
