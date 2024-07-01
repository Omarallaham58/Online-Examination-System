using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectV1
{
    public partial class AssignCourseToTeacher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

            if (IsPostBack)
            {
               // Response.Write("Submitted");
            }
        }


        protected void ButtonClicked(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string commandName = clickedButton.CommandName;
            switch (commandName)
            {
                case "selectCourse":
                    //addTeacher();
                    courseSelected();
                    break;
                case "selectTeacher":
                    teacherSelected();
                    break;
                default: break;
            }
        }

        protected void courseSelected()
        {

            string code = DropDownList1.SelectedItem.ToString();
            Course course = Global.dbHelper.getCourseByCode(code);
            if (course != null)
            {
                //course found
                //if (course.teacher.id != -1)
                if(course.teacher!=null)
                {
                    //already there is a teacher => display div1 and div2
                    teacherTb0.Text=course.teacher.firstName + " " + course.teacher.lastName;
                   // Response.ContentType = "text/html";
                    string script = "<script type=\"text/javascript\"> " +
                        " document.getElementById('div1').style.display = 'block';" +
                        " document.getElementById('div2').style.display = 'block';" +
                        " </script>";
                    //Response.Write(html);
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowDivs", script);

                }
                else
                {
                    //no teacher yet
                    //Response.ContentType="text/html";
                    string script= "<script type=\"text/javascript\"> " +
                        " document.getElementById('div3').style.display = 'block';" +
                        " document.getElementById('div4').style.display = 'block';" +
                        " </script>";
                    //Response.Write(html);
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowDivs", script);
                }
            }
            else
            {
                //course not found
                string errormsg1 = "<p style=\"color:red;\"><strong>Course could not be found !!! </strong></p>";
                Response.ContentType="text/html";
                Response.Write(errormsg1);
            }

        }

        protected void teacherSelected()
        {
            //now teacher is selected => get course => update it
            int teacher_id = int.Parse(DropDownListTeachers.SelectedValue.ToString());
            string code = DropDownList1.SelectedValue.ToString();
            Course course = Global.dbHelper.getCourseByCode(code);
            if (course != null)
            {
                //course found
                //get the teacher
                User teacher = Global.dbHelper.getUserById(teacher_id);
                if(teacher != null)
                {
                        //user found
                        //set it to the course and update
                        course.teacher= teacher;
                    if (Global.dbHelper.updateCourse(course))
                    {
                        //course updated successfully
                        string msg = "<p style=\"color:green;\"><strong>Teacher assigned successfully</strong></p>";
                        Response.ContentType = "text/html";
                        Response.Write(msg);
                    }

                    else
                    {
                        //update failed
                        string msg = "<p style=\"color:red;\"><strong>operation failed !!</strong></p>";
                        Response.ContentType = "text/html";
                        Response.Write(msg);
                    }

                }

                else
                {
                    //user not found
                    string msg = "<p style=\"color:red;\"><strong>Teacher could not be found !!</strong></p>";
                    Response.ContentType = "text/html";
                    Response.Write(msg);

                }
            }

            else
            {
                //course not found
                string msg = "<p style=\"color:red;\"><strong>Course could not be found !!</strong></p>";
                Response.ContentType = "text/html";
                Response.Write(msg);

            }



        }
    }
}