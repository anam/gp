using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

/// <summary>
/// Summary description for getFileName
/// </summary>
public class getFileName
{
    private string _fileName;

    public string FileName
    {
        get { return _fileName; }
        set { _fileName = value; }
    }
	
    public  getFileName(string fileName, string path)
    {   
        System.IO.FileInfo file;
        file = new FileInfo(path + "//" + fileName);
        if (file.Exists)
        {
            string tmpfileName = fileName.Substring(0, fileName.LastIndexOf("."));
            string ftype = fileName.Substring(fileName.LastIndexOf(".") + 1, fileName.Length - fileName.LastIndexOf(".") - 1);

            int i;
            for (i = 1; ; i++)
            {
                file = new System.IO.FileInfo(path + "//" + tmpfileName + i.ToString() + "." + ftype);
                if (file.Exists)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
            FileName = tmpfileName + i.ToString() + "." + ftype;
        }
        else
            FileName = fileName;
    }
}
