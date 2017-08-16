CREATE TABLE [arc].[componentconstruct] (
    [ComponentConstructId] INT                IDENTITY (1001, 1) NOT NULL,
    [ComponentId]          INT                NOT NULL,
    [ConstructId]          INT                NOT NULL,
    [ScopeId]              INT                CONSTRAINT [DF_componentconstruct_ScopeId] DEFAULT ((0)) NOT NULL,
    [Tds]                  DATETIMEOFFSET (7) CONSTRAINT [DF_componentconstruct_Tds] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_componentconstruct] PRIMARY KEY CLUSTERED ([ComponentId] ASC, [ConstructId] ASC, [ScopeId] ASC)
);

