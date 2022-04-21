using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mibocata.Infrastructure.Data.Models;

[Table("Stores")]
public class Store
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public List<Product> Products { get; set; }

    public bool AutoAccept { get; set; }

    public StoreLocation StoreLocation { get; set; }
}
