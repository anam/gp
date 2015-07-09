using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class IncomeManager
{
	public IncomeManager()
	{
	}

    public static DataSet  GetAllIncomes()
    {
       DataSet incomes = new DataSet();
        SqlIncomeProvider sqlIncomeProvider = new SqlIncomeProvider();
        incomes = sqlIncomeProvider.GetAllIncomes();
        return incomes;
    }

	public static void incomesPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadIncomePage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlIncomeProvider sqlIncomeProvider = new SqlIncomeProvider();
		DataSet ds =  sqlIncomeProvider.GetIncomePageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 incomesPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllIncome()
    {
       DataSet incomes = new DataSet();
        SqlIncomeProvider sqlIncomeProvider = new SqlIncomeProvider();
        incomes = sqlIncomeProvider.GetDropDownLisAllIncome();
        return incomes;
    }


    public static Income GetIncomeByIncomeID(int IncomeID)
    {
        Income income = new Income();
        SqlIncomeProvider sqlIncomeProvider = new SqlIncomeProvider();
        income = sqlIncomeProvider.GetIncomeByIncomeID(IncomeID);
        return income;
    }


    public static int InsertIncome(Income income)
    {
        SqlIncomeProvider sqlIncomeProvider = new SqlIncomeProvider();
        return sqlIncomeProvider.InsertIncome(income);
    }


    public static bool UpdateIncome(Income income)
    {
        SqlIncomeProvider sqlIncomeProvider = new SqlIncomeProvider();
        return sqlIncomeProvider.UpdateIncome(income);
    }

    public static bool DeleteIncome(int incomeID)
    {
        SqlIncomeProvider sqlIncomeProvider = new SqlIncomeProvider();
        return sqlIncomeProvider.DeleteIncome(incomeID);
    }
}

