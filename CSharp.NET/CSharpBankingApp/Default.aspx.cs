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
        if (Request.Cookies["ClientID"] != null)
        {
            this.lblUsername.Text = "You are logged in as: " + Request.Cookies["ClientID"].Value;
        }

    }
}
