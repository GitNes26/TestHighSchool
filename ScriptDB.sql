USE master
GO

CREATE DATABASE DB_TestHighSchool
GO 

USE DB_TestHighSchool
GO

CREATE TABLE Subjects(
	Id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name nchar(100) NOT NULL,
	Active tinyint NOT NULL DEFAULT 1
)

CREATE TABLE Teachers(
	Id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name nchar(100) NOT NULL,
	LastName nchar(100) NOT NULL,
	Tel nchar(14) NOT NULL,
	Email nchar(45) NOT NULL,
	SubjectId int FOREIGN KEY REFERENCES Subjects(Id) NOT NULL,
	Active tinyint NOT NULL DEFAULT 1
)

CREATE TABLE Students(
	Id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	NumControl int NOT NULL,
	Name nchar(100) NOT NULL,
	LastName nchar(100) NOT NULL,
	Tel nchar(14) NOT NULL,
	Email nchar(45) NOT NULL,
	Active tinyint NOT NULL DEFAULT 1
)

CREATE TABLE Courses(
	Id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	TeacherId int FOREIGN KEY REFERENCES Teachers(Id) NOT NULL,
	StudentId int FOREIGN KEY REFERENCES Students(Id) NOT NULL,
	Evaluation1 decimal (1,1) NOT NULL DEFAULT 0,
	Evaluation2 decimal (1,1) NOT NULL DEFAULT 0,
	Evaluation3 decimal (1,1) NOT NULL DEFAULT 0,
	FinalAverage decimal(1,1) NULL,
)


--CREATE TABLE StudentTeacher(
--	Id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
--	StudentId int FOREIGN KEY REFERENCES Students(Id) NOT NULL,
--	TeacherId int FOREIGN KEY REFERENCES Teachers(Id) NOT NULL,
--	FinalAverage decimal(1,1) NULL,
--	MaxEvaluations int NOT NULL DEFAULT 3
--)

--CREATE TABLE Evaluations(
--	Id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
--	StudentTeacherId int FOREIGN KEY REFERENCES StudentTeacher(Id) NOT NULL,
--	Qualification decimal(1,1) NOT NULL,
--)