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
    public partial class OftenQuestionDesign : System.Web.UI.Page
    {
        private static QuestionnaireManager _quesMgr = new QuestionnaireManager();
        private static Guid _questionID;
        private static List<QuestionDetailModel> _questionDetail = new List<QuestionDetailModel>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            if (ErrorMsgQuestion(out string mistake))
            {
                this.ltlquesmistMsg.Text = mistake;
                return;
            }
            QuestionDetailModel questionDetail = new QuestionDetailModel();
            questionDetail.quesDetailID = Guid.NewGuid();
            questionDetail.quesID = _questionID;
            questionDetail.quesDetailTitle = this.txtTitle1.Text.Trim();
            questionDetail.quesDetailBody = this.txtAnswer.Text.Trim();
            questionDetail.quesDetailType = (QuestionType)Convert.ToInt32(this.droptype.SelectedValue);
            questionDetail.quesDetailMustKeyIn = this.checMust.Checked;

            _questionDetail.Add(questionDetail);
            HttpContext.Current.Session["qusetionMode"] = _questionDetail;
            InitQues(_questionDetail);
            InitTextbox();
        }
        private bool ErrorMsgQuestion(out string mistake)
        {
            QuestionDetailModel questionDetail = new QuestionDetailModel();
            mistake = string.Empty;
            if (string.IsNullOrWhiteSpace(this.txtTitle1.Text.Trim()))
                mistake += "※必須輸入標題※<br/>";
            if (questionDetail.quesDetailType != QuestionType.單選方塊 && this.txtAnswer.Text == null)
                mistake += "※必須把問題輸入完整※<br/>";
            else if (questionDetail.quesDetailType != QuestionType.複選方塊 && this.txtAnswer.Text == null)
                mistake += "※必須把問題輸入完整※<br/>";

            if (string.IsNullOrEmpty(mistake))
                return false;
            return true;
        }
        private void InitQues(List<QuestionDetailModel> questionList)
        {
            if (questionList != null || questionList.Count > 0)
            {
                int i = 1;
                this.repQuestions.Visible = true;
                this.repQuestions.DataSource = questionList;
                this.repQuestions.DataBind();
                foreach (RepeaterItem repeaterItem in this.repQuestions.Items)
                {
                    Label lblNumber = repeaterItem.FindControl("lblNumber") as Label;
                    lblNumber.Text = i.ToString();
                    i++;
                }
            }
            else
                this.repQuestions.Visible = false;
        }
        private void InitTextbox()
        {
            this.txtTitle1.Text = "";
            this.droptype.SelectedIndex = 0;
            this.checMust.Checked = false;
            this.txtAnswer.Text = "";
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<QuestionDetailModel> detailModels = new List<QuestionDetailModel>();
            foreach (RepeaterItem repeaterItem in this.repQuestions.Items)
            {
                HiddenField hfquesDetailID = repeaterItem.FindControl("hfquesDetailID") as HiddenField;
                CheckBox ckbDel = repeaterItem.FindControl("ckbDel") as CheckBox;
                if (!ckbDel.Checked && Guid.TryParse(hfquesDetailID.Value, out Guid QuesDetailID))
                {
                    QuestionDetailModel questionDetail = _questionDetail.Find(x => x.quesDetailID == QuesDetailID);
                    detailModels.Add(questionDetail);
                }
            }
            InitQues(detailModels);
            HttpContext.Current.Session["questionModel"] = detailModels;
        }
        protected void btnquescancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Oftenusequestion.aspx");
        }
        protected void btnquessave_Click(object sender, EventArgs e)
        {
            if (_quesMgr.GetQuestionModel(_questionID) != null)
                _quesMgr.DeleteQuestion(_questionID);

            int questionNumber = 1;
            foreach (QuestionDetailModel questionDetail in _questionDetail)
            {
                questionDetail.quesDetailID = Guid.NewGuid();
                questionDetail.quesID = _questionID;
                questionDetail.quesDetailNo = questionNumber;
                _quesMgr.CreateQuestionDetail(questionDetail);

                questionNumber++;
            }
            Response.Redirect("Oftenusequestion.aspx");
        }
        public void repQuestions_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "LinkEdit" && Guid.TryParse(e.CommandArgument.ToString(), out Guid QuesDetailID))
                {
                    QuestionDetailModel questionDetail = _questionDetail.Find(x => x.quesDetailID == QuesDetailID);
                    this.txtTitle1.Text = questionDetail.quesDetailTitle;
                    this.txtAnswer.Text = questionDetail.quesDetailBody;
                    this.droptype.SelectedIndex = (int)questionDetail.quesDetailType;
                    this.checMust.Checked = questionDetail.quesDetailMustKeyIn;
                }
            }
        }
    }
}