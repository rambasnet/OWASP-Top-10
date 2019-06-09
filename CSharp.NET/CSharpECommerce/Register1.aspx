<%@ Page Language="C#" AutoEventWireup="false" ValidateRequest="false" %>
<%@ Import Namespace="System"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Configuration" %>


<script runat="server">

    /*
     * Author: Ram Basnet (rambasnet@gmail.com)
     * 4/28/2009
     * All Rights Reserved.
     */
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        //Check if two passowrds match
        if (this.txtPassword.Text != this.txtConfirmPassword.Text)
        {
            this.lblPasswordsDontMatch.Visible = true;
            return;
        }
        else
        {
            this.lblPasswordsDontMatch.Visible = false;
        }
        
        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        string queryString = "insert into Users (Username, Password, FirstName, LastName, Email, DOB, CreditCardNum, Address, AccessLevel) values (";
        queryString += Utilities.SingleQuote(this.txtUsername.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtPassword.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtFirstName.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtLastName.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtEmail.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtDateOfBirth.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtCreditCard.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtAddress.Text) + ",";
        queryString += Utilities.SingleQuote("User");
        queryString += ")";

 
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            command.ExecuteNonQuery();
        }
        string strMessage = "Account registered successfully!";
        Utilities.MessageBox(this, strMessage);
        Response.Redirect("Login.aspx");

    }
</script>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <form id="form1" runat="server" onsubmit="return validateForm(this);">
    <div class="style1">
    
        Client Side Validation!<br />
        <br />
    
        Enter the following information to create a new account!<br />
        <br />
        <span class="style2">* Indicates Required field</span><br />
        <br />
        <br />
    
    </div>
    <div style="height: 314px; margin-left: 40px; font-size: small;">
        <asp:Label ID="Label1" runat="server" Text="Username: *"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
            ControlToValidate="txtUsername" ErrorMessage="Required!" Font-Bold="True" 
            Font-Names="Arial" Font-Size="Small" Display="Dynamic"></asp:RequiredFieldValidator>
        <br />
        Password: *&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;         <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
            ControlToValidate="txtPassword" ErrorMessage="Required!" Font-Bold="True" 
            Font-Names="Arial" Font-Size="Small" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
            ControlToValidate="txtPassword" ErrorMessage="Weak password..." 
            Font-Bold="True" Font-Names="Arial" Font-Size="Small" 
            ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$"></asp:RegularExpressionValidator>
        <br />
        Confirm Password: *&nbsp;&nbsp;
        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" 
            ControlToValidate="txtConfirmPassword" ErrorMessage="Required!" 
            Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
        <br />
        First Name:&nbsp;&nbsp;* &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" 
            ControlToValidate="txtFirstName" ErrorMessage="Required!" Font-Bold="True" 
            Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
        <br />
        Last Name: *&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
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
        Credit Card #:&nbsp; *&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:TextBox ID="txtCreditCard" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvCreditCard" runat="server" 
            ControlToValidate="txtCreditCard" ErrorMessage="Required!" Font-Bold="True" 
            Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
            ControlToValidate="txtCreditCard" ErrorMessage="Enter 16 digits..." 
            Font-Bold="True" Font-Names="Arial" Font-Size="Small" 
            ValidationExpression="(^\d{16})$"></asp:RegularExpressionValidator>
        <br />
        Address:&nbsp; *&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
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
    
    <script type="text/javascript">
        //<![CDATA[

        function validateForm(thisForm) 
        {
            with (thisForm) 
            {
                if (validateRequired(txtUsername, 'Username is Required!') == false) {
                    txtUsername.focus(); 
                    return false;
                }
                if (validateRequired(txtPassword, 'Password is Required!') == false) {
                    txtPassword.focus();
                    return false;
                }
                if (validateRequired(txtFirstName, 'First name is Required!') == false) {
                    txtFirstName.focus();
                    return false;
                }
                if (validateRequired(txtLastName, 'Last name is Required!') == false) {
                    txtLastName.focus();
                    return false;
                }
                if (validateRequired(txtEmail, 'Email is Required!') == false) {
                    txtEmail.focus();
                    return false;
                }
                if (validateEmail(txtEmail) == false) {
                    return false;
                }
            }
        }
        function validateRequired(field, message)
        {
            with (field)
            {
                if (value == null || value == '')
                {
                    alert(message);
                    return false;
                }
                else
                {
                    return true;
                }
                
            }
        }

        function validateEmail(field) {
            with(field)
            {
                atpos = value.indexOf("@");
                dotpos = value.lastIndexOf(".");
                if (atpos < 1 || dotpos - atpos < 2) {
                    alert('Invalid Email format');
                    return false; 
                }
                else {
                    return true; 
                 }
            }
        }
        //]]>
    </script>

</body>
</html>
