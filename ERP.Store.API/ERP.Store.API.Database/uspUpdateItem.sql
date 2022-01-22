USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspUpdateItem]
	 @ItemID		INT
	,@Name			VARCHAR(200)
	,@Price			MONEY
	,@CategoryID	INT
	,@SupplierID	INT
AS
/**********************************************************************************
Create date: 2022-01-02

Description: This procedure updates items in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;

/**********************************************************************************
1 - UPDATING ITEM:
**********************************************************************************/
		BEGIN TRANSACTION

			BEGIN TRY

				UPDATE	Items
				SET
						 SupplierID		= @SupplierID
						,Name			= @Name
						,Price			= @Price
						,@CategoryID	= @CategoryID
				WHERE	ItemID = @ItemID;

			END TRY
			
			BEGIN CATCH

				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;

			END CATCH;

		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;
/**********************************************************************************
1 - UPDATING ITEM.
**********************************************************************************/

	END;
GO