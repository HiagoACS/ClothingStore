# 🛍 Clothing Store Console Application

A console-based clothing sales system developed to practice and demonstrate the **SOLID principles** and apply a clean architecture approach in C#. Now fully integrated with a PostgreSQL database for persistent storage of products, orders, payments, and customers.

The solution includes two separate console apps:
- **ClothingStore.Console**: Customer interface for browsing products, shopping cart management, checkout, and invoice generation.
- **ClothingStore.AdmConsole**: Admin interface for adding, updating, or deleting products, and anothers management tasks.

---

## 🚀 Features

✅ Product catalog loaded from the database  
✅ Shopping cart system with add/remove capabilities  
✅ Order processing with automatic order ID generation  
✅ Discounts applied dynamically based on user input  
✅ Payment processing strategies (credit/debit)  
✅ Invoice generation and saving to `Invoices/` directory  
✅ Separation of responsibilities following SOLID principles  
✅ Integration with PostgreSQL via Entity Framework Core  
✅ Admin console for managing products directly in the database  

---

## 🔨 Technologies

- C# (.NET 9)  
- Entity Framework Core with PostgreSQL provider  
- Microsoft.Extensions.Configuration for config management  
- Console applications designed for modularity  
- Clean Architecture principles applied  
- Ready for future migration to ASP.NET Core Web API or desktop UI  

---

## ⚙️ Requirements

Install the following packages with `dotnet add package`:
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Npgsql.EntityFrameworkCore.PostgreSQL`
- `Microsoft.Extensions.Configuration`
- `Microsoft.Extensions.Configuration.Json`

Create an `appsettings.json` file at the solution root:

```json
{
  "ConnectionStrings": {
    "ClothingStoreDb": "Host=localhost;Database=clothingstore;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

----

## 🛠 Initial Setup
### 1️⃣ Database

- Create a PostgreSQL database named clothingstore.

- Execute the provided SQL script to set up tables: Customers, ClothingItems, Orders, OrderItems, and Payments.

## 2️⃣ Run the Applications
From either ClothingStore.Console or ClothingStore.AdmConsole:
```bash
dotnet run
```

---

## 🛒 Customer Console (ClothingStore.Console)
- View products from the database

- Add or remove products from your cart

- Proceed through checkout

- Enter payment details

- Generate and save an invoice

---

## 🛠️ Admin Console (ClothingStore.AdmConsole)
- Add new clothing items

- Edit existing clothing items

- Delete clothing items

- Generate sales reports directly from the database

---

## 📁 Project Structure
- ClothingStore.Core — Domain logic (entities, discounts, payments, cart, orders).

- ClothingStore.Infrastructure — EF Core entities and DbContext configuration.

- ClothingStore.Console — Customer console UI.

- ClothingStore.AdmConsole — Admin console UI.

---

## 🛤 Roadmap
### ✅ Phase 1 - Core & Console
- ✅ Core domain and console operations

- ✅ Shopping cart and basic checkout

### ✅ Phase 2 - SOLID & Payments
- ✅ Implement SOLID design

- ✅ Payment strategies and discounts

### ✅ Phase 3 - Database Integration
- ✅ Migrate from file-based to PostgreSQL persistence

- ✅ Orders, payments, and products stored in the database

### ✅ Phase 4 - Admin Features
- ✅ Create admin console to manage Database

### 🟣 Phase 5 - Web Migration (Planned)
- [ ] Create ASP.NET Core Web API

- [ ] Build a frontend for customers and admin

- [ ] Deploy online

--- 

## 🤝 Contributing
Feel free to fork this repository, open issues, or create pull requests if you’d like to contribute or suggest improvements.

---

## 📬 Contact
Developed by Hiago Costa — GitHub Profile