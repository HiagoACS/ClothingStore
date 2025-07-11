﻿using ClothingStore.Infrastructure.Data;
// using Core namespaces


// using Microsoft namespaces
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ClothingStore.Infrastructure.Models;
public class MenuActions
{

    // Static list to hold products

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

    }
    public static void DisplayMainMenu()
    {
        Console.WriteLine("CONSOLE ADMINISTRATOR");
        Console.WriteLine("1. View Products"); // Done
        Console.WriteLine("2. Add Product"); // Done
        Console.WriteLine("3. Update Product"); // Done
        Console.WriteLine("4. Remove Product"); // Done
        Console.WriteLine("5. View Customers"); 
        Console.WriteLine("6. Add New Customer");
        Console.WriteLine("7. Update Customer");
        Console.WriteLine("8. Remove Customer");
        Console.WriteLine("9. View Orders");
        Console.WriteLine("10. Get Order");
        Console.WriteLine("11. View Payments");
        Console.WriteLine("12. Generate Monthly Report ");
        Console.WriteLine("13. Exit");
    }

    public void DisplayProducts()
    {
        using var context = _serviceProvider.GetRequiredService<ClothingStoreDbContext>();

        var products = context.Clothingitems.ToList();
        if (products.Count == 0)
        {
            Console.WriteLine("No products on Store.");
            return;
        }
        Console.WriteLine("Products in Store:");
        foreach(var item in products)
        {
            Console.WriteLine($"ID: {item.Clothingitemid}, Name: {item.Name}, Color: {item.Color}, Price: {item.Price}, Size: {item.Size}, Type: {item.Type}");
        }
        Console.WriteLine("++++++++++++++++++++++++++++++++\n");
    }

    public void AddProduct()
    {
        Console.WriteLine("Enter product name:");
        var name = Console.ReadLine();
        Console.WriteLine("Enter product color:");
        var color = Console.ReadLine();
        Console.WriteLine("Enter product size:");
        var size = Console.ReadLine();
        Console.WriteLine("Select product type:");

        Console.WriteLine("1. Shirt, 2. Pants");
        string? choice = Console.ReadLine();
        var type = choice switch
        {
            "1" => "Shirt",
            "2" => "Pants",
            _ => throw new InvalidOperationException("Invalid type selected.")
        };

        Console.WriteLine("Enter product price:");
        if (!decimal.TryParse(Console.ReadLine(), out var price))
        {
            Console.WriteLine("Invalid price. Product not added.\n");
            return;
        }
        var newProduct = new Clothingitem
        {
            Name = name,
            Color = color,
            Size = size,
            Type = type,
            Price = price
        };
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ClothingStoreDbContext>();
            context.Clothingitems.Add(newProduct);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding product: {ex.Message}\n");
            return;
        }

        Console.WriteLine("Product added successfully.\n");
    }

    public void UpdateProduct()
    {
        Console.WriteLine("Enter product ID to update:");
        if (!int.TryParse(Console.ReadLine(), out var productId))
        {
            Console.WriteLine("Invalid ID. Product not updated.\n");
            return;
        }

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ClothingStoreDbContext>();
        var product = context.Clothingitems.FirstOrDefault(c => c.Clothingitemid == productId);
        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }
        Console.WriteLine($"CurrentName:{product.Name}\nEnter new name (leave empty to keep current):");
        var name = Console.ReadLine();
        if (!string.IsNullOrEmpty(name)) product.Name = name;

        Console.WriteLine($"CurrentColor:{product.Color}\nEnter new color (leave empty to keep current):");
        var color = Console.ReadLine();
        if (!string.IsNullOrEmpty(color)) product.Color = color;

        Console.WriteLine($"CurrentSize:{product.Size}\nEnter new size (leave empty to keep current):");
        var size = Console.ReadLine();
        if (!string.IsNullOrEmpty(size)) product.Size = size;

        Console.WriteLine($"CurrentType:{product.Type}\nSelect new type (leave empty to keep current):");
        Console.WriteLine($"CurrentType:{product.Type}\n1. Shirt, 2. Pants");
        string? choice = Console.ReadLine();
        product.Type = choice switch
        {
            "1" => "Shirt",
            "2" => "Pants",
            _ => throw new InvalidOperationException("Invalid type selected.\n")
        };


        Console.WriteLine($"CurrentPrice:{product.Price}\nEnter new price (leave empty to keep current):");
        if (decimal.TryParse(Console.ReadLine(), out var price)) product.Price = price;

        try
        {
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating product: {ex.Message}\n");
            return;
        }
            Console.WriteLine("Product updated successfully.\n");
    }

    public void RemoveProduct()
    {
        Console.WriteLine("Enter product ID to remove:");
        if (!int.TryParse(Console.ReadLine(), out var productId))
        {
            Console.WriteLine("Invalid ID. Product not removed.\n");
            return;
        }

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ClothingStoreDbContext>();
        var product = context.Clothingitems.Find(productId);
        if (product == null)
        {
            Console.WriteLine("Product not found.\n");
            return;
        }
        try
        {
            context.Clothingitems.Remove(product);

        }catch (Exception ex)
        {
            Console.WriteLine($"Error removing product: {ex.Message} \n");
            return;
        }
        context.SaveChanges();
        Console.WriteLine("Product removed successfully.\n");
    }

    public void ViewCustomers()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ClothingStoreDbContext>();
        var customers = context.Customers.ToList();
        if (customers.Count == 0)
        {
            Console.WriteLine("No customers found.\n");
            return;
        }
        Console.WriteLine("Customers:");
        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.Customerid}, Name: {customer.Name}, Email: {customer.Email}");
        }
        Console.WriteLine("++++++++++++++++++++++++++++++++\n");
    }

    public void AddNewCustomer()
    {
        Console.WriteLine("Enter customer name:");
        var name = Console.ReadLine();
        Console.WriteLine("Enter customer email:");
        var email = Console.ReadLine();
        var newCustomer = new Customer
        {
            Name = name,
            Email = email
        };
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ClothingStoreDbContext>();
            context.Customers.Add(newCustomer);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding customer: {ex.Message}\n");
            return;
        }
        Console.WriteLine("Customer added successfully.\n");
    }

    public void UpdateCustomer()
    {
        Console.WriteLine("Enter customer ID to update:");
        if (!int.TryParse(Console.ReadLine(), out var customerId))
        {
            Console.WriteLine("Invalid ID. Customer not updated.\n");
            return;
        }
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ClothingStoreDbContext>();
        var customer = context.Customers.FirstOrDefault(c => c.Customerid == customerId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found.\n");
            return;
        }
        Console.WriteLine($"CurrentName:{customer.Name}\nEnter new name (leave empty to keep current):");
        var name = Console.ReadLine();
        if (!string.IsNullOrEmpty(name)) customer.Name = name;
        Console.WriteLine($"CurrentEmail:{customer.Email}\nEnter new email (leave empty to keep current):");
        var email = Console.ReadLine();
        if (!string.IsNullOrEmpty(email)) customer.Email = email;
        try
        {
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating customer: {ex.Message}\n");
            return;
        }
            Console.WriteLine("Customer updated successfully.\n");
    }

    public void RemoveCustomer()
    {
        Console.WriteLine("Enter customer ID to remove:");
        if (!int.TryParse(Console.ReadLine(), out var customerId))
        {
            Console.WriteLine("Invalid ID. Customer not removed.\n");
            return;
        }
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ClothingStoreDbContext>();
        var customer = context.Customers.Find(customerId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found.\n");
            return;
        }
        try
        {
            context.Customers.Remove(customer);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error removing customer: {ex.Message}\n");
            return;
        }
        Console.WriteLine("Customer removed successfully.\n");
    }

    public void ViewOrders()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ClothingStoreDbContext>();
        var orders = context.Orders.Include(o => o.Orderitems).ToList();
        if (orders.Count == 0)
        {
            Console.WriteLine("No orders found.\n");
            return;
        }
        Console.WriteLine("Orders:");
        foreach (var order in orders)
        {
            Console.WriteLine($"Order ID: {order.Orderid}, Customer ID: {order.Customerid}, Date: {order.Orderdate}");
            foreach (var item in order.Orderitems)
            {
                Console.WriteLine($"  Item ID: {item.Clothingitemid}, Quantity: {item.Quantity}");
            }
        }
        Console.WriteLine("++++++++++++++++++++++++++++++++\n");
    }

    public void GetOrder()
    {
        Console.WriteLine("Enter order ID to view:");
        if (!int.TryParse(Console.ReadLine(), out var orderId))
        {
            Console.WriteLine("Invalid ID. Order not found.\n");
            return;
        }
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ClothingStoreDbContext>();
        var order = context.Orders.Include(o => o.Orderitems).FirstOrDefault(o => o.Orderid == orderId);
        if (order == null)
        {
            Console.WriteLine("Order not found.\n");
            return;
        }
        Console.WriteLine($"Order ID: {order.Orderid}, Customer ID: {order.Customerid}, Date: {order.Orderdate}");
        foreach (var item in order.Orderitems)
        {
            Console.WriteLine($"  Item ID: {item.Clothingitemid}, Quantity: {item.Quantity}");
        }
        Console.WriteLine("++++++++++++++++++++++++++++++++\n");
    }

    public void ViewPayments()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ClothingStoreDbContext>();
        var payments = context.Payments.ToList();
        if (payments.Count == 0)
        {
            Console.WriteLine("No payments found.\n");
            return;
        }
        Console.WriteLine("Payments:");
        foreach (var payment in payments)
        {
            Console.WriteLine($"Payment ID: {payment.Paymentid}, Order ID: {payment.Orderid}, Amount: {payment.Amount}, Date: {payment.Paymentdate}");
        }
        Console.WriteLine("++++++++++++++++++++++++++++++++\n");
    }

    public void GenerateMonthlyReport()
    {
        Console.Clear();
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ClothingStoreDbContext>();
        var orders = context.Orders.Include(o => o.Orderitems).ToList();
        if (orders.Count == 0)
        {
            Console.WriteLine("No orders found for the month.\n");
            return;
        }
        Console.WriteLine("Monthly Report:");
        foreach (var order in orders)
        {
            Console.WriteLine($"Order ID: {order.Orderid}, Customer ID: {order.Customerid}, Date: {order.Orderdate}");
            foreach (var item in order.Orderitems)
            {
                Console.WriteLine($"  Item ID: {item.Clothingitemid}, Quantity: {item.Quantity}");
            }
        }
        Console.WriteLine("++++++++++++++++++++++++++++++++\n");
    }
}
