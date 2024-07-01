using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectV1
{
    public partial class CreateExam : System.Web.UI.Page
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


            if (!IsPostBack)
            {
                //the clear fucntion since it's considered as a submit button in the code 
                //so we has to do it this way using "a href"
                resetForm();
            }
            //condition to know if there exists any questions in this course
            if ((Global.dbHelper.getNbQuestionsPerCourse(int.Parse(Session["course"].ToString()))) != 0)
            {
                //getting course from the stored Session value set in the TeacherPanel.aspx
                //from the select form
                int course_id = int.Parse(Session["course"].ToString());
                Course course = Global.dbHelper.getCourseById(course_id);
                lblCourse.Text = String.Format(" ' {0} - {1} ' ", course.code, course.name);

                qTable.BorderStyle = BorderStyle.Solid;
                qTable.GridLines = GridLines.Both;

                //create fields (table and header of the table) that'll contain the questions, checkboxes and options

                TableRow headerRow = new TableRow();
                headerRow.BorderStyle = BorderStyle.Solid;
                headerRow.BorderWidth = 1;

                TableCell qHeaderCell = new TableCell();
                qHeaderCell.BorderStyle = BorderStyle.Solid;
                qHeaderCell.BorderWidth = 1;

                Label qHeaderLabel = new Label();
                qHeaderLabel.Text = "Question";
                qHeaderLabel.Font.Bold = true;
                qHeaderCell.Controls.Add(qHeaderLabel);
                headerRow.Controls.Add(qHeaderCell);

                TableCell oHeaderCell = new TableCell();
                oHeaderCell.BorderStyle = BorderStyle.Solid;
                oHeaderCell.BorderWidth = 1;

                Label oHeaderLabel = new Label();
                oHeaderLabel.Text = "Options";
                oHeaderLabel.Font.Bold = true;
                oHeaderCell.Controls.Add(oHeaderLabel);
                headerRow.Controls.Add(oHeaderCell);

                qTable.Controls.Add(headerRow);

                //create fields dynamically based on the number of questions existing in this coures

                List<Question> questions = Global.dbHelper.getQuestionsByCourseId(course_id);
                foreach (Question question in questions)
                {
                    TableRow questionRow = new TableRow();
                    questionRow.BorderStyle = BorderStyle.Solid;
                    questionRow.BorderWidth = 1;

                    TableCell qLabelCell = new TableCell();
                    qLabelCell.BorderStyle = BorderStyle.Solid;
                    qLabelCell.BorderWidth = 1;

                    CheckBox cb = new CheckBox();
                    cb.ID = question.id.ToString();
                    cb.ValidationGroup = "cb";

                    Label qLabel = new Label();
                    qLabel.Text = question.text;

                    qLabelCell.Controls.Add(cb);
                    qLabelCell.Controls.Add(qLabel);

                    TableCell optionCell = new TableCell();
                    optionCell.BorderStyle = BorderStyle.Solid;
                    optionCell.BorderWidth = 1;
                    List<Option> options = Global.dbHelper.getOptionsByQuestionId(question.id);
                    //creating the fields of options dynamically for each question

                    foreach (Option option in options)
                    {
                        Label optionLabel = new Label();
                        optionLabel.Text = option.text;
                        optionCell.Controls.Add(optionLabel);
                        optionCell.Controls.Add(new LiteralControl("<br>"));
                    }

                    questionRow.Controls.Add(qLabelCell);
                    questionRow.Controls.Add(optionCell);
                    qTable.Controls.Add(questionRow);
                }
            }
            else
            {
                //not a single question in this course so we set the error message ans disable the fields
                lblStatus.Text = "You must have at least one question to create an exam!";
                lblStatus.ForeColor = Color.Red;
                lblDate.Text = "";
                tbDate.Enabled = false;
                tbDuration.Enabled = false;
                submit.Enabled = false;
            }

        }


        protected void submit_Click(object sender, EventArgs e)
        {
            int flag = 1; //used later to proceed with saving if no error has occureed in the processing of data
            int checkedCount = 0;
            List<int> checkedValues = new List<int>();

            //getting the list of checked checkboxes
            foreach (Control control in qTable.Controls)
            {
                if (control is TableRow)
                {
                    foreach (Control cellControl in ((TableRow)control).Controls)
                    {
                        if (cellControl is TableCell)
                        {
                            foreach (Control innerControl in ((TableCell)cellControl).Controls)
                            {
                                if (innerControl is CheckBox)
                                {
                                    CheckBox cb = (CheckBox)innerControl;
                                    if (cb.Checked)
                                    {
                                        //increment count of checked checkboxes to know if any was selected
                                        checkedCount++;
                                        checkedValues.Add(int.Parse(cb.ID));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //if no checkbox is checked set flag=0 to not proceed with saving
            if (checkedCount == 0)
            {
                lblStatus.Text = "At least one checkbox should be selected!";
                lblStatus.ForeColor = Color.Red;
                flag = 0;
            }
            else lblStatus.Text = "";

            DateTime date;
            DateTime.TryParse(tbDate.Text, out date);

            //if date preceed today's date we set flag=0 to not proceed with saving
            if (date < DateTime.Today.Date)
            {
                lblDate.Text = "Exam date cannot preceed today's date!";
                lblDate.ForeColor = Color.Red;
                flag = 0;
            }

            if (flag == 1) //no errors
            {
                int duration;
                Course course = Global.dbHelper.getCourseById(int.Parse(Session["course"].ToString()));
                bool a = int.TryParse(tbDuration.Text, out duration);
                float grade = 0;
                List<Question> questions = new List<Question>();
                int nbQuestions = checkedCount;
                foreach (int value in checkedValues)
                {
                    //getting questions checked by iterating over the list of checked values previously handled
                    Question question = Global.dbHelper.getQuestionById(value);
                    questions.Add(question);
                    grade += question.grade; //incrementing the overall exam's grade by adding the grade of each question each time
                }
                Exam exam = new Exam(-1, course, date, duration, grade, questions, nbQuestions);
                if (Global.dbHelper.addExam(exam) == true)
                {
                    //creation succeeded meaage and resetting the form
                    lblStatus.Text = "Exam Created Successfully!";
                    lblStatus.ForeColor = Color.Green;
                    lblDate.Text = "pick an exam date (must be after today's date)";
                    lblDate.ForeColor = Color.Blue;
                    tbDate.Text = "";
                    tbDuration.Text = "";
                    foreach (TableRow row in qTable.Rows)
                    {
                        if (row.Cells.Count > 0)
                        {
                            foreach (Control control in row.Cells[0].Controls)
                            {
                                if (control is CheckBox cb)
                                {
                                    cb.Checked = false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblStatus.Text = "Error occurred while creating the exam!";
                    lblStatus.ForeColor = Color.Red;
                }

            }
        }

        protected void resetForm()
        {
            //resetting the form
            lblStatus.Text = "";
            lblDate.Text = "pick an exam date (must be after today's date)";
            lblDate.ForeColor = Color.Blue;
            tbDate.Text = "";
            tbDuration.Text = "";
            foreach (TableRow row in qTable.Rows)
            {
                if (row.Cells.Count > 0)
                {
                    foreach (Control control in row.Cells[0].Controls)
                    {
                        if (control is CheckBox cb)
                        {
                            cb.Checked = false;
                        }
                    }
                }
            }
        }
    }
}