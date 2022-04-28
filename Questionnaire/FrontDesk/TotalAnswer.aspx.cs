using QuestionManagers;
using QuestionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Questionnaire.FrontDesk
{
    public partial class TotalAnswer : System.Web.UI.Page
    {
        public static QuestionnaireManager _quesMgr = new QuestionnaireManager();
        public static Guid _questionnaireID;
        protected void Page_Load(object sender, EventArgs e)
        {
            string QusetionnaireID = Request.QueryString["quesID"];
            if(Guid.TryParse(QusetionnaireID, out _questionnaireID))
            {
                QuestionModel question = _quesMgr.GetQuestionnaire(_questionnaireID);
                this.ltlTitle.Text = question.quesTitle;
                this.ltlBody.Text = question.quesBody;

                List<QuestionDetailModel> questionDetailList = _quesMgr.GetQuestionModel(_questionnaireID);
                List<QuestionTotalModel> questionTotals = _quesMgr.GetTotalAnswerList(_questionnaireID);
                foreach(QuestionDetailModel questionDetails in questionDetailList)
                {
                    string quesDetail = $"<br/>{questionDetails.quesNumber}.{questionDetails.quesDetailTitle}";
                    if (questionDetails.quesDetailMustKeyIn)
                        quesDetail += "(*必填)";
                    Literal ltlquestion = new Literal();
                    ltlquestion.Text = quesDetail + "<br/>";
                    this.panTotal.Controls.Add(ltlquestion);

                    if(questionDetails.quesDetailType != QuestionType.文字方塊)
                    {
                        List<QuestionTotalModel> NoquestionList = questionTotals.FindAll(x => x.quesNumber == questionDetails.quesNumber);
                        int total = 0;
                        foreach (QuestionTotalModel questionTotal in NoquestionList)
                          total += questionTotal.AnsCount;

                        if(total == 0)
                        {
                            Literal ltlNoAnswer = new Literal();
                            ltlNoAnswer.Text = "尚無資料<br/>";
                            this.panTotal.Controls.Add(ltlNoAnswer);
                        }
                        else
                        {
                            string[] arrQues = questionDetails.quesDetailBody.Split(';');
                            for (int i = 0; i < arrQues.Length; i++)
                            {
                                int Anstotal = 0;
                                QuestionTotalModel totalModel = NoquestionList.Find(x => x.Answer == i.ToString());
                                if (totalModel != null)
                                    Anstotal += totalModel.AnsCount;

                                Literal ltlAnswer = new Literal();
                                ltlAnswer.Text = $"arrQues[i] : {Anstotal * 100 / total}% ({Anstotal})";
                                this.panTotal.Controls.Add(ltlAnswer);

                                HtmlGenericControl outotaldiv = new HtmlGenericControl("div");
                                outotaldiv.Style.Value = "width:100%;heigth:30px:border:2px solid blue";
                                this.panTotal.Controls.Add(outotaldiv);

                                HtmlGenericControl intotalanswerdiv = new HtmlGenericControl("div");
                                intotalanswerdiv.Style.Value = $"width:{Anstotal * 100 / total}%;heigth:30px;background-color:skyblue;color:white";
                                outotaldiv.Controls.Add(intotalanswerdiv);
                            }
                        }
                    }
                    else
                    {
                        Literal ltltext = new Literal();
                        ltltext.Text += "-資料不統計-<br/>";
                        this.panTotal.Controls.Add(ltltext);
                    }
                }
            }
        }
    }
}