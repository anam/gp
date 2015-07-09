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

public class ADLItemDescriptionManager
{
	public ADLItemDescriptionManager()
	{
	}

    public static List<ADLItemDescription> GetAllADLItemDescriptions()
    {
        List<ADLItemDescription> aDLItemDescriptions = new List<ADLItemDescription>();
        SqlADLItemDescriptionProvider sqlADLItemDescriptionProvider = new SqlADLItemDescriptionProvider();
        aDLItemDescriptions = sqlADLItemDescriptionProvider.GetAllADLItemDescriptions();
        return aDLItemDescriptions;
    }


    public static ADLItemDescription GetADLItemDescriptionByID(int id)
    {
        ADLItemDescription aDLItemDescription = new ADLItemDescription();
        SqlADLItemDescriptionProvider sqlADLItemDescriptionProvider = new SqlADLItemDescriptionProvider();
        aDLItemDescription = sqlADLItemDescriptionProvider.GetADLItemDescriptionByID(id);
        return aDLItemDescription;
    }


    public static int InsertADLItemDescription(ADLItemDescription aDLItemDescription)
    {
        SqlADLItemDescriptionProvider sqlADLItemDescriptionProvider = new SqlADLItemDescriptionProvider();
        return sqlADLItemDescriptionProvider.InsertADLItemDescription(aDLItemDescription);
    }


    public static bool UpdateADLItemDescription(ADLItemDescription aDLItemDescription)
    {
        SqlADLItemDescriptionProvider sqlADLItemDescriptionProvider = new SqlADLItemDescriptionProvider();
        return sqlADLItemDescriptionProvider.UpdateADLItemDescription(aDLItemDescription);
    }

    public static bool DeleteADLItemDescription(int aDLItemDescriptionID)
    {
        SqlADLItemDescriptionProvider sqlADLItemDescriptionProvider = new SqlADLItemDescriptionProvider();
        return sqlADLItemDescriptionProvider.DeleteADLItemDescription(aDLItemDescriptionID);
    }
}
