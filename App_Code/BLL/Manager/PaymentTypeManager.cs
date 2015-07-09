using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class PaymentTypeManager
{
	public PaymentTypeManager()
	{
	}

    public static DataSet  GetAllPaymentTypes()
    {
       DataSet paymentTypes = new DataSet();
        SqlPaymentTypeProvider sqlPaymentTypeProvider = new SqlPaymentTypeProvider();
        paymentTypes = sqlPaymentTypeProvider.GetAllPaymentTypes();
        return paymentTypes;
    }

	public static void paymentTypesPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadPaymentTypePage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlPaymentTypeProvider sqlPaymentTypeProvider = new SqlPaymentTypeProvider();
		DataSet ds =  sqlPaymentTypeProvider.GetPaymentTypePageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 paymentTypesPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllPaymentType()
    {
       DataSet paymentTypes = new DataSet();
        SqlPaymentTypeProvider sqlPaymentTypeProvider = new SqlPaymentTypeProvider();
        paymentTypes = sqlPaymentTypeProvider.GetDropDownLisAllPaymentType();
        return paymentTypes;
    }


    public static PaymentType GetPaymentTypeByPymnentTypeID(int PymnentTypeID)
    {
        PaymentType paymentType = new PaymentType();
        SqlPaymentTypeProvider sqlPaymentTypeProvider = new SqlPaymentTypeProvider();
        paymentType = sqlPaymentTypeProvider.GetPaymentTypeByPymnentTypeID(PymnentTypeID);
        return paymentType;
    }


    public static int InsertPaymentType(PaymentType paymentType)
    {
        SqlPaymentTypeProvider sqlPaymentTypeProvider = new SqlPaymentTypeProvider();
        return sqlPaymentTypeProvider.InsertPaymentType(paymentType);
    }


    public static bool UpdatePaymentType(PaymentType paymentType)
    {
        SqlPaymentTypeProvider sqlPaymentTypeProvider = new SqlPaymentTypeProvider();
        return sqlPaymentTypeProvider.UpdatePaymentType(paymentType);
    }

    public static bool DeletePaymentType(int paymentTypeID)
    {
        SqlPaymentTypeProvider sqlPaymentTypeProvider = new SqlPaymentTypeProvider();
        return sqlPaymentTypeProvider.DeletePaymentType(paymentTypeID);
    }
}

