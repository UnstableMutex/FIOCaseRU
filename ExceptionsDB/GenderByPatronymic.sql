﻿CREATE TABLE [Casing].[GenderByPatronymic]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [TargetString] VARCHAR(100) NOT NULL, 
    [Gender] TINYINT NOT NULL
)
