using Mibocata.Infrastructure.Data.Models;

namespace Mibocata.Infrastructure.Helpers;

public static class ProductMapper
{
    public static OrderProduct Map(Product product)
    {
        return new OrderProduct()
        {
            IdOriginalProduct = product.Id,
            Name = product.Name,
            UnitPrice = product.UnitPrice,
            Image = product.Image,
            Comment = string.Empty,
            Quantity = 1,
        };
    }
}
