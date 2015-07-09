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

public class Pos_CustomerManager
{
	public Pos_CustomerManager()
	{
	}

    public static List<Pos_Customer> GetAllPos_Customers()
    {
        List<Pos_Customer> pos_Customers = new List<Pos_Customer>();
        SqlPos_CustomerProvider sqlPos_CustomerProvider = new SqlPos_CustomerProvider();
        pos_Customers = sqlPos_CustomerProvider.GetAllPos_Customers();
        return pos_Customers;
    }


    public static Pos_Customer GetPos_CustomerByID(int id)
    {
        Pos_Customer pos_Customer = new Pos_Customer();
        SqlPos_CustomerProvider sqlPos_CustomerProvider = new SqlPos_CustomerProvider();
        pos_Customer = sqlPos_CustomerProvider.GetPos_CustomerByID(id);
        return pos_Customer;
    }


    public static int InsertPos_Customer(Pos_Customer pos_Customer)
    {
        SqlPos_CustomerProvider sqlPos_CustomerProvider = new SqlPos_CustomerProvider();
        return sqlPos_CustomerProvider.InsertPos_Customer(pos_Customer);
    }


    public static bool UpdatePos_Customer(Pos_Customer pos_Customer)
    {
        SqlPos_CustomerProvider sqlPos_CustomerProvider = new SqlPos_CustomerProvider();
        return sqlPos_CustomerProvider.UpdatePos_Customer(pos_Customer);
    }

    public static bool DeletePos_Customer(int pos_CustomerID)
    {
        SqlPos_CustomerProvider sqlPos_CustomerProvider = new SqlPos_CustomerProvider();
        return sqlPos_CustomerProvider.DeletePos_Customer(pos_CustomerID);
    }

    public static List<Pos_Customer> GetAllPos_customersBySearchArg(string cardNo, string mobileNo)
    {
        List<Pos_Customer> pos_customer=new List<Pos_Customer>();
        SqlPos_CustomerProvider posCustomerPro=new SqlPos_CustomerProvider();
        pos_customer = posCustomerPro.GetAllPos_customersBySearchArg(cardNo, mobileNo);
        return pos_customer;
    }
}
