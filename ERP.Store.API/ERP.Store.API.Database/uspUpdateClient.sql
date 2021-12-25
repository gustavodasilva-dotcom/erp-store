USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspUpdateClient]
	 @ClientID			INT
	,@FirstName			VARCHAR(50)
	,@MiddleName		VARCHAR(50)
	,@LastName			VARCHAR(50)
	,@Identification	VARCHAR(50)
AS
/**********************************************************************************
Create date: 2021-12-24

Description: This procedure updates clients in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;

/**********************************************************************************
1 - UPDATING USER CLIENT:
**********************************************************************************/
		BEGIN TRANSACTION;
	
			BEGIN TRY
		
				UPDATE	Client
				SET
						 FirstName		= @FirstName
						,MiddleName		= @MiddleName
						,LastName		= @LastName
						,Identification = @Identification
				WHERE	ClientID = @ClientID;
		
			END TRY
		
			BEGIN CATCH
		
				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;
		
			END CATCH;
		
		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;
/**********************************************************************************
1 - UPDATING USER CLIENT.
**********************************************************************************/
	END;
GO