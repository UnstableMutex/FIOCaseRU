﻿CREATE TABLE [Casing].[Patronymic]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [Nominative] VARCHAR(100) NOT NULL, 
    [Case] TINYINT NOT NULL, 
    [Gender] TINYINT NOT NULL
)
