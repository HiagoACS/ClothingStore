namespace ClothingStore.Core.Models
{
    public abstract class Clothing
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public abstract string Description();
    }
}
