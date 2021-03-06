USE [questionnaire]
GO
/****** Object:  Table [dbo].[AccountInfo]    Script Date: 2022/5/3 下午 05:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountInfo](
	[AccountID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Phone] [nchar](10) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Age] [nchar](5) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[quesID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_AccountInfo] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 2022/5/3 下午 05:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[AnswerID] [uniqueidentifier] NOT NULL,
	[quesID] [uniqueidentifier] NOT NULL,
	[AccountID] [uniqueidentifier] NOT NULL,
	[quesNumber] [int] NOT NULL,
	[Answer] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MainQues]    Script Date: 2022/5/3 下午 05:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MainQues](
	[quesID] [uniqueidentifier] NOT NULL,
	[quesTitle] [nvarchar](150) NOT NULL,
	[quesBody] [nvarchar](250) NULL,
	[quesstart] [datetime] NULL,
	[quesend] [datetime] NULL,
	[quesstates] [bit] NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsOftenUse] [bit] NOT NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[quesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionsDetail]    Script Date: 2022/5/3 下午 05:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionsDetail](
	[quesID] [uniqueidentifier] NOT NULL,
	[quesDetailID] [uniqueidentifier] NOT NULL,
	[quesDetailTitle] [nvarchar](50) NOT NULL,
	[quesDetailBody] [nvarchar](150) NULL,
	[quesDetailType] [int] NOT NULL,
	[quesDetailMustKeyIn] [bit] NOT NULL,
	[quesNumber] [int] NOT NULL,
 CONSTRAINT [PK_QuestionsDetail] PRIMARY KEY CLUSTERED 
(
	[quesDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AccountInfo] ADD  CONSTRAINT [DF_AccountInfo_AccountID]  DEFAULT (newid()) FOR [AccountID]
GO
ALTER TABLE [dbo].[AccountInfo] ADD  CONSTRAINT [DF_AccountInfo_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Answer] ADD  CONSTRAINT [DF_Answer_ID]  DEFAULT (newid()) FOR [AnswerID]
GO
ALTER TABLE [dbo].[MainQues] ADD  CONSTRAINT [DF_Questions_quesID]  DEFAULT (newid()) FOR [quesID]
GO
ALTER TABLE [dbo].[MainQues] ADD  CONSTRAINT [DF_MainQues_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[QuestionsDetail] ADD  CONSTRAINT [DF_QuestionsDetail_quesDetailID]  DEFAULT (newid()) FOR [quesDetailID]
GO
