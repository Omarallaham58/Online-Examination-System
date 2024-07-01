using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectV1
{//test
    public partial class StudentByCourse : System.Web.UI.Page
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
                LoadCourses();

                if (DropDownListCourses.Items.Count > 0)
                {
                    // Set the default selected value to the first item in the list
                    DropDownListCourses.SelectedIndex = 0;
                    LoadStudents(); // Automatically load students for the first course
                }
            }

        }

        protected void DropDownListCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DropDownListCourses.SelectedValue))
            {
                LoadStudents();
            }
            else
            {
                GridViewStudents.DataSource = null;
                GridViewStudents.DataBind();
            }
        }

        private void LoadCourses()
        {
            using (SqlConnection con = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                string query = "SELECT id, code FROM Course";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    DropDownListCourses.DataSource = cmd.ExecuteReader();
                    DropDownListCourses.DataTextField = "code";
                    DropDownListCourses.DataValueField = "id";
                    DropDownListCourses.DataBind();
                }
            }
        }

        private void LoadStudents()
        {
            int courseId = int.Parse(DropDownListCourses.SelectedValue);
            DataTable dtStudents = new DataTable();

            using (SqlConnection con = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                string query = @"SELECT u.id, u.firstName, u.lastName, u.email, 
                                CASE WHEN a.hasPassed=1 THEN 1 ELSE 0 END AS hasPassed
                                FROM [User] u
                                LEFT JOIN Assignment a ON u.id = a.student_id AND a.course_id = @CourseId AND a.course_id IS NOT NULL
                                WHERE u.isTeacher = 0 AND a.course_id IS NOT NULL";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtStudents);
                    }
                }
            }

            GridViewStudents.DataSource = dtStudents;
            GridViewStudents.DataBind();
        }

        protected void GridViewStudents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the student ID from the current row's data
                int studentId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "id"));

                // Find the corresponding grade for the student (if it exists)
                float grade = FindGradeForStudent(studentId);

                // Find the label control in the template column
                Label lblGrade = (Label)e.Row.FindControl("gradeLabel");

                // Set the text of the label control to display the grade or 'N/A'
                lblGrade.Text = (grade >= 0) ? grade.ToString() : "N/A";
            }
        }

        private float FindGradeForStudent(int studentId)
        {
            int courseId = int.Parse(DropDownListCourses.SelectedValue);
            Course course = Global.dbHelper.getCourseById(courseId);
            User student = Global.dbHelper.getUserById(studentId);
            float grade = -1;
            //check if user has done an exam
            if (Global.dbHelper.hasDoneExam(student, course) == true)
            {
                //at least student has done an exam
                //get all the exams of this course sorted by date (decreasing order from the newest to the oldest)
                List<Exam> exams = Global.dbHelper.getExamsByCourse(course);
                //get all the grades of this student
                if (exams != null) { 
                student.grades = Global.dbHelper.getGradesByStudent(student);
                //now loop to find the grade of the latest exam done by this student for this course
                if(student.grades != null) { 
                for (int i = 0; i < exams.Count; i++)
                {
                    for (int j = 0; j < student.grades.Count; j++)
                    {
                        if (student.grades[j].exam.id == exams[i].id)
                        {
                            grade = student.grades[j].value;
                            break;
                        }
                    }
                    if (grade != -1) break;
                }
            }
        }
            }

            else
            {
                //no exams
            }

            return grade;

        }
    }
}