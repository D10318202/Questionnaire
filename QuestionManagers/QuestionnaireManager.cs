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
        #region /*問卷*/
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
                        (quesID, quesTitle, quesBody, quesstart, quesend,quesstates)
                    VALUES 
                        (@quesID, @quesTitle, @quesBody, @quesstart, @quesend, @quesstates) ";
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
                     ORDER BY [quesstart]";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
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
                                quesend = (DateTime)reader["quesend"]
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
            List<QuestionDetailModel> list = new List<QuestionDetailModel>();

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM QuestionsDetail
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
                        while (reader.Read())
                        {
                            QuestionDetailModel questionDetailModel = new QuestionDetailModel()
                            {
                                quesID = (Guid)reader["quesID"],
                                quesDetailID = (Guid)reader["quesDetailID"],
                                quesDetailTitle = reader["quesDetailTitle"] as string,
                                quesDetailBody = reader["quesDetailBody"] as string,
                                //quesDetailType = reader["quesDetailType"] as string,
                                //quesDetailMustKeyIn = (bool)reader["quesDetailMustKeyIn"], //要更改
                            };
                            list.Add(questionDetailModel);
                        }
                        return list;
                    }
                }
            }
            catch (Exception ex)
            {
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
                     WHERE quesstates = 1
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
                     WHERE quesTitle LIKE '%'+ @keyword+ '%' AND quesstates = 1
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
                        (quesID, quesDetailID, quesDetailTitle, quesDetailBody, quesDetailType, quesDetailMustKeyIn)
                    VALUES 
                        (@quesID, @quesDetailID, @quesDetailTitle, @quesDetailBody, @quesDetailType, @quesDetailMustKeyIn) ";

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
                        (quesID, quesTitle, quesstates)
                    VALUES 
                        (@quesID, @quesTitle, @quesstates) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@quesID", quesID);
                        command.Parameters.AddWithValue("@quesTitle", quesTitle);
                        command.Parameters.AddWithValue("@quesstates", 1);

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
                        command.Parameters.AddWithValue("@quesID", quesID);
                        command.Parameters.AddWithValue("@quesTitle", quesTitle);

                        conn.Open();
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
                     ORDER BY [quesstart]";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        QuestionModel question = new QuestionModel();
                        if(reader.Read())
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
                     WHERE quesstates = 1
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
                     WHERE quesTitle LIKE '%'+ @keyword+ '%' AND quesstates = 1
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

    }
}