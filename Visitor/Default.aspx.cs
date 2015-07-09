using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //createDatabaseBackup();
        }
    }

    public void createDatabaseBackup()
    {
        try
        {
            DeleteFileFromFolder("GentleParkHO_Old.rar");
        }
        catch (Exception ex)
        { }

        string sql = @"
            DECLARE @DBName VARCHAR(50)
	            DECLARE @path VARCHAR(256) 
	            DECLARE @file_Name VARCHAR(256) -- filename for backup 
	
	            SET @path = '" + Server.MapPath("..\\") + @"'
	
	            set @DBName='GentleParkHO_Old'
			
	            SET @file_Name = @path + @DBName + '.rar'
	            BACKUP DATABASE @DBName TO DISK = @file_Name 
            ";

        CommonManager.SQLExec(sql);

        Response.Redirect("../GentleParkHO_Old.rar");
    }

    public void DeleteFileFromFolder(string StrFilename)
    {

        try
        {
            string strPhysicalFolder = Server.MapPath("..\\");
            string strFileFullPath = strPhysicalFolder + StrFilename;

            if (System.IO.File.Exists(strFileFullPath))
            {
                System.IO.File.Delete(strFileFullPath);
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnDatabaseBackup_Click(object sender, EventArgs e)
    {
        createDatabaseBackup();
    }
}