using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectV1
{
    public partial class ManageQuestions : System.Web.UI.Page
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
                //signed in, but make sure it's the teacher
                if (Session["isTeacher"] != null)
                {
                    int isTeacher;
                    int.TryParse(Session["isTeacher"].ToString(), out isTeacher);
                    if (isTeacher != 1)
                    {
                        //it's not the admin, log out and go to sign in
                        Session["signed_in"] = 0;
                        Session["isTeacher"] = 0;
                        Session["isAdmin"] = 0;
                        Session["isStudent"] = 0;
                        Response.Redirect("~/Login.aspx");
                    }
                }
            }

            //--------------------------------------
            if (Session["signed_in"] != null)
            {
                int signed;
                int.TryParse(Session["signed_in"].ToString(), out signed);
                if (signed == 1)
                {
                    int isTeacher = 0, isAdmin = 0, isStudent = 0;
                    if (Session["isAdmin"] != null) int.TryParse(Session["isAdmin"].ToString(), out isAdmin);
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
                        Session["isTeacher"] = 0;
                        Session["isStudent"] = 0;
                        Response.Redirect("~/Admin.aspx");

                    }
                    //}

                    //else if (Session["isStudent"] != null)
                    //{
                    //int isStudent;
                    //int.TryParse(Session["isStudent"].ToString(), out isStudent);
                    else if (isStudent == 1)
                    {
                        Session["isAdmin"] = 0;
                        Session["isTeacher"] = 0;
                        Session["isStudent"] = 1;

                        Response.Redirect("~/Student.aspx");

                    }
                    //  }

                    // else  if (Session["isTeacher"] != null)
                    //{
                    // int isTeacher;
                    //int.TryParse(Session["isTeacher"].ToString(), out isTeacher);
                    else if (isTeacher != 1)
                    {

                        Session["isAdmin"] = 0;
                        Session["isTeacher"] = 0;
                        Session["isStudent"] = 0;
                        Response.Redirect("~/Login.aspx");
                    }
                    // }


                }
                //                }


            }//handling ends here

            Course course = Global.dbHelper.getCourseById(int.Parse(Session["course"].ToString()));
            lblCourse.Text = course.code + " - " + course.name;
            if (Global.dbHelper.getNbQuestionsPerCourse(int.Parse(Session["course"].ToString())) == 0)
            {
                //set an error label to visible if the course doesn't contain any question
                lblStatus.Visible = true;
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (Global.dbHelper.removeQuestion(id) == false)
                {
                    //if deletion didn't work that means the question we're attempting to delete is assigned to an
                    //upcoming exaam. Hence it's not allowed to delete the question
                    lblStatus.Text = "Can't delete this question since it's been assigned to an upcoming exam!";
                    lblStatus.Visible = true;
                }
                else Response.Redirect("ManageQuestions.aspx");
            }
            else if (e.CommandName == "UpdateRow")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                //redirect to the update page with the question's id value
                Response.Redirect("UpdateQuestion.aspx?id=" + id);
            }
            else if (e.CommandName == "OptionsRow")
            {
                //panelOptions contains the structure of the question's options information but set to invisible
                //when clicking this button the otpion will be displayed by setting visibility to true
                panelOptions.Visible = true;

                int id = Convert.ToInt32(e.CommandArgument);
                List<Option> options = Global.dbHelper.getOptionsByQuestionId(id);
                lblQuestion.Text = id.ToString();
                //labels that will contains the options' texts
                Label[] labels = { lblOp1, lblOp2, lblOp3, lblOp4 };
                int i = 0;
                foreach (Option option in options)
                {
                    labels[i].BackColor = System.Drawing.Color.White;
                    labels[i].Text = option.text;
                    if (option.isCorrect == 1)
                    {
                        //highlighting correct answer
                        labels[i].BackColor = System.Drawing.Color.Green;
                    }
                    i++;
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //closing the panel of options by setting it to invisible and resetting the labels and colors
            panelOptions.Visible = false;
            lblQuestion.Text = "";
            lblOp1.Text = "";
            lblOp2.Text = "";
            lblOp3.Text = "";
            lblOp4.Text = "";
            lblOp1.BackColor = System.Drawing.Color.White;
            lblOp2.BackColor = System.Drawing.Color.White;
            lblOp3.BackColor = System.Drawing.Color.White;
            lblOp4.BackColor = System.Drawing.Color.White;
        }

    }
}