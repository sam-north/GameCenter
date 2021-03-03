GO

SET NOCOUNT ON

SET IDENTITY_INSERT [Game] ON

MERGE INTO [Game] AS Target
USING (VALUES
  (1,'Mancala', 'Mancala', 0),
  (2,'ConnectFour', 'Connect Four', 0),
  (3,'Checkers', 'Checkers', 0),
  (4,'TicTacToe', 'Tic Tac Toe', 0)
) AS Source ([Id],[ReferenceName], [DisplayName], [IsDeleted])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED THEN
   UPDATE SET
		Target.[ReferenceName] = Source.[ReferenceName],
		Target.[DisplayName] = Source.[DisplayName],
		Target.[IsDeleted] = Source.[IsDeleted]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([Id],[ReferenceName], [DisplayName], [IsDeleted])
 VALUES(Source.[Id],Source.[ReferenceName],Source.[DisplayName], Source.[IsDeleted])
;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [Game]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[Game] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [Game] OFF
GO
SET NOCOUNT OFF
GO