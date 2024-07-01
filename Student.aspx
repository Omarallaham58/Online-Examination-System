<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="ProjectV1.Student" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style4 {
            height: 23px;
        }
        .auto-style5 {
            width: 390px;
        }
        .auto-style6 {
            height: 23px;
            width: 390px;
        }
        .auto-style7 {
            width: 473px;
        }
        .auto-style8 {
            height: 23px;
            width: 473px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin:10px;">
            <center>
                <br />
                <br />
                <br />
                <br />
                <br />
            </center>
        </div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style5"><center>
                <asp:Label ID="lb_welcome" runat="server" Text="Label" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                </center></td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style5">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style5">
                    <center><asp:Button ID="Button1" runat="server" Text="Exams" Font-Size="Large" Height="40px" PostBackUrl="~/Exams.aspx" Width="124px" /></center>
                </td>
                <td>
                    <center></center>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">
                </td>
                <td class="auto-style6">
                    <br />
                </td>
                <td class="auto-style4">
                </td>
            </tr>
            <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style5">
                    <center>
                    <asp:Button ID="Button2" runat="server" Text="Grades History" Font-Size="Large" Height="40px" PostBackUrl="~/Grades.aspx" Width="138px" />
                </center>
                        </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style5">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style5">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style5"><center>
    <button height="40px" width="100px"><a href="LogOut.aspx">Log Out</a></button>
                    </center>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </form>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    </body>
</html>
