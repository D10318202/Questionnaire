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
        List<QuestionDetailModel> quesDetaillist = new List<QuestionDetailModel>();
        private static QuestionnaireManager _quesMgr;
        private QuestionDetailModel questionDetail = new QuestionDetailModel();
        private QuestionAnswerModel quesAnswer = new QuestionAnswerModel();
        int a;
        int b;
        int c;
        //private static QuestionDetail2Model _quesDetail2list;
        protected void Page_Load(object sender, EventArgs e)
        {
            string quesID = Request.QueryString["quesID"];
            //quesDetaillist = _quesMgr.GetQuestionModel(Guid.Parse(quesID));
            int listcount = quesDetaillist.Count;
            int i = 0;
            int j = 0;
            int k = 0;
            foreach (var item in quesDetaillist)
            {
                if (item.quesDetailType == QuestionType.單選方塊)
                {
                    if (item.answer.Contains(","))
                    {
                        this.Form.Controls.Add(new Literal() { ID = "ltl" + k, Text = "<br/>" + item.quesDetailTitle + ":" });
                        //this.quesAnswer.QuestionOfRadio[k] = item.quesDetailTitle;
                        string[] array = item.answer.Split(',');
                        foreach (var item2 in array)
                        {
                            this.Form.Controls.Add(new RadioButton() { ID = "radio" + k, Text = item2 });
                            k++;
                            a++;
                        }
                        continue;
                    }
                    else
                    {
                        BuildRadioBox(k, item.answer, item.quesDetailTitle);
                        a++;
                        k++;
                    }
                }
                if (item.quesDetailType == QuestionType.複選方塊)
                {
                    if (item.answer.Contains(","))
                    {
                        this.Form.Controls.Add(new Literal() { ID = "ltl" + j, Text = "<br/>" + item.quesDetailTitle + ":" + "<br/>" });
                        //this.quesAnswer.QuestionOfCheck[j] = item.quesDetailTitle;
                        string[] array = item.answer.Split(',');
                        foreach (var item2 in array)
                        {
                            this.Form.Controls.Add(new CheckBox() { ID = "check" + j, Text = item2 });
                            j++;
                            b++;
                        }
                        continue;
                    }
                    else
                    {
                        BuildCheckBox(j, item.answer, item.quesDetailTitle);
                        b++;
                        j++;
                    }
                }
                if (item.quesDetailType == QuestionType.文字方塊)
                {
                    //this.quesAnswer.QuestionOfText[i] = item.quesDetailTitle;
                    BuildTextBox(i, item.answer, item.quesDetailTitle);
                    i++;
                    c++;
                }
            }
        }
        private void BuildTextBox(int i, string answer, string quesDetailTitle)
        {

            this.Form.Controls.Add(new Literal() { ID = "ltl" + i, Text = "<br/>" + quesDetailTitle + ":" });
            this.Form.Controls.Add(new TextBox() { ID = "txt" + i });

        }
        private void BuildCheckBox(int j, string answer, string quesDetailTitle)
        {

            this.Form.Controls.Add(new Literal() { ID = "ltl" + j, Text = "<br/>" + quesDetailTitle + ":" + "<br/>" });
            this.Form.Controls.Add(new CheckBox() { ID = "check" + j, Text = answer });
        }
        private void BuildRadioBox(int k, string answer, string quesDetailTitle)
        {

            this.Form.Controls.Add(new Literal() { ID = "ltl" + k, Text = "<br/>" + quesDetailTitle + ":" + "<br/>" });
            this.Form.Controls.Add(new RadioButton() { ID = "radio" + k, Text = answer });
        }
        protected void Cancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("allquestionnaire.aspx");
        }
    }
}