# 🛍 Clothing Store Console Application - User Manual


## 📦 About the Project

This solution simulates a clothing store workflow using two console applications:

- **ClothingStore.Console**: customer-facing interface for viewing products, managing the shopping cart, placing orders, and receiving invoices.
- **ClothingStore.AdmConsole**: administrative interface for managing products (adding, editing, removing items) and generating sales reports directly connected to the PostgreSQL database.

The entire project is built with C#, using Entity Framework Core for data persistence.

---

## 🔗 Requirements

Before running the project, install the following packages with `dotnet add package`:

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Npgsql.EntityFrameworkCore.PostgreSQL`
- `Microsoft.Extensions.Configuration`
- `Microsoft.Extensions.Configuration.Json`

Create a configuration file named `appsettings.json` in the root directory with your PostgreSQL connection string:

```json
{
  "ConnectionStrings": {
    "ClothingStoreDb": "Host=localhost;Database=clothingstore;Username=YOUR_USERNAME_;Password=YOUR_PASSWORD"
  }
}
````

---

## ⚙️ Initial Setup

### 1. Database
Create a PostgreSQL database named clothingstore.

Execute the provided SQL script (or adapt it) to create the tables: Customers, ClothingItems, Orders, OrderItems, and Payments.

### 2. Running the Applications
Navigate to the ClothingStore.Console or ClothingStore.AdmConsole folder and run:

```bash
dotnet run
```
---

## 🛒 Customer Console (ClothingStore.Console)
- View available products

- Add or remove items from the cart

- View shopping cart details

- Proceed to checkout

- Generate and save invoices in the Invoices/ directory

---

## 🛠️ Admin Console (ClothingStore.AdmConsole)
- Add new clothing items to the inventory

- Edit existing products

- Remove items from inventory

- Generate sales reports from the database, including total sales, revenue per product, and more

---

## 📁 Project Structure
- ClothingStore.Core - Domain logic (products, orders, cart, discounts, payment strategies).

- ClothingStore.Infrastructure - Database models (EF Core entities) and context configuration.

- ClothingStore.Console - Customer interface with menu-driven navigation.

- ClothingStore.AdmConsole - Administrator console for inventory management and sales analytics.

---

## 📜 License
This project was created for educational purposes and personal learning. It is not intended for production use.