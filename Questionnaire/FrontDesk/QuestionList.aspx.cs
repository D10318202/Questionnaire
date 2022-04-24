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
        //List<QuestionDetailModel> quesDetaillist = new List<QuestionDetailModel>();
        private static QuestionnaireManager _quesMgr;
        private static Guid _quesID;
        //private QuestionDetailModel questionDetail = new QuestionDetailModel();
        //private QuestionAnswerModel quesAnswer = new QuestionAnswerModel();
        //int a;
        //int b;
        //int c;
        private static QuestionAnswerModel _personanswer;
        protected void Page_Load(object sender, EventArgs e)
        {
            string quesID = Request.QueryString["quesID"];
            if(Guid.TryParse(quesID, out Guid _quesID))
            {
                _personanswer = HttpContext.Current.Session["personAnswer"] as QuestionAnswerModel;
                bool isEditModel = _personanswer == null ? false : true;

                QuestionModel questionModel = _quesMgr.GetQuestionnaire(_quesID);
                this.ltltitle.Text = questionModel.quesTitle;
                this.ltlContent.Text = questionModel.quesBody;

                List<QuestionDetailModel> questionDetails = new List<QuestionDetailModel>();
                //foreach(QuestionDetailModel question in questionDetails)
                //{
                //    string ques = $"<br/>{question.quesDetailNo}. {question.quesDetailTitle}";
                //    if (question.quesDetailMustKeyIn)
                //        ques += "(*必填)";
                //    this.plcquestion.Controls.Add(new LiteralControl(ques));
                //    if(isEditModel)
                //    {
                //        switch(question.quesDetailType)
                //        {
                //            case QuestionType.單選方塊:
                //                CreateRdb(question);
                //                break;
                //            case QuestionType.複選方塊:
                //                CreateCkb(question);
                //                break;
                //            case QuestionType.文字方塊:
                //                CreateTxt(question);
                //                break;
                //        }
                //    }
                //}
            }
            //quesDetaillist = _quesMgr.GetQuestionModel(Guid.Parse(quesID));
            //int listcount = quesDetaillist.Count;
            //int i = 0;
            //int j = 0;
            //int k = 0;
            //foreach (var item in quesDetaillist)
            //{
            //    if (item.quesDetailType == QuestionType.單選方塊)
            //    {
            //        if (quesAnswer.Answer.Contains(","))
            //        {
            //            this.Form.Controls.Add(new Literal() { ID = "ltl" + k, Text = "<br/>" + item.quesDetailTitle + ":" });
            //            //this.quesAnswer.QuestionOfRadio[k] = item.quesDetailTitle;
            //            string[] array = quesAnswer.Answer.Split(',');
            //            foreach (var item2 in array)
            //            {
            //                this.Form.Controls.Add(new RadioButton() { ID = "radio" + k, Text = item2 });
            //                k++;
            //                a++;
            //            }
            //            continue;
            //        }
            //        else
            //        {
            //            BuildRadioBox(k, quesAnswer.Answer, item.quesDetailTitle);
            //            a++;
            //            k++;
            //        }
            //    }
            //    if (item.quesDetailType == QuestionType.複選方塊)
            //    {
            //        if (quesAnswer.Answer.Contains(","))
            //        {
            //            this.Form.Controls.Add(new Literal() { ID = "ltl" + j, Text = "<br/>" + item.quesDetailTitle + ":" + "<br/>" });
            //            //this.quesAnswer.QuestionOfCheck[j] = item.quesDetailTitle;
            //            string[] array = quesAnswer.Answer.Split(',');
            //            foreach (var item2 in array)
            //            {
            //                this.Form.Controls.Add(new CheckBox() { ID = "check" + j, Text = item2 });
            //                j++;
            //                b++;
            //            }
            //            continue;
            //        }
            //        else
            //        {
            //            BuildCheckBox(j, quesAnswer.Answer, item.quesDetailTitle);
            //            b++;
            //            j++;
            //        }
            //    }
            //    if (item.quesDetailType == QuestionType.文字方塊)
            //    {
            //        //this.quesAnswer.QuestionOfText[i] = item.quesDetailTitle;
            //        BuildTextBox(i, quesAnswer.Answer, item.quesDetailTitle);
            //        i++;
            //        c++;
            //    }
            //}
        }

        private void CreateRdb(QuestionDetailModel question)
        {
            RadioButtonList radioButtonList = new RadioButtonList();
            radioButtonList.ID = "Q" + question.quesDetailNo;
            this.plcquestion.Controls.Add(radioButtonList);
            string[] arrQue = question.quesDetailBody.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                radioButtonList.Items.Add(item);
            }
        }
        private void CreateCkb(QuestionDetailModel question)
        {
            CheckBoxList checkBoxList = new CheckBoxList();
            checkBoxList.ID = "Q" + question.quesDetailNo;
            this.plcquestion.Controls.Add(checkBoxList);
            string[] arrQue = question.quesDetailBody.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                checkBoxList.Items.Add(item);
            }
        }
        private void CreateTxt(QuestionDetailModel question)
        {
            TextBox textBox = new TextBox();
            textBox.ID = "Q" + question.quesDetailNo;
            this.plcquestion.Controls.Add(textBox);
        }

        //private void BuildTextBox(int i, string answer, string quesDetailTitle)
        //{

        //    this.Form.Controls.Add(new Literal() { ID = "ltl" + i, Text = "<br/>" + quesDetailTitle + ":" });
        //    this.Form.Controls.Add(new TextBox() { ID = "txt" + i });

        //}
        //private void BuildCheckBox(int j, string answer, string quesDetailTitle)
        //{

        //    this.Form.Controls.Add(new Literal() { ID = "ltl" + j, Text = "<br/>" + quesDetailTitle + ":" + "<br/>" });
        //    this.Form.Controls.Add(new CheckBoxList() { ID = "check" + j, Text = answer });
        //}
        //private void BuildRadioBox(int k, string answer, string quesDetailTitle)
        //{

        //    this.Form.Controls.Add(new Literal() { ID = "ltl" + k, Text = "<br/>" + quesDetailTitle + ":" + "<br/>" });
        //    this.Form.Controls.Add(new RadioButtonList() { ID = "radio" + k, Text = answer });
        //}
        protected void Cancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("allquestionnaire.aspx");
        }
    }
}