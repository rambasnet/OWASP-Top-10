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
using System.Data.SqlClient;

public partial class Admin_DeleteUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //Check if the user is authenticated to access the page
        //Check if session variable ClientID exists
        if (this.Page.Session["ClientID"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        // Now check if the user has Admin privilege
        else
        {
            if (this.Page.Session["AccessLevel"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        if (Request.Params["ClientID"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            if (this.Page.IsPostBack == false)
            {
                this.LoadFromAccounts(System.Convert.ToInt32(Request.Params["ClientID"]));
            }
        }
        
    }
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        string FromAccount = this.ddlFromAccount.SelectedItem.ToString().Substring(0, this.ddlFromAccount.SelectedItem.ToString().IndexOf("("));
        Response.Redirect("DoTransfer.aspx?From=" + FromAccount.Trim() + "&To=" + this.txtToAccount.Text + "&Name=" + this.txtName.Text + "&Amount=" + this.txtAmount.Text );
    }

    protected void LoadFromAccounts(long clientID)
    {

        string connectionString = ConfigurationManager.ConnectionStrings["localAdminConnection"].ConnectionString;
        string queryString = "select AccountNum, Balance from Accounts where ClientID=" + clientID.ToString() + ";";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                this.ddlFromAccount.Items.Clear();
                while (dataReader.Read())
                {
                    this.ddlFromAccount.Items.Add(dataReader["AccountNum"].ToString() + " (" + dataReader["Balance"].ToString() + ")");
                }
                dataReader.Close();
            }
        }
        catch (Exception ex)
        {
            Utilities.MessageBox(this, "Error:: " + ex.ToString());
        }
    }
}
