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
        private static QuestionnaireManager _quesMgr = new QuestionnaireManager();
        private const int _pageSize = 5;
        private static int _pageIndex;
        private static int _totalRows;
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session.RemoveAll();
            string pageIndexText = this.Request.QueryString["Page"];
            _pageIndex =
                (string.IsNullOrWhiteSpace(pageIndexText))
                    ? 1
                    : Convert.ToInt32(pageIndexText);

            if (!this.IsPostBack)
            {
                List<QuestionModel> questionnaireList = _quesMgr.GetQuestionnaireBackadminList();
                InitQuestionnaire(questionnaireList);
            }
                //List<QuestionModel> questionsList = _quesMgr.GetIndexList(_pageIndex, _pageSize, searchlist);
                //_totalRows = searchlist.Count;
                //this.ucPager.TotalRows = _totalRows;
                //this.ucPager.PageIndex = _pageIndex;
                //this.ucPager.Bind(paramKey, paramValue);
            
        }
        private void InitQuestionnaire(List<QuestionModel> questionnaireList)
        {
            this.repQuestionnaire.DataSource = questionnaireList;
            this.repQuestionnaire.DataBind();
            int i = _totalRows - (_pageIndex - 1) * _pageSize;
            foreach (RepeaterItem repeaterItem in this.repQuestionnaire.Items)
            {
                Label lblNumber = repeaterItem.FindControl("lblNumber") as Label;
                lblNumber.Text = i.ToString();
                i--;
            }
        }

        /// <summary>
        /// 增加問卷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void add_Click(object sender, EventArgs e)
        {
            Response.Redirect("Addquestionnaire.aspx");
        }

        /// <summary>
        /// 刪除問卷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void delete_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem repeaterItem in this.repQuestionnaire.Items)
            {
                HiddenField hfquesID = repeaterItem.FindControl("hfquesID") as HiddenField;
                CheckBox ckbDel = repeaterItem.FindControl("ckbDel") as CheckBox;
                if (ckbDel.Checked && Guid.TryParse(hfquesID.Value, out Guid quesID))
                    _quesMgr.DeleteQuestionnaire(quesID);
            }
            List<QuestionModel> questionnaireList = _quesMgr.GetQuestionnaireList();
            InitQuestionnaire(questionnaireList);
        }

        protected void repQuestionnaire_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }

        /// <summary>
        /// 搜尋功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            //if (string.IsNullOrWhiteSpace(keyword))
            //    _quesMgr.GetQuestionnaireList();                
            //else if(!string.IsNullOrWhiteSpace(keyword))
            //{
            //     _quesMgr.GetQuestionnaireList(keyword);
            //}

        }


    }
}