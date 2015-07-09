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

public class MedicineTimenDate
{
    public MedicineTimenDate()
    {
    }

    public MedicineTimenDate
        (
        int medicineTimenDateID, 
        int medicationTimeID, 
        DateTime medicineDate
        )
    {
        this.MedicineTimenDateID = medicineTimenDateID;
        this.MedicationTimeID = medicationTimeID;
        this.MedicineDate = medicineDate;
    }


    private int _medicineTimenDateID;
    public int MedicineTimenDateID
    {
        get { return _medicineTimenDateID; }
        set { _medicineTimenDateID = value; }
    }

    private int _medicationTimeID;
    public int MedicationTimeID
    {
        get { return _medicationTimeID; }
        set { _medicationTimeID = value; }
    }

    private DateTime _medicineDate;
    public DateTime MedicineDate
    {
        get { return _medicineDate; }
        set { _medicineDate = value; }
    }

    private string _extraField1;
    /// <summary>
    /// Comment
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }
    private string _extraField2;
    /// <summary>
    /// Posted
    /// </summary>
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }
    private string _extraField3;
    /// <summary>
    /// Posted user Initial
    /// </summary>
    public string ExtraField3
    {
        get { return _extraField3; }
        set { _extraField3 = value; }
    }
}
