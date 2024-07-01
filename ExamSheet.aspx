<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamSheet.aspx.cs" Inherits="ProjectV1.ExamSheet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            width: 1115px;
        }
        .auto-style4 {
            height: 23px;
            width: 1115px;
        }
    </style>
   
</head>
<body>
    <% DateTime timeOut = (DateTime)Session["timeOut"]; %>
    <script>
        //get timeOut year, mon, day, hour, min, sec
        var year = <%=timeOut.Year %>;
        var month = <%=timeOut.Month %>;
        var day = <%=timeOut.Day %>;
        var hour = <%=timeOut.Hour %>;
        var min = <%=timeOut.Minute %>;
        var sec = <%=timeOut.Second %>;

        //set the timeOut date
        var timeOut = new Date(year, month - 1, day, hour, min, sec, 0).getTime();
        
        //update the count down every 1 second
        var x = setInterval(function () {
            var now = new Date().getTime();

            //calculate the remaining time
            var remainder = timeOut - now;

            //time calculation for hours, minutes and seconds
            var hours = Math.floor((remainder % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minutes = Math.floor((remainder % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((remainder % (1000 * 60)) / 1000);

            document.getElementById("hours").innerHTML = hours.toString();
            document.getElementById("minutes").innerHTML = minutes.toString();
            document.getElementById("seconds").innerHTML = seconds.toString();

            //if time is up
            if (remainder < 0) {
                clearInterval(x);
                //click the finish button when time is up
                document.getElementById('<%="finishBtn" %>').click();
            }
        }, 1000); //1000ms -> 1s
    </script>
    <form id="form1" runat="server">

        <center><asp:Label ID="Label4" runat="server" Font-Size="XX-Large" Text="Exam Sheet" ForeColor="#0000CC"></asp:Label>
        <br /></center>

    <table class="auto-style1">
        <tr>
            <td class="auto-style3">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Underline="True" Text="Course:" Font-Size="Large" ForeColor="#0000CC"></asp:Label>
&nbsp;<asp:Label ID="lbl_name" runat="server" Font-Size="Large"></asp:Label>
            &nbsp;(<asp:Label ID="lbl_code" runat="server" Font-Size="Large"></asp:Label>
                )</td>
            <td rowspan="3">
               <div style="padding: 5px; font-size: x-large; position: fixed; background-color: #C0C0C0;"><span id="hours"></span> : <span id="minutes"></span> : <span id="seconds"></span></div> 
            &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Underline="True" Text="Date:" Font-Size="Large" ForeColor="#0000CC"></asp:Label>
&nbsp;<asp:Label ID="lbl_date" runat="server" Font-Size="Large"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Underline="True" Text="Teacher:" Font-Size="Large" ForeColor="#0000CC"></asp:Label>
&nbsp;<asp:Label ID="lbl_teacher" runat="server" Font-Size="Large"></asp:Label>
            </td>
        </tr>
    </table>
        <br />
        <hr />
    </form>
</body>
</html>
