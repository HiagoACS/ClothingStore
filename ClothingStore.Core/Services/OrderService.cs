using ClothingStore.Core.Interfaces;
using ClothingStore.Core.Models;
namespace ClothingStore.Core.Services{
    public class OrderService : IOrderService
    {
        private readonly IPaymentProcess _paymentProcessor;
        public OrderService(IPaymentProcess paymentProcessor)
        {
            _paymentProcessor = paymentProcessor ?? throw new ArgumentNullException(nameof(paymentProcessor), "Payment processor cannot be null.");
        }
        // Places an order after processing the payment
        public bool PlaceOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");
            }
            // Process the payment
            bool paymentSuccessful = _paymentProcessor.PaymentProcess(order);
            if (!paymentSuccessful)
            {
                Console.WriteLine("Payment failed. Order cannot be placed.");
                return false;
            }
            // Here you would typically save the order to a database or perform other business logic
            Console.WriteLine($"Order {order.OrderId} placed successfully for customer {order.Customer.Name}.");
            return true;
        }
    }
}