'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/24/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data

Partial Class Admin_Admin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Me.Page.IsPostBack) Then
            Me.EnableControls(False)
            Me.LoadUsernames()
            Me.DropDownList1_SelectedIndexChanged(sender, e)
        End If
    End Sub


    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUsers.SelectedIndexChanged
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
        Dim queryString As String = "select * from Users where Username= " + Utilities.SingleQuote(Me.ddlUsers.SelectedValue)

        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Using (connection)

            Dim command As SqlCommand = New SqlCommand(queryString, connection)
            command.Connection.Open()
            Dim dataReader As SqlDataReader = command.ExecuteReader()
            If (dataReader.Read()) Then

                Me.ddlAccessLevel.SelectedValue = dataReader("AccessLevel").ToString()
                'Response.Write(dataReader("Username").ToString())
                Me.literalUsername.Text = dataReader("Username").ToString()
                'me.txtUsername.Text = Server.HtmlDecode(dataReader("Username").ToString())
                Me.txtPassword.Text = dataReader("Password").ToString()
                Me.txtConfirmPassword.Text = dataReader("Password").ToString()
                Me.txtCreditCard.Text = dataReader("CreditCardNum").ToString()
                Me.txtDateOfBirth.Text = dataReader("DOB").ToString()
                Me.txtEmail.Text = dataReader("Email").ToString()
                Me.txtFirstName.Text = dataReader("FirstName").ToString()
                Me.txtLastName.Text = dataReader("LastName").ToString()
                Me.litAddress.Text = dataReader("Address").ToString()

                dataReader.Close()
                command.Connection.Close()
            End If
        End Using
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Me.EnableControls(True)
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        'Delete right here or ask another form to delete it...

        'Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
        'Dim queryString As String = "DELETE Users WHERE Username= " + Utilities.SingleQuote(Me.literalUsername.Text) + ""
        'Dim connection As SqlConnection = New SqlConnection(connectionString)
        'Using (connection)

        '    Dim Command As SqlCommand = New SqlCommand(queryString, connection)
        '    Command.Connection.Open()
        '    Command.ExecuteNonQuery()

        '    Dim strMessage As String = "Account Deleted successfully!"
        '    Utilities.MessageBox(Me, strMessage)
        '    Me.LoadUsernames()
        '    'me.SqlDataSource1.Select()
        '    Me.DropDownList1_SelectedIndexChanged(sender, e)
        '    '
        'End Using
        Response.Redirect("DeleteUser1.aspx?username=" + Server.UrlEncode(Me.ddlUsers.SelectedItem.ToString()))
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("../Register.aspx")
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
        Dim queryString As String = "Update Users set Password=" + Utilities.SingleQuote(Me.txtPassword.Text) + ","
        queryString += "FirstName=" + Utilities.SingleQuote(Me.txtFirstName.Text) + ","
        queryString += "LastName=" + Utilities.SingleQuote(Me.txtLastName.Text) + ","
        queryString += "Email=" + Utilities.SingleQuote(Me.txtEmail.Text) + ","
        queryString += "DOB=" + Utilities.SingleQuote(Me.txtDateOfBirth.Text) + ","
        queryString += "CreditCardNum=" + Utilities.SingleQuote(Me.txtCreditCard.Text) + ","
        queryString += "Address=" + Utilities.SingleQuote(Me.litAddress.Text) + ","
        queryString += "AccessLevel=" + Utilities.SingleQuote(Me.ddlAccessLevel.SelectedValue)
        queryString += " where Username = " + Utilities.SingleQuote(ddlUsers.SelectedValue) + ""
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Using (connection)

            Dim Command As SqlCommand = New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
            Command.Connection.Close()
        End Using
        Dim strMessage As String = "Account Updated successfully!"
        Utilities.MessageBox(Me, strMessage)
    End Sub

    Protected Sub EnableControls(ByVal enable As Boolean)

        'me.txtAddress.Enabled = enable
        Me.txtConfirmPassword.Enabled = enable
        Me.txtCreditCard.Enabled = enable
        Me.txtDateOfBirth.Enabled = enable
        Me.txtEmail.Enabled = enable
        Me.txtFirstName.Enabled = enable
        Me.txtLastName.Enabled = enable
        Me.txtPassword.Enabled = enable
        'me.txtUsername.Enabled = enable
        Me.btnSave.Enabled = enable
        Me.ddlAccessLevel.Enabled = enable

    End Sub

    Protected Sub LoadUsernames()

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
        Dim queryString As String = "select Username from Users" + ";"

        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Using (connection)

            Dim Command As SqlCommand = New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Dim dataReader As SqlDataReader = Command.ExecuteReader()
            Me.ddlUsers.Items.Clear()
            While (dataReader.Read())
                Me.ddlUsers.Items.Add(dataReader("Username").ToString())
            End While
            dataReader.Close()
            Command.Connection.Close()
        End Using

    End Sub

End Class
