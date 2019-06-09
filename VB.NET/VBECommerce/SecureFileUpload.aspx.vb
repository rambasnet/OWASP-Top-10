'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/25/2009
' All Rights Reserved.
'

Imports System.IO

Partial Class SecureFileUpload
    Inherits System.Web.UI.Page

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        ' Check if file was uploaded
        If Not (Me.filePath.PostedFile Is Nothing) Then

            'Get a reference to PostedFile object
            Dim uploadedFile As HttpPostedFile = Me.filePath.PostedFile


            'Create a name for the file to store
            'This is important:
            ' Do not allow user input to be used for any part of a file or path name
            ' Only allow uploads to a path outside of a webroot or to a path that Web
            ' can not access it so it won't be executed directly (but only by the App Code)
            ' App_Data created by default with asp.net application is a good candidate

            Dim serverFilePath As String = Server.MapPath("App_Data/" + Path.GetFileName(uploadedFile.FileName))

            Dim i As Integer = 1
            While (File.Exists(serverFilePath))
                serverFilePath = Path.Combine(Path.GetDirectoryName(serverFilePath), Path.GetFileNameWithoutExtension(serverFilePath) + i.ToString() + Path.GetExtension(serverFilePath))
                i += 1
            End While

            'save the file
            uploadedFile.SaveAs(serverFilePath)
            Utilities.MessageBox(Me, "File Uploaded Successfully!")
        End If
    End Sub
End Class
