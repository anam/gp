using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class RelationshipManager
{
	public RelationshipManager()
	{
	}

    public static DataSet  GetAllRelationships()
    {
       DataSet relationships = new DataSet();
        SqlRelationshipProvider sqlRelationshipProvider = new SqlRelationshipProvider();
        relationships = sqlRelationshipProvider.GetAllRelationships();
        return relationships;
    }

	public static void relationshipsPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
		{
		double dblPageCount = (double)((decimal)recordCount / decimal.Parse(PageSize.ToString()));
		int pageCount = (int)Math.Ceiling(dblPageCount);
		List<ListItem> pages = new List<ListItem>();
		if (pageCount > 0)
		{
 		pages.Add(new ListItem("First", "1", currentPage > 1));
		for (int i = 1; i <= pageCount; i++)
		{
		 pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
		}
		pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
		}
		rptPager.DataSource = pages;
		rptPager.DataBind();
		}
 	public static void LoadRelationshipPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlRelationshipProvider sqlRelationshipProvider = new SqlRelationshipProvider();
		DataSet ds =  sqlRelationshipProvider.GetRelationshipPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 relationshipsPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllRelationship()
    {
       DataSet relationships = new DataSet();
        SqlRelationshipProvider sqlRelationshipProvider = new SqlRelationshipProvider();
        relationships = sqlRelationshipProvider.GetDropDownLisAllRelationship();
        return relationships;
    }


    public static Relationship GetRelationshipByRelationshipID(int RelationshipID)
    {
        Relationship relationship = new Relationship();
        SqlRelationshipProvider sqlRelationshipProvider = new SqlRelationshipProvider();
        relationship = sqlRelationshipProvider.GetRelationshipByRelationshipID(RelationshipID);
        return relationship;
    }


    public static int InsertRelationship(Relationship relationship)
    {
        SqlRelationshipProvider sqlRelationshipProvider = new SqlRelationshipProvider();
        return sqlRelationshipProvider.InsertRelationship(relationship);
    }


    public static bool UpdateRelationship(Relationship relationship)
    {
        SqlRelationshipProvider sqlRelationshipProvider = new SqlRelationshipProvider();
        return sqlRelationshipProvider.UpdateRelationship(relationship);
    }

    public static bool DeleteRelationship(int relationshipID)
    {
        SqlRelationshipProvider sqlRelationshipProvider = new SqlRelationshipProvider();
        return sqlRelationshipProvider.DeleteRelationship(relationshipID);
    }
}

