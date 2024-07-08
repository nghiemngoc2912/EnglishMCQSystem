USE [master]
GO

/*******************************************************************************
   Drop database if it exists
********************************************************************************/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'EnglishMcqSystem')
BEGIN
	ALTER DATABASE EnglishMcqSystem SET OFFLINE WITH ROLLBACK IMMEDIATE;
	ALTER DATABASE EnglishMcqSystem SET ONLINE;
	DROP DATABASE EnglishMcqSystem;
END

GO

CREATE DATABASE EnglishMcqSystem
GO

USE EnglishMcqSystem
GO
CREATE TABLE Roles(
	Id INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL
);
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    [Password] VARCHAR(20) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
    Email VARCHAR(50),
	IsActive Bit,
    [RoleId] int NOT NULL,
	FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);
GO
CREATE TABLE Tests (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    DifficultyLevel VARCHAR(10),
	NumOfQuestions int,
);
GO
CREATE TABLE Questions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    [Text] NVARCHAR(255) NOT NULL,
    CorrectAnswer NVARCHAR(255) NOT NULL
);
GO
CREATE TABLE TestQuestions (
    TestId INT,
    QuestionId INT,
    PRIMARY KEY (TestId, QuestionId),
    FOREIGN KEY (TestId) REFERENCES Tests(Id),
    FOREIGN KEY (QuestionId) REFERENCES Questions(Id)
);
GO

CREATE TABLE UserTests (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT,
    TestId INT,
    Score FLOAT,
    TestDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (TestId) REFERENCES Tests(Id)
);
GO
CREATE TABLE UserTestAnswers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserTestId INT,
    QuestionId INT,
    UserAnswer NVARCHAR(255),
    FOREIGN KEY (UserTestId) REFERENCES UserTests(id),
    FOREIGN KEY (QuestionId) REFERENCES Questions(id)
);
GO
SET IDENTITY_INSERT UserTestAnswers OFF;
SET IDENTITY_INSERT Roles ON;
INSERT INTO Roles (Id,[Name])
VALUES (1,'Admin'),
	   (2,'Examiner');
SET IDENTITY_INSERT Roles OFF
GO

SET IDENTITY_INSERT Users ON;
INSERT INTO Users (Id,Username, [Password],[Name], Email, RoleId)
VALUES (1,'admin', 'admin1234', 'Nghiem Ngoc','nghiemngoc291204@gmail.com', 1),
       (2,'user1', 'user1234', 'Nghiem Ngoc','nghiemngoc2912@gmail.com', 2);
	   SET IDENTITY_INSERT Users OFF;
	   GO
SET IDENTITY_INSERT Tests ON;
INSERT INTO Tests (Id,[Name], DifficultyLevel, NumOfQuestions)
VALUES (1,'Midterm English SU24', 'Easy', 5),
       (2,'Final Exam SP22', 'Medium', 10);
SET IDENTITY_INSERT Tests OFF;
	   GO

SET IDENTITY_INSERT Questions ON;
INSERT INTO Questions (Id,[Text],CorrectAnswer)
VALUES (1,'With increased economic development, the demand for the metal has grown at a faster ____________ than it is being mined.
A. pace
B. move
C. step
D. manner','A'),
(2,'Greenhouse gases ____________ into the atmosphere cause this long-wave radiation to increase. Thus, heat is trapped inside of our planet and creates a general warming effect.
A. appeared
B. released
C. exposed
D. revealed','B'),
(3,'Photosynthesis is a ____________ that removes carbon dioxide from the atmosphere and converts it into organic carbon and oxygen that feeds almost every ecosystem.
A. formation
B. growth
C. movement
D. process','D'),
(4,'Central co-operative banks ____________ all the business of a joint stock bank.
A. direct
B. manage
C. conduct
D. account','C'),
(5,'In terms of spatial distribution, rainfall during this season was most conducive for augmenting agricultural ____________ this year.
A. consumption
B. saving
C. labour
D.','D'),
(6,'The two friends always back                up in everything they do.
A. each other 
B. one another
C. themselves 
D. ourselves','A'),
(7,'I am taking my first exam next week.                          
A. Cheers
B. Good luck 
C. Well done 
D. Congratulations','B'),
(8,'She sings ________ beautifully that everyone enjoys her performances.
A. so
B. too
C. such
D. very','A'),
(9,'I would like ________ a doctor someday.
A. becoming
B. to become
C. become
D. becomes','B'),
(10,'We have known each other ________ we were children.
A. since
B. for
C. from
D. during','A'),
(11,'The package arrived ________ the expected delivery date.
A. on
B. in
C. at
D. before','A'),
(12,'She finished the project ________ the deadline.
A. before
B. in
C. at
D. on','A'),

(13,'He will arrive ________ 3 PM.
A. in
B. on
C. at
D. before','C'),

(14,'They moved to the city ________ 2019.
A. at
B. in
C. on
D. before','B'),

(15,'The concert starts ________ 8 pm.
A. in
B. on
C. at
D. before','C'),

(16,'I have an appointment ________ Monday.
A. on
B. in
C. at
D. before','A'),

(17,'We met ________ a coffee shop.
A. in
B. on
C. at
D. before','C'),

(18,'She was born ________ July.
A. on
B. in
C. at
D. before','B'),

(19,'They will return ________ a week.
A. on
B. in
C. at
D. before','B'),

(20,'The meeting is scheduled ________ the afternoon.
A. on
B. in
C. at
D. before','B');
SET IDENTITY_INSERT Questions OFF;
GO
INSERT INTO TestQuestions (TestId,QuestionId)
VALUES (1,1),
(1,2),
(1,3),
(1,4),
(1,5),
(2,6),
(2,7),
(2,8),
(2,9),
(2,10),
(2,11),
(2,12),
(2,13),
(2,14),
(2,15);
GO
SET IDENTITY_INSERT UserTests ON;
INSERT INTO UserTests (Id,UserId,TestId,Score,TestDate)
VALUES(1,1,1,10,'2024-05-01 14:30:00'),
	  (2,2,1,10,'2024-05-01 10:30:00'),
	  (3,2,1,10,'2024-05-01 12:30:00'),
	  (4,2,2,10,'2024-05-01 13:30:00');
SET IDENTITY_INSERT UserTests OFF;
GO
SET IDENTITY_INSERT UserTestAnswers ON;
INSERT INTO UserTestAnswers (Id,UserTestId,QuestionId,UserAnswer)
VALUES(1,1,1,'A'),
	  (2,1,2,'B'),
	  (3,1,3,'D'),
	  (4,1,4,'C'),
	  (5,1,5,'D'),
	  (6,2,1,'A'),
	  (7,2,2,'B'),
	  (8,2,3,'D'),
	  (9,2,4,'C'),
	  (10,2,5,'D'),
	  (11,3,1,'A'),
	  (12,3,2,'B'),
	  (13,3,3,'D'),
	  (14,3,4,'C'),
	  (15,3,5,'D'),
	  (16,4,6,'A'),
	  (17,4,7,'B'),
	  (18,4,8,'A'),
	  (19,4,9,'B'),
	  (20,4,10,'A'),
	  (21,4,11,'A'),
	  (22,4,12,'A'),
	  (23,4,13,'C'),
	  (24,4,14,'B'),
	  (25,4,15,'C');
SET IDENTITY_INSERT UserTestAnswers OFF;
GO

