USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspInsertCardsInfo]
	 @NameOnCard		VARCHAR(200)
	,@CardNumber		VARCHAR(50)
	,@YearExpiryDate	INT
	,@MonthExpiryDate	INT
	,@SecurityCode		INT
	,@OrderPaymentID	INT
AS
/**********************************************************************************
Create date: 2022-01-09

Description: This procedure registers card's data in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;

/**********************************************************************************
1 - INSERTING CARDSINFO:
**********************************************************************************/
		BEGIN TRANSACTION;

			BEGIN TRY

				INSERT INTO CardsInfo
				VALUES
				(
					 @NameOnCard
					,@CardNumber
					,@YearExpiryDate
					,@MonthExpiryDate
					,@SecurityCode
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
1 - INSERTING CARDSINFO.
**********************************************************************************/
	END;
GO