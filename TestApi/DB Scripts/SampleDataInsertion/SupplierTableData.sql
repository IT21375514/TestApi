-- =============================================
-- Create date: 25.11.2023
-- Description: SupplierTableDataInsertion
-- =============================================

INSERT INTO Supplier (SupplierId, SupplierName, CreatedOn, IsActive)
VALUES 
    (NEWID(), 'Supplier1', GETDATE(), 1),
    (NEWID(), 'Supplier2', GETDATE(), 1),
    (NEWID(), 'Supplier3', GETDATE(), 1)
  ;