﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUpload.aspx.cs" Inherits="FileUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            font-size: large;
            font-weight: bold;
            color: #006699;
        }
        .style3
        {
            font-size: small;
        }
        #File1
        {
            width: 438px;
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
    <div style="font-family: Arial, Helvetica, sans-serif; height: 155px;">
    
        <span class="style2">User File Upload&nbsp;&nbsp;
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Home</asp:HyperLink>
        <br />
        <br />
        </span><br />
        <asp:Label ID="Label1" runat="server" Text="File Name in Server:"></asp:Label>
&nbsp;<asp:TextBox ID="txtFileName" runat="server" Width="308px"></asp:TextBox>
        <br />
        <br />
        <span class="style3">File To Upload:</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     
        <input id="filePath" type="file" size="50" runat="server" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnUpload" runat="server" onclick="btnUpload_Click" 
            style="height: 26px; width: 60px; margin-right: 0px" Text="Upload" 
            Width="75px" />
    </div>
    </form>
</body>
</html>