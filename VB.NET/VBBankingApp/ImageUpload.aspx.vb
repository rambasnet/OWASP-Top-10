'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/26/2009
' All Rights Reserved.
'

Imports System.IO

Partial Class ImageUpload
    Inherits System.Web.UI.Page

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        ' Check if file was uploaded
        If Not (Me.filePath.PostedFile Is Nothing) Then

            'Get a reference to PostedFile object
            Dim uploadedFile As HttpPostedFile = Me.filePath.PostedFile

            'Create a name for the file to store
            Dim serverFilePath As String = Server.MapPath(Path.GetFileName(uploadedFile.FileName))

            uploadedFile.SaveAs(serverFilePath)
            Utilities.MessageBox(Me, "File Uploaded Successfully!")
        End If
    End Sub
End Class
