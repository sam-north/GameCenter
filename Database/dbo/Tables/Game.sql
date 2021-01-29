CREATE TABLE [dbo].[Game] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (100) NOT NULL,
    [IsDeleted] BIT           NOT NULL,
    CONSTRAINT [PK_GameType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

