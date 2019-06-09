<%@ Application Language="C#" %>
<%@ Import Namespace="System.Diagnostics" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e)
    { 
        // Code that runs when an unhandled error occurs
        
        Exception ex = Server.GetLastError().GetBaseException();
        string error = "Error Caught in Application_Error event::\n" +
                "Error in: " + Request.Url.ToString() +
                "\n\nError Message: " + ex.Message.ToString() +
                "\n\nStack Trace: " + ex.StackTrace.ToString() +
                "\n\nClient IP: " + Request.ServerVariables["REMOTE_ADDR"];
        
        //Log it to Windows Event logs
        EventLog.WriteEntry("CSharpEcommerce", error, EventLogEntryType.Error);
        
        //Clear the error if you do not want the error to go on to Web.config file as the last point to trap it
        //Server.ClearError();
        //Utilities.MessageBox(this, "bla");
 
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
