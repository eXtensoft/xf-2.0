CREATE TABLE [arc].[solution] (
    [Id]          UNIQUEIDENTIFIER   CONSTRAINT [DF_solution_Id] DEFAULT (newid()) NOT NULL,
    [SolutionId]  INT                IDENTITY (101, 1) NOT NULL,
    [Name]        NVARCHAR (50)      NOT NULL,
    [Description] NVARCHAR (200)     NOT NULL,
    [Tds]         DATETIMEOFFSET (7) CONSTRAINT [DF_solution_Tds] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_solution] PRIMARY KEY CLUSTERED ([SolutionId] ASC)
);

