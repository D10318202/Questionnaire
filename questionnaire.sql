USE [questionnaire]
GO
/****** Object:  Table [dbo].[AccountInfo]    Script Date: 2022/4/18 下午 01:55:15 ******/
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
	[QuestionOfCheck] [nvarchar](50) NOT NULL,
	[QuestionOfRadio] [nvarchar](50) NOT NULL,
	[QuestionOfText] [nvarchar](50) NOT NULL,
	[AnswerOfCheck] [nvarchar](50) NOT NULL,
	[AnswerOfRadio] [nvarchar](50) NOT NULL,
	[AnswerOfText] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AccountInfo] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MainQues]    Script Date: 2022/4/18 下午 01:55:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MainQues](
	[quesID] [uniqueidentifier] NOT NULL,
	[quesTitle] [nvarchar](150) NOT NULL,
	[quesBody] [nvarchar](250) NULL,
	[quesstart] [datetime] NOT NULL,
	[quesend] [datetime] NOT NULL,
	[quesstates] [bit] NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[quesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionsDetail]    Script Date: 2022/4/18 下午 01:55:15 ******/
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
	[quesDetailMustKeyIn] [char](1) NOT NULL,
 CONSTRAINT [PK_QuestionsDetail] PRIMARY KEY CLUSTERED 
(
	[quesDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionsDetail2]    Script Date: 2022/4/18 下午 01:55:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionsDetail2](
	[quesID] [uniqueidentifier] NOT NULL,
	[quesDetailID] [uniqueidentifier] NOT NULL,
	[quesDetail2ID] [uniqueidentifier] NOT NULL,
	[answer] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_QuestionsDetail2] PRIMARY KEY CLUSTERED 
(
	[quesDetail2ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AccountInfo] ADD  CONSTRAINT [DF_AccountInfo_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[MainQues] ADD  CONSTRAINT [DF_Questions_quesID]  DEFAULT (newid()) FOR [quesID]
GO
ALTER TABLE [dbo].[QuestionsDetail] ADD  CONSTRAINT [DF_QuestionsDetail_quesDetailID]  DEFAULT (newid()) FOR [quesDetailID]
GO
ALTER TABLE [dbo].[QuestionsDetail2] ADD  CONSTRAINT [DF_QuestionsDetail2_quesDetail2ID]  DEFAULT (newid()) FOR [quesDetail2ID]
GO
