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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241119193405_abstractMig'
)
BEGIN
    CREATE TABLE [Certifications] (
        [Id] int NOT NULL IDENTITY,
        [CertificationName] nvarchar(max) NULL,
        [DateIssued] datetime2 NOT NULL,
        [Description] nvarchar(max) NULL,
        [ExpirationDate] datetime2 NULL,
        [IssuingOrganization] nvarchar(max) NULL,
        [ProfileId] int NOT NULL,
        CONSTRAINT [PK_Certifications] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241119193405_abstractMig'
)
BEGIN
    CREATE TABLE [Educations] (
        [Id] int NOT NULL IDENTITY,
        [Institution] nvarchar(200) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [Discriminator] nvarchar(21) NOT NULL,
        [Degree] nvarchar(max) NULL,
        [ProjectTitle] nvarchar(max) NULL,
        [Supervisor] nvarchar(max) NULL,
        [Summary] nvarchar(max) NULL,
        CONSTRAINT [PK_Educations] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241119193405_abstractMig'
)
BEGIN
    CREATE TABLE [Languages] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Level] nvarchar(max) NOT NULL,
        [Checksum] AS CONVERT(VARBINARY(1024),CHECKSUM([Name],[Level])),
        CONSTRAINT [PK_Languages] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241119193405_abstractMig'
)
BEGIN
    CREATE TABLE [Profiles] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(50) NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        [Summary] nvarchar(max) NULL,
        [IsPublic] bit NOT NULL DEFAULT CAST(1 AS bit),
        [Visibility] nvarchar(max) NULL,
        [DetailLevel] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [Discriminator] nvarchar(21) NOT NULL,
        [PersonalGoals] nvarchar(max) NULL,
        [DesiredJobRole] nvarchar(200) NULL,
        [Major] nvarchar(max) NULL,
        [CareerGoals] nvarchar(max) NULL,
        CONSTRAINT [PK_Profiles] PRIMARY KEY ([Id]),
        CONSTRAINT [AK_Profiles_LastName] UNIQUE ([LastName])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241119193405_abstractMig'
)
BEGIN
    CREATE TABLE [DetailInfos] (
        [Id] int NOT NULL IDENTITY,
        [Emails] nvarchar(max) NOT NULL,
        [Phone] nvarchar(20) NOT NULL,
        [ProfileId] int NOT NULL,
        CONSTRAINT [PK_DetailInfos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DetailInfos_Profiles_ProfileId] FOREIGN KEY ([ProfileId]) REFERENCES [Profiles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241119193405_abstractMig'
)
BEGIN
    CREATE TABLE [JobExperiences] (
        [Id] int NOT NULL IDENTITY,
        [Company] nvarchar(max) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [Description] nvarchar(max) NULL,
        [Discriminator] nvarchar(21) NOT NULL,
        [ProfileId] int NULL,
        [Role] nvarchar(max) NULL,
        [Position] nvarchar(max) NULL,
        CONSTRAINT [PK_JobExperiences] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_JobExperiences_Profiles_ProfileId] FOREIGN KEY ([ProfileId]) REFERENCES [Profiles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241119193405_abstractMig'
)
BEGIN
    CREATE TABLE [ProfileEducations] (
        [EducationId] int NOT NULL,
        [ProfileId] int NOT NULL,
        CONSTRAINT [PK_ProfileEducations] PRIMARY KEY ([EducationId], [ProfileId]),
        CONSTRAINT [FK_ProfileEducations_Educations_EducationId] FOREIGN KEY ([EducationId]) REFERENCES [Educations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProfileEducations_Profiles_ProfileId] FOREIGN KEY ([ProfileId]) REFERENCES [Profiles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241119193405_abstractMig'
)
BEGIN
    CREATE TABLE [ProfileLanguages] (
        [LanguageId] int NOT NULL,
        [ProfileId] int NOT NULL,
        CONSTRAINT [PK_ProfileLanguages] PRIMARY KEY ([LanguageId], [ProfileId]),
        CONSTRAINT [FK_ProfileLanguages_Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [Languages] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProfileLanguages_Profiles_ProfileId] FOREIGN KEY ([ProfileId]) REFERENCES [Profiles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241119193405_abstractMig'
)
BEGIN
    CREATE UNIQUE INDEX [IX_DetailInfos_ProfileId] ON [DetailInfos] ([ProfileId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241119193405_abstractMig'
)
BEGIN
    CREATE INDEX [IX_JobExperiences_ProfileId] ON [JobExperiences] ([ProfileId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241119193405_abstractMig'
)
BEGIN
    CREATE INDEX [IX_ProfileEducations_ProfileId] ON [ProfileEducations] ([ProfileId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241119193405_abstractMig'
)
BEGIN
    CREATE INDEX [IX_ProfileLanguages_ProfileId] ON [ProfileLanguages] ([ProfileId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241119193405_abstractMig'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241119193405_abstractMig', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241119194416_enumconveriosn'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Profiles]') AND [c].[name] = N'DetailLevel');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Profiles] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Profiles] ALTER COLUMN [DetailLevel] nvarchar(18) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241119194416_enumconveriosn'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241119194416_enumconveriosn', N'8.0.10');
END;
GO

COMMIT;
GO