BEGIN TRANSACTION;
DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TB_PERSONAGENS]') AND [c].[name] = N'Nome');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [TB_PERSONAGENS] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [TB_PERSONAGENS] ALTER COLUMN [Nome] Varchar(200) NOT NULL;

CREATE TABLE [TB_ARMAS] (
    [Id] int NOT NULL IDENTITY,
    [Nome] Varchar(200) NOT NULL,
    [Dano] int NOT NULL,
    CONSTRAINT [PK_TB_ARMAS] PRIMARY KEY ([Id])
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Dano', N'Nome') AND [object_id] = OBJECT_ID(N'[TB_ARMAS]'))
    SET IDENTITY_INSERT [TB_ARMAS] ON;
INSERT INTO [TB_ARMAS] ([Id], [Dano], [Nome])
VALUES (1, 35, 'Arco e Flecha'),
(2, 33, 'Espada'),
(3, 31, 'Machado'),
(4, 30, 'Punho'),
(5, 34, 'Chicote'),
(6, 33, 'Foice'),
(7, 32, 'Cajado');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Dano', N'Nome') AND [object_id] = OBJECT_ID(N'[TB_ARMAS]'))
    SET IDENTITY_INSERT [TB_ARMAS] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250331112702_MigracaoArma', N'9.0.2');

COMMIT;
GO

