USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspRegisterOrderPayment]
	 @Value				MONEY
	,@OrderID			INT
	,@PaymentID			INT
	,@PaymentStatusID	BIT
AS
/**********************************************************************************
Create date: 2022-01-06

Description: This procedure registers new order payments in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;

		DECLARE @Order_PaymentID INT;

/**********************************************************************************
1 - INSERTING ORDER_PAYMENT:
**********************************************************************************/
		BEGIN TRANSACTION;

			BEGIN TRY

				INSERT INTO Order_Payment
				VALUES
				(
					 @Value
					,@OrderID
					,@PaymentID
					,@PaymentStatusID
					,0
					,GETDATE()
				);

			END TRY

			BEGIN CATCH

				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;

			END CATCH;

		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;

		SET @Order_PaymentID = @@IDENTITY;
/**********************************************************************************
1 - INSERTING ORDER_PAYMENT.
**********************************************************************************/

		SELECT @Order_PaymentID;

	END;
GO