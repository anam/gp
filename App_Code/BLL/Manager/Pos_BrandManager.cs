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

public class Pos_BrandManager
{
	public Pos_BrandManager()
	{
	}

    public static List<Pos_Brand> GetAllPos_Brands()
    {
        List<Pos_Brand> pos_Brands = new List<Pos_Brand>();
        SqlPos_BrandProvider sqlPos_BrandProvider = new SqlPos_BrandProvider();
        pos_Brands = sqlPos_BrandProvider.GetAllPos_Brands();
        return pos_Brands;
    }


    public static Pos_Brand GetPos_BrandByID(int id)
    {
        Pos_Brand pos_Brand = new Pos_Brand();
        SqlPos_BrandProvider sqlPos_BrandProvider = new SqlPos_BrandProvider();
        pos_Brand = sqlPos_BrandProvider.GetPos_BrandByID(id);
        return pos_Brand;
    }


    public static int InsertPos_Brand(Pos_Brand pos_Brand)
    {
        SqlPos_BrandProvider sqlPos_BrandProvider = new SqlPos_BrandProvider();
        return sqlPos_BrandProvider.InsertPos_Brand(pos_Brand);
    }


    public static bool UpdatePos_Brand(Pos_Brand pos_Brand)
    {
        SqlPos_BrandProvider sqlPos_BrandProvider = new SqlPos_BrandProvider();
        return sqlPos_BrandProvider.UpdatePos_Brand(pos_Brand);
    }

    public static bool DeletePos_Brand(int pos_BrandID)
    {
        SqlPos_BrandProvider sqlPos_BrandProvider = new SqlPos_BrandProvider();
        return sqlPos_BrandProvider.DeletePos_Brand(pos_BrandID);
    }
}
