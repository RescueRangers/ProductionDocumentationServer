CREATE TABLE [dbo].[ReportPictures]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SectionName] NVARCHAR(16) NULL, 
    [PictureUrl] NVARCHAR(50) NOT NULL, 
    [ReportId] INT NOT NULL
)
