<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuestions.aspx.cs" Inherits="ProjectV1.AddQuestions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">
        
        .validatorClass {
           color: red;
          font-weight: bold;
        }
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: center;
        }
        .auto-style3 {
            width: 87px;
        }
        .auto-style4 {
            width: 324px;
        }
        .auto-style5 {
            width: 388px;
        }
        .auto-style6 {
            width: 53px;
        }
    </style>
</head>
<body style="height: 660px">
    <form id="form1" runat="server">
        <div>
            <br/>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2" colspan="4">
                        <h3>Add a Question for
                            <asp:Label ID="lblCourse" runat="server" Text="course"></asp:Label>
&nbsp;Course</h3>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="4">
                        <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Italic="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">All fields are required. Note that only one option can be selected as a correct answer.</td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">question :</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="tbQuestion" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbQuestion" Display="Dynamic" ErrorMessage="Question field is required!" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">grade :</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="tbGrade" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style5">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbGrade" Display="Dynamic" ErrorMessage="Grade field is required!" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbGrade" Display="Dynamic" ErrorMessage="Grade must be a double!" Font-Bold="True" Font-Italic="True" ForeColor="Red" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">option 1 :</td>
                    <td class="auto-style4">
                        <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" GroupName="rb1" />
                        <asp:TextBox ID="tbOp1" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbOp1" Display="Dynamic" ErrorMessage="Option1 field is required!" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">option 2 : </td>
                    <td class="auto-style4">
                        <asp:RadioButton ID="RadioButton2" runat="server" GroupName="rb1" />
                        <asp:TextBox ID="tbOp2" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbOp2" Display="Dynamic" ErrorMessage="Option 2 is required!" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">option 3 :</td>
                    <td class="auto-style4">
                        <asp:RadioButton ID="RadioButton3" runat="server" GroupName="rb1" />
                        <asp:TextBox ID="tbOp3" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbOp3" Display="Dynamic" ErrorMessage="Option 3 is required!" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">option 4 :</td>
                    <td class="auto-style4">
                        <asp:RadioButton ID="RadioButton4" runat="server" GroupName="rb1" />
                        <asp:TextBox ID="tbOp4" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbOp4" Display="Dynamic" ErrorMessage="Option 4 is required!" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <button><a href="TeacherPanel.aspx">Back</a></button>&nbsp;&nbsp;
                        </td>
                    <td class="auto-style4">
                        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add question" />
                    </td>
                    <td colspan="2">
                         <button id="btnReset" class="auto-style6"><a href="AddQuestions.aspx">Clear</a></button>
                    </td>
                </tr>
            </table>
        </div>
         
    </form>
</body>
</html>
