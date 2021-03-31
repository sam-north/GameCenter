CREATE TABLE [dbo].[GameInstanceUserMessage] (
    [Id]             BIGINT           IDENTITY (1, 1) NOT NULL,
    [GameInstanceId] UNIQUEIDENTIFIER NOT NULL,
    [UserId]         INT              NOT NULL,
    [Text]        VARCHAR (500)    NOT NULL,
    [DateCreated] DATETIMEOFFSET (7)  NOT NULL,
    [DateDeleted] DATETIMEOFFSET (7)  NULL,
    CONSTRAINT [PK_GameInstanceUserMessage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GameInstanceUserMessage_GameInstance] FOREIGN KEY ([GameInstanceId]) REFERENCES [GameInstance]([Id]),
    CONSTRAINT [FK_GameInstanceUserMessage_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
);

