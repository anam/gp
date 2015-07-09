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

public class DefaultButton
{
    public DefaultButton()
    {
    }

    public DefaultButton
        (
        int defaultButtonID, 
        string defaultButtonName, 
        string defaultButtonText
        )
    {
        this.DefaultButtonID = defaultButtonID;
        this.DefaultButtonName = defaultButtonName;
        this.DefaultButtonText = defaultButtonText;
    }


    private int _defaultButtonID;
    public int DefaultButtonID
    {
        get { return _defaultButtonID; }
        set { _defaultButtonID = value; }
    }

    private string _defaultButtonName;
    public string DefaultButtonName
    {
        get { return _defaultButtonName; }
        set { _defaultButtonName = value; }
    }

    private string _defaultButtonText;
    public string DefaultButtonText
    {
        get { return _defaultButtonText; }
        set { _defaultButtonText = value; }
    }
}
