using ClothingStore.Core.Models;
using ClothingStore.Core.Services;

class Program
{
    static void Main()
    {
        var customer = new Customer("Customer One", "customer@test.com");
        var shoppingCart = new ShoppingCart();
        var menuActions = new MenuActions();

        bool running = true;
        while (running)
        {
            MenuActions.DisplayMainMenu();
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    DisplayProducts();
                    break;
                case "2":
                    Console.Clear();
                    DisplayProducts();
                    menuActions.AddItemToCart(shoppingCart);
                    break;
                case "3":
                    Console.Clear();
                    menuActions.ViewCartItems(shoppingCart);
                    menuActions.RemoveItemFromCart(shoppingCart);
                    break;
                case "4":
                    Console.Clear();
                    menuActions.ViewCartItems(shoppingCart);
                    break;
                case "5":
                    Console.Clear();
                    menuActions.Checkout(customer, shoppingCart);
                    break;
                case "6":
                    Console.Clear();
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private static void DisplayProducts()
    {
        Console.WriteLine("\nAvailable Products:");
        if (MenuActions.products.Count == 0)
        {
            Console.WriteLine("No products available.");
        }
        else
        {
            foreach (var product in MenuActions.products)
            {
                Console.WriteLine($"{product.Id}: {product.Description()}");
            }
        }
        Console.WriteLine();
    }
}
