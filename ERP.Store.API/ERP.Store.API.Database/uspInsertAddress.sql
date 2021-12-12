USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspInsertAddress]
	 @Zip				VARCHAR(10)
	,@Street			VARCHAR(50)
	,@Comment			VARCHAR(50)
	,@Neighborhood		VARCHAR(50)
	,@City				VARCHAR(50)
	,@State				VARCHAR(2)
	,@Country			VARCHAR(50)
AS
/**********************************************************************************
Create date: 2021-12-11

Description: This procedure registers address data in the database.
**********************************************************************************/
	BEGIN

		SET NOCOUNT ON;

		DECLARE @AddressID INT;

/**********************************************************************************
1 - INSERTING ADDRESS DATA:
**********************************************************************************/
		BEGIN TRANSACTION
	
			BEGIN TRY
	
				INSERT INTO Address
				VALUES
				(
					 @Zip
					,@Street
					,@Comment
					,@Neighborhood
					,@City
					,@State
					,@Country
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
	
		SET @AddressID = @@IDENTITY;
/**********************************************************************************
1 - INSERTING ADDRESS DATA.
**********************************************************************************/

		SELECT @AddressID;
	END;
GO