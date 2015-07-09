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

public class Pos_CostTypeManager
{
	public Pos_CostTypeManager()
	{
	}

    public static List<Pos_CostType> GetAllPos_CostTypes()
    {
        List<Pos_CostType> pos_CostTypes = new List<Pos_CostType>();
        SqlPos_CostTypeProvider sqlPos_CostTypeProvider = new SqlPos_CostTypeProvider();
        pos_CostTypes = sqlPos_CostTypeProvider.GetAllPos_CostTypes();
        return pos_CostTypes;
    }


    public static Pos_CostType GetPos_CostTypeByID(int id)
    {
        Pos_CostType pos_CostType = new Pos_CostType();
        SqlPos_CostTypeProvider sqlPos_CostTypeProvider = new SqlPos_CostTypeProvider();
        pos_CostType = sqlPos_CostTypeProvider.GetPos_CostTypeByID(id);
        return pos_CostType;
    }


    public static int InsertPos_CostType(Pos_CostType pos_CostType)
    {
        SqlPos_CostTypeProvider sqlPos_CostTypeProvider = new SqlPos_CostTypeProvider();
        return sqlPos_CostTypeProvider.InsertPos_CostType(pos_CostType);
    }


    public static bool UpdatePos_CostType(Pos_CostType pos_CostType)
    {
        SqlPos_CostTypeProvider sqlPos_CostTypeProvider = new SqlPos_CostTypeProvider();
        return sqlPos_CostTypeProvider.UpdatePos_CostType(pos_CostType);
    }

    public static bool DeletePos_CostType(int pos_CostTypeID)
    {
        SqlPos_CostTypeProvider sqlPos_CostTypeProvider = new SqlPos_CostTypeProvider();
        return sqlPos_CostTypeProvider.DeletePos_CostType(pos_CostTypeID);
    }
}
