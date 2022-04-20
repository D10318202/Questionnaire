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
    public partial class Oftenusequestion : System.Web.UI.Page
    {
        private static QuestionnaireManager _quesMgr = new QuestionnaireManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                List<QuestionModel> questionnaireList = _quesMgr.GetQuestionList();
                InitQuesOften(questionnaireList);
            }
            //if (!this.IsPostBack)
            //{
            //    string keyword = this.Request.QueryString["keyword"];
            //    this.txtkeyword.Text = keyword;
            //}
        }
        private void InitQuesOften(List<QuestionModel> questionnaireList)
        {
            this.rptQuestionOften.DataSource = questionnaireList;
            this.rptQuestionOften.DataBind();
            int i = questionnaireList.Count;
            foreach (RepeaterItem repeaterItem in this.rptQuestionOften.Items)
            {
                Label lblNumber = repeaterItem.FindControl("lblNumber") as Label;
                lblNumber.Text = i.ToString();
                i--;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = this.txtkeyword.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
                Response.Redirect("Allquestionnaire.aspx");
            else
                Response.Redirect("Allquestionnaire.aspx?keyword=" + keyword);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuestionDetailModel");
        }
    }
}