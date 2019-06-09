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
using System.IO;

public partial class SecureFileUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        // Check if file was uploaded
        if (this.filePath.PostedFile != null)
        {
            //Get a reference to PostedFile object
            HttpPostedFile uploadedFile = this.filePath.PostedFile;

            //Find size of the file 
            int fileSize = uploadedFile.ContentLength;

            if (fileSize > 0)
            {
                //Allocalte a buffer for reading of the file
                byte[] fileBuffer = new byte[fileSize];

                //Read uploaded file from the Stream
                uploadedFile.InputStream.Read(fileBuffer, 0, fileSize);

                //Create a name for the file to store
                //This is important:
                // Do not allow user input to be used for any part of a file or path name
                // Only allow uploads to a path outside of a webroot or to a path that Web
                // can not access it so it won't be executed directly (but only by the App Code)
                // App_Data created by default with asp.net application is a good candidate

                string serverFilePath = Path.GetFileName(uploadedFile.FileName);

                //Write data into a file
                try
                {
                    this.WriteToFile(Server.MapPath("App_Data/" + serverFilePath), ref fileBuffer);
                    //this.WriteToFile("C:\\Temp\\" + serverFilePath, ref fileBuffer);
                    Utilities.MessageBox(this, "File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    //log error message
                    Utilities.MessageBox(this, "Error:: " + ex.ToString());
                }
            }

        }
    }

    private void WriteToFile(string serverPath, ref byte[] buffer)
    {
        //Check if the file exists; overwriting existing file may have consequences
        //If file name exists append a number at the end of the file name
        string newServerPath = serverPath;
        int i = 1;
        while (File.Exists(newServerPath))
        {
            newServerPath = Path.Combine(Path.GetDirectoryName(serverPath), Path.GetFileNameWithoutExtension(serverPath) + i.ToString() + Path.GetExtension(serverPath));
            i += 1;
        }
        
        //Response.Write(newServerPath);
        //Create a file
        FileStream newFile = new FileStream(newServerPath, FileMode.Create);

        //Write data to the file
        newFile.Write(buffer, 0, buffer.Length);

        //Close file
        newFile.Close();
    }
}
