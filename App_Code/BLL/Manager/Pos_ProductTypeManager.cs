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

public class Pos_ProductTypeManager
{
	public Pos_ProductTypeManager()
	{
	}

    public static List<Pos_ProductType> GetAllPos_ProductTypes()
    {
        List<Pos_ProductType> pos_ProductTypes = new List<Pos_ProductType>();
        SqlPos_ProductTypeProvider sqlPos_ProductTypeProvider = new SqlPos_ProductTypeProvider();
        pos_ProductTypes = sqlPos_ProductTypeProvider.GetAllPos_ProductTypes();
        return pos_ProductTypes;
    }


    public static Pos_ProductType GetPos_ProductTypeByID(int id)
    {
        Pos_ProductType pos_ProductType = new Pos_ProductType();
        SqlPos_ProductTypeProvider sqlPos_ProductTypeProvider = new SqlPos_ProductTypeProvider();
        pos_ProductType = sqlPos_ProductTypeProvider.GetPos_ProductTypeByID(id);
        return pos_ProductType;
    }


    public static int InsertPos_ProductType(Pos_ProductType pos_ProductType)
    {
        SqlPos_ProductTypeProvider sqlPos_ProductTypeProvider = new SqlPos_ProductTypeProvider();
        return sqlPos_ProductTypeProvider.InsertPos_ProductType(pos_ProductType);
    }


    public static bool UpdatePos_ProductType(Pos_ProductType pos_ProductType)
    {
        SqlPos_ProductTypeProvider sqlPos_ProductTypeProvider = new SqlPos_ProductTypeProvider();
        return sqlPos_ProductTypeProvider.UpdatePos_ProductType(pos_ProductType);
    }

    public static bool DeletePos_ProductType(int pos_ProductTypeID)
    {
        SqlPos_ProductTypeProvider sqlPos_ProductTypeProvider = new SqlPos_ProductTypeProvider();
        return sqlPos_ProductTypeProvider.DeletePos_ProductType(pos_ProductTypeID);
    }
}
