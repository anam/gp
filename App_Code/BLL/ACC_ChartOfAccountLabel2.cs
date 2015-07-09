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

public class ACC_ChartOfAccountLabel2
{
    public ACC_ChartOfAccountLabel2()
    {
    }

    public ACC_ChartOfAccountLabel2
        (
        int aCC_ChartOfAccountLabel2ID, 
        string code, 
        int aCC_ChartOfAccountLabel1ID, 
        string chartOfAccountLabel2Text, 
        string extraField1, 
        string extraField2, 
        string extraField3, 
        int addedBy, 
        DateTime addedDate, 
        int updatedBy, 
        DateTime updatedDate, 
        int rowStatusID
        )
    {
        this.ACC_ChartOfAccountLabel2ID = aCC_ChartOfAccountLabel2ID;
        this.Code = code;
        this.ACC_ChartOfAccountLabel1ID = aCC_ChartOfAccountLabel1ID;
        this.ChartOfAccountLabel2Text = chartOfAccountLabel2Text;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.ExtraField3 = extraField3;
        this.AddedBy = addedBy;
        this.AddedDate = addedDate;
        this.UpdatedBy = updatedBy;
        this.UpdatedDate = updatedDate;
        this.RowStatusID = rowStatusID;
    }


    private int _aCC_ChartOfAccountLabel2ID;
    public int ACC_ChartOfAccountLabel2ID
    {
        get { return _aCC_ChartOfAccountLabel2ID; }
        set { _aCC_ChartOfAccountLabel2ID = value; }
    }

    private string _code;
    public string Code
    {
        get { return _code; }
        set { _code = value; }
    }

    private int _aCC_ChartOfAccountLabel1ID;
    public int ACC_ChartOfAccountLabel1ID
    {
        get { return _aCC_ChartOfAccountLabel1ID; }
        set { _aCC_ChartOfAccountLabel1ID = value; }
    }

    private string _chartOfAccountLabel2Text;
    public string ChartOfAccountLabel2Text
    {
        get { return _chartOfAccountLabel2Text; }
        set { _chartOfAccountLabel2Text = value; }
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

    private int _addedBy;
    public int AddedBy
    {
        get { return _addedBy; }
        set { _addedBy = value; }
    }

    private DateTime _addedDate;
    public DateTime AddedDate
    {
        get { return _addedDate; }
        set { _addedDate = value; }
    }

    private int _updatedBy;
    public int UpdatedBy
    {
        get { return _updatedBy; }
        set { _updatedBy = value; }
    }

    private DateTime _updatedDate;
    public DateTime UpdatedDate
    {
        get { return _updatedDate; }
        set { _updatedDate = value; }
    }

    private int _rowStatusID;
    public int RowStatusID
    {
        get { return _rowStatusID; }
        set { _rowStatusID = value; }
    }
}
