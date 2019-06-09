Imports Microsoft.VisualBasic

Public Module Utilities
    Public Function SingleQuote(ByVal str As String) As String
        Return "'" + str + "'"

    End Function


    Public Sub MessageBox(ByRef page As System.Web.UI.Page, ByVal message As String)

        Dim strScript As String = "<script language=JavaScript>"
        strScript += "alert('" + message + "');"
        strScript += "</script>"

        If (page.ClientScript.IsStartupScriptRegistered("messageBox") = False) Then
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "messageBox", strScript)
        End If
    End Sub

    Public Function EscapeSingleQuote(ByVal value As String) As String

        'Replace each single quote sql literal with two single quotes specific to MS-SQL
        Return "'" + value.Replace("'", "''") + "'"
    End Function

End Module
