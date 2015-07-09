using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class GiftManager
{
	public GiftManager()
	{
	}

    public static DataSet  GetAllGifts()
    {
       DataSet gifts = new DataSet();
        SqlGiftProvider sqlGiftProvider = new SqlGiftProvider();
        gifts = sqlGiftProvider.GetAllGifts();
        return gifts;
    }

	public static void giftsPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadGiftPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlGiftProvider sqlGiftProvider = new SqlGiftProvider();
		DataSet ds =  sqlGiftProvider.GetGiftPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 giftsPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllGift()
    {
       DataSet gifts = new DataSet();
        SqlGiftProvider sqlGiftProvider = new SqlGiftProvider();
        gifts = sqlGiftProvider.GetDropDownLisAllGift();
        return gifts;
    }


    public static Gift GetGiftByGiftID(int GiftID)
    {
        Gift gift = new Gift();
        SqlGiftProvider sqlGiftProvider = new SqlGiftProvider();
        gift = sqlGiftProvider.GetGiftByGiftID(GiftID);
        return gift;
    }


    public static int InsertGift(Gift gift)
    {
        SqlGiftProvider sqlGiftProvider = new SqlGiftProvider();
        return sqlGiftProvider.InsertGift(gift);
    }


    public static bool UpdateGift(Gift gift)
    {
        SqlGiftProvider sqlGiftProvider = new SqlGiftProvider();
        return sqlGiftProvider.UpdateGift(gift);
    }

    public static bool DeleteGift(int giftID)
    {
        SqlGiftProvider sqlGiftProvider = new SqlGiftProvider();
        return sqlGiftProvider.DeleteGift(giftID);
    }
}

