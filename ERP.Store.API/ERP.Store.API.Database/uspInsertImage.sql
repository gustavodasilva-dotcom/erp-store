USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspInsertImage]
	 @Base64 VARCHAR(MAX)
AS
/**********************************************************************************
Create date: 2021-12-12

Description: This procedure registers new employees' image in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;

		DECLARE @ImageID INT;

/**********************************************************************************
1 - INSERTING IMAGE:
**********************************************************************************/
		BEGIN TRANSACTION;
		
			BEGIN TRY

				INSERT INTO Image
				VALUES
				(
					 @Base64
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
		
		SET @ImageID = @@IDENTITY;
/**********************************************************************************
1 - INSERTING IMAGE.
**********************************************************************************/

		SELECT @ImageID;
	END;
GO