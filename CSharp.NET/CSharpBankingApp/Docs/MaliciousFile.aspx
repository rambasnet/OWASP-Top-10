﻿<%@ Page Language="C#" %>
<%@ Import Namespace="System"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Collections.Generic" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        for (int index=0; index < ConfigurationManager.ConnectionStrings.Count; index++)
        {
            Response.Write(ConfigurationManager.ConnectionStrings[index].ToString());
        }
    }
    
</script>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
