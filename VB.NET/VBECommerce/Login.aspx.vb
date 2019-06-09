'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/24/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data

Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        'Information Leakage, sql injection
        Me.LoginUsingDynamicSQL()

        'Insecure Stored Procedure usage, sql injection
        'this.LoginWithStoredProcedure()
    End Sub

    Protected Sub LoginUsingDynamicSQL()

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localAdminConnection").ConnectionString
        Dim queryString As String = "select * from Users where Username= " + Utilities.SingleQuote(Me.txtUsername.Text)

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
                    Me.Session.Add("Username", dataReader("Username").ToString())
                    Me.Session.Add("AccessLevel", AccessLevel)

                    'Also add cookies which can be used for authentication
                    Me.Response.Cookies("Username").Value = dataReader("Username").ToString()
                    Me.Response.Cookies("Username").Expires = DateTime.Now.AddDays(365)
                    Me.Response.Cookies("LastVisit").Value = DateTime.Now.ToString()
                    Me.Response.Cookies("LastVisit").Expires = DateTime.Now.AddDays(365)
                    Me.Response.Cookies("Password").Value = txtPassword.Text
                    Me.Response.Cookies("Password").Expires = DateTime.Now.AddDays(365)
                    'Limit cookie domain scope
                    Me.Response.Cookies("domain").Value = DateTime.Now.ToString()
                    Me.Response.Cookies("domain").Expires = DateTime.Now.AddDays(365)
                    Response.Cookies("domain").Domain = ".domain.com"
                    dataReader.Close()
                    If (AccessLevel = "Admin") Then

                        Response.Redirect("Admin/Admin.aspx")

                    Else

                        Response.Redirect("Account.aspx?username=" + Me.txtUsername.Text)
                    End If

                Else

                    Me.lblErrorMessage.Text = "Password doesn't match for username: " + dataReader("Username").ToString()
                    Me.lblErrorMessage.Visible = True
                End If

            Else

                Me.lblErrorMessage.Text = "Username " + Me.txtUsername.Text + " doesn't exist!"
                Me.lblErrorMessage.Visible = True
            End If
        End Using

    End Sub

    'Use of stored procedure
    Protected Sub LoginWithStoredProcedure()

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localAdminConnection").ConnectionString

        Dim queryString As String = "select * from Users where Username= " + Utilities.SingleQuote(Me.txtUsername.Text) + " and Password = " + Utilities.SingleQuote(Me.txtPassword.Text) + ""
        'Response.Write(queryString)
        Me.lblErrorMessage.Visible = False
        'return
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Using (connection)

            Dim Command As SqlCommand = New SqlCommand("ExecuteStoredProcedure", connection)
            'Tell the command to execute stored procedure
            Command.CommandType = CommandType.StoredProcedure

            Command.Connection.Open()
            Command.Parameters.Add("@sql", SqlDbType.NVarChar, 4000)
            Command.Parameters("@sql").Value = queryString

            'command.Parameters.Add("@Password", SqlDbType.VarChar, 256)
            'command.Parameters("@Password").Value = me.txtPassword.Text

            Dim dataReader As SqlDataReader = Command.ExecuteReader()
            If (dataReader.Read()) Then

                Dim AccessLevel As String = dataReader("AccessLevel").ToString()

                Me.Session.Add("Username", dataReader("Username").ToString())
                Me.Session.Add("AccessLevel", AccessLevel)
                dataReader.Close()
                If (AccessLevel = "Admin") Then

                    Response.Redirect("Admin/Admin.aspx")

                Else

                    Response.Redirect("Account.aspx?username=" + Me.txtUsername.Text)
                End If


            Else

                Me.lblErrorMessage.Text = "Invalid Username or Password!"
                Me.lblErrorMessage.Visible = True


            End If
        End Using
    End Sub


End Class
