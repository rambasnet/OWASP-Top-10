/*
 * Author: Ram Basnet (rambasnet@gmail.com)
 * 4/28/2009
 * All Rights Reserved.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class DoTransfer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string FromAccount = Request.Params["From"].ToString();
        string ToAccount = Request.Params["To"].ToString();
        string Amount = Request.Params["Amount"].ToString();

        string connectionString = ConfigurationManager.ConnectionStrings["localAdminConnection"].ConnectionString;
        string queryStringSubtract = "UPDATE Accounts SET Balance = Balance - " + Amount + " WHERE AccountNum=" + FromAccount + ";";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(queryStringSubtract, connection);
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.CommandText = "UPDATE Accounts SET Balance = Balance + " + Amount + " WHERE AccountNum=" + ToAccount + ";";
            command.ExecuteNonQuery();
            string queryActivity = "INSERT INTO AccountActivity (AccountNum, Activity, Amount, Timestamp) ";
            queryActivity += " values (" + FromAccount + "," + Utilities.SingleQuote("Transfer to Acct #: " + ToAccount) + "," + (-float.Parse(Amount)).ToString() + "," + Utilities.SingleQuote(DateTime.Now.ToString()) + ")";

            command.CommandText = queryActivity;
            command.ExecuteNonQuery();
            queryActivity = "INSERT INTO AccountActivity (AccountNum, Activity, Amount, Timestamp) ";
            queryActivity += " values (" + ToAccount + "," + Utilities.SingleQuote("Transfer From Acct #: " + FromAccount) + "," + Amount + "," + Utilities.SingleQuote(DateTime.Now.ToString()) + ")";

            command.CommandText = queryActivity;
            command.ExecuteNonQuery();
        }

        string strMessage = "Money trasferred successfully!";
        Utilities.MessageBox(this, strMessage);
        Response.Redirect("Transfer.aspx");
        
    }
}
