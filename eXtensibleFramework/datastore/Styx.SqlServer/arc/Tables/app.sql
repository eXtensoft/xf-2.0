CREATE TABLE [arc].[app] (
    [Id]          UNIQUEIDENTIFIER   CONSTRAINT [DF_app_Id] DEFAULT (newid()) NOT NULL,
    [AppId]       INT                IDENTITY (101, 1) NOT NULL,
    [AppTypeId]   INT                CONSTRAINT [DF_app_AppTypeId] DEFAULT ((0)) NOT NULL,
    [Name]        NVARCHAR (50)      NOT NULL,
    [Alias]       NVARCHAR (50)      NULL,
    [Description] NVARCHAR (200)     NULL,
    [Tags]        NVARCHAR (100)     NULL,
    [Tds]         DATETIMEOFFSET (7) CONSTRAINT [DF_app_Tds] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_app] PRIMARY KEY CLUSTERED ([AppId] ASC)
);

