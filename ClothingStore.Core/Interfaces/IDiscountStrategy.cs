using System;

namespace ClothingStore.Core.Interfaces
{
	public interface IDiscountStrategy
	{
		decimal ApplyDiscount(decimal originalPrice);
	}
}
