CREATE TABLE [dbo].[Contact]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [Birthdate] DATE NULL, 
    [Email] NVARCHAR(384) NULL UNIQUE,
    [UserId] INT NOT NULL,
    CONSTRAINT FK_Contact_User FOREIGN KEY (UserId)
    REFERENCES [User](Id)
   
)
