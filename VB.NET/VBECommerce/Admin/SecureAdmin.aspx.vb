'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/25/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data

Partial Class Admin_SecureAdmin
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Assigns an identifier to an individual user in the view-state variable associated with the current page.
        '/*
        ' * Setting the ViewStateUserKey property can help you prevent attacks on your application from malicious users.
        ' * It does this by allowing you to assign an identifier to the view-state variable for individual users so that
        ' * they cannot use the variable to generate an attack. You can set this property to any string value, such as the 
        ' * user's session ID or the user's authenticated name.
        ' */
        If Not (Session("Token") Is Nothing) Then
            'Page.ViewStateUserKey = Session["Token"].ToString()
            Me.lblToken.Text = Session("Token").ToString()
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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


        If (Me.Page.IsPostBack = False) Then

            Me.EnableControls(False)
            Me.LoadUsernames()
            Me.DropDownList1_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Check if two passowrds match
        If (Me.txtPassword.Text <> Me.txtConfirmPassword.Text) Then

            Me.lblPasswordsDontMatch.Visible = True
            Return
        Else
            Me.lblPasswordsDontMatch.Visible = False
        End If

        Dim HashedPassword As String = FormsAuthentication.HashPasswordForStoringInConfigFile(Me.txtPassword.Text, "sha1")
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
        Dim queryString As String = "Update Users set Password=" + Utilities.EscapeSingleQuote(HashedPassword) + ","
        queryString += "FirstName=" + Utilities.EscapeSingleQuote(Me.txtFirstName.Text) + ","
        queryString += "LastName=" + Utilities.EscapeSingleQuote(Me.txtLastName.Text) + ","
        queryString += "Email=" + Utilities.EscapeSingleQuote(Me.txtEmail.Text) + ","
        queryString += "DOB=" + Utilities.EscapeSingleQuote(Me.txtDateOfBirth.Text) + ","
        queryString += "CreditCardNum=" + Utilities.EscapeSingleQuote(Me.txtCreditCard.Text) + ","
        queryString += "Address=" + Utilities.EscapeSingleQuote(Me.txtAddress.Text) + ","
        queryString += "AccessLevel=" + Utilities.EscapeSingleQuote(Me.ddlAccessLevel.SelectedValue)
        queryString += " where Username = " + Utilities.EscapeSingleQuote(ddlUsers.SelectedValue) + ""
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Try
            Using (connection)
                Dim Command As SqlCommand = New SqlCommand(queryString, connection)
                Command.Connection.Open()
                Command.ExecuteNonQuery()
                Command.Connection.Close()
            End Using
            Dim strMessage As String = "Account Updated successfully!"
            Utilities.MessageBox(Me, strMessage)
        Catch ex As Exception
            Utilities.MessageBox(Me, "Error:: " + Server.HtmlEncode(ex.ToString()))
        End Try
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Me.EnableControls(True)
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Response.Redirect("DeleteUser.aspx?username=" + Server.UrlEncode(Me.ddlUsers.SelectedItem.ToString()))
    End Sub

    Protected Sub btnSecureDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSecureDelete.Click
        Context.Items("Token") = Me.lblToken.Text
        Server.Transfer("SecureDeleteUser.aspx?username=" + Server.UrlEncode(Me.ddlUsers.SelectedItem.ToString()))
        'Response.Redirect("SecureDeleteUser.aspx?username=" + Server.UrlEncode(this.ddlUsers.SelectedItem.ToString()))
    End Sub

    Protected Sub btnNew_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("../SecureRegister.aspx?returnUrl=Admin/SecureAdmin.aspx")
    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Me.Session.Clear()
        Me.Session.Abandon()
        Response.Redirect("../SecureLogin.aspx?returnUrl=Admin/SecureAdmin.aspx")
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUsers.SelectedIndexChanged
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
        Dim queryString As String = "select * from Users where Username= " + Utilities.EscapeSingleQuote(Me.ddlUsers.SelectedValue)

        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Try
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
                    Me.txtAddress.Text = dataReader("Address").ToString()

                    dataReader.Close()
                    command.Connection.Close()
                End If
            End Using
        Catch ex As Exception
            Utilities.MessageBox(Me, "Error:: " + Server.HtmlEncode(ex.ToString()))
        End Try
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
        Try
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
        Catch ex As Exception
            Utilities.MessageBox(Me, "Error:: " + Server.HtmlEncode(ex.ToString()))
        End Try

    End Sub
End Class
