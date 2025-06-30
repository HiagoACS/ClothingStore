using System;
using System.Collections.Generic;

namespace ClothingStore.Infrastructure.Models;

public partial class Orderitem
{
    public int Orderitemid { get; set; }

    public int Orderid { get; set; }

    public int Clothingitemid { get; set; }

    public int Quantity { get; set; }

    public virtual Clothingitem Clothingitem { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
