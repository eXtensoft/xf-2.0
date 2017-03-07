/****** Object:  Schema [log]    Script Date: 3/6/2017 7:17:50 PM ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'log')
EXEC sys.sp_executesql N'CREATE SCHEMA [log]'

GO