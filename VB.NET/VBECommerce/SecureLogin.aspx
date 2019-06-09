<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SecureLogin.aspx.vb" Inherits="SecureLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .style1
        {
            font-family: Arial, Helvetica, sans-serif;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="style1">
        Enter Username and Password to Log In:<br />
        <br />
        &nbsp;<asp:Label ID="Label1" runat="server" style="font-size: small" 
            Text="Username:"></asp:Label>
        &nbsp;<asp:TextBox ID="txtUsername" runat="server" MaxLength="25"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
            ControlToValidate="txtUsername" ErrorMessage="Required!" Font-Size="Small"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label2" runat="server" style="font-size: small" Text="Password:"></asp:Label>
        &nbsp;
        <asp:TextBox ID="txtPassword" runat="server" Height="22px" MaxLength="25" 
            TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
            ControlToValidate="txtPassword" ErrorMessage="Required!" Font-Size="Small"></asp:RequiredFieldValidator>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblErrorMessage" runat="server" 
            Font-Bold="True" Font-Size="Small" ForeColor="Red" Visible="False"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnLogin" runat="server" onclick="btnLogin_Click" Text="Login" 
            Width="47px" />
        &nbsp;<br />
        &nbsp;
        <br />
    </div>
    </form>
</body>
</html>
