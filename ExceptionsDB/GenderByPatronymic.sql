﻿CREATE TABLE [dbo].[GenderByPatronymic]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [TargetString] VARCHAR(100) NOT NULL, 
    [Gender] NCHAR(10) NOT NULL
)
