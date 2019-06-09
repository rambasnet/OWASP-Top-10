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
using System.Web.Security;


public partial class SecureRegister : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        //Server validation of form data
        string result = this.ValidateData();
        if (result != "successful")
        {
            Utilities.MessageBox(this, result);
            return;
        }
        
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
        
        string  HashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtPassword.Text, "sha1");
        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        string queryString = "insert into Users (Username, Password, FirstName, LastName, Email, DOB, CreditCardNum, Address, AccessLevel) values (";
        queryString += Utilities.SingleQuote(this.txtUsername.Text) + ",";
        queryString += Utilities.SingleQuote(HashedPassword) + ",";
        queryString += Utilities.SingleQuote(this.txtFirstName.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtLastName.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtEmail.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtDateOfBirth.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtCreditCard.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtAddress.Text) + ",";
        queryString += Utilities.SingleQuote("User");
        queryString += ")";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
                string strMessage = "Account registered successfully!";
                Utilities.MessageBox(this, strMessage);
                if (Request.QueryString["returnUrl"] != null)
                {
                    Response.Redirect(Request.QueryString["returnUrl"]);
                }
                else
                {
                    Response.Redirect("SecureLogin.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            //Log the exception
            Utilities.MessageBox(this, "Error:: " + ex.ToString());
        }

    }

    protected string ValidateData()
    {
        string msg = "successful";
        if (this.txtUsername.Text.Length == 0)
        {
            msg =  "Username is required!";
            this.txtUsername.Focus();
        }
        else if (this.txtPassword.Text.Length <= 6)
        {
            msg = "Password must be at least 7 characters long!";
            this.txtPassword.Focus();
        }
        else if (this.txtFirstName.Text.Length == 0)
        {
            this.txtFirstName.Focus();
            msg = "First name is required!";
        }
        else if (this.txtLastName.Text.Length == 0)
        {
            this.txtLastName.Focus();
            msg = "Last name is required!";
        }
        else if (this.txtDateOfBirth.Text.Length < 6)
        {
            this.txtDateOfBirth.Focus();
            msg = "Invalid Date!";
        }
        else if (this.txtCreditCard.Text.Length < 16)
        {
            this.txtCreditCard.Focus();
            msg = "Invalid Credit Card number!";
        }
        else if (this.txtEmail.Text.Length < 6)
        {
            this.txtEmail.Focus();
            msg = "Invalid Email!";
        }
        else if (this.txtAddress.Text.Length == 0)
        {
            this.txtAddress.Focus();
            msg = "Address is required!";
        }
        return msg;

    }
}
