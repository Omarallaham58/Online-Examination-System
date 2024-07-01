<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProjectV1.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            height: 100vh;
            background-color: #f5f5f5;
        }
        .header {
            width: 100%;
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 10px 50px;
            box-sizing: border-box;
            position: absolute;
            top: 0;
        }
        .header .logo {
            width: 100px;
            height: auto;
        }
        .header .words {
            text-align: left;
        }
        .header .words p {
            margin: 0;
        }
        .content {
            text-align: center;
            height:60%
        }
        .content h1 {
            margin-bottom: 20px;
        }
        .login-form {
            background-color: #fff;
            padding: 20px 30px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);

        }
        .form-group {
            margin-bottom: 15px;
            text-align: left;
        }
        .form-group label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
        }
        .form-group input {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .form-group .error-message {
            color: red;
            font-size: 0.9em;
        }
        .form-group button {
            width: 100%;
            padding: 10px;
            background-color: #007bff;
            border: none;
            color: #fff;
            font-size: 16px;
            border-radius: 4px;
            cursor: pointer;
        }
        .form-group button:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <div class="header">
        <div class="words">
            <p><strong>Lebaneese University</strong></p>
            <p><strong>Faculty Of Science</strong></p>
            <p><strong>Branch III</strong></p>
        </div>
        <img src="LuLogo.png" alt="Logo" class="logo" />
    </div>
    <h1>Online Examination System</h1><br /><br />
    <div class="content">
        
        <div class="login-form">
            <form id="form1" runat="server">
                <div class="form-group">
                    <label for="usernameTb">Username</label>
                    <asp:TextBox ID="usernameTb" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="usernameValidator" runat="server" ControlToValidate="usernameTb" Display="Dynamic" ErrorMessage="Please Enter a username" CssClass="error-message"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="passwordTb">Password</label>
                    <asp:TextBox ID="passwordTb" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="passwordValidator" runat="server" ControlToValidate="passwordTb" Display="Dynamic" ErrorMessage="Please Enter a password" CssClass="error-message"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="passwordLengthValidator" runat="server" ControlToValidate="passwordTb" Display="Dynamic" ErrorMessage="Password must have 8 characters at least" CssClass="error-message" ValidationExpression=".{8,}"></asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <asp:Button ID="submitBt" runat="server" OnClick="submitBt_Click" Text="Sign in" CssClass="btn btn-primary" />
                </div>
            </form>
        </div>
    </div>
</body>
</html>
