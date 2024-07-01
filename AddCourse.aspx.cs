using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectV1
{
    public partial class AddCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //unauthorized access handling:
            int a = 0;
            if (Session["signed_in"] != null)
            {
                bool f = int.TryParse(Session["signed_in"].ToString(), out a);
            }

            if (a != 1) Response.Redirect("~/Login.aspx");
            else
            {
                //signed in, but make sure it's the admin
                if (Session["isAdmin"] != null)
                {
                    if (int.Parse(Session["isAdmin"].ToString()) != 1)
                    {
                        //it's not the admin, log out and go to sign in
                        Session["signed_in"] = 0;
                        Response.Redirect("~/Login.aspx");
                    }
                }
            }


            if (!IsPostBack)
            {
                resetForm();
            }

        }

        protected void addCourseBt_Click(object sender, EventArgs e)
        {
            Course course = new Course(-1, null, codeTb.Text, nameTb.Text, int.Parse(creditsTb.Text), 0);
            if (Global.dbHelper.CourseExist(course) == false)
            {
                //course does not exist => add it

                if (Global.dbHelper.AddCourse(course))
                {
                    //course added successfully
                    string msg = "<p style=\"color:green;\"><strong>Course added successfully!!</strong></p>";
                    Response.ContentType = "text/html";
                    Response.Write(msg);
                    resetForm();
                }
                else
                {
                    //course not added
                    string errormsg1 = "<p style=\"color:red;\"><strong>Course could not be added ! </strong></p>";
                    Response.ContentType = "text/html";
                    Response.Write(errormsg1);
                }
            }

            else
            {
                //course already exists
                string errormsg2 = "<p style=\"color:red;\"><strong>Course with this code alreay exists !!!</strong></p>";
                Response.ContentType = "text/html";
                Response.Write(errormsg2);

            }
        }

        protected void resetForm()
        {
            codeTb.Text = "";
            nameTb.Text = "";
            creditsTb.Text = "";
        }
    }
}