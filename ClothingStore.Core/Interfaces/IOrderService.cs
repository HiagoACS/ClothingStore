namespace ClothingStore.Core.Interfaces
{
    // This interface defines the contract for order-related operations in the Clothing Store application.
    using ClothingStore.Core.Models;
    public interface IOrderService
    {
        bool PlaceOrder(Order order);
    }
}
