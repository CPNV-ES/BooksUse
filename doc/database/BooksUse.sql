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

-- Create tables 
CREATE TABLE books (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  title VARCHAR(200) NOT NULL,
  ISBN INT NOT NULL UNIQUE,
  year DATE NULL DEFAULT NULL,
  author VARCHAR(45) NOT NULL,
  unitsInStock INT NULL DEFAULT NULL,
  price DECIMAL(6,2) NULL DEFAULT NULL)

CREATE TABLE teachers (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  intranetUserID INT NOT NULL UNIQUE,
  initials VARCHAR(3) NOT NULL UNIQUE,
  firstName VARCHAR(45) NOT NULL,
  lastName VARCHAR(45) NOT NULL,
  userRole INT NOT NULL,
  email VARCHAR(45) NULL DEFAULT NULL UNIQUE,
  phone VARCHAR(20) NULL DEFAULT NULL UNIQUE)

CREATE TABLE requests (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  forYear INT NOT NULL,
  approved INT NOT NULL,
  Teacher_id INT NOT NULL,
  Book_id INT NOT NULL,
  CONSTRAINT fk_Orders_Books1
    FOREIGN KEY (Book_id)
    REFERENCES books (id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_Orders_Teachers
    FOREIGN KEY (Teacher_id)
    REFERENCES teachers (id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
