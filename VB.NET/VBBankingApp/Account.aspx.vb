'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/25/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data

Partial Class Account
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Check if the user is authenticated to access the page
        'Check if session variable Username exists
        If (Me.Page.Session("ClientID") Is Nothing) Then
            Response.Redirect("Login.aspx")
        End If

        'Check for username in QueryString
        If (Request.QueryString("ClientID") Is Nothing) Then
            Response.Redirect("Login.aspx")
        End If

        If Not (Me.Page.IsPostBack) Then

            Me.EnableContronls(False)
            Me.DisplayUserProfile(Request.QueryString("ClientID").ToString())
            Me.LoadAccounts(Request.QueryString("ClientID").ToString())
            Me.ddlAccounts_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Protected Sub DisplayUserProfile(ByVal ClientID As String)

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localAdminConnection").ConnectionString
        Dim queryString As String = "select * from Clients where ID= " + ClientID + ""
        Try
            Dim connection As SqlConnection = New SqlConnection(connectionString)
        
            Using (connection)

                Dim Command As SqlCommand = New SqlCommand(queryString, connection)
                Command.Connection.Open()
                Dim dataReader As SqlDataReader = Command.ExecuteReader()
                If (dataReader.Read()) Then
                    Me.lblGreeting.Text = "Hello, " + dataReader("Username").ToString()
                    Me.literalUsername.Text = dataReader("Username").ToString()
                    Me.txtPassword.Text = dataReader("Password").ToString()
                    Me.txtConfirmPassword.Text = dataReader("Password").ToString()
                    Me.txtSSN.Text = dataReader("SSN").ToString()
                    Me.txtDateOfBirth.Text = dataReader("DOB").ToString()
                    Me.txtEmail.Text = dataReader("Email").ToString()
                    Me.txtFirstName.Text = dataReader("FirstName").ToString()
                    Me.txtLastName.Text = dataReader("LastName").ToString()
                    Me.txtAddress.Text = dataReader("Address").ToString()

                    dataReader.Close()
                End If
            End Using

        Catch ex As Exception
            Utilities.MessageBox(Me, "Error: " + ex.ToString())
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

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localAdminConnection").ConnectionString
        Dim queryString As String = "Update Clients set Password=" + Utilities.SingleQuote(Me.txtPassword.Text) + ","
        queryString += "FirstName=" + Utilities.SingleQuote(Me.txtFirstName.Text) + ","
        queryString += "LastName=" + Utilities.SingleQuote(Me.txtLastName.Text) + ","
        queryString += "Email=" + Utilities.SingleQuote(Me.txtEmail.Text) + ","
        queryString += "DOB=" + Utilities.SingleQuote(Me.txtDateOfBirth.Text) + ","
        queryString += "SSN=" + Utilities.SingleQuote(Me.txtSSN.Text) + ","
        queryString += "Address=" + Utilities.SingleQuote(Me.txtAddress.Text)
        queryString += " where ID = " + Request.QueryString("ClientID").ToString() + ""

        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Using (connection)

            Dim Command As SqlCommand = New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
            Command.Connection.Close()
            Me.DisplayUserProfile(Request.QueryString("ClientID").ToString())
            Me.LoadAccounts(Request.QueryString("ClientID"))
            Me.EnableContronls(False)

            Dim strMessage As String = "Account Updated successfully!"
            Utilities.MessageBox(Me, strMessage)
        End Using

    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Me.EnableContronls(True)
    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Response.Redirect("Login.aspx")
    End Sub

    Protected Sub EnableContronls(ByVal enable As Boolean)

        Me.txtAddress.Enabled = enable
        Me.txtConfirmPassword.Enabled = enable
        Me.txtSSN.Enabled = enable
        Me.txtDateOfBirth.Enabled = enable
        Me.txtEmail.Enabled = enable
        Me.txtFirstName.Enabled = enable
        Me.txtLastName.Enabled = enable
        Me.txtPassword.Enabled = enable
        Me.btnSave.Enabled = enable
    End Sub

    Protected Sub LoadAccounts(ByVal clientID As String)

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localAdminConnection").ConnectionString
        Dim queryString As String = "select AccountNum from Accounts where ClientID=" + clientID + ""
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Using (connection)

            Dim Command As SqlCommand = New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Dim dataReader As SqlDataReader = Command.ExecuteReader()
            Me.ddlAccounts.Items.Clear()
            While (dataReader.Read())
                Me.ddlAccounts.Items.Add(dataReader("AccountNum").ToString())
            End While
            dataReader.Close()
        End Using
    End Sub

    Protected Sub ddlAccounts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAccounts.SelectedIndexChanged
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localAdminConnection").ConnectionString
        Dim queryString As String = "select AccountType, Balance from Accounts where AccountNum= " + Utilities.SingleQuote(Me.ddlAccounts.SelectedValue) + ""
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Using (connection)

            Dim Command As SqlCommand = New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Dim dataReader As SqlDataReader = Command.ExecuteReader()
            If (dataReader.Read()) Then
                Me.lblAccountType.Text = dataReader("AccountType").ToString()
                Me.lblBalance.Text = dataReader("Balance").ToString()
            End If
            dataReader.Close()
        End Using

    End Sub
End Class
