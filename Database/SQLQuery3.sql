IF DB_ID('New CyberSecureBotDB') IS NULL
BEGIN
    CREATE DATABASE [New CyberSecureBotDB];
END
GO

USE [New CyberSecureBotDB];
GO

IF OBJECT_ID('dbo.Table', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.[Table]
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserMessage NVARCHAR(MAX) NOT NULL,
        BotResponse NVARCHAR(MAX) NOT NULL,
        SentAt DATETIME NOT NULL
    );
END
GO