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

public class Pos_ColorManager
{
	public Pos_ColorManager()
	{
	}

    public static List<Pos_Color> GetAllPos_Colors()
    {
        List<Pos_Color> pos_Colors = new List<Pos_Color>();
        SqlPos_ColorProvider sqlPos_ColorProvider = new SqlPos_ColorProvider();
        pos_Colors = sqlPos_ColorProvider.GetAllPos_Colors();
        return pos_Colors;
    }


    public static Pos_Color GetPos_ColorByID(int id)
    {
        Pos_Color pos_Color = new Pos_Color();
        SqlPos_ColorProvider sqlPos_ColorProvider = new SqlPos_ColorProvider();
        pos_Color = sqlPos_ColorProvider.GetPos_ColorByID(id);
        return pos_Color;
    }


    public static int InsertPos_Color(Pos_Color pos_Color)
    {
        SqlPos_ColorProvider sqlPos_ColorProvider = new SqlPos_ColorProvider();
        return sqlPos_ColorProvider.InsertPos_Color(pos_Color);
    }


    public static bool UpdatePos_Color(Pos_Color pos_Color)
    {
        SqlPos_ColorProvider sqlPos_ColorProvider = new SqlPos_ColorProvider();
        return sqlPos_ColorProvider.UpdatePos_Color(pos_Color);
    }

    public static bool DeletePos_Color(int pos_ColorID)
    {
        SqlPos_ColorProvider sqlPos_ColorProvider = new SqlPos_ColorProvider();
        return sqlPos_ColorProvider.DeletePos_Color(pos_ColorID);
    }
}
