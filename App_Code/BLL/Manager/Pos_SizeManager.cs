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

public class Pos_SizeManager
{
	public Pos_SizeManager()
	{
	}

    public static List<Pos_Size> GetAllPos_Sizes()
    {
        List<Pos_Size> pos_Sizes = new List<Pos_Size>();
        SqlPos_SizeProvider sqlPos_SizeProvider = new SqlPos_SizeProvider();
        pos_Sizes = sqlPos_SizeProvider.GetAllPos_Sizes();
        return pos_Sizes;
    }


    public static Pos_Size GetPos_SizeByID(int id)
    {
        Pos_Size pos_Size = new Pos_Size();
        SqlPos_SizeProvider sqlPos_SizeProvider = new SqlPos_SizeProvider();
        pos_Size = sqlPos_SizeProvider.GetPos_SizeByID(id);
        return pos_Size;
    }


    public static int InsertPos_Size(Pos_Size pos_Size)
    {
        SqlPos_SizeProvider sqlPos_SizeProvider = new SqlPos_SizeProvider();
        return sqlPos_SizeProvider.InsertPos_Size(pos_Size);
    }


    public static bool UpdatePos_Size(Pos_Size pos_Size)
    {
        SqlPos_SizeProvider sqlPos_SizeProvider = new SqlPos_SizeProvider();
        return sqlPos_SizeProvider.UpdatePos_Size(pos_Size);
    }

    public static bool DeletePos_Size(int pos_SizeID)
    {
        SqlPos_SizeProvider sqlPos_SizeProvider = new SqlPos_SizeProvider();
        return sqlPos_SizeProvider.DeletePos_Size(pos_SizeID);
    }
}
