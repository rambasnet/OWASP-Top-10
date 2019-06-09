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

public partial class Admin_SecureDeleteUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        /*
        if (Page.IsValid == false)
        {
            Response.Write("invalid page!");
        }
         */


        /*
        //Check the user authentication...this should safeguard against CSRF/XSRF/one-click attack, session riding
        if (this.Session["Token"] == null || Context.Items["Token"] == null)
        {
            //Log this activity as a possible attack
            Response.Redirect("../SecureLogin.aspx");
            return;
        }
         * */
        

        //Requiring authentication in GET and POST parameters, not only cookies
        //reverify the authorization of action from the legitimate user by comparing the tokens
        /*
        if (Context.Items["Token"].ToString() != this.Session["Token"].ToString())
        {
            // Some must be trying for CSRS attackt
            // Log this activity if you wish
            Response.Redirect("../SecureLogin.aspx");
        }
         * */
        
        //Check if the user is authenticated to access the page

        //Check if session variable Username exists
        
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
            
            else
            {
                if (this.Page.Session["AccessLevel"].ToString().ToLower() != "admin")
                {
                    Response.Redirect("../SecureLogin.aspx");
                }
                // has admin privilege otherwise
            }
        }
        

        if (Request.Params["username"] == null)
        {
            Response.Redirect("../SecureLogin.aspx");
        }
        else
        {
            string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
            string queryString = "DELETE Users WHERE Username= @Username;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();
                    command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 25);
                    command.Parameters["@Username"].Value = Request.Params["username"];
                    command.ExecuteNonQuery();
                }
                string strMessage = "Account Deleted successfully!";
                Utilities.MessageBox(this, strMessage);
                //Response.Redirect("SecureAdmin.aspx");
            }
            catch (Exception ex)
            {
                //log the exception
                Utilities.MessageBox(this, "Error: " + Server.HtmlEncode(ex.ToString()));
            }
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ViewStateUserKey = Session.SessionID;
    }
}
