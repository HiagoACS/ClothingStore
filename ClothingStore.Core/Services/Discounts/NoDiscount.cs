using System;

using ClothingStore.Core.Interfaces;

namespace ClothingStore.Core.Services.Discounts
{
	public class NoDiscount : IDiscountStrategy
	{
		public decimal _discountPercentage { get; set; } = 0m;
        public decimal ApplyDiscount(decimal originalPrice) => originalPrice;
	}
}
