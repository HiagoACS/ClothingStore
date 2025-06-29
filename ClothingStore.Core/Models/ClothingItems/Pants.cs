
using ClothingStore.Core.Models;
namespace ClothingStore.Core.Models.ClothingItems
{
    public class Pants : Clothing
    {
        public string Length { get; set; } // Length of the pants (e.g., "30", "32", "34")
        public string Fit { get; set; } // Fit type (e.g., "Slim", "Regular", "Relaxed")
        public Pants(int id, string name, string color, string size, decimal price, string length, string fit)
        {
            Id = id;
            Name = name;
            Color = color;
            Size = size;
            Price = price;
            Length = length;
            Fit = fit;
        }
        public override string Description()
        {
            return $"Pants: {Name}, Color: {Color}, Size: {Size}, Length: {Length}, Fit: {Fit}, Price: {Price:C}";
        }
    }
}