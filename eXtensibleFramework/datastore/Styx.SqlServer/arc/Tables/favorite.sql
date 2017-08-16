CREATE TABLE [arc].[favorite] (
    [Id]         UNIQUEIDENTIFIER   CONSTRAINT [DF_favorite_Id] DEFAULT (newid()) NOT NULL,
    [FavoriteId] INT                IDENTITY (101, 1) NOT NULL,
    [Username]   NVARCHAR (25)      NOT NULL,
    [Model]      NVARCHAR (25)      NOT NULL,
    [ModelId]    INT                CONSTRAINT [DF_favorite_ModelId] DEFAULT ((0)) NOT NULL,
    [Tds]        DATETIMEOFFSET (7) CONSTRAINT [DF_favorite_Tds] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_favorite] PRIMARY KEY CLUSTERED ([FavoriteId] ASC)
);

