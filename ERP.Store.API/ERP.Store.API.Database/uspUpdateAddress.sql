USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspUpdateAddress]
	 @AddressID			INT
	,@Zip				VARCHAR(10)
	,@Street			VARCHAR(50)
	,@Number			VARCHAR(10)
	,@Comment			VARCHAR(50)
	,@Neighborhood		VARCHAR(50)
	,@City				VARCHAR(50)
	,@State				VARCHAR(2)
	,@Country			VARCHAR(50)
AS
/**********************************************************************************
Create date: 2021-12-21

Description: This procedure updates address data in the database.
**********************************************************************************/
	BEGIN

		SET NOCOUNT ON;

/**********************************************************************************
1 - UPDATING ADDRESS DATA:
**********************************************************************************/

		BEGIN TRANSACTION;

			BEGIN TRY

				UPDATE	Address
				SET
						 Zip			= @Zip
						,Street			= @Street
						,Number			= @Number
						,Comment		= @Comment
						,Neighborhood	= @Neighborhood
						,City			= @City
						,State			= @State
						,Country		= @Country
				WHERE	AddressID = @AddressID;

			END TRY

			BEGIN CATCH

				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;

			END CATCH;

		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;

/**********************************************************************************
1 - UPDATING ADDRESS DATA.
**********************************************************************************/

	END;
GO