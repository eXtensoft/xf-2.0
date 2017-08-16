CREATE TABLE [arc].[construct] (
    [Id]              UNIQUEIDENTIFIER   CONSTRAINT [DF_construct_Id] DEFAULT (newid()) NOT NULL,
    [ConstructId]     INT                IDENTITY (1001, 1) NOT NULL,
    [ConstructTypeId] INT                CONSTRAINT [DF_construct_ConstructTypeId] DEFAULT ((0)) NOT NULL,
    [ScopeId]         INT                CONSTRAINT [DF_construct_ScopeId] DEFAULT ((0)) NOT NULL,
    [Name]            NVARCHAR (50)      NOT NULL,
    [Alias]           NVARCHAR (50)      NULL,
    [Tds]             DATETIMEOFFSET (7) CONSTRAINT [DF_construct_Tds] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_construct] PRIMARY KEY CLUSTERED ([ConstructId] ASC)
);

