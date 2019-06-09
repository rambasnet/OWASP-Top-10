using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Utilities
/// </summary>
public class Utilities
{
	public Utilities()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string SingleQuote(string value)
    {
        return "'" + value + "'";
    }


    public static void MessageBox(System.Web.UI.Page page, string message)
    {
        string strScript = "<script language=JavaScript>";
        strScript += "alert('" + message + "');";
        strScript += "</script>";

        if (page.ClientScript.IsStartupScriptRegistered("messageBox") == false)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "messageBox", strScript);
        }
    }

    public static string EscapeSingleQuote(string value)
    {
        //Replace each single quote sql literal with two single quotes
        return "'" + value.Replace("'", "''") + "'";
    }
}
