# 🛒 Clothing Store - Complete Purchase Flow Overview

This document details the entire functional flow of the clothing sales system developed in C#, highlighting the steps, the classes involved, and how the SOLID principles were applied throughout the process.

---

## 📂 Flow Steps

### 1️⃣ Customer
- The process starts with creating a `Customer` object, which contains buyer information such as `Name` and `Email`.
- The `Customer` class follows **SRP (Single Responsibility Principle)** since it only manages customer data.

---

### 2️⃣ ShoppingCart
- The customer adds products from the catalog to the shopping cart.
- The `ShoppingCart` keeps an internal list of `Clothing` objects and exposes it as `IReadOnlyCollection` for read-only access, ensuring encapsulation.
- It provides methods to:
  - `AddItem()` — add an item.
  - `RemoveItem()` — remove an item.
  - `TotalPrice()` — calculate the subtotal of the items.

---

### 3️⃣ Clothing Hierarchy
- The abstract class `Clothing` defines attributes such as `Name`, `Color`, `Size`, and `Price`, as well as the abstract method `Description()`.
- Derived classes like `Shirt` and `Pants` implement `Description()` with specific details.
- This hierarchy demonstrates **LSP (Liskov Substitution Principle)** since subclasses can be used wherever `Clothing` is expected.

---

### 4️⃣ Discount Strategy
- The system asks the customer if they have a discount.
- If yes, it applies the `PercentageDiscount` strategy; if not, `NoDiscount` is used.
- Both implement the `IDiscountStrategy` interface, which defines `ApplyDiscount()`.
- Using this interface supports **OCP (Open/Closed Principle)**: new discount types can be added without modifying existing code that uses the abstraction.

---

### 5️⃣ Order
- When the customer decides to finalize the purchase, the cart is converted into an `Order`, containing:
  - The customer (`Customer`)
  - The list of purchased clothes (`List<Clothing>`)
  - The order date (`OrderDate`)
  - The total price with discount
- The `Order` is a **passive domain model**, simply storing order data.

---

### 6️⃣ Payment Processor
- The customer selects their payment method: credit or debit.
- This choice determines which `IPaymentProcessor` implementation will be used:
  - `CreditCardPaymentProcessor`
  - `DebitCardPaymentProcessor`
- Payment classes implement only the required methods, in line with **ISP (Interface Segregation Principle)**.

---

### 7️⃣ OrderService
- Receives the `Order` object and the `IPaymentProcessor`.
- The `PlaceOrder()` method performs:
  - Payment processing via `paymentProcessor.ProcessPayment(order.TotalPrice)`.
  - Confirms the order completion.
- `OrderService` depends only on the abstraction `IPaymentProcessor`, following **DIP (Dependency Inversion Principle)** and enabling changing payment logic without modifying the service.

---

## 🔗 Summary Flow

```plaintext
Customer → ShoppingCart → DiscountStrategy → Order → PaymentProcessor → OrderService
```

---

### ✅ SOLID Principles Applied

- SRP: Classes with single, well-defined responsibilities (Customer, ShoppingCart, Order, OrderService).

- OCP: Discount strategy with the IDiscountStrategy interface.

- LSP: Clothing subclasses replace the abstract class without issues.

- ISP: Specific interfaces like IPaymentProcessor with only necessary methods.

- DIP: OrderService depends on the abstraction IPaymentProcessor, not concrete implementations.
