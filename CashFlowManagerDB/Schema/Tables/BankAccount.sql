CREATE TABLE [dbo].[BankAccount]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [AccountName] NVARCHAR(255) NULL, 
    [AccountNumber] NVARCHAR(255) NULL, 
    [Balance] DECIMAL(18, 2) NULL, 
    [ClientId] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [FK_BankAccount_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([Id])
)
