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

public class ADLDetailnStatus
{
    public ADLDetailnStatus()
    {
    }

    public ADLDetailnStatus
        (
        int aDLDetailnStatusID, 
        int aDLDetailID, 
        int aDLStatusID
        )
    {
        this.ADLDetailnStatusID = aDLDetailnStatusID;
        this.ADLDetailID = aDLDetailID;
        this.ADLStatusID = aDLStatusID;
    }


    private int _aDLDetailnStatusID;
    public int ADLDetailnStatusID
    {
        get { return _aDLDetailnStatusID; }
        set { _aDLDetailnStatusID = value; }
    }

    private int _aDLDetailID;
    public int ADLDetailID
    {
        get { return _aDLDetailID; }
        set { _aDLDetailID = value; }
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
