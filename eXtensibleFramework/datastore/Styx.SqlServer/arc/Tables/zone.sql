CREATE TABLE [arc].[zone] (
    [Id]      UNIQUEIDENTIFIER   CONSTRAINT [DF_zone_Id] DEFAULT (newid()) NOT NULL,
    [ZoneId]  INT                IDENTITY (101, 1) NOT NULL,
    [ScopeId] INT                CONSTRAINT [DF_zone_ScopeId] DEFAULT ((0)) NOT NULL,
    [Name]    NVARCHAR (50)      NOT NULL,
    [Alias]   NVARCHAR (50)      NULL,
    [Token]   NVARCHAR (50)      NOT NULL,
    [Tds]     DATETIMEOFFSET (7) CONSTRAINT [DF_zone_Tds] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_zone] PRIMARY KEY CLUSTERED ([ZoneId] ASC)
);

