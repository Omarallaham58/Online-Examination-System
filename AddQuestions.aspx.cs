using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectV1
{
    public partial class AddQuestions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


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


            }

            if (!IsPostBack)
            {
                //getting course from the stored Session value set in the TeacherPanel.aspx
                //from the select form
                int c_id = int.Parse(Session["course"].ToString());
                Course course = Global.dbHelper.getCourseById(c_id);
                lblCourse.Text = course.code + " - " + course.name;

                //the clear fucntion since it's considered as a submit button in the code 
                //so we has to do it this way using "a href"
                resetForm();
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string text = tbQuestion.Text;
            float grade;
            bool a = float.TryParse(tbGrade.Text, out grade);

            int course_id = int.Parse(Session["course"].ToString());
            Course course = Global.dbHelper.getCourseById(course_id);
            List<Option> options = new List<Option>();

            //set the actual of the textboxes and radiobuttons in the add form in the next 2 arrays
            TextBox[] ops = { tbOp1, tbOp2, tbOp3, tbOp4 };
            RadioButton[] rb = { RadioButton1, RadioButton2, RadioButton3, RadioButton4 };
            int isCorrect;

            for (int i = 0; i < 4; i++)
            {
                //iterating over the radiobuttons to get the correct option and set the isCorrect=1
                if (rb[i].Checked)
                {
                    isCorrect = 1;
                }
                else isCorrect = 0;

                //add the option in the options list which will be passed to the question object
                options.Add(new Option(-1, -1, ops[i].Text, isCorrect));
            }
            Question question = new Question(-1, course, text, options, grade);
            if (Global.dbHelper.addQuestion(question) == true)
            {
                //creation succeeded and restting the form
                lblStatus.Text = "Question added successfully";
                lblStatus.ForeColor = System.Drawing.Color.Green;
                tbQuestion.Text = "";
                tbGrade.Text = "";
                tbOp1.Text = "";
                tbOp2.Text = "";
                tbOp3.Text = "";
                tbOp4.Text = "";
                if (RadioButton1.Checked == true)
                    RadioButton1.Checked = false;
                if (RadioButton2.Checked == true)
                    RadioButton2.Checked = false;
                if (RadioButton3.Checked == true)
                    RadioButton3.Checked = false;
                if (RadioButton4.Checked == true)
                    RadioButton4.Checked = false;
                RadioButton1.Checked = true;
            }
            else
            {
                //creation failed
                lblStatus.Text = "Problem occurred while adding the question";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }

        }


        protected void resetForm()
        {
            //resetting the form
            lblStatus.Text = "";
            tbQuestion.Text = "";
            tbGrade.Text = "";
            tbOp1.Text = "";
            tbOp2.Text = "";
            tbOp3.Text = "";
            tbOp4.Text = "";
            if (RadioButton1.Checked == true)
                RadioButton1.Checked = false;
            if (RadioButton2.Checked == true)
                RadioButton2.Checked = false;
            if (RadioButton3.Checked == true)
                RadioButton3.Checked = false;
            if (RadioButton4.Checked == true)
                RadioButton4.Checked = false;
            RadioButton1.Checked = true;
        }


    }
}