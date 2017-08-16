CREATE TABLE [arc].[solutionzone] (
    [SolutionZoneId] INT IDENTITY (101, 1) NOT NULL,
    [SolutionId]     INT NOT NULL,
    [ZoneId]         INT NOT NULL,
    CONSTRAINT [PK_solutionzone] PRIMARY KEY CLUSTERED ([SolutionId] ASC, [ZoneId] ASC)
);

