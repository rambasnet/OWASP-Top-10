<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" ValidateRequest="false"%>

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
        .style2
        {
            font-size: small;
        }
        .style3
        {
            text-align: center;
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
            font-size: x-large;
        }
        #Text1
        {
            width: 249px;
        }
        .style7
        {
            color: #0066CC;
            font-size: small;
            font-weight: bold;
        }
        </style>
 

</head>
<!--
 <body onload="javascript:alert(document.cookie);">
-->
<body>
    <form id="form1" runat="server">
    <div class="style7">
    <asp:Label ID="lblUsername" runat="server" Text="Hello, Guest" 
            style="font-size: small"></asp:Label>
    </div>
    <div class="style1">
            <span class="style2">
           
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="Login.aspx">Login to Your Online banking!</a>&nbsp;
        <br />
        <br />
        </span>
    
    </div>
    <div class="style3">
        Welcome to Your Online Bank!         <br />
    </div>
    
    </form>
    
    <form action="http://localhost:1298/MaliciousSite/Default.aspx" name="MaliciousForm" method="get">
    <input type="hidden" id="MaliciousControl" name="MaliciousControl" value="" />
    </form>
    <p style="font-family: Arial, Helvetica, sans-serif; font-size: small">
        &nbsp;</p>
    
    </body>
</html>
