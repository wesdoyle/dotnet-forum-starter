IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [Email] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [LockoutEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [PasswordHash] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [UserName] nvarchar(256) NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_UserId] ON [AspNetUserRoles] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'00000000000000_CreateIdentitySchema', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910021917_Add base forum models')
BEGIN
    DROP INDEX [UserNameIndex] ON [AspNetUsers];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910021917_Add base forum models')
BEGIN
    DROP INDEX [IX_AspNetUserRoles_UserId] ON [AspNetUserRoles];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910021917_Add base forum models')
BEGIN
    DROP INDEX [RoleNameIndex] ON [AspNetRoles];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910021917_Add base forum models')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [UserDescription] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910021917_Add base forum models')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910021917_Add base forum models')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910021917_Add base forum models')
BEGIN
    ALTER TABLE [AspNetUserTokens] ADD CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910021917_Add base forum models')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170910021917_Add base forum models', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910031359_Add dbsets')
BEGIN
    CREATE TABLE [Forums] (
        [Id] int NOT NULL IDENTITY,
        [Created] datetime2 NOT NULL,
        [Description] nvarchar(max) NULL,
        [Title] nvarchar(max) NULL,
        CONSTRAINT [PK_Forums] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910031359_Add dbsets')
BEGIN
    CREATE TABLE [Posts] (
        [Id] int NOT NULL IDENTITY,
        [Content] nvarchar(max) NULL,
        [Created] datetime2 NOT NULL,
        [ForumId] int NULL,
        [Title] nvarchar(max) NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_Posts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Posts_Forums_ForumId] FOREIGN KEY ([ForumId]) REFERENCES [Forums] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Posts_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910031359_Add dbsets')
BEGIN
    CREATE TABLE [PostReplies] (
        [Id] int NOT NULL IDENTITY,
        [Content] nvarchar(max) NULL,
        [Created] datetime2 NOT NULL,
        [PostId] int NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_PostReplies] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PostReplies_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PostReplies_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910031359_Add dbsets')
BEGIN
    CREATE INDEX [IX_PostReplies_PostId] ON [PostReplies] ([PostId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910031359_Add dbsets')
BEGIN
    CREATE INDEX [IX_PostReplies_UserId] ON [PostReplies] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910031359_Add dbsets')
BEGIN
    CREATE INDEX [IX_Posts_ForumId] ON [Posts] ([ForumId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910031359_Add dbsets')
BEGIN
    CREATE INDEX [IX_Posts_UserId] ON [Posts] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910031359_Add dbsets')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170910031359_Add dbsets', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910033145_Update post')
BEGIN
    ALTER TABLE [Posts] ADD [IsArchived] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170910033145_Update post')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170910033145_Update post', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911014945_Update user model')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [ProfileImageUrl] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911014945_Update user model')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Rating] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911014945_Update user model')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170911014945_Update user model', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911025023_ImageUrl on Forum')
BEGIN
    ALTER TABLE [Forums] ADD [ImageUrl] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911025023_ImageUrl on Forum')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170911025023_ImageUrl on Forum', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911150322_UserFeature matrix')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170911150322_UserFeature matrix', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911151631_UserFeature matrix correction')
BEGIN
    CREATE TABLE [FeatureTypes] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_FeatureTypes] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911151631_UserFeature matrix correction')
BEGIN
    CREATE TABLE [PrivateMessages] (
        [Id] int NOT NULL IDENTITY,
        [Created] datetime2 NOT NULL,
        [Description] nvarchar(max) NULL,
        [ReceiverId] nvarchar(450) NULL,
        [SenderId] nvarchar(450) NULL,
        [Subject] nvarchar(max) NULL,
        CONSTRAINT [PK_PrivateMessages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PrivateMessages_AspNetUsers_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PrivateMessages_AspNetUsers_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911151631_UserFeature matrix correction')
BEGIN
    CREATE TABLE [UserFeatures] (
        [Id] int NOT NULL IDENTITY,
        [FeatureId] int NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_UserFeatures] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserFeatures_FeatureTypes_FeatureId] FOREIGN KEY ([FeatureId]) REFERENCES [FeatureTypes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_UserFeatures_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911151631_UserFeature matrix correction')
BEGIN
    CREATE INDEX [IX_PrivateMessages_ReceiverId] ON [PrivateMessages] ([ReceiverId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911151631_UserFeature matrix correction')
BEGIN
    CREATE INDEX [IX_PrivateMessages_SenderId] ON [PrivateMessages] ([SenderId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911151631_UserFeature matrix correction')
BEGIN
    CREATE INDEX [IX_UserFeatures_FeatureId] ON [UserFeatures] ([FeatureId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911151631_UserFeature matrix correction')
BEGIN
    CREATE INDEX [IX_UserFeatures_UserId] ON [UserFeatures] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911151631_UserFeature matrix correction')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170911151631_UserFeature matrix correction', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911162137_Add forum virtual to post')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170911162137_Add forum virtual to post', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911170816_Add favorites')
BEGIN
    CREATE TABLE [Favorites] (
        [Id] int NOT NULL IDENTITY,
        [ForumId] int NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_Favorites] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Favorites_Forums_ForumId] FOREIGN KEY ([ForumId]) REFERENCES [Forums] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Favorites_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911170816_Add favorites')
BEGIN
    CREATE INDEX [IX_Favorites_ForumId] ON [Favorites] ([ForumId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911170816_Add favorites')
BEGIN
    CREATE INDEX [IX_Favorites_UserId] ON [Favorites] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170911170816_Add favorites')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170911170816_Add favorites', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170912004257_Drop feature matrix')
BEGIN
    DROP TABLE [Favorites];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170912004257_Drop feature matrix')
BEGIN
    DROP TABLE [UserFeatures];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170912004257_Drop feature matrix')
BEGIN
    DROP TABLE [FeatureTypes];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170912004257_Drop feature matrix')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170912004257_Drop feature matrix', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170913012303_Add IsAdmin bool to ApplicationUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [IsAdmin] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170913012303_Add IsAdmin bool to ApplicationUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170913012303_Add IsAdmin bool to ApplicationUser', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170913024245_Add DateTime to AppUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [MemberSince] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170913024245_Add DateTime to AppUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170913024245_Add DateTime to AppUser', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170914064154_User Active')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [IsActive] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170914064154_User Active')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170914064154_User Active', N'2.0.0-rtm-26452');
END;

GO

