CREATE TABLE [arc].[artifact] (
    [Id]                  UNIQUEIDENTIFIER   CONSTRAINT [DF_artifact_Id] DEFAULT (newid()) NOT NULL,
    [ArtifactId]          INT                IDENTITY (101, 1) NOT NULL,
    [ArtifactTypeId]      INT                CONSTRAINT [DF_artifact_ArtifactTypeId] DEFAULT ((0)) NOT NULL,
    [ArtifactScopeTypeId] INT                CONSTRAINT [DF_artifact_ArtifactScopeTypeId] DEFAULT ((0)) NOT NULL,
    [ArtifactScopeid]     INT                CONSTRAINT [DF_artifact_ArtifactScopeid] DEFAULT ((0)) NOT NULL,
    [Mime]                NVARCHAR (50)      NOT NULL,
    [ContentLength]       BIGINT             NOT NULL,
    [OriginalFilename]    NVARCHAR (100)     NOT NULL,
    [Location]            NVARCHAR (200)     NOT NULL,
    [Title]               NVARCHAR (50)      NOT NULL,
    [DocumentId]          INT                CONSTRAINT [DF_artifact_DocumentId] DEFAULT ((0)) NOT NULL,
    [Tds]                 DATETIMEOFFSET (7) CONSTRAINT [DF_artifact_Tds] DEFAULT (sysdatetimeoffset()) NULL,
    CONSTRAINT [PK_artifact] PRIMARY KEY CLUSTERED ([ArtifactId] ASC)
);

