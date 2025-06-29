# 🏗 Architecture Overview

This document provides an overview of the architecture of the Clothing Store console application, explaining how the project is organized, its main components, and their relationships.

---

## 📁 Project Structure

The project follows a layered and modular organization, with separation of concerns to promote maintainability and scalability:

ClothingStore/  
│  
├── ClothingStore.Console/ # Console application entry point  
│  
├── ClothingStore.Core/ # Core domain logic  
│ ├── Models/ # Domain models (e.g., Customer, Clothing, Order)  
│ │ └── Models/ClothingItems/ # Specific clothing subclasses (e.g., Shirt, Pants)  
│ ├── Interfaces/ # Interfaces for abstractions (e.g., IDiscountStrategy, IPaymentProcessor)  
│ └── Services/ # Business logic and services (e.g., OrderService, discount and payment implementations)  
│ │ ├── Services/Discounts/ # Discount strategies (e.g., PercentageDiscount, NoDiscount)  
│ │ └── Services/Payments/ # Payment processors (e.g., CreditCardPaymentProcessor, DebitCardPaymentProcessor)  


---

## 🧩 Main Components

### 1. Core Domain Models (`ClothingStore.Core.Models`)
- Represent the fundamental business entities:
  - `Customer`: Stores customer details.
  - `Clothing` (abstract): Base class for clothing items.
  - `Shirt`, `Pants`: Concrete clothing types inheriting from `Clothing`.
  - `Order`: Represents a purchase order.
  - `ShoppingCart`: Holds items before order is finalized.

### 2. Interfaces (`ClothingStore.Core.Interfaces`)
- Define contracts to decouple implementation:
  - `IDiscountStrategy`: Abstraction for discount calculation.
  - `IPaymentProcessor`: Abstraction for payment processing.

### 3. Services (`ClothingStore.Core.Services`)
- Contain business logic, orchestrate domain objects and interfaces:
  - `OrderService`: Handles order placement and payment processing.
  - Discount strategies (e.g., `PercentageDiscount`, `NoDiscount`).
  - Payment processors (e.g., `CreditCardPayment`, `DebitCardPayment`).

### 4. Console Application (`ClothingStore.Console`)
- Entry point that interacts with the user:
  - Handles input/output.
  - Creates and coordinates domain objects and services.
  - Demonstrates system usage.

---

## 🔗 Dependencies and Relationships

- The **Console project** depends on the **Core project**.
- The **Core project** is divided into domain models, interfaces, and services to respect separation of concerns.
- Services depend on interfaces (abstractions), not concrete implementations, following the **Dependency Inversion Principle (DIP)**.
- Domain models are kept clean and focused on data and behavior, following the **Single Responsibility Principle (SRP)**.
- Interface segregation is applied to keep interfaces specific and minimal.

---

## 🧠 SOLID Principles in Architecture

| Principle                | Where Applied                                   |
|-------------------------|------------------------------------------------|
| SRP (Single Responsibility) | Each class has a focused responsibility (e.g., Customer, Order, ShoppingCart) |
| OCP (Open/Closed)        | Discount system is extendable via `IDiscountStrategy` interface |
| LSP (Liskov Substitution) | Clothing subclasses (`Shirt`, `Pants`) can replace base class |
| ISP (Interface Segregation) | Payment and discount interfaces are specific and minimal |
| DIP (Dependency Inversion) | Services depend on interfaces, not concrete implementations |

---

## 📈 Future Considerations

- Implement persistence layer (database or file storage) for orders and customers.
- Add authentication and authorization services.
- Create a web API using ASP.NET Core reusing Core domain logic.
- Develop a front-end UI (Razor Pages, Blazor, or SPA frameworks).
- Add unit and integration tests for higher confidence.

---

*This architecture aims to create a maintainable, extensible, and testable system following best practices and design principles.*

