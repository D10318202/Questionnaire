using QuestionHelpers;
using QuestionModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QuestionManagers
{
    public class QuestionnaireManager
    {
        public List<QuestionModel> GetIndexList(int PageIndex, int PageSize, List<QuestionModel> list)
        {
            int skip = PageSize * (PageIndex - 1); //計算跳頁
            if (skip < 0)
                skip = 0;

            return list.Skip(skip).Take(PageSize).ToList();
        }

        #region /*問卷* 常用問題=0; 不是常用問題=1;/
        /// <summary>
        /// 創建問卷
        /// </summary>
        /// <param name="question"></param>
        /// 
        public void CreateQuestionnaire(QuestionModel question)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [MainQues] 
                        (quesID, quesTitle, quesBody, quesstart, quesend, quesstates, IsOftenUse)
                    VALUES 
                        (@quesID, @quesTitle, @quesBody, @quesstart, @quesend, @quesstates, @IsOftenUse) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@quesID", question.quesID);
                        command.Parameters.AddWithValue("@quesTitle", question.quesTitle);
                        command.Parameters.AddWithValue("@quesBody", question.quesBody);
                        command.Parameters.AddWithValue("@quesstart", question.quesstart);
                        command.Parameters.AddWithValue("@quesend", question.quesend);
                        command.Parameters.AddWithValue("@quesstates", question.stateType);
                        command.Parameters.AddWithValue("@IsOftenUse", 1);  //常用問題=0; 不是常用問題=1;

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.CreateQuestionnaire", ex);
                throw;
            }
        }

        /// <summary>
        /// 編輯問卷
        /// </summary>
        /// <param name="question">問卷代號</param>
        public void UpdateQuestionnaire(QuestionModel question)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  UPDATE [MainQues]
                    SET quesTitle = @quesTitle, 
                        quesBody = @quesBody
                        quesstart = @quesstart 
                        quesend = @quesend
                        quesstates = @quesstates
                    WHERE quesID = @quesID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@quesID", question.quesID);
                        command.Parameters.AddWithValue("@quesTitle", question.quesTitle);
                        command.Parameters.AddWithValue("@quesBody", question.quesBody);
                        command.Parameters.AddWithValue("@quesstart", question.quesstart);
                        command.Parameters.AddWithValue("@quesend", question.quesend);
                        command.Parameters.AddWithValue("@quesstates", question.stateType);

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.UpdateQuestionnaire", ex);
                throw;
            }
        }

        /// <summary>
        /// 刪除問卷
        /// </summary>
        /// <param name="quesID">問卷代號</param>
        /// <returns></returns>
        public bool DeleteQuestionnaire(Guid quesID)
        {

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @" DELETE FROM MainQues 
                   WHERE quesID = @quesID";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        DeleteQuestion(quesID);
                        DeleteAnswer(quesID);
                        conn.Open();
                        command.Parameters.AddWithValue("@quesID", quesID);
                        command.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// 刪除問題
        /// </summary>
        /// <param name="quesID"></param>
        /// <returns></returns>
        public bool DeleteQuestion(Guid quesID)
        {

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @" DELETE FROM QuestionDetail 
                   WHERE quesID = @quesID";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@quesID", quesID);
                        command.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// 刪除答案
        /// </summary>
        /// <param name="quesID"></param>
        /// <returns></returns>
        public bool DeleteAnswer(Guid quesID)
        {

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @" DELETE FROM Answer 
                   WHERE quesID = @quesID";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@quesID", quesID);
                        command.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// 取得問卷
        /// </summary>
        /// <param name="quesID"></param>
        /// <returns></returns>
        public QuestionModel GetQuestionnaire(Guid quesID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [MainQues]
                     WHERE quesID = @quesID
                     ORDER BY [quesstart]";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@quesID", quesID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            QuestionModel question = new QuestionModel()
                            {
                                quesID = (Guid)reader["quesID"],
                                quesTitle = reader["quesTitle"] as string,
                                quesBody = reader["quesBody"] as string,
                                quesstart = (DateTime)reader["quesstart"],
                                quesend = (DateTime)reader["quesend"],
                                CreateTime = (DateTime)reader["CreateTime"],
                            };
                            return question;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(" QuestionnaireManager.GetQuestionnaire", ex);
                throw;
            }
        }

        /// <summary>
        /// 列出問卷詳細內容(列出題目內容資訊)
        /// </summary>
        /// <param name="quesID"></param>
        /// <returns></returns>
        public List<QuestionDetailModel> GetQuestionModel(Guid quesID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM QuestionsDetail
                    WHERE quesID = @quesID
                    ORDER BY quesNumber";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@quesID", quesID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<QuestionDetailModel> questionDetail = new List<QuestionDetailModel>();
                        while (reader.Read())
                        {
                            QuestionDetailModel questionDetails = new QuestionDetailModel()
                            {
                                quesID = (Guid)reader["quesID"],
                                quesDetailID = (Guid)reader["quesDetailID"],
                                quesDetailTitle = reader["quesDetailTitle"] as string,
                                quesDetailBody = reader["quesDetailBody"] as string,
                                quesDetailType = (QuestionType)reader["quesDetailType"],
                                quesDetailMustKeyIn = (bool)reader["quesDetailMustKeyIn"],
                                quesNumber = (int)reader["quesNumber"]
                            };
                            questionDetail.Add(questionDetails);
                        }
                        return questionDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetQuestionModel", ex);
                return null;
            }
        }

        /// <summary>
        /// 列出問卷內容資訊
        /// </summary>
        /// <returns></returns>
        public List<QuestionModel> GetQuestionnaireList()
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [MainQues]
                     WHERE quesstates = 1 AND IsOftenUse = 1
                     ORDER BY CreateTime DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<QuestionModel> Questionnairelist = new List<QuestionModel>();
                        while (reader.Read())
                        {
                            QuestionModel question = new QuestionModel()
                            {
                                quesID = (Guid)reader["quesID"],
                                quesTitle = reader["quesTitle"] as string,
                                quesBody = reader["quesBody"] as string,
                                quesstart = (DateTime)reader["quesstart"],
                                quesend = (DateTime)reader["quesend"],
                                CreateTime = (DateTime)reader["CreateTime"]
                            };
                            question.stateType = (question.quesend < DateTime.Now) ? StateType.關閉 : StateType.已啟用;
                            Questionnairelist.Add(question);
                        }
                        return Questionnairelist;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.GetQuestionnaireList", ex);
                throw;
            }
        }

        /// <summary>
        /// 列出問卷內容資訊(後台)
        /// </summary>
        /// <returns></returns>
        public List<QuestionModel> GetQuestionnaireBackadminList()
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [MainQues]
                     WHERE IsOftenUse = 1
                     ORDER BY CreateTime DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<QuestionModel> Questionnairelist = new List<QuestionModel>();
                        while (reader.Read())
                        {
                            QuestionModel question = new QuestionModel()
                            {
                                quesID = (Guid)reader["quesID"],
                                quesTitle = reader["quesTitle"] as string,
                                quesBody = reader["quesBody"] as string,
                                quesstart = (DateTime)reader["quesstart"],
                                quesend = (DateTime)reader["quesend"],
                                CreateTime = (DateTime)reader["CreateTime"]
                            };
                            question.stateType = (question.quesend < DateTime.Now) ? StateType.關閉 : StateType.已啟用;
                            Questionnairelist.Add(question);
                        }
                        return Questionnairelist;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.GetQuestionnaireBackadminList", ex);
                throw;
            }
        }

        /// <summary>
        /// 列出問卷內容資訊(搜尋功能後台)
        /// </summary>
        /// <returns></returns>
        public List<QuestionModel> GetQuestionnaireBackadminList(string keyword)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [MainQues]
                     WHERE quesTitle LIKE '%'+ @keyword+ '%' AND IsOftenUse = 1
                     ORDER BY CreateTime DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                            command.Parameters.AddWithValue("@keyword", keyword);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<QuestionModel> Questionnairelist = new List<QuestionModel>();
                        while (reader.Read())
                        {
                            QuestionModel question = new QuestionModel()
                            {
                                quesID = (Guid)reader["quesID"],
                                quesTitle = reader["quesTitle"] as string,
                                quesBody = reader["quesBody"] as string,
                                quesstart = (DateTime)reader["quesstart"],
                                quesend = (DateTime)reader["quesend"],
                                CreateTime = (DateTime)reader["CreateTime"]
                            };
                            question.stateType = (question.quesend < DateTime.Now) ? StateType.關閉 : StateType.已啟用;
                            Questionnairelist.Add(question);
                        }
                        return Questionnairelist;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.GetQuestionnaireBackadminList", ex);
                throw;
            }
        }

        /// <summary>
        /// 列出問卷內容資訊(搜尋功能)
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<QuestionModel> GetQuestionnaireList(string keyword)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [MainQues]
                     WHERE quesTitle LIKE '%'+ @keyword+ '%' AND quesstates = 1 AND IsOftenUse = 1
                     ORDER BY CreateTime DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                            command.Parameters.AddWithValue("@keyword", keyword);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<QuestionModel> question = new List<QuestionModel>();
                        while (reader.Read())
                        {
                            QuestionModel questionmo = new QuestionModel()
                            {
                                quesID = (Guid)reader["quesID"],
                                quesTitle = reader["quesTitle"] as string,
                                quesBody = reader["quesBody"] as string,
                                quesstart = (DateTime)reader["quesstart"],
                                quesend = (DateTime)reader["quesend"]
                            };
                            questionmo.stateType = (questionmo.quesend < DateTime.Now) ? StateType.關閉 : StateType.已啟用;
                            question.Add(questionmo);
                        }
                        return question;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(" QuestionnaireManager.GetQuestionnaireList", ex);
                throw;
            }
        }

        #endregion        

        #region /*問卷題目*/
        /// <summary>
        /// 創建問題
        /// </summary>
        /// <param name="questionDetail"></param>
        public void CreateQuestionDetail(QuestionDetailModel questionDetail)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [QuestionsDetail] 
                        (quesID, quesDetailID, quesDetailTitle, quesDetailBody, quesDetailType, quesDetailMustKeyIn, quesNumber)
                    VALUES 
                        (@quesID, @quesDetailID, @quesDetailTitle, @quesDetailBody, @quesDetailType, @quesDetailMustKeyIn, @quesNumber) ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@quesID", questionDetail.quesID);
                        command.Parameters.AddWithValue("@quesDetailID", questionDetail.quesDetailID);
                        command.Parameters.AddWithValue("@quesDetailTitle", questionDetail.quesDetailTitle);
                        command.Parameters.AddWithValue("@quesDetailBody", questionDetail.quesDetailBody);
                        command.Parameters.AddWithValue("@quesDetailType", questionDetail.quesDetailType);
                        command.Parameters.AddWithValue("@quesDetailMustKeyIn", questionDetail.quesDetailMustKeyIn);
                        command.Parameters.AddWithValue("@quesNumber", questionDetail.quesNumber);

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.CreateQuestionDetail", ex);
                throw;
            }
        }

        /// <summary>
        /// 編輯問題
        /// </summary>
        /// <param name="questionDetail"></param>
        public void UpdateQuestionDetail(QuestionDetailModel questionDetail)
        {

        }

        #endregion

        #region /*常用問題*/

        /// <summary>
        /// 建立常用問題
        /// </summary>
        /// <param name="quesID"></param>
        /// <param name="quesTitle"></param>
        public void CreateOftenUseExamp(Guid quesID, string quesTitle)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [MainQues] 
                        (quesID, quesTitle, IsOftenUse)
                    VALUES 
                        (@quesID, @quesTitle, @IsOftenUse) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@quesID", quesID);
                        command.Parameters.AddWithValue("@quesTitle", quesTitle);
                        command.Parameters.AddWithValue("@IsOftenUse", 0);  

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.CreateOftenUseExamp", ex);
                throw;
            }
        }

        /// <summary>
        /// 編輯常用問題
        /// </summary>
        /// <param name="quesID"></param>
        /// <param name="quesTitle"></param>
        public void UpdateOftenUseExamp(Guid quesID, string quesTitle)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  UPDATE[MainQues] 
                    SET  quesTitle = @quesTitle, 
                    WHERE  quesID = @quesID";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@quesID", quesID);
                        command.Parameters.AddWithValue("@quesTitle", quesTitle);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.UpdateOftenUseExamp", ex);
                throw;
            }
        }

        /// <summary>
        /// 取得常用問題
        /// </summary>
        /// <param name="quesID"></param>
        /// <returns></returns>
        public QuestionModel GetOftenUse(Guid quesID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [MainQues]
                     WHERE quesID = @quesID";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@quesID", quesID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        QuestionModel question = new QuestionModel();
                        if (reader.Read())
                        {
                            question.quesID = (Guid)reader["quesID"];
                            question.quesTitle = reader["quesTitle"] as string;
                            question.CreateTime = (DateTime)reader["CreateTime"];
                        }
                        return question;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(" QuestionnaireManager.GetOftenUse", ex);
                throw;
            }
        }
        #endregion

        #region 列出常用問題

        /// <summary>
        /// 列出常用問題
        /// </summary>
        /// <returns></returns>
        public List<QuestionModel> GetQuestionList()
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [MainQues]
                     WHERE IsOftenUse = 0
                     ORDER BY CreateTime DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<QuestionModel> Questionnairelist = new List<QuestionModel>();
                        while (reader.Read())
                        {

                            QuestionModel question = new QuestionModel()
                            {
                                quesID = (Guid)reader["quesID"],
                                quesTitle = reader["quesTitle"] as string,
                                CreateTime = (DateTime)reader["CreateTime"]
                            };
                            Questionnairelist.Add(question);
                        }
                        return Questionnairelist;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.GetQuestionList", ex);
                throw;
            }
        }

        /// <summary>
        /// 列出常用問題(搜尋功能)
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<QuestionModel> GetQuestionList(string keyword)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [MainQues]
                     WHERE quesTitle LIKE '%'+ @keyword+ '%' AND IsOftenUse = 0
                     ORDER BY CreateTime DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                            command.Parameters.AddWithValue("@keyword", keyword);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<QuestionModel> question = new List<QuestionModel>();
                        while (reader.Read())
                        {
                            QuestionModel questionmo = new QuestionModel()
                            {
                                quesID = (Guid)reader["quesID"],
                                quesTitle = reader["quesTitle"] as string,
                                CreateTime = (DateTime)reader["CreateTime"]
                            };
                            question.Add(questionmo);
                        }
                        return question;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(" QuestionnaireManager.GetQuestionList", ex);
                throw;
            }
        }

        #endregion

        #region 回答用

        /// <summary>
        /// 建立回答人
        /// </summary>
        /// <param name="account"></param>
        public void CreatePersonInfo(AccountInfoModel account)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [AccountInfo] 
                        (AccountID, Name, Phone, Email, Age,CreateTime,quesID)
                    VALUES 
                        (@AccountID, @Name, @Phone, @Email, @Age, @CreateTime, @quesID) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@quesID", account.quesID);
                        command.Parameters.AddWithValue("@AccountID", account.AccountID);
                        command.Parameters.AddWithValue("@Name", account.Name);
                        command.Parameters.AddWithValue("@Phone", account.Phone);
                        command.Parameters.AddWithValue("@Email", account.Email);
                        command.Parameters.AddWithValue("@Age", account.Age);
                        command.Parameters.AddWithValue("@CreateTime", account.CreateTime);

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.CreatePersonInfo", ex);
                throw;
            }
        }

        /// <summary>
        /// 建立答案
        /// </summary>
        /// <param name="account"></param>
        public void CreateAnswer(QuestionAnswerModel answer)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [AccountInfo] 
                        (AnswerID, quesID, AccountID, quesNumber, Answer)
                    VALUES 
                        (@AnswerID, @quesID, @AccountID, @quesNumber, @Answer) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@AnswerID", answer.AnswerID);
                        command.Parameters.AddWithValue("@quesID", answer.quesID);
                        command.Parameters.AddWithValue("@AccountID", answer.AccountID);
                        command.Parameters.AddWithValue("@quesNumber", answer.quesNumber);
                        command.Parameters.AddWithValue("@Answer", answer.Answer);

                        conn.Open();
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnaireManager.CreateAnswer", ex);
                throw;
            }
        }

        /// <summary>
        /// 取得回答人列表
        /// </summary>
        /// <param name="quesID"></param>
        /// <returns></returns>
        public List<AccountInfoModel> GetPersonInfoList(Guid quesID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM AccountInfo
                    WHERE quesID = @quesID
                    ORDER BY CreateTime DESC";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@quesID", quesID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<AccountInfoModel> accountInfo = new List<AccountInfoModel>();
                        while (reader.Read())
                        {
                            AccountInfoModel Account = new AccountInfoModel()
                            {
                                quesID = (Guid)reader["quesID"],
                                AccountID = (Guid)reader["AccountID"],
                                Name = reader["Name"] as string,
                                Age = reader["Age"] as string,
                                Email = reader["Email"] as string,
                                Phone = reader["Phone"] as string,
                                CreateTime = (DateTime)reader["CreateTime"]
                            };
                            accountInfo.Add(Account);
                        }
                        return accountInfo;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetPersonInfoList", ex);
                throw;
            }
        }

        /// <summary>
        /// 取得答案
        /// </summary>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        public List<QuestionAnswerModel> GetAnswerList(Guid AccountID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Answer]
                     WHERE AccountID = @AccountID 
                     ORDER BY QuestionNo, Answer ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@AccountID", AccountID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<QuestionAnswerModel> answerList = new List<QuestionAnswerModel>();
                        while (reader.Read())
                        {
                            QuestionAnswerModel answer = new QuestionAnswerModel()
                            {
                                quesNumber = Convert.ToInt32(reader["[quesNumber"]),
                                Answer = reader["Answer"] as string
                            };
                            answerList.Add(answer);
                        }
                        return answerList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetAnswerList", ex);
                throw;
            }
        }

        /// <summary>
        /// 取得回答人
        /// </summary>
        /// <param name="quesID"></param>
        /// <returns></returns>
        public AccountInfoModel GetPersonInfo(Guid AccountID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM AccountInfo
                    WHERE AccountID = @AccountID";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@AccountID", AccountID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        AccountInfoModel Account = new AccountInfoModel();
                        if (reader.Read())
                        {
                            Account.quesID = (Guid)reader["quesID"];
                            Account.AccountID = (Guid)reader["AccountID"];
                            Account.Name = reader["Name"] as string;
                            Account.Age = reader["Age"] as string;
                            Account.Email = reader["Email"] as string;
                            Account.Phone = reader["Phone"] as string;
                            Account.CreateTime = (DateTime)reader["CreateTime"];
                        }
                        return Account;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetPersonInfo", ex);
                throw;
            }
        }

        public List<QuestionTotalModel> GetTotalAnswerList(Guid quesID)
        {

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT quesNumber, Answer, COUNT(quesID) AS AnsCount
                     FROM [Answer]
                     WHERE quesID = @quesID 
                     GROUP BY quesNumber, Answer, quesID
                     ORDER BY quesNumber, Answer ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@quesID", quesID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<QuestionTotalModel> totalList = new List<QuestionTotalModel>();
                        while (reader.Read())
                        {
                            QuestionTotalModel total = new QuestionTotalModel()
                            {
                                quesNumber = Convert.ToInt32(reader["[quesNumber"]),
                                Answer = reader["Answer"] as string,
                                AnsCount = (int)reader["AnsCount"]
                            };
                            totalList.Add(total);
                        }
                        return totalList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetTotalAnswerList", ex);
                throw;
            }
        }

        #endregion
    }
}