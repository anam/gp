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

public class ADLDateTime
{
    public ADLDateTime()
    {
    }

    public ADLDateTime
        (
        int aDLDateTimeID, 
        DateTime aDLDateTimeValue
        )
    {
        this.ADLDateTimeID = aDLDateTimeID;
        this.ADLDateTimeValue = aDLDateTimeValue;
    }


    private int _aDLDateTimeID;
    public int ADLDateTimeID
    {
        get { return _aDLDateTimeID; }
        set { _aDLDateTimeID = value; }
    }

    private DateTime _aDLDateTimeValue;
    public DateTime ADLDateTimeValue
    {
        get { return _aDLDateTimeValue; }
        set { _aDLDateTimeValue = value; }
    }
}
