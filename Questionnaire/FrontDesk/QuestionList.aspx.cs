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
    public partial class QuestionList : System.Web.UI.Page
    {
        private static QuestionnaireManager _quesMgr = new QuestionnaireManager();
        private static Guid _questionID;
        private static List<QuestionAnswerModel> _questionAnswers = new List<QuestionAnswerModel>();
        private QuestionAnswerModel personAnswer = new QuestionAnswerModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            string Ques = Request.QueryString["quesID"];
            if (Guid.TryParse(Ques, out _questionID))
            {
                _questionAnswers = HttpContext.Current.Session["peopleAnswer"] as List<QuestionAnswerModel>;
                //_isEditMode = _personanswer == null ? false : true;
                //if (_isEditMode)
                //{
                //    AccountInfoModel accountInfoModel = HttpContext.Current.Session["personInfo"] as AccountInfoModel;
                //    this.txtname.Text = accountInfoModel.Name;
                //    this.txtphone.Text = accountInfoModel.Phone;
                //    this.txtemail.Text = accountInfoModel.Email;
                //    this.txtage.Text = accountInfoModel.Age;
                //}

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
            RadioButtonList radioButtonList = new RadioButtonList();
            radioButtonList.ID = "Q" + question.quesNumber;
            if (question.quesDetailMustKeyIn)
                radioButtonList.CssClass = "Must";
            this.plcquestion.Controls.Add(radioButtonList);
            string[] arrQue = question.quesDetailBody.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                radioButtonList.Items.Add(item);
            }
        }

        /// <summary>
        /// 複選方塊
        /// </summary>
        /// <param name="question"></param>
        private void CreateCheck(QuestionDetailModel question)
        {
            CheckBoxList checkBoxList = new CheckBoxList();
            checkBoxList.ID = "Q" + question.quesNumber;
            if (question.quesDetailMustKeyIn)
                checkBoxList.CssClass = "Must";
            this.plcquestion.Controls.Add(checkBoxList);
            string[] arrQue = question.quesDetailBody.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                checkBoxList.Items.Add(item);
            }
        }

        /// <summary>
        /// 文字方塊
        /// </summary>
        /// <param name="question"></param>
        private void CreateText(QuestionDetailModel question)
        {
            TextBox textBox = new TextBox();
            textBox.ID = "Q" + question.quesNumber;
            if (question.quesDetailMustKeyIn)
                textBox.CssClass = "Must";
            this.plcquestion.Controls.Add(textBox);
        }

        protected void btncancle_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.RemoveAll();
            Response.Redirect("Allquestionnaire.aspx");
        }
    }
}