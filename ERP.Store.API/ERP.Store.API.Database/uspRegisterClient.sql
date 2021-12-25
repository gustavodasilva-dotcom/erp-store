USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspRegisterClient]
	 @FirstName			VARCHAR(50)
	,@MiddleName		VARCHAR(50)
	,@LastName			VARCHAR(50)
	,@Identification	VARCHAR(50)
	,@AddressID			INT
	,@ContactID			INT
	,@ImageID			INT
AS
/**********************************************************************************
Create date: 2021-12-24

Description: This procedure registers new clients in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;
	
		DECLARE @ClientID INT;

/**********************************************************************************
1 - INSERTING USER CLIENT:
**********************************************************************************/
		BEGIN TRANSACTION;
	
			BEGIN TRY
		
				INSERT INTO Client
				VALUES
				(
					 @FirstName
					,@MiddleName
					,@LastName
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
	
		SET @ClientID = @@IDENTITY;
/**********************************************************************************
1 - INSERTING USER CLIENT.
**********************************************************************************/

/**********************************************************************************
2 - INSERTING CLIENT_IMAGE:
**********************************************************************************/
		IF @ImageID != 0
		BEGIN

			BEGIN TRANSACTION;
		
				BEGIN TRY

					INSERT INTO Client_Image
					VALUES
					(
						 @ClientID
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
2 - INSERTING CLIENT_IMAGE.
**********************************************************************************/
	END;
GO