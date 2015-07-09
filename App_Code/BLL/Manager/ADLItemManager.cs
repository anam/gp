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

public class ADLItemManager
{
	public ADLItemManager()
	{
	}

    public static List<ADLItem> GetAllADLItems()
    {
        List<ADLItem> aDLItems = new List<ADLItem>();
        SqlADLItemProvider sqlADLItemProvider = new SqlADLItemProvider();
        aDLItems = sqlADLItemProvider.GetAllADLItems();
        return aDLItems;
    }

    public static List<ADLItem> SearchADLItems(string SearchString)
    {
        List<ADLItem> aDLItems = new List<ADLItem>();
        SqlADLItemProvider sqlADLItemProvider = new SqlADLItemProvider();
        aDLItems = sqlADLItemProvider.SearchADLItems(SearchString);
        return aDLItems;
    }


    public static ADLItem GetADLItemByID(int id)
    {
        ADLItem aDLItem = new ADLItem();
        SqlADLItemProvider sqlADLItemProvider = new SqlADLItemProvider();
        aDLItem = sqlADLItemProvider.GetADLItemByID(id);
        return aDLItem;
    }


    public static int InsertADLItem(ADLItem aDLItem)
    {
        SqlADLItemProvider sqlADLItemProvider = new SqlADLItemProvider();
        return sqlADLItemProvider.InsertADLItem(aDLItem);
    }


    public static bool UpdateADLItem(ADLItem aDLItem)
    {
        SqlADLItemProvider sqlADLItemProvider = new SqlADLItemProvider();
        return sqlADLItemProvider.UpdateADLItem(aDLItem);
    }

    public static bool DeleteADLItem(int aDLItemID)
    {
        SqlADLItemProvider sqlADLItemProvider = new SqlADLItemProvider();
        return sqlADLItemProvider.DeleteADLItem(aDLItemID);
    }
}
