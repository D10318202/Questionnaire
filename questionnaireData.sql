USE [questionnaire]
GO
INSERT [dbo].[MainQues] ([quesID], [quesTitle], [quesBody], [quesstart], [quesend], [quesstates], [CreateTime], [IsOftenUse]) VALUES (N'cafa914b-7b7d-450a-aa64-7f4a41fc4a48', N'東京哪裡好玩', NULL, NULL, NULL, NULL, CAST(N'2022-04-27T13:28:02.157' AS DateTime), 0)
INSERT [dbo].[MainQues] ([quesID], [quesTitle], [quesBody], [quesstart], [quesend], [quesstates], [CreateTime], [IsOftenUse]) VALUES (N'd96c9a8a-d8de-4f65-a93a-9b9d7105f26a', N'東京去過哪裡', N'有沒有喜歡去日本東京遊玩的朋友啊', CAST(N'2022-04-27T12:43:00.000' AS DateTime), CAST(N'2022-04-30T12:43:00.000' AS DateTime), 1, CAST(N'2022-04-27T12:43:28.070' AS DateTime), 1)
INSERT [dbo].[MainQues] ([quesID], [quesTitle], [quesBody], [quesstart], [quesend], [quesstates], [CreateTime], [IsOftenUse]) VALUES (N'c56c53bc-e0ea-4776-bd7c-9d0eba295da5', N'東京哪裡好玩', NULL, NULL, NULL, NULL, CAST(N'2022-04-27T12:59:43.967' AS DateTime), 0)
GO
INSERT [dbo].[QuestionsDetail] ([quesID], [quesDetailID], [quesDetailTitle], [quesDetailBody], [quesDetailType], [quesDetailMustKeyIn], [quesNumber]) VALUES (N'd96c9a8a-d8de-4f65-a93a-9b9d7105f26a', N'f02d4964-f86d-4aa6-bf2c-f3efd288f102', N'新宿區哪裡好玩', N'', 2, 1, 1)
GO
