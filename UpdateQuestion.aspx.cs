using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectV1
{
    public partial class UpdateQuestion : System.Web.UI.Page
    {
        int id = 0;
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


            if (!IsPostBack)
            {
                //getting the question's id sent with the link from the ManageQuestions.aspx
                if (Request.QueryString["id"] != null)
                {
                    id = Convert.ToInt32(Request.QueryString["id"]);
                    Question question = Global.dbHelper.getQuestionById(id);
                    if (question != null)
                    {
                        //setting the question and options values in their corresponding fields so the
                        //doctor is able to update them without entering everything another time
                        tbQuestion.Text = question.text;
                        tbGrade.Text = question.grade.ToString();
                        List<Option> options = question.options;
                        TextBox[] ops = { tbOp1, tbOp2, tbOp3, tbOp4 };
                        RadioButton[] rb = { RadioButton1, RadioButton2, RadioButton3, RadioButton4 };
                        int i = 0;
                        foreach (Option option in options)
                        {
                            ops[i].Text = option.text;
                            if (option.isCorrect == 1)
                            {
                                rb[i].Checked = true;
                            }
                            i++;
                        }
                    }
                    //resend the question's id value but in a hidden form
                    hidden_id.Value = id.ToString();
                }

            }
            else
            {
                lblStatus.Text = "Couldn't get the question to update!";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void btnUpdate_Click1(object sender, EventArgs e)
        {
            int id = int.Parse(hidden_id.Value);
            if (id != 0)
            {
                //getting the original options value for the question to edit on them 
                List<Option> edited_options = Global.dbHelper.getOptionsByQuestionId(id);
                string text = tbQuestion.Text;
                float grade;
                bool a = float.TryParse(tbGrade.Text, out grade);

                int course_id = int.Parse(Session["course"].ToString());
                Course course = Global.dbHelper.getCourseById(course_id);

                //set the actual of the textboxes and radiobuttons in the add form in the next 2 arrays
                TextBox[] ops = { tbOp1, tbOp2, tbOp3, tbOp4 };
                RadioButton[] rb = { RadioButton1, RadioButton2, RadioButton3, RadioButton4 };
                int isCorrect;
                int i = 0;
                foreach (Option option in edited_options)
                {
                    //iterating over the radiobuttons to get the correct option and set the isCorrect=1
                    if (rb[i].Checked)
                    {
                        isCorrect = 1;
                    }
                    else isCorrect = 0;
                    option.text = ops[i].Text;
                    option.isCorrect = isCorrect;
                    i++;
                }
                //pass the edited options list to the question to set the new values along with the question's
                //new fields' values
                Question question = new Question(id, course, text, edited_options, grade);
                if (Global.dbHelper.updateQuestion(question) == true)
                {
                    lblStatus.Text = "Question updated successfully";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblStatus.Text = "Error Occurred while updating the question!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
        }


    }
}