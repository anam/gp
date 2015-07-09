using System;
using System.Collections.Generic;
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

public class CountryManager
{
	public CountryManager()
	{
	}

    public static List<Country> GetAllCountries()
    {
        List<Country> countries = new List<Country>();
        SqlCountryProvider sqlCountryProvider = new SqlCountryProvider();
        countries = sqlCountryProvider.GetAllCountries();
        return countries;
    }


    public static Country GetCountryByID(int id)
    {
        Country country = new Country();
        SqlCountryProvider sqlCountryProvider = new SqlCountryProvider();
        country = sqlCountryProvider.GetCountryByID(id);
        return country;
    }


    public static int InsertCountry(Country country)
    {
        SqlCountryProvider sqlCountryProvider = new SqlCountryProvider();
        return sqlCountryProvider.InsertCountry(country);
    }


    public static bool UpdateCountry(Country country)
    {
        SqlCountryProvider sqlCountryProvider = new SqlCountryProvider();
        return sqlCountryProvider.UpdateCountry(country);
    }

    public static bool DeleteCountry(int countryID)
    {
        SqlCountryProvider sqlCountryProvider = new SqlCountryProvider();
        return sqlCountryProvider.DeleteCountry(countryID);
    }
}
