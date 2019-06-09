'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/24/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient

Partial Class Product
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Check if the user is authenticated
        ' Check if session variable Username exists
        If Not (Me.Page.Session("Username") Is Nothing) Then
            Me.lblUsername.Text = "Welcome, " + Me.Page.Session("Username") + "  |"
            Me.lblUsername.Visible = True
            Me.btnLogout.Visible = True
        Else
            Me.lblUsername.Visible = False
            Me.btnLogout.Visible = False
        End If

        If Not (Me.Page.IsPostBack) Then
            If Not Request.QueryString("product") Is Nothing Then
                Me.txtProduct.Text = Request.QueryString("product")
                Me.SearchProduct(Me.txtProduct.Text)
            Else
                Me.ShowAllProducts()
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.SearchProduct(Me.txtProduct.Text)
    End Sub

    Protected Sub SearchProduct(ByVal product As String)

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localConnection").ConnectionString
        Dim queryString As String = "select * from Products where Name like " + Utilities.EscapeSingleQuote("%" + product + "%") + ""
        'Response.Write(queryString)
        Me.lblProductResult.Text = "Search results for <b>" + product + "</b>"
        Me.literalResults.Text = ""
        Try
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            Using (connection)
                Dim Command As SqlCommand = New SqlCommand(queryString, connection)
                Command.Connection.Open()
                Dim dataReader As SqlDataReader = Command.ExecuteReader()
                If (dataReader.HasRows) Then

                    Dim data As String = "<table border=""1"" style=""width:100%"" > "
                    data += "<tr bgcolor=""#0066CC"" style=""color: #FFFFFF""><td>ID</td><td>Name</td><td>Description</td><td>Price</td></tr>"

                    While (dataReader.Read())

                        Data += "<tr>"
                        Data += "<td>" + dataReader("ID").ToString() + "</td>"
                        Data += "<td>" + dataReader("Name").ToString() + "</td>"
                        Data += "<td>" + dataReader("Description").ToString() + "</td>"
                        Data += "<td>" + dataReader("Price").ToString() + "</td></tr>"
                        'Response.Write(data)
                    End While

                    data += ("</table>")
                    Me.literalResults.Text = data

                Else
                    Me.lblProductResult.Text = "No Result found for " + product
                    'me.lblProductResult.Visible = true
                End If

            End Using
        Catch ex As Exception
            Response.Write(ex)
            Me.lblProductResult.Text = "No Result found for " + product
            'me.lblProductResult.Visible = true
        End Try
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
                        Data += "<td>" + dataReader("Name").ToString() + "</td>"
                        Data += "<td>" + dataReader("Description").ToString() + "</td>"
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
        Me.Response.Redirect("Login.aspx")
    End Sub
End Class
