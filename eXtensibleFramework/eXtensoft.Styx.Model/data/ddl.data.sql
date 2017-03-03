USE [DevOps]
GO
/****** Object:  Table [dbo].[App]    Script Date: 7/25/2016 12:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[App](
	[AppId] [int] IDENTITY(1,1) NOT NULL,
	[AppTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Alias] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Tags] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[AppId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AppComponent]    Script Date: 7/25/2016 12:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppComponent](
	[AppComponentId] [int] IDENTITY(1,1) NOT NULL,
	[AppId] [int] NOT NULL,
	[ComponentId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AppComponentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Artifact]    Script Date: 7/25/2016 12:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artifact](
	[ArtifactId] [int] IDENTITY(0,1) NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ArtifactTypeId] [int] NOT NULL CONSTRAINT [DF_Artifact_ArtifactTypeId]  DEFAULT ((0)),
	[Mime] [nvarchar](100) NOT NULL,
	[ContentLength] [bigint] NOT NULL CONSTRAINT [DF_Artifact_ContentLength]  DEFAULT ((0)),
	[OriginalFilename] [nvarchar](200) NOT NULL,
	[Location] [nvarchar](300) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Tds] [datetimeoffset](7) NOT NULL CONSTRAINT [DF_Artifact_Tds]  DEFAULT (sysdatetimeoffset()),
 CONSTRAINT [PK_Artifact] PRIMARY KEY CLUSTERED 
(
	[ArtifactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Component]    Script Date: 7/25/2016 12:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Component](
	[ComponentId] [int] IDENTITY(1,1) NOT NULL,
	[ComponentTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Alias] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](300) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ComponentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ComponentConstruct]    Script Date: 7/25/2016 12:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComponentConstruct](
	[ComponentConstructId] [int] IDENTITY(1,1) NOT NULL,
	[ComponentId] [int] NOT NULL,
	[ConstructId] [int] NOT NULL,
 CONSTRAINT [PK_ComponentConstruct] PRIMARY KEY CLUSTERED 
(
	[ComponentId] ASC,
	[ConstructId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Construct]    Script Date: 7/25/2016 12:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Construct](
	[ConstructId] [int] IDENTITY(1,1) NOT NULL,
	[ConstructTypeId] [int] NOT NULL,
	[ScopeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Alias] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ConstructId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Documentation]    Script Date: 7/25/2016 12:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documentation](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[ArtifactId] [int] NOT NULL,
	[ArtifactScopeTypeId] [int] NOT NULL,
	[ArtifactScopeId] [int] NOT NULL,
 CONSTRAINT [PK_Documentation] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Favorite]    Script Date: 7/25/2016 12:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorite](
	[FavoriteId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Model] [nvarchar](25) NOT NULL,
	[ModelId] [int] NOT NULL CONSTRAINT [DF_Favorite_ModelId]  DEFAULT ((0)),
	[Groupname] [nvarchar](25) NOT NULL CONSTRAINT [DF_Favorite_Groupname]  DEFAULT (N'none'),
	[Color] [nvarchar](12) NOT NULL CONSTRAINT [DF_Favorite_Color]  DEFAULT (N'none'),
	[Tds] [datetime] NOT NULL CONSTRAINT [DF_Favorite_Tds]  DEFAULT (getdate()),
 CONSTRAINT [PK_Favorite] PRIMARY KEY CLUSTERED 
(
	[FavoriteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Selection]    Script Date: 7/25/2016 12:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Selection](
	[SelectionId] [int] IDENTITY(0,1) NOT NULL,
	[Display] [nvarchar](50) NOT NULL,
	[Token] [nvarchar](50) NOT NULL,
	[Sort] [int] NOT NULL CONSTRAINT [DF__Selection__Sort__182C9B23]  DEFAULT ((0)),
	[GroupId] [int] NULL CONSTRAINT [DF__Selection__Group__1920BF5C]  DEFAULT ((0)),
	[MasterId] [int] NULL CONSTRAINT [DF__Selection__Maste__1A14E395]  DEFAULT ((0)),
	[Icon] [nvarchar](50) NULL,
 CONSTRAINT [PK__Selectio__7F17914F6521B50E] PRIMARY KEY CLUSTERED 
(
	[SelectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Server]    Script Date: 7/25/2016 12:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Server](
	[ServerId] [int] IDENTITY(1,1) NOT NULL,
	[OperatingSystemId] [int] NOT NULL,
	[HostPlatformId] [int] NOT NULL,
	[SecurityId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Alias] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[ExternalIP] [nvarchar](20) NULL,
	[InternalIP] [nvarchar](20) NULL,
	[Tags] [nvarchar](100) NULL,
 CONSTRAINT [PK__Server__C56AC8E66D3924D6] PRIMARY KEY CLUSTERED 
(
	[ServerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServerApp]    Script Date: 7/25/2016 12:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServerApp](
	[ServerAppId] [int] IDENTITY(1,1) NOT NULL,
	[ServerId] [int] NOT NULL,
	[AppId] [int] NOT NULL,
	[ZoneId] [int] NOT NULL,
	[ScopeId] [int] NOT NULL,
	[DomainId] [int] NULL,
	[Folderpath] [nvarchar](300) NULL,
	[BackupFolderpath] [nvarchar](300) NULL,
 CONSTRAINT [PK_ServerApp] PRIMARY KEY CLUSTERED 
(
	[ScopeId] ASC,
	[ZoneId] ASC,
	[AppId] ASC,
	[ServerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Solution]    Script Date: 7/25/2016 12:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Solution](
	[SolutionId] [int] IDENTITY(1,1) NOT NULL,
	[ScopeId] [int] NOT NULL CONSTRAINT [DF__Solution__ScopeI__20C1E124]  DEFAULT ((0)),
	[Name] [nvarchar](50) NOT NULL,
	[Alias] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK__Solution__6B633AD0FE0E775C] PRIMARY KEY CLUSTERED 
(
	[SolutionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SolutionApp]    Script Date: 7/25/2016 12:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionApp](
	[SolutionAppId] [int] IDENTITY(1,1) NOT NULL,
	[SolutionId] [int] NOT NULL,
	[AppId] [int] NOT NULL,
	[Sort] [int] NOT NULL,
 CONSTRAINT [PK_SolutionApp_1] PRIMARY KEY CLUSTERED 
(
	[SolutionId] ASC,
	[AppId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SolutionZone]    Script Date: 7/25/2016 12:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionZone](
	[SolutionZoneId] [int] IDENTITY(1,1) NOT NULL,
	[SolutionId] [int] NOT NULL,
	[ZoneId] [int] NOT NULL,
	[DomainId] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](300) NULL,
 CONSTRAINT [PK_SolutionZone_1] PRIMARY KEY CLUSTERED 
(
	[SolutionId] ASC,
	[ZoneId] ASC,
	[DomainId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Zone]    Script Date: 7/25/2016 12:56:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zone](
	[ZoneId] [int] IDENTITY(0,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Alias] [nvarchar](50) NOT NULL,
	[Token] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Zone__601667B55FC20E16] PRIMARY KEY CLUSTERED 
(
	[ZoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[App] ON 

GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (1, 27, N'OneClickdigital API', N'API', N'The API', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (2, 26, N'OneClickdigital SPA', N'SPA', N'A Single Page Application (Web) for Patron use', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (3, 38, N'OneClick Db', N'OneClick Db', N'Legacy monolithic datastore', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (4, 38, N'DPCore Db', N'DPCore', N'Data Platform Core datastore, housing transactional data for the digital platform', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (5, 38, N'DPMetadata Db', N'DPMetadata', N'Datastore containing title metadata', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (6, 27, N'BigData API', N'Big Data', N'Logging', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (7, 29, N'METL North America', N'METL NA', N'Metadata ETL for North America', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (8, 29, N'METL United Kingdom', N'METL UK', N'Metadata ETL for UK', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (9, 25, N'OnePing', N'OnePing', N'OnePing', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (10, 25, N'Publisher Portal', N'Publisher Portal', N'Publisher Administration site', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (11, 29, N'ACS Packager', N'ACS Packager', N'ACS Packager', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (12, 29, N'eBook Filemover', N'eBook Filemover', N'eBook Filemover', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (13, 29, N'Email Sender', N'Email Sender', N'Email Sender', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (14, 29, N'OneClick MessageQueue', N'OneClick MessageQueue', N'OneClick MessageQueue', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (15, 38, N'Elmah Db', N'Elmah', N'Logging', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (16, 38, N'Sitecore Db CMW', N'Sitecore CMW', N'Sitecore Core Master Web', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (17, 38, N'SOA Logging Db', N'SOA Logging', N'SOA Logging', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (18, 38, N'Trilogy Db', N'Trilogy', N'Trilogy', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (19, 38, N'OrderReview Db', N'OrderReview', N'OrderReview', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (20, 38, N'WebStaging Db', N'WebStaging', N'WebStaging', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (21, 24, N'OneClick Admin', N'OneClick Admin', N'Admin site for legacy OneClick platform', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (22, 38, N'ACS Db', N'ACS Db', N'ACS Db', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (23, 38, N'BuyItNow Db', N'BuyItNow Db', N'BuyItNow Db', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (24, 38, N'ReportServer Db', N'ReportServer', N'ReportServer', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (25, 25, N'Bluefire', N'Bluefire', N'Bluefire', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (26, 29, N'Sync Ownership Full', N'Sync Ownership Full', N'Sync Ownership Full', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (27, 29, N'Sync Ownership Delta', N'Sync Ownership Delta', N'Sync Ownership Delta', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (28, 38, N'WsWfhOrderBroker Db', N'WsWfhOrderBroker Db', N'WsWfhOrderBroker Db', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (29, 27, N'WebMarket API', N'WebMarket API', N'API for the Kentico CMS site', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (30, 41, N'Webmarket ElasticSearch', N'ElasticSearch', N'ElasticSearch', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (31, 24, N'WebMarket Website', N'Kentico Site', N'Kentico Site', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (32, 38, N'Kentico Db', N'Kentico Db', N'Kentico CMS datastore', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (33, 29, N'OneMart METL NA', N'OneMart METL NA', N'OneMart METL NA', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (34, 28, N'SOA Logging', N'Logging', N'Logging', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (35, 28, N'Orders Pickup', N'Order Pickup Service', N'Picks up Orders from WebMarket', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (36, 28, N'Orders Review', N'Order Review Web Service', N'Order Review Web Service', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (37, 29, N'Order Broker', N'Order Broker', N'Order Broker AB Job', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (38, 82, N'Trilogy extract to Rackspace', N'Trilogy minime', N'Trilogy extract to Rackspace', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (39, 83, N'Web Product Load ET', N'Trilogy tables to WebStaging', N'Products, lookups, etc to WebStaging ETL', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (40, 29, N'METL OneMart', N'Metadata ETL OneMart', N'Metadata ETL for OneMart', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (41, 83, N'Trilogy Purchase Detail Update', N'Purchase Details', N'Purchase Details', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (42, 83, N'Customer Subscriptions', N'Customer Subscription Group DAta', N'Customer Subscription Group Data', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (43, 39, N'ProfileData Db', N'profiledata_db', N'Profiling data for all the search calls', NULL)
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (44, 39, N'Metadata', N'Metadata', N'Title Metadata', N'metadata')
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (45, 41, N'SOLR4', N'SOLR4', N'SOLR4 Search', N'search')
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (46, 28, N'Fulfillments', N'Fulfillments Service', N'Product Fulfillments to the Digital Platform', N'dp, fulfillment')
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (47, 85, N'Global', N'global', N'global', N'global')
GO
INSERT [dbo].[App] ([AppId], [AppTypeId], [Name], [Alias], [Description], [Tags]) VALUES (48, 39, N'Big Data Log', N'Big Data', N'Big Data Logging', NULL)
GO
SET IDENTITY_INSERT [dbo].[App] OFF
GO
SET IDENTITY_INSERT [dbo].[Artifact] ON 

GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (0, N'9c670a70-1e55-4242-88cd-16d066c08829', 63, N'image/gif', 1236676, N'laughing-man.gif', N'E:\Webservices\Ops\app_files\file-uploads\image-gif\9c670a70-1e55-4242-88cd-16d066c08829.gif', N'meeting pic', CAST(N'2016-04-29T10:50:03.1643936-04:00' AS DateTimeOffset))
GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (1, N'daa1a937-c4e5-4787-9d58-ed537df1b400', 54, N'image/png', 108584, N'target.PNG', N'E:\Webservices\Ops\app_files\file-uploads\image-png\daa1a937-c4e5-4787-9d58-ed537df1b400.PNG', N'target Auth', CAST(N'2016-05-04T07:36:09.2375936-04:00' AS DateTimeOffset))
GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (2, N'58606a1b-7800-43a9-b695-774d53fff45d', 60, N'text/plain', 368206, N'OrderSearch.txt', N'E:\Webservices\Ops\app_files\file-uploads\text-plain\58606a1b-7800-43a9-b695-774d53fff45d.txt', N'my kj', CAST(N'2016-06-07T14:20:51.7612522-04:00' AS DateTimeOffset))
GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (3, N'2b058fc5-a682-45c6-9009-69a1bb9ff072', 84, N'image/png', 132040, N'holding.state.PNG', N'E:\Webservices\Ops\app_files\file-uploads\image-png\2b058fc5-a682-45c6-9009-69a1bb9ff072.PNG', N'Holding Availability', CAST(N'2016-06-10T08:13:08.9089368-04:00' AS DateTimeOffset))
GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (4, N'a482a051-85cc-4119-a2e6-5f50e33776f8', 65, N'image/png', 88712, N'dpcore.ebook.eaudio.PNG', N'E:\Webservices\Ops\app_files\file-uploads\image-png\a482a051-85cc-4119-a2e6-5f50e33776f8.PNG', N'eBook eAudio', CAST(N'2016-06-10T08:37:41.8959306-04:00' AS DateTimeOffset))
GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (5, N'09eb7ff6-ca41-45c9-b37f-ccf5997ba91d', 54, N'image/png', 56945, N'api.tiers.PNG', N'E:\Webservices\Ops\app_files\file-uploads\image-png\09eb7ff6-ca41-45c9-b37f-ccf5997ba91d.PNG', N'API Tiers', CAST(N'2016-06-10T08:42:43.5706644-04:00' AS DateTimeOffset))
GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (6, N'e5b9f0e1-b240-4231-b7de-a3c49d2e8d63', 54, N'image/png', 46133, N'fulfillment.tier.PNG', N'E:\Webservices\Ops\app_files\file-uploads\image-png\e5b9f0e1-b240-4231-b7de-a3c49d2e8d63.PNG', N'Fulfillment Tiers', CAST(N'2016-06-10T08:44:39.8013870-04:00' AS DateTimeOffset))
GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (7, N'e6b5ac1c-5810-41f6-95ff-c9310bd2f47c', 51, N'image/png', 146387, N'dp.dataflow.PNG', N'E:\Webservices\Ops\app_files\file-uploads\image-png\e6b5ac1c-5810-41f6-95ff-c9310bd2f47c.PNG', N'Digital Platform DFD', CAST(N'2016-06-10T08:52:44.0752913-04:00' AS DateTimeOffset))
GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (8, N'1766ccf7-0e96-4f18-bdef-d333ef66134f', 51, N'image/png', 116304, N'mark.generation.PNG', N'E:\Webservices\Ops\app_files\file-uploads\image-png\1766ccf7-0e96-4f18-bdef-d333ef66134f.PNG', N'Marc Record Generation', CAST(N'2016-06-10T08:57:41.4911978-04:00' AS DateTimeOffset))
GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (9, N'f1b44b50-c92d-4374-bb12-98464efb3661', 54, N'image/png', 56182, N'api.search.PNG', N'E:\Webservices\Ops\app_files\file-uploads\image-png\f1b44b50-c92d-4374-bb12-98464efb3661.PNG', N'Search Layers', CAST(N'2016-06-10T09:00:04.2321128-04:00' AS DateTimeOffset))
GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (10, N'63793189-b686-4f8e-935f-781e0bc25e8f', 54, N'image/png', 82169, N'api.search.components.PNG', N'E:\Webservices\Ops\app_files\file-uploads\image-png\63793189-b686-4f8e-935f-781e0bc25e8f.PNG', N'Search components', CAST(N'2016-06-10T09:01:58.7000112-04:00' AS DateTimeOffset))
GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (11, N'1d559393-bc75-4dca-b0e5-b3102c2e8c3d', 54, N'image/png', 115711, N'dp.tiers.PNG', N'E:\Webservices\Ops\app_files\file-uploads\image-png\1d559393-bc75-4dca-b0e5-b3102c2e8c3d.PNG', N'DP Tiers', CAST(N'2016-06-10T09:04:07.9156395-04:00' AS DateTimeOffset))
GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (12, N'2116de1f-4295-479d-b015-36f972c72782', 54, N'image/png', 59932, N'dp.logical.PNG', N'E:\Webservices\Ops\app_files\file-uploads\image-png\2116de1f-4295-479d-b015-36f972c72782.PNG', N'Component Layers', CAST(N'2016-06-10T09:07:04.0407685-04:00' AS DateTimeOffset))
GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (13, N'2a614507-d124-4d70-b6c3-864a4dbdd240', 56, N'image/png', 116085, N'auth.current.PNG', N'E:\Webservices\Ops\app_files\file-uploads\image-png\2a614507-d124-4d70-b6c3-864a4dbdd240.PNG', N'Current Auth', CAST(N'2016-06-10T09:11:08.7751373-04:00' AS DateTimeOffset))
GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (14, N'dc87fdcb-6d0f-402b-b6a8-7fb7ed75e61a', 56, N'image/png', 112804, N'auth.target.PNG', N'E:\Webservices\Ops\app_files\file-uploads\image-png\dc87fdcb-6d0f-402b-b6a8-7fb7ed75e61a.PNG', N'Target Auth', CAST(N'2016-06-10T09:11:25.9508474-04:00' AS DateTimeOffset))
GO
INSERT [dbo].[Artifact] ([ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title], [Tds]) VALUES (15, N'2938cdd8-2c8e-48d1-a3da-d38c7ccc4f18', 86, N'application/vnd.openxmlformats-officedocument.wordprocessingml.document', 286504, N'Merge SQL Scripts Deployment.docx', N'E:\Webservices\Ops\app_files\file-uploads\application-vnd.openxmlformats-officedocument.wordprocessingml.document\2938cdd8-2c8e-48d1-a3da-d38c7ccc4f18.docx', N'Merge Deployment', CAST(N'2016-06-10T09:46:40.4083243-04:00' AS DateTimeOffset))
GO
SET IDENTITY_INSERT [dbo].[Artifact] OFF
GO
SET IDENTITY_INSERT [dbo].[Documentation] ON 

GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (1, 0, 81, 5)
GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (2, 1, 75, 7)
GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (3, 2, 75, 1)
GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (4, 3, 75, 4)
GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (5, 4, 75, 4)
GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (6, 5, 75, 1)
GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (7, 6, 75, 1)
GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (8, 7, 75, 47)
GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (9, 8, 75, 47)
GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (10, 9, 75, 1)
GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (11, 10, 75, 1)
GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (12, 11, 75, 2)
GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (13, 12, 75, 2)
GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (14, 13, 75, 47)
GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (15, 14, 75, 47)
GO
INSERT [dbo].[Documentation] ([DocumentId], [ArtifactId], [ArtifactScopeTypeId], [ArtifactScopeId]) VALUES (16, 15, 75, 47)
GO
SET IDENTITY_INSERT [dbo].[Documentation] OFF
GO
SET IDENTITY_INSERT [dbo].[Favorite] ON 

GO
INSERT [dbo].[Favorite] ([FavoriteId], [Username], [Model], [ModelId], [Groupname], [Color], [Tds]) VALUES (2, N'bnelson@recordedbooks.com', N'server', 25, N'none', N'none', CAST(N'2016-04-28 09:12:25.183' AS DateTime))
GO
INSERT [dbo].[Favorite] ([FavoriteId], [Username], [Model], [ModelId], [Groupname], [Color], [Tds]) VALUES (3, N'bnelson@recordedbooks.com', N'server', 24, N'none', N'none', CAST(N'2016-04-28 09:12:49.760' AS DateTime))
GO
INSERT [dbo].[Favorite] ([FavoriteId], [Username], [Model], [ModelId], [Groupname], [Color], [Tds]) VALUES (6, N'rrama@recordedbooks.com', N'server', 38, N'none', N'none', CAST(N'2016-04-29 08:18:49.213' AS DateTime))
GO
INSERT [dbo].[Favorite] ([FavoriteId], [Username], [Model], [ModelId], [Groupname], [Color], [Tds]) VALUES (7, N'bnelson@recordedbooks.com', N'server', 29, N'none', N'none', CAST(N'2016-04-29 08:24:08.760' AS DateTime))
GO
INSERT [dbo].[Favorite] ([FavoriteId], [Username], [Model], [ModelId], [Groupname], [Color], [Tds]) VALUES (8, N'bnelson@recordedbooks.com', N'server', 38, N'none', N'none', CAST(N'2016-04-29 09:30:46.080' AS DateTime))
GO
INSERT [dbo].[Favorite] ([FavoriteId], [Username], [Model], [ModelId], [Groupname], [Color], [Tds]) VALUES (10, N'rnadar@recordedbooks.com', N'server', 53, N'none', N'none', CAST(N'2016-05-02 14:53:21.363' AS DateTime))
GO
INSERT [dbo].[Favorite] ([FavoriteId], [Username], [Model], [ModelId], [Groupname], [Color], [Tds]) VALUES (11, N'bnelson@recordedbooks.com', N'server', 83, N'none', N'none', CAST(N'2016-06-03 08:10:02.277' AS DateTime))
GO
INSERT [dbo].[Favorite] ([FavoriteId], [Username], [Model], [ModelId], [Groupname], [Color], [Tds]) VALUES (12, N'bnelson@recordedbooks.com', N'server', 82, N'none', N'none', CAST(N'2016-06-03 08:13:02.283' AS DateTime))
GO
INSERT [dbo].[Favorite] ([FavoriteId], [Username], [Model], [ModelId], [Groupname], [Color], [Tds]) VALUES (13, N'bnelson@recordedbooks.com', N'server', 81, N'none', N'none', CAST(N'2016-06-03 08:14:57.880' AS DateTime))
GO
INSERT [dbo].[Favorite] ([FavoriteId], [Username], [Model], [ModelId], [Groupname], [Color], [Tds]) VALUES (14, N'bnelson@recordedbooks.com', N'server', 85, N'none', N'none', CAST(N'2016-06-03 08:36:49.653' AS DateTime))
GO
INSERT [dbo].[Favorite] ([FavoriteId], [Username], [Model], [ModelId], [Groupname], [Color], [Tds]) VALUES (15, N'bnelson@recordedbooks.com', N'server', 84, N'none', N'none', CAST(N'2016-06-03 08:38:38.507' AS DateTime))
GO
INSERT [dbo].[Favorite] ([FavoriteId], [Username], [Model], [ModelId], [Groupname], [Color], [Tds]) VALUES (16, N'bnelson@recordedbooks.com', N'server', 40, N'none', N'none', CAST(N'2016-06-23 10:27:18.407' AS DateTime))
GO
INSERT [dbo].[Favorite] ([FavoriteId], [Username], [Model], [ModelId], [Groupname], [Color], [Tds]) VALUES (17, N'bnelson@recordedbooks.com', N'server', 41, N'none', N'none', CAST(N'2016-06-23 10:27:26.870' AS DateTime))
GO
INSERT [dbo].[Favorite] ([FavoriteId], [Username], [Model], [ModelId], [Groupname], [Color], [Tds]) VALUES (18, N'bnelson@recordedbooks.com', N'serverapp', 134, N'none', N'none', CAST(N'2016-07-13 08:16:41.383' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Favorite] OFF
GO
SET IDENTITY_INSERT [dbo].[Selection] ON 

GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (0, N'None', N'null', 0, 0, 0, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (1, N'Operating System', N'operating-system', 0, 0, 0, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (2, N'Application Type', N'app-type', 0, 0, 0, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (3, N'Domain', N'domain', 0, 0, 0, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (4, N'Server Security', N'security', 0, 0, 0, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (5, N'Scope', N'scope', 0, 0, 0, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (6, N'Artifact Types', N'artifact-type', 0, 0, 0, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (7, N'Artifact Scopes', N'artifact-scope', 0, 0, 0, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (8, N'Hosting', N'hosting', 0, 0, 0, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (9, N'Url', N'url', 0, 0, 0, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (10, N'Mime', N'windows', 0, 0, 0, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (11, N'Windows', N'windows', 0, 0, 1, N'app.windows.png')
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (12, N'Linux', N'linux', 0, 0, 1, N'linux.png')
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (13, N'Windows 2008 R2', N'windows-2008-r2', 0, 0, 1, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (14, N'Windows 2012 R2', N'windows-2012-r2', 0, 0, 1, N'app.windows.png')
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (15, N'Redhat 6', N'redhat-6', 0, 0, 1, N'linux.png')
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (16, N'RBENT', N'rbent', 0, 0, 4, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (17, N'RBDEV', N'rbdev', 0, 0, 4, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (18, N'RBUAT', N'rbuat', 0, 0, 4, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (19, N'RBRS', N'rbrs', 0, 0, 4, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (20, N'Amazon', N'amazon', 0, 0, 8, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (21, N'Rackspace', N'rackspace', 0, 0, 8, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (22, N'Hanover', N'hanover', 0, 0, 8, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (23, N'Prince Frederick', N'Prince Frederick', 0, 0, 8, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (24, N'Web Forms', N'web-forms', 0, 0, 2, N'app.website.png')
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (25, N'Web MVC', N'web-mvc', 0, 0, 2, N'app.website.png')
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (26, N'Web SPA', N'web-spa', 0, 0, 2, N'app.website.png')
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (27, N'Web API', N'web-api', 0, 0, 2, N'app.webservice.png')
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (28, N'Web WCF', N'web-wcf', 0, 0, 2, N'app.webservice.png')
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (29, N'Windows Console', N'windows-console', 0, 0, 2, N'app.processing.png')
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (30, N'Windows Service', N'windows-service', 0, 0, 2, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (31, N'Windows Client', N'windows-client', 0, 0, 2, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (32, N'Windows Forms', N'windows-forms', 0, 0, 2, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (33, N'Windows WPF', N'windows-wpf', 0, 0, 2, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (34, N'Windows Universal', N'windows-universal', 0, 0, 2, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (35, N'Device-iOS', N'device-ios', 0, 0, 2, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (36, N'Device-Android', N'device-android', 0, 0, 2, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (37, N'Device-Xamarin', N'device-xamarin', 0, 0, 2, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (38, N'Sql Server', N'sql-server', 0, 0, 2, N'app.datastore.png')
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (39, N'MongoDb', N'mongodb', 0, 0, 2, N'app.mongo.png')
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (40, N'Redis', N'redis', 0, 0, 2, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (41, N'Search', N'search', 0, 0, 2, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (42, N'North America', N'north-america', 0, 0, 5, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (43, N'United Kingdom', N'uk', 0, 0, 5, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (44, N'Global', N'global', 0, 0, 5, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (45, N'.net', N'net', 0, 0, 3, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (46, N'.biz', N'biz', 0, 0, 3, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (47, N'.us', N'us', 0, 0, 3, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (48, N'.eu', N'eu', 0, 0, 3, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (49, N'uat.com', N'uat-com', 0, 0, 3, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (50, N'.com', N'com', 0, 0, 3, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (51, N'Dataflow Diagram', N'dataflow-diagram', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (52, N'Sequence Diagram', N'sequence-diagram', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (53, N'Scope Diagram', N'scope-diagram', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (54, N'Tiers & Layers', N'tiers-layers', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (55, N'Context Diagram', N'context-diagram', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (56, N'Block and Line Diagram', N'block-line-diagram', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (57, N'Functional Requirements', N'functional-requirements', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (58, N'Non-functional Requirements', N'non-functional-requirements', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (59, N'Use Cases', N'use-case', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (60, N'KJ Analysis', N'kj-analysis', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (61, N'Personas and Scenarios', N'persona-scenario', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (62, N'Component Diagram', N'component-diagram', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (63, N'Whiteboard', N'whiteboard', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (64, N'Data Model', N'data-model', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (65, N'Physical Data Model', N'physical-data-model', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (66, N'Logical Data Model', N'logical-data-model', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (67, N'Prototype', N'prototype', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (68, N'Security Threat Model', N'security-threat-model', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (69, N'Risk Analysis', N'Risk Analysis', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (70, N'Vision Statement', N'vision-statement', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (71, N'Storyboard', N'storyboard', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (72, N'Decision Analysis', N'decision-analysis', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (73, N'Wideband Delphi', N'wideband-delphi', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (74, N'User Interface Analysis', N'ui-analysis', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (75, N'Solution Artifact', N'solution-artifact', 0, 0, 7, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (76, N'Server Artifact', N'server-artifact', 0, 0, 7, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (77, N'Application Artifact', N'app-artifact', 0, 0, 7, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (78, N'Component Artifact', N'component-artifact', 0, 0, 7, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (79, N'Deployment Artifact', N'deployment-artifact', 0, 0, 7, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (80, N'Zone Artifact', N'zone-artifact', 0, 0, 7, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (81, N'Server App Artifact', N'server-app-artifact', 0, 0, 7, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (82, N'SSIS', N'sql-server-ssis', 0, 0, 2, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (83, N'Sql Scripts', N'sql-server-script', 0, 0, 2, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (84, N'State Diagram', N'state-diagram', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (85, N'Solution', N'solution', 0, 0, 2, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (86, N'Deployment', N'deployment', 0, 0, 6, NULL)
GO
INSERT [dbo].[Selection] ([SelectionId], [Display], [Token], [Sort], [GroupId], [MasterId], [Icon]) VALUES (87, N'uat.eu', N'uat-eu', 0, 0, 3, NULL)
GO
SET IDENTITY_INSERT [dbo].[Selection] OFF
GO
SET IDENTITY_INSERT [dbo].[Server] ON 

GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (1, 13, 21, 19, N'632996-ROWEB04', N'NA Web 4', N'Web App 4', N'162.209.16.188', N'10.138.94.204', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (2, 13, 21, 19, N'633000-ROWEB05', N'NA Web 5', N'NA Web 5', N'162.209.16.189', N'10.138.94.207', N'web')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (3, 13, 21, 19, N'new 3', N'n', N'n', N'0', N'0', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (4, 13, 21, 19, N'new 4', N'n', N'n', N'0', N'0', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (5, 13, 21, 19, N'new 5', N'n', N'n', N'0', N'0', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (6, 14, 21, 19, N'735877-RSDB01', N'735877-RSDB01', N'Database server', N'50.57.14.222', N'10.138.225.145', N'sqlserver, 737423-SQLCLUS1')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (7, 13, 21, 19, N'493385-RSACS02', N'ACS Packager 2', N'ACS Packager 2', N'108.166.34.46', N'10.138.185.73', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (8, 13, 21, 19, N'493412-RSACS01', N'ACS Packager 01', N'ACS Packager 01', N'162.209.16.203', N'10.138.185.93', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (9, 13, 21, 19, N'655934-ROMETL02', N'NA METL', N'NA METL', N'184.106.85.196', N'10.138.225.133', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (10, 11, 21, 19, N'644837-ROSCM02', N'OneClick Admin Web', N'OneClick Admin Web', N'50.57.29.16', N'10.138.225.141', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (11, 13, 21, 19, N'487398-RSMV01', N'487398-RSMV01', N'Media Validation Server', N'184.106.44.46', N'10.138.185.70', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (12, 13, 21, 19, N'345995-OCDRPT', N'Production Reports', N'Production Reports', N'184.106.48.52', N'10.138.94.198', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (13, 12, 21, 19, N'606296-ROSOLR03', N'Mongo Arbiter', N'Mongo Arbiter', N'184.106.48.53', N'unknown', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (14, 12, 21, 19, N'652552-REMONGO1', N'Mongo', N'Mongo', N'184.106.85.195', N'unknown', N'mongodb, unknown')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (15, 12, 21, 19, N'641565-REMETL01', N'641565-REMETL01', N'Mongo 2', N'104.130.107.130', N'unknown', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (16, 12, 21, 19, N'643612-ROSOLR07', N'643612-ROSOLR07', N'SOLR3 Memcache', N'184.106.32.144', N'unknown', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (17, 12, 21, 19, N'641558-ROSOLR05', N'641558-ROSOLR05', N'SOLR4 Redis', N'104.130.107.128', N'unknown', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (18, 12, 21, 19, N'641564-ROSOLR06', N'641564-ROSOLR06', N'SOLR4, Redis', N'104.130.107.129', N'unknown', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (19, 13, 21, 19, N'631182-RSAB01', N'Active Batch (Rack)', N'Active Batch', N'192.237.227.168', N'10.138.94.202', N'AB, activebatch')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (20, 13, 21, 19, N'564662-RSFTP', N'564662-RSFTP', N'FTP Server', N'50.56.44.8', N'10.138.94.194', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (21, 14, 20, 19, N'778026-ROMETL01', N'Production NA Domain Controller', N'Production NA Domain Controller', N'184.106.48.243', N'10.138.106.199', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (22, 14, 21, 19, N'327195-RSDC02', N'Production NA Domain Controller', N'Production NA Domain Controller', N'184.106.48.50', N'10.138.94.214', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (23, 14, 21, 19, N'unknown', N'unknown', N'New email server', N'50.56.44.13', N'unknown', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (24, 14, 20, 18, N'RBUAT-NA-PDBW2', N'RBUAT-NA-PDBW2', N'RBUAT-NA-PDBW2', N'184.72.96.136', N'10.0.1.48', N'partner')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (25, 13, 20, 18, N'RBUAT-NA-PAPI', N'RBUAT-NA-PAPI', N'RBUAT-NA-PAPI', N'54.208.83.241', N'10.0.1.147', N'partner')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (26, 13, 20, 18, N'RBUAT-NA-PADM', N'RBUAT-NA-PADM', N'RBUAT-NA-PADM', N'107.23.84.96', N'10.0.1.50', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (27, 12, 20, 18, N'RBUAT-NA-PRMS', N'RBUAT-NA-PRMS', N'RBUAT-NA-PRMS', N'unknown', N'10.0.1.7', N'partner')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (28, 12, 20, 18, N'RBUAT-NA-PSOLR3', N'RBUAT-NA-PSOLR3', N'RBUAT-NA-PSOLR3', N'unknown', N'10.0.1.254', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (29, 14, 20, 18, N'RBUAT-NA-SSQL2', N'RBUAT-NA-SSQL2', N'RBUAT-NA-SSQL2', N'107.23.84.86', N'10.0.1.251', N'uat, uat.com')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (30, 13, 20, 18, N'RBUAT-NA-API1', N'RBUAT-NA-API1', N'RBUAT-NA-API1', N'54.208.69.180', N'10.0.1.234', N'uat')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (31, 13, 20, 16, N'RBUAT-NA-ADM', N'RBUAT-NA-ADM', N'RBUAT-NA-ADM', N'54.208.52.255', N'10.0.1.64', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (32, 13, 20, 18, N'RBUAT-NA-ACS', N'RBUAT-NA-ACS', N'RBUAT-NA-ACS', N'107.23.84.84', N'10.0.1.252', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (33, 13, 20, 18, N'RBUAT-NA-WEB', N'RBUAT-NA-WEB', N'RBUAT-NA-WEB', N'107.23.70.154', N'10.0.1.11', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (34, 12, 20, 18, N'RBUAT-NA-SOLR3', N'RBUAT-NA-SOLR3', N'RBUAT-NA-SOLR3', N'unknown', N'10.0.1.193', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (35, 12, 20, 18, N'RBUAT-NA-SOLR4', N'RBUAT-NA-SOLR4', N'RBUAT-NA-SOLR4', N'107.21.18.172', N'10.0.1.171', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (36, 12, 20, 18, N'RBUAT-NA-META', N'RBUAT-NA-META', N'RBUAT-NA-META', N'107.23.84.89', N'10.0.1.233', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (37, 12, 20, 18, N'RBUAT-NA-BIGD', N'RBUAT-NA-BIGD', N'RBUAT-NA-BIGD', N'54.208.88.217', N'10.0.1.161', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (38, 13, 20, 18, N'RBUAT-BATCH', N'RBUAT-BATCH', N'RBUAT-BATCH', N'54.208.54.97', N'10.0.1.58', N'NA,UAT,Jobs,Box, partner')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (39, 14, 21, 18, N'AMRBUATDC', N'AMRBUATDC', N'AMRBUATDC', N'54.208.86.86', N'10.0.1.248', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (40, 14, 20, 18, N'RBUAT-EU-PDBW2', N'RBUAT-EU-PDBW2', N'RBUAT-EU-PDBW2', N'184.72.124.73', N'10.0.1.241', N'ws, wfh, broker, partner, eu')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (41, 13, 20, 18, N'RBUAT-EU-PAPI', N'RBUAT-EU-PAPI', N'RBUAT-EU-PAPI', N'54.208.39.176', N'10.0.1.35', N'partner eu')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (42, 13, 20, 18, N'RBUAT-EU-PADM', N'RBUAT-EU-PAPI', N'RBUAT-EU-PAPI', N'107.23.69.136', N'10.0.1.100', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (43, 13, 20, 18, N'RBUAT-EU-ADM', N'RBUAT-EU-ADM', N'RBUAT-EU-ADM', N'107.23.217.111', N'10.0.1.128', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (44, 13, 20, 18, N'RBUAT-EU-API', N'RBUAT-EU-API', N'RBUAT-EU-API', N'184.72.118.231', N'10.0.1.12', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (45, 13, 20, 18, N'RBUAT-EU-WEB', N'RBUAT-EU-WEB', N'RBUAT-EU-WEB', N'184.72.119.136', N'10.0.1.157', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (46, 14, 20, 18, N'RBUAT-EU-SQL2', N'RBUAT-EU-SQL2', N'RBUAT-EU-SQL2', N'184.72.123.74', N'10.0.1.221', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (47, 13, 20, 18, N'RBUAT-EU-ACS', N'RBUAT-EU-ACS', N'RBUAT-EU-ACS', N'54.208.39.140', N'10.0.1.207', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (48, 14, 21, 19, N'762730-RSKSRV01', N'API1', N'762730-RSKSRV01', N'108.166.3.81', N'10.138.225.155', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (49, 14, 21, 19, N'762731-RSKSRV02', N'API2', N'762731-RSKSRV02', N'108.166.3.83', N'10.138.225.152', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (50, 14, 21, 19, N'762729-RSKENT02', N'WEB 02', N'762729-RSKENT02', N'184.106.48.54', N'10.138.225.148', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (51, 14, 21, 19, N'762728-RSKENT01', N'WEB 01', N'762728-RSKENT01', N'184.106.48.55', N'10.138.225.149', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (52, 12, 21, 19, N'Kentico Mongo', N'Kentico Mongo', N'Kentico Mongo', N'108.166.3.84', N'unknown', N'mgo')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (53, 12, 21, 19, N'Kentico ElasticSearch 1', N'Search 01', N'Kentico ElasticSearch 1', N'108.171.167.216', N'unknown', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (54, 12, 21, 19, N'Kentico ElasticSearch 2', N'Search 02', N'Kentico ElasticSearch 2', N'108.166.34.41', N'unknown', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (55, 12, 21, 19, N'Kentico ElasticSearch 3', N'Search 03', N'Kentico ElasticSearch 3', N'108.166.3.85', N'unknown', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (56, 13, 21, 19, N'644843-RESCM02', N'644843-RESCM02', N'644843-RESCM02', N'50.57.29.17', N'10.138.225.140', N'wfh, sitecore')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (57, 13, 21, 19, N'674727-REMETL02', N'674727-REMETL02', N'674727-REMETL02', N'184.106.32.148', N'10.138.94.196', N'wfh, metl')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (58, 13, 21, 19, N'633005-REWEB03', N'633005-REWEB03', N'633005-REWEB03', N'192.237.149.27', N'10.138.94.211', N'wfh')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (59, 13, 21, 19, N'633011-REWEB04', N'633011-REWEB04', N'633011-REWEB04', N'192.237.227.169', N'10.138.94.212', N'wfh')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (60, 13, 21, 19, N'546013-REACS02', N'546013-REACS02', N'546013-REACS02', N'192.237.227.170', N'10.138.185.86', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (61, 13, 21, 19, N'633015-RESVC04', N'633015-RESVC04', N'633015-RESVC04', N'192.237.227.171', N'10.138.94.213', N'wfh, prod')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (62, 13, 21, 19, N'633013-RESVC03', N'633013-RESVC03', N'633013-RESVC03', N'192.237.227.175', N'10.138.94.220', N'wfh, prod')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (63, 13, 21, 19, N'735878-RSDB02', N'735878-RSDB02', N'735878-RSDB02', N'50.56.44.12', N'10.138.225.151', N'sqlserver, 737423-SQLCLUS1')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (64, 13, 21, 19, N'545988-REACS01', N'545988-REACS01', N'545988-REACS01', N'192.237.227.172', N'10.138.185.82', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (65, 15, 21, 19, N'643334-RESOLR03', N'643334-RESOLR03', N'Active Search / Passive Redis', N'104.130.107.131', N'192.168.100.131', N'wfh, solr')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (66, 15, 21, 19, N'643335-RESOLR04', N'643335-RESOLR04', N'643335-RESOLR04', N'108.166.3.86', N'192.168.100.86', N'wfh, solr, passive search, arbiter mongo, active redis')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (67, 12, 21, 19, N'674726-REMONG1', N'674726-REMONG1', N'674726-REMONG1', N'184.106.48.242', N'192.168.100.242', N'wfh, mongodb')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (68, 12, 21, 19, N'674725-REMONG02', N'674725-REMONG02', N'674725-REMONG02', N'184.106.32.150', N'192.168.100.150', N'wfh, mongodb')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (69, 15, 21, 19, N'647466-RESOLR05', N'647466-RESOLR05', N'647466-RESOLR05', N'184.106.85.194', N'192.168.100.194', N'wfh, solr')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (70, 13, 21, 19, N'313946-VM1', N'313946-VM1', N'313946-VM1', N'184.106.32.147', N'10.138.94.200', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (71, 14, 22, 16, N'HNSQLDEV02', N'HNSQLDEV02', N'HNSQLDEV02', N'192.168.97.29', N'unknown', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (72, 11, 22, 16, N'RBFS03', N'RBFS03', N'RBFS03', N'192.1683973246', N'unknown', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (73, 11, 20, 16, N'HNOCADM01', N'HNOCADM01', N'HNOCADM01', N'192.168.97.149', N'unknown', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (74, 12, 20, 18, N'RBUAT-EU-PRMS', N'RBUAT-EU-PRMS', N'RBUAT-EU-PRMS', N'unknown', N'10.0.1.145', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (75, 12, 20, 18, N'RBUAT-EU-PSOLR3', N'RBUAT-EU-PSOLR3', N'RBUAT-EU-PSOLR3', N'unknown', N'10.0.1.4', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (76, 12, 20, 18, N'RBUAT-EU-META', N'RBUAT-EU-META', N'RBUAT-EU-META', N'107.23.10.185', N'10.0.1.62', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (77, 12, 20, 18, N'RBUAT-EU-SOLR3', N'RBUAT-EU-SOLR3', N'RBUAT-EU-SOLR3', N'unknown', N'10.0.1.118', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (78, 12, 20, 18, N'RBUAT-EU-BIGD', N'RBUAT-EU-BIGD', N'RBUAT-EU-BIGD', N'107.23.84.89', N'10.0.1.22', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (79, 12, 20, 18, N'RBUAT-EU-SOLR4', N'RBUAT-EU-SOLR4', N'RBUAT-EU-SOLR4', N'unknown', N'10.0.1.34', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (80, 14, 23, 16, N'PFWEBPROD01', N'PFWEBPROD01', N'Prince Frederick Production Web 01', N'192.168.40.48', N'192.168.40.248', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (81, 14, 21, 19, N'778032-ROSVC02', N'Prod NA API 2', N'new prod', N'192.237.149.26', N'192.168.100.26', N'prod, api')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (82, 14, 21, 19, N'778034-ROSVC03', N'Prod NA API 3', N'Production API', N'162.209.16.190', N'192.168.100.190', N'prod, api')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (83, 14, 21, 19, N'788035-ROSVC01', N'Prod API 1', N'Production API', N'184.106.48.247', N'192.168.100.247', N'prod, api')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (84, 14, 21, 19, N'778036-ROWEB01', N'Prod NA Web 1', N'Prod NA Web 1', N'162.209.38.52', N'192.168.101.52', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (85, 14, 21, 19, N'778033-ROWEB02', N'Prod NA Web 2', N'ROWEB02', N'108.166.42.197', N'192.168.101.197', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (86, 14, 21, 19, N'778026-ROMETL01', N'Prod NA METL', N'Prod NA METL', N'184.106.48.243', N'192.168.100.243', NULL)
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (87, 13, 23, 16, N'RBACTIVEBATCH02', N'PF ActiveBatch', N'Active Batch server in PF', N'192.168.40.82', N'192.168.10.1', N'ab, pf')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (88, 12, 21, 19, N'778048-ROMONG01', N'Mongo', N'Mongo', N'162.209.38.55', N'192.168.101.55', N'mongodb')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (89, 12, 21, 19, N'778052-ROMONG02', N'Mongo', N'Mongo', N'162.209.38.49', N'192.168.101.49', N'mongodb')
GO
INSERT [dbo].[Server] ([ServerId], [OperatingSystemId], [HostPlatformId], [SecurityId], [Name], [Alias], [Description], [ExternalIP], [InternalIP], [Tags]) VALUES (90, 12, 21, 19, N'778046-ROARBIT01', N'Mongo', N'mongo', N'162.209.38.53', N'192.168.101.53', NULL)
GO
SET IDENTITY_INSERT [dbo].[Server] OFF
GO
SET IDENTITY_INSERT [dbo].[ServerApp] ON 

GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (54, 45, 2, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (60, 46, 3, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (57, 46, 4, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (58, 46, 5, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (53, 45, 10, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (1, 19, 11, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (2, 19, 12, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (3, 19, 13, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (4, 19, 14, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (10, 24, 15, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (40, 40, 15, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (59, 46, 15, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (12, 24, 16, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (43, 40, 16, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (62, 46, 16, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (13, 24, 17, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (44, 40, 17, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (63, 46, 17, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (14, 24, 18, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (45, 40, 18, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (64, 46, 18, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (15, 24, 20, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (46, 40, 20, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (65, 46, 20, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (49, 42, 21, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (50, 43, 21, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (36, 40, 22, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (55, 46, 22, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (37, 40, 23, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (56, 46, 23, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (42, 40, 24, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (61, 46, 24, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (74, 58, 25, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (66, 48, 29, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (67, 49, 29, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (71, 55, 30, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (68, 50, 31, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (69, 51, 31, 0, 0, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (92, 9, 7, 0, 42, 50, NULL, NULL)
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (31, 30, 1, 4, 42, 49, N'e:\webservices\api', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (33, 33, 2, 4, 42, 49, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (25, 29, 3, 4, 42, 49, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (22, 29, 4, 4, 42, 49, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (23, 29, 5, 4, 42, 49, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (101, 38, 7, 4, 42, 49, N'd:\Oneclick Jobs\NA\OneclickUAT\METL', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (35, 33, 10, 4, 42, 49, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (24, 29, 15, 4, 42, 49, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (26, 29, 16, 4, 42, 49, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (29, 29, 18, 4, 42, 49, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (30, 29, 20, 4, 42, 49, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (20, 29, 22, 4, 42, 49, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (21, 29, 23, 4, 42, 49, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (27, 29, 24, 4, 42, 49, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (32, 33, 25, 4, 42, 49, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (134, 36, 44, 4, 42, 49, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (135, 37, 48, 4, 42, 50, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (16, 25, 1, 7, 42, 47, N'e:\webservices\API', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (19, 24, 2, 7, 42, 47, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (11, 24, 3, 7, 42, 47, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (8, 24, 4, 7, 42, 47, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (9, 24, 5, 7, 42, 47, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (17, 25, 6, 7, 42, 47, N'e:\webservices\ApiBigData', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (99, 38, 7, 7, 42, 49, N'd:\Oneclick Jobs\NA\Oneclick-Partner\METL', NULL)
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (18, 26, 21, 7, 42, 47, N'e:\sitecore\website', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (129, 84, 9, 8, 42, 50, N'd:\websites\oneping', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (115, 81, 1, 9, 42, 50, N'd:\webservices\api', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (120, 82, 1, 9, 42, 50, N'd:\webservices\api', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (110, 83, 1, 9, 42, 50, N'd:\webservices\api', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (128, 84, 2, 9, 42, 50, N'd:\websites\rb.oneclick', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (125, 85, 2, 9, 42, 50, N'd:\websites\rb.oneclick', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (89, 6, 3, 9, 42, 50, NULL, NULL)
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (7, 6, 4, 9, 42, 50, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (90, 6, 5, 9, 42, 50, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (116, 81, 6, 9, 42, 50, N'd:\webservices\apibigdata', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (121, 82, 6, 9, 42, 50, N'd:\webservices\apibigdata', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (111, 83, 6, 9, 42, 50, N'd:\webservices\apibigdata', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (131, 86, 7, 9, 42, 50, N'd:\jobs\metl', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (127, 85, 9, 9, 42, 50, N'd:\websites\oneping', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (130, 84, 10, 9, 42, 50, N'd:\websites\rb.publisher', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (126, 85, 10, 9, 42, 50, N'd:\websites\rb.publisher', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (100, 7, 11, 9, 42, 50, NULL, NULL)
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (91, 6, 17, 9, 42, 50, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (97, 80, 18, 9, 42, 50, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (98, 80, 19, 9, 42, 50, NULL, NULL)
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (132, 86, 26, 9, 42, 50, N'd:\jobs\ownershipsync', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (133, 86, 27, 9, 42, 50, N'd:\jobs\ownershipsync', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (106, 50, 31, 9, 42, NULL, NULL, NULL)
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (107, 51, 31, 9, 42, 50, NULL, NULL)
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (70, 6, 32, 9, 42, 50, NULL, NULL)
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (117, 81, 34, 9, 42, 50, N'd:\webservices/logging', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (122, 82, 34, 9, 42, 50, N'd:\webservices\logging', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (113, 83, 34, 9, 42, 50, N'd:\webservices\logging', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (118, 81, 35, 9, 42, 50, N'd:\webservices.orders.pickup.service', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (123, 82, 35, 9, 42, 50, N'd:\webservices\orders.pickup.service', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (114, 83, 35, 9, 42, 50, N'd:\webservices\orders.pickup.service', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (96, 80, 36, 9, 42, 50, N'c:\inetpub\wwwroot\orderbrokerrequest', N'c:\backups\order.broker.request')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (105, 52, 43, 9, 42, 50, N'/data/mongo/data', NULL)
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (119, 81, 46, 9, 42, 50, N'd:\webservices\fulfillment', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (124, 82, 46, 9, 42, 50, N'd:\webservices\fulfillment', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (112, 83, 46, 9, 42, 50, N'd:\webservices\fulfillment', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (51, 44, 1, 4, 43, 48, N'd:\webservices\api', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (102, 38, 8, 4, 43, 49, N'd:\Oneclick Jobs\EU\OneclickUAT\METL', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (28, 29, 17, 4, 43, 49, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (52, 45, 25, 4, 43, 46, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (39, 40, 5, 7, 43, 46, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (104, 38, 8, 7, 43, 49, N'd:\Oneclick Jobs\EU\METL', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (47, 40, 28, 7, 43, NULL, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (108, 74, 44, 7, 43, 46, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (109, 74, 45, 7, 43, 46, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (80, 61, 1, 9, 43, 48, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (83, 62, 1, 9, 43, 48, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (75, 58, 2, 9, 43, 48, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (78, 59, 2, 9, 43, 48, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (81, 61, 6, 9, 43, 48, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (84, 62, 6, 9, 43, 48, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (73, 57, 8, 9, 43, 48, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (76, 58, 9, 9, 43, 48, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (79, 59, 9, 9, 43, 48, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (77, 58, 10, 9, 43, 48, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (72, 56, 21, 9, 43, 48, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (82, 61, 34, 9, 43, 48, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (85, 62, 34, 9, 43, 48, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (48, 41, 1, 10, 43, 46, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (41, 40, 3, 10, 43, 46, N'd:\none', N'c:\none')
GO
INSERT [dbo].[ServerApp] ([ServerAppId], [ServerId], [AppId], [ZoneId], [ScopeId], [DomainId], [Folderpath], [BackupFolderpath]) VALUES (38, 40, 4, 10, 43, 46, N'd:\none', N'c:\none')
GO
SET IDENTITY_INSERT [dbo].[ServerApp] OFF
GO
SET IDENTITY_INSERT [dbo].[Solution] ON 

GO
INSERT [dbo].[Solution] ([SolutionId], [ScopeId], [Name], [Alias], [Description]) VALUES (1, 42, N'Digital Platform NA', N'Digital Platform NA', N'Digital Libraries for patrons in North America')
GO
INSERT [dbo].[Solution] ([SolutionId], [ScopeId], [Name], [Alias], [Description]) VALUES (2, 42, N'WebMarket NA', N'Kentico NA', N'Kentico for North America')
GO
INSERT [dbo].[Solution] ([SolutionId], [ScopeId], [Name], [Alias], [Description]) VALUES (3, 42, N'Order Broker NA', N'Order Broker', N'Coordinated pickup of orders with insertion into OrderReview')
GO
INSERT [dbo].[Solution] ([SolutionId], [ScopeId], [Name], [Alias], [Description]) VALUES (4, 42, N'My Solution', N'My Solution', N'My Solution')
GO
INSERT [dbo].[Solution] ([SolutionId], [ScopeId], [Name], [Alias], [Description]) VALUES (5, 43, N'Digital Platform EU', N'Digital Platform EU', N'Digital Libraries for patrons in EU')
GO
SET IDENTITY_INSERT [dbo].[Solution] OFF
GO
SET IDENTITY_INSERT [dbo].[SolutionApp] ON 

GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (2, 1, 1, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (1, 1, 2, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (6, 1, 3, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (4, 1, 4, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (5, 1, 5, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (3, 1, 7, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (25, 1, 26, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (26, 1, 27, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (27, 1, 44, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (12, 2, 18, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (10, 2, 20, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (8, 2, 29, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (9, 2, 30, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (7, 2, 31, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (11, 2, 32, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (13, 2, 33, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (24, 2, 43, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (15, 3, 3, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (18, 3, 18, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (14, 3, 19, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (16, 3, 32, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (19, 3, 35, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (20, 3, 36, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (21, 3, 37, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (23, 4, 11, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (22, 4, 12, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (28, 5, 1, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (29, 5, 2, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (30, 5, 3, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (31, 5, 4, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (32, 5, 5, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (33, 5, 8, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (34, 5, 26, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (35, 5, 27, 0)
GO
INSERT [dbo].[SolutionApp] ([SolutionAppId], [SolutionId], [AppId], [Sort]) VALUES (36, 5, 44, 0)
GO
SET IDENTITY_INSERT [dbo].[SolutionApp] OFF
GO
SET IDENTITY_INSERT [dbo].[SolutionZone] ON 

GO
INSERT [dbo].[SolutionZone] ([SolutionZoneId], [SolutionId], [ZoneId], [DomainId], [Name], [Description]) VALUES (8, 1, 4, 50, NULL, NULL)
GO
INSERT [dbo].[SolutionZone] ([SolutionZoneId], [SolutionId], [ZoneId], [DomainId], [Name], [Description]) VALUES (13, 1, 7, 46, NULL, NULL)
GO
INSERT [dbo].[SolutionZone] ([SolutionZoneId], [SolutionId], [ZoneId], [DomainId], [Name], [Description]) VALUES (2, 1, 7, 47, NULL, NULL)
GO
INSERT [dbo].[SolutionZone] ([SolutionZoneId], [SolutionId], [ZoneId], [DomainId], [Name], [Description]) VALUES (4, 1, 9, 50, NULL, NULL)
GO
INSERT [dbo].[SolutionZone] ([SolutionZoneId], [SolutionId], [ZoneId], [DomainId], [Name], [Description]) VALUES (7, 2, 2, 50, NULL, NULL)
GO
INSERT [dbo].[SolutionZone] ([SolutionZoneId], [SolutionId], [ZoneId], [DomainId], [Name], [Description]) VALUES (6, 2, 3, 50, NULL, NULL)
GO
INSERT [dbo].[SolutionZone] ([SolutionZoneId], [SolutionId], [ZoneId], [DomainId], [Name], [Description]) VALUES (5, 2, 9, 50, NULL, NULL)
GO
INSERT [dbo].[SolutionZone] ([SolutionZoneId], [SolutionId], [ZoneId], [DomainId], [Name], [Description]) VALUES (9, 3, 9, 50, NULL, NULL)
GO
INSERT [dbo].[SolutionZone] ([SolutionZoneId], [SolutionId], [ZoneId], [DomainId], [Name], [Description]) VALUES (11, 4, 1, 0, NULL, NULL)
GO
INSERT [dbo].[SolutionZone] ([SolutionZoneId], [SolutionId], [ZoneId], [DomainId], [Name], [Description]) VALUES (10, 4, 9, 50, NULL, NULL)
GO
INSERT [dbo].[SolutionZone] ([SolutionZoneId], [SolutionId], [ZoneId], [DomainId], [Name], [Description]) VALUES (16, 5, 4, 87, NULL, NULL)
GO
INSERT [dbo].[SolutionZone] ([SolutionZoneId], [SolutionId], [ZoneId], [DomainId], [Name], [Description]) VALUES (15, 5, 9, 48, NULL, NULL)
GO
INSERT [dbo].[SolutionZone] ([SolutionZoneId], [SolutionId], [ZoneId], [DomainId], [Name], [Description]) VALUES (14, 5, 10, 46, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[SolutionZone] OFF
GO
SET IDENTITY_INSERT [dbo].[Zone] ON 

GO
INSERT [dbo].[Zone] ([ZoneId], [Name], [Alias], [Token]) VALUES (0, N'None', N'none', N'no-zone')
GO
INSERT [dbo].[Zone] ([ZoneId], [Name], [Alias], [Token]) VALUES (1, N'Local', N'Local', N'local')
GO
INSERT [dbo].[Zone] ([ZoneId], [Name], [Alias], [Token]) VALUES (2, N'Development', N'development', N'development')
GO
INSERT [dbo].[Zone] ([ZoneId], [Name], [Alias], [Token]) VALUES (3, N'QA', N'QA', N'qa')
GO
INSERT [dbo].[Zone] ([ZoneId], [Name], [Alias], [Token]) VALUES (4, N'UAT', N'UAT', N'uat')
GO
INSERT [dbo].[Zone] ([ZoneId], [Name], [Alias], [Token]) VALUES (5, N'Integration', N'Integration', N'integration')
GO
INSERT [dbo].[Zone] ([ZoneId], [Name], [Alias], [Token]) VALUES (6, N'Testing', N'Testing', N'testing')
GO
INSERT [dbo].[Zone] ([ZoneId], [Name], [Alias], [Token]) VALUES (7, N'Partner', N'Partner', N'partner')
GO
INSERT [dbo].[Zone] ([ZoneId], [Name], [Alias], [Token]) VALUES (8, N'Staging', N'Staging', N'staging')
GO
INSERT [dbo].[Zone] ([ZoneId], [Name], [Alias], [Token]) VALUES (9, N'Production', N'Production', N'production')
GO
INSERT [dbo].[Zone] ([ZoneId], [Name], [Alias], [Token]) VALUES (10, N'Partner II', N'Partner II', N'partner-ii')
GO
SET IDENTITY_INSERT [dbo].[Zone] OFF
GO
ALTER TABLE [dbo].[AppComponent]  WITH CHECK ADD  CONSTRAINT [FK_AppComponent_App] FOREIGN KEY([AppId])
REFERENCES [dbo].[App] ([AppId])
GO
ALTER TABLE [dbo].[AppComponent] CHECK CONSTRAINT [FK_AppComponent_App]
GO
ALTER TABLE [dbo].[AppComponent]  WITH CHECK ADD  CONSTRAINT [FK_AppComponent_Component] FOREIGN KEY([ComponentId])
REFERENCES [dbo].[Component] ([ComponentId])
GO
ALTER TABLE [dbo].[AppComponent] CHECK CONSTRAINT [FK_AppComponent_Component]
GO
ALTER TABLE [dbo].[ComponentConstruct]  WITH CHECK ADD  CONSTRAINT [FK_ComponentConstruct_Component] FOREIGN KEY([ComponentId])
REFERENCES [dbo].[Component] ([ComponentId])
GO
ALTER TABLE [dbo].[ComponentConstruct] CHECK CONSTRAINT [FK_ComponentConstruct_Component]
GO
ALTER TABLE [dbo].[ComponentConstruct]  WITH CHECK ADD  CONSTRAINT [FK_ComponentConstruct_Construct] FOREIGN KEY([ConstructId])
REFERENCES [dbo].[Construct] ([ConstructId])
GO
ALTER TABLE [dbo].[ComponentConstruct] CHECK CONSTRAINT [FK_ComponentConstruct_Construct]
GO
ALTER TABLE [dbo].[SolutionApp]  WITH CHECK ADD  CONSTRAINT [FK_SolutionApp_App] FOREIGN KEY([AppId])
REFERENCES [dbo].[App] ([AppId])
GO
ALTER TABLE [dbo].[SolutionApp] CHECK CONSTRAINT [FK_SolutionApp_App]
GO
