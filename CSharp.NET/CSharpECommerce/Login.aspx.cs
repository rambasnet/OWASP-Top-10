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

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //Information Leakage, sql injection
        this.LoginUsingDynamicSQL();

        //Insecure Stored Procedure usage, sql injection
        //this.LoginWithStoredProcedure();

    }


    protected void LoginUsingDynamicSQL()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["localAdminConnection"].ConnectionString;
        string queryString = "select * from Users where Username= " + Utilities.SingleQuote(this.txtUsername.Text);

        this.lblErrorMessage.Visible = false;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                if (dataReader["Password"].ToString() == this.txtPassword.Text)
                {
                    string AccessLevel = dataReader["AccessLevel"].ToString();

                    // Session variables to track authentication
                    this.Session.Add("Username", dataReader["Username"].ToString());
                    this.Session.Add("AccessLevel", AccessLevel);

                    //Also add cookies which can be used for authentication
                    this.Response.Cookies["Username"].Value = dataReader["Username"].ToString();
                    this.Response.Cookies["Username"].Expires = DateTime.Now.AddDays(365);
                    this.Response.Cookies["LastVisit"].Value = DateTime.Now.ToString();
                    this.Response.Cookies["LastVisit"].Expires = DateTime.Now.AddDays(365);
                    this.Response.Cookies["Password"].Value = txtPassword.Text;
                    this.Response.Cookies["Password"].Expires = DateTime.Now.AddDays(365);
                    //Limit cookie domain scope
                    this.Response.Cookies["domain"].Value = DateTime.Now.ToString();
                    this.Response.Cookies["domain"].Expires = DateTime.Now.AddDays(365);
                    Response.Cookies["domain"].Domain = ".domain.com";
                    dataReader.Close();
                    if (AccessLevel == "Admin")
                    {
                        Response.Redirect("Admin/Admin.aspx");
                    }
                    else
                    {
                        Response.Redirect("Account.aspx?username=" + this.txtUsername.Text);
                    }
                }
                else
                {
                    this.lblErrorMessage.Text = "Password doesn't match for username: " + dataReader["Username"].ToString();
                    this.lblErrorMessage.Visible = true;
                }
            }
            else
            {
                this.lblErrorMessage.Text = "Username " + this.txtUsername.Text + " doesn't exist!";
                this.lblErrorMessage.Visible = true;
            }
        }
    }

    //Use of stored procedure
    protected void LoginWithStoredProcedure()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["localAdminConnection"].ConnectionString;

        string queryString = "select * from Users where Username= " + Utilities.SingleQuote(this.txtUsername.Text) + " and Password = " + Utilities.SingleQuote(this.txtPassword.Text) + ";";
        //Response.Write(queryString);
        this.lblErrorMessage.Visible = false;
        //return;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand("ExecuteStoredProcedure", connection);
            //Tell the command to execute stored procedure
            command.CommandType = CommandType.StoredProcedure;

            command.Connection.Open();
            command.Parameters.Add("@sql", SqlDbType.NVarChar, 4000);
            command.Parameters["@sql"].Value = queryString;

            //command.Parameters.Add("@Password", SqlDbType.VarChar, 256);
            //command.Parameters["@Password"].Value = this.txtPassword.Text;

            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                string AccessLevel = dataReader["AccessLevel"].ToString();

                this.Session.Add("Username", dataReader["Username"].ToString());
                this.Session.Add("AccessLevel", AccessLevel);
                dataReader.Close();
                if (AccessLevel == "Admin")
                {
                    Response.Redirect("Admin/Admin.aspx");
                }
                else
                {
                    Response.Redirect("Account.aspx?username=" + this.txtUsername.Text);
                }
            }
            else
            {
                this.lblErrorMessage.Text = "Invalid Username or Password!";
                this.lblErrorMessage.Visible = true;
            }
        }
    }
}
