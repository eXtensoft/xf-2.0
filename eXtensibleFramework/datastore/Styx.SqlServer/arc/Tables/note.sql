CREATE TABLE [arc].[note] (
    [Id]           UNIQUEIDENTIFIER   CONSTRAINT [DF_note_Id] DEFAULT (newid()) NOT NULL,
    [NoteId]       INT                IDENTITY (1001, 1) NOT NULL,
    [Subject]      NVARCHAR (50)      NOT NULL,
    [Body]         NVARCHAR (MAX)     NOT NULL,
    [UserIdentity] NVARCHAR (25)      NOT NULL,
    [Tds]          DATETIMEOFFSET (7) CONSTRAINT [DF_note_Tds] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_note] PRIMARY KEY CLUSTERED ([NoteId] ASC)
);

