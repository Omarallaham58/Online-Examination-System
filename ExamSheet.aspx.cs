using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectV1
{
    public partial class ExamSheet : System.Web.UI.Page
    {


        private Exam exam;
        //list of all radion button lists to access them later
        private List<RadioButtonList> allRdbList;

        protected void Page_Init(object sender, EventArgs e)
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


            int examId;
            //get the exam id
            bool b = int.TryParse(HttpContext.Current.Request["exam_id"], out examId);
            if (b)
            {
                exam = Global.dbHelper.getExamById(examId);
                //the Exam is found
                if (exam != null)
                {
                    if (Session["timeOut"] == null)
                    {
                        //Record the timeOut for this exam session
                        //get duration of exam in hours and minutes
                        int durationHour = exam.duration / 60;
                        int durationMin = exam.duration % 60;

                        //get hour and minutes to add to the current date time
                        int hour = (durationHour + DateTime.Now.Hour) % 24;
                        int min = (durationMin + DateTime.Now.Minute) % 60;

                        DateTime timeOut = new DateTime(exam.date.Year, exam.date.Month, exam.date.Day, hour, min, DateTime.Now.Second);
                        if (min < DateTime.Now.Minute)
                        {
                            timeOut = timeOut.AddHours(1);
                        }
                        if (hour < DateTime.Now.Hour)
                        {
                            timeOut = timeOut.AddDays(1);
                        }

                        Session["timeOut"] = timeOut;
                    }

                    //fill the header of the exam sheet
                    lbl_name.Text = exam.course.name;
                    lbl_code.Text = exam.course.code;
                    lbl_date.Text = exam.date.ToShortDateString();
                    User teacher = exam.course.teacher;
                    lbl_teacher.Text = teacher.firstName + " " + teacher.lastName;

                    //get all questions for this exam
                    List<Question> questions = exam.questions;
                    int questionNb = 1;

                    //list of all radio button lists to access them later
                    allRdbList = new List<RadioButtonList>();

                    //display all questions for this exam with their options
                    foreach (Question question in questions)
                    {
                        //display the question
                        Label quesText = new Label();
                        quesText.Text = "<br><b>" + questionNb + ". " + question.text + "</b>(" + question.grade + " pts)";
                        form1.Controls.Add(quesText);

                        RadioButtonList rdbList = new RadioButtonList();
                        //display all options for this question
                        foreach (Option option in question.options)
                        {
                            ListItem radio = new ListItem();
                            radio.Text = option.text;
                            //if this is the correct option for this question
                            if (option.isCorrect == 1)
                            {
                                radio.Value = question.grade.ToString();
                            }
                            //if this is the wrong option
                            else
                            {
                                radio.Value = "0";
                            }
                            //add this ListItem to the RadioButtonList
                            rdbList.Items.Add(radio);
                        }
                        //add this RadioButtonList to the form
                        form1.Controls.Add(rdbList);
                        questionNb++;
                        //add this RadioButtonList to the List
                        allRdbList.Add(rdbList);
                    }

                    //display button to complete the exam
                    Button finishBtn = new Button();
                    finishBtn.ID = "finishBtn";
                    //add the handler to OnClick event for this button
                    finishBtn.Click += new EventHandler(finishBtn_Click);
                    finishBtn.Text = "Finish the Exam";
                    finishBtn.ForeColor = System.Drawing.Color.Red;
                    finishBtn.Font.Size = FontUnit.Large;
                    finishBtn.Style.Add("margin-top", "15px");

                    //add this button to the form
                    form1.Controls.Add(finishBtn);
                }
            }
            //incorrect exam id
            else
            {
                Response.Redirect("~/Exams.aspx");
            }

        }

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

            if (Session["timeOut"] == null)
            {
                Response.Redirect("~/Exam.aspx");
            }

            /*add if ! ispostback => get the flag parameter => if null then the student
             returned from grade page=>return to student home page*/
            string flag;
            if (!IsPostBack)
            {
                 flag = Request.QueryString["flag"];
                if (String.IsNullOrEmpty(flag))
                {
                    Response.Redirect("~/Student.aspx");
                }
            }

             flag = Request.QueryString["flag"];
            if (String.IsNullOrEmpty(flag))
            {
                Response.Redirect("~/Student.aspx");
            }

        }

        protected void finishBtn_Click(object sender, EventArgs e)
        {
            double grade = 0;
            //traverse all questions
            foreach (RadioButtonList rdbList in allRdbList)
            {
                //if the student answered for this question
                if (rdbList.SelectedIndex != -1)
                {
                    grade += Convert.ToDouble(rdbList.SelectedValue);
                }
            }

            //calculate the final grade for this exam
            grade = (grade * 100) / exam.grade;

            Session["timeOut"] = null;

            //User student = (User)Session["user"];
            int student_id = 0;
            int.TryParse(Session["user"].ToString(), out student_id);
            User student = Global.dbHelper.getUserById(student_id);
            Grade g = new Grade(-1, exam, student, (float)grade);
            //insert this grade in DB
            Global.dbHelper.addGrade(g);

            //redirect the student to see the final grade
            Response.Redirect("~/displayGrade.aspx?grade=" + grade.ToString("0.00"));
        }
    }
}