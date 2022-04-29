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
    public partial class OftenUseQuestionDesign : System.Web.UI.Page
    {
        private static Guid _questionID;
        private static QuestionnaireManager _quesMgr = new QuestionnaireManager();
        private static List<QuestionDetailModel> _question = new List<QuestionDetailModel>();
        private static QuestionDetailModel questionDetail = new QuestionDetailModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            _question = HttpContext.Current.Session["questionDetail"] as List<QuestionDetailModel>;
            if (!IsPostBack)
            {
                string QuesID = Request.QueryString["quesID"];
                if (Guid.TryParse(QuesID, out _questionID))
                {
                    List<QuestionDetailModel> questionList = _quesMgr.GetQuestionModel(_questionID);
                    this.txtTitle.Text = _quesMgr.GetOftenUse(_questionID).quesTitle;
                    InitQues(questionList);
                    HttpContext.Current.Session["questionDetail"] = questionList;
                }
                else
                    Response.Redirect("Oftenusequestion.aspx");
            }
        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            if (ErrorMsgQuestion(out string mistake))
            {
                this.ltlquesmistMsg.Text = mistake;
                return;
            }
            QuestionDetailModel questionDetail = new QuestionDetailModel()
            {
                quesDetailID = Guid.NewGuid(),
                quesID = _questionID,
                quesDetailTitle = this.txtTitle1.Text.Trim(),
                quesDetailBody = this.txtAnswer.Text.Trim(),
                quesDetailType = (QuestionType)Convert.ToInt32(this.droptype.SelectedValue),
                quesDetailMustKeyIn = this.checMust.Checked,
            };
            _question.Add(questionDetail);
            HttpContext.Current.Session["questionDetail"] = _question;
            InitQues(_question);
            InitTextbox();
        }
        private bool ErrorMsgQuestion(out string mistake)
        {
            mistake = string.Empty;
            if (string.IsNullOrWhiteSpace(this.txtTitle1.Text.Trim()))
                mistake += "※必須輸入標題※<br/>";
            else if (this.txtTitle1.Text.Length < 3)
                mistake += "※標題必須至少要有3個字※<br/>";

            if (questionDetail.quesDetailType != QuestionType.單選方塊 && this.txtAnswer.Text == null)
                mistake += "※必須把問題和答案輸入完整※<br/>";
            else if (questionDetail.quesDetailType != QuestionType.複選方塊 && this.txtAnswer.Text == null)
                mistake += "※必須把問題和答案輸入完整※<br/>";
            else if (questionDetail.quesDetailType == QuestionType.文字方塊 && this.txtAnswer.Text != null)
                mistake += "※選擇文字方塊不需要輸入回答欄位※<br/>";

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
                    QuestionDetailModel questionDetail = _question.Find(x => x.quesDetailID == QuesDetailID);
                    detailModels.Add(questionDetail);
                }
            }
            InitQues(detailModels);
            HttpContext.Current.Session["questionDetail"] = detailModels;
        }
        protected void btnquescancle_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.RemoveAll();
            Response.Redirect("Allquestionnaires.aspx");
        }
        protected void btnquessave_Click(object sender, EventArgs e)
        {
            if (_quesMgr.GetQuestionModel(_questionID) != null)
                _quesMgr.DeleteQuestion(_questionID);

            #region 防呆部份
            if (string.IsNullOrWhiteSpace(this.txtTitle.Text))
            {
                this.ltlquesmistMsg.Visible = true;
                this.ltlquesmistMsg.Text = "*請輸入常用問題的標題*";
                return;
            }
            else
                this.ltlquesmistMsg.Visible = false;

            if (_question.Count == 0)
            {
                this.ltlquesmistMsg.Visible = true;
                this.ltlquesmistMsg.Text = "*只少要輸入一個常用問題*";
                return;
            }
            #endregion

            int questionNumber = 1;
            foreach (QuestionDetailModel questionDetail in _question)
            {
                questionDetail.quesDetailID = Guid.NewGuid();
                questionDetail.quesID = _questionID;
                questionDetail.quesNumber = questionNumber;
                _quesMgr.CreateQuestionDetail(questionDetail);

                questionNumber++;
            }
            Response.Redirect("Allquestionnaires.aspx");
        }
        public void repQuestions_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "LinkEdit" && Guid.TryParse(e.CommandArgument.ToString(), out Guid QuesDetailID))
                {
                    QuestionDetailModel questionDetail = _question.Find(x => x.quesDetailID == QuesDetailID);
                    this.txtTitle1.Text = questionDetail.quesDetailTitle;
                    this.txtAnswer.Text = questionDetail.quesDetailBody;
                    this.droptype.SelectedIndex = (int)questionDetail.quesDetailType;
                    this.checMust.Checked = questionDetail.quesDetailMustKeyIn;
                }
            }
        }
    }
}