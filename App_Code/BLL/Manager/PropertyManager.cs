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

public class PropertyManager
{
	public PropertyManager()
	{
	}

    public static List<Property> GetAllProperties()
    {
        List<Property> properties = new List<Property>();
        SqlPropertyProvider sqlPropertyProvider = new SqlPropertyProvider();
        properties = sqlPropertyProvider.GetAllPropertiesSearch("");
        return properties;
    }

    public static List<Property> GetAllPropertiesSearch(string SearchString)
    {
        List<Property> properties = new List<Property>();
        SqlPropertyProvider sqlPropertyProvider = new SqlPropertyProvider();
        properties = sqlPropertyProvider.GetAllPropertiesSearch(SearchString);
        return properties;
    }

    public static Property GetPropertyByID(int id)
    {
        Property property = new Property();
        SqlPropertyProvider sqlPropertyProvider = new SqlPropertyProvider();
        property = sqlPropertyProvider.GetPropertyByID(id);
        return property;
    }


    public static int InsertProperty(Property property)
    {
        SqlPropertyProvider sqlPropertyProvider = new SqlPropertyProvider();
        return sqlPropertyProvider.InsertProperty(property);
    }


    public static bool UpdateProperty(Property property)
    {
        SqlPropertyProvider sqlPropertyProvider = new SqlPropertyProvider();
        return sqlPropertyProvider.UpdateProperty(property);
    }

    public static bool DeleteProperty(int propertyID)
    {
        SqlPropertyProvider sqlPropertyProvider = new SqlPropertyProvider();
        return sqlPropertyProvider.DeleteProperty(propertyID);
    }
}
