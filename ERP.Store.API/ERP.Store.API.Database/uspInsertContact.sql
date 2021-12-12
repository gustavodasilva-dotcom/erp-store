USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspInsertContact]
	 @Email				VARCHAR(50)
	,@Cellphone			VARCHAR(20)
	,@Phone				VARCHAR(20)
AS
/**********************************************************************************
Create date: 2021-12-11

Description: This procedure registers user's contact data in the database.
**********************************************************************************/
	BEGIN

		SET NOCOUNT ON;

		DECLARE @ContactID INT;

/**********************************************************************************
1 - INSERTING USER CONTACT:
**********************************************************************************/
		BEGIN TRANSACTION;
	
			BEGIN TRY
	
				INSERT INTO Contact
				VALUES
				(
					 @Email
					,@Cellphone
					,@Phone
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
	
		SET @ContactID = @@IDENTITY;
/**********************************************************************************
1 - INSERTING USER CONTACT.
**********************************************************************************/
		
		SELECT @ContactID;
	END;
GO