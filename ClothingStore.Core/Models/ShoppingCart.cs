using System;
using System.Linq;
using System.Collections.Generic;

namespace ClothingStore.Core.Models
{
    public class ShoppingCart
    {
        private List<Clothing> _items = new ();
        public IReadOnlyList<Clothing> Items => _items.AsReadOnly();

        public void AddItem(Clothing item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null");
            }
            _items.Add(item);
        }

        public void RemoveItem(Clothing item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null");
            }
            _items.Remove(item);
        }

        public decimal TotalPrice()
        {
            return _items.Sum(item => item.Price);
        }

        public void ClearCart()
        {
            _items.Clear();
        }
    }
}

