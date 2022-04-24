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
    public class QuestionAnswerHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (string.Compare("POST", context.Request.HttpMethod) == 0 &&
                 Guid.TryParse(context.Request.QueryString["quesID"], out Guid quesID))
            {
                string name = context.Request.Form["Name"];
                string phone = context.Request.Form["Phone"];
                string email = context.Request.Form["Email"];
                string age = context.Request.Form["Age"];

                AccountInfoModel accountInfoModel = new AccountInfoModel()
                {
                    AccountID = Guid.NewGuid(),
                    Name = name,
                    Phone = phone,
                    Age = age,
                    Email = email,
                    quesID = quesID,
                };
                HttpContext.Current.Session["questionanswer"] = accountInfoModel;
                string quesans = context.Request.Form["Answer"];
                if (string.IsNullOrWhiteSpace(quesans))
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("noAnswer");
                    return;
                }

                string[] AnswerArr = quesans.Trim().Split(' ');
                List<QuestionAnswerModel> questionAnswers = new List<QuestionAnswerModel>();
                foreach (string item in AnswerArr)
                {
                    string[] answer = item.Split(' ');

                    QuestionAnswerModel questionAnswerModel = new QuestionAnswerModel()
                    {
                        AccountID = Guid.NewGuid(),
                        quesID = quesID,
                        quesNumber = Convert.ToInt32(answer[0].Replace('Q', '0')),
                        Answer = AnswerArr[1],
                    };
                    questionAnswers.Add(questionAnswerModel);
                }
                HttpContext.Current.Session["peopleAnswer"] = questionAnswers;
                context.Response.ContentType = "text/plain";
                context.Response.Write("Questionnaire Complete");
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