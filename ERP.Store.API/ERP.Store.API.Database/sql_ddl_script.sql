DROP DATABASE IF EXISTS ERP_Order
CREATE DATABASE ERP_Order;
GO

USE ERP_Order;
GO

DROP TABLE IF EXISTS Address
CREATE TABLE Address
(
	 AddressID		INT				NOT NULL	IDENTITY(10000000, 1)
	,Zip			VARCHAR(10)		NOT NULL
	,Street			VARCHAR(50)		NOT NULL
	,Number			VARCHAR(10)		NOT NULL
	,Comment		VARCHAR(50)		NOT NULL
	,Neighborhood	VARCHAR(50)		NOT NULL
	,City			VARCHAR(50)		NOT NULL
	,State			VARCHAR(2)		NOT NULL
	,Country		VARCHAR(50)		NOT NULL
	,Deleted		BIT				NOT NULL
	,InsertDate		DATETIME		NOT NULL

	CONSTRAINT PK_AddressID PRIMARY KEY(AddressID)
);

DROP TABLE IF EXISTS Access_Level
CREATE TABLE Access_Level
(
	 Access_LevelID		INT				NOT NULL	IDENTITY(1, 1)
	,Description		VARCHAR(60)		NOT NULL
	,Deleted			BIT				NOT NULL
	,InsertDate			DATETIME		NOT NULL

	CONSTRAINT PK_Access_LevelID PRIMARY KEY(Access_LevelID)
);

INSERT INTO Access_Level VALUES
 ('Administrator', 0, GETDATE())
,('Salesperson', 0, GETDATE());


DROP TABLE IF EXISTS Jobs
CREATE TABLE Jobs
(
	 JobID				INT				NOT NULL	IDENTITY(1, 1)
	,Description		VARCHAR(60)		NOT NULL
	,Deleted			BIT				NOT NULL
	,InsertDate			DATETIME		NOT NULL

	CONSTRAINT PK_JobID PRIMARY KEY(JobID)
);

INSERT INTO Jobs VALUES
 ('Manager', 0, GETDATE())
,('Assistant manager', 0, GETDATE())
,('Office administrator', 0, GETDATE())
,('Administrator', 0, GETDATE())
,('Salesperson', 0, GETDATE());


DROP TABLE IF EXISTS User_Info
CREATE TABLE User_Info
(
	 User_InfoID	INT				NOT NULL	IDENTITY(10000000, 1)
	,Username		VARCHAR(50)		NOT NULL
	,Password		VARBINARY(MAX)	NOT NULL
	,Deleted		BIT				NOT NULL
	,InsertDate		DATETIME		NOT NULL

	CONSTRAINT PK_User_InfoID PRIMARY KEY(User_InfoID)
);

DROP TABLE IF EXISTS Contact
CREATE TABLE Contact
(
	 ContactID		INT				NOT NULL	IDENTITY(10000000, 1)
	,Email			VARCHAR(50)		NOT NULL
	,Cellphone		VARCHAR(20)		NOT NULL
	,Phone			VARCHAR(20)		NOT NULL
	,Deleted		BIT				NOT NULL
	,InsertDate		DATETIME		NOT NULL

	CONSTRAINT PK_ContactID PRIMARY KEY(ContactID)
);

DROP TABLE IF EXISTS Client
CREATE TABLE Client
(
	 ClientID			INT				NOT NULL	IDENTITY(10000000, 1)
	,FirstName			VARCHAR(50)		NOT NULL
	,MiddleName			VARCHAR(50)
	,LastName			VARCHAR(50)		NOT NULL
	,Identification		VARCHAR(50)		NOT NULL
	,ContactID			INT				NOT NULL
	,AddressID			INT				NOT NULL
	,Deleted			BIT				NOT NULL
	,InsertDate			DATETIME		NOT NULL

	CONSTRAINT PK_ClientID PRIMARY KEY(ClientID)

	CONSTRAINT FK_Client_ContactID FOREIGN KEY(ContactID)
	REFERENCES Contact(ContactID),

	CONSTRAINT PK_Client_AddressID FOREIGN KEY(AddressID)
	REFERENCES Address(AddressID)
);

