USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspRegisterItem]
	 @Name			VARCHAR(200)
	,@Price			MONEY
	,@CategoryID	INT
	,@SupplierID	INT
	,@ImageID		INT
AS
/**********************************************************************************
Create date: 2021-12-30

Description: This procedure registers new items in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;

		DECLARE @ItemID INT;

/**********************************************************************************
1 - INSERTING USER ITEM:
**********************************************************************************/
		BEGIN TRANSACTION;

			BEGIN TRY
			
				INSERT INTO Items
				VALUES
				(
					 @SupplierID
					,0
					,GETDATE()
					,@Name
					,@Price
					,@CategoryID
				);

			END TRY

			BEGIN CATCH

				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;

			END CATCH;

		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;

		SET @ItemID = @@IDENTITY;
/**********************************************************************************
1 - INSERTING USER ITEM.
**********************************************************************************/

/**********************************************************************************
2 - INSERTING ITEM_IMAGE:
**********************************************************************************/
		IF @ImageID != 0
		BEGIN

			BEGIN TRANSACTION;
		
				BEGIN TRY

					INSERT INTO Item_Image
					VALUES
					(
						 @ItemID
						,@ImageID
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

		END;
/**********************************************************************************
2 - INSERTING ITEM_IMAGE.
**********************************************************************************/

		SELECT @ItemID;

	END;
GO