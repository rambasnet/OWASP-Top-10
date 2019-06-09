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

public partial class Admin_DeleteUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        // Check if the user is authenticated to access the page
        // Doesn't re-verify authorization of action
        // Check if session variable Username exists
        if (this.Page.Session["Username"] == null)
        {
            Response.Redirect("../SecureLogin.aspx");
        }
        // Now check if the user has Admin privilege
        else
        {
            if (this.Page.Session["AccessLevel"] == null)
            {
                Response.Redirect("../SecureLogin.aspx");
            }
        }

        if (Request.Params["username"] == null)
        {
            Response.Redirect("../SecureLogin.aspx");
        }
        else
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
    }
}
