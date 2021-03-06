
CREATE TABLE [Login](
	[LoginId] [uniqueidentifier] NOT NULL,
	[PersonId] [uniqueidentifier] NOT NULL,
	[IP] [varchar](20) NULL,
	[DateLoggedIn] [datetime] NOT NULL,
	CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED ([LoginId] ASC)
)

CREATE TABLE [Person](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](250) NOT NULL,
	[LastName] [nvarchar](250) NOT NULL,
	[AddressLine1] [nvarchar](250) NOT NULL,
	[AddressLine2] [nvarchar](250) NULL,
	[TownCity] [nvarchar](50) NOT NULL,
	[County] [nvarchar](50) NULL,
	[PostCode] [nvarchar](20) NOT NULL,
	[Type] [int] NULL,
	[DateCreated] [datetime] NOT NULL,
	[LastUpdated] [datetime] NULL,
	CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [PersonsToServers](
	[PersonId] [uniqueidentifier] NOT NULL,
	[ServerId] [uniqueidentifier] NOT NULL,
	CONSTRAINT [PK_PersonToServers] PRIMARY KEY CLUSTERED ([PersonId] ASC, [ServerId] ASC)
)

CREATE TABLE [Server](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[IPWhitelist] [varchar](max) NOT NULL,
	CONSTRAINT [PK_Server] PRIMARY KEY CLUSTERED ([Id] ASC)
)

ALTER TABLE [Login]
ADD CONSTRAINT [FK_Login_Person] FOREIGN KEY([PersonId])
REFERENCES [Person] ([Id])

ALTER TABLE [PersonsToServers]
ADD CONSTRAINT [FK_PersonsToServers_Person] FOREIGN KEY([PersonId])
REFERENCES [Person] ([Id])

ALTER TABLE [PersonsToServers]
ADD CONSTRAINT [FK_PersonsToServers_Server] FOREIGN KEY([ServerId])
REFERENCES [Server] ([Id])



INSERT [dbo].[Server] ([Id], [Name], [IPWhitelist]) VALUES (N'c577bf57-665a-4e8a-88d5-a76000aea108', N'DEV-001', N'["127.0.0.1"]')
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [AddressLine1], [AddressLine2], [TownCity], [County], [PostCode], [Type], [DateCreated], [LastUpdated]) VALUES (N'b50f15b4-b8e8-448c-9525-a76000aea108', N'Johnny', N'McJohn', N'25 Test St', NULL, N'Leeds', N'West Yorkshire', N'LS1 2BC', NULL, CAST(N'2017-04-25 10:35:48.000' AS DateTime), NULL)
INSERT [dbo].[PersonsToServers] ([PersonId], [ServerId]) VALUES (N'b50f15b4-b8e8-448c-9525-a76000aea108', N'c577bf57-665a-4e8a-88d5-a76000aea108')
INSERT [dbo].[Login] ([LoginId], [PersonId], [IP], [DateLoggedIn]) VALUES (N'16066ce2-28d3-4d11-93ad-a76000aea108', N'b50f15b4-b8e8-448c-9525-a76000aea108', N'127.0.0.1', CAST(N'2017-04-25 10:35:48.000' AS DateTime))
