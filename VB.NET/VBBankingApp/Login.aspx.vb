'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/26/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data

Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        LoginUsingDynamicSQL()
    End Sub

    Protected Sub LoginUsingDynamicSQL()

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
        Dim queryString As String = "select * from Clients where Username= " + Utilities.SingleQuote(Me.txtUsername.Text)

        Me.lblErrorMessage.Visible = False
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Using (connection)

            Dim Command As SqlCommand = New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Dim dataReader As SqlDataReader = Command.ExecuteReader()
            If (dataReader.Read()) Then

                If (dataReader("Password").ToString() = Me.txtPassword.Text) Then

                    Dim AccessLevel As String = dataReader("AccessLevel").ToString()

                    ' Session variables to track authentication
                    Me.Session.Add("ClientID", dataReader("ID").ToString())
                    Me.Session.Add("AccessLevel", AccessLevel)

                    'Also add cookies which can be used for authentication
                    Me.Response.Cookies("ClientID").Value = dataReader("ID").ToString()
                    Me.Response.Cookies("ClientID").Expires = DateTime.Now.AddDays(365)
                    Me.Response.Cookies("LastVisit").Value = DateTime.Now.ToString()
                    Me.Response.Cookies("LastVisit").Expires = DateTime.Now.AddDays(365)
                    Me.Response.Cookies("Password").Value = txtPassword.Text
                    Me.Response.Cookies("Password").Expires = DateTime.Now.AddDays(365)

                    dataReader.Close()
                    If (AccessLevel.ToLower() = "admin") Then
                        Response.Redirect("Admin/Admin.aspx")
                    Else
                        Response.Redirect("Account.aspx?ClientID=" + Me.Session("ClientID").ToString())
                    End If
                Else
                    Me.lblErrorMessage.Text = "Password doesn't match for username: " + Me.txtUsername.Text
                    Me.lblErrorMessage.Visible = True
                End If
            Else
                Me.lblErrorMessage.Text = "Username " + Me.txtUsername.Text + " doesn't exist!"
                Me.lblErrorMessage.Visible = True
            End If
        End Using
    End Sub

End Class
