<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="displayGrade.aspx.cs" Inherits="ProjectV1.displayGrade" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center; padding: 20px">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text="Your Final Grade is:" ForeColor="Green"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lbl_grade" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="Green"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" PostBackUrl="~/Student.aspx" Text="OK" Width="165px" />
        </div>
    </form>
</body>
</html>
