<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Product.aspx.cs" Inherits="Product"  ValidateRequest="false" %>

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
        #Text1
        {
            width: 249px;
        }
        .style4
        {
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
        }
        .style5
        {
            text-align: center;
        }
        .style6
        {
            font-family: Arial, Helvetica, sans-serif;
        }
    </style>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function Submit1_onclick() {

        }

// ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="style1">
    
        <p style="margin-left: 480px; text-align: right; width: 167px;">
    <asp:Label ID="lblUsername" runat="server" Text="Hello Username |" Font-Bold="True" 
                Font-Size="Small" ForeColor="Blue" 
                style="font-family: Arial, Helvetica, sans-serif" Visible="False"></asp:Label>
            &nbsp;<asp:LinkButton ID="btnLogout" runat="server" onclick="btnLogout_Click" 
                Font-Bold="True" Font-Size="Small" Visible="False">Log out</asp:LinkButton>
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
    <asp:Label ID="lblProductResult" runat="server" ForeColor="Red" Text="Label" 
        CssClass="style6"></asp:Label>
    <br />
    <br />
    <asp:Literal ID="literalResults" runat="server"></asp:Literal>
    <br />
    <br />
    </form>
    </body>
</html>
