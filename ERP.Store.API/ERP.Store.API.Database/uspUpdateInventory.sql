USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspUpdateInventory]
	 @ItemID		INT
	,@SupplierID	INT
	,@Quantity		INT
AS
/**********************************************************************************
Create date: 2022-01-02

Description: This procedure updates inventories for items in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;

/**********************************************************************************
1 - UPDATING ITEMS_INVENTORY:
**********************************************************************************/
		BEGIN TRANSACTION;

			BEGIN TRY

				UPDATE	Items_Inventory
				SET
						Quantity = @Quantity
				WHERE	ItemID = @ItemID;

			END TRY

			BEGIN CATCH

				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;
			
			END CATCH;

		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;
/**********************************************************************************
1 - UPDATING ITEMS_INVENTORY.
**********************************************************************************/

	END;
GO