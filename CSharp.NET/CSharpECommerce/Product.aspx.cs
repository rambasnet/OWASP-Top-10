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

public partial class Product : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Check if the user is authenticated
        //Check if session variable Username exists
        if (this.Page.Session["Username"] != null)
        {
            this.lblUsername.Text = "Welcome, " + this.Page.Session["Username"] + "  |";
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
                this.txtProduct.Text = Request.QueryString["product"];
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
        string queryString = "select * from Products where Name like " + Utilities.EscapeSingleQuote("%" + product + "%") + ";";
        //Response.Write(queryString);
        this.lblProductResult.Text = "Search results for <b>" + product + "</b>";
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
    
                    while (dataReader.Read())
                    {
                        data += "<tr>";
                        data += "<td>" + dataReader["ID"].ToString() + "</td>";
                        data += "<td>" + dataReader["Name"].ToString() + "</td>";
                        data += "<td>" + dataReader["Description"].ToString() + "</td>";
                        data += "<td>" + dataReader["Price"].ToString() + "</td></tr>";
                        //Response.Write(data);

                    }
                    data += ("</table>");
                    this.literalResults.Text = data;
                }
                else
                {
                    this.lblProductResult.Text = "No Result found for " + product;
                    //this.lblProductResult.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
            this.lblProductResult.Text = "No Result found for " + product;
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
        Response.Redirect("Login.aspx");
    }

    protected void ShowAllProducts()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        string queryString = "select * from Products;";
        //Response.Write(queryString);
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
                        data += "<td>" + dataReader["Name"].ToString() + "</td>";
                        data += "<td>" + dataReader["Description"].ToString() + "</td>";
                        data += "<td>" + dataReader["Price"].ToString() + "</td></tr>";
                        //Response.Write(data);
                    }
                    //Response.Write("</table>");
                    data += "</table>";
                    //Label lblProducts = new Label();
                    this.literalResults.Text = data;
                    //Response.Write(data);
                    //this.placeHolderProducts.Controls.Add(lblProducts);
                }
                else
                {
                    this.lblProductResult.Text = "No product found!";
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
            this.lblProductResult.Text = "No product found;";
        }
    }
}
