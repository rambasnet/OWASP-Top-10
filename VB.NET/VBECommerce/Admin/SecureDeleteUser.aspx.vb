'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/25/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data

Partial Class Admin_SecureDeleteUser
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Check the user authentication...me should safeguard against CSRF/XSRF/one-click attack, session riding
        If (Me.Session("Token") Is Nothing Or Context.Items("Token") Is Nothing) Then
            'Log me activity as a possible attack
            Response.Redirect("../SecureLogin.aspx")
            Return
        End If


        'Requiring authentication in GET and POST parameters, not only cookies
        'reverify the authorization of action from the legitimate by comparing the tokens
        If (Context.Items("Token").ToString() <> Me.Session("Token").ToString()) Then
            ' Some must be trying for CSRS attackt
            ' Log me activity if you wish
            Response.Redirect("../SecureLogin.aspx")
        End If

        'Check if the user is authenticated to access the page

        'Check if session variable Username exists
        If (Me.Page.Session("Username") Is Nothing) Then
            Response.Redirect("../SecureLogin.aspx")
        Else
            ' Now check if the user has Admin privilege
            If (Me.Page.Session("AccessLevel") Is Nothing) Then

                Response.Redirect("../SecureLogin.aspx")
            Else
                If (Me.Page.Session("AccessLevel").ToString().ToLower() <> "admin") Then
                    Response.Redirect("../SecureLogin.aspx")
                    ' has admin privilege otherwise
                End If
            End If
        End If

        If (Request.Params("username") Is Nothing) Then
            Response.Redirect("../SecureAdmin.aspx")
        Else

            Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
            Dim queryString As String = "DELETE Users WHERE Username= @Username"
            Try
                Dim connection As SqlConnection = New SqlConnection(connectionString)
                Using (connection)

                    Dim Command As SqlCommand = New SqlCommand(queryString, connection)

                    Command.Connection.Open()
                    Command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 25)
                    Command.Parameters("@Username").Value = Request.Params("username")
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
