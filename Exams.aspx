<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Exams.aspx.cs" Inherits="ProjectV1.Exams" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 550px;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
    
    <center>
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Choose an Exam to take" Font-Bold="True" Font-Size="X-Large"></asp:Label>
        <br />
        <br />
        <br />
    </center>
    
    <asp:RadioButtonList ID="rdbtn_list_exams" runat="server">
    </asp:RadioButtonList>
    
    <br />
    <table class="auto-style1">
        <tr>
            <td class="auto-style2">
                <asp:Button ID="Button1" runat="server" PostBackUrl="~/Student.aspx" Text="Back" Font-Size="Medium" />
            </td>
            <td>
                <asp:Button ID="btn_take" runat="server" OnClick="btn_take_Click" Text="Take the Exam" Font-Size="Medium" Height="30px" Width="133px" />
            </td>
        </tr>
    </table>
    
</form>
</body>
</html>
