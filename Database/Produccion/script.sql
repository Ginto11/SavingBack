IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Usuario] (
    [Id] int NOT NULL IDENTITY,
    [PrimerNombre] nvarchar(max) NOT NULL,
    [SegundoNombre] nvarchar(max) NOT NULL,
    [Cedula] bigint NOT NULL,
    [NombreUsuario] nvarchar(max) NOT NULL,
    [Correo] nvarchar(max) NOT NULL,
    [FechaNacimiento] datetime2 NOT NULL,
    [Contrasena] nvarchar(max) NOT NULL,
    [AceptaTerminos] bit NOT NULL,
    [Rol] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Usuario_Rol] CHECK (Rol IN ('Cliente', 'Admin'))
);
GO

CREATE TABLE [MetaAhorro] (
    [Id] int NOT NULL IDENTITY,
    [UsuarioId] int NOT NULL,
    [Nombre] nvarchar(max) NOT NULL,
    [MontoObjetivo] decimal(18,2) NOT NULL,
    [MontoActual] decimal(18,2) NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [Estado] nvarchar(max) NULL,
    CONSTRAINT [PK_MetaAhorro] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_MetaAhorro_Estado] CHECK (Estado IN ('Activa', 'Cumplida', 'Cancelada')),
    CONSTRAINT [FK_MetaAhorro_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Ahorro] (
    [Id] int NOT NULL IDENTITY,
    [UsuarioId] int NOT NULL,
    [Monto] decimal(18,2) NOT NULL,
    [Fecha] datetime2 NOT NULL,
    [Descripcion] nvarchar(max) NULL,
    [MetaAhorroId] int NULL,
    CONSTRAINT [PK_Ahorro] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Ahorro_MetaAhorro_MetaAhorroId] FOREIGN KEY ([MetaAhorroId]) REFERENCES [MetaAhorro] ([Id]),
    CONSTRAINT [FK_Ahorro_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AceptaTerminos', N'Cedula', N'Contrasena', N'Correo', N'FechaNacimiento', N'NombreUsuario', N'PrimerNombre', N'Rol', N'SegundoNombre') AND [object_id] = OBJECT_ID(N'[Usuario]'))
    SET IDENTITY_INSERT [Usuario] ON;
INSERT INTO [Usuario] ([Id], [AceptaTerminos], [Cedula], [Contrasena], [Correo], [FechaNacimiento], [NombreUsuario], [PrimerNombre], [Rol], [SegundoNombre])
VALUES (1, CAST(1 AS bit), CAST(1001944317 AS bigint), N'+osxdTMbvrdSIPgNenMgtQ==', N'Salinitosnelson@gmail.com', '2001-08-11T00:00:00.0000000', N'Root', N'Admin', N'Admin', N'');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AceptaTerminos', N'Cedula', N'Contrasena', N'Correo', N'FechaNacimiento', N'NombreUsuario', N'PrimerNombre', N'Rol', N'SegundoNombre') AND [object_id] = OBJECT_ID(N'[Usuario]'))
    SET IDENTITY_INSERT [Usuario] OFF;
GO

CREATE INDEX [IX_Ahorro_MetaAhorroId] ON [Ahorro] ([MetaAhorroId]);
GO

CREATE INDEX [IX_Ahorro_UsuarioId] ON [Ahorro] ([UsuarioId]);
GO

CREATE INDEX [IX_MetaAhorro_UsuarioId] ON [MetaAhorro] ([UsuarioId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251115051814_CreacionInicial', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Usuario].[SegundoNombre]', N'PrimerApellido', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251115203437_ActualizacionDeColumna', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuario]') AND [c].[name] = N'Rol');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Usuario] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Usuario] ALTER COLUMN [Rol] nvarchar(max) NULL;
GO

ALTER TABLE [Usuario] ADD [ManejaGastos] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

CREATE TABLE [CategoriaGasto] (
    [Id] bigint NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_CategoriaGasto] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre') AND [object_id] = OBJECT_ID(N'[CategoriaGasto]'))
    SET IDENTITY_INSERT [CategoriaGasto] ON;
INSERT INTO [CategoriaGasto] ([Id], [Nombre])
VALUES (CAST(1 AS bigint), N'Alimentación'),
(CAST(2 AS bigint), N'Transporte'),
(CAST(3 AS bigint), N'Salud'),
(CAST(4 AS bigint), N'Hogar'),
(CAST(5 AS bigint), N'Servicios'),
(CAST(6 AS bigint), N'Educación'),
(CAST(7 AS bigint), N'Entretenimiento'),
(CAST(8 AS bigint), N'Mascotas'),
(CAST(9 AS bigint), N'Ropa'),
(CAST(10 AS bigint), N'Deudas'),
(CAST(11 AS bigint), N'Inversiones'),
(CAST(12 AS bigint), N'Otros');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre') AND [object_id] = OBJECT_ID(N'[CategoriaGasto]'))
    SET IDENTITY_INSERT [CategoriaGasto] OFF;
GO

UPDATE [Usuario] SET [ManejaGastos] = CAST(0 AS bit)
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251214182710_AñadiendoManejoGastosEnUsuario', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Usuario] ADD [FotoPerfil] nvarchar(max) NOT NULL DEFAULT N'';
GO

UPDATE [Usuario] SET [FotoPerfil] = N'/Uploads/Fotos/default.png'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251214184549_AgregandoColumnaFotoPerfil', N'8.0.1');
GO

COMMIT;
GO

