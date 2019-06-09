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

public partial class FileUpload : System.Web.UI.Page
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
                string serverFilePath = "";
                if (this.txtFileName.Text.Length > 0)
                {
                    serverFilePath = this.txtFileName.Text;
                }
                else
                {
                    serverFilePath = Path.GetFileName(uploadedFile.FileName);
                }

                //Write data into a file
                this.WriteToFile(Server.MapPath(serverFilePath), ref fileBuffer);
                Utilities.MessageBox(this, "File Uploaded Successfully!");
            }
            

        }
    }

    private void WriteToFile(string serverPath, ref byte[] buffer)
    {
        //Create a file
        FileStream newFile = new FileStream(serverPath, FileMode.Create);

        //Write data to the file
        newFile.Write(buffer, 0, buffer.Length);

        //Close file
        newFile.Close();
    }
}
