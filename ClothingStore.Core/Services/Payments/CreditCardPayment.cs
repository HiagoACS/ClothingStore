using ClothingStore.Core.Interfaces;
using ClothingStore.Core.Models;

namespace ClothingStore.Core.Services.Payments
{
    public class CreditCardPayment : IPaymentProcess
    {
        private readonly string _cardNumber;
        private readonly string _cardHolderName;
        private readonly DateTime _expiryDate;
        private readonly string _cvv;
        public CreditCardPayment(string cardNumber, string cardHolderName, DateTime expiryDate, string cvv)
        {
            _cardNumber = cardNumber;
            _cardHolderName = cardHolderName;
            _expiryDate = expiryDate;
            _cvv = cvv;
        }
        public bool PaymentProcess(Order order)
        {
            // Simulate payment processing logic
            Console.WriteLine($"Processing credit card payment for order {order.OrderId}.");
            return true; // Assume payment is successful
        }
    }
}