using System.IO;
using System.Text;
using ClothingStore.Core.Models;
namespace ClothingStore.Core.Services
{
    public static class InvoiceService
    {
        public static void GenerateInvoice(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Shopping cart cannot be null.");
            }
            if (string.IsNullOrEmpty(order.Customer.Name))
            {
                throw new ArgumentException("Customer name cannot be null or empty.", nameof(order.Customer.Name));
            }
            // Ensure the invoice directory exists
            var invoiceDir = "Invoices";
            // Create the directory if it does not exist
            if (!Directory.Exists(invoiceDir))
            {
                Directory.CreateDirectory(invoiceDir);
            }
            // Create a unique filename for the invoice
            var invoiceFilePath = Path.Combine(invoiceDir, $"invoice_{order.OrderId}.txt");
            StringBuilder invoice = new StringBuilder();
            invoice.AppendLine("========= Clothing Store Invoice =========");
            invoice.AppendLine($"Order ID: {order.OrderId}");
            invoice.AppendLine($"Customer: {order.Customer.Name} ({order.Customer.Email})");
            invoice.AppendLine($"Date: {order.OrderDate}");
            invoice.AppendLine("-----------------------------------------");
            invoice.AppendLine("Items:");
            foreach (var item in order.Items)
            {
                invoice.AppendLine($"{item.Description()} - {item.Price:C}");
            }
            invoice.AppendLine("-----------------------------------------");
            invoice.AppendLine($"Total Before Discount: {(order.Items.Sum(i => i.Price)):C}");
            invoice.AppendLine($"Total After Discount: {order.TotalPrice:C}");
            invoice.AppendLine("=========================================");

            File.WriteAllText(invoiceFilePath, invoice.ToString());

            Console.WriteLine($"Invoice saved to: {invoiceFilePath}");
        }
    }
}
