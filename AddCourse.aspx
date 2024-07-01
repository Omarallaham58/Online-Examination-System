<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="ProjectV1.AddCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 111px;
        }
        .auto-style3 {
            width: 408px;
        }
        .auto-style4 {
            width: 408px;
            height: 23px;
        }
        .auto-style5 {
            width: 111px;
            height: 23px;
        }
        .auto-style6 {
            height: 23px;
        }
        .auto-style7 {
            width: 188px;
        }
        .auto-style8 {
            height: 23px;
            width: 188px;
        }
        .auto-style9 {
            width: 408px;
            height: 24px;
        }
        .auto-style10 {
            width: 111px;
            height: 24px;
        }
        .auto-style11 {
            width: 188px;
            height: 24px;
        }
        .auto-style12 {
            height: 24px;
        }
        .auto-style13 {
            width: 93px;
        }
        .auto-style14 {
            width: 93px;
            height: 23px;
        }
        .auto-style15 {
            width: 93px;
            height: 24px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <table class="auto-style1">
         <tr><td colspan="5"><center><h1>Add Course</h1></center></td></tr>
        <tr>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style7">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td class="auto-style14"></td>
            <td class="auto-style5">Code: </td>
            <td class="auto-style8">
                <asp:TextBox ID="codeTb" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td class="auto-style6">
                <asp:RequiredFieldValidator ID="codeValidator1" runat="server" ControlToValidate="codeTb" Display="Dynamic" ErrorMessage="Enter Course Code" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style2">Name: </td>
            <td class="auto-style7">
                <asp:TextBox ID="nameTb" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="nameValidator1" runat="server" ControlToValidate="nameTb" Display="Dynamic" ErrorMessage="Enter Course Name" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style2">Credits: </td>
            <td class="auto-style7">
                <asp:TextBox ID="creditsTb" runat="server" TextMode="Number" Width="150px"></asp:TextBox>
            </td>
            <td><strong>
                <asp:RequiredFieldValidator ID="creditsValidator1" runat="server" ControlToValidate="creditsTb" Display="Dynamic" ErrorMessage="Enter number of credits" ForeColor="Red"></asp:RequiredFieldValidator>
                </strong></td>
        </tr>
        <tr>
            <td class="auto-style9">&nbsp;</td>
            <td class="auto-style15"></td>
            <td class="auto-style10"></td>
            <td class="auto-style11"><strong>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="creditsTb" Display="Dynamic" ErrorMessage="Number of credits must be between 1 and 6" ForeColor="Red" MaximumValue="6" MinimumValue="1"></asp:RangeValidator>
                </strong></td>
            <td class="auto-style12"></td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td class="auto-style14"></td>
            <td class="auto-style5"><button><a href="ManageCourses.aspx">Back</a></button></td>
            <td class="auto-style8">
                <asp:Button ID="addCourseBt" runat="server" OnClick="addCourseBt_Click" Text="Add Course" Width="150px" />
            </td>
            <td class="auto-style6"><button><a href="AddCourse.aspx">Reset</a></button></td>
        </tr>
    </table>
    </form>
    </body>
</html>
