<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageQuestions.aspx.cs" Inherits="ProjectV1.ManageQuestions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: center;
        }
        .auto-style3 {
            text-align: center;
            width: 700px;
        }
        .auto-style4 {
            width: 49px;
        }
        .auto-style5 {
            width: 700px;
        }
        .auto-style6 {
            width: 141px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2" colspan="2">
                        <h2 class="auto-style2">Manage Questions<br />
                            Course :
                            <asp:Label ID="lblCourse" runat="server" Text="Label"></asp:Label>
                        </h2>
                        <p class="auto-style2">
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Red" Text="You have no questions to manage!" Visible="False"></asp:Label>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="id" DataSourceID="SqlDataSource1"  OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                <asp:BoundField DataField="text" HeaderText="text" SortExpression="text" />
                                <asp:BoundField DataField="grade" HeaderText="grade" SortExpression="grade" />
                                <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteRow" CommandArgument='<%# Eval("id") %>' />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="UpdateRow" CommandArgument='<%# Eval("id") %>' />
                <asp:Button ID="btnView" runat="server" Text="Options" CommandName="OptionsRow" CommandArgument='<%# Eval("id") %>' />
            </ItemTemplate>
        </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#330099" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
                            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:UserConnectionString %>" SelectCommand="SELECT id, text, grade FROM Question WHERE (course_id = @course_id)">
                            <SelectParameters>
                                <asp:SessionParameter Name="course_id" SessionField="course" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <br />
                        <button class="auto-style4"><a href="TeacherPanel.aspx">Back</a></button>&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:Panel ID="panelOptions" runat="server" Visible="False">
                            <table class="auto-style1">
                                <tr>
                                    <td class="auto-style6"><strong>Question ID:</strong></td>
                                    <td>
                                        <asp:Label ID="lblQuestion" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style6"><strong>Option 1:</strong></td>
                                    <td>
                                        <asp:Label ID="lblOp1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style6"><strong>Option 2:</strong></td>
                                    <td>
                                        <asp:Label ID="lblOp2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style6"><strong>Option 3:</strong></td>
                                    <td>
                                        <asp:Label ID="lblOp3" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style6"><strong>Option 4:</strong></td>
                                    <td>
                                        <asp:Label ID="lblOp4" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnClose" runat="server" Text="close" OnClick="btnClose_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td class="auto-style2">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
