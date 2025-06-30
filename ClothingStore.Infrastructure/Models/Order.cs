using System;
using System.Collections.Generic;

namespace ClothingStore.Infrastructure.Models;

public partial class Order
{
    public int Orderid { get; set; }

    public int Customerid { get; set; }

    public DateTime Orderdate { get; set; }

    public decimal Totalprice { get; set; }

    public decimal? Discountapplied { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
