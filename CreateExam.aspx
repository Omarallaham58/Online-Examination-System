<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateExam.aspx.cs" Inherits="ProjectV1.CreateExam" %>

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
            width: 292px;
        }
        .auto-style4 {
            height: 514px;
            text-align: center;
        }
        .auto-style5 {
            width: 439px;
        }
        .auto-style6 {
            width: 439px;
            text-align: left;
        }
        .auto-style7{
            display : flex;
            justify-content :center;
            text-align : center;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style4">
            <div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style2" colspan="3">
                    <h2>Create an Exam to
                        <asp:Label ID="lblCourse" runat="server" Text="Label"></asp:Label>
&nbsp;Course</h2>
                </td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="3">
                    <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Italic="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="3">
                    <asp:Label ID="lblDate" runat="server" ForeColor="Blue" Text="pick an exam date (must be after today's date)" Font-Bold="True" Font-Italic="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Exam Date:</td>
                <td class="auto-style6">
                    <asp:TextBox ID="tbDate" runat="server" TextMode="Date" Width="186px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbdate" Display="Dynamic" ErrorMessage="exam's date cant be empty!" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">Exam Duration (in minutes):</td>
                <td class="auto-style6">
                    <asp:TextBox ID="tbDuration" runat="server" Width="184px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbDuration" Display="Dynamic" ErrorMessage="duration cannot be empty!" Font-Bold="True" ForeColor="Red" Font-Italic="True"></asp:RequiredFieldValidator>
                    
                </td>
                <td><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbDuration" Display="Dynamic" ErrorMessage="duration cannot be negative!" Font-Bold="True" ForeColor="Red" Operator="GreaterThan" ValueToCompare="0" Font-Italic="True"></asp:CompareValidator>&nbsp;</td>
            </tr>
            
        </table>
                </div>
            <div class="auto-style2">
            <br />
            </div>
  <asp:Panel ID="qPanel" runat="server">
      <div class="auto-style7">
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:Table ID="qTable" runat="server">
          </asp:Table>
          <br />
      </div>
            </asp:Panel>
            <div class="auto-style2">
                <button><a href="TeacherPanel.aspx">Back</a></button>
                &nbsp;&nbsp;
            <asp:Button ID="submit" runat="server" Text="Create Exam" OnClick="submit_Click" />
                &nbsp;&nbsp; <button id="btnReset"><a href="CreateExam.aspx">Clear</a></button>
            </div>
            </div>
    </form>
</body>
</html>
