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
    public partial class RegisterStudentsInCourse : System.Web.UI.Page
    {
        /* protected void Page_Load(object sender, EventArgs e)
         {
             if (!IsPostBack)
             {
                 // Load the students only when the page is first loaded
                 LoadStudents();
                 //DropDownListCourses.Items.Insert(0, "--course--");
             }


         }

         protected void DropDownListCourses_SelectedIndexChanged(object sender, EventArgs e)
         {
             // Load the students whenever a course is selected
             if (DropDownListCourses.SelectedValue != "0")
             {
                 LoadStudents();
             }
            // LoadStudents();
         }


         private void LoadStudents()
         {
             // DropDownListCourses.SelectedIndex = 0;

             int courseId;
             bool flag= int.TryParse(DropDownListCourses.SelectedValue.ToString(),out courseId);
             if (flag == true)
             {
                 DataTable dtStudents = new DataTable();

                 using (SqlConnection con = new SqlConnection(Constants.DB_CONNECTION_STRING))
                 {
                     string query = @"SELECT u.id, u.firstName, u.lastName, u.email, 
                                 CASE WHEN a.course_id IS NOT NULL THEN 1 ELSE 0 END AS isEnrolled
                                 FROM [User] u
                                 LEFT JOIN Assignment a ON u.id = a.student_id AND a.course_id = @CourseId
                                 WHERE u.isTeacher = 0";
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
         }

         protected void ButtonSave_Click(object sender, EventArgs e)
         {
             int courseId = int.Parse(DropDownListCourses.SelectedValue);

             foreach (GridViewRow row in GridViewStudents.Rows)
             {
                 int studentId = int.Parse(GridViewStudents.DataKeys[row.RowIndex].Value.ToString());
                 CheckBox chkEnrolled = (CheckBox)row.FindControl("CheckBoxEnrolled");

                 using (SqlConnection con = new SqlConnection(Constants.DB_CONNECTION_STRING))
                 {
                     con.Open();
                     SqlCommand cmd;
                     if (chkEnrolled.Checked)
                     {
                         // Insert if not already enrolled
                         string checkQuery = "SELECT COUNT(*) FROM Assignment WHERE course_id = @CourseId AND student_id = @StudentId";
                         cmd = new SqlCommand(checkQuery, con);
                         cmd.Parameters.AddWithValue("@CourseId", courseId);
                         cmd.Parameters.AddWithValue("@StudentId", studentId);

                         int count = (int)cmd.ExecuteScalar();
                         if (count == 0)
                         {
                             string insertQuery = "INSERT INTO Assignment (course_id, student_id, hasPassed) VALUES (@CourseId, @StudentId, 0)";
                             cmd = new SqlCommand(insertQuery, con);
                             cmd.Parameters.AddWithValue("@CourseId", courseId);
                             cmd.Parameters.AddWithValue("@StudentId", studentId);
                             cmd.ExecuteNonQuery();
                         }
                     }
                     else
                     {
                         // Delete if enrolled
                         string deleteQuery = "DELETE FROM Assignment WHERE course_id = @CourseId AND student_id = @StudentId";
                         cmd = new SqlCommand(deleteQuery, con);
                         cmd.Parameters.AddWithValue("@CourseId", courseId);
                         cmd.Parameters.AddWithValue("@StudentId", studentId);
                         cmd.ExecuteNonQuery();
                     }
                 }
             }

             // Reload the students to reflect changes
             LoadStudents();
         }*/

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

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            int courseId = int.Parse(DropDownListCourses.SelectedValue);
            Course course = Global.dbHelper.getCourseById(courseId);

            foreach (GridViewRow row in GridViewStudents.Rows)
            {
                int studentId = int.Parse(GridViewStudents.DataKeys[row.RowIndex].Value.ToString());
                CheckBox chkEnrolled = (CheckBox)row.FindControl("CheckBoxEnrolled");

                using (SqlConnection con = new SqlConnection(Constants.DB_CONNECTION_STRING))
                {
                    con.Open();
                    SqlCommand cmd;
                    if (chkEnrolled.Checked)
                    {
                        // Insert if not already enrolled
                        string checkQuery = "SELECT COUNT(*) FROM Assignment WHERE course_id = @CourseId AND student_id = @StudentId";
                        cmd = new SqlCommand(checkQuery, con);
                        cmd.Parameters.AddWithValue("@CourseId", courseId);
                        cmd.Parameters.AddWithValue("@StudentId", studentId);

                        int count = (int)cmd.ExecuteScalar();
                        if (count == 0)
                        {//inserting
                            string insertQuery = "INSERT INTO Assignment (course_id, student_id, hasPassed) VALUES (@CourseId, @StudentId, 0)";
                            cmd = new SqlCommand(insertQuery, con);
                            cmd.Parameters.AddWithValue("@CourseId", courseId);
                            cmd.Parameters.AddWithValue("@StudentId", studentId);
                            if(cmd.ExecuteNonQuery()>0)
                            course.nbStudents++;//add number of students enrolled in this course
                        }
                    }
                    else
                    {
                        // Delete if enrolled
                        string deleteQuery = "DELETE FROM Assignment WHERE course_id = @CourseId AND student_id = @StudentId AND hasPassed=0";
                        // AND isPassed=0
                        //so this means that only failed assignment will be deleted but the user will be checked
                        cmd = new SqlCommand(deleteQuery, con);
                        cmd.Parameters.AddWithValue("@CourseId", courseId);
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                       if( cmd.ExecuteNonQuery()>0) course.nbStudents--; //decrement nb of students if one not assigned
                    }
                }
            }

            if (Global.dbHelper.updateCourse(course) == false)
            {
                Response.Write("Failed to update course");
            }
            else
            {
                /* Response.ContentType = "text/html";
                 string html = "<p style=\"color:green;\"><strong>Students Assigned Successfully</strong></p>";
                 Response.Write(html);*/
                Label1.Text = "Students Assigned Successfully";
            }
            // Reload the students to reflect changes
            LoadStudents();
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
                                CASE WHEN a.course_id IS NOT NULL THEN 1 ELSE 0 END AS isEnrolled
                                FROM [User] u
                                LEFT JOIN Assignment a ON u.id = a.student_id AND a.course_id = @CourseId
                                WHERE u.isTeacher = 0";
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

    }
}