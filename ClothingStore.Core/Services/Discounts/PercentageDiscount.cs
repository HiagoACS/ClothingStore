using System;

using ClothingStore.Core.Interfaces;

namespace ClothingStore.Core.Services.Discounts
{
    public class PercentageDiscount : IDiscountStrategy
    {
        private readonly decimal _discountPercentage;
        public PercentageDiscount(decimal discountPercentage)
        {
            if (discountPercentage < 0 || discountPercentage > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount percentage must be between 0 and 100.");
            }
            _discountPercentage = discountPercentage;
        }

        // Discounts the original price by the specified percentage
        public decimal ApplyDiscount(decimal originalPrice)
        {
            if (originalPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(originalPrice), "Original price cannot be negative.");
            }
            return originalPrice * (1 - _discountPercentage / 100);
        }
    }
}