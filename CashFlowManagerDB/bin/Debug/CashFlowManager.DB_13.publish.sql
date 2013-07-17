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
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


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

INSERT INTO Schedule(Id,Name) VALUES ('7D839FDC-C273-4BE4-B9F2-028F818E53BB','One Off','1')
INSERT INTO Schedule(Id,Name) VALUES ('295DC3D3-8EBF-4D26-A8A8-6EC19B9C06E0','Daily','2')
INSERT INTO Schedule(Id,Name) VALUES ('A0D9330C-8E55-441A-B31F-6679C657B3A0','Weekly','3')
INSERT INTO Schedule(Id,Name) VALUES ('9384E00A-9332-4DA6-9D8E-E849CB0000A0','Fortnightly','4')
INSERT INTO Schedule(Id,Name) VALUES ('67160043-4E9E-4E3F-9181-C501ADF48C49','Monthly','5')
INSERT INTO Schedule (Id,Name) VALUES('3CDD6BF9-FA88-41E6-A288-03A80F494C4C','Yearly','6')
GO

GO
PRINT N'Update complete.';


GO
