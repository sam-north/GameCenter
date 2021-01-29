CREATE TABLE [dbo].[User] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Username]  VARCHAR (50)  NOT NULL,
    [Password]  VARCHAR (500) NOT NULL,
    [IsDeleted] BIT           NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

