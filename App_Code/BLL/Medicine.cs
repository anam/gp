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

public class Medicine
{
    public Medicine()
    {
    }

    public Medicine
        (
        int medicineID, 
        string medicineName
        )
    {
        this.MedicineID = medicineID;
        this.MedicineName = medicineName;
    }


    private int _medicineID;
    public int MedicineID
    {
        get { return _medicineID; }
        set { _medicineID = value; }
    }

    private string _medicineName;
    public string MedicineName
    {
        get { return _medicineName; }
        set { _medicineName = value; }
    }
}
