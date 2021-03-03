CREATE TABLE [dbo].[GameInstanceState] (
    [Id]          BIGINT             IDENTITY (1, 1) NOT NULL,
    [GameInstanceId] UNIQUEIDENTIFIER NOT NULL, 
    [DataAsJson]  VARCHAR (2000)     NOT NULL,
    [DateCreated] DATETIMEOFFSET (7) NOT NULL,
    CONSTRAINT [PK_GameInstanceState] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_GameInstanceState_GameInstance] FOREIGN KEY ([GameInstanceId]) REFERENCES [GameInstance]([Id])
);

