'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/24/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.Security

'MS AnitXSS Libraries
Imports Microsoft.Security.Application
Imports Microsoft.Security.Application.SecurityRuntimeEngine

Partial Class SecureProduct
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Check if the user is authenticated
        'Check if session variable Username exists
        If Not (Me.Page.Session("Username") Is Nothing) Then

            'me.lblUsername.Text = "Welcome, " + Server.HtmlEncode(me.Page.Session("Username").ToString()) + " | "
            Me.lblUsername.Text = "Welcome, " + AntiXss.HtmlEncode(Me.Page.Session("Username").ToString(), System.Drawing.KnownColor.Red) + " | "
            Me.lblUsername.Visible = True
            Me.btnLogout.Visible = True
        Else

            Me.lblUsername.Visible = False
            Me.btnLogout.Visible = False
            If (Me.Page.IsPostBack = False) Then
                If Not (Request.QueryString("product") Is Nothing) Then
                    'me.txtProduct.Text = Server.HtmlEncode(Request.QueryString("product").ToString())
                    Me.txtProduct.Text = AntiXss.HtmlEncode(Request.QueryString("product").ToString())
                    Me.SearchProduct(Me.txtProduct.Text)
                Else
                    Me.ShowAllProducts()
                End If
            End If
        End If
    End Sub

    Protected Sub SearchProduct(ByVal product As String)

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
        'string queryString = "select * from Products where Name like " + Utilities.EscapeSingleQuote("%" + product + "%") + ""

        'me.lblProductResult.Visible = false
        'me.lblProductResult.Text = "Search results for <b>" + Server.HtmlEncode(product) + "</b>"
        Me.lblProductResult.Text = "Search results for <b>" + AntiXss.HtmlEncode(product, System.Drawing.KnownColor.Red) + "</b>"
        Me.literalResults.Text = ""
        Try
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            Using (connection)

                Dim Command As SqlCommand = New SqlCommand("ProductSearch", connection)
                Command.Connection.Open()

                'Tell the command to execute stored procedure
                Command.CommandType = System.Data.CommandType.StoredProcedure

                'Add @Product parameter

                Command.Parameters.Add("@Product", System.Data.SqlDbType.VarChar, 200)
                Command.Parameters("@Product").Value = "%" + Me.txtProduct.Text + "%"

                Dim dataReader As SqlDataReader = Command.ExecuteReader()
                If (dataReader.HasRows) Then
                    Dim data As String = "<table border=""1"" style=""width:100%"">"
                    data += "<tr bgcolor=""#0066CC"" style=""color: #FFFFFF""><td>ID</td><td>Name</td><td>Description</td><td>Price</td></tr>"
                    'Response.Write(table)
                    While (dataReader.Read())
                        data += "<tr>"
                        data += "<td>" + dataReader("ID").ToString() + "</td>"
                        'data += "<td>" + Server.HtmlEncode(dataReader("Name").ToString()) + "</td>"
                        data += "<td>" + AntiXss.HtmlEncode(dataReader("Name").ToString()) + "</td>"

                        'data += "<td>" + Server.HtmlEncode(dataReader("Description").ToString()) + "</td>"
                        data += "<td>" + AntiXss.HtmlEncode(dataReader("Description").ToString()) + "</td>"
                        data += "<td>" + dataReader("Price").ToString() + "</td></tr>"
                        'Response.Write(data)
                    End While
                    data += "</table>"
                    Me.literalResults.Text = data
                Else
                    'me.lblProductResult.Text = "No Result found for " + Server.HtmlEncode(product)
                    Me.lblProductResult.Text = "No Result found for " + AntiXss.HtmlEncode(product, System.Drawing.KnownColor.Red)
                    'me.lblProductResult.Visible = true
                End If
            End Using
        Catch ex As Exception

            Response.Write(ex)
            Me.lblProductResult.Text = "No Result found for " + Server.HtmlEncode(product)
            'me.lblProductResult.Visible = true

        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.SearchProduct(Me.txtProduct.Text)
    End Sub


    Protected Sub ShowAllProducts()
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
        Dim queryString As String = "select * from Products"
        'Response.Write(queryString)
        Me.lblProductResult.Text = "Showing all products..."
        Me.literalResults.Text = ""
        Try
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            Using (connection)

                Dim Command As SqlCommand = New SqlCommand(queryString, connection)
                Command.Connection.Open()
                Dim dataReader As SqlDataReader = Command.ExecuteReader()
                If (dataReader.HasRows) Then

                    Dim data As String = "<table border=""1"" style=""width:100%"">"
                    data += "<tr bgcolor=""#0066CC"" style=""color: #FFFFFF""><td>ID</td><td>Name</td><td>Description</td><td>Price</td></tr>"
                    'Response.Write(table)

                    While (dataReader.Read())

                        Data += "<tr>"
                        Data += "<td>" + dataReader("ID").ToString() + "</td>"
                        data += "<td>" + AntiXss.HtmlEncode(dataReader("Name").ToString()) + "</td>"
                        'data += "<td>" + Server.HtmlEncode(dataReader("Description").ToString()) + "</td>"
                        data += "<td>" + AntiXss.HtmlEncode(dataReader("Description").ToString()) + "</td>"
                        data += "<td>" + AntiXss.HtmlEncode(dataReader("Description").ToString()) + "</td>"
                        Data += "<td>" + dataReader("Price").ToString() + "</td></tr>"
                        'Response.Write(data)
                    End While
                    'Response.Write("</table>")
                    data += "</table>"
                    'Label lblProducts = new Label()
                    Me.literalResults.Text = data
                    'Response.Write(data)
                    'me.placeHolderProducts.Controls.Add(lblProducts)
                Else
                    Me.lblProductResult.Text = "No product found!"
                End If
            End Using
        Catch ex As Exception

            Response.Write(ex)
            Me.lblProductResult.Text = "No product found"
        End Try
    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Me.Page.Session.Clear()
        Me.Page.Session.Abandon()
        Response.Redirect("SecureLogin.aspx")
    End Sub
End Class
