CREATE TABLE [dbo].[ProductionReports]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Date] DATETIME NOT NULL, 
    [ItemName] NVARCHAR(50) NULL, 
    [ItemNumber] NVARCHAR(18) NULL, 
    [OrderId] INT NULL, 
    [TimeCode] NVARCHAR(12) NULL 
)
