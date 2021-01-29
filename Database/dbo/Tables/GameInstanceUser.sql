CREATE TABLE [dbo].[GameInstanceUser] (
    [Id]             BIGINT           IDENTITY (1, 1) NOT NULL,
    [GameInstanceId] UNIQUEIDENTIFIER NOT NULL,
    [UserId]         INT              NOT NULL,
    [Role]           VARCHAR (50)     NOT NULL,
    CONSTRAINT [PK_GameInstanceUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GameInstanceUser_GameInstance] FOREIGN KEY ([GameInstanceId]) REFERENCES [GameInstance]([Id]),
    CONSTRAINT [FK_GameInstanceUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
);

