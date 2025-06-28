using System;
using System.Linq;
using ClothingStore.Core.Interfaces;
using ClothingStore.Core.Models;
using ClothingStore.Core.Models.ClothingItems;
using ClothingStore.Core.Services;
using ClothingStore.Core.Services.Discounts;
using ClothingStore.Core.Services.Payments;

Console.WriteLine("Welcome to the Clothing Store!");

var customer = new Customer("Customer One", "customer@test.com");
Console.WriteLine($"Welcome, {customer.Name}");

// Create a shopping cart
var shoppingCart = new ShoppingCart();

// Create some clothing items
var shirt1 = new Shirt("Casual Shirt", "Blue", "M", 29.99m);
var shirt2 = new Shirt("Formal Shirt", "White", "L", 49.99m);
var pants1 = new Pants("Jeans", "Black", "32", 59.99m, "40", "40");

// Add items to the shopping cart
shoppingCart.AddItem(shirt1);
shoppingCart.AddItem(shirt2);
shoppingCart.AddItem(pants1);

// Display items in the shopping cart
Console.WriteLine("\nItems in your shopping cart:");
foreach (var item in shoppingCart.Items)
{
    Console.WriteLine(item.Description());
}

// Display total price of items
var total = shoppingCart.TotalPrice();
Console.WriteLine($"\nTotal Price (before discount): {total:C}");

// Ask for discount
Console.WriteLine("\nDo you have a discount? (yes/no)");
var discountAnswer = Console.ReadLine()?.Trim().ToLower();

IDiscountStrategy discountStrategy;
if (discountAnswer == "yes")
{
    discountStrategy = new PercentageDiscount(15m); // 15% discount
    Console.WriteLine("Applying 15% discount...");
}
else
{
    discountStrategy = new NoDiscount();
    Console.WriteLine("No discount applied.");
}

var discountedTotal = discountStrategy.ApplyDiscount(total);
Console.WriteLine($"Total after discount: {discountedTotal:C}");

// Choose payment method
Console.WriteLine("\nChoose payment method: 1 - Credit, 2 - Debit");
var paymentChoice = Console.ReadLine()?.Trim();

Console.WriteLine("\nCard Details:");
Console.Write("Card Number: ");
var cardNumber = Console.ReadLine()?.Trim() ?? "1234567890";
Console.Write("Card Holder Name: ");
var cardHolderName = Console.ReadLine()?.Trim() ?? "Hiago";
DateTime cardExpiry = DateTime.Now;
Console.Write("Card CVV: ");
var cardCvv = Console.ReadLine()?.Trim() ?? "123";
var order = new Order(customer, shoppingCart.Items.ToList(), discountStrategy);
Console.WriteLine("\nOrder Details:");
Console.WriteLine(order.ToString());
IPaymentProcess paymentProcessor = paymentChoice switch
{
    "1" => new CreditCardPayment(cardNumber, cardHolderName, cardExpiry, cardCvv),
    "2" => new DebitCardPayment(cardNumber, cardHolderName, cardExpiry, cardCvv),
    _ => throw new InvalidOperationException("Invalid payment option")
};

// Create an order

// Process order with payment
var orderService = new OrderService(paymentProcessor);
orderService.PlaceOrder(order);

Console.WriteLine("\nThank you for your purchase!");
