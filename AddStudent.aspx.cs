using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectV1
{
    public partial class AddStudent : System.Web.UI.Page
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
                resetForm();
            }
           /* if (IsPostBack)
            {
                //if (IsPostBackEventControl(this.addStudentBt))
                //{
                    DateTime date;
                    DateTime.TryParse(dobTb.Text, out date);
                    User user = new User(-1, null, firstNameTb.Text, lastNameTb.Text, date, emailTb.Text, phoneNumberTb.Text, 0);
                    //if (Global.dbHelper.UserExist(User) == false)
                    //{
                    //does not exist
                    //create the username and password of the account
                    string formattedDate = date.ToString("yyyy-MM-dd");
                    char[] formattedDate2 = formattedDate.ToCharArray();
                    int nb = ProjectV1.User.GetCurrentNumberOfUsers();
                    //string username = "";
                    // username += firstNameTb.Text.ToCharArray()[0] + lastNameTb.Text.ToCharArray()[0] + formattedDate2[8] + formattedDate2[9] + formattedDate2[5] + formattedDate2[6] + formattedDate2[0] + formattedDate2[1] + formattedDate2[2] + formattedDate2[3]+nb.ToString();
                    char[] usernameArray = { firstNameTb.Text.ToCharArray()[0], lastNameTb.Text.ToCharArray()[0], formattedDate2[8], formattedDate2[9], formattedDate2[5], formattedDate2[6], formattedDate2[0], formattedDate2[1], formattedDate2[2], formattedDate2[3] };
                    string username = new string(usernameArray, 0, usernameArray.Length);
                    username += nb.ToString();
                    Random rand = new Random();
                    int k = rand.Next(firstNameTb.Text.ToCharArray().Length);
                    int k2 = rand.Next(lastNameTb.Text.ToCharArray().Length);

                    char[] passwdArray = { firstNameTb.Text.ToCharArray()[k], lastNameTb.Text.ToCharArray()[k2], formattedDate2[8], formattedDate2[9], formattedDate2[5], formattedDate2[6], formattedDate2[0], formattedDate2[1], formattedDate2[2], formattedDate2[3] };
                    string password = new string(passwdArray, 0, passwdArray.Length);
                    password += nb.ToString();
                    password = Constants.Shuffle(password);
                    Account account = new Account(-1, username, password);


                    Response.ContentType = "text/html";
                    string html = "<p style=\"color:green;\">Student Added Successfully !</p>";
                    Response.Write(html);
                   // resetBt_Click(sender, e);


                    //int id = Global.dbHelper.AddAccount(account);
                    //if (id != -1)
                    //{
                    //account added
                    //}
                    //else
                    //{
                    //account not added
                    //}
                    // Response.Write(username);
                    // Response.Write(password);
                    //}
                    //else
                    //{
                    //already exists 
                    //}

               // }
            }*/
        }

        protected void addStudentBt_Click(object sender, EventArgs e)
        {

        }
        protected void addStudent()
        {
            DateTime date;
            DateTime.TryParse(dobTb.Text, out date);
            User user = new User(-1, null, firstNameTb.Text, lastNameTb.Text, date, emailTb.Text, phoneNumberTb.Text, 0);
            if (Global.dbHelper.UserExist(user) == false)
            {
                //does not exist
                //create the username and password of the account
                string formattedDate = date.ToString("yyyy-MM-dd");
                char[] formattedDate2 = formattedDate.ToCharArray();
                int nb = ProjectV1.User.GetCurrentNumberOfUsers();
                //string username = "";
                // username += firstNameTb.Text.ToCharArray()[0] + lastNameTb.Text.ToCharArray()[0] + formattedDate2[8] + formattedDate2[9] + formattedDate2[5] + formattedDate2[6] + formattedDate2[0] + formattedDate2[1] + formattedDate2[2] + formattedDate2[3]+nb.ToString();
                char[] usernameArray = { firstNameTb.Text.ToCharArray()[0], lastNameTb.Text.ToCharArray()[0], formattedDate2[8], formattedDate2[9], formattedDate2[5], formattedDate2[6], formattedDate2[0], formattedDate2[1], formattedDate2[2], formattedDate2[3] };
                string username = new string(usernameArray, 0, usernameArray.Length);
                username += nb.ToString();
                Random rand = new Random();
                int k = rand.Next(firstNameTb.Text.ToCharArray().Length);
                int k2 = rand.Next(lastNameTb.Text.ToCharArray().Length);

                char[] passwdArray = { firstNameTb.Text.ToCharArray()[k], lastNameTb.Text.ToCharArray()[k2], formattedDate2[8], formattedDate2[9], formattedDate2[5], formattedDate2[6], formattedDate2[0], formattedDate2[1], formattedDate2[2], formattedDate2[3] };
                string password = new string(passwdArray, 0, passwdArray.Length);
                password += nb.ToString();
                password = Constants.Shuffle(password);
                Account account = new Account(-1, username, password);




                int id = Global.dbHelper.AddAccount(account);
                if (id != -1)
                {
                    //account added
                    account.id = id;
                user.account = account;
                    if (Global.dbHelper.AddUser(user))
                    {
                        //user added
                        Response.ContentType = "text/html";
                        string html = "<p style=\"color:green;\">Student Added Successfully !</p>";
                        Response.Write(html);
                        resetForm();

                    }
                    else
                    {
                        //user not added
                        Response.ContentType = "text/html";
                        string errormsg1 = "<p style=\"color:red;\">User not Added </p>";
                        Response.Write(errormsg1);
                       // resetForm();
                    }
                }
                else
                {

                    //account not added
                    Response.ContentType = "text/html";
                    string errormsg2 = "<p style=\"color:red;\">Account could not be added.</p>";
                    Response.Write(errormsg2);
                   // resetForm();
                }





                /*Response.ContentType = "text/html";
                string html = "<p style=\"color:green;\">Student Added Successfully !</p>";
                Response.Write(html);
                resetForm();*/
              

            }
            else
            {
                //user already exist
                Response.ContentType = "text/html";
                string errormsg3 = "<p style=\"color:red;\">User already exists!!!</p>";
                Response.Write(errormsg3);
            }
        }

            protected void resetForm()
        {
            firstNameTb.Text = "";
            lastNameTb.Text = "";
            dobTb.Text = "";
            emailTb.Text = "";
            phoneNumberTb.Text = "";
        }

        protected void resetBt_Click(object sender, EventArgs e)
        {
            firstNameTb.Text = "";
            lastNameTb.Text = "";
            dobTb.Text = "";
            emailTb.Text = "";
            phoneNumberTb.Text = "";
        }

        protected void ButtonClicked(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string commandName = clickedButton.CommandName;
            switch (commandName)
            {
                case "AddStudent":
                    addStudent();
                    break;
                case "ResetButton":
                    resetForm();
                    break;
                default: break;
            }
        }

       /* private bool IsPostBackEventControl(Control control)
        {
            string postBackControlName = Page.Request.Params.Get("__EVENTTARGET");
            return !string.IsNullOrEmpty(postBackControlName) && postBackControlName.EndsWith(control.UniqueID);
        }*/
    }
}