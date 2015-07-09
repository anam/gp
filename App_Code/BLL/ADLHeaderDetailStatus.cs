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

public class ADLHeaderDetailStatus
{
    public ADLHeaderDetailStatus()
    {
    }

    public ADLHeaderDetailStatus
        (
        int aDLHeaderDetailStatusID, 
        string aDLHeaderDetailStatusName
        )
    {
        this.ADLHeaderDetailStatusID = aDLHeaderDetailStatusID;
        this.ADLHeaderDetailStatusName = aDLHeaderDetailStatusName;
    }


    private int _aDLHeaderDetailStatusID;
    public int ADLHeaderDetailStatusID
    {
        get { return _aDLHeaderDetailStatusID; }
        set { _aDLHeaderDetailStatusID = value; }
    }

    private string _aDLHeaderDetailStatusName;
    public string ADLHeaderDetailStatusName
    {
        get { return _aDLHeaderDetailStatusName; }
        set { _aDLHeaderDetailStatusName = value; }
    }
}
