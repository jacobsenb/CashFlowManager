﻿/*
Deployment script for CashFlowManager

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "CashFlowManager"
:setvar DefaultFilePrefix "CashFlowManager"
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
	DROP TABLE [Transaction]
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='Schedule')
BEGIN
	DELETE FROM  Schedule
	DROP TABLE Schedule
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TransactionType')
BEGIN
	DELETE FROM TransactionType
	DROP TABLE TransactionType
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='BankAccount')
BEGIN
	DELETE FROM BankAccount
	DROP TABLE BankAccount
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='Client')
BEGIN
	DELETE FROM Client
	DROP TABLE Client
END
GO


IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='webpages_UsersInRoles')
BEGIN
	DELETE FROM webpages_UsersInRoles
	DROP TABLE webpages_UsersInRoles
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='webpages_Roles')
BEGIN
	DELETE FROM webpages_Roles
	DROP TABLE webpages_Roles
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='webpages_OAuthMembership')
BEGIN
	DELETE FROM webpages_OAuthMembership
	DROP TABLE webpages_OAuthMembership
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='webpages_Membership')
BEGIN
	DELETE FROM webpages_Membership
	DROP TABLE webpages_Membership
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='UserProfile')
BEGIN
	DELETE FROM UserProfile
	DROP TABLE UserProfile
END
GO

IF EXISTS (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='Practice')
BEGIN
	DELETE FROM Practice
	DROP TABLE Practice
END
GO





GO

GO
PRINT N'Creating [dbo].[Practice]...';


GO
CREATE TABLE [dbo].[Practice] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR (255)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[UserProfile]...';


GO
CREATE TABLE [dbo].[UserProfile] (
    [UserId]     INT              IDENTITY (1, 1) NOT NULL,
    [UserName]   NVARCHAR (MAX)   NULL,
    [PracticeId] UNIQUEIDENTIFIER NULL,
    [Email]      NVARCHAR (500)   NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC) ON [PRIMARY]
);


GO
PRINT N'Creating [dbo].[webpages_Membership]...';


GO
CREATE TABLE [dbo].[webpages_Membership] (
    [UserId]                                  INT            NOT NULL,
    [CreateDate]                              DATETIME       NULL,
    [ConfirmationToken]                       NVARCHAR (128) NULL,
    [IsConfirmed]                             BIT            NULL,
    [LastPasswordFailureDate]                 DATETIME       NULL,
    [PasswordFailuresSinceLastSuccess]        INT            NOT NULL,
    [Password]                                NVARCHAR (128) NOT NULL,
    [PasswordChangedDate]                     DATETIME       NULL,
    [PasswordSalt]                            NVARCHAR (128) NOT NULL,
    [PasswordVerificationToken]               NVARCHAR (128) NULL,
    [PasswordVerificationTokenExpirationDate] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC) ON [PRIMARY]
);


GO
PRINT N'Creating [dbo].[webpages_OAuthMembership]...';


GO
CREATE TABLE [dbo].[webpages_OAuthMembership] (
    [Provider]       NVARCHAR (30)  NOT NULL,
    [ProviderUserId] NVARCHAR (100) NOT NULL,
    [UserId]         INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Provider] ASC, [ProviderUserId] ASC) ON [PRIMARY]
);


GO
PRINT N'Creating [dbo].[webpages_Roles]...';


GO
CREATE TABLE [dbo].[webpages_Roles] (
    [RoleId]   INT            IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (256) NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC) ON [PRIMARY],
    UNIQUE NONCLUSTERED ([RoleName] ASC) ON [PRIMARY]
);


GO
PRINT N'Creating [dbo].[webpages_UsersInRoles]...';


GO
CREATE TABLE [dbo].[webpages_UsersInRoles] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC) ON [PRIMARY]
);


GO
PRINT N'Creating Default Constraint on [dbo].[webpages_Membership]....';


GO
ALTER TABLE [dbo].[webpages_Membership]
    ADD DEFAULT ((0)) FOR [IsConfirmed];


GO
PRINT N'Creating Default Constraint on [dbo].[webpages_Membership]....';


GO
ALTER TABLE [dbo].[webpages_Membership]
    ADD DEFAULT ((0)) FOR [PasswordFailuresSinceLastSuccess];


GO
PRINT N'Creating FK_UserProfile_Practice...';


GO
ALTER TABLE [dbo].[UserProfile] WITH NOCHECK
    ADD CONSTRAINT [FK_UserProfile_Practice] FOREIGN KEY ([PracticeId]) REFERENCES [dbo].[Practice] ([Id]);


GO
PRINT N'Creating fk_RoleId...';


GO
ALTER TABLE [dbo].[webpages_UsersInRoles] WITH NOCHECK
    ADD CONSTRAINT [fk_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[webpages_Roles] ([RoleId]);


GO
PRINT N'Creating fk_UserId...';


GO
ALTER TABLE [dbo].[webpages_UsersInRoles] WITH NOCHECK
    ADD CONSTRAINT [fk_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile] ([UserId]);


GO
PRINT N'Creating FK_Client_Practice...';


GO
ALTER TABLE [dbo].[Client] WITH NOCHECK
    ADD CONSTRAINT [FK_Client_Practice] FOREIGN KEY ([PracticeId]) REFERENCES [dbo].[Practice] ([Id]);


GO
PRINT N'Creating FK_Transaction_Client...';


GO
ALTER TABLE [dbo].[Transaction] WITH NOCHECK
    ADD CONSTRAINT [FK_Transaction_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]);


GO
PRINT N'Creating FK_Transaction_Schedule...';


GO
ALTER TABLE [dbo].[Transaction] WITH NOCHECK
    ADD CONSTRAINT [FK_Transaction_Schedule] FOREIGN KEY ([ScheduleId]) REFERENCES [dbo].[Schedule] ([Id]);


GO
PRINT N'Creating FK_Transaction_TransactionType...';


GO
ALTER TABLE [dbo].[Transaction] WITH NOCHECK
    ADD CONSTRAINT [FK_Transaction_TransactionType] FOREIGN KEY ([TransactionTypeId]) REFERENCES [dbo].[TransactionType] ([Id]);


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



INSERT INTO Schedule(Id,Name,Sequence) VALUES ('7D839FDC-C273-4BE4-B9F2-028F818E53BB','One Off',1)
INSERT INTO Schedule(Id,Name,Sequence) VALUES ('295DC3D3-8EBF-4D26-A8A8-6EC19B9C06E0','Daily',2)
INSERT INTO Schedule(Id,Name,Sequence) VALUES ('A0D9330C-8E55-441A-B31F-6679C657B3A0','Weekly',3)
INSERT INTO Schedule(Id,Name,Sequence) VALUES ('9384E00A-9332-4DA6-9D8E-E849CB0000A0','Fortnightly',4)
INSERT INTO Schedule(Id,Name,Sequence) VALUES ('67160043-4E9E-4E3F-9181-C501ADF48C49','Monthly',5)
INSERT INTO Schedule (Id,Name,Sequence) VALUES('3CDD6BF9-FA88-41E6-A288-03A80F494C4C','Yearly',6)
GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[UserProfile] WITH CHECK CHECK CONSTRAINT [FK_UserProfile_Practice];

ALTER TABLE [dbo].[webpages_UsersInRoles] WITH CHECK CHECK CONSTRAINT [fk_RoleId];

ALTER TABLE [dbo].[webpages_UsersInRoles] WITH CHECK CHECK CONSTRAINT [fk_UserId];

ALTER TABLE [dbo].[Client] WITH CHECK CHECK CONSTRAINT [FK_Client_Practice];

ALTER TABLE [dbo].[Transaction] WITH CHECK CHECK CONSTRAINT [FK_Transaction_Client];

ALTER TABLE [dbo].[Transaction] WITH CHECK CHECK CONSTRAINT [FK_Transaction_Schedule];

ALTER TABLE [dbo].[Transaction] WITH CHECK CHECK CONSTRAINT [FK_Transaction_TransactionType];


GO
PRINT N'Update complete.';


GO
