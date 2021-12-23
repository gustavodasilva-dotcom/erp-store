USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspUpdateImage]
	  @ImageID	INT
	 ,@Base64	VARCHAR(MAX)
AS
/**********************************************************************************
Create date: 2021-12-21

Description: This procedure updates new employees' image in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;

/**********************************************************************************
1 - UPDATING IMAGE:
**********************************************************************************/
		
		BEGIN TRANSACTION;
		
			BEGIN TRY

				UPDATE	Image
				SET
						Base64 = @Base64
				WHERE	ImageID = @ImageID;

			END TRY

			BEGIN CATCH

				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;

			END CATCH;

		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;
		
/**********************************************************************************
1 - UPDATING IMAGE.
**********************************************************************************/

	END;
GO