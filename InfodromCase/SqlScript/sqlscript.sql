USE [InfodromCase]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 4/16/2023 11:46:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 4/16/2023 11:46:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationNumber] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[DepartmentId] [int] NOT NULL,
 CONSTRAINT [staf] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Id], [Name], [ParentId]) VALUES (1, N'Genel Müdür', NULL)
INSERT [dbo].[Department] ([Id], [Name], [ParentId]) VALUES (2, N'Yazılım Departman Sorumlusu', 1)
INSERT [dbo].[Department] ([Id], [Name], [ParentId]) VALUES (3, N'Proje Departman Sorumlusu', 1)
INSERT [dbo].[Department] ([Id], [Name], [ParentId]) VALUES (4, N'Yazılım Uzmanı', 2)
INSERT [dbo].[Department] ([Id], [Name], [ParentId]) VALUES (5, N'Departman Uzmanı', 3)
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Staff] ON 

INSERT [dbo].[Staff] ([Id], [RegistrationNumber], [Name], [Surname], [DepartmentId]) VALUES (4, 1234568, N'Aydın', N'Yılmaz', 2)
INSERT [dbo].[Staff] ([Id], [RegistrationNumber], [Name], [Surname], [DepartmentId]) VALUES (6, 1234567, N'Ali', N'Mutlu', 4)
INSERT [dbo].[Staff] ([Id], [RegistrationNumber], [Name], [Surname], [DepartmentId]) VALUES (9, 23423423, N'Ertuğrul', N'Cetinkaya', 2)
INSERT [dbo].[Staff] ([Id], [RegistrationNumber], [Name], [Surname], [DepartmentId]) VALUES (10, 345345, N'Test', N'User', 4)
SET IDENTITY_INSERT [dbo].[Staff] OFF
GO
ALTER TABLE [dbo].[Department] ADD  CONSTRAINT [DF_Department_ParentId]  DEFAULT ((0)) FOR [ParentId]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Department] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Department]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Department]
GO
