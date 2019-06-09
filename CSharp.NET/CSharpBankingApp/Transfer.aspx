<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Transfer.aspx.cs" Inherits="Admin_DeleteUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
            font-size: large;
            color: #0066CC;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="style1">
    
        Transfer Funds between accounts!<br />
        <br />
    </div>
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" 
        Font-Size="Small" Text="From Account:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="ddlFromAccount" runat="server">
    </asp:DropDownList>
    <br />
    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" 
        Font-Size="Small" Text="Amount:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtAmount" runat="server" Width="128px"></asp:TextBox>
    <p style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-weight: 700">
        To Account:</p>
    <p style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-weight: 700">
        Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;         <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <br />
        Number:&nbsp;&nbsp;&nbsp;&nbsp;         <asp:TextBox ID="txtToAccount" runat="server"></asp:TextBox>
    </p>
    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnTransfer" runat="server" onclick="btnTransfer_Click" 
            Text="Transfer" />
    </p>
    </form>
</body>
</html>
