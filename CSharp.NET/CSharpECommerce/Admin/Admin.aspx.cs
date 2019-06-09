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
            this.LoadUsernames();
            this.DropDownList1_SelectedIndexChanged(sender, e);
        }
    }
 
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        this.EnableContronls(true);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //Delete right here or ask another form to delete it...
        /*
        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        string queryString = "DELETE Users WHERE Username= " + Utilities.SingleQuote(this.literalUsername.Text) + ";";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            command.ExecuteNonQuery();
        }
        string strMessage = "Account Deleted successfully!";
        Utilities.MessageBox(this, strMessage);
        this.LoadUsernames();
        //this.SqlDataSource1.Select();
        this.DropDownList1_SelectedIndexChanged(sender, e);
        */

        Response.Redirect("DeleteUser1.aspx?username=" + Server.UrlEncode(this.ddlUsers.SelectedItem.ToString()));
    }


    protected void EnableContronls(bool enable)
    {
        //this.txtAddress.Enabled = enable;
        this.txtConfirmPassword.Enabled = enable;
        this.txtCreditCard.Enabled = enable;
        this.txtDateOfBirth.Enabled = enable;
        this.txtEmail.Enabled = enable;
        this.txtFirstName.Enabled = enable;
        this.txtLastName.Enabled = enable;
        this.txtPassword.Enabled = enable;
        //this.txtUsername.Enabled = enable;
        this.btnSave.Enabled = enable;
        this.ddlAccessLevel.Enabled = enable;

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        string queryString = "select * from Users where Username= " + Utilities.SingleQuote(this.ddlUsers.SelectedValue)+ ";";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                this.ddlAccessLevel.SelectedValue = dataReader["AccessLevel"].ToString();
                //Response.Write(dataReader["Username"].ToString());
                this.literalUsername.Text = dataReader["Username"].ToString();
                //this.txtUsername.Text = Server.HtmlDecode(dataReader["Username"].ToString());
                this.txtPassword.Text = dataReader["Password"].ToString();
                this.txtConfirmPassword.Text = dataReader["Password"].ToString();
                this.txtCreditCard.Text = dataReader["CreditCardNum"].ToString();
                this.txtDateOfBirth.Text = dataReader["DOB"].ToString();
                this.txtEmail.Text = dataReader["Email"].ToString();
                this.txtFirstName.Text = dataReader["FirstName"].ToString();
                this.txtLastName.Text = dataReader["LastName"].ToString();
                this.litAddress.Text = dataReader["Address"].ToString();

                dataReader.Close();
                command.Connection.Close();
            }
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
        string queryString = "Update Users set Password=" + Utilities.SingleQuote(this.txtPassword.Text) + ",";
        queryString += "FirstName=" + Utilities.SingleQuote(this.txtFirstName.Text) + ",";
        queryString += "LastName=" + Utilities.SingleQuote(this.txtLastName.Text) + ",";
        queryString += "Email=" + Utilities.SingleQuote(this.txtEmail.Text) + ",";
        queryString += "DOB=" + Utilities.SingleQuote(this.txtDateOfBirth.Text) + ",";
        queryString += "CreditCardNum=" + Utilities.SingleQuote(this.txtCreditCard.Text) + ",";
        queryString += "Address=" + Utilities.SingleQuote(this.litAddress.Text) + ",";
        queryString += "AccessLevel=" + Utilities.SingleQuote(this.ddlAccessLevel.SelectedValue);
        queryString += " where Username = " + Utilities.SingleQuote(ddlUsers.SelectedValue) + ";";

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

    protected void LoadUsernames()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        string queryString = "select Username from Users" + ";";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            this.ddlUsers.Items.Clear();
            while (dataReader.Read())
            {
                this.ddlUsers.Items.Add(dataReader["Username"].ToString());
            }
            dataReader.Close();
            command.Connection.Close();
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Register.aspx");
    }
}
