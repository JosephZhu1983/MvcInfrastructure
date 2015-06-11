using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppInfoCenter.DemoWebApp
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            aa.Text = Request.QueryString["aa"] ?? "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            aa.Text = "bbb";
        }


    }
}
