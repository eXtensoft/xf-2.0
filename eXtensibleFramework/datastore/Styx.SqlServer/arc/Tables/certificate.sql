CREATE TABLE [arc].[certificate] (
    [CertificateId] INT                IDENTITY (101, 1) NOT NULL,
    [Name]          NVARCHAR (50)      NOT NULL,
    [Domain]        NVARCHAR (100)     NOT NULL,
    [BeginOn]       DATE               NOT NULL,
    [EndOn]         DATE               NOT NULL,
    [Tds]           DATETIMEOFFSET (7) CONSTRAINT [DF_certificate_Tds] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_certificate] PRIMARY KEY CLUSTERED ([CertificateId] ASC)
);

