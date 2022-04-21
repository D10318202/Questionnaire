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
    public partial class Allquestionnaire : System.Web.UI.Page
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

        /// <summary>
        /// 搜尋功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //待更改
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = this.txtquestitle.Text.Trim();
            List<QuestionModel> questionmosear = new List<QuestionModel>();
            List<QuestionModel> questionmodata =
                                        (string.IsNullOrWhiteSpace(keyword))
                                         ? _quesMgr.GetQuestionnaireList()
                                         : _quesMgr.GetQuestionnaireList(keyword);

            foreach (QuestionModel result in questionmodata)
            {
                if (DateTime.TryParse(this.txtstart.Text, out DateTime startTime) &&
                    result.quesstart < startTime)
                    return;
                else if (DateTime.TryParse(this.txtend.Text, out DateTime endTime) &&
                   result.quesend > endTime)
                    return;
                questionmosear.Add(result);
            }
            InitQuestionnaire(questionmosear);
        }
    }
}