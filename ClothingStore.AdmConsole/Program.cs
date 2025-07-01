using ClothingStore.Core.Models;
using ClothingStore.Core.Services;

class Program
{
    static void Main()
    {
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
                    menuActions.DisplayProducts();
                    break;
                case "2":
                    Console.Clear();
                    menuActions.AddProduct();
                    break;
                case "3":
                    Console.Clear();
                    menuActions.UpdateProduct();
                    break;
                case "4":
                    Console.Clear();
                    menuActions.RemoveProduct();
                    break;
                case "13":
                    Console.Clear();
                    running = false;
                    Console.WriteLine("Exiting the application. Goodbye!");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

        }
    }
}