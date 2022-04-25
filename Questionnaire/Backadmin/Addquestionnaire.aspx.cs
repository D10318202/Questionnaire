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
        private static List<AccountInfoModel> _accountInfo = new List<AccountInfoModel>();
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
                EditMode(_questionID);
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

        #region 問卷Questionnaire

        /// <summary>
        /// 問卷Questionnaire
        /// </summary>
        /// <param name="quesID"></param> 
        protected void Save_Click(object sender, EventArgs e)
        {
            if (ErrorMsg(out string mistake))
            {
                this.ltlmistamsg.Text = mistake;
                return;
            }

            QuestionModel question = new QuestionModel()
            {
                //quesID = Guid.NewGuid(),
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
            Response.Redirect("Addquestionnaire.aspx?quesID=" + question.quesID);   //感覺有點奇怪，但可行
            //this.panQuestionnaire.Visible = false;
            //this.panQuestions.Visible = true;
        }
        private void EditMode(Guid quesID)
        {
            QuestionModel question = _quesMgr.GetQuestionnaire(quesID);
            this.txtTitle.Text = question.quesTitle;
            this.txtBody.Text = question.quesBody;
            this.txtStart.Text = question.quesstart.ToString("yyyy/MM/dd");
            this.txtEnd.Text = question.quesend.ToString("yyyy/MM/dd");
        }
        protected void Cancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Allquestionnaires.aspx");
        }
        private bool ErrorMsg(out string mistake)
        {
            mistake = string.Empty;
            if (string.IsNullOrWhiteSpace(this.txtTitle.Text.Trim()))
                mistake += "※必須輸入標題※<br/>";
            if (string.IsNullOrWhiteSpace(this.txtStart.Text))
                mistake += "※必須輸入開始日期※<br/>";
            else if (Convert.ToDateTime(this.txtStart.Text) < DateTime.Today && isCreateMode)
                mistake += "※起始日期不可早於今天※<br/>";
            if (string.IsNullOrWhiteSpace(this.txtEnd.Text))
                mistake += "※必須輸入結束日期※<br/>";
            if (string.IsNullOrEmpty(mistake))
                return false;
            return true;
        }
        #endregion

        #region 問題Questions

        /// <summary>
        /// 問題Questions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            HttpContext.Current.Session["qusetionModel"] = _questionDetail;
            InitQues(_questionDetail);
            InitTextbox();
        }
        private bool ErrorMsgQuestion(out string mistake)
        {
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
            Response.Redirect("Allquestionnaires.aspx");
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
                    QuestionDetailModel questionDetail = _questionDetail.Find(x => x.quesDetailID == QuesDetailID);
                    this.txtTitle1.Text = questionDetail.quesDetailTitle;
                    this.txtAnswer.Text = questionDetail.quesDetailBody;
                    this.droptype.SelectedIndex = (int)questionDetail.quesDetailType;
                    this.checMust.Checked = questionDetail.quesDetailMustKeyIn;
                }
            }
        }
        protected void dropclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Guid.TryParse(this.dropclass.SelectedValue, out Guid OftenUseQuestion))
            {
                List<QuestionDetailModel> questionoften = _quesMgr.GetQuestionModel(OftenUseQuestion);
                HttpContext.Current.Session["qusetionDetail"] = questionoften;
                InitQues(questionoften);
            }
            else
            {
                InitQues(new List<QuestionDetailModel>());
            }
        }

        #endregion

        /// <summary>
        /// 填寫資料FillQuestions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                HttpContext.Current.Response.ContentType = "application/x-msexcel";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
                using (MemoryStream mystream = new MemoryStream())
                {
                    StreamWriter sw = new StreamWriter(mystream, Encoding.UTF8);
                    StringBuilder sbcsvContent = new StringBuilder();
                    List<QuestionDetailModel> questionDetails = _quesMgr.GetQuestionModel(_questionID);
                    string muststring = "";
                    foreach (QuestionDetailModel question in questionDetails)
                    {
                        muststring += $",{question.quesNumber}.{question.quesDetailTitle}";
                        if (question.quesDetailMustKeyIn)
                            muststring += "(*必填)";
                    }
                    sbcsvContent.Append($"姓名,年齡,手機號碼,E-mail,填寫時間{muststring}\t\n");

                    string tablestring = "";
                    foreach (AccountInfoModel accountInfos in _accountInfo)
                    {
                        tablestring += $"{accountInfos.Name},{accountInfos.Email},{accountInfos.Phone},{accountInfos.Age},{accountInfos.CreateTime}";
                        List<QuestionAnswerModel> questionAnswers = _quesMgr.GetAnswerList(accountInfos.AccountID);
                        foreach (QuestionDetailModel questionDetail in questionDetails)
                        {
                            switch (questionDetail.quesDetailType)
                            {
                                case QuestionType.單選方塊:
                                    tablestring += ",";
                                    QuestionAnswerModel radio = questionAnswers.Find(x => x.quesNumber == questionDetail.quesNumber);
                                    if (radio != null)
                                    {
                                        string[] arrtitle = questionDetail.quesDetailTitle.Split(';');

                                        string title = arrtitle[Convert.ToInt32(radio.Answer)];

                                        tablestring += title;
                                    }
                                    break;

                                case QuestionType.複選方塊:
                                    tablestring += ",";
                                    List<QuestionAnswerModel> check = questionAnswers.FindAll(x => x.quesNumber == questionDetail.quesNumber);
                                    if (check != null)
                                    {
                                        string[] arrtitle = questionDetail.quesDetailTitle.Split(';');

                                        for (int i = 0; i < check.Count; i++)
                                        {
                                            if (i != 0)
                                            {
                                                tablestring += ";";                                                 
                                            }
                                            tablestring += arrtitle[Convert.ToInt32(check[i].Answer)];                                          
                                        }
                                    }
                                    break;

                                case QuestionType.文字方塊:
                                    tablestring += ",";
                                    QuestionAnswerModel txt = questionAnswers.Find(x => x.quesNumber == questionDetail.quesNumber);
                                    if (txt != null)
                                    {
                                        tablestring += txt.Answer;
                                    }
                                    break;
                            }
                        }
                        tablestring += "\r\n";
                    }
                    sbcsvContent.Append(tablestring);
                    sw.Write(sbcsvContent);
                    sw.Flush();
                    mystream.Position = 0;
                    mystream.WriteTo(Response.OutputStream);

                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();

                }
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