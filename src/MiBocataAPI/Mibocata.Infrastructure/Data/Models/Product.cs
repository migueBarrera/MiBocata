using Models.Responses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mibocata.Infrastructure.Data.Models;

[Table("Products")]
public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public double UnitPrice { get; set; }

    public int StoreId { get; set; }
}
