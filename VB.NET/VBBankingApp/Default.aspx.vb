'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/25/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Request.Cookies("ClientID") Is Nothing) Then
            Me.lblUsername.Text = "You are logged in as: " + Request.Cookies("ClientID").Value
        End If
    End Sub
End Class
