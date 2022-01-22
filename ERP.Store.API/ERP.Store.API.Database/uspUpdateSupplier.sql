USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspUpdateSupplier]
	 @SupplierID		INT
	,@Name				VARCHAR(150)
	,@Identification	VARCHAR(50)
AS
/**********************************************************************************
Create date: 2021-12-26

Description: This procedure updates supplier in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;

/**********************************************************************************
1 - UPDATING SUPPLIER:
**********************************************************************************/
		BEGIN TRANSACTION;
	
			BEGIN TRY
		
				UPDATE	Supplier
				SET
						 Name			= @Name
						,Identification = @Identification
				WHERE	SupplierID = @SupplierID;
		
			END TRY
		
			BEGIN CATCH
		
				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;
		
			END CATCH;
		
		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;
/**********************************************************************************
1 - UPDATING SUPPLIER.
**********************************************************************************/
	END;
GO