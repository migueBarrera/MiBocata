namespace Models.Responses;

public class OrdersResponse
{
    public int Id { get; set; }

    public double Amount { get; set; }

    public OrderStates State { get; set; } = OrderStates.DEFAULT;

    public DateTime PickupTime { get; set; }

    public DateTime Created { get; set; }

    public string Comment { get; set; } = string.Empty;

    public int ClientId { get; set; }

    public Client Client { get; set; }

    public int StoreId { get; set; }

    public IEnumerable<OrderProduct> OrderProducts { get; set; }

    public StoreResponse Store { get; set; }

    public static Order Parse(OrdersResponse o)
    {
        return new Order()
        {
            Amount = o.Amount,
            State = o.State,
            PickupTime = o.PickupTime,
            Created = o.Created,
            Comment = o.Comment,
            ClientId = o.ClientId,
            StoreId = o.StoreId,
            Id = o.Id,
            Client = o.Client,
            Store = o.Store != null
                ? StoreResponse.Parse(o.Store)
                : new Core.Store(),
            OrderProducts = o.OrderProducts,
        };
    }
}
