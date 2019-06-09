<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImageUpload.aspx.vb" Inherits="ImageUpload" %>

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
        </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" method="post">
    <div style="font-family: Arial, Helvetica, sans-serif">
        <span class="style2">User File Upload&nbsp;&nbsp;
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Home</asp:HyperLink>
        </span>
        <br />
        <br />
        <span class="style3">Enter Local File Path to Upload:</span>&nbsp;&nbsp;
        <input id="filePath" runat="server" size="50" type="file" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnUpload" runat="server" onclick="btnUpload_Click" 
            style="height: 26px; width: 60px; margin-right: 0px" Text="Upload" 
            Width="75px" />
    </div>
    </form>
</body>
</html>
