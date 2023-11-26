-- =============================================
-- Create date: 25.11.2023
-- Description: CreateProductTable
-- =============================================

CREATE TABLE [Product] (
    ProductId UNIQUEIDENTIFIER PRIMARY KEY,
    ProductName VARCHAR(50),
    UnitPrice DECIMAL(18, 2),
    SupplierId UNIQUEIDENTIFIER,
    CreatedOn DATETIME,
    IsActive BIT,
    FOREIGN KEY (SupplierId) REFERENCES Supplier(SupplierId)
);