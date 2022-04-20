using QuestionManagers;
using QuestionModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire.Backadmin
{
    public partial class Addquestionnaire : System.Web.UI.Page
    {
        private static QuestionnaireManager _quesMgr = new QuestionnaireManager();
        private QuestionDetailModel questionDetail = new QuestionDetailModel();
        private QuestionModel question = new QuestionModel();
        private static List<QuestionDetailModel> _questionDetail = new List<QuestionDetailModel>();
        private static Guid _questionID;
        private bool isCreateMode;
        protected void Page_Load(object sender, EventArgs e)
        {
            string QusetionnaireID = Request.QueryString["quesID"];
            if (string.IsNullOrWhiteSpace(QusetionnaireID))
            {
                isCreateMode = true;
                HttpContext.Current.Session.Remove("quesID");
            }
            else if (Guid.TryParse(QusetionnaireID, out _questionID))
            {
                isCreateMode = false;
                initEditMode(_questionID);
                HttpContext.Current.Session["quesID"] = _questionID;
            }
            else
                Response.Redirect("Allquestionnaires.aspx");
        }

        /// <summary>
        /// 分頁切換
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        protected void LinkQuestionnaire_Click(object sender, EventArgs e)
        {
            this.ChangeStatus(PageStatus.Questionnaire);
        }
        protected void LinkQuestions_Click(object sender, EventArgs e)
        {
            this.ChangeStatus(PageStatus.Questions);
        }
        protected void LinkFillQuestions_Click(object sender, EventArgs e)
        {
            this.ChangeStatus(PageStatus.FillQuestions);
        }
        protected void LinkTotal_Click(object sender, EventArgs e)
        {
            this.ChangeStatus(PageStatus.Total);
        }
        private void ChangeStatus(PageStatus Status)
        {
            this.LinkQuestionnaire.Enabled = (Status != PageStatus.Questionnaire);
            this.LinkQuestions.Enabled = (Status != PageStatus.Questions);
            this.LinkFillQuestions.Enabled = (Status != PageStatus.FillQuestions);
            this.LinkTotal.Enabled = (Status != PageStatus.Total);

            this.panQuestionnaire.Visible = (Status == PageStatus.Questionnaire);
            this.panQuestions.Visible = (Status == PageStatus.Questions);
            this.panFillQuestions.Visible = (Status == PageStatus.FillQuestions);
            this.panTotal.Visible = (Status == PageStatus.Total);
        }
        private enum PageStatus
        {
            //問卷
            Questionnaire,
            //問題
            Questions,
            //填寫資料
            FillQuestions,
            //資料統計
            Total
        }
        #endregion

        /// <summary>
        /// 問卷Questionnaire
        /// </summary>
        /// <param name="quesID"></param>
        private void initEditMode(Guid quesID)
        {
            QuestionModel question = _quesMgr.GetQuestionnaire(quesID);
            this.txtTitle.Text = question.quesTitle;
            this.txtBody.Text = question.quesBody;
            this.txtStart.Text = question.quesstart.ToString();
            this.txtEnd.Text = question.quesend.ToString();
        }
        protected void Cancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Allquestionnaires.aspx");
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            QuestionModel question = new QuestionModel()
            {
                quesID = Guid.NewGuid(),
                quesstart = Convert.ToDateTime(this.txtStart.Text),
                quesend = Convert.ToDateTime(this.txtEnd.Text),
                quesTitle = this.txtTitle.Text.Trim(),
                quesBody = this.txtBody.Text.Trim()
            };
            if (this.checUse.Checked == false)
            {
                question.stateType = StateType.關閉;
            }
            else
            {
                question.stateType = StateType.已啟用;
            }

            if (isCreateMode)
            {
                question.quesID = Guid.NewGuid();
                _quesMgr.CreateQuestionnaire(question);
            }
            else
            {
                question.quesID = _questionID;
                _quesMgr.UpdateQuestionnaire(question);
            }
            HttpContext.Current.Session["quesID"] = question.quesID;
            this.panQuestionnaire.Visible = false;
            this.panQuestions.Visible = true;
        }

        /// <summary>
        /// 問題Questions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAdd_Click(object sender, EventArgs e)
        {          
            QuestionDetailModel questionDetail = new QuestionDetailModel();
            questionDetail.quesDetailID = Guid.NewGuid();
            questionDetail.quesID = _questionID;
            questionDetail.quesDetailTitle = this.txtTitle1.Text.Trim();
            questionDetail.quesDetailBody = this.txtAnswer.Text.Trim();
            questionDetail.quesDetailType = (QuestionType)Convert.ToInt32(this.droptype.SelectedValue);
            questionDetail.quesDetailMustKeyIn = this.checMust.Checked;

            _questionDetail.Add(questionDetail);
            HttpContext.Current.Session["qusetionModel"] = _questionDetail;
            InitQues(_questionDetail);
            InitTextbox();
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
            Response.Redirect("Allquestionnaires.aspx");
        }

        protected void btnquessave_Click(object sender, EventArgs e)
        {
            int questionNumber = 1;
            foreach (QuestionDetailModel questionDetail in _questionDetail)
            {
                questionDetail.quesDetailNo = questionNumber;
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
                    QuestionDetailModel questionDetail = _questionDetail.Find(x => x.quesDetailID == QuesDetailID);
                    this.txtTitle1.Text = questionDetail.quesDetailTitle;
                    this.txtAnswer.Text = questionDetail.quesDetailBody;
                    this.droptype.SelectedIndex = (int)questionDetail.quesDetailType;
                    this.checMust.Checked = questionDetail.quesDetailMustKeyIn;
                }
            }
        }

        #region /*填寫資料FillQuestions*/
        protected void btnsavefile_Click(object sender, EventArgs e)
        {
            QuestionModel questionModel = _quesMgr.GetQuestionnaire(_questionID);
            string FileName = questionModel.quesTitle + ".csv";
            string DownFile = "C:\\Users\\YUKI\\Desktop\\Questionnaire\\Download";
            if (!Directory.Exists(DownFile))
            {
                Directory.CreateDirectory(DownFile);
            }
            try
            {
                Response.Clear();
                Response.Buffer = true;

                #region 匯出csv
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                using (MemoryStream mystream = new MemoryStream())
                {
                    StreamWriter sw = new StreamWriter(mystream, Encoding.UTF8);
                    StringBuilder sbcsvContent = new StringBuilder();
                    List<QuestionDetailModel> questionDetailModels = new List<QuestionDetailModel>();


                }


                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                #endregion

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('匯出失敗')</script>");
            }
        }
        #endregion




    }
}