'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/24/2009
' All Rights Reserved.
'

Imports System.IO

Partial Class FileUpload
    Inherits System.Web.UI.Page

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        ' Check if file was uploaded
        If Not (Me.filePath.PostedFile Is Nothing) Then

            'Get a reference to PostedFile object
            Dim uploadedFile As HttpPostedFile = Me.filePath.PostedFile

            'Create a name for the file to store
            Dim serverFilePath As String = ""
            If (Me.txtFileName.Text.Length > 0) Then
                serverFilePath = Me.txtFileName.Text
            Else
                serverFilePath = Server.MapPath(Path.GetFileName(uploadedFile.FileName))
            End If

            uploadedFile.SaveAs(serverFilePath)
            Utilities.MessageBox(Me, "File Uploaded Successfully!")
        End If

    End Sub


End Class
