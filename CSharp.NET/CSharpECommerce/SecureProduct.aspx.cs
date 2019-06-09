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
using System.Web.Security;

//MS AnitXSS Libraries
using Microsoft.Security.Application;
using Microsoft.Security.Application.SecurityRuntimeEngine;


public partial class SecureProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            //Check if the user is authenticated
            //Check if session variable Username exists
            if (this.Page.Session["Username"] != null)
            {
                //this.lblUsername.Text = "Welcome, " + Server.HtmlEncode(this.Page.Session["Username"].ToString()) + " | ";
                this.lblUsername.Text = "Welcome, " + AntiXss.HtmlEncode(this.Page.Session["Username"].ToString(), System.Drawing.KnownColor.Red) + " | ";
                this.lblUsername.Visible = true;
                this.btnLogout.Visible = true;
            }
            else
            {
                this.lblUsername.Visible = false;
                this.btnLogout.Visible = false;
            }

            if (this.Page.IsPostBack == false)
            {
                if (Request.QueryString["product"] != null)
                {
                    //this.txtProduct.Text = Server.HtmlEncode(Request.QueryString["product"].ToString());
                    this.txtProduct.Text = AntiXss.HtmlEncode(Request.QueryString["product"].ToString());
                    this.SearchProduct(this.txtProduct.Text);
                }
                else
                {
                    this.ShowAllProducts();
                }
            }
        
    }

    protected void SearchProduct(string product)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        //string queryString = "select * from Products where Name like " + Utilities.EscapeSingleQuote("%" + product + "%") + ";";
        
        //this.lblProductResult.Visible = false
        //this.lblProductResult.Text = "Search results for <b>" + Server.HtmlEncode(product) + "</b>";
        this.lblProductResult.Text = "Search results for <b>" + AntiXss.HtmlEncode(product, System.Drawing.KnownColor.Red) + "</b>";
        this.literalResults.Text = "";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ProductSearch", connection);
                command.Connection.Open();

                //Tell the command to execute stored procedure
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Add @Product parameter

                command.Parameters.Add("@Product", System.Data.SqlDbType.VarChar, 200);
                command.Parameters["@Product"].Value = "%" + this.txtProduct.Text + "%";

                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    string data = "<table border=\"1\" style=\"width:100%;\">";
                    data += "<tr bgcolor=\"#0066CC\" style=\"color: #FFFFFF\"><td>ID</td><td>Name</td><td>Description</td><td>Price</td></tr>";
                    //Response.Write(table);

                    while (dataReader.Read())
                    {
                        data += "<tr>";
                        data += "<td>" + dataReader["ID"].ToString() + "</td>";
                        //data += "<td>" + Server.HtmlEncode(dataReader["Name"].ToString()) + "</td>";
                        data += "<td>" + AntiXss.HtmlEncode(dataReader["Name"].ToString()) + "</td>";

                        //data += "<td>" + Server.HtmlEncode(dataReader["Description"].ToString()) + "</td>";
                        data += "<td>" + AntiXss.HtmlEncode(dataReader["Description"].ToString()) + "</td>";
                        data += "<td>" + dataReader["Price"].ToString() + "</td></tr>";
                        //Response.Write(data);
                    }
                    data += "</table>";
                    this.literalResults.Text = data;
                }
                else
                {
                    //this.lblProductResult.Text = "No Result found for " + Server.HtmlEncode(product);
                    this.lblProductResult.Text = "No Result found for " + AntiXss.HtmlEncode(product, System.Drawing.KnownColor.Red);
                    //this.lblProductResult.Visible = true;
                }
            }
        }
        catch(Exception ex)
        {
            Response.Write(ex);
            this.lblProductResult.Text = "No Result found for " + Server.HtmlEncode(product);
            //this.lblProductResult.Visible = true;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.SearchProduct(this.txtProduct.Text);
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        this.Page.Session.Clear();
        this.Page.Session.Abandon();
        Response.Redirect("SecureLogin.aspx");
    }

    protected void ShowAllProducts()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        string queryString = "select * from Products;";
        //Response.Write(queryString);
        //this.lblProductResult.Visible = true;
        this.lblProductResult.Text = "Showing all products...";
        this.literalResults.Text = "";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    string data = "<table border=\"1\" style=\"width:100%;\">";
                    data += "<tr bgcolor=\"#0066CC\" style=\"color: #FFFFFF\"><td>ID</td><td>Name</td><td>Description</td><td>Price</td></tr>";
                    //Response.Write(table);
                    
                    while (dataReader.Read())
                    {
                        data += "<tr>";
                        data += "<td>" + dataReader["ID"].ToString() + "</td>";
                        //data += "<td>" + Server.HtmlEncode(dataReader["Name"].ToString()) + "</td>";
                        data += "<td>" + AntiXss.HtmlEncode(dataReader["Name"].ToString()) + "</td>";

                        //data += "<td>" + Server.HtmlEncode(dataReader["Description"].ToString()) + "</td>";
                        data += "<td>" + AntiXss.HtmlEncode(dataReader["Description"].ToString()) + "</td>";
                        data += "<td>" + dataReader["Price"].ToString() + "</td></tr>";
                        //Response.Write(data);
                    }
                    //Response.Write("</table>");
                    data += "</table>";
                    //Response.Write(data);

                    this.literalResults.Text = data;
                }
                else
                {
                    this.lblProductResult.Text = "No product found!";

                }
                dataReader.Close();
                command.Connection.Close();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
            this.lblProductResult.Text = "No product found;";
        }
    }
}
