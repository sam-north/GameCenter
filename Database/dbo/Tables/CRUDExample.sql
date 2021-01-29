CREATE TABLE [dbo].[CRUDExample] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Text]      VARCHAR (200) NOT NULL,
    [DateCreated] DATETIMEOFFSET NOT NULL,
    [IsDeleted] BIT           NOT NULL,
    [DateDeleted] DATETIMEOFFSET NULL, 
    CONSTRAINT [PK_CRUDExample] PRIMARY KEY CLUSTERED ([Id] ASC)
);

