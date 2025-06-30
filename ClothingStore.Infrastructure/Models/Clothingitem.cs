using System;
using System.Collections.Generic;

namespace ClothingStore.Infrastructure.Models;

public partial class Clothingitem
{
    public int Clothingitemid { get; set; }

    public string Name { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string Size { get; set; } = null!;

    public decimal Price { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
}
