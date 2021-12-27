USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspRegisterSupplier]
	 @Name				VARCHAR(50)
	,@Identification	VARCHAR(50)
	,@AddressID			INT
	,@ContactID			INT
AS
/**********************************************************************************
Create date: 2021-12-26

Description: This procedure registers new suppliers in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;

/**********************************************************************************
1 - INSERTING SUPPLIER:
**********************************************************************************/
		BEGIN TRANSACTION

			BEGIN TRY

				INSERT INTO Supplier
				VALUES
				(
					 @Name
					,@Identification
					,@ContactID
					,@AddressID
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
2 - INSERTING SUPPLIER.
**********************************************************************************/
	END;
GO