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
using System.Web.Security;

public partial class Admin_SecureAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
        
        if (this.Page.IsPostBack == false)
        {
            this.EnableContronls(false);
            this.LoadUsernames();
            this.DropDownList1_SelectedIndexChanged(sender, e);
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //Assigns an identifier to an individual user in the view-state variable associated with the current page.
        /*
         * Setting the ViewStateUserKey property can help you prevent attacks on your application from malicious users.
         * It does this by allowing you to assign an identifier to the view-state variable for individual users so that
         * they cannot use the variable to generate an attack. You can set this property to any string value, such as the 
         * user's session ID or the user's authenticated name.
         */
        if (Session["Token"] != null)
        {
            //Page.ViewStateUserKey = Session["Token"].ToString();
            this.lblToken.Text = Session["Token"].ToString();
        }
        

        /*This doesn't seem to work on my settings
         * 
        if (User.Identity.IsAuthenticated)
        {
            Page.ViewStateUserKey = User.Identity.Name
        }
        */
    }
     
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        this.EnableContronls(true);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeleteUser.aspx?username=" + Server.UrlEncode(this.ddlUsers.SelectedItem.ToString()));
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
        this.ddlAccessLevel.Enabled = enable;

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        string queryString = "select * from Users where Username= " + Utilities.EscapeSingleQuote(this.ddlUsers.SelectedValue)+ ";";
        try
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    this.ddlAccessLevel.SelectedValue = dataReader["AccessLevel"].ToString();
                    this.literalUsername.Text = Server.HtmlEncode(dataReader["Username"].ToString());
                    //this.txtUsername.Text = dataReader["Username"].ToString();
                    this.txtPassword.Text = dataReader["Password"].ToString();
                    this.txtConfirmPassword.Text = dataReader["Password"].ToString();
                    this.txtCreditCard.Text = dataReader["CreditCardNum"].ToString();
                    this.txtDateOfBirth.Text = dataReader["DOB"].ToString();
                    this.txtEmail.Text = dataReader["Email"].ToString();
                    this.txtFirstName.Text = dataReader["FirstName"].ToString();
                    this.txtLastName.Text = dataReader["LastName"].ToString();
                    this.txtAddress.Text = dataReader["Address"].ToString();

                    dataReader.Close();
                    command.Connection.Close();
                }
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

        string HashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtPassword.Text, "sha1");
        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        string queryString = "Update Users set Password=" + Utilities.EscapeSingleQuote(HashedPassword) + ",";
        queryString += "FirstName=" + Utilities.EscapeSingleQuote(this.txtFirstName.Text) + ",";
        queryString += "LastName=" + Utilities.EscapeSingleQuote(this.txtLastName.Text) + ",";
        queryString += "Email=" + Utilities.EscapeSingleQuote(this.txtEmail.Text) + ",";
        queryString += "DOB=" + Utilities.EscapeSingleQuote(this.txtDateOfBirth.Text) + ",";
        queryString += "CreditCardNum=" + Utilities.EscapeSingleQuote(this.txtCreditCard.Text) + ",";
        queryString += "Address=" + Utilities.EscapeSingleQuote(this.txtAddress.Text) + ",";
        queryString += "AccessLevel=" + Utilities.EscapeSingleQuote(this.ddlAccessLevel.SelectedValue);
        queryString += " where Username = " + Utilities.EscapeSingleQuote(ddlUsers.SelectedValue) + ";";
        try
        {
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
        catch (Exception ex)
        {
            Utilities.MessageBox(this, "Error:: " + Server.HtmlEncode(ex.ToString()));
        }
    }

    protected void LoadUsernames()
    {

        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        string queryString = "select Username from Users" + ";";

        try
        {
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
        catch (Exception ex)
        {
            Utilities.MessageBox(this, "Error:: " + Server.HtmlEncode(ex.ToString()));
        }
    }

    protected void btnNew_Click1(object sender, EventArgs e)
    {
        Response.Redirect("../SecureRegister.aspx?returnUrl=Admin/SecureAdmin.aspx");
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        this.Session.Clear();
        this.Session.Abandon();
        Response.Redirect("../SecureLogin.aspx?returnUrl=Admin/SecureAdmin.aspx");
    }

    protected void btnSecureDelete_Click(object sender, EventArgs e)
    {
        Context.Items["Token"] = this.lblToken.Text;
        Server.Transfer("SecureDeleteUser.aspx?username=" + Server.UrlEncode(this.ddlUsers.SelectedItem.ToString()));
        //Response.Redirect("SecureDeleteUser.aspx?username=" + Server.UrlEncode(this.ddlUsers.SelectedItem.ToString()));
    }
}
