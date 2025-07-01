using System;
//using Infrastructure namespaces
using ClothingStore.Infrastructure.Data;
// using Core namespaces


// this using is used to differentiate between the Infrastructure and Core models
using InfraModel = ClothingStore.Infrastructure.Models;
using CoreModel = ClothingStore.Core.Models;

// using Microsoft namespaces
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ClothingStore.Infrastructure.Models;
public class MenuActions
{

    // Static list to hold products
    public static List<Clothingitem> products { get; private set; } = new();

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
        Console.WriteLine("1. View Products");
        Console.WriteLine("2. Add Product");
        Console.WriteLine("3. Remove Product");
        Console.WriteLine("4. View Customers");
        Console.WriteLine("5. Add New Customer");
        Console.WriteLine("6. Remove Customer");
        Console.WriteLine("7. View Orders");
        Console.WriteLine("8. Get Order");
        Console.WriteLine("9. View Payments");
        Console.WriteLine("10. Generate Monthly Report ");
    }
}