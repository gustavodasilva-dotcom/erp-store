USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspUpdateContact]
	 @ContactID		INT
	,@Email			VARCHAR(50)
	,@Cellphone		VARCHAR(20)
	,@Phone			VARCHAR(20)
AS
/**********************************************************************************
Create date: 2021-12-21

Description: This procedure updates user's contact data in the database.
**********************************************************************************/
	BEGIN

		SET NOCOUNT ON;

/**********************************************************************************
1 - UPDATING USER CONTACT:
**********************************************************************************/
		
		BEGIN TRANSACTION;
	
			BEGIN TRY
	
				UPDATE	Contact
				SET
						 Email		= @Email
						,Cellphone	= @Cellphone
						,Phone		= @Phone
				WHERE	ContactID = @ContactID;
	
			END TRY
	
			BEGIN CATCH
	
				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;
	
			END CATCH;
	
		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;

/**********************************************************************************
1 - UPDATING USER CONTACT.
**********************************************************************************/
	END;
GO