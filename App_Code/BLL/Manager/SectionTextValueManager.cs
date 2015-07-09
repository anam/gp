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

public class SectionTextValueManager
{
	public SectionTextValueManager()
	{
	}

    public static List<SectionTextValue> GetAllSectionTextValues()
    {
        List<SectionTextValue> sectionTextValues = new List<SectionTextValue>();
        SqlSectionTextValueProvider sqlSectionTextValueProvider = new SqlSectionTextValueProvider();
        sectionTextValues = sqlSectionTextValueProvider.GetAllSectionTextValues();
        return sectionTextValues;
    }


    public static SectionTextValue GetSectionTextValueByID(int id)
    {
        SectionTextValue sectionTextValue = new SectionTextValue();
        SqlSectionTextValueProvider sqlSectionTextValueProvider = new SqlSectionTextValueProvider();
        sectionTextValue = sqlSectionTextValueProvider.GetSectionTextValueByID(id);
        return sectionTextValue;
    }

    public static SectionTextValue GetSectionTextValueByCarePlanDateTimeID(int CarePlanDateTimeID)
    {
        SectionTextValue sectionTextValue = new SectionTextValue();
        SqlSectionTextValueProvider sqlSectionTextValueProvider = new SqlSectionTextValueProvider();
        sectionTextValue = sqlSectionTextValueProvider.GetSectionTextValueByCarePlanDateTimeID(CarePlanDateTimeID);
        return sectionTextValue;
    }



    public static int InsertSectionTextValue(SectionTextValue sectionTextValue)
    {
        SqlSectionTextValueProvider sqlSectionTextValueProvider = new SqlSectionTextValueProvider();
        return sqlSectionTextValueProvider.InsertSectionTextValue(sectionTextValue);
    }


    public static bool UpdateSectionTextValue(SectionTextValue sectionTextValue)
    {
        SqlSectionTextValueProvider sqlSectionTextValueProvider = new SqlSectionTextValueProvider();
        return sqlSectionTextValueProvider.UpdateSectionTextValue(sectionTextValue);
    }

    public static bool DeleteSectionTextValue(int sectionTextValueID)
    {
        SqlSectionTextValueProvider sqlSectionTextValueProvider = new SqlSectionTextValueProvider();
        return sqlSectionTextValueProvider.DeleteSectionTextValue(sectionTextValueID);
    }
}
