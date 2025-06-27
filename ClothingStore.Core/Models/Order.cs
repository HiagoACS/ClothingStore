using System;
using System.Linq;

namespace ClothingStore.Core.Models
{
    public class Order
    {
        public Customer Customer { get; set; }
        public List<Clothing> Items { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Order(Customer customer, List<Clothing> items)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
            Items = items ?? throw new ArgumentNullException(nameof(items), "Items cannot be null");
            OrderDate = DateTime.Now;
            TotalPrice = CalculateTotalPrice();
        }
        private decimal CalculateTotalPrice()
        {
            return Items.Sum(item => item.Price);
        }
        public override string ToString()
        {
            return $"Order for {Customer.Name} on {OrderDate}, Total: {TotalPrice:C}";
        }
    }
}