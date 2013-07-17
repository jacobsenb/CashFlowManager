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
The type for column Id in table [dbo].[Schedule] is currently  INT IDENTITY (1, 1) NOT NULL but is being changed to  UNIQUEIDENTIFIER NOT NULL. There is no implicit or explicit conversion.
*/

IF EXISTS (select top 1 1 from [dbo].[Schedule])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The type for column Id in table [dbo].[Transaction] is currently  INT NOT NULL but is being changed to  UNIQUEIDENTIFIER NOT NULL. There is no implicit or explicit conversion.

The type for column ScheduleId in table [dbo].[Transaction] is currently  INT NULL but is being changed to  UNIQUEIDENTIFIER NULL. There is no implicit or explicit conversion.
*/

IF EXISTS (select top 1 1 from [dbo].[Transaction])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping FK_Transaction_Schedule...';


GO
ALTER TABLE [dbo].[Transaction] DROP CONSTRAINT [FK_Transaction_Schedule];


GO
PRINT N'Starting rebuilding table [dbo].[Schedule]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Schedule] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR (255)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Schedule])
    BEGIN
        
        INSERT INTO [dbo].[tmp_ms_xx_Schedule] ([Id], [Name])
        SELECT   [Id],
                 [Name]
        FROM     [dbo].[Schedule]
        ORDER BY [Id] ASC;
        
    END

DROP TABLE [dbo].[Schedule];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Schedule]', N'Schedule';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[Transaction]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Transaction] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [Narration]  NVARCHAR (500)   NULL,
    [Amount]     DECIMAL (18, 2)  NULL,
    [ScheduleId] UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Transaction])
    BEGIN
        
        INSERT INTO [dbo].[tmp_ms_xx_Transaction] ([Id], [Narration], [Amount], [ScheduleId])
        SELECT   [Id],
                 [Narration],
                 [Amount],
                 [ScheduleId]
        FROM     [dbo].[Transaction]
        ORDER BY [Id] ASC;
        
    END

DROP TABLE [dbo].[Transaction];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Transaction]', N'Transaction';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating FK_Transaction_Schedule...';


GO
ALTER TABLE [dbo].[Transaction] WITH NOCHECK
    ADD CONSTRAINT [FK_Transaction_Schedule] FOREIGN KEY ([ScheduleId]) REFERENCES [dbo].[Schedule] ([Id]);


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

DELETE FROM  Schedule

INSERT INTO Schedule(Name) VALUES ('One Off')
INSERT INTO Schedule(Name) VALUES ('Daily')
INSERT INTO Schedule(Name) VALUES ('Weekly')
INSERT INTO Schedule(Name) VALUES ('Fortnightly')
INSERT INTO Schedule(Name) VALUES ('Monthly')
INSERT INTO Schedule (Name) VALUES('Yearly')
GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Transaction] WITH CHECK CHECK CONSTRAINT [FK_Transaction_Schedule];


GO
PRINT N'Update complete.';


GO