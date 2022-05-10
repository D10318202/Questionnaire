using QuestionManagers;
using QuestionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Questionnaire
{
    public partial class Allquestionnaire : System.Web.UI.Page
    {
        private static QuestionnaireManager _quesMgr = new QuestionnaireManager();
        private const int _PageSize = 5;
        private static int _PageIndex;
        private static int _TotalRows;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    List<QuestionModel> questionnaireList = _quesMgr.GetQuestionnaireList();
            //    InitQuestionnaire(questionnaireList);
            //}
            HttpContext.Current.Session.RemoveAll();
            string pageIndexText = this.Request.QueryString["Index"];
            _PageIndex =
                (string.IsNullOrWhiteSpace(pageIndexText))
                    ? 1
                    : Convert.ToInt32(pageIndexText);

            if (!IsPostBack)
            {
                GetSearchList();
            }
        }
        private void GetSearchList()
        {
            List<QuestionModel> questionmosear = new List<QuestionModel>();
            string keyword = this.Request.QueryString["keyword"];
            string starttime = this.Request.QueryString["starttime"];
            string endtime = this.Request.QueryString["endtime"];
            List<QuestionModel> questionmodata =
                                         string.IsNullOrWhiteSpace(keyword)
                                         ? _quesMgr.GetQuestionnaireList()
                                         : _quesMgr.GetQuestionnaireList(keyword);
            this.txtquestitle.Text = keyword;
            if (DateTime.TryParse(starttime, out DateTime start))
                this.txtstart.Text = start.ToString("yyyy-MM-dd");
            if (DateTime.TryParse(endtime, out DateTime end))
                this.txtend.Text = end.ToString("yyyy-MM-dd");
            foreach (QuestionModel result in questionmodata)
            {
                if (starttime != null && result.quesstart < start)
                    continue;
                else if (endtime != null && result.quesend > end)
                    continue;
                questionmosear.Add(result);
            }

            List<QuestionModel> quesresultList = _quesMgr.GetIndexList(_PageIndex, _PageSize, questionmosear);
            _TotalRows = questionmosear.Count;
            this.ucPage.TotalRows = _TotalRows;
            this.ucPage.PageIndex = _PageIndex;
            string[] paramKey = { "keyword", "starttime", "endtime" };
            string[] paramValues = { keyword, starttime, endtime };
            this.ucPage.Bind(paramKey, paramValues);
            InitQuestionnaire(quesresultList);
        }

        private void InitQuestionnaire(List<QuestionModel> questionnaireList)
        {
            this.repQuestionnaire.DataSource = questionnaireList;
            this.repQuestionnaire.DataBind();
            int i = _TotalRows - (_PageIndex - 1) * _PageSize;
            foreach (RepeaterItem repeaterItem in this.repQuestionnaire.Items)
            {
                Label lblNumber = repeaterItem.FindControl("lblNumber") as Label;
                lblNumber.Text = i.ToString();
                i--;
                //讓url變成單純的文字
                HiddenField hfquesID = repeaterItem.FindControl("hfquesID") as HiddenField;
                HtmlAnchor QuesLink = repeaterItem.FindControl("QuesLink") as HtmlAnchor;
                Label lblquesstates = repeaterItem.FindControl("lblquesstates") as Label;
                if (lblquesstates.Text != "已啟用")
                    QuesLink.HRef = "";
                else
                    QuesLink.HRef = "Allquestionnaire.aspx" + "?quesID=" + hfquesID.Value;
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
            string redirectUrl = this.Request.Url.LocalPath + "?Index=1";
            if (!string.IsNullOrWhiteSpace(this.txtquestitle.Text.Trim()))
                redirectUrl += "&keyword=" + this.txtquestitle.Text.Trim();
            if (DateTime.TryParse(this.txtstart.Text, out DateTime start))
                redirectUrl += "&starttime=" + start.ToShortDateString();
            if (DateTime.TryParse(this.txtend.Text, out DateTime end))
                redirectUrl += "&endtime=" + end.ToShortDateString();
            Response.Redirect(redirectUrl);
        }
    }
}