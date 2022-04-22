using Models.Responses;

namespace Mibocata.Infrastructure.Data.Models.Mappers
{
    public static class OrderMapper
    {
        public static OrdersResponse Parse(Order order)
        {
            return new OrdersResponse()
            {
                Id = order.Id,
                StoreId = order.StoreId,
                State = order.State,
                Amount = order.Amount,
                ClientId = order.ClientId,
                Comment = order.Comment,
                PickupTime = order.PickupTime,
                Created = order.Created,
                //TODO review
                //Client = order.Client,
                //Store = order.Store,
                //OrderProducts = order.OrderProducts,
            };
        }
    }
}
