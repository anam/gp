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

public class MedicationTime
{
    public MedicationTime()
    {
    }

    public MedicationTime
        (
        int medicationTimeID, 
        string takingTime, 
        int medicineID, 
        int residentID, 
        string quantity, 
        string quality, 
        string frequency, 
        string extraField1, 
        string extraField2, 
        int addedBy, 
        DateTime addedDate, 
        int updateBy, 
        DateTime updateDate
        )
    {
        this.MedicationTimeID = medicationTimeID;
        this.TakingTime = takingTime;
        this.MedicineID = medicineID;
        this.ResidentID = residentID;
        this.Quantity = quantity;
        this.Quality = quality;
        this.Frequency = frequency;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.AddedBy = addedBy;
        this.AddedDate = addedDate;
        this.UpdateBy = updateBy;
        this.UpdateDate = updateDate;
    }


    private int _medicationTimeID;
    public int MedicationTimeID
    {
        get { return _medicationTimeID; }
        set { _medicationTimeID = value; }
    }

    private string _takingTime;
    public string TakingTime
    {
        get { return _takingTime; }
        set { _takingTime = value; }
    }

    private int _medicineID;
    public int MedicineID
    {
        get { return _medicineID; }
        set { _medicineID = value; }
    }

    private int _residentID;
    public int ResidentID
    {
        get { return _residentID; }
        set { _residentID = value; }
    }

    private string _quantity;
    /// <summary>
    /// Direction For Use
    /// </summary>
    public string Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    private string _quality;
    /// <summary>
    /// Strength
    /// </summary>
    public string Quality
    {
        get { return _quality; }
        set { _quality = value; }
    }

    private string _frequency;
    /// <summary>
    /// Dosage
    /// </summary>
    public string Frequency
    {
        get { return _frequency; }
        set { _frequency = value; }
    }

    private string _extraField1;
    /// <summary>
    /// EX#
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// txtRouteOfAdmin
    /// </summary>
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }

    private string _extraField3;
    /// <summary>
    /// txtAmount
    /// </summary>
    public string ExtraField3
    {
        get { return _extraField3; }
        set { _extraField3 = value; }
    }
    
    private string _extraField4;
    /// <summary>
    /// PHARMACY NAME 
    /// </summary>
    public string ExtraField4
    {
        get { return _extraField4; }
        set { _extraField4 = value; }
    }

    private string _extraField5;
    /// <summary>
    /// Is Discharge Time
    /// </summary>
    public string ExtraField5
    {
        get { return _extraField5; }
        set { _extraField5 = value; }
    }

    private string _extraField6;
    /// <summary>
    /// Status
    /// </summary>
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

    private int _updateBy;
    public int UpdateBy
    {
        get { return _updateBy; }
        set { _updateBy = value; }
    }

    private DateTime _updateDate;
    public DateTime UpdateDate
    {
        get { return _updateDate; }
        set { _updateDate = value; }
    }
}
