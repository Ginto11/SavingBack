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

CREATE TABLE [CategoriaGasto] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_CategoriaGasto] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Usuario] (
    [Id] int NOT NULL IDENTITY,
    [PrimerNombre] nvarchar(max) NOT NULL,
    [PrimerApellido] nvarchar(max) NOT NULL,
    [Cedula] bigint NOT NULL,
    [NombreUsuario] nvarchar(max) NOT NULL,
    [Correo] nvarchar(max) NOT NULL,
    [FechaNacimiento] datetime2 NOT NULL,
    [Contrasena] nvarchar(max) NOT NULL,
    [AceptaTerminos] bit NOT NULL,
    [ManejaGastos] bit NOT NULL,
    [Rol] nvarchar(max) NULL,
    [FotoPerfil] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Usuario_Rol] CHECK (Rol IN ('Cliente', 'Admin'))
);
GO

CREATE TABLE [Egreso] (
    [Id] int NOT NULL IDENTITY,
    [UsuarioId] int NOT NULL,
    [Tipo] nvarchar(max) NOT NULL,
    [FechaRegistro] datetime2 NOT NULL,
    [Monto] int NOT NULL,
    [CategoriaGastoId] int NOT NULL,
    CONSTRAINT [PK_Egreso] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Egreso_Tipo] CHECK ([Tipo] IN ('Efectivo', 'App', 'Nequi', 'Banco')),
    CONSTRAINT [FK_Egreso_CategoriaGasto_CategoriaGastoId] FOREIGN KEY ([CategoriaGastoId]) REFERENCES [CategoriaGasto] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Egreso_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Ingreso] (
    [Id] int NOT NULL IDENTITY,
    [UsuarioId] int NOT NULL,
    [Tipo] nvarchar(max) NOT NULL,
    [FechaRegistro] datetime2 NOT NULL,
    [Monto] int NOT NULL,
    CONSTRAINT [PK_Ingreso] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Ingreso_Tipo] CHECK ([Tipo] IN ('Efectivo', 'App', 'Nequi', 'Banco')),
    CONSTRAINT [FK_Ingreso_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]) ON DELETE NO ACTION
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
    [MetaAhorroId] int NOT NULL,
    CONSTRAINT [PK_Ahorro] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Ahorro_MetaAhorro_MetaAhorroId] FOREIGN KEY ([MetaAhorroId]) REFERENCES [MetaAhorro] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Ahorro_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre') AND [object_id] = OBJECT_ID(N'[CategoriaGasto]'))
    SET IDENTITY_INSERT [CategoriaGasto] ON;
INSERT INTO [CategoriaGasto] ([Id], [Nombre])
VALUES (1, N'Alimentación'),
(2, N'Transporte'),
(3, N'Salud'),
(4, N'Hogar'),
(5, N'Servicios'),
(6, N'Educación'),
(7, N'Entretenimiento'),
(8, N'Mascotas'),
(9, N'Ropa'),
(10, N'Deudas'),
(11, N'Inversiones'),
(12, N'Otros');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre') AND [object_id] = OBJECT_ID(N'[CategoriaGasto]'))
    SET IDENTITY_INSERT [CategoriaGasto] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AceptaTerminos', N'Cedula', N'Contrasena', N'Correo', N'FechaNacimiento', N'FotoPerfil', N'ManejaGastos', N'NombreUsuario', N'PrimerApellido', N'PrimerNombre', N'Rol') AND [object_id] = OBJECT_ID(N'[Usuario]'))
    SET IDENTITY_INSERT [Usuario] ON;
INSERT INTO [Usuario] ([Id], [AceptaTerminos], [Cedula], [Contrasena], [Correo], [FechaNacimiento], [FotoPerfil], [ManejaGastos], [NombreUsuario], [PrimerApellido], [PrimerNombre], [Rol])
VALUES (1, CAST(1 AS bit), CAST(1001944317 AS bigint), N'+osxdTMbvrdSIPgNenMgtQ==', N'Salinitosnelson@gmail.com', '2001-08-11T00:00:00.0000000', N'/Uploads/Fotos/default.png', CAST(0 AS bit), N'Root', N'', N'Admin', N'Admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AceptaTerminos', N'Cedula', N'Contrasena', N'Correo', N'FechaNacimiento', N'FotoPerfil', N'ManejaGastos', N'NombreUsuario', N'PrimerApellido', N'PrimerNombre', N'Rol') AND [object_id] = OBJECT_ID(N'[Usuario]'))
    SET IDENTITY_INSERT [Usuario] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoriaGastoId', N'FechaRegistro', N'Monto', N'Tipo', N'UsuarioId') AND [object_id] = OBJECT_ID(N'[Egreso]'))
    SET IDENTITY_INSERT [Egreso] ON;
INSERT INTO [Egreso] ([Id], [CategoriaGastoId], [FechaRegistro], [Monto], [Tipo], [UsuarioId])
VALUES (1, 1, '2026-04-01T08:50:12.9870164-05:00', 100000, N'Nequi', 1),
(2, 4, '2026-04-01T08:50:12.9870168-05:00', 8000, N'App', 1),
(3, 10, '2026-04-01T08:50:12.9870170-05:00', 12000, N'Efectivo', 1),
(4, 5, '2026-04-01T08:50:12.9870172-05:00', 60000, N'Efectivo', 1),
(5, 2, '2026-04-01T08:50:12.9870175-05:00', 180000, N'Banco', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoriaGastoId', N'FechaRegistro', N'Monto', N'Tipo', N'UsuarioId') AND [object_id] = OBJECT_ID(N'[Egreso]'))
    SET IDENTITY_INSERT [Egreso] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'FechaRegistro', N'Monto', N'Tipo', N'UsuarioId') AND [object_id] = OBJECT_ID(N'[Ingreso]'))
    SET IDENTITY_INSERT [Ingreso] ON;
INSERT INTO [Ingreso] ([Id], [FechaRegistro], [Monto], [Tipo], [UsuarioId])
VALUES (1, '2026-04-01T08:50:12.9869791-05:00', 10000, N'Efectivo', 1),
(2, '2026-04-01T08:50:12.9869795-05:00', 80000, N'Nequi', 1),
(3, '2026-04-01T08:50:12.9869797-05:00', 120000, N'App', 1),
(4, '2026-04-01T08:50:12.9869799-05:00', 66000, N'Efectivo', 1),
(5, '2026-04-01T08:50:12.9869801-05:00', 150000, N'Banco', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'FechaRegistro', N'Monto', N'Tipo', N'UsuarioId') AND [object_id] = OBJECT_ID(N'[Ingreso]'))
    SET IDENTITY_INSERT [Ingreso] OFF;
GO

CREATE INDEX [IX_Ahorro_MetaAhorroId] ON [Ahorro] ([MetaAhorroId]);
GO

CREATE INDEX [IX_Ahorro_UsuarioId] ON [Ahorro] ([UsuarioId]);
GO

CREATE INDEX [IX_Egreso_CategoriaGastoId] ON [Egreso] ([CategoriaGastoId]);
GO

CREATE INDEX [IX_Egreso_UsuarioId] ON [Egreso] ([UsuarioId]);
GO

CREATE INDEX [IX_Ingreso_UsuarioId] ON [Ingreso] ([UsuarioId]);
GO

CREATE INDEX [IX_MetaAhorro_UsuarioId] ON [MetaAhorro] ([UsuarioId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260401135013_ImplementacionInicial', N'8.0.1');
GO

COMMIT;
GO

