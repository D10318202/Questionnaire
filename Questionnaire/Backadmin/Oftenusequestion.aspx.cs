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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string keyword = this.Request.QueryString["keyword"];
                this.txtkeyword.Text = keyword;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = this.txtkeyword.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
                Response.Redirect("Allquestionnaire.aspx");
            else
                Response.Redirect("Allquestionnaire.aspx?keyword=" + keyword);
        }
    }
}