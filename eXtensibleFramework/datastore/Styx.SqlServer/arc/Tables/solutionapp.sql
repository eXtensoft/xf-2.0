CREATE TABLE [arc].[solutionapp] (
    [SolutionAppId] INT                IDENTITY (1001, 1) NOT NULL,
    [SolutionId]    INT                NOT NULL,
    [AppId]         INT                NOT NULL,
    [Sort]          INT                CONSTRAINT [DF_solutionapp_Sort] DEFAULT ((5)) NOT NULL,
    [Tds]           DATETIMEOFFSET (7) CONSTRAINT [DF_solutionapp_Tds] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_solutionapp] PRIMARY KEY CLUSTERED ([SolutionId] ASC, [AppId] ASC)
);

