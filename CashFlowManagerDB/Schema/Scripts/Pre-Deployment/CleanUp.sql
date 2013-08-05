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





