using System;

using ClothingStore.Core.Interfaces;

namespace ClothingStore.Core.Services.Discounts
{
	public class NoDiscount : IDiscountStrategy
	{
        // NoDiscount class implements IDiscountStrategy
        public decimal ApplyDiscount(decimal originalPrice) => originalPrice;
	}
}
