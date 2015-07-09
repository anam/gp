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

public class ACC_HeadType
{
    public ACC_HeadType()
    {
    }

    public ACC_HeadType
        (
        int aCC_HeadTypeID, 
        string headTypeName, 
        string extraField1, 
        string extraField2, 
        string extraField3
        )
    {
        this.ACC_HeadTypeID = aCC_HeadTypeID;
        this.HeadTypeName = headTypeName;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.ExtraField3 = extraField3;
    }


    private int _aCC_HeadTypeID;
    public int ACC_HeadTypeID
    {
        get { return _aCC_HeadTypeID; }
        set { _aCC_HeadTypeID = value; }
    }

    private string _headTypeName;
    public string HeadTypeName
    {
        get { return _headTypeName; }
        set { _headTypeName = value; }
    }

    private string _extraField1;
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

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
}
