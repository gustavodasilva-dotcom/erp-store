USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspRegisterEmployee]
	 @FirstName			VARCHAR(50)
	,@MiddleName		VARCHAR(50)
	,@LastName			VARCHAR(50)
	,@Identification	VARCHAR(50)
	,@Username			VARCHAR(50)
	,@Password			VARCHAR(50)
	,@AddressID			INT
	,@ContactID			INT
	,@ImageID			INT
	,@Access_LevelID	INT
	,@Salary			MONEY
	,@JobID				INT
AS
/**********************************************************************************
Create date: 2021-12-11

Description: This procedure registers new employees in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;
	
		DECLARE @EmployeeID		INT,
				@User_InfoID	INT;
	
/**********************************************************************************
1 - INSERTING USER DATA:
**********************************************************************************/
		BEGIN TRANSACTION;
	
			BEGIN TRY
	
				INSERT INTO User_Info
				VALUES
				(
					 @Username
					,ENCRYPTBYPASSPHRASE('key', @Password)
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
	
		SET @User_InfoID = @@IDENTITY;
/**********************************************************************************
1 - INSERTING USER DATA.
**********************************************************************************/
	
/**********************************************************************************
2 - INSERTING USER EMPLOYEE:
**********************************************************************************/
		BEGIN TRANSACTION;
	
			BEGIN TRY
		
				INSERT INTO Employee
				VALUES
				(
					 @FirstName
					,@MiddleName
					,@LastName
					,@Identification
					,@Access_LevelID
					,@User_InfoID
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
	
		SET @EmployeeID = @@IDENTITY;
/**********************************************************************************
2 - INSERTING USER EMPLOYEE.
**********************************************************************************/
	
/**********************************************************************************
3 - INSERTING USER EMPLOYEE_JOB:
**********************************************************************************/
		BEGIN TRANSACTION;
	
			BEGIN TRY
		
				INSERT INTO Employee_Job
				VALUES
				(
					 @Salary
					,@EmployeeID
					,@JobID
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
3 - INSERTING USER EMPLOYEE_JOB.
**********************************************************************************/

/**********************************************************************************
4 - INSERTING EMPLOYEE_IMAGE:
**********************************************************************************/
		IF @ImageID != 0
		BEGIN

			BEGIN TRANSACTION;
		
				BEGIN TRY

					INSERT INTO Employee_Image
					VALUES
					(
						 @EmployeeID
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
4 - INSERTING EMPLOYEE_IMAGE.
**********************************************************************************/
	END;
GO