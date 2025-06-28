using System;
using System.Linq;
using ClothingStore.Core.Interfaces;

namespace ClothingStore.Core.Models
{
    public class Order
    {
        public Customer Customer { get; set; }
        public List<Clothing> Items { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        public IDiscountStrategy DiscountStrategy { get; set; } // Default to no discount

        public int OrderId { get; private set; } = new Random().Next(1000, 9999); // Simple random ID generation
        public Order(Customer customer, List<Clothing> items, IDiscountStrategy discountStrategy)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
            Items = items ?? throw new ArgumentNullException(nameof(items), "Items cannot be null");
            OrderDate = DateTime.Now;
            DiscountStrategy = discountStrategy ?? throw new ArgumentNullException(nameof(discountStrategy), "Discount strategy cannot be null");
            TotalPrice = CalculateTotalPrice(DiscountStrategy);
        }
        private decimal CalculateTotalPrice(IDiscountStrategy discountStrategy)
        {
            return discountStrategy.ApplyDiscount(Items.Sum(item => item.Price));
        }
        public override string ToString()
        {
            return $"Order for {Customer.Name} on {OrderDate}, Total: {TotalPrice:C}";
        }
    }
}