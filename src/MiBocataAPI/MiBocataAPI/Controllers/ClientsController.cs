namespace MiBocataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : MBControllerBase
{
    public ClientsController(IConfiguration configuration, MBDBContext context)
        : base(configuration, context)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetClient()
    {
        return await _context.Client.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClient(int id)
    {
        var client = await _context.Client.FindAsync(id);

        if (client == null)
        {
            return NotFound();
        }

        return client;
    }

    [HttpGet("{id}/orders")]
    public async Task<ActionResult<IEnumerable<Order>>> GetClientOrders(int id)
    {
        return await _context.Order
                                .Where(o => o.ClientId == id)
                                .Include(o => o.OrderProducts)
                                .Include(o => o.Client)
                                .Include(o => o.Store)
                                    .ThenInclude(s => s.StoreLocation)
                                .ToListAsync();
    }

    [HttpPost]
    [Route("SignUp")]
    public async Task<ActionResult<ClientSignUpResponse>> PostSignUp(ClientSignUpRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Required parameters");
        }

        if (ClientExists(request.Email))
        {
            return Conflict();
        }

        var client = new Client()
        {
            Name = request.Name,
            Password = Hash.Create(request.Password),
            Email = request.Email,
        };

        _context.Client.Add(client);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetClient", new { id = client.Id }, new ClientSignUpResponse()
        {
            Id = client.Id,
            Email = request.Email,
            Name = request.Name,
            Token = JwtSecurityTokenHelper.BuildToken(configuration["Jwt:Key"], client),
        });
    }

    [HttpPost]
    [Route("SignIn")]
    public ActionResult<ClientSignInResponse> PostSignIn(ClientSignInRequest request)
    {
        var client = _context.Client
            .Where(a => a.Email == request.Email)
            .Single();
        if (client == null)
        {
            return NotFound();
        }

        if (!Hash.Validate(request.Password, client.Password))
        {
            return NotFound();
        }

        return Ok(new ClientSignInResponse()
        {
            Id = client.Id,
            Email = client.Email,
            Name = client.Name,
            Token = JwtSecurityTokenHelper.BuildToken(configuration["Jwt:Key"], client),
        });
    }

    [HttpPost]
    [Route("{id}/image")]
    public async Task<IActionResult> PostUploadImage(int id, IFormFile file)
    {
        if (file == null)
        {
            return BadRequest();
        }

        var url = await ImageHelper.UploadImage("clients", file);

        if (!string.IsNullOrWhiteSpace(url))
        {
            var client = await _context.Client.FindAsync(id);
            client.Image = url;
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(url);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutClient(int id, ClientUpdateRequest request)
    {
        var client = await _context.Client.FindAsync(id);
        if(client == null)
        {
            return NotFound();
        }

        client.Phone = request.Phone;
        client.Email = request.Email;
        client.Name = request.Name;

        _context.Entry(client).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Client>> DeleteClient(int id)
    {
        var client = await _context.Client.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }

        _context.Client.Remove(client);
        await _context.SaveChangesAsync();

        return client;
    }

    private async Task<IActionResult> UpdateClients(Client client)
    {
        try
        {
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClientExists(client.Id))
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

    private bool ClientExists(int id)
    {
        return _context.Client.Any(e => e.Id == id);
    }

    private bool ClientExists(string email)
    {
        return _context.Client.Any(e => e.Email == email);
    }
}
