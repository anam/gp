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

public class ADLItem
{
    public ADLItem()
    {
    }

    public ADLItem
        (
        int aDLItemID, 
        string aDLItemName, 
        int aDLItemDescriptionID, 
        int aDLStatusID, 
        int residentID, 
        DateTime iteamTime, 
        string comment, 
        string extraField1, 
        string extraField2, 
        string extraField3, 
        string extraField4, 
        string extraField5, 
        string extraField6, 
        string extraField7, 
        string extraField8, 
        string extraField9, 
        string extraField10
        )
    {
        this.ADLItemID = aDLItemID;
        this.ADLItemName = aDLItemName;
        this.ADLItemDescriptionID = aDLItemDescriptionID;
        this.ADLStatusID = aDLStatusID;
        this.ResidentID = residentID;
        this.IteamTime = iteamTime;
        this.Comment = comment;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.ExtraField3 = extraField3;
        this.ExtraField4 = extraField4;
        this.ExtraField5 = extraField5;
        this.ExtraField6 = extraField6;
        this.ExtraField7 = extraField7;
        this.ExtraField8 = extraField8;
        this.ExtraField9 = extraField9;
        this.ExtraField10 = extraField10;
    }


    private int _aDLItemID;
    public int ADLItemID
    {
        get { return _aDLItemID; }
        set { _aDLItemID = value; }
    }

    private string _aDLItemName;
    public string ADLItemName
    {
        get { return _aDLItemName; }
        set { _aDLItemName = value; }
    }

    private int _aDLItemDescriptionID;
    public int ADLItemDescriptionID
    {
        get { return _aDLItemDescriptionID; }
        set { _aDLItemDescriptionID = value; }
    }

    private int _aDLStatusID;
    public int ADLStatusID
    {
        get { return _aDLStatusID; }
        set { _aDLStatusID = value; }
    }

    private int _residentID;
    public int ResidentID
    {
        get { return _residentID; }
        set { _residentID = value; }
    }

    private DateTime _iteamTime;
    public DateTime IteamTime
    {
        get { return _iteamTime; }
        set { _iteamTime = value; }
    }

    private string _comment;
    public string Comment
    {
        get { return _comment; }
        set { _comment = value; }
    }


    /// <summary>
    /// Iteam Description at display time
    /// </summary>
    private string _extraField1;
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    /// <summary>
    /// Iteam Status at display time
    /// </summary>
    private string _extraField2;
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }

    private string _extraField3;
    public string ExtraField3
    {
        get { return _extraField3; }
        set { _extraField3 = value; }
    }

    private string _extraField4;
    public string ExtraField4
    {
        get { return _extraField4; }
        set { _extraField4 = value; }
    }

    private string _extraField5;
    public string ExtraField5
    {
        get { return _extraField5; }
        set { _extraField5 = value; }
    }

    private string _extraField6;
    public string ExtraField6
    {
        get { return _extraField6; }
        set { _extraField6 = value; }
    }

    private string _extraField7;
    public string ExtraField7
    {
        get { return _extraField7; }
        set { _extraField7 = value; }
    }

    private string _extraField8;
    public string ExtraField8
    {
        get { return _extraField8; }
        set { _extraField8 = value; }
    }

    private string _extraField9;
    public string ExtraField9
    {
        get { return _extraField9; }
        set { _extraField9 = value; }
    }

    private string _extraField10;
    public string ExtraField10
    {
        get { return _extraField10; }
        set { _extraField10 = value; }
    }
}
