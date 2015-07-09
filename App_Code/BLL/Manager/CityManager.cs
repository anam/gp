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

public class CityManager
{
	public CityManager()
	{
	}

    public static List<City> GetAllCities()
    {
        List<City> cities = new List<City>();
        SqlCityProvider sqlCityProvider = new SqlCityProvider();
        cities = sqlCityProvider.GetAllCities();
        return cities;
    }


    public static City GetCityByID(int id)
    {
        City city = new City();
        SqlCityProvider sqlCityProvider = new SqlCityProvider();
        city = sqlCityProvider.GetCityByID(id);
        return city;
    }


    public static int InsertCity(City city)
    {
        SqlCityProvider sqlCityProvider = new SqlCityProvider();
        return sqlCityProvider.InsertCity(city);
    }


    public static bool UpdateCity(City city)
    {
        SqlCityProvider sqlCityProvider = new SqlCityProvider();
        return sqlCityProvider.UpdateCity(city);
    }

    public static bool DeleteCity(int cityID)
    {
        SqlCityProvider sqlCityProvider = new SqlCityProvider();
        return sqlCityProvider.DeleteCity(cityID);
    }
}
