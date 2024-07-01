<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTeacher.aspx.cs" Inherits="ProjectV1.AddTeacher" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 23px;
        }
        .auto-style5 {
            height: 23px;
            width: 142px;
        }
        .auto-style9 {
            width: 112px;
        }
        .auto-style10 {
            width: 55px;
        }
        .auto-style25 {
            margin-left: 0px;
        }
        .auto-style27 {
            width: 139px;
        }
        .auto-style28 {
            height: 23px;
            width: 139px;
        }
        .auto-style29 {
            height: 23px;
            width: 99px;
        }
        .auto-style30 {
            width: 99px;
        }
        .auto-style31 {
            height: 23px;
            width: 102px;
        }
        .auto-style32 {
            width: 102px;
        }
        .auto-style33 {
            width: 139px;
            height: 26px;
        }
        .auto-style34 {
            width: 99px;
            height: 26px;
        }
        .auto-style35 {
            width: 102px;
            height: 26px;
        }
        .auto-style36 {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center><h1>Add a teacher</h1>
                <p>&nbsp;</p>
                <p>&nbsp;</p></center>
        </div>
        <table class="auto-style1">
        <tr>
            <td class="auto-style28"></td>
            <td class="auto-style29">First Name: </td>
            <td class="auto-style31">
                <asp:TextBox ID="firstNameTb2" runat="server" Width="186px"></asp:TextBox>
            </td>
            <td class="auto-style2">
                <asp:RequiredFieldValidator ID="firstNameValidator2" runat="server" ControlToValidate="firstNameTb2" Display="Dynamic" ErrorMessage="Enter a username" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style27">&nbsp;</td>
            <td class="auto-style30">Last Name: </td>
            <td class="auto-style32">
                <asp:TextBox ID="lastNameTb2" runat="server" Width="186px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="lastNameValidator2" runat="server" ControlToValidate="lastNameTb2" Display="Dynamic" ErrorMessage="Enter your last name" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style33"></td>
            <td class="auto-style34">Date of Birth:&nbsp; </td>
            <td class="auto-style35">
                <asp:TextBox ID="dobTb2" runat="server" TextMode="Date" Width="186px"></asp:TextBox>
            </td>
            <td class="auto-style36">
                <asp:RequiredFieldValidator ID="dobValidator2" runat="server" ControlToValidate="dobTb2" Display="Dynamic" ErrorMessage="Enter your date of birth" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style27">&nbsp;</td>
            <td class="auto-style30">Email: </td>
            <td class="auto-style32">
                <asp:TextBox ID="emailTb2" runat="server" TextMode="Email" Width="186px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="emailValidator2" runat="server" ControlToValidate="emailTb2" Display="Dynamic" ErrorMessage="Enter your email" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style27">&nbsp;</td>
            <td class="auto-style30">Phone number: </td>
            <td class="auto-style32">
                <asp:TextBox ID="phoneNumberTb2" runat="server" Width="186px" CssClass="auto-style25"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="phoneNumberValidator3" runat="server" ControlToValidate="phoneNumberTb2" Display="Dynamic" ErrorMessage="Enter your phone number" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style28">&nbsp;</td>
            <td class="auto-style29">&nbsp;</td>
            <td class="auto-style31">
                <asp:RegularExpressionValidator ID="phoneNumberValidator4" runat="server" ControlToValidate="phoneNumberTb2" Display="Dynamic" ErrorMessage="Phone number must be like the following: +xxx-xxx-xxx-xx or 00xxxxxxxxxxx or +xxxxxxxxxxx" Font-Bold="True" Font-Italic="False" ForeColor="Red" ValidationExpression="(\+|00)\d{3}-\d{2}-\d{3}-\d{3}|\b00\d{11}\b|\+\d{11}"></asp:RegularExpressionValidator>
            </td>
            <td class="auto-style2">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style28"></td>
            <td class="auto-style29"></td>
            <td class="auto-style31"></td>
            <td class="auto-style2"></td>
        </tr>
        <tr>
            <td class="auto-style28">&nbsp;</td>
            <td class="auto-style29">&nbsp;<button class="auto-style9"><a href="ManageTeachers.aspx">Back</a></button></td>
            <td class="auto-style31">
                <asp:Button ID="addTeachertBt" runat="server" Text="ADD" Width="203px" OnClick="ButtonClicked2" CommandName="AddTeacher" />
                &nbsp;&nbsp; <button class="auto-style10" ><a href="AddTeacher.aspx">Reset</a></button>
            </td>
            <td class="auto-style5">&nbsp;
               
                
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
