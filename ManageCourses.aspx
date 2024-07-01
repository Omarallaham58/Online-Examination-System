<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageCourses.aspx.cs" Inherits="ProjectV1.ManageCourses" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 227px;
        }
        .auto-style3 {
            height: 227px;
            width: 358px;
        }
        .auto-style4 {
            width: 358px;
        }
        .auto-style5 {
            height: 227px;
            width: 487px;
        }
        .auto-style6 {
            width: 487px;
        }
        .auto-style7 {
            width: 358px;
            height: 23px;
        }
        .auto-style8 {
            width: 487px;
            height: 23px;
        }
        .auto-style9 {
            height: 23px;
        }
        .auto-style10 {
            margin-right: 21px;
        }
    </style>
</head>
<body>
 
    <form id="form1" runat="server">
        <center>
            
        <div>
            <table class="auto-style1">
                <tr><td colspan="3"><center><h1>Manage Courses</h1></center></td></tr>
                <tr>
                    <td class="auto-style3">
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:UserConnectionString %>" DeleteCommand="DELETE FROM [Course] WHERE [id] = @id" InsertCommand="INSERT INTO [Course] ([teacher_id], [code], [name], [credits], [nbStudents]) VALUES (@teacher_id, @code, @name, @credits, @nbStudents)" 
                            SelectCommand="SELECT c.*, CASE WHEN c.teacher_id = -1 THEN 'N/A' ELSE SUBSTRING(t.firstName, 1, 1) + '.' + t.lastName END AS teacher_name FROM [Course] c LEFT JOIN [User] t ON c.teacher_id=t.id"
                            UpdateCommand="UPDATE [Course] SET [name] = @name, [credits] = @credits WHERE [id] = @id">
                            <DeleteParameters>
                                <asp:Parameter Name="id" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="teacher_id" Type="Int32" />
                                <asp:Parameter Name="code" Type="String" />
                                <asp:Parameter Name="name" Type="String" />
                                <asp:Parameter Name="credits" Type="Int32" />
                                <asp:Parameter Name="nbStudents" Type="Int32" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="name" Type="String" />
                                <asp:Parameter Name="credits" Type="Int32" />
                                <asp:Parameter Name="id" Type="Int32" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td class="auto-style5">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1" Width="623px" CssClass="auto-style10">
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                <asp:BoundField DataField="teacher_id" HeaderText="teacher_id" SortExpression="teacher_id" ReadOnly="True" Visible="False" />
                                <asp:BoundField DataField="code" HeaderText="code" SortExpression="code" ReadOnly="True" />
                                <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                                <asp:BoundField DataField="credits" HeaderText="credits" SortExpression="credits" />
                                <asp:BoundField DataField="nbStudents" HeaderText="nbStudents" SortExpression="nbStudents" ReadOnly="True" />
                                 <asp:BoundField DataField="teacher_name" HeaderText="Teacher Name" SortExpression="teacher_name" ReadOnly="True" />
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td class="auto-style2"></td>
                </tr>
                <tr>
                    <td class="auto-style7"></td>
                    <td class="auto-style8"><center>
                <a href="StudentsByCourse.aspx">View Students By Course</a></center></td>
                    <td class="auto-style9"></td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style6"><center>
                <a href="AddCourse.aspx">Add a New Course</a></center></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style6"><center>
                <a href="AssignCourseToTeacher.aspx">Assign Courses To Teachers</a></center></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style6"><center>
                <a href="RegisterStudentsInCourse.aspx">Register Students into Courses</a></center></td>
                    <td>&nbsp;</td>
                </tr>
                  <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style6"><center>
                <button><a href="Admin.aspx">Back</a></button></center></td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div></center><br /><br />
      
    </form>
</body>
</html>
