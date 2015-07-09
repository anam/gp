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

public class SectionLabelManager
{
	public SectionLabelManager()
	{
	}

    public static List<SectionLabel> GetAllSectionLabels()
    {
        List<SectionLabel> sectionLabels = new List<SectionLabel>();
        SqlSectionLabelProvider sqlSectionLabelProvider = new SqlSectionLabelProvider();
        sectionLabels = sqlSectionLabelProvider.GetAllSectionLabels();
        return sectionLabels;
    }


    public static SectionLabel GetSectionLabelByID(int id)
    {
        SectionLabel sectionLabel = new SectionLabel();
        SqlSectionLabelProvider sqlSectionLabelProvider = new SqlSectionLabelProvider();
        sectionLabel = sqlSectionLabelProvider.GetSectionLabelByID(id);
        return sectionLabel;
    }


    public static int InsertSectionLabel(SectionLabel sectionLabel)
    {
        SqlSectionLabelProvider sqlSectionLabelProvider = new SqlSectionLabelProvider();
        return sqlSectionLabelProvider.InsertSectionLabel(sectionLabel);
    }


    public static bool UpdateSectionLabel(SectionLabel sectionLabel)
    {
        SqlSectionLabelProvider sqlSectionLabelProvider = new SqlSectionLabelProvider();
        return sqlSectionLabelProvider.UpdateSectionLabel(sectionLabel);
    }

    public static bool DeleteSectionLabel(int sectionLabelID)
    {
        SqlSectionLabelProvider sqlSectionLabelProvider = new SqlSectionLabelProvider();
        return sqlSectionLabelProvider.DeleteSectionLabel(sectionLabelID);
    }
}
