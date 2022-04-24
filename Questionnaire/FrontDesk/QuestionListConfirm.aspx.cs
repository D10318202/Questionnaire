using QuestionManagers;
using QuestionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire.FrontDesk
{
    public partial class QuestionListConfirm : System.Web.UI.Page
    {
        private static QuestionnaireManager _quesMgr = new QuestionnaireManager();
        private static Guid _questionID;
        private static List<QuestionAnswerModel> _personanswer;
        private static Guid _personID;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}