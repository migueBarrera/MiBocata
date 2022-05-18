namespace Models.Core;

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

    public static OrderProduct Parse(Product product)
    {
        return new OrderProduct()
        {
            IdOriginalProduct = product.Id,
            Name = product.Name,
            UnitPrice = product.UnitPrice,
            Image = product.Image,
            Comment = string.Empty,
            Quantity = 1,
            Id = product.Id,
        };
    }
}