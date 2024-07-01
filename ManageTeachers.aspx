<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageTeachers.aspx.cs" Inherits="ProjectV1.ManageTeachers" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Teachers</title>
    <style>
        body {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            height: 100vh;
            margin: 0;
            font-family: Arial, sans-serif;
            box-sizing: border-box;
        }

        h1 {
            margin-bottom: 20px;
        }

        form {
            display: flex;
            flex-direction: column;
            align-items: center;
            width: 100%;
            box-sizing: border-box;
        }

        .button-container {
            display: flex;
            justify-content: space-between;
            width: 200px;
            margin-top: 20px;
        }

        .button-container button {
            padding: 10px 20px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            text-decoration: none;
        }

        .button-container button:hover {
            background-color: #0056b3;
        }

        .button-container button a {
            color: white;
            text-decoration: none;
        }

        .gridview-container {
            display: flex;
            justify-content: center;
            width: 100%;
            overflow-x: auto;
            box-sizing: border-box;
        }

        table {
            width: 100%;
            max-width: 100%;
            box-sizing: border-box;
        }

        th, td {
            word-wrap: break-word;
        }
    </style>
</head>
<body>
    <h1>Manage Teachers</h1>
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" 
            ConnectionString="<%$ ConnectionStrings:UserConnectionString %>" 
            DeleteCommand="DELETE FROM [User] WHERE [id] = @original_id AND [account_id] = @original_account_id AND [firstName] = @original_firstName AND [lastName] = @original_lastName AND [dob] = @original_dob AND (([phoneNumber] = @original_phoneNumber) OR ([phoneNumber] IS NULL AND @original_phoneNumber IS NULL)) AND [email] = @original_email" 
            InsertCommand="INSERT INTO [User] ([account_id], [firstName], [lastName], [dob], [phoneNumber], [email]) VALUES (@account_id, @firstName, @lastName, @dob, @phoneNumber, @email)" 
            OldValuesParameterFormatString="original_{0}" 
            SelectCommand="SELECT u.[id], u.[account_id], u.[firstName], u.[lastName], u.[dob], u.[email], u.[phoneNumber], a.[username], a.[password] FROM [User] u, [Account] a WHERE ([isTeacher] = @isTeacher) AND (u.[account_id]=a.[id])" 
            UpdateCommand="UPDATE [User] SET  [firstName] = @firstName, [lastName] = @lastName,  [email] = @email, [phoneNumber] = @phoneNumber WHERE [id] = @original_id AND [account_id] = @original_account_id AND [firstName] = @original_firstName AND [lastName] = @original_lastName AND [email] = @original_email AND (([phoneNumber] = @original_phoneNumber) OR ([phoneNumber] IS NULL AND @original_phoneNumber IS NULL));
                            UPDATE [Account] SET [username]=@username, [password]=@password WHERE [id]=@original_account_id">
            <DeleteParameters>
                <asp:Parameter Name="original_id" Type="Int32" />
                <asp:Parameter Name="original_account_id" Type="Int32" />
                <asp:Parameter Name="original_firstName" Type="String" />
                <asp:Parameter Name="original_lastName" Type="String" />
                <asp:Parameter DbType="Date" Name="original_dob" />
                <asp:Parameter Name="original_phoneNumber" Type="String" />
                <asp:Parameter Name="original_email" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="account_id" Type="Int32" />
                <asp:Parameter Name="firstName" Type="String" />
                <asp:Parameter Name="lastName" Type="String" />
                <asp:Parameter DbType="Date" Name="dob" />
                <asp:Parameter Name="phoneNumber" Type="String" />
                <asp:Parameter Name="email" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="1" Name="isTeacher" Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="firstName" Type="String" />
                <asp:Parameter Name="lastName" Type="String" />
                <asp:Parameter Name="email" Type="String" />
                <asp:Parameter Name="phoneNumber" Type="String" />
                <asp:Parameter Name="original_id" Type="Int32" />
                <asp:Parameter Name="original_account_id" Type="Int32" />
                <asp:Parameter Name="original_firstName" Type="String" />
                <asp:Parameter Name="original_lastName" Type="String" />
                <asp:Parameter Name="original_email" Type="String" />
                <asp:Parameter Name="original_phoneNumber" Type="String" />
                <asp:Parameter Name="username" />
                <asp:Parameter Name="password" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <div class="gridview-container">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                    <asp:BoundField DataField="account_id" HeaderText="account_id" ReadOnly="True" SortExpression="account_id" />
                    <asp:BoundField DataField="firstName" HeaderText="firstName" SortExpression="firstName" />
                    <asp:BoundField DataField="lastName" HeaderText="lastName" SortExpression="lastName" />
                    <asp:BoundField DataField="dob" DataFormatString="{0:yyyy-MM-dd}" HeaderText="dob" ReadOnly="True" SortExpression="dob" />
                    <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                    <asp:BoundField DataField="phoneNumber" HeaderText="phoneNumber" SortExpression="phoneNumber" />
                    <asp:BoundField DataField="username" HeaderText="username" SortExpression="username" />
                    <asp:BoundField DataField="password" HeaderText="password" SortExpression="password" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="button-container">
            <button><a href="AddTeacher.aspx">Add a teacher</a></button>
            <button><a href="Admin.aspx">Back</a></button>
        </div>
    </form>
</body>
</html>
