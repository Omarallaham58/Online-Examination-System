﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterStudentsInCourse.aspx.cs" Inherits="ProjectV1.RegisterStudentsInCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assign Students to Course</title>
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f9;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 800px;
            margin: 50px auto;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }
        h1 {
            text-align: center;
            color: #333;
        }
        .form-group {
            text-align: center;
            margin-bottom: 20px;
        }
        .form-group select, .form-group button {
            padding: 10px;
            font-size: 16px;
        }
        .form-group select {
            width: 50%;
        }
        .form-group button {
            background-color: #007bff;
            color: white;
            border: none;
            cursor: pointer;
        }
        .form-group button:hover {
            background-color: #0056b3;
        }
        .gridview-container {
            margin-top: 20px;
        }
        .aspNetHidden {
            display: none;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }
        th, td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }
        th {
            background-color: #f8f9fa;
            color: #333;
        }
        tr:hover {
            background-color: #f1f1f1;
        }
        .error-message {
            color: red;
            text-align: center;
            margin-bottom: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Assign Students to Course</h1>

            <div class="form-group">
                <asp:DropDownList ID="DropDownListCourses" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCourses_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCourse" runat="server" 
                    ControlToValidate="DropDownListCourses" Display="Dynamic" 
                    ErrorMessage="Select a Course" CssClass="error-message"></asp:RequiredFieldValidator>
            </div>

            <div class="gridview-container">
                <asp:GridView ID="GridViewStudents" runat="server" AutoGenerateColumns="false" DataKeyNames="id">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Student ID" ReadOnly="true" />
                        <asp:BoundField DataField="firstName" HeaderText="First Name" />
                        <asp:BoundField DataField="lastName" HeaderText="Last Name" />
                        <asp:BoundField DataField="email" HeaderText="Email" />
                        <asp:TemplateField HeaderText="Enrolled">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBoxEnrolled" runat="server" 
                                    Checked='<%# Convert.ToBoolean(Eval("isEnrolled")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="form-group">
                <asp:Button ID="ButtonSave" runat="server" Text="Save" OnClick="ButtonSave_Click" />
                <br />
                <br />
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Green"></asp:Label>
                <br />
                <br />
                <a href="ManageCourses.aspx">Back to Management Page</a>
            </div>
        </div>
    </form>
</body>
</html>
