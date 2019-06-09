<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Product.aspx.vb" Inherits="Product" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .style1
        {
            text-align: right;
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
            font-size: large;
        }
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
    <form id="form1" runat="server">
    <div class="style1">
        <p style="margin-left: 480px; text-align: right; width: 167px;">
            <asp:Label ID="lblUsername" runat="server" Font-Bold="True" Font-Size="Small" 
                ForeColor="Blue" style="font-family: Arial, Helvetica, sans-serif" 
                Text="Hello Username |" Visible="False"></asp:Label>
            &nbsp;<asp:LinkButton ID="btnLogout" runat="server" Font-Bold="True" 
                Font-Size="Small" onclick="btnLogout_Click" Visible="False">Log out</asp:LinkButton>
        </p>
        <p style="margin-left: 480px; text-align: left; width: 167px;">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Home</asp:HyperLink>
        </p>
    </div>
    <div class="style3">
        Products<br />
        <br />
    </div>
    <div class="style5">
        <span class="style4">Product Search:</span>
        <asp:TextBox ID="txtProduct" runat="server" Width="267px"></asp:TextBox>
        &nbsp;
        <asp:Button ID="btnSearch" runat="server" Height="23px" 
            onclick="btnSearch_Click" Text="Search" Width="86px" />
    </div>
    <asp:Label ID="lblProductResult" runat="server" CssClass="style6" 
        ForeColor="Red" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Literal ID="literalResults" runat="server"></asp:Literal>
    <br />
    <br />
    </form>
</body>
</html>
