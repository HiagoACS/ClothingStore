using System;

namespace ClothingStore.Core.Models
{
	public class Shirt : Clothing
	{
		
		public Shirt(string name, string color, string size, decimal price)
		{
			Name = name;
			Color = color;
			Size = size;
			Price = price;
        }
        public override string Description()
		{
			return $"Shirt: {Name}, Color: {Color}, Size: {Size},  Price: {Price:C}";
		}
	}

}
