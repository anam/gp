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

public class Country
{
    public Country()
    {
    }

    public Country
        (
        int countryID, 
        string countryName
        )
    {
        this.CountryID = countryID;
        this.CountryName = countryName;
    }


    private int _countryID;
    public int CountryID
    {
        get { return _countryID; }
        set { _countryID = value; }
    }

    private string _countryName;
    public string CountryName
    {
        get { return _countryName; }
        set { _countryName = value; }
    }
}
