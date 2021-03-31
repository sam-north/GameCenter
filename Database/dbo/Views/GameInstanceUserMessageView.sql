CREATE VIEW [dbo].[GameInstanceUserMessageView]
	AS 
SELECT 
	GIUM.[Id],
	GIUM.[GameInstanceId],
	GIUM.[UserId],
	U.[Email] AS [UserEmail],
	GIUM.[Text],
	GIUM.[DateCreated],	
	GIUM.[DateDeleted]
FROM [dbo].[GameInstanceUserMessage] GIUM
INNER JOIN [dbo].[User] U ON GIUM.[UserId] = U.[Id]