USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspRegisterInventory]
	 @ItemID		INT
	,@SupplierID	INT
AS
/**********************************************************************************
Create date: 2021-12-31

Description: This procedure registers new inventories for items in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;

/**********************************************************************************
1 - INSERTING ITEMS_INVENTORY:
**********************************************************************************/
		BEGIN TRANSACTION;

			BEGIN TRY

				INSERT INTO Items_Inventory
				VALUES
				(
					 0
					,@ItemID
					,@SupplierID
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
1 - INSERTING ITEMS_INVENTORY:
**********************************************************************************/

	END;
GO