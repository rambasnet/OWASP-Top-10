<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecureAdmin.aspx.cs" Inherits="Admin_SecureAdmin" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-size: large;
            font-weight: bold;
            color: #0033CC;
        }
    </style>
</head>
<body style="font-family: Arial, Helvetica, sans-serif; font-size: small">
    <form id="form1" runat="server">
    <div>
    
        <span class="style1">Admin Control Panel</span><br />
        <br />
        Select User:
        <asp:DropDownList ID="ddlUsers" runat="server" AutoPostBack="True" DataTextField="Username" 
            DataValueField="Username" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="btnEdit" runat="server" Height="25px" onclick="btnEdit_Click" 
            Text="Edit" Width="68px" style="margin-right: 3px" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDelete" runat="server" Height="25px" 
            onclick="btnDelete_Click" Text="Delete" Width="67px" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSecureDelete" runat="server" onclick="btnSecureDelete_Click" 
            style="margin-bottom: 0px" Text="Secure Delete" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnNew" runat="server" Height="26px" Text="Add New" 
            Width="68px" onclick="btnNew_Click1" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnLogout" runat="server" onclick="btnLogout_Click" 
            Text="Logout" Width="67px" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Username:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Literal ID="literalUsername" runat="server"></asp:Literal>
        <input id="Token" type="hidden" name="Token" value="<%Response.Write(Server.HtmlEncode(Session["Token"].ToString()));%>"/>
        <br />
        Password:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
        <br />
        Confirm Password:&nbsp;&nbsp;
        <asp:TextBox ID="txtConfirmPassword" runat="server"></asp:TextBox>
        <br />
        First Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;         <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
        <br />
        Last Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
        <br />
        Email:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <br />
        Date of Birth:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtDateOfBirth" runat="server"></asp:TextBox>
        <br />
        Credit Card #:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtCreditCard" runat="server"></asp:TextBox>
        <br />
        Address:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
        <br />
        <br />
        AccessLevel:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddlAccessLevel" runat="server">
            <asp:ListItem>User</asp:ListItem>
            <asp:ListItem>Admin</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Label ID="lblToken" runat="server" Text="Token" Visible="False"></asp:Label>
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblPasswordsDontMatch" runat="server" ForeColor="Red" 
            Text="Passwords do not match!" Visible="False"></asp:Label>
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSave" runat="server" Height="24px" onclick="btnSave_Click" 
            Text="Update" Width="71px" />
    
    </div>
    </form>
</body>
</html>
