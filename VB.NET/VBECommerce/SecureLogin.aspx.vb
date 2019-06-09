'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/24/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data


Partial Class SecureLogin
    Inherits System.Web.UI.Page

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Me.SecureLoginWithSanitizedInput()

        'me.SecureLoginWithParameterizedStoredProcedure()
        'me.SecureLoginWithParameterizedDynamicSQL()
    End Sub

    Protected Sub SecureLoginWithSanitizedInput()

        If (Me.txtUsername.Text.Length = 0) Then

            Utilities.MessageBox(Me, "Username is required!")
            Return
        ElseIf (Me.txtPassword.Text.Length = 0) Then
            Utilities.MessageBox(Me, "Password is required!")
            Return
        End If

        'stor hashed password
        Dim hashedPassword As String = FormsAuthentication.HashPasswordForStoringInConfigFile(Me.txtPassword.Text, "sha1")
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
        Dim queryString As String = "select * from Users where Username= " + Utilities.EscapeSingleQuote(Me.txtUsername.Text) + " and Password = " + Utilities.EscapeSingleQuote(hashedPassword) + ""

        Me.lblErrorMessage.Visible = False
        Try
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            'using statement automatically closes the connection when it exists
            Using (connection)

                Dim Command As SqlCommand = New SqlCommand(queryString, connection)
                Command.Connection.Open()
                Dim dataReader As SqlDataReader = Command.ExecuteReader()
                If (dataReader.Read()) Then

                    '/* Add a globally unique identifier (guid) as a session id token
                    ' * A GUID is a Globally Unique ID, a 128-bit number that can be quickly 
                    ' * generated with one method call, producing a hexidecimal string that 
                    ' * is very difficult to guess, ex. 9c7aa4a6-c1f4-424c-a4be-f7ea0cb4744b
                    '*/
                    Me.Session.Add("Token", System.Guid.NewGuid().ToString())

                    Dim AccessLevel As String = dataReader("AccessLevel").ToString()

                    Me.Session.Add("Username", dataReader("Username").ToString())
                    Me.Session.Add("AccessLevel", AccessLevel)
                    dataReader.Close()
                    If (AccessLevel.ToLower() = "admin") Then
                        Response.Redirect("Admin/SecureAdmin.aspx")
                    Else
                        Response.Redirect("SecureAccount.aspx?username=" + Server.UrlEncode(Me.txtUsername.Text))
                    End If
                Else

                    Me.lblErrorMessage.Text = "Invalid Username or Password!"
                    Me.lblErrorMessage.Visible = True

                End If
            End Using

        Catch ex As Exception

            'Internally log error message with time, client ip, and url
            Me.lblErrorMessage.Text = "Invalid Username or Password!"
            Me.lblErrorMessage.Visible = True
        End Try
    End Sub

    'User parameters with stored procedure
    Protected Sub SecureLoginWithParameterizedStoredProcedure()

        If (Me.txtUsername.Text.Length = 0) Then

            Utilities.MessageBox(Me, "Username is required!")
            Return
        ElseIf (Me.txtPassword.Text.Length = 0) Then

            Utilities.MessageBox(Me, "Password is required!")
            Return
        End If
        Dim hashedPassword As String = FormsAuthentication.HashPasswordForStoringInConfigFile(Me.txtPassword.Text, "sha1")
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString

        Me.lblErrorMessage.Visible = False
        Try
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            'automatically closes the connection
            Using (connection)

                Dim Command As SqlCommand = New SqlCommand("LoginStoredProcedure", connection)
                'Tell the command to execute stored procedure
                Command.CommandType = CommandType.StoredProcedure

                Command.Connection.Open()
                Command.Parameters.Add("@Username", SqlDbType.VarChar, 50)
                Command.Parameters("@Username").Value = Me.txtUsername.Text

                Command.Parameters.Add("@Password", SqlDbType.VarChar, 256)
                Command.Parameters("@Password").Value = hashedPassword

                Dim dataReader As SqlDataReader = Command.ExecuteReader()
                If (dataReader.Read()) Then

                    '/* Add a globally unique identifier (guid) as a session id token
                    ' * A GUID is a Globally Unique ID, a 128-bit number that can be quickly 
                    ' * generated with one method call, producing a hexidecimal string that 
                    ' * is very difficult to guess, ex. 9c7aa4a6-c1f4-424c-a4be-f7ea0cb4744b
                    '*/
                    Dim guid As String = System.Guid.NewGuid().ToString()
                    Me.Session.Add("Token", Guid)

                    Dim AccessLevel As String = dataReader("AccessLevel").ToString()

                    Me.Session.Add("Username", dataReader("Username").ToString())
                    Me.Session.Add("AccessLevel", AccessLevel)
                    dataReader.Close()
                    If (AccessLevel = "Admin") Then
                        Response.Redirect("Admin/SecureAdmin.aspx")
                    Else
                        Response.Redirect("SecureAccount.aspx?username=" + Server.UrlEncode(Me.txtUsername.Text))
                    End If
                else
                    Me.lblErrorMessage.Text = "Invalid Username or Password!"
                    Me.lblErrorMessage.Visible = True
                End If
            End Using

        Catch ex As Exception

            'Internally log error message with time, client ip, and url
            Me.lblErrorMessage.Text = "Invalid Username or Password!"
            Me.lblErrorMessage.Visible = True
        End Try
    End Sub

    'User parameters with dynamic sql
    Protected Sub SecureLoginWithParameterizedDynamicSQL()

        If (Me.txtUsername.Text.Length = 0) Then
            Utilities.MessageBox(Me, "Username is required!")
            Return
        ElseIf (Me.txtPassword.Text.Length = 0) Then
            Utilities.MessageBox(Me, "Password is required!")
            Return
        End If

        Dim hashedPassword As String = FormsAuthentication.HashPasswordForStoringInConfigFile(Me.txtPassword.Text, "sha1")
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
        Dim queryString As String = "select * from Users where Username= @Username and Password = @Password"
        Me.lblErrorMessage.Visible = False
        Try
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            'automatically closes the connection
            Using (connection)
                Dim Command As SqlCommand = New SqlCommand(queryString, connection)

                Command.Connection.Open()
                Command.Parameters.Add("@Username", SqlDbType.VarChar, 25)
                Command.Parameters("@Username").Value = Me.txtUsername.Text

                Command.Parameters.Add("@Password", SqlDbType.VarChar, 256)
                Command.Parameters("@Password").Value = hashedPassword

                Dim dataReader As SqlDataReader = Command.ExecuteReader()
                If (dataReader.Read()) Then

                    '/* Add a globally unique identifier (guid) as a session id token
                    ' * A GUID is a Globally Unique ID, a 128-bit number that can be quickly 
                    ' * generated with one method call, producing a hexidecimal string that 
                    ' * is very difficult to guess, ex. 9c7aa4a6-c1f4-424c-a4be-f7ea0cb4744b
                    '*/
                    Me.Session.Add("Token", System.Guid.NewGuid().ToString())

                    Dim AccessLevel As String = dataReader("AccessLevel").ToString()
                    'Add a globally unique identifier to thawrt xsrf
                    Me.Session.Add("Token", System.Guid.NewGuid().ToString())
                    Me.Session.Add("Username", dataReader("Username").ToString())
                    Me.Session.Add("AccessLevel", AccessLevel)
                    dataReader.Close()
                    If (AccessLevel = "Admin") Then
                        Response.Redirect("Admin/SecureAdmin.aspx")
                    Else
                        Response.Redirect("SecureAccount.aspx?username=" + Server.UrlEncode(Me.txtUsername.Text))
                    End If
                Else
                    Me.lblErrorMessage.Text = "Invalid Username or Password!"
                    Me.lblErrorMessage.Visible = True
                End If
            End Using
        catch ex As Exception

            'Internally log error message with time, client ip, and url
            Me.lblErrorMessage.Text = "Invalid Username or Password!"
            Me.lblErrorMessage.Visible = True
        End Try
    End Sub

End Class
