using System.ComponentModel.DataAnnotations.Schema;

namespace Mibocata.Infrastructure.Data.Models;

[Table("OrderProducts")]
public class OrderProduct
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; } = string.Empty;

    public string Comment { get; set; } = string.Empty;

    public double UnitPrice { get; set; }

    public int Quantity { get; set; }

    public int OrderId { get; set; }

    public int IdOriginalProduct { get; set; }
}
