using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiBocataAPI.DB;
using Models;
using MiBocataAPI.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using MiBocataAPI.Framework;

namespace MiBocataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopkeepersController : MBControllerBase
    {
        public ShopkeepersController(IConfiguration configuration, MBDBContext context) 
            : base(configuration, context)
        {
        }

        // GET: api/Shopkeepers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shopkeeper>>> GetShopkeeper()
        {
            return await _context.Shopkeeper.ToListAsync();
        }

        // GET: api/Shopkeepers/5
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

        // POST: api/Shopkeepers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult<Client>> PostSignUp(Shopkeeper shopkeeper)
        {
            if (string.IsNullOrWhiteSpace(shopkeeper.Email) ||
                string.IsNullOrWhiteSpace(shopkeeper.Password))
            {
                return BadRequest("Required parameters");
            }

            if (ShopkeeperExists(shopkeeper.Email))
            {
                return Conflict();
            }

            shopkeeper.Password = Hash.Create(shopkeeper.Password);

            _context.Shopkeeper.Add(shopkeeper);
            await _context.SaveChangesAsync();

            shopkeeper.Password = string.Empty;
            shopkeeper.Token = JwtSecurityTokenHelper.BuildToken(configuration["Jwt:Key"], shopkeeper);

            return CreatedAtAction("GetShopkeeper", new { id = shopkeeper.Id }, shopkeeper);
        }
        
        // POST: api/Shopkeepers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Route("SignIn")]
        public ActionResult<Shopkeeper> PostSignIn(Shopkeeper shopkeeper)
        {
            var user = _context.Shopkeeper.Where(a => a.Email == shopkeeper.Email).Single();
            if (user == null)
            {
                return NotFound();
            }

            if (!Hash.Validate(shopkeeper.Password, user.Password))
            {
                return NotFound();
            }

            user.Password = string.Empty;
            user.Token = JwtSecurityTokenHelper.BuildToken(configuration["Jwt:Key"], user);

            return user;
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

        // DELETE: api/Shopkeepers/5
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
}
