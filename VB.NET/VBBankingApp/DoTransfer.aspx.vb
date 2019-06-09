'
' Author: Ram Basnet (rambasnet@gmail.com)
' 8/26/2009
' All Rights Reserved.
'

Imports System.Data.SqlClient
Imports System.Data

Partial Class DoTransfer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FromAccount As String = Request.Params("From").ToString()
        Dim ToAccount As String = Request.Params("To").ToString()
        Dim Amount As String = Request.Params("Amount").ToString()

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("localAdminConnection").ConnectionString
        Dim queryStringSubtract As String = "UPDATE Accounts SET Balance = Balance - " + Amount + " WHERE AccountNum=" + FromAccount + ""

        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Using (connection)

            Dim Command As SqlCommand = New SqlCommand(queryStringSubtract, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
            Command.CommandText = "UPDATE Accounts SET Balance = Balance + " + Amount + " WHERE AccountNum=" + ToAccount + ""
            Command.ExecuteNonQuery()
            Dim queryActivity = "INSERT INTO AccountActivity (AccountNum, Activity, Amount, Timestamp) "
            queryActivity += " values (" + FromAccount + "," + Utilities.SingleQuote("Transfer to Acct #: " + ToAccount) + "," + (-float.Parse(Amount)).ToString() + "," + Utilities.SingleQuote(DateTime.Now.ToString()) + ")"

            Command.CommandText = queryActivity
            Command.ExecuteNonQuery()
            queryActivity = "INSERT INTO AccountActivity (AccountNum, Activity, Amount, Timestamp) "
            queryActivity += " values (" + ToAccount + "," + Utilities.SingleQuote("Transfer From Acct #: " + FromAccount) + "," + Amount + "," + Utilities.SingleQuote(DateTime.Now.ToString()) + ")"

            Command.CommandText = queryActivity
            Command.ExecuteNonQuery()

            Dim strMessage As String = "Money trasferred successfully! <a href=""Transfer.aspx"">Go back to Transfer page</a>."
            Utilities.MessageBox(Me, strMessage)
            'Response.Redirect("Transfer.aspx")
        End Using

    End Sub
End Class
