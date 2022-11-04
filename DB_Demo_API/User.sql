CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL  IDENTITY, 
    [Email] NVARCHAR(100) UNIQUE NOT NULL, 
    [Password] NVARCHAR(100) NOT NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [FirstName] NVARCHAR(50) NULL, 
    [BirthDate] DATE NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
)

