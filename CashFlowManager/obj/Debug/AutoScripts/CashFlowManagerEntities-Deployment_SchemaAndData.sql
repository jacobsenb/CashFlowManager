SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankAccount](
	[Id] [uniqueidentifier] NOT NULL,
	[AccountName] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccountNumber] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Balance] [decimal](18, 2) NULL,
	[ClientId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PracticeId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Practice](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Sequence] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[Id] [uniqueidentifier] NOT NULL,
	[Narration] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Amount] [decimal](18, 2) NULL,
	[ScheduleId] [uniqueidentifier] NOT NULL,
	[TransactionTypeId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[StartDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Sequence] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
INSERT [dbo].[BankAccount] ([Id], [AccountName], [AccountNumber], [Balance], [ClientId]) VALUES (N'b7b075c9-b3a2-4458-9f00-1e16e772ed13', N'streamline', N'112391238129083', NULL, NULL)
GO
INSERT [dbo].[BankAccount] ([Id], [AccountName], [AccountNumber], [Balance], [ClientId]) VALUES (N'fae54faf-dfba-4144-a648-25d88372e409', N'cheque', N'6123784682368', CAST(-98.23 AS Decimal(18, 2)), N'c378d43b-8d04-4d8c-b053-ef4cf52cbdc5')
GO
INSERT [dbo].[BankAccount] ([Id], [AccountName], [AccountNumber], [Balance], [ClientId]) VALUES (N'e66ab604-db94-4f8b-bef4-81a3d57519eb', N'streamline', N'112391238129083', CAST(15000.55 AS Decimal(18, 2)), N'c378d43b-8d04-4d8c-b053-ef4cf52cbdc5')
GO
INSERT [dbo].[BankAccount] ([Id], [AccountName], [AccountNumber], [Balance], [ClientId]) VALUES (N'3da18a4e-439c-434b-89d0-8a6bd9c93ad4', N'streamline', N'112391238129083', NULL, NULL)
GO
INSERT [dbo].[BankAccount] ([Id], [AccountName], [AccountNumber], [Balance], [ClientId]) VALUES (N'9fac76f1-c0c9-4f77-aad9-b740f8debb88', N'streamline', N'112391238129083', NULL, NULL)
GO
INSERT [dbo].[BankAccount] ([Id], [AccountName], [AccountNumber], [Balance], [ClientId]) VALUES (N'8647227b-d4fb-45b4-a908-e1332f64a034', N'streamline', N'112391238129083', NULL, NULL)
GO
INSERT [dbo].[BankAccount] ([Id], [AccountName], [AccountNumber], [Balance], [ClientId]) VALUES (N'ef077bee-9a9b-4f58-a1d3-fddea6f57593', N'streamline', N'112391238129083', NULL, NULL)
GO
INSERT [dbo].[Client] ([Id], [Name], [PracticeId]) VALUES (N'ef0e215a-68be-4cbb-8543-7d0204469c8a', N'Bob', N'506b1019-b825-42fd-9ae4-ea1d74753c92')
GO
INSERT [dbo].[Client] ([Id], [Name], [PracticeId]) VALUES (N'c378d43b-8d04-4d8c-b053-ef4cf52cbdc5', N'Client1', N'506b1019-b825-42fd-9ae4-ea1d74753c92')
GO
INSERT [dbo].[Practice] ([Id], [Name]) VALUES (N'506b1019-b825-42fd-9ae4-ea1d74753c92', N'Brett''s Practice')
GO
INSERT [dbo].[Schedule] ([Id], [Name], [Sequence]) VALUES (N'7d839fdc-c273-4be4-b9f2-028f818e53bb', N'One Off', 1)
GO
INSERT [dbo].[Schedule] ([Id], [Name], [Sequence]) VALUES (N'3cdd6bf9-fa88-41e6-a288-03a80f494c4c', N'Yearly', 6)
GO
INSERT [dbo].[Schedule] ([Id], [Name], [Sequence]) VALUES (N'a0d9330c-8e55-441a-b31f-6679c657b3a0', N'Weekly', 3)
GO
INSERT [dbo].[Schedule] ([Id], [Name], [Sequence]) VALUES (N'295dc3d3-8ebf-4d26-a8a8-6ec19b9c06e0', N'Daily', 2)
GO
INSERT [dbo].[Schedule] ([Id], [Name], [Sequence]) VALUES (N'67160043-4e9e-4e3f-9181-c501adf48c49', N'Monthly', 5)
GO
INSERT [dbo].[Schedule] ([Id], [Name], [Sequence]) VALUES (N'9384e00a-9332-4da6-9d8e-e849cb0000a0', N'Fortnightly', 4)
GO
INSERT [dbo].[Transaction] ([Id], [Narration], [Amount], [ScheduleId], [TransactionTypeId], [ClientId], [StartDate]) VALUES (N'0312c202-03e0-4f06-90bb-02751883935e', N'Rebate', CAST(456.23 AS Decimal(18, 2)), N'7d839fdc-c273-4be4-b9f2-028f818e53bb', N'b7fbf3d0-3ef8-4465-af4e-e0c8d230e13c', N'c378d43b-8d04-4d8c-b053-ef4cf52cbdc5', CAST(0x0000A21D00000000 AS DateTime))
GO
INSERT [dbo].[Transaction] ([Id], [Narration], [Amount], [ScheduleId], [TransactionTypeId], [ClientId], [StartDate]) VALUES (N'6f177dc7-f228-4d7b-b611-0531a86672b3', N'ABC', CAST(22.50 AS Decimal(18, 2)), N'7d839fdc-c273-4be4-b9f2-028f818e53bb', N'445cdd4e-ccfa-41f0-89a4-771275881dea', N'ef0e215a-68be-4cbb-8543-7d0204469c8a', CAST(0x0000A1F900000000 AS DateTime))
GO
INSERT [dbo].[Transaction] ([Id], [Narration], [Amount], [ScheduleId], [TransactionTypeId], [ClientId], [StartDate]) VALUES (N'7e06d714-7462-4fbf-b642-098d80d6d85c', N'Groceries', CAST(150.00 AS Decimal(18, 2)), N'a0d9330c-8e55-441a-b31f-6679c657b3a0', N'445cdd4e-ccfa-41f0-89a4-771275881dea', N'c378d43b-8d04-4d8c-b053-ef4cf52cbdc5', CAST(0x0000A1FC00000000 AS DateTime))
GO
INSERT [dbo].[Transaction] ([Id], [Narration], [Amount], [ScheduleId], [TransactionTypeId], [ClientId], [StartDate]) VALUES (N'd65b43a7-5e4a-43e8-8844-105d966b4ecf', N'Petrol', CAST(60.00 AS Decimal(18, 2)), N'a0d9330c-8e55-441a-b31f-6679c657b3a0', N'445cdd4e-ccfa-41f0-89a4-771275881dea', N'c378d43b-8d04-4d8c-b053-ef4cf52cbdc5', CAST(0x0000A1F900000000 AS DateTime))
GO
INSERT [dbo].[Transaction] ([Id], [Narration], [Amount], [ScheduleId], [TransactionTypeId], [ClientId], [StartDate]) VALUES (N'1de0e3d3-cdb5-4bda-bc2a-747ccafed3cf', N'wages', CAST(500.00 AS Decimal(18, 2)), N'a0d9330c-8e55-441a-b31f-6679c657b3a0', N'b7fbf3d0-3ef8-4465-af4e-e0c8d230e13c', N'c378d43b-8d04-4d8c-b053-ef4cf52cbdc5', CAST(0x0000A1F700000000 AS DateTime))
GO
INSERT [dbo].[Transaction] ([Id], [Narration], [Amount], [ScheduleId], [TransactionTypeId], [ClientId], [StartDate]) VALUES (N'2a98c820-9515-4b3a-86ff-a1c1cf06e31f', N'Christmas Presents', CAST(25.00 AS Decimal(18, 2)), N'7d839fdc-c273-4be4-b9f2-028f818e53bb', N'445cdd4e-ccfa-41f0-89a4-771275881dea', N'c378d43b-8d04-4d8c-b053-ef4cf52cbdc5', CAST(0x0000A1FE00000000 AS DateTime))
GO
INSERT [dbo].[Transaction] ([Id], [Narration], [Amount], [ScheduleId], [TransactionTypeId], [ClientId], [StartDate]) VALUES (N'a6fb6587-c264-42e4-9c63-afd7d2991633', N'Rates', CAST(20000.00 AS Decimal(18, 2)), N'7d839fdc-c273-4be4-b9f2-028f818e53bb', N'445cdd4e-ccfa-41f0-89a4-771275881dea', N'c378d43b-8d04-4d8c-b053-ef4cf52cbdc5', CAST(0x0000A20E00000000 AS DateTime))
GO
INSERT [dbo].[TransactionType] ([Id], [Name], [Sequence]) VALUES (N'445cdd4e-ccfa-41f0-89a4-771275881dea', N'Expense', 1)
GO
INSERT [dbo].[TransactionType] ([Id], [Name], [Sequence]) VALUES (N'b7fbf3d0-3ef8-4465-af4e-e0c8d230e13c', N'Income', 2)
GO
ALTER TABLE [dbo].[BankAccount]  WITH NOCHECK ADD  CONSTRAINT [FK_BankAccount_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([Id])
GO
ALTER TABLE [dbo].[BankAccount] CHECK CONSTRAINT [FK_BankAccount_Client]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Practice] FOREIGN KEY([PracticeId])
REFERENCES [dbo].[Practice] ([Id])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Practice]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Client]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Schedule] FOREIGN KEY([ScheduleId])
REFERENCES [dbo].[Schedule] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Schedule]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_TransactionType] FOREIGN KEY([TransactionTypeId])
REFERENCES [dbo].[TransactionType] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_TransactionType]
GO
