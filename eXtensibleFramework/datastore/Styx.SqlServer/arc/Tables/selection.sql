CREATE TABLE [arc].[selection] (
    [Id]          UNIQUEIDENTIFIER   CONSTRAINT [DF_selection_Id] DEFAULT (newid()) NOT NULL,
    [SelectionId] INT                IDENTITY (101, 1) NOT NULL,
    [Display]     NVARCHAR (50)      NOT NULL,
    [Token]       NVARCHAR (50)      NOT NULL,
    [Sort]        INT                CONSTRAINT [DF_selection_Sort] DEFAULT ((5)) NOT NULL,
    [GroupId]     INT                CONSTRAINT [DF_selection_GroupId] DEFAULT ((0)) NOT NULL,
    [MasterId]    INT                CONSTRAINT [DF_selection_MasterId] DEFAULT ((0)) NOT NULL,
    [Icon]        NVARCHAR (50)      NULL,
    [Tds]         DATETIMEOFFSET (7) CONSTRAINT [DF_selection_Tds] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_selection] PRIMARY KEY CLUSTERED ([SelectionId] ASC)
);

