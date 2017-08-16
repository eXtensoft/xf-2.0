CREATE TABLE [arc].[serverapp] (
    [ServerAppId]      INT                IDENTITY (101, 1) NOT NULL,
    [ServerId]         INT                NOT NULL,
    [AppId]            INT                NOT NULL,
    [ZoneId]           INT                NOT NULL,
    [ScopeId]          INT                NOT NULL,
    [SecurityId]       INT                NOT NULL,
    [DomainId]         INT                CONSTRAINT [DF_serverapp_DomainId] DEFAULT ((0)) NOT NULL,
    [Folderpath]       NVARCHAR (200)     NULL,
    [BackupFolderpath] NVARCHAR (200)     NULL,
    [Tds]              DATETIMEOFFSET (7) CONSTRAINT [DF_serverapp_Tds] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_serverapp] PRIMARY KEY CLUSTERED ([ServerId] ASC, [AppId] ASC, [ZoneId] ASC, [ScopeId] ASC)
);

