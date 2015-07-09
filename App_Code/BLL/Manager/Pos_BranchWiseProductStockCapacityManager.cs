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

public class Pos_BranchWiseProductStockCapacityManager
{
	public Pos_BranchWiseProductStockCapacityManager()
	{
	}

    public static List<Pos_BranchWiseProductStockCapacity> GetAllPos_BranchWiseProductStockCapacities()
    {
        List<Pos_BranchWiseProductStockCapacity> pos_BranchWiseProductStockCapacities = new List<Pos_BranchWiseProductStockCapacity>();
        SqlPos_BranchWiseProductStockCapacityProvider sqlPos_BranchWiseProductStockCapacityProvider = new SqlPos_BranchWiseProductStockCapacityProvider();
        pos_BranchWiseProductStockCapacities = sqlPos_BranchWiseProductStockCapacityProvider.GetAllPos_BranchWiseProductStockCapacities();
        return pos_BranchWiseProductStockCapacities;
    }


    public static Pos_BranchWiseProductStockCapacity GetPos_BranchWiseProductStockCapacityByID(int id)
    {
        Pos_BranchWiseProductStockCapacity pos_BranchWiseProductStockCapacity = new Pos_BranchWiseProductStockCapacity();
        SqlPos_BranchWiseProductStockCapacityProvider sqlPos_BranchWiseProductStockCapacityProvider = new SqlPos_BranchWiseProductStockCapacityProvider();
        pos_BranchWiseProductStockCapacity = sqlPos_BranchWiseProductStockCapacityProvider.GetPos_BranchWiseProductStockCapacityByID(id);
        return pos_BranchWiseProductStockCapacity;
    }


    public static int InsertPos_BranchWiseProductStockCapacity(Pos_BranchWiseProductStockCapacity pos_BranchWiseProductStockCapacity)
    {
        SqlPos_BranchWiseProductStockCapacityProvider sqlPos_BranchWiseProductStockCapacityProvider = new SqlPos_BranchWiseProductStockCapacityProvider();
        return sqlPos_BranchWiseProductStockCapacityProvider.InsertPos_BranchWiseProductStockCapacity(pos_BranchWiseProductStockCapacity);
    }


    public static bool UpdatePos_BranchWiseProductStockCapacity(Pos_BranchWiseProductStockCapacity pos_BranchWiseProductStockCapacity)
    {
        SqlPos_BranchWiseProductStockCapacityProvider sqlPos_BranchWiseProductStockCapacityProvider = new SqlPos_BranchWiseProductStockCapacityProvider();
        return sqlPos_BranchWiseProductStockCapacityProvider.UpdatePos_BranchWiseProductStockCapacity(pos_BranchWiseProductStockCapacity);
    }

    public static bool DeletePos_BranchWiseProductStockCapacity(int pos_BranchWiseProductStockCapacityID)
    {
        SqlPos_BranchWiseProductStockCapacityProvider sqlPos_BranchWiseProductStockCapacityProvider = new SqlPos_BranchWiseProductStockCapacityProvider();
        return sqlPos_BranchWiseProductStockCapacityProvider.DeletePos_BranchWiseProductStockCapacity(pos_BranchWiseProductStockCapacityID);
    }
}
