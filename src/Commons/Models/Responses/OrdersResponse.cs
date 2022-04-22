using Models.Core;
using System;
using System.Collections.Generic;

namespace Models.Responses
{
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

        public Store Store { get; set; }

        public static Order Parse(OrdersResponse o)
        {
            throw new NotImplementedException();
        }
    }
}
