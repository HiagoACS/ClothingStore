using System;
using System.Collections.Generic;

namespace ClothingStore.Infrastructure.Models
{

    public partial class Payment
    {
        public int Paymentid { get; set; }

        public int Orderid { get; set; }

        public string Paymenttype { get; set; } = null!;

        public decimal Amount { get; set; }

        public DateTime Paymentdate { get; set; }

        public string Status { get; set; } = null!;

        public virtual Order Order { get; set; } = null!;
    }
}