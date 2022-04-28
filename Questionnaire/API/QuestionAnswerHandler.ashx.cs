using QuestionManagers;
using QuestionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.API
{
    /// <summary>
    /// QuestionAnswerHandler 的摘要描述
    /// </summary>
    public class QuestionAnswerHandler : IHttpHandler  ,System.Web.SessionState.IRequiresSessionState
    {
        private static QuestionnaireManager _quesMgr = new QuestionnaireManager();
        public void ProcessRequest(HttpContext context)
        {
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                 Guid.TryParse(context.Request.QueryString["quesID"], out Guid QuesID))
            {
                //string name = context.Request.Form["Name"];
                //string phone = context.Request.Form["Phone"];
                //string email = context.Request.Form["Email"];
                //string age = context.Request.Form["Age"];

                AccountInfoModel accountInfoModel = new AccountInfoModel()
                {
                    AccountID = Guid.NewGuid(),
                    Name = context.Request.Form["Name"],
                    Phone = context.Request.Form["Phone"],
                    Age = context.Request.Form["Age"],
                    Email = context.Request.Form["Email"],
                    quesID = QuesID
                };
                HttpContext.Current.Session["questionanswer"] = accountInfoModel;
                string quesans = context.Request.Form["Answer"];
                if (string.IsNullOrWhiteSpace(quesans))
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("noAnswer");
                    return;
                }

                string[] AnswerArr = quesans.Trim().Split(';');
                List<QuestionAnswerModel> questionAnswers = new List<QuestionAnswerModel>();
                foreach (string ans in AnswerArr)
                {
                    string[] answer = ans.Split('_');

                    QuestionAnswerModel questionAnswerModel = new QuestionAnswerModel()
                    {
                        AccountID = accountInfoModel.AccountID,
                        quesID = QuesID,
                        quesNumber = Convert.ToInt32(answer[0].Replace('Q', '0')),
                        Answer = answer[1],
                    };
                    questionAnswers.Add(questionAnswerModel);
                }
                HttpContext.Current.Session["peopleAnswer"] = questionAnswers;
                context.Response.ContentType = "text/plain";
                context.Response.Write("success");
                return;
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}