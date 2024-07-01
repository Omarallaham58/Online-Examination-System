<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignCourseToTeacher.aspx.cs" Inherits="ProjectV1.AssignCourseToTeacher" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assign Course To Teacher</title>
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            min-height: 100vh;
            background-color: #f5f5f5;
        }
        .container {
            text-align: center;
            padding: 20px;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        h1 {
            margin-bottom: 20px;
        }
        .form-group {
            margin-bottom: 15px;
            text-align: left;
        }
        .form-group label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
        }
        .form-group select,
        .form-group input {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .form-group .error-message {
            color: red;
            font-size: 0.9em;
        }
        .button-group {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-top: 20px;
        }
        .button-group a,
        .button-group button {
            padding: 10px 20px;
            background-color: #007bff;
            border: none;
            color: #fff;
            font-size: 16px;
            border-radius: 4px;
            text-decoration: none;
            cursor: pointer;
        }
        .button-group button:hover,
        .button-group a:hover {
            background-color: #0056b3;
        }
        .hidden {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function openForm() {
            document.getElementById('div3').style.display = 'block';
            document.getElementById('div4').style.display = 'block';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Assign Course To Teacher</h1>
            <div class="form-group">
                <label for="teacherTb">Teacher</label>
                <asp:Label ID="teacherTb" runat="server" CssClass="form-control"></asp:Label>
            </div>
            <div class="form-group">
                <label for="DropDownList1">Select a Course:</label>
                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="code" DataValueField="code" CssClass="form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownList1" Display="Dynamic" ErrorMessage="Select a Course" CssClass="error-message"></asp:RequiredFieldValidator>
            </div>
            <div class="button-group">
                <a href="ManageCourses.aspx">Back</a>
                <asp:Button Style=" padding: 10px 20px;
            background-color: #007bff;
            border: none;
            color: #fff;
            font-size: 16px;
            border-radius: 4px;
            text-decoration: none;
            cursor: pointer;" ID="selectCourseBt" runat="server" Text="OK" CommandName="selectCourse" OnClick="ButtonClicked" />
            </div>

            <div id="div1" class="form-group hidden">
                <p>This course has an assigned teacher: <strong><asp:Label ID="teacherTb0" runat="server" ForeColor="#007bff"></asp:Label></strong></p>
            </div>
            <div id="div2" class="form-group hidden">
                <p>Do you want to replace this teacher?</p>
                <div class="button-group">
                    <button type="button" onclick="openForm()">Yes</button>
                    <button>No</button>
                </div>
            </div>
            <div id="div3" class="form-group hidden">
                <label for="DropDownListTeachers">Select a teacher:</label>
                <asp:DropDownList ID="DropDownListTeachers" runat="server" DataSourceID="SqlDataSourceTeachers" DataTextField="DisplayName" DataValueField="id" CssClass="form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownListTeachers" Display="Dynamic" ErrorMessage="Select a Teacher" CssClass="error-message"></asp:RequiredFieldValidator>
            </div>
            <div id="div4" class="form-group hidden">
                <asp:Button ID="selectTeacherBt" runat="server" CommandName="selectTeacher" Text="OK" OnClick="ButtonClicked" />
            </div>
        </div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:UserConnectionString %>" SelectCommand="SELECT [code] FROM [Course]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceTeachers" runat="server" ConnectionString="<%$ ConnectionStrings:UserConnectionString %>" SelectCommand="SELECT id, SUBSTRING(firstName, 1, 1) + '. ' + lastName AS DisplayName FROM [User] WHERE isTeacher = 1"></asp:SqlDataSource>
    </form>
</body>
</html>
