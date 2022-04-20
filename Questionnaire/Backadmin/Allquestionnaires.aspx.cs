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
    public partial class Allquestionnaires : System.Web.UI.Page
    {
        private static List<QuestionModel> _question = new List<QuestionModel>();
        private static QuestionnaireManager _quesMgr = new QuestionnaireManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<QuestionModel> questionnaireList = _quesMgr.GetQuestionnaireList();
                InitQuestionnaire(questionnaireList);
            }
        }
        private void InitQuestionnaire(List<QuestionModel> questionnaireList)
        {
            if (questionnaireList != null || questionnaireList.Count > 0)
            {
                this.repQuestionnaire.DataSource = questionnaireList;
                this.repQuestionnaire.DataBind();
                int i = questionnaireList.Count;
                foreach (RepeaterItem repeaterItem in this.repQuestionnaire.Items)
                {
                    Label lblNumber = repeaterItem.FindControl("lblNumber") as Label;
                    lblNumber.Text = i.ToString();
                    i--;
                }
            }
        }
        protected void add_Click(object sender, EventArgs e)
        {
            Response.Redirect("Addquestionnaire.aspx");
        }

        protected void repQuestionnaire_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }
    }
}