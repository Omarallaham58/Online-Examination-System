using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectV1
{
    public partial class displayGrade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            //unauthorized access handling
            int a = 0;
            if (Session["signed_in"] != null)
            {
                bool f = int.TryParse(Session["signed_in"].ToString(), out a);
            }

            if (a != 1)
            {

                Session["isStudent"] = 0;
                Session["isAdmin"] = 0;
                Session["isTeacher"] = 0;
                Session["signed_in"] = 0;
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                //signed in, but make sure it's the student
                if (Session["isStudent"] != null)
                {
                    int isStudent;
                    int.TryParse(Session["isStudent"].ToString(), out isStudent);
                    if (isStudent != 1)
                    {
                        //it's not the student, log out and go to sign in
                        Session["isStudent"] = 0;
                        Session["isAdmin"] = 0;
                        Session["isTeacher"] = 0;
                        Session["signed_in"] = 0;
                        Response.Redirect("~/Login.aspx");
                    }
                }
            }



            ///-------------------
            if (Session["signed_in"] != null)
            {
                int signed;
                int.TryParse(Session["signed_in"].ToString(), out signed);
                if (signed == 1)
                {
                    //    isStudent = 0;
                    int isAdmin = 0, isStudent = 0, isTeacher = 0;
                    if (Session["isAdmin"] != null) int.TryParse(Session["isAdmin"].ToString(), out isAdmin);
                    if (Session["isTeacher"] != null) int.TryParse(Session["isTeacher"].ToString(), out isTeacher);
                    if (Session["isStudent"] != null) int.TryParse(Session["isStudent"].ToString(), out isStudent);
                    //already signed in
                    // if (Session["isAdmin"] != null)
                    //  {
                    //    int isAdmin;
                    //  int.TryParse(Session["isAdmin"].ToString(), out isAdmin);
                    if (isAdmin == 1)
                    {
                        Session["isAdmin"] = 1;
                        Session["isStudent"] = 0;
                        Session["isTeacher"] = 0;
                        Response.Redirect("~/Admin.aspx");
                    }

                    // else if (Session["isTeacher"] != null)
                    //{
                    //int isTeacher;
                    //int.TryParse(Session["isTeacher"].ToString(), out isTeacher);
                    else if (isTeacher == 1)
                    {
                        Session["isTeacher"] = 1;
                        Session["isStudent"] = 0;
                        Session["isAdmin"] = 0;

                        Response.Redirect("~/TeacherPanel.aspx");
                    }
                    // }

                    // else if (Session["isStudent"] != null)
                    //{
                    // int isStudent;
                    //int.TryParse(Session["isStudent"].ToString(), out isStudent);
                    else if (isStudent != 1)
                    {
                        Session["signed_in"] = 0;
                        Session["isTeacher"] = 0;
                        Session["isStudent"] = 0;
                        Session["isAdmin"] = 0;
                        Response.Redirect("~/Login.aspx");
                    }
                    //}
                    // }
                    //                }


                }


            }//handling ends here
            lbl_grade.Text = HttpContext.Current.Request["grade"];
           // }
        }
    }
}