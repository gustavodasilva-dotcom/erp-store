USE ERP_Order;
GO

CREATE PROCEDURE [dbo].[uspUpdateEmployee]
	 @EmployeeID		INT
	,@Access_LevelID	INT
	,@JobID				INT
	,@User_InfoID		INT
	,@FirstName			VARCHAR(50)
	,@MiddleName		VARCHAR(50)
	,@LastName			VARCHAR(50)
	,@Identification	VARCHAR(50)
	,@Username			VARCHAR(50)
	,@Password			VARCHAR(50)
	,@Salary			MONEY
AS
/**********************************************************************************
Create date: 2021-12-21

Description: This procedure updates employees in the database.
**********************************************************************************/
	BEGIN
	
		SET NOCOUNT ON;

/**********************************************************************************
1 - UPDATING USER DATA:
**********************************************************************************/
		BEGIN TRANSACTION;
	
			BEGIN TRY
	
				UPDATE	User_Info
				SET
					 Username = @Username
					,Password = ENCRYPTBYPASSPHRASE('key', @Password)
				WHERE	User_InfoID = @User_InfoID;
	
			END TRY
	
			BEGIN CATCH
	
				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;
	
			END CATCH;
	
		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;
/**********************************************************************************
1 - UPDATING USER DATA.
**********************************************************************************/
	
/**********************************************************************************
2 - UPDATING USER EMPLOYEE:
**********************************************************************************/
		BEGIN TRANSACTION;
	
			BEGIN TRY
		
				UPDATE	Employee
				SET
						 FirstName		= @FirstName
						,MiddleName		= @MiddleName
						,LastName		= @LastName
						,Identification = @Identification
						,Access_LevelID = @Access_LevelID
				WHERE	EmployeeID = @EmployeeID;
		
			END TRY
		
			BEGIN CATCH
		
				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;
		
			END CATCH;
		
		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;
/**********************************************************************************
2 - UPDATING USER EMPLOYEE.
**********************************************************************************/
	
/**********************************************************************************
3 - UPDATING USER EMPLOYEE_JOB:
**********************************************************************************/
		BEGIN TRANSACTION;
	
			BEGIN TRY
		
				UPDATE	Employee_Job
				SET
						 Salary = @Salary
						,JobID	= @JobID
				WHERE	EmployeeID = @EmployeeID;
		
			END TRY
		
			BEGIN CATCH
		
				IF @@TRANCOUNT > 0
					ROLLBACK TRANSACTION;
		
			END CATCH;
		
		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;
/**********************************************************************************
3 - UPDATING USER EMPLOYEE_JOB.
**********************************************************************************/

	END;
GO