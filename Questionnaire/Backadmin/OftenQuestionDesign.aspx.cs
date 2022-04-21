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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnquescancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Oftenusequestion.aspx");
        }
    }
}