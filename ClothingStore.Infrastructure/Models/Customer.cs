﻿using System;
using System.Collections.Generic;

namespace ClothingStore.Infrastructure.Models
{

    public partial class Customer
    {
        public int Customerid { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}