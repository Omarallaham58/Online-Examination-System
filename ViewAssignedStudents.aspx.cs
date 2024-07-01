using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectV1
{
    public partial class ViewAssignedStudents : System.Web.UI.Page
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
              /*  LoadCourses();

                if (DropDownListCourses.Items.Count > 0)
                {
                    // Set the default selected value to the first item in the list
                    DropDownListCourses.SelectedIndex = 0;
                    LoadStudents(); // Automatically load students for the first course
                }*/
            }
            LoadStudents();
        }


        /*protected void DropDownListCourses_SelectedIndexChanged(object sender, EventArgs e)
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
        }*/

        /*private void LoadCourses()
        {
            //int courseId = int.Parse(DropDownListCourses.SelectedValue);
            using (SqlConnection con = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                string query = "SELECT id, code FROM Course WHERE teacher_id=@teacher_id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@teacher_id", int.Parse(Session["user"].ToString()));
                    con.Open();
                    DropDownListCourses.DataSource = cmd.ExecuteReader();
                    DropDownListCourses.DataTextField = "code";
                    DropDownListCourses.DataValueField = "id";
                    DropDownListCourses.DataBind();
                }
            }
        }*/

        private void LoadStudents()
        {
            //int courseId = int.Parse(DropDownListCourses.SelectedValue);
            int courseId = int.Parse(Session["course"].ToString());
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
            // int courseId = int.Parse(DropDownListCourses.SelectedValue);
            int courseId = int.Parse(Session["course"].ToString());
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
                if (exams != null)
                {
                    student.grades = Global.dbHelper.getGradesByStudent(student);
                    if (student.grades != null)
                    {
                        //now loop to find the grade of the latest exam done by this student for this course
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