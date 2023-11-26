-- =============================================
-- Create date: 25.11.2023
-- Description: GetActiveOrdersByCustomer
-- =============================================

CREATE PROCEDURE GetActiveOrdersByCustomer
    @CustomerId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT
        OrderId,
        ProductId,
        OrderStatus,
        OrderType,
        OrderBy AS CustomerId,
        OrderedOn,
        ShippedOn,
        IsActive
    FROM
        [Order]
    WHERE
        IsActive = 1
        AND OrderBy = @CustomerId;
END;