'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/25/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data

Partial Class Admin_DeleteUser1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Doesn't check for Admin Privilege
        ' Simply deletes the account by reading the username in url and
        ' directs the page to SecureLogin.aspx
        Try

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
        Catch ex As Exception
            ' Log the exception
            Utilities.MessageBox(Me, "Error:: " + ex.ToString())
        End Try
    End Sub
End Class
