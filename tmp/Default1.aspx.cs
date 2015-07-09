using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class tmp_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql = @"
SELECT TOP 1000 [Inv_ItemID]
      ,[ItemCode]
      ,[RawMaterialID]
      
  FROM [GentleParkHO_Old].[dbo].[Inv_Item]
  where --ItemCode= '02006007516' and
   [RawMaterialID]=41 and Inv_ItemID >=6233
";
            DataSet ds = CommonManager.SQLExec(sql);
            sql="";
            int id = 7516;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sql += @"
update Inv_Item set ItemCode='0200600" + (++id).ToString() + @"' where Inv_ItemID=" + dr["Inv_ItemID"].ToString()+";";
            }

            CommonManager.SQLExec(sql);
        }
    }
}