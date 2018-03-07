-->> NOTE: THIS SCRIPT MUST BE RUN IN SQLCMD MODE INSIDE SQL SERVER MANAGEMENT STUDIO. <<--
:on error exit

SET NOCOUNT OFF;
GO

PRINT CONVERT(varchar(1000), @@VERSION);
GO

PRINT '';
PRINT 'Started - ' + CONVERT(varchar, GETDATE(), 121);
GO

USE [master];
GO
-- ****************************************
-- Drop Database
-- ****************************************
PRINT '';
PRINT '*** Dropping Database';
GO

IF EXISTS (SELECT [name] FROM [master].[sys].[databases] WHERE [name] = N'AwesomeDb')
    DROP DATABASE [AwesomeDb];

-- If the database has any other open connections close the network connection.
IF @@ERROR = 3702
    RAISERROR('[AwesomeDb] database cannot be dropped because there are still other open connections', 127, 127) WITH NOWAIT, LOG;
GO

-- ****************************************
-- Create Database
-- ****************************************
PRINT '';
PRINT '*** Creating Database';
GO

CREATE DATABASE [AwesomeDb]
GO

PRINT '';
PRINT '*** Checking for AwesomeDb Database';

/* CHECK FOR DATABASE IF IT DOESN'T EXISTS, DO NOT RUN THE REST OF THE SCRIPT */
IF NOT EXISTS (SELECT TOP 1 1 FROM sys.databases WHERE name = N'AwesomeDb')
BEGIN
PRINT 'AwesomeDb Database does not exist.  Make sure that the script is being run in SQLCMD mode and that the variables have been correctly set.';
SET NOEXEC ON;
END
GO

ALTER DATABASE [AwesomeDb]
SET RECOVERY SIMPLE,
    ANSI_NULLS ON,
    ANSI_PADDING ON,
    ANSI_WARNINGS ON,
    ARITHABORT ON,
    CONCAT_NULL_YIELDS_NULL ON,
    QUOTED_IDENTIFIER ON,
    NUMERIC_ROUNDABORT OFF,
    PAGE_VERIFY CHECKSUM,
    ALLOW_SNAPSHOT_ISOLATION OFF;
GO

USE [AwesomeDb];
GO

-- ******************************************************
-- Create tables
-- ******************************************************

PRINT '';
PRINT '*** Creating Tables';
GO

CREATE TABLE [dbo].[User](
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(64) NOT NULL,
    [IsForQuery] BIT NOT NULL
) ON [PRIMARY];
GO

INSERT INTO [dbo].[User] VALUES
    ('E395B6FC-14FF-47DA-819E-526D6C9896D3', 'user-query-1', 1),
    ('DFBFD41D-E7C6-4709-9BF0-BD490D227B8F', 'user-query-2', 1);
GO

CREATE TABLE [dbo].[Preference](
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [Theme] NVARCHAR(64) NOT NULL,
    [IsForQuery] BIT NOT NULL,
)
GO

INSERT INTO [dbo].[Preference] VALUES
    ('E395B6FC-14FF-47DA-819E-526D6C9896D3', 'Dark', 1),
    ('DFBFD41D-E7C6-4709-9BF0-BD490D227B8F', 'Monokai', 1);
GO

ALTER TABLE [dbo].[User] WITH CHECK ADD
    CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED
    (
        [Id]
    );
GO

ALTER TABLE [dbo].[Preference] WITH CHECK ADD
    CONSTRAINT [PK_Preference_Id] PRIMARY KEY CLUSTERED
    (
        [UserId]
    );
GO

-- ****************************************
-- Shrink Database
-- ****************************************
PRINT '';
PRINT '*** Shrinking Database';
GO

DBCC SHRINKDATABASE ([AwesomeDb]);
GO

USE [master];
GO

PRINT 'Finished - ' + CONVERT(varchar, GETDATE(), 121);
GO

SET NOEXEC OFF