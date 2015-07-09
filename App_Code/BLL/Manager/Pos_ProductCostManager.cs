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

public class Pos_ProductCostManager
{
	public Pos_ProductCostManager()
	{
	}

    public static List<Pos_ProductCost> GetAllPos_ProductCosts()
    {
        List<Pos_ProductCost> pos_ProductCosts = new List<Pos_ProductCost>();
        SqlPos_ProductCostProvider sqlPos_ProductCostProvider = new SqlPos_ProductCostProvider();
        pos_ProductCosts = sqlPos_ProductCostProvider.GetAllPos_ProductCosts();
        return pos_ProductCosts;
    }


    public static Pos_ProductCost GetPos_ProductCostByID(int id)
    {
        Pos_ProductCost pos_ProductCost = new Pos_ProductCost();
        SqlPos_ProductCostProvider sqlPos_ProductCostProvider = new SqlPos_ProductCostProvider();
        pos_ProductCost = sqlPos_ProductCostProvider.GetPos_ProductCostByID(id);
        return pos_ProductCost;
    }


    public static int InsertPos_ProductCost(Pos_ProductCost pos_ProductCost)
    {
        SqlPos_ProductCostProvider sqlPos_ProductCostProvider = new SqlPos_ProductCostProvider();
        return sqlPos_ProductCostProvider.InsertPos_ProductCost(pos_ProductCost);
    }


    public static bool UpdatePos_ProductCost(Pos_ProductCost pos_ProductCost)
    {
        SqlPos_ProductCostProvider sqlPos_ProductCostProvider = new SqlPos_ProductCostProvider();
        return sqlPos_ProductCostProvider.UpdatePos_ProductCost(pos_ProductCost);
    }

    public static bool DeletePos_ProductCost(int pos_ProductCostID)
    {
        SqlPos_ProductCostProvider sqlPos_ProductCostProvider = new SqlPos_ProductCostProvider();
        return sqlPos_ProductCostProvider.DeletePos_ProductCost(pos_ProductCostID);
    }
}
