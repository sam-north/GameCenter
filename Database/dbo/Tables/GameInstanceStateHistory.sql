CREATE TABLE [dbo].[GameInstanceStateHistory] (
    [Id]          BIGINT             IDENTITY (1, 1) NOT NULL,
    [GameInstanceId] UNIQUEIDENTIFIER NOT NULL, 
    [DataAsJson]  VARCHAR (2000)     NOT NULL,
    [DateCreated] DATETIMEOFFSET (7) NOT NULL,
    [Message] VARCHAR(50)          NOT NULL, 
    CONSTRAINT [PK_GameInstanceStateHistory] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_GameInstanceStateHistory_GameInstance] FOREIGN KEY ([GameInstanceId]) REFERENCES [GameInstance]([Id])
);

