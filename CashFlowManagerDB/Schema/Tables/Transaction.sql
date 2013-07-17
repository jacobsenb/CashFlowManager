CREATE TABLE [dbo].[Transaction]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Narration] NVARCHAR(500) NULL, 
    [Amount] DECIMAL(18, 2) NULL, 
    [ScheduleId] UNIQUEIDENTIFIER NOT NULL, 
    [TransactionTypeId] UNIQUEIDENTIFIER NOT NULL, 
    [ClientId] UNIQUEIDENTIFIER NOT NULL, 
    [StartDate] DATETIME NULL, 
    CONSTRAINT [FK_Transaction_Schedule] FOREIGN KEY (ScheduleId) REFERENCES [Schedule]([Id]), 
    CONSTRAINT [FK_Transaction_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([Id]), 
    CONSTRAINT [FK_Transaction_TransactionType] FOREIGN KEY ([TransactionTypeId]) REFERENCES [TransactionType]([Id])
)

