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

public class DoctorsOrder
{
    public DoctorsOrder()
    {
    }

    public DoctorsOrder
        (
        int doctorsOrderID, 
        string clinicalFindings, 
        string orders, 
        int residentID, 
        DateTime orderDate, 
        int addeBy, 
        DateTime addedDate, 
        int updatedBy, 
        DateTime updatedDate, 
        string extraField1, 
        string extraField2, 
        string extraField3, 
        string extraField4, 
        string extraField5
        )
    {
        this.DoctorsOrderID = doctorsOrderID;
        this.ClinicalFindings = clinicalFindings;
        this.Orders = orders;
        this.ResidentID = residentID;
        this.OrderDate = orderDate;
        this.AddeBy = addeBy;
        this.AddedDate = addedDate;
        this.UpdatedBy = updatedBy;
        this.UpdatedDate = updatedDate;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.ExtraField3 = extraField3;
        this.ExtraField4 = extraField4;
        this.ExtraField5 = extraField5;
    }


    private int _doctorsOrderID;
    public int DoctorsOrderID
    {
        get { return _doctorsOrderID; }
        set { _doctorsOrderID = value; }
    }

    private string _clinicalFindings;
    public string ClinicalFindings
    {
        get { return _clinicalFindings; }
        set { _clinicalFindings = value; }
    }

    private string _orders;
    public string Orders
    {
        get { return _orders; }
        set { _orders = value; }
    }

    private int _residentID;
    public int ResidentID
    {
        get { return _residentID; }
        set { _residentID = value; }
    }

    private DateTime _orderDate;
    public DateTime OrderDate
    {
        get { return _orderDate; }
        set { _orderDate = value; }
    }

    private int _addeBy;
    public int AddeBy
    {
        get { return _addeBy; }
        set { _addeBy = value; }
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

    private string _extraField1;
    /// <summary>
    /// Physician
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// MR#
    /// </summary>
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }

    private string _extraField3;
    /// <summary>
    /// Staff Notified
    /// </summary>
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
}
