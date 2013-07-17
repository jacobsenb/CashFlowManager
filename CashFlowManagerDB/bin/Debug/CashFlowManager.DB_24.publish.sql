﻿/*
Deployment script for C:\USERS\BRETT.JACOBSEN\DOCUMENTS\VISUAL STUDIO 2012\PROJECTS\CASHFLOWMANAGER\CASHFLOWMANAGER\APP_DATA\CASHFLOWMANAGER.MDF

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "C:\USERS\BRETT.JACOBSEN\DOCUMENTS\VISUAL STUDIO 2012\PROJECTS\CASHFLOWMANAGER\CASHFLOWMANAGER\APP_DATA\CASHFLOWMANAGER.MDF"
:setvar DefaultFilePrefix "C_\USERS\BRETT.JACOBSEN\DOCUMENTS\VISUAL STUDIO 2012\PROJECTS\CASHFLOWMANAGER\CASHFLOWMANAGER\APP_DATA\CASHFLOWMANAGER.MDF_"
:setvar DefaultDataPath "C:\Users\Brett.jacobsen\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\v11.0\"
:setvar DefaultLogPath "C:\Users\Brett.jacobsen\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\v11.0\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='Transaction')
BEGIN
	DELETE FROM [Transaction]
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='Schedule')
BEGIN
	DELETE FROM  Schedule
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TransactionType')
BEGIN
	DELETE FROM TransactionType
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='BankAccounts')
BEGIN
	DELETE FROM BankAccounts
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='Client')
BEGIN
	DELETE FROM Client
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='Practice')
BEGIN
	DELETE FROM Practice
END
GO




GO

GO
/*
The type for column Balance in table [dbo].[BankAccount] is currently  NVARCHAR (255) NULL but is being changed to  DECIMAL (18, 2) NULL. Data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[BankAccount])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Altering [dbo].[BankAccount]...';


GO
ALTER TABLE [dbo].[BankAccount] ALTER COLUMN [Balance] DECIMAL (18, 2) NULL;


GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/



INSERT INTO TransactionType VALUES ('445CDD4E-CCFA-41F0-89A4-771275881DEA','Expense',1)
INSERT INTO TransactionType VALUES ('B7FBF3D0-3EF8-4465-AF4E-E0C8D230E13C','Income',2)
GO

GO
PRINT N'Update complete.';


GO