-- =======================================
-- 📋 Customers
-- =======================================
CREATE TABLE Customers (
    CustomerId SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE
);

-- =======================================
-- 👕 ClothingItems - Table to store clothing items
-- =======================================
CREATE TABLE ClothingItems (
    ClothingItemId SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Color VARCHAR(50) NOT NULL,
    Size VARCHAR(10) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Type VARCHAR(50) NOT NULL
);

-- =======================================
-- 🧾 Orders
-- =======================================
CREATE TABLE Orders (
    OrderId SERIAL PRIMARY KEY,
    CustomerId INTEGER NOT NULL REFERENCES Customers(CustomerId),
    OrderDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    TotalPrice DECIMAL(10,2) NOT NULL,
    DiscountApplied DECIMAL(5,2) DEFAULT 0.00  -- percentual de desconto usado no pedido
);

-- =======================================
-- 📦 OrderItems
-- =======================================
CREATE TABLE OrderItems (
    OrderItemId SERIAL PRIMARY KEY,
    OrderId INTEGER NOT NULL REFERENCES Orders(OrderId) ON DELETE CASCADE,
    ClothingItemId INTEGER NOT NULL REFERENCES ClothingItems(ClothingItemId),
    Quantity INTEGER NOT NULL DEFAULT 1
);

-- =======================================
-- 💳 Payments
-- =======================================
CREATE TABLE Payments (
    PaymentId SERIAL PRIMARY KEY,
    OrderId INTEGER NOT NULL REFERENCES Orders(OrderId) ON DELETE CASCADE,
    PaymentType VARCHAR(50) NOT NULL,          -- ex.: Credit, Debit, etc.
    Amount DECIMAL(10,2) NOT NULL,
    PaymentDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Status VARCHAR(50) NOT NULL                -- ex.: Success, Failed, Pending
);
