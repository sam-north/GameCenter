CREATE TABLE [dbo].[GameInstance] (
    [Id]          UNIQUEIDENTIFIER   NOT NULL,
    [GameId]      INT                NOT NULL,
    [DateCreated] DATETIMEOFFSET (7) NOT NULL,
    [IsDeleted]   BIT                NOT NULL,
    CONSTRAINT [PK_GameInstance] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_GameInstance_Game] FOREIGN KEY ([GameId]) REFERENCES [Game]([Id])
);

