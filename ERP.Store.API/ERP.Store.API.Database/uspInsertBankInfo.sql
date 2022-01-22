USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspInsertBankInfo]
	 @Number			VARCHAR(50)
	,@Agency			VARCHAR(50)
	,@BankName			VARCHAR(100)
	,@OrderPaymentID	INT
AS
/**********************************************************************************
Create date: 2022-01-09

Description: This procedure registers client's bank account data in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;

/**********************************************************************************
1 - INSERTING BANKINFO:
**********************************************************************************/
		BEGIN TRANSACTION;

			BEGIN TRY

				INSERT INTO BankInfo
				VALUES
				(
					 @Number
					,@Agency
					,@BankName
					,@OrderPaymentID
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
1 - INSERTING BANKINFO.
**********************************************************************************/
	END;
GO