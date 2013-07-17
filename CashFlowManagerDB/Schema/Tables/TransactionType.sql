CREATE TABLE [dbo].[TransactionType]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(255) NULL, 
    [Sequence] INT NULL
)
