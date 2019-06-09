Imports Microsoft.VisualBasic

Public Module Utilities

    Public Function EscapeSingleQuote(ByVal value As String) As String
        Return "'" + value.Replace("'", "''") + "'"
    End Function

End Module
