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
        private const int _PageSize = 5;
        private static int _PageIndex;
        private static int _TotalRows;
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session.RemoveAll();
            string pageIndexText = this.Request.QueryString["Index"];
            _PageIndex =
                (string.IsNullOrWhiteSpace(pageIndexText))
                    ? 1
                    : Convert.ToInt32(pageIndexText);

            if (!this.IsPostBack)
            {
                GetSearchList();
            }
        }
        private void GetSearchList()
        {
            try
            {
                string keyword = this.Request.QueryString["keyword"];
                List<QuestionModel> questionmodata =
                                             string.IsNullOrWhiteSpace(keyword)
                                             ? _quesMgr.GetQuestionList()
                                             : _quesMgr.GetQuestionList(keyword);
                List<QuestionModel> quesresultList = _quesMgr.GetIndexList(_PageIndex, _PageSize, questionmodata);
                this.txtkeyword.Text = keyword;
                _TotalRows = quesresultList.Count;
                this.ucPage.TotalRows = _TotalRows;
                this.ucPage.PageIndex = _PageIndex;
                string[] paramKey = { "keyword" };
                string[] paramValues = { keyword };
                this.ucPage.Bind(paramKey, paramValues);
                InitQuesOften(quesresultList);

            }
            catch (Exception ex)
            {
                Response.Redirect("Oftenusequestion.aspx");
            }

        }

        private void InitQuesOften(List<QuestionModel> questionnaireList)
        {
            this.rptQuestionOften.DataSource = questionnaireList;
            this.rptQuestionOften.DataBind();
            int i = _TotalRows - (_PageIndex - 1) * _PageSize;
            foreach (RepeaterItem repeaterItem in this.rptQuestionOften.Items)
            {
                Label lblNumber = repeaterItem.FindControl("lblNumber") as Label;
                lblNumber.Text = i.ToString();
                i--;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {   
            string redirectUrl = this.Request.Url.LocalPath + "?Index=1";         
            else if (!string.IsNullOrWhiteSpace(this.txtkeyword.Text.Trim()))
                redirectUrl += "&keyword=" + this.txtkeyword.Text.Trim();
            Response.Redirect(redirectUrl);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Guid newquestionID = Guid.NewGuid();
            _quesMgr.CreateOftenUseExamp(newquestionID, this.txtCreate.Text.Trim());
            Response.Redirect("OftenQuestionDesign.aspx?quesID=" + newquestionID);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem repeaterItem in this.rptQuestionOften.Items)
            {
                HiddenField hfID = repeaterItem.FindControl("hfID") as HiddenField;
                CheckBox ckbDel = repeaterItem.FindControl("ckbDel") as CheckBox;
                if (ckbDel.Checked && Guid.TryParse(hfID.Value, out Guid quesID))
                    _quesMgr.DeleteQuestionnaire(quesID);
            }
            List<QuestionModel> questionnaireList = _quesMgr.GetQuestionList();
            InitQuesOften(questionnaireList);
            Response.Redirect("Oftenusequestion.aspx");
        }
    }
}