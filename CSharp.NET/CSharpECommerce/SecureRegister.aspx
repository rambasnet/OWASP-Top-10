<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecureRegister.aspx.cs" Inherits="SecureRegister" ValidateRequest="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
            font-size: medium;
        }
        .style2
        {
            font-size: small;
            color: #FF0000;
        }
    </style>
</head>
<body style="font-family: Arial, Helvetica, sans-serif">
    <form id="form1" runat="server">
    <div class="style1">
    
        Enter the following information to create a new account!&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Home</asp:HyperLink>
        <br />
        <span class="style2">* Indicates Required field</span><br />
        <br />
    
    </div>
    <div style="height: 314px; margin-left: 40px">
        <asp:Label ID="Label1" runat="server" Text="Username: *"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
            ControlToValidate="txtUsername" ErrorMessage="Required!" Font-Bold="True" 
            Font-Names="Arial" Font-Size="Small" Display="Dynamic"></asp:RequiredFieldValidator>
        <br />
        Password: *&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
            ControlToValidate="txtPassword" ErrorMessage="Required!" Font-Bold="True" 
            Font-Names="Arial" Font-Size="Small" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
            ControlToValidate="txtPassword" ErrorMessage="Weak password..." 
            Font-Bold="True" Font-Names="Arial" Font-Size="Small" 
            ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9].{8,})$"></asp:RegularExpressionValidator>
        <br />
        Confirm Password: *&nbsp;&nbsp;
        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" 
            ControlToValidate="txtConfirmPassword" ErrorMessage="Required!" 
            Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
        <br />
        First Name:&nbsp;&nbsp;* &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; 
        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" 
            ControlToValidate="txtFirstName" ErrorMessage="Required!" Font-Bold="True" 
            Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
        <br />
        Last Name: *&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;         <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvLastname" runat="server" 
            ControlToValidate="txtLastName" ErrorMessage="Required!" Font-Bold="True" 
            Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
        <br />
        Email:&nbsp;&nbsp;* &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
            ControlToValidate="txtEmail" ErrorMessage="Required!" Font-Bold="True" 
            Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
        <br />
        Date of Birth: *&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtDateOfBirth" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvDOB" runat="server" 
            ControlToValidate="txtDateOfBirth" ErrorMessage="Required!" Font-Bold="True" 
            Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="txtDateOfBirth" ErrorMessage="Date format: mm/dd/yyyy" 
            Font-Bold="True" Font-Names="Arial" Font-Size="Small" 
            ValidationExpression="^\d{1,2}/\d{1,2}/\d{2,4}$"></asp:RegularExpressionValidator>
        <br />
        Credit Card #:&nbsp; *&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtCreditCard" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvCreditCard" runat="server" 
            ControlToValidate="txtCreditCard" ErrorMessage="Required!" Font-Bold="True" 
            Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
            ControlToValidate="txtCreditCard" ErrorMessage="Enter 16 digits..." 
            Font-Bold="True" Font-Names="Arial" Font-Size="Small" 
            ValidationExpression="(^\d{16})$"></asp:RegularExpressionValidator>
        <br />
        Address:&nbsp; *&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;         <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" 
            ControlToValidate="txtAddress" ErrorMessage="Required!" Font-Bold="True" 
            Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblPasswordsDontMatch" runat="server" ForeColor="Red" 
            Text="Passwords do not match!" Visible="False"></asp:Label>
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnRegister" runat="server" onclick="btnRegister_Click" 
            Text="Register" Width="70px" />
    </div>
    </form>
</body>
</html>
