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
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

public partial class SecureLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        this.SecureLoginWithSanitizedInput();

        //this.SecureLoginWithParameterizedStoredProcedure();
        //this.SecureLoginWithParameterizedDynamicSQL();
    }

    protected void SecureLoginWithSanitizedInput()
    {
        if (this.txtUsername.Text.Length == 0)
        {
            Utilities.MessageBox(this, "Username is required!");
            return;
        }
        else if (this.txtPassword.Text.Length == 0)
        {
            Utilities.MessageBox(this, "Password is required!");
            return;
        }
        
        //stor hashed password
        string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtPassword.Text, "sha1");
        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        string queryString = "select * from Users where Username= " + Utilities.EscapeSingleQuote(this.txtUsername.Text) + " and Password = " + Utilities.EscapeSingleQuote(hashedPassword) + ";";

        this.lblErrorMessage.Visible = false;
        try
        {
            //this statement automatically closes the connection when it exists
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    /* Add a globally unique identifier (guid) as a session id token
                     * A GUID is a Globally Unique ID, a 128-bit number that can be quickly 
                     * generated with one method call, producing a hexidecimal string that 
                     * is very difficult to guess, ex. 9c7aa4a6-c1f4-424c-a4be-f7ea0cb4744b
                    */
                    this.Session.Add("Token", System.Guid.NewGuid().ToString());

                    string AccessLevel = dataReader["AccessLevel"].ToString();

                    this.Session.Add("Username", dataReader["Username"].ToString());
                    this.Session.Add("AccessLevel", AccessLevel);
                    dataReader.Close();
                    if (AccessLevel.ToLower() == "admin")
                    {
                        Response.Redirect("Admin/SecureAdmin.aspx");
                    }
                    else
                    {
                        Response.Redirect("SecureAccount.aspx?username=" + Server.UrlEncode(this.txtUsername.Text));
                    }
                }
                else
                {
                    this.lblErrorMessage.Text = "Invalid Username or Password!";
                    this.lblErrorMessage.Visible = true;
                }
            }
        }
        catch (Exception)
        {
            //Internally log error message with time, client ip, and url
            this.lblErrorMessage.Text = "Invalid Username or Password!";
            this.lblErrorMessage.Visible = true;
        }
    }

    //User parameters with stored procedure
    protected void SecureLoginWithParameterizedStoredProcedure()
    {
        if (this.txtUsername.Text.Length == 0)
        {
            Utilities.MessageBox(this, "Username is required!");
            return;
        }
        else if (this.txtPassword.Text.Length == 0)
        {
            Utilities.MessageBox(this, "Password is required!");
            return;
        }

        string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtPassword.Text, "sha1");
        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;

        this.lblErrorMessage.Visible = false;
        try
        {
            //automatically closes the connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("LoginStoredProcedure", connection);
                //Tell the command to execute stored procedure
                command.CommandType = CommandType.StoredProcedure;

                command.Connection.Open();
                command.Parameters.Add("@Username", SqlDbType.VarChar, 50);
                command.Parameters["@Username"].Value = this.txtUsername.Text;

                command.Parameters.Add("@Password", SqlDbType.VarChar, 256);
                command.Parameters["@Password"].Value = hashedPassword;

                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    /* Add a globally unique identifier (guid) as a session id token
                     * A GUID is a Globally Unique ID, a 128-bit number that can be quickly 
                     * generated with one method call, producing a hexidecimal string that 
                     * is very difficult to guess, ex. 9c7aa4a6-c1f4-424c-a4be-f7ea0cb4744b
                    */
                    string guid = System.Guid.NewGuid().ToString();
                    this.Session.Add("Token", guid);

                    string AccessLevel = dataReader["AccessLevel"].ToString();

                    this.Session.Add("Username", dataReader["Username"].ToString());
                    this.Session.Add("AccessLevel", AccessLevel);
                    dataReader.Close();
                    if (AccessLevel == "Admin")
                    {
                        Response.Redirect("Admin/SecureAdmin.aspx");
                    }
                    else
                    {
                        Response.Redirect("SecureAccount.aspx?username=" + Server.UrlEncode(this.txtUsername.Text));
                    }
                }
                else
                {
                    this.lblErrorMessage.Text = "Invalid Username or Password!";
                    this.lblErrorMessage.Visible = true;
                }
            }
        }
        catch (Exception)
        {
            //Internally log error message with time, client ip, and url
            this.lblErrorMessage.Text = "Invalid Username or Password!";
            this.lblErrorMessage.Visible = true;
        }
    }

    //User parameters with dynamic sql
    protected void SecureLoginWithParameterizedDynamicSQL()
    {
        if (this.txtUsername.Text.Length == 0)
        {
            Utilities.MessageBox(this, "Username is required!");
            return;
        }
        else if (this.txtPassword.Text.Length == 0)
        {
            Utilities.MessageBox(this, "Password is required!");
            return;
        }

        string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtPassword.Text, "sha1");
        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        string queryString = "select * from Users where Username= @Username and Password = @Password;";
        this.lblErrorMessage.Visible = false;
        try
        {
            //automatically closes the connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                                
                command.Connection.Open();
                command.Parameters.Add("@Username", SqlDbType.VarChar, 25);
                command.Parameters["@Username"].Value = this.txtUsername.Text;

                command.Parameters.Add("@Password", SqlDbType.VarChar, 256);
                command.Parameters["@Password"].Value = hashedPassword;

                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    /* Add a globally unique identifier (guid) as a session id token
                     * A GUID is a Globally Unique ID, a 128-bit number that can be quickly 
                     * generated with one method call, producing a hexidecimal string that 
                     * is very difficult to guess, ex. 9c7aa4a6-c1f4-424c-a4be-f7ea0cb4744b
                    */
                    this.Session.Add("Token", System.Guid.NewGuid().ToString());

                    string AccessLevel = dataReader["AccessLevel"].ToString();
                    //Add a globally unique identifier to thawrt xsrf
                    this.Session.Add("Token", System.Guid.NewGuid().ToString());
                    this.Session.Add("Username", dataReader["Username"].ToString());
                    this.Session.Add("AccessLevel", AccessLevel);
                    dataReader.Close();
                    if (AccessLevel == "Admin")
                    {
                        Response.Redirect("Admin/SecureAdmin.aspx");
                    }
                    else
                    {
                        Response.Redirect("SecureAccount.aspx?username="+Server.UrlEncode(this.txtUsername.Text));
                    }
                }
                else
                {
                    this.lblErrorMessage.Text = "Invalid Username or Password!";
                    this.lblErrorMessage.Visible = true;
                }
            }
        }
        catch (Exception)
        {
            //Internally log error message with time, client ip, and url
            this.lblErrorMessage.Text = "Invalid Username or Password!";
            this.lblErrorMessage.Visible = true;
        }
    }
}
