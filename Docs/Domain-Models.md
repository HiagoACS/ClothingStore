# 📚 Domain Models

This document describes the main domain entities of the Clothing Store system, their purposes, and their properties.

---

## 🧑‍💼 Customer

Represents a customer of the clothing store.

- **Properties:**
  - `Name (string)`: Customer's full name.
  - `Email (string)`: Customer's email address.

- **Methods:**
  - `ToString()`: Returns a summary string of the customer's information.

---

## 👕 Clothing (abstract)

Base class representing a generic clothing item.

- **Properties:**
  - `Name (string)`: Name of the clothing item.
  - `Color (string)`: Color of the item.
  - `Size (string)`: Size specification (e.g., "M", "L", "32").
  - `Price (decimal)`: Price of the item.

- **Methods:**
  - `Description() (abstract)`: Returns a detailed description of the clothing item.

---

### 👔 Shirt : Clothing

Concrete class representing a shirt.

- **Properties (inherited from Clothing):**
  - `Name`, `Color`, `Size`, `Price`.

- **Methods:**
  - `Description()`: Returns a formatted description of the shirt.

---

### 👖 Pants : Clothing

Concrete class representing pants.

- **Properties (inherited from Clothing):**
  - `Name`, `Color`, `Size`, `Price`.

- **Additional Properties:**
  - `Length (string)`: Length of the pants (e.g., "30", "32", "34").
  - `Fit (string)`: Fit type of the pants (e.g., "Slim", "Regular", "Relaxed").

- **Methods:**
  - `Description()`: Returns a formatted description of the pants.

---

## 🛒 ShoppingCart

Represents the customer's shopping cart, storing items before checkout.

- **Properties:**
  - `Items (IReadOnlyList<Clothing>)`: List of clothing items added to the cart.

- **Methods:**
  - `AddItem(Clothing)`: Adds a clothing item to the cart.
  - `RemoveItem(Clothing)`: Removes a clothing item from the cart.
  - `TotalPrice()`: Calculates the total price of items in the cart.

---

## 🧾 Order

Represents a purchase order created by the customer at checkout.

- **Properties:**
  - `OrderId (int)`: Randomly generated order ID.
  - `Customer (Customer)`: Customer who placed the order.
  - `Items (List<Clothing>)`: List of clothing items included in the order.
  - `OrderDate (DateTime)`: Date and time when the order was created.
  - `TotalPrice (decimal)`: Total price of the order after discount.
  - `DiscountStrategy (IDiscountStrategy)`: Strategy used to calculate the discount.

- **Methods:**
  - `ToString()`: Returns a summary of the order.
  - `CalculateTotalPrice(IDiscountStrategy)`: Calculates the total price after applying the discount strategy.

---

📌 *This document serves as a quick reference for the core entities of the Clothing Store system.*
