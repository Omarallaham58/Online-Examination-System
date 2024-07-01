using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectV1
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["signed_in"] = 0;
            Session["isTeacher"] = 0;
            Session["isAdmin"] = 0;
            Session["isStudent"] = 0;
            Response.Redirect("~/Login.aspx");
        }
    }
}