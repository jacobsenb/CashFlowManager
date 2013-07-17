﻿CREATE TABLE [dbo].[UserProfile](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
[PracticeId] UNIQUEIDENTIFIER NULL, 
    [Email] NVARCHAR(500) NULL, 
    PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY], 
    CONSTRAINT [FK_UserProfile_Practice] FOREIGN KEY ([PracticeId]) REFERENCES [Practice]([Id])
) 
