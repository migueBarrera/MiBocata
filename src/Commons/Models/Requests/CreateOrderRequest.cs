using Models.Core;
using System;
using System.Collections.Generic;

namespace Models.Requests;

public class CreateOrderRequest
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

    public Store Store { get; set; }

    public static CreateOrderRequest Parse(Order order)
    {
        return new CreateOrderRequest 
        {
            Id = order.Id,
            Amount = order.Amount,
            State = order.State,
            PickupTime = order.PickupTime,
            Created = order.Created,
            Comment = order.Comment,
            ClientId = order.ClientId,
            StoreId = order.StoreId,
            OrderProducts = order.OrderProducts,
            Store = order.Store,
        };
    }
}
