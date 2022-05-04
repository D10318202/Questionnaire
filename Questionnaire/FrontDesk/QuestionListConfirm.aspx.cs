using QuestionManagers;
using QuestionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire
{
    public partial class QuestionListConfirm : System.Web.UI.Page
    {
        private static QuestionnaireManager _quesMgr = new QuestionnaireManager();
        private static Guid _questionID;
        private static List<QuestionAnswerModel> _questionAnswers = new List<QuestionAnswerModel>();
        private static AccountInfoModel _person = new AccountInfoModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            _questionAnswers = HttpContext.Current.Session["peopleAnswer"] as List<QuestionAnswerModel>;    //["             "]跟ashx設的session要一致
            string Ques = Request.QueryString["quesID"];
            if (Guid.TryParse(Ques, out _questionID) && _questionAnswers != null)
            {
                QuestionModel questionModel = _quesMgr.GetQuestionnaire(_questionID);
                this.hfID.Value = _questionID.ToString();
                this.ltltitle.Text = questionModel.quesTitle;
                this.ltlContent.Text = questionModel.quesBody;

                _person = HttpContext.Current.Session["personInfo"] as AccountInfoModel;
                this.txtname.Text = _person.Name;
                this.txtemail.Text = _person.Email;
                this.txtphone.Text = _person.Phone;
                this.txtage.Text = _person.Age;

                List<QuestionDetailModel> questionDetails = _quesMgr.GetQuestionModel(_questionID);
                foreach (QuestionDetailModel question in questionDetails)
                {
                    string ques = $"<br/>{question.quesNumber}. {question.quesDetailTitle}";
                    if (question.quesDetailMustKeyIn)
                        ques += "(*必填)";
                    Literal ltlquestionDetail = new Literal();
                    ltlquestionDetail.Text = ques + "<br/>";
                    this.plcquestion.Controls.Add(ltlquestionDetail);
                    switch (question.quesDetailType)
                    {
                        case QuestionType.單選方塊:
                            CreateRadio(question);
                            break;
                        case QuestionType.複選方塊:
                            CreateCheck(question);
                            break;
                        case QuestionType.文字方塊:
                            CreateText(question);
                            break;
                    }
                }
            }
            else
                Response.Redirect("Allquestionnaire.aspx");
        }

        /// <summary>
        /// 單選方塊
        /// </summary>
        /// <param name="question"></param>
        private void CreateRadio(QuestionDetailModel question)
        {
            QuestionAnswerModel qamrad = _questionAnswers.Find(x => x.quesNumber == question.quesNumber);
            RadioButtonList radioButtonList = new RadioButtonList();
            radioButtonList.ID = "Q" + question.quesNumber;
            radioButtonList.Enabled = false;
            this.plcquestion.Controls.Add(radioButtonList);
            string[] arrQue = question.quesDetailBody.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                if (qamrad != null && Convert.ToInt32(qamrad.Answer) == i)
                {
                    ListItem item = new ListItem(arrQue[i], i.ToString());
                    item.Selected = true;
                    radioButtonList.Items.Add(item);
                }
            }
        }
        /// <summary>
        /// 複選方塊
        /// </summary>
        /// <param name="question"></param>
        private void CreateCheck(QuestionDetailModel question)
        {
            List<QuestionAnswerModel> qamrad = _questionAnswers.FindAll(x => x.quesNumber == question.quesNumber);
            CheckBoxList checkBoxList = new CheckBoxList();
            checkBoxList.ID = "Q" + question.quesNumber;
            checkBoxList.Enabled = false;
            this.plcquestion.Controls.Add(checkBoxList);
            string[] arrQue = question.quesDetailBody.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                if (qamrad.Find(x => x.Answer == i.ToString()) != null)
                    item.Selected = true;
                checkBoxList.Items.Add(item);
            }
        }
        /// <summary>
        /// 文字方塊
        /// </summary>
        /// <param name="question"></param>
        private void CreateText(QuestionDetailModel question)
        {
            if (question != null)
            {
                QuestionAnswerModel qamrad = _questionAnswers.Find(x => x.quesNumber == question.quesNumber);
                TextBox textBox = new TextBox();
                textBox.ID = "Q" + question.quesNumber;
                textBox.Enabled = false;
                textBox.Text = qamrad.Answer;
                this.plcquestion.Controls.Add(textBox);
            }
            else
            {
                Literal litNoAnswer = new Literal();
                litNoAnswer.Text = "*此題未做答*";
                this.plcquestion.Controls.Add(litNoAnswer);
            }
        }

        protected void Cancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuestionList.aspx?quesID=" + _questionID);
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            _quesMgr.CreatePersonInfo(_person);
            foreach (QuestionAnswerModel questionAnswer in _questionAnswers)
            {
                _quesMgr.CreateAnswer(questionAnswer);
            }
            HttpContext.Current.Session.RemoveAll();
            Response.Redirect("TotalAnswer.aspx?quesID=" + _questionID);
        }
    }
}