
Partial Class _Default
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Request.Params("MaliciousControl") Is Nothing Then
            Response.Write("Haha gotcha...</br>I wouldn't normally let you know about it, but here you go... </br>Your cookie is: ")
            ' record the received cookie infor into database or something
            ' display it back to the victim for demo purpose
            Response.Write(Request.Params("MaliciousControl"))
        Else
            Response.Write("No malicious script injected!")
        End If
    End Sub
End Class
