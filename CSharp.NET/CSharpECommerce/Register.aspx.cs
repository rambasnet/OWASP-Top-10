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

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnRegister_Click(object sender, EventArgs e)
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
        string queryString = "insert into Users (Username, Password, FirstName, LastName, Email, DOB, CreditCardNum, Address, AccessLevel) values (";
        queryString += Utilities.SingleQuote(this.txtUsername.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtPassword.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtFirstName.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtLastName.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtEmail.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtDateOfBirth.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtCreditCard.Text) + ",";
        queryString += Utilities.SingleQuote(this.txtAddress.Text) + ",";
        queryString += Utilities.SingleQuote("User");
        queryString += ")";

 
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }
        string strMessage = "Account registered successfully!";
        Utilities.MessageBox(this, strMessage);
        Response.Redirect("Login.aspx");

    }
}
