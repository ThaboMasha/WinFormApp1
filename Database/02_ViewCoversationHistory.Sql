USE [New CyberSecureBotDB];
GO

SELECT
    Id,
    UserMessage,
    BotResponse,
    SentAt
FROM dbo.[Table]
ORDER BY Id DESC;
GO