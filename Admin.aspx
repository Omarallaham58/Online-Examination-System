<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="ProjectV1.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body><br /><br />
    <center><h1>Admin Panel</h1></center>
    <form id="form1" runat="server">
        <br /><br />
        <center>
        <div>
            <a href="ManageStudents.aspx" style="font-size:large;">Manage Students</a><br />
            <a href="ManageTeachers.aspx" style="font-size:large;">Manage Teachers</a><br />
            <a href="ManageCourses.aspx" style="font-size:large;">Manage Courses</a><br /><br />
            <button><a href="LogOut.aspx">Log out</a></button>
        </div>
            </center>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
