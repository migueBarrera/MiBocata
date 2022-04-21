namespace MiBocataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : MBControllerBase
{
    public OrdersController(IConfiguration configuration, MBDBContext context)
        : base(configuration, context)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        return await _context.Order
                                .Include(s => s.OrderProducts)
                                .Include(s => s.Client)
                                .Include(s => s.Store)
                                .OrderByDescending(o => o.Created)
                                .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _context.Order
                                    .Include(s => s.OrderProducts)
                                    .Include(s => s.Client)
                                    .Include(s => s.Store)
                                    .Where(s => s.Id == id)
                                    .SingleAsync();

        if (order == null)
        {
            return NotFound();
        }

        return order;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(int id, Order order)
    {
        if (id != order.Id)
        {
            return BadRequest();
        }

        _context.Entry(order).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrderExists(id))
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
    public async Task<ActionResult<Order>> PostOrder(Order order)
    {
        _context.Order.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetOrder", new { id = order.Id }, order);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Order>> DeleteOrder(int id)
    {
        var order = await _context.Order.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        _context.Order.Remove(order);
        await _context.SaveChangesAsync();

        return order;
    }

    private bool OrderExists(int id)
    {
        return _context.Order.Any(e => e.Id == id);
    }
}
