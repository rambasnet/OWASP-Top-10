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
using System.Data.SqlClient;
using System.Configuration;

public partial class Admin_Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack == false)
        {
            this.EnableContronls(false);
            this.LoadClientIDs();
            this.ddlClients_SelectedIndexChanged(sender, e);
            this.ddlAccounts_SelectedIndexChanged(sender, e);
        }
    }
 
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        this.EnableContronls(true);
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
        this.ddlAccessLevel.Enabled = enable;

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
        string queryString = "Update Clients set Password=" + Utilities.SingleQuote(this.txtPassword.Text) + ",";
        queryString += "FirstName=" + Utilities.SingleQuote(this.txtFirstName.Text) + ",";
        queryString += "LastName=" + Utilities.SingleQuote(this.txtLastName.Text) + ",";
        queryString += "Email=" + Utilities.SingleQuote(this.txtEmail.Text) + ",";
        queryString += "DOB=" + Utilities.SingleQuote(this.txtDateOfBirth.Text) + ",";
        queryString += "SSN=" + Utilities.SingleQuote(this.txtSSN.Text) + ",";
        queryString += "Address=" + Utilities.SingleQuote(this.txtAddress.Text) + ",";
        queryString += "AccessLevel=" + Utilities.SingleQuote(this.ddlAccessLevel.SelectedValue);
        queryString += " where ID = " + Utilities.SingleQuote(ddlClients.SelectedValue) + ";";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }
        string strMessage = "Account Updated successfully!";
        Utilities.MessageBox(this, strMessage);
    }

    protected void LoadClientIDs()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["localAdminConnection"].ConnectionString;
        string queryString = "select ID from Clients" + ";";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            this.ddlClients.Items.Clear();
            while (dataReader.Read())
            {
                this.ddlClients.Items.Add(dataReader["ID"].ToString());
            }
            dataReader.Close();
        }
    }

    protected void LoadAccounts()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["localAdminConnection"].ConnectionString;
        string queryString = "select AccountNum from Accounts where ClientID=" + this.ddlClients.SelectedItem.ToString() + ";";

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

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    }

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Transfer.aspx");
    }


    protected void ddlClients_SelectedIndexChanged(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["localAdminConnection"].ConnectionString;
        string queryString = "select * from Clients where ID= " + this.ddlClients.SelectedValue.ToString() + ";";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                this.ddlAccessLevel.SelectedValue = dataReader["AccessLevel"].ToString();
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
        
        this.LoadAccounts();
        this.ddlAccounts_SelectedIndexChanged(sender, e);
    }
    protected void ddlAccounts_SelectedIndexChanged(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["localAdminConnection"].ConnectionString;
        string queryString = "select AccountType, Balance from Accounts where AccountNum= " + this.ddlAccounts.SelectedValue.ToString() + ";";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                this.lblAccountType.Text = dataReader["AccountType"].ToString();
                this.lblBalance.Text = dataReader["Balance"].ToString();
                
            }
            dataReader.Close();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}
