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
        if (this.Page.Session["ClientID"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        //Check for username in QueryString
        if (Request.QueryString["ClientID"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (this.Page.IsPostBack == false)
        {
            this.EnableContronls(false);
            this.DisplayUserProfile(Request.QueryString["ClientID"].ToString());
            this.LoadAccounts(Request.QueryString["ClientID"].ToString());
            this.ddlAccounts_SelectedIndexChanged(sender, e);
        }
        
    }


    protected void DisplayUserProfile(string ClientID)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["localAdminConnection"].ConnectionString;
        string queryString = "select * from Clients where ID= " + ClientID + ";";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    this.lblGreeting.Text = "Hello, " + dataReader["Username"].ToString();
                    this.literalUsername.Text = dataReader["Username"].ToString();
                    this.txtPassword.Text = dataReader["Password"].ToString();
                    this.txtConfirmPassword.Text = dataReader["Password"].ToString();
                    this.txtSSN.Text = dataReader["SSN"].ToString();
                    this.txtDateOfBirth.Text = dataReader["DOB"].ToString();
                    this.txtEmail.Text = dataReader["Email"].ToString();
                    this.txtFirstName.Text = dataReader["FirstName"].ToString();
                    this.txtLastName.Text = dataReader["LastName"].ToString();
                    this.txtAddress.Text = dataReader["Address"].ToString();

                    dataReader.Close();
                }


            }
        }
        catch (Exception ex)
        {
            Utilities.MessageBox(this, "Error: " + ex.ToString());
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

        string connectionString = ConfigurationManager.ConnectionStrings["localAdminConnection"].ConnectionString;
        string queryString = "Update Clients set Password=" + Utilities.SingleQuote(this.txtPassword.Text) + ",";
        queryString += "FirstName=" + Utilities.SingleQuote(this.txtFirstName.Text) + ",";
        queryString += "LastName=" + Utilities.SingleQuote(this.txtLastName.Text) + ",";
        queryString += "Email=" + Utilities.SingleQuote(this.txtEmail.Text) + ",";
        queryString += "DOB=" + Utilities.SingleQuote(this.txtDateOfBirth.Text) + ",";
        queryString += "SSN=" + Utilities.SingleQuote(this.txtSSN.Text) + ",";
        queryString += "Address=" + Utilities.SingleQuote(this.txtAddress.Text);
        queryString += " where ID = " + Request.QueryString["ClientID"].ToString() + ";";
        
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
            this.DisplayUserProfile(Request.QueryString["ClientID"].ToString());
            LoadAccounts(Request.QueryString["ClientID"]);
            this.EnableContronls(false);
        }
        string strMessage = "Account Updated successfully!";
        Utilities.MessageBox(this, strMessage);

    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

    protected void EnableContronls(bool enable)
    {
        this.txtAddress.Enabled = enable;
        this.txtConfirmPassword.Enabled = enable;
        this.txtSSN.Enabled = enable;
        this.txtDateOfBirth.Enabled = enable;
        this.txtEmail.Enabled = enable;
        this.txtFirstName.Enabled = enable;
        this.txtLastName.Enabled = enable;
        this.txtPassword.Enabled = enable;
        this.btnSave.Enabled = enable;
    }


    protected void btnEdit_Click(object sender, EventArgs e)
    {
        this.EnableContronls(true);
    }

    protected void LoadAccounts(string clientID)
    {

        string connectionString = ConfigurationManager.ConnectionStrings["localAdminConnection"].ConnectionString;
        string queryString = "select AccountNum from Accounts where ClientID=" + clientID + ";";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            this.ddlAccounts.Items.Clear();
            while (dataReader.Read())
            {
                this.ddlAccounts.Items.Add(dataReader["AccountNum"].ToString());
            }
            dataReader.Close();
        }
        
    }


    protected void ddlAccounts_SelectedIndexChanged(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["localAdminConnection"].ConnectionString;
        string queryString = "select AccountType, Balance from Accounts where AccountNum= " + Utilities.SingleQuote(this.ddlAccounts.SelectedValue) + ";";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                this.lblAccountType.Text = dataReader["AccountType"].ToString();
                this.lblBalance.Text = dataReader["Balance"].ToString();
                
                dataReader.Close();
            }
        }
        
    }
}
