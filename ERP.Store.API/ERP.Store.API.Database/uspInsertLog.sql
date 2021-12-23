USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspInsertLog]
	 @Json		VARCHAR(MAX)
	,@Message	VARCHAR(200)
	,@Process	VARCHAR(100)
	,@Token		VARCHAR(MAX) = NULL
	,@ID		INT			 = 0
AS
/**********************************************************************************
Create date: 2021-12-22

Description: This procedure inserts logs in the database.
**********************************************************************************/
	BEGIN

		SET NOCOUNT ON;

/**********************************************************************************
1 - INSERTING LOG WITHOUT ID:
**********************************************************************************/
		IF @ID = 0
			BEGIN

				BEGIN TRANSACTION;

					BEGIN TRY

						INSERT INTO Logs
						(
							 Message
							,Json
							,Process
							,Deleted
							,InsertDate
						)
						VALUES
						(
							 @Message
							,@Json
							,@Process
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
1 - INSERTING LOG WITHOUT ID.
**********************************************************************************/
		
		ELSE

/**********************************************************************************
2 - INSERTING LOG WITH TOKEN:
**********************************************************************************/
			BEGIN

				IF @Token = NULL AND @ID != 0
					BEGIN
	
						BEGIN TRANSACTION;

							BEGIN TRY

								INSERT INTO Logs
								(
									 Message
									,Json
									,Process
									,ID
									,Deleted
									,InsertDate
								)
								VALUES
								(
									 @Message
									,@Json
									,@Process
									,@ID
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
2 - INSERTING LOG WITH TOKEN.
**********************************************************************************/

				ELSE

/**********************************************************************************
3 - INSERTING LOG WITH ID:
**********************************************************************************/
					BEGIN

						BEGIN TRANSACTION;

							BEGIN TRY

								INSERT INTO Logs
								(
									 Message
									,Json
									,Process
									,Token
									,ID
									,Deleted
									,InsertDate
								)
								VALUES
								(
									 @Message
									,@Json
									,@Process
									,@Token
									,@ID
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

			END;
/**********************************************************************************
3 - INSERTING LOG WITH ID.
**********************************************************************************/

	END;
GO