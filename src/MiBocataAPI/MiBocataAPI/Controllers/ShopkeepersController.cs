namespace MiBocataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShopkeepersController : MBControllerBase
{
    public ShopkeepersController(
        IConfiguration configuration, 
        MBDBContext context) 
        : base(configuration, context)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Shopkeeper>>> GetShopkeeper()
    {
        return await _context.Shopkeeper.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Shopkeeper>> GetShopkeeper(string id)
    {
        var shopkeeper = await _context.Shopkeeper.FindAsync(id);

        if (shopkeeper == null)
        {
            return NotFound();
        }

        return shopkeeper;
    }

    [HttpPost]
    [Route("SignUp")]
    public async Task<ActionResult<ShopkeeperSignUpResponse>> PostSignUp(ShopkeeperSignUpRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Required parameters");
        }

        if (ShopkeeperExists(request.Email))
        {
            return Conflict();
        }

        request.Password = Hash.Create(request.Password);

        var shopkeeper = new Shopkeeper()
        {
            Email = request.Email,
            Password = request.Password,
        };

        _context.Shopkeeper.Add(shopkeeper);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetShopkeeper", new { id = shopkeeper.Id }, new ShopkeeperSignInResponse()
        {
            Id = shopkeeper.Id,
            Email = shopkeeper.Email,
            Name = shopkeeper.Name,
            Token = JwtSecurityTokenHelper.BuildToken(configuration["Jwt:Key"], shopkeeper),
        });
    }
    
    [HttpPost]
    [Route("SignIn")]
    public ActionResult<ShopkeeperSignInResponse> PostSignIn(ShopkeeperSignInRequest request)
    {
        var shopkeeper = _context
            .Shopkeeper
            .Where(a => a.Email == request.Email)
            .FirstOrDefault();
        if (shopkeeper == null)
        {
            return NotFound();
        }

        if (!Hash.Validate(request.Password, shopkeeper.Password))
        {
            return NotFound();
        }

        return Ok(new ShopkeeperSignInResponse()
        {
            Id = shopkeeper.Id,
            Email = shopkeeper.Email,
            Name = shopkeeper.Name,
            IdStore = shopkeeper.IdStore,
            Token = JwtSecurityTokenHelper.BuildToken(configuration["Jwt:Key"], shopkeeper),
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

        var url = await ImageHelper.UploadImage("shopkeepers", file);

        if (!string.IsNullOrWhiteSpace(url))
        {
            var shopkeeper = await _context.Shopkeeper.FindAsync(id);
            //todo add image shopkeeper.I
            await UpdateShopkeepers(id, shopkeeper);
            return Ok(url);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public Task<IActionResult> PutShopkeeper(int id, Shopkeeper shopkeeper)
    {
        return UpdateShopkeepers(id, shopkeeper);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Shopkeeper>> DeleteShopkeeper(string id)
    {
        var shopkeeper = await _context.Shopkeeper.FindAsync(id);
        if (shopkeeper == null)
        {
            return NotFound();
        }

        _context.Shopkeeper.Remove(shopkeeper);
        await _context.SaveChangesAsync();

        return shopkeeper;
    }

    private bool ShopkeeperExists(int id)
    {
        return _context.Shopkeeper.Any(e => e.Id == id);
    }
    
    private bool ShopkeeperExists(string email)
    {
        return _context.Shopkeeper.Any(e => e.Email == email);
    }

    private async Task<IActionResult> UpdateShopkeepers(int id, Shopkeeper shopkeeper)
    {
        if (id != shopkeeper.Id)
        {
            return BadRequest();
        }

        _context.Entry(shopkeeper).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShopkeeperExists(id))
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
}
