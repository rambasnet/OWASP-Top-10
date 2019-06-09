<%@ Application Language="VB" %>
<%@ Import Namespace="System.Diagnostics" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
        
        Dim ex As Exception = Server.GetLastError().GetBaseException()
        Dim errorMsg As String = "Error Caught in Application_Error event::\n" & _
                "Error in: " + Request.Url.ToString() & _
                "\n\nError Message: " + ex.Message.ToString() & _
                "\n\nStack Trace: " + ex.StackTrace.ToString() & _
                "\n\nClient IP: " + Request.ServerVariables("REMOTE_ADDR")
        
        'Log it to Windows Event logs
        EventLog.WriteEntry("CSharpEcommerce", errorMsg, EventLogEntryType.Error)
        
        'Clear the error if you do not want the error to go on to Web.config file as the last point to trap it
        'Server.ClearError()
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
</script>