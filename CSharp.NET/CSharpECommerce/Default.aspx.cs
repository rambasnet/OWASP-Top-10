/*
 * Author: Ram Basnet (rambasnet@gmail.com)
 * 4/28/2009
 * All Rights Reserved.
 */

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["Username"] != null)
        {
            this.lblUsername.Text = "Welcome, " + Request.Cookies["Username"].Value;
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //Bad one
        if (this.txtProduct.Text.Length != 0)
        {
            // Make QueryString to access it as GET Method
            Response.Redirect("./Product.aspx?product=" + this.txtProduct.Text);
        }
    }
}
