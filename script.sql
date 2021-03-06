USE [AdmissionsDB]
GO
/****** Object:  Table [dbo].[AdmissionForm]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdmissionForm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Method] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Counselor]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Counselor](
	[Id] [int] NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Avatar] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Counselor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[District]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[District](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Major]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Major](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OldSchool]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OldSchool](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_OldSchool] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rate]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rate] [int] NULL,
	[TalkshowId] [int] NULL,
	[StudentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](max) NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slot]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slot](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Price] [int] NOT NULL,
	[TalkshowId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
 CONSTRAINT [PK_Slot] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Avatar] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[DOB] [date] NULL,
	[OldSchoolId] [int] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Talkshow]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Talkshow](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[urlMeet] [varchar](max) NOT NULL,
	[Price] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[isFinish] [bit] NOT NULL,
	[isCancel] [bit] NOT NULL,
	[isApprove] [bit] NOT NULL,
	[isBanner] [bit] NOT NULL,
	[CounselorId] [int] NOT NULL,
	[MajorId] [int] NULL,
	[UniversityId] [int] NULL,
 CONSTRAINT [PK__Talkshow__3214EC07077715C5] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Desciption] [nvarchar](max) NOT NULL,
	[WalletId] [int] NOT NULL,
 CONSTRAINT [PK__Transact__3214EC07B29B020F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UniAddress]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UniAddress](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[DistrictId] [int] NULL,
	[UniversityId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UniAdmission]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UniAdmission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UniversityId] [int] NOT NULL,
	[AdmissionId] [int] NOT NULL,
 CONSTRAINT [PK_UniAdmission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UniImage]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UniImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Src] [nvarchar](max) NULL,
	[Alt] [nvarchar](max) NULL,
	[isLogo] [bit] NULL,
	[UniversityId] [int] NULL,
 CONSTRAINT [PK__UniImage__3214EC0710416C31] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UniMajor]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UniMajor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UniversityId] [int] NULL,
	[MajorId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[University]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[University](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Email] [varchar](max) NULL,
	[Facebook] [varchar](max) NULL,
	[Website] [varchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[LastYearBenchmark] [decimal](18, 0) NULL,
	[MinFee] [decimal](18, 0) NULL,
	[MaxFee] [decimal](18, 0) NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](max) NULL,
	[isActive] [bit] NULL,
	[RoleId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wallet]    Script Date: 11/7/2021 12:04:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wallet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
 CONSTRAINT [PK_Wallet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AdmissionForm] ON 

INSERT [dbo].[AdmissionForm] ([Id], [Method], [Description]) VALUES (1, N'Nộp học bạ', N'Xét tuyển trên điểm cấp 3')
INSERT [dbo].[AdmissionForm] ([Id], [Method], [Description]) VALUES (2, N'Tuyển sinh trực tiếp', N'Xét tuyển bằng kết quả kì thi trung học phổ thông quốc gia')
INSERT [dbo].[AdmissionForm] ([Id], [Method], [Description]) VALUES (3, N'Thi đánh giá', N'Xét tuyển bằng bài thi đánh giá của trường')
SET IDENTITY_INSERT [dbo].[AdmissionForm] OFF
GO
INSERT [dbo].[Counselor] ([Id], [FullName], [Phone], [Avatar], [Description]) VALUES (29, N'Counselor', NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[District] ON 

INSERT [dbo].[District] ([Id], [Name]) VALUES (1, N'Quận 1')
INSERT [dbo].[District] ([Id], [Name]) VALUES (2, N'Quận 2')
INSERT [dbo].[District] ([Id], [Name]) VALUES (3, N'Quận 3')
INSERT [dbo].[District] ([Id], [Name]) VALUES (4, N'Quận 4')
INSERT [dbo].[District] ([Id], [Name]) VALUES (5, N'Quận 5')
INSERT [dbo].[District] ([Id], [Name]) VALUES (6, N'Quận 6')
INSERT [dbo].[District] ([Id], [Name]) VALUES (7, N'Quận 7')
INSERT [dbo].[District] ([Id], [Name]) VALUES (8, N'Quận 8')
INSERT [dbo].[District] ([Id], [Name]) VALUES (9, N'Quận 9')
INSERT [dbo].[District] ([Id], [Name]) VALUES (10, N'Quận 10')
INSERT [dbo].[District] ([Id], [Name]) VALUES (11, N'Quận 11')
INSERT [dbo].[District] ([Id], [Name]) VALUES (12, N'Quận 12')
INSERT [dbo].[District] ([Id], [Name]) VALUES (13, N'Quận Gò Vấp')
INSERT [dbo].[District] ([Id], [Name]) VALUES (14, N'Quận Tân Bình')
INSERT [dbo].[District] ([Id], [Name]) VALUES (15, N'Quận Bình Thạnh')
INSERT [dbo].[District] ([Id], [Name]) VALUES (16, N'Quận Bình Tân')
SET IDENTITY_INSERT [dbo].[District] OFF
GO
SET IDENTITY_INSERT [dbo].[Major] ON 

INSERT [dbo].[Major] ([Id], [Name], [Description]) VALUES (1, N'Kĩ thuật phần mềm', NULL)
INSERT [dbo].[Major] ([Id], [Name], [Description]) VALUES (2, N'AI', NULL)
INSERT [dbo].[Major] ([Id], [Name], [Description]) VALUES (3, N'An toàn thông tin', NULL)
INSERT [dbo].[Major] ([Id], [Name], [Description]) VALUES (4, N'Kinh doanh quốc tế', NULL)
INSERT [dbo].[Major] ([Id], [Name], [Description]) VALUES (5, N'Quản trị du lịch', NULL)
INSERT [dbo].[Major] ([Id], [Name], [Description]) VALUES (6, N'Quản trị khách sạn', NULL)
INSERT [dbo].[Major] ([Id], [Name], [Description]) VALUES (7, N'Ngôn ngữ Anh', NULL)
INSERT [dbo].[Major] ([Id], [Name], [Description]) VALUES (8, N'Ngôn ngữ Hàn', NULL)
INSERT [dbo].[Major] ([Id], [Name], [Description]) VALUES (9, N'Ngôn ngữ Nhật', NULL)
INSERT [dbo].[Major] ([Id], [Name], [Description]) VALUES (10, N'Ngôn ngữ Trung', NULL)
INSERT [dbo].[Major] ([Id], [Name], [Description]) VALUES (11, N'Cơ điện tử', NULL)
INSERT [dbo].[Major] ([Id], [Name], [Description]) VALUES (12, N'Cơ khí', NULL)
INSERT [dbo].[Major] ([Id], [Name], [Description]) VALUES (13, N'Ô tô', NULL)
SET IDENTITY_INSERT [dbo].[Major] OFF
GO
SET IDENTITY_INSERT [dbo].[OldSchool] ON 

INSERT [dbo].[OldSchool] ([Id], [Name]) VALUES (1, N'Quốc Học Quy Nhơn')
INSERT [dbo].[OldSchool] ([Id], [Name]) VALUES (2, N'Lê Quy Đôn Quy Nhơn')
SET IDENTITY_INSERT [dbo].[OldSchool] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [RoleName], [Description]) VALUES (1, N'Admin', NULL)
INSERT [dbo].[Role] ([Id], [RoleName], [Description]) VALUES (2, N'Counselor', NULL)
INSERT [dbo].[Role] ([Id], [RoleName], [Description]) VALUES (3, N'Student', NULL)
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Slot] ON 

INSERT [dbo].[Slot] ([Id], [Price], [TalkshowId], [StudentId]) VALUES (25, 20, 2, 33)
INSERT [dbo].[Slot] ([Id], [Price], [TalkshowId], [StudentId]) VALUES (26, 10, 3, 33)
SET IDENTITY_INSERT [dbo].[Slot] OFF
GO
INSERT [dbo].[Student] ([Id], [FullName], [Phone], [Avatar], [Address], [DOB], [OldSchoolId]) VALUES (33, N'Student', N'string', N'string', N'string', CAST(N'2021-11-07' AS Date), 1)
GO
SET IDENTITY_INSERT [dbo].[Talkshow] ON 

INSERT [dbo].[Talkshow] ([Id], [Description], [Image], [urlMeet], [Price], [CreatedDate], [StartDate], [isFinish], [isCancel], [isApprove], [isBanner], [CounselorId], [MajorId], [UniversityId]) VALUES (1, N'string', N'string', N'string', 10, CAST(N'2021-11-06T05:28:32.430' AS DateTime), CAST(N'2021-11-06T06:26:37.263' AS DateTime), 0, 0, 0, 0, 29, NULL, NULL)
INSERT [dbo].[Talkshow] ([Id], [Description], [Image], [urlMeet], [Price], [CreatedDate], [StartDate], [isFinish], [isCancel], [isApprove], [isBanner], [CounselorId], [MajorId], [UniversityId]) VALUES (2, N'string', N'string', N'string', 20, CAST(N'2021-11-06T05:45:59.723' AS DateTime), CAST(N'2021-11-06T08:45:34.687' AS DateTime), 0, 0, 1, 0, 29, NULL, NULL)
INSERT [dbo].[Talkshow] ([Id], [Description], [Image], [urlMeet], [Price], [CreatedDate], [StartDate], [isFinish], [isCancel], [isApprove], [isBanner], [CounselorId], [MajorId], [UniversityId]) VALUES (3, N'string', N'string', N'string', 10, CAST(N'2021-11-07T07:04:39.910' AS DateTime), CAST(N'2021-11-07T08:03:43.943' AS DateTime), 1, 0, 1, 0, 29, 1, 1)
INSERT [dbo].[Talkshow] ([Id], [Description], [Image], [urlMeet], [Price], [CreatedDate], [StartDate], [isFinish], [isCancel], [isApprove], [isBanner], [CounselorId], [MajorId], [UniversityId]) VALUES (4, N'string', N'string', N'string', 10, CAST(N'2021-11-07T07:05:55.590' AS DateTime), CAST(N'2021-11-07T08:03:43.943' AS DateTime), 0, 1, 1, 0, 29, 1, 1)
SET IDENTITY_INSERT [dbo].[Talkshow] OFF
GO
SET IDENTITY_INSERT [dbo].[Transaction] ON 

INSERT [dbo].[Transaction] ([Id], [Amount], [CreatedDate], [Desciption], [WalletId]) VALUES (41, -10, CAST(N'2021-11-06T05:30:35.430' AS DateTime), N'Booking talkshow of Counselor', 5)
INSERT [dbo].[Transaction] ([Id], [Amount], [CreatedDate], [Desciption], [WalletId]) VALUES (42, -20, CAST(N'2021-11-07T07:22:18.617' AS DateTime), N'Booking talkshow of Counselor', 5)
INSERT [dbo].[Transaction] ([Id], [Amount], [CreatedDate], [Desciption], [WalletId]) VALUES (43, -10, CAST(N'2021-11-07T07:22:57.160' AS DateTime), N'Booking talkshow of Counselor', 5)
INSERT [dbo].[Transaction] ([Id], [Amount], [CreatedDate], [Desciption], [WalletId]) VALUES (44, -10, CAST(N'2021-11-07T07:23:04.050' AS DateTime), N'Booking talkshow of Counselor', 5)
INSERT [dbo].[Transaction] ([Id], [Amount], [CreatedDate], [Desciption], [WalletId]) VALUES (45, 10, CAST(N'2021-11-07T08:00:26.280' AS DateTime), N'Cancel talkshow of Counselor', 5)
INSERT [dbo].[Transaction] ([Id], [Amount], [CreatedDate], [Desciption], [WalletId]) VALUES (46, 10, CAST(N'2021-11-07T08:02:59.800' AS DateTime), N'Talkshow of Counselor has been canceled', 5)
SET IDENTITY_INSERT [dbo].[Transaction] OFF
GO
SET IDENTITY_INSERT [dbo].[UniAddress] ON 

INSERT [dbo].[UniAddress] ([Id], [Address], [DistrictId], [UniversityId]) VALUES (1, N'36B đường D1 Khu công nghệ cao', 9, 1)
INSERT [dbo].[UniAddress] ([Id], [Address], [DistrictId], [UniversityId]) VALUES (2, N'232 quang trung Khu chế xuất Tân Tạo', 7, 2)
INSERT [dbo].[UniAddress] ([Id], [Address], [DistrictId], [UniversityId]) VALUES (3, N'1 Đ. Võ Văn Ngân, Linh Chiểu', 9, 3)
INSERT [dbo].[UniAddress] ([Id], [Address], [DistrictId], [UniversityId]) VALUES (4, N'475A Điện Biên Phủ, Phường 25,', 15, 4)
INSERT [dbo].[UniAddress] ([Id], [Address], [DistrictId], [UniversityId]) VALUES (5, N'23A Bình Trưng', 2, 5)
INSERT [dbo].[UniAddress] ([Id], [Address], [DistrictId], [UniversityId]) VALUES (6, N'3 Hoàng Việt, Phường 4', 15, 6)
INSERT [dbo].[UniAddress] ([Id], [Address], [DistrictId], [UniversityId]) VALUES (7, N'45 Nguyễn Khắc Nhu, Phường Cô Giang', 1, 7)
SET IDENTITY_INSERT [dbo].[UniAddress] OFF
GO
SET IDENTITY_INSERT [dbo].[UniAdmission] ON 

INSERT [dbo].[UniAdmission] ([Id], [UniversityId], [AdmissionId]) VALUES (1, 1, 1)
INSERT [dbo].[UniAdmission] ([Id], [UniversityId], [AdmissionId]) VALUES (2, 1, 2)
SET IDENTITY_INSERT [dbo].[UniAdmission] OFF
GO
SET IDENTITY_INSERT [dbo].[UniImage] ON 

INSERT [dbo].[UniImage] ([Id], [Src], [Alt], [isLogo], [UniversityId]) VALUES (1, N'https://media.publit.io/file/truong-dai-hoc-fpt-tp-hcm-1.jpg', N'img', 0, 1)
INSERT [dbo].[UniImage] ([Id], [Src], [Alt], [isLogo], [UniversityId]) VALUES (2, N'https://media.publit.io/file/hongban.png', NULL, 0, 6)
INSERT [dbo].[UniImage] ([Id], [Src], [Alt], [isLogo], [UniversityId]) VALUES (3, N'https://media.publit.io/file/HUTECH-2.jpg', NULL, 0, 4)
INSERT [dbo].[UniImage] ([Id], [Src], [Alt], [isLogo], [UniversityId]) VALUES (4, N'https://media.publit.io/file/rmit-q.jpg', NULL, 0, 2)
INSERT [dbo].[UniImage] ([Id], [Src], [Alt], [isLogo], [UniversityId]) VALUES (5, N'https://media.publit.io/file/DH-Su-pham-Ky-thuat-TP.HCM-chao-mung-90-nam-Ngay-thanh-lap-Dang.jpg', NULL, 0, 3)
INSERT [dbo].[UniImage] ([Id], [Src], [Alt], [isLogo], [UniversityId]) VALUES (6, N'https://media.publit.io/file/dh-van-lang-7411.jpg', NULL, 0, 7)
INSERT [dbo].[UniImage] ([Id], [Src], [Alt], [isLogo], [UniversityId]) VALUES (7, N'https://media.publit.io/file/neu.jpg', NULL, 0, 5)
SET IDENTITY_INSERT [dbo].[UniImage] OFF
GO
SET IDENTITY_INSERT [dbo].[UniMajor] ON 

INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (1, 1, 1)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (2, 1, 2)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (3, 1, 3)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (4, 1, 4)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (5, 1, 5)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (6, 1, 6)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (7, 1, 7)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (8, 1, 8)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (9, 1, 9)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (10, 2, 1)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (11, 2, 2)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (12, 2, 3)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (13, 2, 4)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (14, 2, 5)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (15, 2, 6)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (16, 2, 7)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (17, 3, 1)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (18, 3, 2)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (19, 3, 3)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (20, 3, 4)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (21, 3, 5)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (22, 3, 6)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (23, 3, 7)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (24, 4, 1)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (25, 4, 2)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (26, 4, 3)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (27, 4, 4)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (28, 4, 5)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (29, 4, 6)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (30, 5, 1)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (31, 5, 2)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (32, 5, 3)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (33, 5, 4)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (34, 5, 5)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (35, 5, 6)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (36, 6, 1)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (37, 6, 2)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (38, 6, 3)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (39, 6, 4)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (40, 6, 5)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (41, 6, 6)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (42, 6, 7)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (43, 6, 8)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (44, 7, 1)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (45, 7, 2)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (46, 7, 3)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (47, 7, 4)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (48, 7, 5)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (49, 7, 6)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (50, 7, 7)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (51, 7, 8)
INSERT [dbo].[UniMajor] ([Id], [UniversityId], [MajorId]) VALUES (52, 7, 9)
SET IDENTITY_INSERT [dbo].[UniMajor] OFF
GO
SET IDENTITY_INSERT [dbo].[University] ON 

INSERT [dbo].[University] ([Id], [Code], [Name], [Email], [Facebook], [Website], [Description], [LastYearBenchmark], [MinFee], [MaxFee], [isActive]) VALUES (1, N'FPT001', N'Đại học FPT', N'fpt@mail.fpt.com', N'www.facebook/fptuni.com.vn', N'www.fptuni.com', N'Đại học công nghệ', CAST(21 AS Decimal(18, 0)), CAST(23000000 AS Decimal(18, 0)), CAST(27000000 AS Decimal(18, 0)), 1)
INSERT [dbo].[University] ([Id], [Code], [Name], [Email], [Facebook], [Website], [Description], [LastYearBenchmark], [MinFee], [MaxFee], [isActive]) VALUES (2, N'RMIT', N'Đại học Rmit', N'rmit@mail.edu.vn', N'www.facebook/rmitedu.com.vn', N'www.rmit.com.vn', N'Đại học quốc tế', CAST(19 AS Decimal(18, 0)), CAST(15000000 AS Decimal(18, 0)), CAST(132000000 AS Decimal(18, 0)), 1)
INSERT [dbo].[University] ([Id], [Code], [Name], [Email], [Facebook], [Website], [Description], [LastYearBenchmark], [MinFee], [MaxFee], [isActive]) VALUES (3, N'UIT', N'Đại học sư phạm kĩ thuật', N'uit@mail.edu.vn', N'www.facebook/', N'www.uit.com.vn', N'Đại học ô tô', CAST(25 AS Decimal(18, 0)), CAST(23000000 AS Decimal(18, 0)), CAST(56000000 AS Decimal(18, 0)), 1)
INSERT [dbo].[University] ([Id], [Code], [Name], [Email], [Facebook], [Website], [Description], [LastYearBenchmark], [MinFee], [MaxFee], [isActive]) VALUES (4, N'HUTECH', N'Đại học Hutech', N'hutech@mail.edu.vn', N'www.facebook/hutech.com.vn', N'www.hutech.com.vn', N'Đại học về công nghệ', CAST(18 AS Decimal(18, 0)), CAST(200000000 AS Decimal(18, 0)), CAST(123000340 AS Decimal(18, 0)), 1)
INSERT [dbo].[University] ([Id], [Code], [Name], [Email], [Facebook], [Website], [Description], [LastYearBenchmark], [MinFee], [MaxFee], [isActive]) VALUES (5, N'NEU', N'Đại học kinh tế quốc dân', N'neu@mail.com.vn', N'www.facebook/neu.com.vn', N'www.neu.com.vn', N'Đại học về kinh tế', CAST(20 AS Decimal(18, 0)), CAST(25000000 AS Decimal(18, 0)), CAST(27000000 AS Decimal(18, 0)), 1)
INSERT [dbo].[University] ([Id], [Code], [Name], [Email], [Facebook], [Website], [Description], [LastYearBenchmark], [MinFee], [MaxFee], [isActive]) VALUES (6, N'HB', N'Đại học hồng bàng', N'hb@mail.com.vn', N'www.facebook/hb.com.vn', N'www.hb.com.vn', N'Đại học hồng bàn', CAST(20 AS Decimal(18, 0)), CAST(25000000 AS Decimal(18, 0)), CAST(27000000 AS Decimal(18, 0)), 1)
INSERT [dbo].[University] ([Id], [Code], [Name], [Email], [Facebook], [Website], [Description], [LastYearBenchmark], [MinFee], [MaxFee], [isActive]) VALUES (7, N'VLU', N'Đại học văn lang', N'vl@mail.com.vn', N'wwww.facebook/vlu.com.vn', N'www.vl.com.vn', N'Đại học về kinh tế,du lịch', CAST(21 AS Decimal(18, 0)), CAST(23000000 AS Decimal(18, 0)), CAST(25000000 AS Decimal(18, 0)), 1)
SET IDENTITY_INSERT [dbo].[University] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Email], [isActive], [RoleId]) VALUES (28, N'ADMIN', 1, 1)
INSERT [dbo].[User] ([Id], [Email], [isActive], [RoleId]) VALUES (29, N'COUNSELOR', 1, 2)
INSERT [dbo].[User] ([Id], [Email], [isActive], [RoleId]) VALUES (33, N'STUDENT', 1, 3)
INSERT [dbo].[User] ([Id], [Email], [isActive], [RoleId]) VALUES (34, N'tinltse140972@fpt.edu.vn', 1, 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Wallet] ON 

INSERT [dbo].[Wallet] ([Id], [Amount], [StudentId]) VALUES (5, 70, 33)
SET IDENTITY_INSERT [dbo].[Wallet] OFF
GO
ALTER TABLE [dbo].[Counselor]  WITH CHECK ADD  CONSTRAINT [FK_Counselor_User] FOREIGN KEY([Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Counselor] CHECK CONSTRAINT [FK_Counselor_User]
GO
ALTER TABLE [dbo].[Rate]  WITH CHECK ADD  CONSTRAINT [FK__Rate__Talkshow__5441852A] FOREIGN KEY([TalkshowId])
REFERENCES [dbo].[Talkshow] ([Id])
GO
ALTER TABLE [dbo].[Rate] CHECK CONSTRAINT [FK__Rate__Talkshow__5441852A]
GO
ALTER TABLE [dbo].[Rate]  WITH CHECK ADD  CONSTRAINT [FK_Rate_Student1] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[Rate] CHECK CONSTRAINT [FK_Rate_Student1]
GO
ALTER TABLE [dbo].[Slot]  WITH CHECK ADD  CONSTRAINT [FK_Slot_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[Slot] CHECK CONSTRAINT [FK_Slot_Student]
GO
ALTER TABLE [dbo].[Slot]  WITH CHECK ADD  CONSTRAINT [FK_Slot_Talkshow] FOREIGN KEY([TalkshowId])
REFERENCES [dbo].[Talkshow] ([Id])
GO
ALTER TABLE [dbo].[Slot] CHECK CONSTRAINT [FK_Slot_Talkshow]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_OldSchool] FOREIGN KEY([OldSchoolId])
REFERENCES [dbo].[OldSchool] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_OldSchool]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_User] FOREIGN KEY([Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_User]
GO
ALTER TABLE [dbo].[Talkshow]  WITH CHECK ADD  CONSTRAINT [FK_Talkshow_Counselor] FOREIGN KEY([CounselorId])
REFERENCES [dbo].[Counselor] ([Id])
GO
ALTER TABLE [dbo].[Talkshow] CHECK CONSTRAINT [FK_Talkshow_Counselor]
GO
ALTER TABLE [dbo].[Talkshow]  WITH CHECK ADD  CONSTRAINT [FK_Talkshow_Major] FOREIGN KEY([MajorId])
REFERENCES [dbo].[Major] ([Id])
GO
ALTER TABLE [dbo].[Talkshow] CHECK CONSTRAINT [FK_Talkshow_Major]
GO
ALTER TABLE [dbo].[Talkshow]  WITH CHECK ADD  CONSTRAINT [FK_Talkshow_University] FOREIGN KEY([UniversityId])
REFERENCES [dbo].[University] ([Id])
GO
ALTER TABLE [dbo].[Talkshow] CHECK CONSTRAINT [FK_Talkshow_University]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Wallet] FOREIGN KEY([WalletId])
REFERENCES [dbo].[Wallet] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Wallet]
GO
ALTER TABLE [dbo].[UniAddress]  WITH CHECK ADD FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([Id])
GO
ALTER TABLE [dbo].[UniAddress]  WITH CHECK ADD FOREIGN KEY([UniversityId])
REFERENCES [dbo].[University] ([Id])
GO
ALTER TABLE [dbo].[UniAdmission]  WITH CHECK ADD  CONSTRAINT [FK_UniAdmission_AdmissionForm] FOREIGN KEY([AdmissionId])
REFERENCES [dbo].[AdmissionForm] ([Id])
GO
ALTER TABLE [dbo].[UniAdmission] CHECK CONSTRAINT [FK_UniAdmission_AdmissionForm]
GO
ALTER TABLE [dbo].[UniAdmission]  WITH CHECK ADD  CONSTRAINT [FK_UniAdmission_University] FOREIGN KEY([UniversityId])
REFERENCES [dbo].[University] ([Id])
GO
ALTER TABLE [dbo].[UniAdmission] CHECK CONSTRAINT [FK_UniAdmission_University]
GO
ALTER TABLE [dbo].[UniImage]  WITH CHECK ADD  CONSTRAINT [FK__UniImage__Univer__4BAC3F29] FOREIGN KEY([UniversityId])
REFERENCES [dbo].[University] ([Id])
GO
ALTER TABLE [dbo].[UniImage] CHECK CONSTRAINT [FK__UniImage__Univer__4BAC3F29]
GO
ALTER TABLE [dbo].[UniMajor]  WITH CHECK ADD FOREIGN KEY([MajorId])
REFERENCES [dbo].[Major] ([Id])
GO
ALTER TABLE [dbo].[UniMajor]  WITH CHECK ADD FOREIGN KEY([UniversityId])
REFERENCES [dbo].[University] ([Id])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK__User__RoleId__4F7CD00D] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK__User__RoleId__4F7CD00D]
GO
ALTER TABLE [dbo].[Wallet]  WITH CHECK ADD  CONSTRAINT [FK_Wallet_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[Wallet] CHECK CONSTRAINT [FK_Wallet_Student]
GO
