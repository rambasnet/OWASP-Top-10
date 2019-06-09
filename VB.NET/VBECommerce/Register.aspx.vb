'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/24/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data

Partial Class Register
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        'Check if two passowrds match
        If (Me.txtPassword.Text <> Me.txtConfirmPassword.Text) Then

            Me.lblPasswordsDontMatch.Visible = True
            Return

        Else

            Me.lblPasswordsDontMatch.Visible = False

        End If
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
        Dim queryString As String = "insert into Users (Username, Password, FirstName, LastName, Email, DOB, CreditCardNum, Address, AccessLevel) values ("
        queryString += Utilities.SingleQuote(Me.txtUsername.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtPassword.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtFirstName.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtLastName.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtEmail.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtDateOfBirth.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtCreditCard.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtAddress.Text) + ","
        queryString += Utilities.SingleQuote("User")
        queryString += ")"

        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Using (connection)

            Dim Command As SqlCommand = New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
            Command.Connection.Close()
        End Using
        Dim strMessage As String = "Account registered successfully!"
        Utilities.MessageBox(Me, strMessage)
        Response.Redirect("Login.aspx")
    End Sub
End Class
