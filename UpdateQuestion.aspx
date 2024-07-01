<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateQuestion.aspx.cs" Inherits="ProjectV1.UpdateQuestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title></title>
    <style type="text/css">
        .auto-style3 {
            width: 100%;
        }
        .auto-style4 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style3">
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style4" colspan="4">Update a Question</td>
                </tr>
                <tr>
                    <td class="auto-style4" colspan="4">
                        <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Italic="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4" colspan="4">All fields are required. Note that only one option can be selected as a correct answer.</td>
                </tr>
                <tr>
                    <td class="auto-style4" colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td>question :</td>
                    <td>
                        <asp:TextBox ID="tbQuestion" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbQuestion" Display="Dynamic" ErrorMessage="Question field is required!" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>grade :</td>
                    <td>
                        <asp:TextBox ID="tbGrade" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbGrade" Display="Dynamic" ErrorMessage="Grade field is required!" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbGrade" Display="Dynamic" ErrorMessage="Grade must be a double!" Font-Bold="True" Font-Italic="True" ForeColor="Red" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>option 1 :</td>
                    <td>
                        <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" GroupName="rb1" />
                        <asp:TextBox ID="tbOp1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbOp1" Display="Dynamic" ErrorMessage="Option1 field is required!" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>option 2 :</td>
                    <td>
                        <asp:RadioButton ID="RadioButton2" runat="server" GroupName="rb1" />
                        <asp:TextBox ID="tbOp2" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbOp2" Display="Dynamic" ErrorMessage="Option 2 is required!" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>option 3 :</td>
                    <td>
                        <asp:RadioButton ID="RadioButton3" runat="server" GroupName="rb1" />
                        <asp:TextBox ID="tbOp3" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbOp3" Display="Dynamic" ErrorMessage="Option 3 is required!" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>option 4 :</td>
                    <td>
                        <asp:RadioButton ID="RadioButton4" runat="server" GroupName="rb1" />
                        <asp:TextBox ID="tbOp4" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbOp4" Display="Dynamic" ErrorMessage="Option 4 is required!" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <button><a href="ManageQuestions.aspx">Back</a></button></td>
                    <td>
                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click1" Text="update" />
                        <asp:HiddenField ID="hidden_id" runat="server" />
                    </td>
                    <td>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
