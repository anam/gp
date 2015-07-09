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

public class Pos_FabricsTypeManager
{
	public Pos_FabricsTypeManager()
	{
	}

    public static List<Pos_FabricsType> GetAllPos_FabricsTypes()
    {
        List<Pos_FabricsType> pos_FabricsTypes = new List<Pos_FabricsType>();
        SqlPos_FabricsTypeProvider sqlPos_FabricsTypeProvider = new SqlPos_FabricsTypeProvider();
        pos_FabricsTypes = sqlPos_FabricsTypeProvider.GetAllPos_FabricsTypes();
        return pos_FabricsTypes;
    }


    public static Pos_FabricsType GetPos_FabricsTypeByID(int id)
    {
        Pos_FabricsType pos_FabricsType = new Pos_FabricsType();
        SqlPos_FabricsTypeProvider sqlPos_FabricsTypeProvider = new SqlPos_FabricsTypeProvider();
        pos_FabricsType = sqlPos_FabricsTypeProvider.GetPos_FabricsTypeByID(id);
        return pos_FabricsType;
    }


    public static int InsertPos_FabricsType(Pos_FabricsType pos_FabricsType)
    {
        SqlPos_FabricsTypeProvider sqlPos_FabricsTypeProvider = new SqlPos_FabricsTypeProvider();
        return sqlPos_FabricsTypeProvider.InsertPos_FabricsType(pos_FabricsType);
    }


    public static bool UpdatePos_FabricsType(Pos_FabricsType pos_FabricsType)
    {
        SqlPos_FabricsTypeProvider sqlPos_FabricsTypeProvider = new SqlPos_FabricsTypeProvider();
        return sqlPos_FabricsTypeProvider.UpdatePos_FabricsType(pos_FabricsType);
    }

    public static bool DeletePos_FabricsType(int pos_FabricsTypeID)
    {
        SqlPos_FabricsTypeProvider sqlPos_FabricsTypeProvider = new SqlPos_FabricsTypeProvider();
        return sqlPos_FabricsTypeProvider.DeletePos_FabricsType(pos_FabricsTypeID);
    }
}
