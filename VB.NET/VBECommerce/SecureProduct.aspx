<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SecureProduct.aspx.vb" Inherits="SecureProduct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .style3
        {
            text-align: center;
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
            font-size: large;
        }
        
        .style5
        {
            text-align: center;
        }
        .style4
        {
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server" visible="True">
    <div>
        <p style="margin-left: 480px; text-align: right;">
            <asp:Label ID="lblUsername" runat="server" Font-Bold="True" Font-Size="Small" 
                ForeColor="Blue" style="font-family: Arial, Helvetica, sans-serif" 
                Text="Hello Username |" Visible="False"></asp:Label>
            &nbsp;<asp:LinkButton ID="btnLogout" runat="server" Font-Bold="True" 
                Font-Size="Small" onclick="btnLogout_Click" Visible="False">Log out</asp:LinkButton>
        </p>
        <p style="margin-left: 480px; text-align: right;">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Home</asp:HyperLink>
        </p>
        <div class="style3">
            Products<br />
        </div>
        <div class="style5">
            <span class="style4">Product Search:</span>
            <asp:TextBox ID="txtProduct" runat="server" Width="267px"></asp:TextBox>
            &nbsp;
            <asp:Button ID="btnSearch" runat="server" Height="23px" 
                onclick="btnSearch_Click" Text="Search" Width="86px" />
        </div>
    </div>
    <asp:Label ID="lblProductResult" runat="server" ForeColor="Black" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Literal ID="literalResults" runat="server" Text="Results"></asp:Literal>
    <br />
    </form>
</body>
</html>
