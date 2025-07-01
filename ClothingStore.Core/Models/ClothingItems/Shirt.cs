using System;

namespace ClothingStore.Core.Models.ClothingItems
{
	public class Shirt : Clothing
	{
		
		public Shirt(int id, string name, string color, string size, decimal price)
		{
			Id = id;
            Name = name;
			Color = color;
			Size = size;
			Price = price;
			Type = "Shirt"; // Set the type to Shirt
        }
        public override string Description()
		{
			return $"Shirt: {Name}, Color: {Color}, Size: {Size},  Price: {Price:C}";
		}
	}

}
