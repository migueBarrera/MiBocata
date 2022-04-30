using MiBocataAPI.DB;
using MiBocataAPI.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MiBocataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : MBControllerBase
{
    public TestController(IConfiguration configuration, MBDBContext context)
        : base(configuration, context)
    {
    }

    [HttpGet]
    public string GetProduct()
    {
        return "Hello";
    }

    [HttpGet("/create")]
    public string GetCreateDB()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        return "Database Created";
    }
}
