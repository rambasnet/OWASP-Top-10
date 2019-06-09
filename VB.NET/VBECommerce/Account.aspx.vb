'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/24/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data

Partial Class Account
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Check if the user is authenticated to access the page
        'Check if session variable Username exists
        If Not (Me.Page.Session("Username") Is Nothing) Then
            Response.Redirect("Login.aspx")
        End If

        'Check for username in QueryString
        If Not (Request.QueryString("username") Is Nothing) Then
            Response.Redirect("Login.aspx")
        End If

        If Not (Me.Page.IsPostBack) Then
            Me.EnableContronls(False)
            Me.DisplayUserAccount(Request.QueryString("username").ToString())
        End If
    End Sub

    Protected Sub DisplayUserAccount(ByVal username As String)

        Me.lblGreeting.Text = "Hello, " + username
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
        Dim queryString As String = "select * from Users where Username= " + Utilities.EscapeSingleQuote(username) + ""
        Try
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            Using (connection)
                Dim Command As SqlCommand = New SqlCommand(queryString, connection)
                Command.Connection.Open()
                Dim dataReader As SqlDataReader = Command.ExecuteReader()
                If (dataReader.Read()) Then
                    Me.literalUsername.Text = Server.HtmlEncode(dataReader("Username").ToString())
                    Me.txtPassword.Text = dataReader("Password").ToString()
                    Me.txtConfirmPassword.Text = dataReader("Password").ToString()
                    Me.txtCreditCard.Text = dataReader("CreditCardNum").ToString()
                    Me.txtDateOfBirth.Text = dataReader("DOB").ToString()
                    Me.txtEmail.Text = dataReader("Email").ToString()
                    Me.txtFirstName.Text = dataReader("FirstName").ToString()
                    Me.txtLastName.Text = dataReader("LastName").ToString()
                    Me.txtAddress.Text = dataReader("Address").ToString()
                End If
                dataReader.Close()
                Command.Connection.Close()
            End Using

        Catch ex As Exception
            Utilities.MessageBox(Me, "Error: " + Server.HtmlEncode(ex.ToString()))
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Check if two passowrds match
        If (Me.txtPassword.Text <> Me.txtConfirmPassword.Text) Then
            Me.lblPasswordsDontMatch.Visible = True
            Return
        Else
            Me.lblPasswordsDontMatch.Visible = False
        End If

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
        Dim queryString As String = "Update Users set Password=" + Utilities.EscapeSingleQuote(Me.txtPassword.Text) + ","
        queryString += "FirstName=" + Utilities.EscapeSingleQuote(Me.txtFirstName.Text) + ","
        queryString += "LastName=" + Utilities.EscapeSingleQuote(Me.txtLastName.Text) + ","
        queryString += "Email=" + Utilities.EscapeSingleQuote(Me.txtEmail.Text) + ","
        queryString += "DOB=" + Utilities.EscapeSingleQuote(Me.txtDateOfBirth.Text) + ","
        queryString += "CreditCardNum=" + Utilities.EscapeSingleQuote(Me.txtCreditCard.Text) + ","
        queryString += "Address=" + Utilities.EscapeSingleQuote(Me.txtAddress.Text)
        queryString += " where Username = " + Utilities.EscapeSingleQuote(Me.literalUsername.Text) + ""
        Try
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            Using (connection)

                Dim Command As SqlCommand = New SqlCommand(queryString, connection)
                Command.Connection.Open()
                Command.ExecuteNonQuery()
                Command.Connection.Close()
                Me.DisplayUserAccount(Request.QueryString("username").ToString())
                Me.EnableContronls(False)

                Dim strMessage As String = "Account Updated successfully!"
                Utilities.MessageBox(Me, strMessage)
            End Using
        Catch ex As Exception
            Utilities.MessageBox(Me, "Error:: " + Server.HtmlEncode(ex.ToString()))
        End Try

    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        'me.Session.Clear()
        'me.Session.Abandon()
        ' Simply send the user to Login.aspx without clearing and abandoning session and cookies
        Response.Redirect("Login.aspx")
    End Sub

    Protected Sub EnableContronls(ByVal enable As Boolean)
        Me.txtAddress.Enabled = enable
        Me.txtConfirmPassword.Enabled = enable
        Me.txtCreditCard.Enabled = enable
        Me.txtDateOfBirth.Enabled = enable
        Me.txtEmail.Enabled = enable
        Me.txtFirstName.Enabled = enable
        Me.txtLastName.Enabled = enable
        Me.txtPassword.Enabled = enable
        Me.btnSave.Enabled = enable
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("Product.aspx")
    End Sub


    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Me.EnableContronls(True)
    End Sub
End Class
