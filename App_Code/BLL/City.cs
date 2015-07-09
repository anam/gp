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

public class City
{
    public City()
    {
    }

    public City
        (
        int cityID, 
        string cityName, 
        int stateID
        )
    {
        this.CityID = cityID;
        this.CityName = cityName;
        this.StateID = stateID;
    }


    private int _cityID;
    public int CityID
    {
        get { return _cityID; }
        set { _cityID = value; }
    }

    private string _cityName;
    public string CityName
    {
        get { return _cityName; }
        set { _cityName = value; }
    }

    private int _stateID;
    public int StateID
    {
        get { return _stateID; }
        set { _stateID = value; }
    }
}
