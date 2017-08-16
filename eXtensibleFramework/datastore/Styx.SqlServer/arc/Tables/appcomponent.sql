CREATE TABLE [arc].[appcomponent] (
    [AppComponentId] INT                IDENTITY (1001, 1) NOT NULL,
    [AppId]          INT                NOT NULL,
    [ComponetId]     INT                NOT NULL,
    [ScopeId]        INT                CONSTRAINT [DF_appcomponent_ScopeId] DEFAULT ((0)) NOT NULL,
    [Tds]            DATETIMEOFFSET (7) CONSTRAINT [DF_appcomponent_Tds] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_appcomponent] PRIMARY KEY CLUSTERED ([AppId] ASC, [ComponetId] ASC, [ScopeId] ASC)
);

