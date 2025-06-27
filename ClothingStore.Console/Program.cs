using System;
using System.Linq;
using ClothingStore.Core.Models;

//Test namespace ClothingStore.Console and the ClothingStore.Core.Models

Console.WriteLine("Welcome to the Clothing Store!");

var customer = new Customer("Customer One", "customer@test.com"); //Create a new customer
Console.WriteLine("Welcome, " + customer.Name);

//Create a shopping cart
var shoppingCart = new ShoppingCart();

// Create some clothing items
var shirt1 = new Shirt("Casual Shirt", "Blue", "M", 29.99m);
var shirt2 = new Shirt("Formal Shirt", "White", "L", 49.99m);

// Add items to the shopping cart
shoppingCart.AddItem(shirt1);
shoppingCart.AddItem(shirt2);

// Display items in the shopping cart
Console.WriteLine("Items in your shopping cart:");
foreach (var item in shoppingCart.Items)
{
    Console.WriteLine(item.Description());
}

// Display total price of items in the shopping cart
Console.WriteLine($"Total Price: {shoppingCart.TotalPrice():C}");

// Create an order
var order = new Order(customer, shoppingCart.Items.ToList());
// Display order details
Console.WriteLine("Order Details:");
Console.WriteLine(order.ToString());

