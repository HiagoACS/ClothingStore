using System;

namespace ClothingStore.Core.Interfaces
{

	public interface IDiscountStrategy
	{
		public decimal _discountPercentage { get; set; }
        decimal ApplyDiscount(decimal originalPrice);
	}
}
