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

public class ADLStatus
{
    public ADLStatus()
    {
    }

    public ADLStatus
        (
        int aDLStatusID, 
        string aDLStatusName
        )
    {
        this.ADLStatusID = aDLStatusID;
        this.ADLStatusName = aDLStatusName;
    }


    private int _aDLStatusID;
    public int ADLStatusID
    {
        get { return _aDLStatusID; }
        set { _aDLStatusID = value; }
    }

    private string _aDLStatusName;
    public string ADLStatusName
    {
        get { return _aDLStatusName; }
        set { _aDLStatusName = value; }
    }
}
