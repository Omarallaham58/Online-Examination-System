<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherPanel.aspx.cs" Inherits="ProjectV1.TeacherPanel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 324px;
        }
        .auto-style3 {
            width: 891px;
        }
        .auto-style5 {
            width: 324px;
            height: 38px;
        }
        .auto-style6 {
            width: 891px;
            height: 38px;
        }
        .auto-style7 {
            height: 38px;
        }
        .auto-style8 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <table class="auto-style1">
            <tr>
                <td class="auto-style8" colspan="3">
                    <h2><strong>Teacher Panel</strong></h2>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Teacher :
                    <asp:Label ID="lblTeacherName" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style3">&nbsp;</td>
                <td>Date :
                    <asp:Label ID="lblDate" runat="server" Text="lblDate"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2"><button><a href="LogOut.aspx">Logout</a></button></td>
                <td class="auto-style3">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">Select a Course to manage:</td>
                <td class="auto-style6">
                    <asp:DropDownList ID="listCourses" runat="server" DataSourceID="SqlDataSource1" DataTextField="code" DataValueField="id">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:UserConnectionString %>" SelectCommand="SELECT [id], [code] FROM [Course] WHERE ([teacher_id] = @teacher_id)">
                        <SelectParameters>
                            <asp:SessionParameter Name="teacher_id" SessionField="user" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:Button ID="btnSelect" runat="server" OnClick="btnSelect_Click" Text="select" />
                </td>
                <td class="auto-style7"></td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    <asp:HyperLink ID="linkAddQuestion" runat="server" NavigateUrl="~/AddQuestions.aspx" Visible="False">Add Questions</asp:HyperLink>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    <asp:HyperLink ID="linkManageQuestion" runat="server" Visible="False" NavigateUrl="~/ManageQuestions.aspx">Manage Questions</asp:HyperLink>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    <asp:HyperLink ID="linkCreateExam" runat="server" Visible="False" NavigateUrl="~/CreateExam.aspx">Create an Exam</asp:HyperLink>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    <asp:HyperLink ID="linkViewStudents" runat="server" Visible="False" NavigateUrl="~/ViewAssignedStudents.aspx">View Assigned Students</asp:HyperLink>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <div>
        </div>
    </form>
</body>
</html>
