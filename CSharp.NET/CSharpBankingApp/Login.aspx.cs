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
        this.LoginUsingDynamicSQL();
    }


    protected void LoginUsingDynamicSQL()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["localAdminConnection"].ConnectionString;
        string queryString = "select * from Clients where Username= " + Utilities.SingleQuote(this.txtUsername.Text);

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
                    this.Session.Add("ClientID", dataReader["ID"].ToString());
                    this.Session.Add("AccessLevel", AccessLevel);

                    //Also add cookies which can be used for authentication
                    this.Response.Cookies["ClientID"].Value = dataReader["ID"].ToString();
                    this.Response.Cookies["ClientID"].Expires = DateTime.Now.AddDays(365);
                    this.Response.Cookies["LastVisit"].Value = DateTime.Now.ToString();
                    this.Response.Cookies["LastVisit"].Expires = DateTime.Now.AddDays(365);
                    this.Response.Cookies["Password"].Value = txtPassword.Text;
                    this.Response.Cookies["Password"].Expires = DateTime.Now.AddDays(365);
                    
                    dataReader.Close();
                    if (AccessLevel.ToLower() == "admin")
                    {
                        Response.Redirect("Admin/Admin.aspx");
                    }
                    else
                    {
                        Response.Redirect("Account.aspx?ClientID=" + this.Session["ClientID"].ToString());
                    }
                }
                else
                {
                    this.lblErrorMessage.Text = "Password doesn't match for username: " + this.txtUsername.Text;
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
}
