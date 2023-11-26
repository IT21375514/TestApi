-- =============================================
-- Create date: 25.11.2023
-- Description: CustomerTableDataInsertion
-- =============================================

INSERT INTO Customer (UserId, Username, Email, FirstName, LastName, CreatedOn, IsActive)
VALUES
  (NEWID(), 'john_doe', 'john.doe@example.com', 'John', 'Doe', GETDATE(), 1),
  (NEWID(), 'jane_smith', 'jane.smith@example.com', 'Jane', 'Smith', GETDATE(), 1)
  ;