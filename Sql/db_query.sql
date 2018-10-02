-- EntranceLogger database creation
CREATE DATABASE EntranceLogger;
GO

USE EntranceLogger;
GO

-- Main schema creation
CREATE SCHEMA Main;
GO

-- Tools schema creation
CREATE SCHEMA Tools;
GO

-- EmployeeRoles table creation
CREATE TABLE Main.EmployeeRoles
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	Title nvarchar(256) NOT NULL UNIQUE
);

-- default roles insertion
INSERT INTO Main.EmployeeRoles(Title)
VALUES ('Администратор'), ('Оператор')

-- Employees table creation
CREATE TABLE Main.Employees
(
	Id int NOT NULL PRIMARY KEY IDENTITY,
	Name nvarchar(256) NOT NULL,
	Surname nvarchar(256) NOT NULL,
	Patronymic nvarchar(256) NOT NULL,
	Login nvarchar(256) NOT NULL,
	Password nvarchar(256) NOT NULL CHECK(len(Password) >= 8),
	RoleId int NOT NULL FOREIGN KEY REFERENCES Main.EmployeeRoles (Id),
	IsActive bit NOT NULL
);

-- DocumentTypes table creation
CREATE TABLE Main.DocumentTypes
(
	Id int NOT NULL PRIMARY KEY IDENTITY,
	Title nvarchar(256) NOT NULL UNIQUE
);

-- default document types insertion
INSERT INTO Main.DocumentTypes (Title)
VALUES ('Паспорт'), ('Служебное удостоверение');

-- Visitors table creation
CREATE TABLE Main.Visitors
(
	Id int NOT NULL PRIMARY KEY IDENTITY,
	Name nvarchar(256) NOT NULL,
	Surname nvarchar(256) NOT NULL,
	Patronymic nvarchar(256) NOT NULL,
	DocumentTypeId int NOT NULL FOREIGN KEY REFERENCES Main.DocumentTypes (Id),
	DocumentSeries nchar(15) NOT NULL,
	DocumentNumber int NOT NULL
);

-- Visits table creation
CREATE TABLE Main.Visits
(
	Id int NOT NULL PRIMARY KEY IDENTITY,
	VisitorId int NOT NULL FOREIGN KEY REFERENCES Main.Visitors (Id),
	EmployeeId int NOT NULL FOREIGN KEY REFERENCES Main.Employees (Id),
	EntranceDate datetime NOT NULL,
	ExitDate datetime,
	VisitTarget nvarchar(512),
	Remark nvarchar(512)
);


-- test data insertion
INSERT INTO Main.Visitors (Name, Surname, Patronymic, DocumentTypeId, DocumentSeries, DocumentNumber)
VALUES ('Иван', 'Иванов', 'Иванович', 1, 'AB-2500-ZER', 3513001),
	   ('Федор', 'Федоров', 'Федорович', 2, 'AB-2500-ZZA', 9210031),
	   ('Петр', 'Петров', 'Петрович', 1, 'CB-2565-BEZ', 45139912),
	   ('Вася', 'Васильев', 'Васильевич', 1, 'AA-1120-JJR', 31599901)

INSERT INTO Main.Employees (Name, Surname, Patronymic, Login, Password, RoleId, IsActive)
VALUES ('Филлип', 'Филипский', 'Александрович', 'FILLIP1990', '12345789', 1, 1),
	   ('Александр', 'Александров', 'Николаевич', 'ALEKS_1', 'qwertyuiop', 2, 1)

INSERT INTO Main.Visits (VisitorId, EmployeeId, EntranceDate, ExitDate, VisitTarget, Remark)
VALUES (1, 2, GETDATE(), NULL, NULL, NULL),
	   (2, 2, GETDATE(), NULL, NULL, NULL),
	   (3, 1, GETDATE(), NULL, NULL, NULL)

SELECT * FROM Main.DocumentTypes;
SELECT * FROM Main.EmployeeRoles;
SELECT * FROM Main.Employees;
SELECT * FROM Main.Visitors;
SELECT * FROM Main.Visits;

