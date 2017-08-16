CREATE TABLE [arc].[component] (
    [Id]              UNIQUEIDENTIFIER   NOT NULL,
    [ComponentId]     INT                IDENTITY (1001, 1) NOT NULL,
    [ComponentTypeId] INT                CONSTRAINT [DF_component_ComponentTypeId] DEFAULT ((0)) NOT NULL,
    [Name]            NVARCHAR (50)      NOT NULL,
    [Alias]           NVARCHAR (50)      NULL,
    [Description]     NVARCHAR (200)     NULL,
    [Tds]             DATETIMEOFFSET (7) CONSTRAINT [DF_component_Tds] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_component] PRIMARY KEY CLUSTERED ([ComponentId] ASC)
);

