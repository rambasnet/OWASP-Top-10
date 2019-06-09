'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/24/2009
' All Rights Reserved.
'


Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Request.Cookies("Username") Is Nothing) Then
            Me.lblUsername.Text = "Welcome, " + Request.Cookies("Username").Value
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ' bad one
        If (Me.txtProduct.Text.Length <> 0) Then
            Response.Redirect("./Product.aspx?" + Me.txtProduct.Text)
        End If
    End Sub
End Class
