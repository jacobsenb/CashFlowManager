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