'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/24/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data

Partial Class SecureRegister
    Inherits System.Web.UI.Page

    Protected Sub btnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        'Server validation of form data
        Dim result As String = Me.ValidateData()
        If (result <> "successful") Then
            Utilities.MessageBox(Me, result)
            Return
        End If

        'Check if two passowrds match
        If (Me.txtPassword.Text <> Me.txtConfirmPassword.Text) Then
            Me.lblPasswordsDontMatch.Visible = True
            Return
        Else
            Me.lblPasswordsDontMatch.Visible = False
        End If

        Dim HashedPassword As String = FormsAuthentication.HashPasswordForStoringInConfigFile(Me.txtPassword.Text, "sha1")
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
        Dim queryString As String = "insert into Users (Username, Password, FirstName, LastName, Email, DOB, CreditCardNum, Address, AccessLevel) values ("
        queryString += Utilities.SingleQuote(Me.txtUsername.Text) + ","
        queryString += Utilities.SingleQuote(HashedPassword) + ","
        queryString += Utilities.SingleQuote(Me.txtFirstName.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtLastName.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtEmail.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtDateOfBirth.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtCreditCard.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtAddress.Text) + ","
        queryString += Utilities.SingleQuote("User")
        queryString += ")"

        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Try
            Using (connection)
                Dim Command As SqlCommand = New SqlCommand(queryString, connection)
                Command.Connection.Open()
                Command.ExecuteNonQuery()
                Command.Connection.Close()
            End Using
            Dim strMessage As String = "Account registered successfully!"
            Utilities.MessageBox(Me, strMessage)
            If Not (Request.QueryString("returnUrl") Is Nothing) Then
                Response.Redirect(Request.QueryString("returnUrl"))
            Else
                Response.Redirect("SecureLogin.aspx")
            End If
        Catch ex As Exception
            'Log the exception
            Utilities.MessageBox(Me, "Error:: " + ex.ToString())
        End Try
    End Sub

    Protected Function ValidateData() As String

        Dim msg As String = "successful"
        If (Me.txtUsername.Text.Length = 0) Then
            msg = "Username is required!"
            Me.txtUsername.Focus()
        ElseIf (Me.txtPassword.Text.Length <= 6) Then
            msg = "Password must be at least 7 characters long!"
            Me.txtPassword.Focus()
        ElseIf (Me.txtFirstName.Text.Length = 0) Then
            Me.txtFirstName.Focus()
            msg = "First name is required!"
        ElseIf (Me.txtLastName.Text.Length = 0) Then
            Me.txtLastName.Focus()
            msg = "Last name is required!"
        ElseIf (Me.txtDateOfBirth.Text.Length < 6) Then
            Me.txtDateOfBirth.Focus()
            msg = "Invalid Date!"
        ElseIf (Me.txtCreditCard.Text.Length < 16) Then
            Me.txtCreditCard.Focus()
            msg = "Invalid Credit Card number!"
        ElseIf (Me.txtEmail.Text.Length < 6) Then
            Me.txtEmail.Focus()
            msg = "Invalid Email!"
        ElseIf (Me.txtAddress.Text.Length = 0) Then
            Me.txtAddress.Focus()
            msg = "Address is required!"
        End If
        Return msg
    End Function

End Class
