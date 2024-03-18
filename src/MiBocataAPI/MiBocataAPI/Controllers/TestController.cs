using Azure.Core;
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

        var store = new Store()
        {
            Id = 1,
            AutoAccept = true,
            Image = string.Empty,
            Name = "Test store",
            //TODO review
            //Products = request.Products,
            StoreLocation = StoreLocation.Parse(new StoreLocationRequest()),
        };

        _context.Add(store);
        _context.Add(new Shopkeeper() { Email = "shopkeeper@email.com", Password = Hash.Create("123456"), IdStore = store.Id});
        _context.Add(new Client() { Email = "shopkeeper@email.com", Password = Hash.Create("123456")});
        _context.SaveChanges();

        return "Database Created";
    }
}
