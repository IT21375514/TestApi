-- =============================================
-- Create date: 25.11.2023
-- Description: CreateSupplierTable
-- =============================================

CREATE TABLE [Supplier] (
    SupplierId UNIQUEIDENTIFIER PRIMARY KEY,
    SupplierName VARCHAR(50),
    CreatedOn DATETIME,
    IsActive BIT
);