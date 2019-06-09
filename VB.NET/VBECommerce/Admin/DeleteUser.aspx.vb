'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/25/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data

Partial Class Admin_DeleteUser
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Check if the user is authenticated to access the page
        ' Doesn't re-verify authorization of action
        'Check if session variable Username exists
        If (Me.Page.Session("Username") Is Nothing) Then
            Response.Redirect("../SecureLogin.aspx")
            ' Now check if the user has Admin privilege
        Else
            If (Me.Page.Session("AccessLevel") Is Nothing) Then
                Response.Redirect("../SecureLogin.aspx")
            End If
        End If

        If (Request.Params("username") Is Nothing) Then
            Response.Redirect("../SecureLogin.aspx")
        Else
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
            Dim queryString As String = "DELETE Users WHERE Username= " + Utilities.EscapeSingleQuote(Request.Params("username")) + ""
            Try
                Dim connection As SqlConnection = New SqlConnection(connectionString)
                Using (connection)

                    Dim Command As SqlCommand = New SqlCommand(queryString, connection)
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()

                    Dim strMessage As String = "Account Deleted successfully!"
                    Utilities.MessageBox(Me, strMessage)
                    Response.Redirect("SecureAdmin.aspx")
                End Using
            Catch ex As Exception
                'log the exception
                Utilities.MessageBox(Me, "Error: " + Server.HtmlEncode(ex.ToString()))
            End Try
        End If

    End Sub
End Class
