using System;
//using Infrastructure namespaces
using ClothingStore.Infrastructure.Data;
using ClothingStore.Infrastructure.Models;

// using Core namespaces
using ClothingStore.Core.Models;
using ClothingStore.Core.Interfaces;
using ClothingStore.Core.Services.Discounts;
using ClothingStore.Core.Models.ClothingItems;
using ClothingStore.Core.Services;
using ClothingStore.Core.Services.Payments;


// this using is used to differentiate between the Infrastructure and Core models
using InfraModel = ClothingStore.Infrastructure.Models;
using CoreModel = ClothingStore.Core.Models;

// using Microsoft namespaces
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
public class MenuActions
{

    // Static list to hold products
    public static List<Clothing> products { get; private set; } = new ();

    private readonly IServiceProvider _serviceProvider;

    public MenuActions()
    {
        // Load products from a file or database if needed
        var basePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));

        //file
        // Load configuration settings
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath) // sobe 3 níveis para chegar na raiz da solução
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Set up dependency injection
        var services = new ServiceCollection();

        // Register the DbContext with PostgreSQL
        services.AddDbContext<ClothingStoreDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("ClothingStoreDb"))); // Use the connection string from appsettings.json

        _serviceProvider = services.BuildServiceProvider();
        /*
        LoadProducts(Path.Combine(basePath, "clothes.txt"));
        // I used only one time for upload the products to the database using the file clothes.txt
        */
        LoadProductsFromDatabase();
    }
    
    public void LoadProducts(string FilePath)
    {
        var context = _serviceProvider.GetRequiredService<ClothingStoreDbContext>();
        if (File.Exists(FilePath))
        {
            var lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                InfraModel.Clothingitem clothing;
                if (parts.Length == 5)
                {
                    clothing = new InfraModel.Clothingitem
                    {
                        Name = parts[1],
                        Color = parts[2],
                        Size = parts[3],
                        Price = decimal.Parse(parts[4]),
                        Type = "Shirt" // Assuming all items in the file are shirts for simplicity
                    };
                    context.Clothingitems.Add(clothing);
                    context.SaveChanges();
                }
            }
        }
        else
        {
            Console.WriteLine("Product file not found.");
        }
    }

    public void LoadProductsFromDatabase()
    {
        // Get the DbContext from the service provider
        var context = _serviceProvider.GetRequiredService<ClothingStoreDbContext>(); // using var statement ensures the context is disposed of properly after use

        // Load products from the database
        var items = context.Clothingitems.ToList();
        foreach (var item in items)
        {
            Clothing clothing;
            if(item.Type == "Shirt")
            {
                clothing = new Shirt(item.Clothingitemid, item.Name, item.Color, item.Size, item.Price);
            }
            else if(item.Type == "Pants"){
                clothing = new Pants(item.Clothingitemid, item.Name, item.Color, item.Size, item.Price);
            }
            else
            {
                // Handle other types of clothing if needed
                continue; // Skip unsupported types for now
            }
            if (!products.Any(p => p.Id == clothing.Id)) // Check if the product already exists
            {
                products.Add(clothing);
            }
        }
    }

    public static void DisplayProducts()
    {
        Console.WriteLine("Available Products:");
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Id}: {product.Description()}");
        }
    }

    public static void DisplayMainMenu()
    {
        Console.WriteLine("Welcome to the Clothing Store!");
        Console.WriteLine("1. View Products");
        Console.WriteLine("2. Add Product to Cart");
        Console.WriteLine("3. Remove Product from Cart");
        Console.WriteLine("4. View Cart");
        Console.WriteLine("5. Checkout");
        Console.WriteLine("6. Exit");
    }

    public Clothing? GetProductById(int id)
    {
        return products.SingleOrDefault(p => p.Id == id);
    }

    public void AddItemToCart(ShoppingCart cart)
    {
        Console.WriteLine("Enter the product ID to add to cart:");
        var productId = Console.ReadLine();
        // Logic to find product by ID and add to cart
        if (int.TryParse(productId, out int id))
        {
            var product = GetProductById(id);
            if (product != null)
            {
                cart.AddItem(product);
                Console.WriteLine($"{product.Name} has been added to your cart.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid product ID.");
        }
    }

    public void RemoveItemFromCart(ShoppingCart cart)
    {
        Console.WriteLine("Enter the product ID to remove from cart:");
        var productId = Console.ReadLine();
        // Logic to find product by ID and remove from cart
        if (int.TryParse(productId, out int id))
        {
            var product = GetProductById(id);
            if (product != null)
            {
                cart.RemoveItem(product);
                Console.WriteLine($"{product.Name} has been removed from your cart.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid product ID.");
        }
    }

    public void ViewCartItems(ShoppingCart cart)
    {
        if (cart.Items.Count == 0)
        {
            Console.WriteLine("Your cart is empty.");
        }
        else
        {
            Console.WriteLine("Items in your cart:");
            foreach (var item in cart.Items)
            {
                Console.WriteLine($"{item.Id}, {item.Description()}");
            }
            Console.WriteLine($"Total Price: {cart.TotalPrice():C}");
        }
    }
    public void Checkout(CoreModel.Customer customer, ShoppingCart cart)
    {
        
        using var context = _serviceProvider.GetRequiredService<ClothingStoreDbContext>();

        // Ensure the customer exists in the database
        var customerInfra = new InfraModel.Customer
        {
            Name = customer.Name,
            Email = customer.Email
        };

        // Check if the customer already exists in the database
        var existingCustomer = context.Customers.FirstOrDefault(c => c.Email == customerInfra.Email);
        if (existingCustomer != null)
        {
            customer.Id = existingCustomer.Customerid; // Set the ID from the existing customer
        }
        else
        {             // Add the new customer to the database
            context.Customers.Add(customerInfra);
            context.SaveChanges();
            customer.Id = customerInfra.Customerid; // Set the ID from the newly created customer}
        }

        // Proceed with checkout
        if (cart.Items.Count == 0)
        {
            Console.WriteLine("Your cart is empty. Cannot proceed to checkout.");
            return;
        }

        // Ask if the customer has a discount code
        Console.WriteLine("Do you have a discount code? (yes/no)");
        var hasDiscount = Console.ReadLine()?.Trim().ToLower() == "yes";
        IDiscountStrategy discountStrategy = hasDiscount ? new PercentageDiscount(10) : new NoDiscount();

        // Display cart items and total price
        Console.WriteLine("Proceeding to checkout...");
        Console.WriteLine($"Total amount due: {cart.TotalPrice():C}");

        var order = new CoreModel.Order(customer, cart.Items.ToList(), discountStrategy);

        // Save the order to the database
        var orderInfra = new InfraModel.Order
        {
            Customerid = order.Customer.Id,
            Orderdate = order.OrderDate,
            Totalprice = order.TotalPrice,
            Discountapplied = order.DiscountStrategy._discountPercentage
        };
        context.Orders.Add(orderInfra);
        context.SaveChanges();
        order.OrderId = orderInfra.Orderid; // Set the OrderId from the database

        Console.WriteLine(order.ToString());

        Console.WriteLine("Processing payment...");
        Console.WriteLine("Enter your payment details:");
        Console.Write("Card Number: ");
        string cardNumber = Console.ReadLine()?.Trim() ?? "1234567890";
        Console.Write("Card Holder Name: ");
        string cardHolderName = Console.ReadLine()?.Trim() ?? "Hiago";
        Console.Write("Expiry Date (MM/YY): ");
        DateTime expiryDate = DateTime.Now;
        Console.Write("CVV: ");
        string cvv = Console.ReadLine()?.Trim() ?? "123";

        // Simulate payment processing
        Console.WriteLine("1 for Debit, 2 for Credit: ");
        string? paymentType = Console.ReadLine()?.Trim();
        IPaymentProcess paymentProcessor = paymentType switch
        {
            "1" => new DebitCardPayment(cardNumber, cardHolderName, expiryDate, cvv),
            "2" => new CreditCardPayment(cardNumber, cardHolderName, expiryDate, cvv),
            _ => throw new InvalidOperationException("Invalid payment type selected.")
        };
        var orderService = new OrderService(paymentProcessor);
        if (!orderService.PlaceOrder(order))
        {
            Console.WriteLine("Order could not be placed due to payment failure.");
            return;
        }

        var payment = new InfraModel.Payment
        {
            Orderid = order.OrderId,
            Amount = order.TotalPrice,
            Paymentdate = DateTime.Now,
            Status = "Completed",
            Paymenttype = paymentType == "1" ? "Debit Card" : "Credit Card"
        };
        context.Payments.Add(payment);

        Console.Clear();
        Console.WriteLine("Order placed successfully!");
        context.SaveChanges();

        // Group items by ProductId and count quantities
        var groupedItems = order.Items
            .GroupBy(item => item.Id)
            .Select(group => new
            {
                ProductId = group.Key,
                Quantity = group.Count()
            });

        // Add each item to the Orderitem table
        foreach (var item in groupedItems)
        {
            var orderItem = new InfraModel.Orderitem
            {
                Orderid = order.OrderId,
                Clothingitemid = item.ProductId,
                Quantity = item.Quantity,
            };
            context.Orderitems.Add(orderItem);
        }
        context.SaveChanges();

        Console.WriteLine("Order details:");
        InvoiceService.GenerateInvoice(order);
        Console.WriteLine("Thank you for your purchase!");

        // Optionally clear the cart after checkout
        cart.ClearCart();
        Console.ReadKey();
        Console.Clear();
    }

}