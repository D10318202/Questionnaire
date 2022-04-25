using QuestionManagers;
using QuestionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire.FrontDesk
{
    public partial class QuestionList : System.Web.UI.Page
    {
        private static QuestionnaireManager _quesMgr = new QuestionnaireManager();
        private static Guid _questionID;
        private static List<QuestionAnswerModel> _personanswer;
        private static bool _isEditMode;
        protected void Page_Load(object sender, EventArgs e)
        {
            string quesID = Request.QueryString["quesID"];
            if(Guid.TryParse(quesID, out Guid _questionID))
            {
                _personanswer = HttpContext.Current.Session["personAnswer"] as List<QuestionAnswerModel>;
                _isEditMode = _personanswer == null ? false : true;

                QuestionModel questionModel = _quesMgr.GetQuestionnaire(_questionID);
                this.hfID.Value = _questionID.ToString();
                this.ltltitle.Text = questionModel.quesTitle;
                this.ltlContent.Text = questionModel.quesBody;

                List<QuestionDetailModel> questionDetails = _quesMgr.GetQuestionModel(_questionID);
                foreach (QuestionDetailModel question in questionDetails)
                {
                    string ques = $"<br/>{question.quesNumber}. {question.quesDetailTitle}";
                    if (question.quesDetailMustKeyIn)
                        ques += "(*必填)";
                    Literal ltlquestionDetail = new Literal();
                    ltlquestionDetail.Text = ques + "<br/>";
                    this.plcquestion.Controls.Add(ltlquestionDetail);
                    if (_isEditMode)
                    {
                        switch (question.quesDetailType)
                        {
                            case QuestionType.單選方塊:
                                EditRadio(question);
                                break;
                            case QuestionType.複選方塊:
                                EditCheck(question);
                                break;
                            case QuestionType.文字方塊:
                                EditText(question);
                                break;
                        }
                    }
                    else
                    {
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
                if (_isEditMode)
                {
                    AccountInfoModel accountInfoModel = HttpContext.Current.Session["personInfo"] as AccountInfoModel;
                    this.txtname.Text = accountInfoModel.Name;
                    this.txtphone.Text = accountInfoModel.Phone;
                    this.txtemail.Text = accountInfoModel.Email;
                    this.txtage.Text = accountInfoModel.Age;
                }
            }
            else
                Response.Redirect("Allquestionnaire.aspx");
        }

        private void CreateRadio(QuestionDetailModel question)
        {
            RadioButtonList radioButtonList = new RadioButtonList();
            radioButtonList.ID = "Q" + question.quesNumber;
            this.plcquestion.Controls.Add(radioButtonList);
            string[] arrQue = question.quesDetailBody.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                radioButtonList.Items.Add(item);
            }
        }
        private void CreateCheck(QuestionDetailModel question)
        {
            CheckBoxList checkBoxList = new CheckBoxList();
            checkBoxList.ID = "Q" + question.quesNumber;
            this.plcquestion.Controls.Add(checkBoxList);
            string[] arrQue = question.quesDetailBody.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                checkBoxList.Items.Add(item);
            }
        }
        private void CreateText(QuestionDetailModel question)
        {
            TextBox textBox = new TextBox();
            textBox.ID = "Q" + question.quesNumber;
            this.plcquestion.Controls.Add(textBox);
        }

        private void EditRadio(QuestionDetailModel question)
        {
            QuestionAnswerModel qamrad = _personanswer.Find(x => x.quesNumber == question.quesNumber);
            RadioButtonList radioButtonList = new RadioButtonList();
            radioButtonList.ID = "Q" + question.quesNumber;
            this.plcquestion.Controls.Add(radioButtonList);
            string[] arrQue = question.quesDetailBody.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                if (Convert.ToInt32(qamrad.Answer) == i)
                    item.Selected = true;
                radioButtonList.Items.Add(item);
            }
        }
        private void EditCheck(QuestionDetailModel question)
        {
            List<QuestionAnswerModel> qamrad = _personanswer.FindAll(x => x.quesNumber == question.quesNumber);
            CheckBoxList checkBoxList = new CheckBoxList();
            checkBoxList.ID = "Q" + question.quesNumber;
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
        private void EditText(QuestionDetailModel question)
        {
            QuestionAnswerModel qamrad = _personanswer.Find(x => x.quesNumber == question.quesNumber);
            TextBox textBox = new TextBox();
            textBox.ID = "Q" + question.quesNumber;
            textBox.Text = qamrad.Answer;
            this.plcquestion.Controls.Add(textBox);
        }

        protected void btncancle_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.RemoveAll();
            Response.Redirect("Allquestionnaire.aspx");
        }
    }
}