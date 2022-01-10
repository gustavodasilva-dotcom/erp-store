USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspInsertOrderItem]
	 @OrderID	INT
	,@ItemID	INT
	,@Quantity	INT
AS
/**********************************************************************************
Create date: 2022-01-09

Description: This procedure registers order items in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;

/**********************************************************************************
1 - INSERTING ORDER_ITEM:
**********************************************************************************/
		BEGIN TRANSACTION;

			BEGIN TRY

				INSERT INTO Order_Item
				VALUES
				(
					 @OrderID
					,@ItemID
					,@Quantity
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
/**********************************************************************************
1 - INSERTING ORDER_ITEM.
**********************************************************************************/
	END;
GO