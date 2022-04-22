using Mibocata.Infrastructure.Data.Models.Mappers;

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
    public async Task<ActionResult<IEnumerable<StoreResponse>>> GetStore()
    {
        var stores = await _context.Store
                             .Include(s => s.Products)
                             .Include(s => s.StoreLocation)
                             .ToListAsync();

        return Ok(stores.Select(s => StoreMapper.Parse(s)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreResponse>> GetStore(int id)
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

        return Ok(StoreMapper.Parse(store));
    }

    [HttpGet("{id}/orders")]
    public async Task<ActionResult<IEnumerable<OrdersResponse>>> GetStoreOrders(int id)
    {
        var orders = await _context.Order
                                .Where(o => o.StoreId == id)
                                .Include(s => s.OrderProducts)
                                .Include(s => s.Client)
                                .Include(s => s.Store)
                                .ToListAsync();

        return Ok(orders
            .Select(o => OrderMapper.Parse(o)));
    }
    
    [HttpGet("{id}/products")]
    public async Task<ActionResult<IEnumerable<ProductsResponse>>> GetStoreProducts(int id)
    {
        var products = await _context.Product
                                .Where(o => o.StoreId == id)
                                .ToListAsync();

        return Ok(products.Select(p => StoreMapper.Parse(p)));
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutStore(int id, StoreUpdateRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }

        var store = new Store()
        {
            Id = id,
            AutoAccept = request.AutoAccept,
            Image = request.Image,
            Name = request.Name,
            //TODO review
            //Products = request.Products,
            //StoreLocation = request.StoreLocation,
        };

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
    public async Task<ActionResult<StoreResponse>> PostStore(StoreCreateRequest request)
    {
        var idEmpleado = 0;
        var currentUser = HttpContext.User;
        if (currentUser.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
        {
            idEmpleado = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
        var user = _context.Shopkeeper.Where(a => a.Id == idEmpleado).Single();

        var store = StoreMapper.Parse(request);
        _context.Store.Add(store);
        await _context.SaveChangesAsync();

        user.IdStore = store.Id;
        _context.Shopkeeper.Update(user);

        await _context.SaveChangesAsync();

        return CreatedAtAction("GetStore", new { id = store.Id }, StoreMapper.Parse(store));
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
