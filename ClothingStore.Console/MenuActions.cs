using ClothingStore.Core.Models;
using ClothingStore.Core.Interfaces;
using ClothingStore.Core.Services.Discounts;
using ClothingStore.Core.Models.ClothingItems;
using ClothingStore.Core.Services;
using ClothingStore.Core.Services.Payments;
public class MenuActions
{

    // Static list to hold products
    public static List<Clothing> products { get; private set; } = new ();

    public MenuActions()
    {
        // Load products from a file or database if needed
        LoadProducts("clothes.txt");
    }
    public void LoadProducts(string FilePath)
    {
        if(File.Exists(FilePath))
        {
            var lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 5)
                {
                    var clothing = new Shirt(
                        int.Parse(parts[0]),
                        parts[1],
                        parts[2],
                        parts[3],
                        decimal.Parse(parts[4])
                    );
                    products.Add(clothing); ;
                }
            }
        }
        else
        {
            Console.WriteLine("Product file not found.");
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

    public void Checkout(Customer customer, ShoppingCart cart)
    {
        if (cart.Items.Count == 0)
        {
            Console.WriteLine("Your cart is empty. Cannot proceed to checkout.");
            return;
        }
        Console.WriteLine("Do you have a discount code? (yes/no)");
        var hasDiscount = Console.ReadLine()?.Trim().ToLower() == "yes";
        IDiscountStrategy discountStrategy = hasDiscount ? new PercentageDiscount(10) : new NoDiscount();
        Console.WriteLine("Proceeding to checkout...");
        Console.WriteLine($"Total amount due: {cart.TotalPrice():C}");
        // Here you can add logic for payment processing, order creation, etc.
        var order = new Order(customer, cart.Items.ToList(), discountStrategy);
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
        Console.WriteLine("Order placed successfully!");
        InvoiceService.GenerateInvoice(order);
        Console.WriteLine("Thank you for your purchase!");
        // Optionally clear the cart after checkout
        cart.ClearCart();
        Console.ReadKey();
        Console.Clear();
    }
}
