/*
 * Author: Ram Basnet (rambasnet@gmail.com)
 * 4/28/2009
 * All Rights Reserved.
 */

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Account : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Check if the user is authenticated to access the page
        //Check if session variable Username exists
        if (this.Page.Session["Username"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        //Check for username in QueryString
        if (Request.QueryString["username"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (this.Page.IsPostBack == false)
        {
            this.EnableContronls(false);
            this.DisplayUserAccount(Request.QueryString["username"].ToString());
        }
        
    }


    protected void DisplayUserAccount(string username)
    {
        this.lblGreeting.Text = "Hello, " + username;
        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        string queryString = "select * from Users where Username= " + Utilities.EscapeSingleQuote(username) + ";";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    this.literalUsername.Text = Server.HtmlEncode(dataReader["Username"].ToString());
                    this.txtPassword.Text = dataReader["Password"].ToString();
                    this.txtConfirmPassword.Text = dataReader["Password"].ToString();
                    this.txtCreditCard.Text = dataReader["CreditCardNum"].ToString();
                    this.txtDateOfBirth.Text = dataReader["DOB"].ToString();
                    this.txtEmail.Text = dataReader["Email"].ToString();
                    this.txtFirstName.Text = dataReader["FirstName"].ToString();
                    this.txtLastName.Text = dataReader["LastName"].ToString();
                    this.txtAddress.Text = dataReader["Address"].ToString();

                }
                dataReader.Close();
                command.Connection.Close();
            }
        }
        catch (Exception ex)
        {
            Utilities.MessageBox(this, "Error: " + Server.HtmlEncode(ex.ToString()));
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Check if two passowrds match
        if (this.txtPassword.Text != this.txtConfirmPassword.Text)
        {
            this.lblPasswordsDontMatch.Visible = true;
            return;
        }
        else
        {
            this.lblPasswordsDontMatch.Visible = false;
        }

        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        string queryString = "Update Users set Password=" + Utilities.EscapeSingleQuote(this.txtPassword.Text) + ",";
        queryString += "FirstName=" + Utilities.EscapeSingleQuote(this.txtFirstName.Text) + ",";
        queryString += "LastName=" + Utilities.EscapeSingleQuote(this.txtLastName.Text) + ",";
        queryString += "Email=" + Utilities.EscapeSingleQuote(this.txtEmail.Text) + ",";
        queryString += "DOB=" + Utilities.EscapeSingleQuote(this.txtDateOfBirth.Text) + ",";
        queryString += "CreditCardNum=" + Utilities.EscapeSingleQuote(this.txtCreditCard.Text) + ",";
        queryString += "Address=" + Utilities.EscapeSingleQuote(this.txtAddress.Text);
        queryString += " where Username = " + Utilities.EscapeSingleQuote(this.literalUsername.Text) + ";";
        try
        {
            //Response.Write(queryString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
                this.DisplayUserAccount(Request.QueryString["username"].ToString());
                this.EnableContronls(false);
            }
            string strMessage = "Account Updated successfully!";
            Utilities.MessageBox(this, strMessage);
        }
        catch (Exception ex)
        {
            Utilities.MessageBox(this, "Error:: " + Server.HtmlEncode(ex.ToString()));
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        //this.Session.Clear();
        //this.Session.Abandon();
        // Simply send the user to Login.aspx without clearing and abandoning session and cookies
        Response.Redirect("Login.aspx");
    }

    protected void EnableContronls(bool enable)
    {
        this.txtAddress.Enabled = enable;
        this.txtConfirmPassword.Enabled = enable;
        this.txtCreditCard.Enabled = enable;
        this.txtDateOfBirth.Enabled = enable;
        this.txtEmail.Enabled = enable;
        this.txtFirstName.Enabled = enable;
        this.txtLastName.Enabled = enable;
        this.txtPassword.Enabled = enable;
        this.btnSave.Enabled = enable;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Product.aspx");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        this.EnableContronls(true);
    }
 
}
