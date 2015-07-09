using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class CustomerManager
{
	public CustomerManager()
	{
	}

    public static DataSet  GetAllCustomers()
    {
       DataSet customers = new DataSet();
        SqlCustomerProvider sqlCustomerProvider = new SqlCustomerProvider();
        customers = sqlCustomerProvider.GetAllCustomers();
        return customers;
    }

	public static void customersPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadCustomerPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlCustomerProvider sqlCustomerProvider = new SqlCustomerProvider();
		DataSet ds =  sqlCustomerProvider.GetCustomerPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 customersPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllCustomer()
    {
       DataSet customers = new DataSet();
        SqlCustomerProvider sqlCustomerProvider = new SqlCustomerProvider();
        customers = sqlCustomerProvider.GetDropDownLisAllCustomer();
        return customers;
    }

    public static DataSet   GetAllCustomersWithRelation()
    {
       DataSet customers = new DataSet();
        SqlCustomerProvider sqlCustomerProvider = new SqlCustomerProvider();
        customers = sqlCustomerProvider.GetAllCustomers();
        return customers;
    }


    public static Customer GetRelationshipByRelationshipID(int RelationshipID)
    {
        Customer customer = new Customer();
        SqlCustomerProvider sqlCustomerProvider = new SqlCustomerProvider();
        customer = sqlCustomerProvider.GetCustomerByRelationshipID(RelationshipID);
        return customer;
    }


    public static Customer GetIncomeByIncomeID(int IncomeID)
    {
        Customer customer = new Customer();
        SqlCustomerProvider sqlCustomerProvider = new SqlCustomerProvider();
        customer = sqlCustomerProvider.GetCustomerByIncomeID(IncomeID);
        return customer;
    }


    public static Customer GetCustomerByCustomerID(int CustomerID)
    {
        Customer customer = new Customer();
        SqlCustomerProvider sqlCustomerProvider = new SqlCustomerProvider();
        customer = sqlCustomerProvider.GetCustomerByCustomerID(CustomerID);
        return customer;
    }


    public static int InsertCustomer(Customer customer)
    {
        SqlCustomerProvider sqlCustomerProvider = new SqlCustomerProvider();
        return sqlCustomerProvider.InsertCustomer(customer);
    }


    public static bool UpdateCustomer(Customer customer)
    {
        SqlCustomerProvider sqlCustomerProvider = new SqlCustomerProvider();
        return sqlCustomerProvider.UpdateCustomer(customer);
    }

    public static bool DeleteCustomer(int customerID)
    {
        SqlCustomerProvider sqlCustomerProvider = new SqlCustomerProvider();
        return sqlCustomerProvider.DeleteCustomer(customerID);
    }
}

