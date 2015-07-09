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

public class CarePlanDateTime
{
    public CarePlanDateTime()
    {
    }

    public CarePlanDateTime
        (
        int carePlanDateTimeID, 
        DateTime carePlanDateTimeValue
        )
    {
        this.CarePlanDateTimeID = carePlanDateTimeID;
        this.CarePlanDateTimeValue = carePlanDateTimeValue;
    }


    private int _carePlanDateTimeID;
    public int CarePlanDateTimeID
    {
        get { return _carePlanDateTimeID; }
        set { _carePlanDateTimeID = value; }
    }

    private DateTime _carePlanDateTimeValue;
    public DateTime CarePlanDateTimeValue
    {
        get { return _carePlanDateTimeValue; }
        set { _carePlanDateTimeValue = value; }
    }

    private int _residentID;

    public int ResidentID
    {
        get { return _residentID; }
        set { _residentID = value; }
    }
}
