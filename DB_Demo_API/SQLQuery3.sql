USE [API_DEMO]
GO


CREATE TABLE [dbo].[Contact](
	[Id] [int] PRIMARY KEY IDENTITY NOT NULL,
	[LastName] [varchar](255) NOT NULL,
	[FirstName] [varchar](255) NOT NULL,
	[Email] [varchar](255)UNIQUE NOT NULL,
	[SurName] [varchar](255) NULL,
	[Phone] [varchar](255) NULL,
	[Birthdate] [datetime2](7) NULL,
	[UserId] int NOT NULL
	CONSTRAINT FK_UserID FOREIGN KEY ([UserId])REFERENCES [User](Id)
)


INSERT INTO [User] VALUES ('ben@mail.com', 'Test1234=', 'Sterckx', 'Benjamin','1980-12-10')

INSERT INTO [Contact] VALUES ('Tom','Tom','tom@mail.com', 'Tom','0465/123456', '1980-12-10',1)

Select * from Contact;
