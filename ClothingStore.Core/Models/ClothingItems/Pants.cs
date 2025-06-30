
using ClothingStore.Core.Models;
namespace ClothingStore.Core.Models.ClothingItems
{
    public class Pants : Clothing
    {
        public Pants(int id, string name, string color, string size, decimal price)
        {
            Id = id;
            Name = name;
            Color = color;
            Size = size;
            Price = price;
        }
        public override string Description()
        {
            return $"Pants: {Name}, Color: {Color}, Size: {Size}, Price: {Price:C}";
        }
    }
}