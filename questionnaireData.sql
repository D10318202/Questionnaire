USE [questionnaire]
GO
INSERT [dbo].[MainQues] ([quesID], [quesTitle], [quesBody], [quesstart], [quesend], [quesstates], [CreateTime]) VALUES (N'ade8d082-eaea-464d-af72-23bfdc67349f', N'東京去過哪裡', N'有沒有喜歡去日本東京遊玩的朋友啊', CAST(N'2022-04-23T12:51:00.000' AS DateTime), CAST(N'2022-04-30T12:51:00.000' AS DateTime), 1, CAST(N'2022-04-23T12:51:54.017' AS DateTime))
GO
INSERT [dbo].[QuestionsDetail] ([quesID], [quesDetailID], [quesDetailTitle], [quesDetailBody], [quesDetailType], [quesDetailMustKeyIn]) VALUES (N'477a69eb-5efc-425c-bcde-5d7405abf1f8', N'fb9bc82c-c79a-4daa-a829-0328992437d5', N'涉谷區哪裡好玩', N'涉谷,中目黑,代官山', 1, 1)
INSERT [dbo].[QuestionsDetail] ([quesID], [quesDetailID], [quesDetailTitle], [quesDetailBody], [quesDetailType], [quesDetailMustKeyIn]) VALUES (N'dff000ff-3a74-4f5c-ba82-8284a579deaf', N'50de191a-1daa-4ae4-923a-75b003616c2f', N'涉谷區哪裡好玩', N'涉谷,中目黑,代官山', 1, 1)
INSERT [dbo].[QuestionsDetail] ([quesID], [quesDetailID], [quesDetailTitle], [quesDetailBody], [quesDetailType], [quesDetailMustKeyIn]) VALUES (N'ade8d082-eaea-464d-af72-23bfdc67349f', N'c98d8ab2-79f2-454d-ae8d-9f26e4347bf4', N'涉谷區哪裡好玩', N'涉谷,中目黑,代官山', 1, 1)
INSERT [dbo].[QuestionsDetail] ([quesID], [quesDetailID], [quesDetailTitle], [quesDetailBody], [quesDetailType], [quesDetailMustKeyIn]) VALUES (N'235231ef-998b-44c8-9d79-f3c525a2971f', N'7dcbb752-b4c0-4939-8302-c10170d55c86', N'涉谷區哪裡好玩', N'涉谷,中目黑,代官山', 1, 1)
GO
