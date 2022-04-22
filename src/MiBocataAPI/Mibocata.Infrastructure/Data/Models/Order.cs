using Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mibocata.Infrastructure.Data.Models;

[Table("Orders")]
public class Order
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
}
