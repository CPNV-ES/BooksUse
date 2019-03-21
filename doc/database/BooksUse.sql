-- Date: Feb 2019
-- Author: X. Carrel
-- Goal: Creates the BooksUse DB as ASP course material

USE master
GO

-- First delete the database if it exists
IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'BooksUse'))
BEGIN
	USE master
	ALTER DATABASE BooksUse SET SINGLE_USER WITH ROLLBACK IMMEDIATE; -- Disconnect users the hard way (we cannot drop the db if someone's connected)
	DROP DATABASE BooksUse -- Destroy it
END
GO

-- Second ensure we have the proper directory structure
SET NOCOUNT ON;
GO
CREATE TABLE #ResultSet (Directory varchar(200)) -- Temporary table (name starts with #) -> will be automatically destroyed at the end of the session

INSERT INTO #ResultSet EXEC master.sys.xp_subdirs 'c:\' -- Stored procedure that lists subdirectories

(Select * FROM #ResultSet where Directory = 'DATA')
	EXEC master.sys.xp_create_subdir 'C:\DATA\' -- create DATA

DELETE FROM #ResultSet -- start over for MSSQL subdir
INSERT INTO #ResultSet EXEC master.sys.xp_subdirs 'c:\DATA'

(Select * FROM #ResultSet where Directory = 'MSSQL')
	EXEC master.sys.xp_create_subdir 'C:\DATA\MSSQL'

DROP TABLE #ResultSet -- Explicitely delete it because the script may be executed multiple times during the same session
GO

-- Everything is ready, we can create the db
CREATE DATABASE BooksUse ON  PRIMARY 
( NAME = 'BooksUse_data', FILENAME = 'C:\DATA\MSSQL\BooksUse.mdf' , SIZE = 20480KB , MAXSIZE = 51200KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = 'BooksUse_log', FILENAME = 'C:\DATA\MSSQL\BooksUse.ldf' , SIZE = 10240KB , MAXSIZE = 20480KB , FILEGROWTH = 1024KB )

GO

USE BooksUse
GO

-- Create tables 
CREATE TABLE Years (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  title INT NOT NULL,
  [open] BIT NOT NULL)

CREATE TABLE Roles (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  name VARCHAR(25) NOT NULL)

CREATE TABLE SchoolClasses (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  name VARCHAR(25) NOT NULL,
  studentsnumber INT NOT NULL)

CREATE TABLE Books (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  title VARCHAR(200) NOT NULL,
  ISBN VARCHAR(200) NOT NULL UNIQUE,
  author VARCHAR(45) NOT NULL,
  unitsInStock INT NULL DEFAULT NULL,
  price DECIMAL(6,2) NULL DEFAULT NULL)

CREATE TABLE Users (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  intranetUserID INT NOT NULL UNIQUE,
  initials VARCHAR(3) NOT NULL UNIQUE,
  firstName VARCHAR(45) NOT NULL,
  lastName VARCHAR(45) NOT NULL,
  FK_Roles INT NOT NULL,
  email VARCHAR(45) NULL DEFAULT NULL UNIQUE,
  phone VARCHAR(20) NULL DEFAULT NULL UNIQUE,
  [password] VARCHAR(255) NOT NULL,
  CONSTRAINT FK_Roles
    FOREIGN KEY (FK_Roles)
    REFERENCES Roles (id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,)

CREATE TABLE Requests (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  approved INT NOT NULL,
  FK_Years INT NOT NULL,
  FK_Users INT NOT NULL,
  FK_Books INT NOT NULL,
  CONSTRAINT FK_Years
    FOREIGN KEY (FK_Years)
    REFERENCES Years (id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FK_Users
    FOREIGN KEY (FK_Users)
    REFERENCES Users (id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FK_Books
    FOREIGN KEY (FK_Books)
    REFERENCES Books (id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)

CREATE TABLE SchoolClasses_Requests (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  FK_SchoolClasses INT NOT NULL,
  FK_Requests INT NOT NULL,
  CONSTRAINT FK_SchoolClasses
    FOREIGN KEY (FK_SchoolClasses)
    REFERENCES SchoolClasses (id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FK_Requests
    FOREIGN KEY (FK_Requests)
    REFERENCES Requests (id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)