namespace Models.Requests;

public class UpdateOrderRequest
{
    public int Id { get; set; }

    public double Amount { get; set; }

    public OrderStates State { get; set; } = OrderStates.DEFAULT;

    public DateTime PickupTime { get; set; }

    public DateTime Created { get; set; }

    public string Comment { get; set; } = string.Empty;

    public int ClientId { get; set; }

    public int StoreId { get; set; }

    public static UpdateOrderRequest Parse(Order o)
    {
        return new UpdateOrderRequest()
        {
            Amount = o.Amount,
            State = o.State,
            PickupTime = o.PickupTime,
            Created = o.Created,
            Comment = o.Comment,
            ClientId = o.ClientId,
            StoreId = o.StoreId,
            Id = o.Id,
        };
    }
}