DROP TABLE IF EXISTS Employee
CREATE TABLE Employee
(
	 EmployeeID			INT				NOT NULL	IDENTITY(10000000, 1)
	,FirstName			VARCHAR(50)		NOT NULL
	,MiddleName			VARCHAR(50)
	,LastName			VARCHAR(50)		NOT NULL
	,Identification		VARCHAR(50)		NOT NULL
	,Access_LevelID		INT				NOT NULL
	,User_InfoID		INT				NOT NULL
	,ContactID			INT				NOT NULL
	,AddressID			INT				NOT NULL
	,Deleted			BIT				NOT NULL
	,InsertDate			DATETIME		NOT NULL

	CONSTRAINT PK_EmployeeID PRIMARY KEY(EmployeeID)

	CONSTRAINT FK_Employee_Access_LevelID FOREIGN KEY(Access_LevelID)
	REFERENCES Access_Level(Access_LevelID),

	CONSTRAINT FK_Employee_User_InfoID FOREIGN KEY(User_InfoID)
	REFERENCES User_Info(User_InfoID),

	CONSTRAINT FK_Employee_ContactID FOREIGN KEY(ContactID)
	REFERENCES Contact(ContactID),

	CONSTRAINT PK_Employee_AddressID FOREIGN KEY(AddressID)
	REFERENCES Address(AddressID)
);

DROP TABLE IF EXISTS Employee_Job
CREATE TABLE Employee_Job
(
	 Employee_JobID		INT			NOT NULL	IDENTITY(10000000, 1)
	,Salary				MONEY		NOT NULL
	,EmployeeID			INT			NOT NULL
	,JobID				INT			NOT NULL
	,Deleted			BIT			NOT NULL
	,InsertDate			DATETIME	NOT NULL

	CONSTRAINT PK_Employee_JobID PRIMARY KEY(Employee_JobID)

	CONSTRAINT FK_Employee_Job_EmployeeID FOREIGN KEY(EmployeeID)
	REFERENCES Employee(EmployeeID),

	CONSTRAINT FK_Employee_Job_JobID FOREIGN KEY(JobID)
	REFERENCES Jobs(JobID)
);

DROP TABLE IF EXISTS Image
CREATE TABLE Image
(
	 ImageID		INT				NOT NULL	IDENTITY(10000000, 1)
	,Base64			VARCHAR(MAX)	NOT NULL
	,Deleted		BIT				NOT NULL
	,InsertDate		DATETIME		NOT NULL

	CONSTRAINT PK_ImagemID PRIMARY KEY(ImageID)
);

DROP TABLE IF EXISTS Employee_Image
CREATE TABLE Employee_Image
(
	 Employee_ImageID	INT				NOT NULL	IDENTITY(10000000, 1)
	,EmployeeID			INT				NOT NULL
	,ImageID			INT				NOT NULL
	,Deleted			BIT				NOT NULL
	,InsertDate			DATETIME		NOT NULL

	CONSTRAINT PK_Employee_ImagemID PRIMARY KEY(Employee_ImageID)

	CONSTRAINT FK_Employee_Imagem_EmployeeID FOREIGN KEY(EmployeeID)
	REFERENCES Employee(EmployeeID),

	CONSTRAINT FK_Employee_ImagemID FOREIGN KEY(ImageID)
	REFERENCES Image(ImageID)
);

DROP TABLE IF EXISTS Supplier
CREATE TABLE Supplier
(
	 SupplierID		INT				NOT NULL	IDENTITY(10000000, 1)
	,Name			VARCHAR(200)	
	,Identification	VARCHAR(50)		NOT NULL
	,ContactID		INT				NOT NULL
	,AddressID		INT				NOT NULL
	,Deleted		BIT				NOT NULL
	,InsertDate		DATETIME		NOT NULL

	CONSTRAINT PK_SupplierID PRIMARY KEY(SupplierID)

	CONSTRAINT FK_Supplier_ContactID FOREIGN KEY(ContactID)
	REFERENCES Contact(ContactID),

	CONSTRAINT PK_Supplier_AddressID FOREIGN KEY(AddressID)
	REFERENCES Address(AddressID)
);

DROP TABLE IF EXISTS Orders
CREATE TABLE Orders
(
	 OrderID		INT			NOT NULL	IDENTITY(10000000, 1)
	,ClientID		INT			NOT NULL
	,Deleted		BIT			NOT NULL
	,InsertDate		DATETIME	NOT NULL

	CONSTRAINT PK_OrderID PRIMARY KEY(OrderID)

	CONSTRAINT FK_Orders_ClientID FOREIGN KEY(ClientID)
	REFERENCES Client(ClientID)
);

