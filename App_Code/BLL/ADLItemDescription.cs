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

public class ADLItemDescription
{
    public ADLItemDescription()
    {
    }

    public ADLItemDescription
        (
        int aDLItemDescriptionID, 
        string aDLItemDescriptionName
        )
    {
        this.ADLItemDescriptionID = aDLItemDescriptionID;
        this.ADLItemDescriptionName = aDLItemDescriptionName;
    }


    private int _aDLItemDescriptionID;
    public int ADLItemDescriptionID
    {
        get { return _aDLItemDescriptionID; }
        set { _aDLItemDescriptionID = value; }
    }

    private string _aDLItemDescriptionName;
    public string ADLItemDescriptionName
    {
        get { return _aDLItemDescriptionName; }
        set { _aDLItemDescriptionName = value; }
    }
}
