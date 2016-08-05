﻿CREATE TABLE [Casing].[FirstName]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [Nominative] VARCHAR(100) NOT NULL, 
    [Case] TINYINT NOT NULL, 
    [Gender] TINYINT NOT NULL, 
    [Cased] VARCHAR(100) NOT NULL
)
