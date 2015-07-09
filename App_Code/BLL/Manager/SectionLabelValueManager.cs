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

public class SectionLabelValueManager
{
	public SectionLabelValueManager()
	{
	}

    public static List<SectionLabelValue> GetAllSectionLabelValues()
    {
        List<SectionLabelValue> sectionLabelValues = new List<SectionLabelValue>();
        SqlSectionLabelValueProvider sqlSectionLabelValueProvider = new SqlSectionLabelValueProvider();
        sectionLabelValues = sqlSectionLabelValueProvider.GetAllSectionLabelValues();
        return sectionLabelValues;
    }


    public static List<SectionLabelValue> GetAllSectionLabelValuesByCarePlanDateTimeID(int CarePlanDateTimeID)
    {
        List<SectionLabelValue> sectionLabelValues = new List<SectionLabelValue>();
        SqlSectionLabelValueProvider sqlSectionLabelValueProvider = new SqlSectionLabelValueProvider();
        sectionLabelValues = sqlSectionLabelValueProvider.GetAllSectionLabelValuesByCarePlanDateTimeID(CarePlanDateTimeID);
        return sectionLabelValues;
    }



    public static SectionLabelValue GetSectionLabelValueByID(int id)
    {
        SectionLabelValue sectionLabelValue = new SectionLabelValue();
        SqlSectionLabelValueProvider sqlSectionLabelValueProvider = new SqlSectionLabelValueProvider();
        sectionLabelValue = sqlSectionLabelValueProvider.GetSectionLabelValueByID(id);
        return sectionLabelValue;
    }


    public static int InsertSectionLabelValue(SectionLabelValue sectionLabelValue)
    {
        SqlSectionLabelValueProvider sqlSectionLabelValueProvider = new SqlSectionLabelValueProvider();
        return sqlSectionLabelValueProvider.InsertSectionLabelValue(sectionLabelValue);
    }


    public static bool UpdateSectionLabelValue(SectionLabelValue sectionLabelValue)
    {
        SqlSectionLabelValueProvider sqlSectionLabelValueProvider = new SqlSectionLabelValueProvider();
        return sqlSectionLabelValueProvider.UpdateSectionLabelValue(sectionLabelValue);
    }

    public static bool DeleteSectionLabelValue(int sectionLabelValueID)
    {
        SqlSectionLabelValueProvider sqlSectionLabelValueProvider = new SqlSectionLabelValueProvider();
        return sqlSectionLabelValueProvider.DeleteSectionLabelValue(sectionLabelValueID);
    }
    public static bool DeleteSectionLabelValueByCarePlanDateTimeID(string CarePlanDateTimeID)
    {
        SqlSectionLabelValueProvider sqlSectionLabelValueProvider = new SqlSectionLabelValueProvider();
        return sqlSectionLabelValueProvider.DeleteSectionLabelValueByCarePlanDateTimeID(CarePlanDateTimeID);
    }

}
