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

public class DoctorsOrderManager
{
	public DoctorsOrderManager()
	{
	}

    public static List<DoctorsOrder> GetAllDoctorsOrders()
    {
        List<DoctorsOrder> doctorsOrders = new List<DoctorsOrder>();
        SqlDoctorsOrderProvider sqlDoctorsOrderProvider = new SqlDoctorsOrderProvider();
        doctorsOrders = sqlDoctorsOrderProvider.GetAllDoctorsOrders();
        return doctorsOrders;
    }

    public static List<DoctorsOrder> GetAllDoctorsOrdersByResidentID(int residentID)
    {
        List<DoctorsOrder> doctorsOrders = new List<DoctorsOrder>();
        SqlDoctorsOrderProvider sqlDoctorsOrderProvider = new SqlDoctorsOrderProvider();
        doctorsOrders = sqlDoctorsOrderProvider.GetAllDoctorsOrdersByResidentID(residentID);
        return doctorsOrders;
    }


    public static DoctorsOrder GetDoctorsOrderByID(int id)
    {
        DoctorsOrder doctorsOrder = new DoctorsOrder();
        SqlDoctorsOrderProvider sqlDoctorsOrderProvider = new SqlDoctorsOrderProvider();
        doctorsOrder = sqlDoctorsOrderProvider.GetDoctorsOrderByID(id);
        return doctorsOrder;
    }


    public static int InsertDoctorsOrder(DoctorsOrder doctorsOrder)
    {
        SqlDoctorsOrderProvider sqlDoctorsOrderProvider = new SqlDoctorsOrderProvider();
        return sqlDoctorsOrderProvider.InsertDoctorsOrder(doctorsOrder);
    }


    public static bool UpdateDoctorsOrder(DoctorsOrder doctorsOrder)
    {
        SqlDoctorsOrderProvider sqlDoctorsOrderProvider = new SqlDoctorsOrderProvider();
        return sqlDoctorsOrderProvider.UpdateDoctorsOrder(doctorsOrder);
    }

    public static bool DeleteDoctorsOrder(int doctorsOrderID)
    {
        SqlDoctorsOrderProvider sqlDoctorsOrderProvider = new SqlDoctorsOrderProvider();
        return sqlDoctorsOrderProvider.DeleteDoctorsOrder(doctorsOrderID);
    }
}
