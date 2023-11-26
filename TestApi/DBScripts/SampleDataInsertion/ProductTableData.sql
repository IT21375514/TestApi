-- =============================================
-- Create date: 25.11.2023
-- Description: ProductTableDataInsertion
-- =============================================

INSERT INTO Product (ProductId, ProductName, UnitPrice, SupplierId, CreatedOn, IsActive)
VALUES
    (NEWID(), 'Product 1', 10.99,'3034B909-FE18-4407-8603-422885CFA284', GETDATE(), 1),
    (NEWID(), 'Product 2', 19.99, '36A0F0E8-818A-4228-87ED-4E88F0ACA783', GETDATE(), 1),
    (NEWID(), 'Product 3', 15.99,'EF43AA98-D79A-4DFE-AB5F-6B2C526176B7', GETDATE(), 1),
    (NEWID(), 'Product 4', 25.99, '3034B909-FE18-4407-8603-422885CFA284', GETDATE(), 1)
	;