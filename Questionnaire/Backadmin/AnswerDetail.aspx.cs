using QuestionManagers;
using QuestionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire.Backadmin
{
    public partial class AnswerDetail : System.Web.UI.Page
    {
        private static QuestionnaireManager _quesMgr = new QuestionnaireManager();
        private static List<QuestionAnswerModel> _personanswer = new List<QuestionAnswerModel>();
        private static QuestionAnswerModel _answer;
        private QuestionAnswerModel QuestionAnswer = new QuestionAnswerModel();
        private static Guid _personID;
        protected void Page_Load(object sender, EventArgs e)
        {
            string personstring = Request.QueryString["AccountID"];
            if (Guid.TryParse(personstring, out _personID))
            {
                AccountInfoModel accountinfo = _quesMgr.GetPersonInfo(_personID);
                QuestionModel questionModel = _quesMgr.GetQuestionnaire(accountinfo.quesID);
                this.ltltitle.Text = questionModel.quesTitle;
                this.ltlContent.Text = questionModel.quesBody;

                this.txtname.Text = accountinfo.Name;
                this.txtemail.Text = accountinfo.Email;
                this.txtphone.Text = accountinfo.Phone;
                this.txtage.Text = accountinfo.Age;

                _personanswer = _quesMgr.GetAnswerList(_personID);
                List<QuestionDetailModel> questionDetails = _quesMgr.GetQuestionModel(accountinfo.quesID);
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
                Response.Redirect("Allquestionnaires.aspx");
        }
        private void CreateRadio(QuestionDetailModel question)
        {
            QuestionAnswerModel qamrad = _personanswer.Find(x => x.quesNumber == question.quesNumber);
            RadioButtonList radioButtonList = new RadioButtonList();
            radioButtonList.ID = "Q" + question.quesNumber;
            radioButtonList.Enabled = false;
            this.plcquestion.Controls.Add(radioButtonList);
            string[] arrQue = question.quesDetailBody.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                if (qamrad != null && Convert.ToInt32(qamrad.Answer) == i)
                    item.Selected = true;
                radioButtonList.Items.Add(item);
            }
        }
        private void CreateCheck(QuestionDetailModel question)
        {
            List<QuestionAnswerModel> qamrad = _personanswer.FindAll(x => x.quesNumber == question.quesNumber);
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
        private void CreateText(QuestionDetailModel question)
        {
            QuestionAnswerModel questionAnswer = _personanswer.Find(x => x.quesNumber == question.quesNumber);
            TextBox textBox = new TextBox();
            textBox.ID = "Q" + question.quesNumber;
            textBox.Enabled = false;
            if(questionAnswer != null)
                textBox.Text = questionAnswer.Answer;
            this.plcquestion.Controls.Add(textBox);
        }
    }
}