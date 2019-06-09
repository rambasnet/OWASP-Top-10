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

public partial class Admin_DeleteUser1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //Doesn't check for Admin Privilege
        // Simply deletes the account by reading the username in url and
        // directs the page to SecureLogin.aspx
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
            string queryString = "DELETE Users WHERE Username= " + Utilities.EscapeSingleQuote(Request.Params["username"]) + ";";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                string strMessage = "Account Deleted successfully!";
                Utilities.MessageBox(this, strMessage);
                Response.Redirect("SecureAdmin.aspx");
            }
            catch (Exception ex)
            {
                //log the exception
                Utilities.MessageBox(this, "Error: " + Server.HtmlEncode(ex.ToString()));
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            Utilities.MessageBox(this, "Error:: " + ex.ToString());
        }
    }
}
