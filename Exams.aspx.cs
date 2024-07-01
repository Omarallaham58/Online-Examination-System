using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectV1
{
    public partial class Exams : System.Web.UI.Page
    {

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
            //User student = (User)Session["user"];
            int student_id = 0;
            int.TryParse(Session["user"].ToString(), out student_id);
            User student = Global.dbHelper.getUserById(student_id);
            //get all exams available for this student
            List<Exam> exams = Global.dbHelper.getExamsByStudentId(student.id);


            //display the radio buttons to select an exam
            foreach (Exam exam in exams)
            {
                int isDone = Global.dbHelper.isDoneExam(exam.id, student.id);

                //if the student did not take this exam before  --> display the exam to take
                if (isDone == 0)
                {
                    ListItem radio = new ListItem();
                    radio.Value = exam.id.ToString();
                    //if today is the date of the exam
                    if (exam.date == DateTime.Today)
                    {
                        radio.Text = exam.course.code + ": " + exam.course.name + "<span style='color:green;'> &nbsp; &nbsp;" +
                                exam.date.ToShortDateString() + "</span>";
                    }
                    else
                    {
                        radio.Text = exam.course.code + ": " + exam.course.name + " &nbsp; &nbsp; " + exam.date.ToShortDateString();
                        radio.Enabled = false;
                    }
                    rdbtn_list_exams.Items.Add(radio);
                }
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

            if (Session["timeOut"] != null)
            {
                Response.Redirect("~/ExamSheet.aspx?flag=true&exam_id=" + Session["exam_id"]);//add another parameter to control back 
            }


        }


        protected void btn_take_Click(object sender, EventArgs e)
        {
            //if the student did not select anything and clicked on the button
            if (rdbtn_list_exams.SelectedIndex == -1)
            {
                Response.Write("<p style='color:red;'>Please choose an available exam to take</p>");
            }
            else
            {
                Session["exam_id"] = rdbtn_list_exams.SelectedValue;
                Response.Redirect("~/ExamSheet.aspx?flag=true&exam_id=" + Session["exam_id"]);//add another parameter to control back
            }
        }
    }
}