DROP TABLE IF EXISTS Items
CREATE TABLE Items
(
	 ItemID			INT			NOT NULL	IDENTITY(10000000, 1)
	,SupplierID		INT			NOT NULL
	,Deleted		BIT			NOT NULL
	,InsertDate		DATETIME	NOT NULL

	CONSTRAINT PK_ItemID PRIMARY KEY(ItemID)

	CONSTRAINT FK_Items_SupplierID FOREIGN KEY(SupplierID)
	REFERENCES Supplier(SupplierID)
);

DROP TABLE IF EXISTS Order_Item
CREATE TABLE Order_Item
(
	 Order_ItemID	INT			NOT NULL	IDENTITY(10000000, 1)
	,OrderID		INT			NOT NULL
	,ItemID			INT			NOT NULL
	,Deleted		BIT			NOT NULL
	,InsertDate		DATETIME	NOT NULL

	CONSTRAINT PK_Order_ItemID PRIMARY KEY(Order_ItemID)

	CONSTRAINT FK_Order_Items_OrderID FOREIGN KEY(OrderID)
	REFERENCES Orders(OrderID),

	CONSTRAINT FK_Order_Items_ItemID FOREIGN KEY(ItemID)
	REFERENCES Items(ItemID)
);

DROP TABLE IF EXISTS Items_Inventory
CREATE TABLE Items_Inventory
(
	 Inventory_ItemID	INT			NOT NULL	IDENTITY(10000000, 1)
	,Quantity			INT			NOT NULL
	,ItemID				INT			NOT NULL
	,SupplierID			INT			NOT NULL
	,Deleted			BIT			NOT NULL
	,InsertDate			DATETIME	NOT NULL
	
	CONSTRAINT Inventory_Item PRIMARY KEY(Inventory_ItemID)

	CONSTRAINT FK_Inventory_Item_ItemID FOREIGN KEY(ItemID)
	REFERENCES Items(ItemID),

	CONSTRAINT FK_Inventory_Item_SupplierID FOREIGN KEY(SupplierID)
	REFERENCES Supplier(SupplierID)
);

DROP TABLE IF EXISTS Logs
CREATE TABLE Logs
(
	 LogID			INT				NOT NULL	IDENTITY(1, 1)
	,Message		VARCHAR(200)	NOT NULL
	,Json			VARCHAR(MAX)
	,ProcedureName	VARCHAR(100)
	,Process		VARCHAR(100)
	,ID				INT
	,Deleted		BIT			NOT NULL
	,InsertDate		DATETIME	NOT NULL

	CONSTRAINT PK_LogID PRIMARY KEY(LogID)
);

/***********************************************************************************************
INSERTING DEFAULT USER ADMINISTRATOR:
************************************************************************************************/
BEGIN TRANSACTION;

	BEGIN TRY

		INSERT INTO Address
		VALUES
		(
			 '01310300'
			,'Avenida Paulista'
			,'0'
			,''
			,'Bela Vista'
			,'São Paulo'
			,'SP'
			,'Brasil'
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

DECLARE @AddressID INT = @@IDENTITY;


BEGIN TRANSACTION;

	BEGIN TRY

		INSERT INTO User_Info
		VALUES
		(
			 'admin'
			,ENCRYPTBYPASSPHRASE('key', 'Admin123@')
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

DECLARE @User_InfoID INT = @@IDENTITY;


BEGIN TRANSACTION;

	BEGIN TRY

		INSERT INTO Contact
		VALUES
		(
			 'admin@admin.com'
			,'11900000000'
			,'1120000000'
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

DECLARE @ContactID INT = @@IDENTITY;


BEGIN TRANSACTION;

	BEGIN TRY

		INSERT INTO Employee
		VALUES
		(
			 'User'
			,NULL
			,'Admin'
			,'00000000000'
			,1
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

DECLARE @EmployeeID INT = @@IDENTITY;


BEGIN TRANSACTION;

	BEGIN TRY

		INSERT INTO Employee_Job
		VALUES
		(
			 0
			,@EmployeeID
			,1
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
/***********************************************************************************************
INSERTING DEFAULT USER ADMINISTRATOR.
************************************************************************************************/