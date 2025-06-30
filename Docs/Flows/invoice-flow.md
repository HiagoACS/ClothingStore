# 📝 Flow: Invoice Generation

This document describes the detailed flow of how an invoice is generated and saved in the **Clothing Store Console App**.

---

## 🎯 Purpose

Generate a detailed invoice file summarizing the order information, customer data, purchased items, and totals. The invoice serves as a receipt for both the store and the customer.

---

## 🛠 Steps

1. **Validate Input**  
   - Checks if the `Order` object is not null.  
   - Checks if the `Customer` name is present.  
   - Prevents generating invalid invoices.

2. **Ensure Directory Exists**  
   - Defines the folder path: `Invoices`.  
   - Checks whether the folder exists in the project root.  
   - Creates the folder automatically if it does not exist.

3. **Generate Invoice Filename**  
   - Builds a unique filename:  
     ```
     invoice_{OrderId}.txt
     ```  
   - Ensures each invoice is traceable to its order ID.

4. **Build Invoice Content**  
   - Uses a `StringBuilder` to generate a human-readable invoice.  
   - Adds:
     - Store header.
     - Order ID, customer name, email, and date.
     - List of items with their descriptions and prices.
     - Total price before discount.
     - Total price after discount.  
   - Example of item line:  
     ```
     Shirt: Casual Shirt, Color: Blue, Size: M, Price: $29.99
     ```

5. **Write Invoice to File**  
   - Saves the built content as a `.txt` file inside the `Invoices` directory.

6. **Notify the User**  
   - Prints the full path of the generated invoice to the console, informing the user where to find it.

---

## 📂 Example Output

Example saved invoice file content:

========= Clothing Store Invoice =========  
Order ID: 1234  
Customer: John Doe (john@example.com)  
Date: 6/28/2025 10:15:00 AM  
Items:  
Shirt: Casual Shirt, Color: Blue, Size: M, Price: $29.99 - $29.99  
Pants: Jeans, Color: Black, Size: 32, Length: 40, Fit: 40, Price: $59.99 - $59.99  
Total Before Discount: $89.98  
Total After Discount: $80.98  

---

## ✅ Responsibilities

- **InvoiceService**: Encapsulates the entire invoice creation logic, following the Single Responsibility Principle (SRP).  
- **MenuActions.Checkout**: Calls `InvoiceService.GenerateInvoice(order)` after a successful order placement.
