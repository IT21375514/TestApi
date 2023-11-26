-- =============================================
-- Create date: 25.11.2023
-- Description: CreateCustomerTable
-- =============================================

CREATE TABLE [Customer] (
    UserId UNIQUEIDENTIFIER PRIMARY KEY,
    Username VARCHAR(30),
    Email VARCHAR(50),
    FirstName VARCHAR(20),
    LastName VARCHAR(20),
    CreatedOn DATETIME,
    IsActive BIT
);