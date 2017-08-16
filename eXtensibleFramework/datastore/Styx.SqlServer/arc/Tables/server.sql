CREATE TABLE [arc].[server] (
    [Id]                UNIQUEIDENTIFIER   CONSTRAINT [DF_server_Id] DEFAULT (newid()) NOT NULL,
    [ServerId]          INT                IDENTITY (1, 101) NOT NULL,
    [OperatingSystemId] INT                CONSTRAINT [DF_server_OperatingSystemId] DEFAULT ((0)) NOT NULL,
    [HostPlatformId]    INT                CONSTRAINT [DF_server_HostPlatformId] DEFAULT ((0)) NOT NULL,
    [SecurityId]        INT                CONSTRAINT [DF_server_SecurityId] DEFAULT ((0)) NOT NULL,
    [Name]              NVARCHAR (50)      NOT NULL,
    [Alias]             NVARCHAR (50)      NULL,
    [Description]       NVARCHAR (200)     NULL,
    [ExternalIP]        NVARCHAR (16)      NOT NULL,
    [InternalIP]        NVARCHAR (16)      NOT NULL,
    [Tags]              NVARCHAR (100)     NULL,
    [Tds]               DATETIMEOFFSET (7) CONSTRAINT [DF_server_Tds] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_server] PRIMARY KEY CLUSTERED ([ServerId] ASC)
);

