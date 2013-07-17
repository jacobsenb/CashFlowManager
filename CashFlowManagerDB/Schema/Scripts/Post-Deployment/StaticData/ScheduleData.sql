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



INSERT INTO Schedule(Id,Name,Sequence) VALUES ('7D839FDC-C273-4BE4-B9F2-028F818E53BB','One Off',1)
INSERT INTO Schedule(Id,Name,Sequence) VALUES ('295DC3D3-8EBF-4D26-A8A8-6EC19B9C06E0','Daily',2)
INSERT INTO Schedule(Id,Name,Sequence) VALUES ('A0D9330C-8E55-441A-B31F-6679C657B3A0','Weekly',3)
INSERT INTO Schedule(Id,Name,Sequence) VALUES ('9384E00A-9332-4DA6-9D8E-E849CB0000A0','Fortnightly',4)
INSERT INTO Schedule(Id,Name,Sequence) VALUES ('67160043-4E9E-4E3F-9181-C501ADF48C49','Monthly',5)
INSERT INTO Schedule (Id,Name,Sequence) VALUES('3CDD6BF9-FA88-41E6-A288-03A80F494C4C','Yearly',6)