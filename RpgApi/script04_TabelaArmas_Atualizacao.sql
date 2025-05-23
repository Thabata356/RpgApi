﻿BEGIN TRANSACTION;
ALTER TABLE [TB_PERSONAGENS] ADD [Derrotas] int NOT NULL DEFAULT 0;

ALTER TABLE [TB_PERSONAGENS] ADD [Disputas] int NOT NULL DEFAULT 0;

ALTER TABLE [TB_PERSONAGENS] ADD [Vitorias] int NOT NULL DEFAULT 0;

ALTER TABLE [TB_ARMAS] ADD [PersonagemId] int NOT NULL DEFAULT 0;

UPDATE [TB_ARMAS] SET [PersonagemId] = 1
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


UPDATE [TB_ARMAS] SET [PersonagemId] = 2
WHERE [Id] = 2;
SELECT @@ROWCOUNT;


UPDATE [TB_ARMAS] SET [PersonagemId] = 3
WHERE [Id] = 3;
SELECT @@ROWCOUNT;


UPDATE [TB_ARMAS] SET [PersonagemId] = 4
WHERE [Id] = 4;
SELECT @@ROWCOUNT;


UPDATE [TB_ARMAS] SET [PersonagemId] = 5
WHERE [Id] = 5;
SELECT @@ROWCOUNT;


UPDATE [TB_ARMAS] SET [PersonagemId] = 6
WHERE [Id] = 6;
SELECT @@ROWCOUNT;


UPDATE [TB_ARMAS] SET [PersonagemId] = 7
WHERE [Id] = 7;
SELECT @@ROWCOUNT;


UPDATE [TB_PERSONAGENS] SET [Derrotas] = 0, [Disputas] = 0, [Vitorias] = 0
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


UPDATE [TB_PERSONAGENS] SET [Derrotas] = 0, [Disputas] = 0, [Vitorias] = 0
WHERE [Id] = 2;
SELECT @@ROWCOUNT;


UPDATE [TB_PERSONAGENS] SET [Derrotas] = 0, [Disputas] = 0, [Vitorias] = 0
WHERE [Id] = 3;
SELECT @@ROWCOUNT;


UPDATE [TB_PERSONAGENS] SET [Derrotas] = 0, [Disputas] = 0, [Vitorias] = 0
WHERE [Id] = 4;
SELECT @@ROWCOUNT;


UPDATE [TB_PERSONAGENS] SET [Derrotas] = 0, [Disputas] = 0, [Vitorias] = 0
WHERE [Id] = 5;
SELECT @@ROWCOUNT;


UPDATE [TB_PERSONAGENS] SET [Derrotas] = 0, [Disputas] = 0, [Vitorias] = 0
WHERE [Id] = 6;
SELECT @@ROWCOUNT;


UPDATE [TB_PERSONAGENS] SET [Derrotas] = 0, [Disputas] = 0, [Vitorias] = 0
WHERE [Id] = 7;
SELECT @@ROWCOUNT;


UPDATE [TB_USUARIOS] SET [passwordSalt] = 0xF3EA56AA715F6A4376B2F80AA7E7130EAF3833A55D1B009354C57BB601FF823D269AF528D24F363AF2F219DF967B7BBCCE7FE3708446316C0BB00B13105E726C63ADED433038335EB6376EED6C822043CBF2DBF93F1E1E1CF4260B931F93EBA8BC7DB37D6C2A7BA5BBDFDD32F5FDB21C36F61D0148820944BB06A1E71488DD38, [PasswordHash] = 0xE09A69F794F471999AA9A1F185A457E6B91F7ECAAED6DC1211AEB11F212B8B8818B0A48DF205DB437C69FBC8319BB6FDD4572B3D4CE1FA18B173F2DF24FB44B6
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


CREATE UNIQUE INDEX [IX_TB_ARMAS_PersonagemId] ON [TB_ARMAS] ([PersonagemId]);

ALTER TABLE [TB_ARMAS] ADD CONSTRAINT [FK_TB_ARMAS_TB_PERSONAGENS_PersonagemId] FOREIGN KEY ([PersonagemId]) REFERENCES [TB_PERSONAGENS] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250413191741_MigracaoUmParaUm', N'9.0.2');

COMMIT;
GO

