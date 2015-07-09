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

public class Pos_ProductStatusManager
{
	public Pos_ProductStatusManager()
	{
	}

    public static List<Pos_ProductStatus> GetAllPos_ProductStatuss()
    {
        List<Pos_ProductStatus> pos_ProductStatuss = new List<Pos_ProductStatus>();
        SqlPos_ProductStatusProvider sqlPos_ProductStatusProvider = new SqlPos_ProductStatusProvider();
        pos_ProductStatuss = sqlPos_ProductStatusProvider.GetAllPos_ProductStatuss();
        return pos_ProductStatuss;
    }


    public static Pos_ProductStatus GetPos_ProductStatusByID(int id)
    {
        Pos_ProductStatus pos_ProductStatus = new Pos_ProductStatus();
        SqlPos_ProductStatusProvider sqlPos_ProductStatusProvider = new SqlPos_ProductStatusProvider();
        pos_ProductStatus = sqlPos_ProductStatusProvider.GetPos_ProductStatusByID(id);
        return pos_ProductStatus;
    }


    public static int InsertPos_ProductStatus(Pos_ProductStatus pos_ProductStatus)
    {
        SqlPos_ProductStatusProvider sqlPos_ProductStatusProvider = new SqlPos_ProductStatusProvider();
        return sqlPos_ProductStatusProvider.InsertPos_ProductStatus(pos_ProductStatus);
    }


    public static bool UpdatePos_ProductStatus(Pos_ProductStatus pos_ProductStatus)
    {
        SqlPos_ProductStatusProvider sqlPos_ProductStatusProvider = new SqlPos_ProductStatusProvider();
        return sqlPos_ProductStatusProvider.UpdatePos_ProductStatus(pos_ProductStatus);
    }

    public static bool DeletePos_ProductStatus(int pos_ProductStatusID)
    {
        SqlPos_ProductStatusProvider sqlPos_ProductStatusProvider = new SqlPos_ProductStatusProvider();
        return sqlPos_ProductStatusProvider.DeletePos_ProductStatus(pos_ProductStatusID);
    }
}
