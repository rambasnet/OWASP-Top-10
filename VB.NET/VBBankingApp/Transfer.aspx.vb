'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/26/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data

Partial Class Transfer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Check if the user is authenticated to access the page
        'Check if session variable ClientID exists
        If (Me.Page.Session("ClientID") Is Nothing) Then
            Response.Redirect("Login.aspx")
            ' Now check if the user has Admin privilege
        Else
            If (Me.Page.Session("AccessLevel") Is Nothing) Then
                Response.Redirect("Login.aspx")
            End If
        End If


        If (Request.Params("ClientID") Is Nothing) Then
            Response.Redirect("Login.aspx")
        Else
            If (Me.Page.IsPostBack = False) Then
                Me.LoadFromAccounts(System.Convert.ToInt32(Request.Params("ClientID")))

            End If
        End If


    End Sub

    Protected Sub btnTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        Dim FromAccount As String = Me.ddlFromAccount.SelectedItem.ToString().Substring(0, Me.ddlFromAccount.SelectedItem.ToString().IndexOf("("))
        Response.Redirect("DoTransfer.aspx?From=" + FromAccount.Trim() + "&To=" + Me.txtToAccount.Text + "&Name=" + Me.txtName.Text + "&Amount=" + Me.txtAmount.Text)
    End Sub

    Protected Sub LoadFromAccounts(ByVal clientID As Long)
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localAdminConnection").ConnectionString
        Dim queryString As String = "select AccountNum, Balance from Accounts where ClientID=" + clientID.ToString() + ""
        Try
            Dim connection As SqlConnection = New SqlConnection(connectionString)
        
            Using (connection)

                Dim Command As SqlCommand = New SqlCommand(queryString, connection)
                Command.Connection.Open()
                Dim dataReader As SqlDataReader = Command.ExecuteReader()
                Me.ddlFromAccount.Items.Clear()
                While (dataReader.Read())
                    Me.ddlFromAccount.Items.Add(dataReader("AccountNum").ToString() + " (" + dataReader("Balance").ToString() + ")")
                End While

                dataReader.Close()
            End Using

        Catch ex As Exception
            Utilities.MessageBox(Me, "Error:: " + ex.ToString())
        End Try

    End Sub


End Class
