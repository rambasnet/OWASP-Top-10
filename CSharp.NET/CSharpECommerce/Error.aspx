<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Curstom Error Page!</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <b>Custom Error page!</b>
     <br>
         You have been redirected here from the customErrors <customErrors> section of the 
         web.config file.<br />
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" 
            Font-Size="Large" ForeColor="Red" 
            Text="Oops... Error Occured! Sorry for inconvinience!!"></asp:Label>
        <br />
        <br />
        <asp:Literal ID="litDetailError" runat="server"></asp:Literal>
        
    </div>
    </form>
</body>
</html>
