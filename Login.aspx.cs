using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectV1
{
    public partial class Login : System.Web.UI.Page
    {
       // public static DBHelper dbHelper;
       protected void Page_Init(object sender, EventArgs e)
        {
            //dbHelper = new DBHelper();
        }
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["signed_in"] != null)
            {
                int signed;
                int.TryParse(Session["signed_in"].ToString(), out signed);
                if (signed == 1)

                {
                    int isAdmin=0, isStudent=0, isTeacher=0;
                    if (Session["isAdmin"]!=null) int.TryParse(Session["isAdmin"].ToString(),out isAdmin);
                    if (Session["isTeacher"] != null) int.TryParse(Session["isTeacher"].ToString(), out isTeacher);
                    if (Session["isStudent"] != null) int.TryParse(Session["isStudent"].ToString(), out isStudent);
                    //already signed in
                   // if (Session["isAdmin"] != null)
                    //{
                      //  int isAdmin;
                        //int.TryParse(Session["isAdmin"].ToString(), out isAdmin);
                        if (isAdmin == 1)
                        {
                            Session["isAdmin"] = 1;
                            Session["isStudent"] = 0;
                            Session["isTeacher"] = 0;
                            Response.Redirect("~/Admin.aspx");
                        }
                  //  }

                   // else if (Session["isTeacher"] != null)
                    //{
                        //int isTeacher;
                      //  int.TryParse(Session["isTeacher"].ToString(), out isTeacher);
                        else if (isTeacher == 1)
                        {
                            Session["isTeacher"] = 1;
                            Session["isStudent"] = 0;
                            Session["isAdmin"] = 0;
                            Response.Redirect("~/TeacherPanel.aspx");
                        }
                    //}

                   // else if (Session["isStudent"] != null)
                    //{
                      //  int isStudent;
                        //int.TryParse(Session["isStudent"].ToString(), out isStudent);
                      else  if (isStudent == 1)
                        {
                            Session["isStudent"] = 1;
                            Session["isAdmin"] = 0;
                            Session["isTeacher"] = 0;
                            Response.Redirect("~/Student.aspx");
                        }
                   // }
                    }
                    //                }


                }

           // }


            if (IsPostBack)
            {
                Session["isTeacher"] = 0;
                Session["isStudent"] = 0;
                Session["isAdmin"] = 0;
                Session["signed_in"] = 0;

                if (usernameTb.Text == Constants.ADMIN_USERNAME && passwordTb.Text == Constants.ADMIN_PASSWORD)
                {
                    Session["signed_in"] = 1;
                    Session["isAdmin"] = 1;
                    Session["isTeacher"] = 0;
                    Session["isStudent"] = 0;
                    Response.Redirect("~/Admin.aspx");
                }
                else
                { //not admin}

                    Account acc = Global.dbHelper.accountExist(usernameTb.Text, passwordTb.Text);
                    if (acc != null)
                    {
                        User user = Global.dbHelper.getUserByAccountId(acc.id);
                        if (user != null)
                        {
                            Session["user"] = user.id;//save id in the session
                            Session["signed_in"] = 1;
                            if (user.isTeacher == 1)
                            {
                                Session["isTeacher"] = 1;
                                Session["isStudent"] = 0;
                                Session["isAdmin"] = 0;
                                Response.Redirect("~/TeacherPanel.aspx");
                            }
                            else
                            {
                                Session["isStudent"] = 1;
                                Session["isTeacher"] = 0;
                                Session["isAdmin"] = 0;
                                Response.Redirect("~/Student.aspx");
                            }
                        }
                        else
                        {
                            //user not found
                            Response.Write("USER NOT FOUND !!!");
                        }
                    }
                    else
                    {
                        Response.ContentType = "text/html";
                        string html = "<p style=\"color:red;\">Invalid credentials, enter a valid username and password to proceed";
                        Response.Write(html);
                        //account not found => invalid credentials
                    }
                }
                /*int id= DBHelper.accountExist(usernameTb.Text, passwordTb.Text)
                if ( id != -1)
                {

                    //user exist
                    User user = dbHelper.getUserByAccountId(id);
                    if (user != null)
                    {
                        Session["user"] = user;
                        Session["signed_in"] = 1;
                        if (user.isTeacher()){
                        
                        Session["isTeacher"]=1;
                        Response.Redirect("~/Teacher.aspx");
                        
                }
                        else{
                        Session["isStudent"]=1;
                        Response.Redirect("~/Student.aspx");
                    }
                }

                else
                {
                    //user not found
                    Response.Write("Invalid credentials, please enter a valid username and password");
                }*/

            }

            //else
            //  {

           /* if (Session["signed_in"] != null)
            {
                int signed;
                int.TryParse(Session["signed_in"].ToString(), out signed);
                if (signed == 1)
                {
                    //already signed in
                    if (Session["isAdmin"] != null)
                    {
                        int isAdmin;
                        int.TryParse(Session["isAdmin"].ToString(), out isAdmin);
                        if (isAdmin == 1)
                        {
                            Session["isStudent"] = 0;
                            Session["isTeacher"] = 0;
                            Response.Redirect("~/Admin.aspx");
                        }

                        if (Session["isTeacher"] != null)
                        {
                            int isTeacher;
                            int.TryParse(Session["isTeacher"].ToString(), out isTeacher);
                            if (isTeacher == 1)
                            {
                                Session["isStudent"] = 0;
                                Session["isAdmin"] = 0;
                                Response.Redirect("~/Teacher.aspx");
                            }
                        }

                        if (Session["isStudent"] != null)
                        {
                            int isStudent;
                            int.TryParse(Session["isStudent"].ToString(), out isStudent);
                            if (isStudent == 1)
                            {
                                Session["isAdmin"] = 0;
                                Session["isTeacher"] = 0;
                                Response.Redirect("~/Student.aspx");
                            }
                        }
                    }
                    //                }


                }

            }*/
        }

        protected void submitBt_Click(object sender, EventArgs e)
        {

        }
    }
}