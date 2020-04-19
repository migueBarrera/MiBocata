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
using System.IO;
using MiBocataAPI.Framework;

namespace MiBocataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : MBControllerBase
    {
        public ClientsController(IConfiguration configuration, MBDBContext context)
            : base(configuration, context)
        {
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClient()
        {
            return await _context.Client.ToListAsync();
        }

        // GET: api/Clients/5
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
        
        // GET: api/Clients/5/Orders
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

        // POST: api/Clients/SignUp
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult<Client>> PostSignUp(Client client)
        {
            if (string.IsNullOrWhiteSpace(client.Name) ||
                string.IsNullOrWhiteSpace(client.Password))
            {
                return BadRequest("Required parameters");
            }

            if (ClientExists(client.Email))
            {
                return Conflict();
            }

            client.Password = Hash.Create(client.Password);

            _context.Client.Add(client);
            await _context.SaveChangesAsync();

            //client.Password = string.Empty;
            client.Token = JwtSecurityTokenHelper.BuildToken(configuration["Jwt:Key"], client);

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        // POST: api/Clients
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Route("SignIn")]
        public ActionResult<Client> PostSignIn(Client client)
        {
            var user = _context.Client.Where(a => a.Email == client.Email).Single();
            if (user == null)
            {
                return NotFound();
            }

            if (!Hash.Validate(client.Password, user.Password))
            {
                return NotFound();
            }

            //user.Password = string.Empty;
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

            var url = await ImageHelper.UploadImage("clients", file);

            if (!string.IsNullOrWhiteSpace(url))
            {
                var client = await _context.Client.FindAsync(id);
                client.Image = url;
                await UpdateClients(id, client);
                return Ok(url);
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<IActionResult> UpdateClients(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // PUT: api/Clients/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public Task<IActionResult> PutClient(int id, Client client)
        {
            return UpdateClients(id, client);
        }

        // DELETE: api/Clients/5
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
    }
}
