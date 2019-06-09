'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/26/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data

Partial Class Admin_Register
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

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localAdminConnection").ConnectionString
        Dim queryString As String = "insert into Clients (Username, Password, FirstName, LastName, Email, DOB, SSN, Address, AccessLevel) values ("
        queryString += Utilities.SingleQuote(Me.txtUsername.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtPassword.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtFirstName.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtLastName.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtEmail.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtDateOfBirth.Text) + ","
        queryString += Utilities.SingleQuote(Me.txSSN.Text) + ","
        queryString += Utilities.SingleQuote(Me.txtAddress.Text) + ","
        queryString += Utilities.SingleQuote(Me.ddlAccessLevel.SelectedItem.ToString())
        queryString += ") SELECT CustID = SCOPE_IDENTITY()"

        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Using (connection)
            Dim Command As SqlCommand = New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Dim dataReader As SqlDataReader = Command.ExecuteReader()

            If (dataReader.HasRows) Then
                dataReader.Read()
                Dim clientID As Long = Convert.ToInt32(dataReader("CustID"))
                dataReader.Close()
                queryString = "insert into Accounts(ClientID, AccountType, Balance) values ("
                queryString += ClientID.ToString() + "," + Utilities.SingleQuote(Me.ddlAccountType.SelectedItem.ToString()) + "," + Me.txtBalance.Text + ")"
                Command.CommandText = queryString
                Command.ExecuteNonQuery()
            End If

            Dim strMessage As String = "Account created successfully!"
            Utilities.MessageBox(Me, strMessage)
            Response.Redirect("Admin.aspx")
        End Using
    End Sub
End Class
