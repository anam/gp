using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class POS_AllBarcodeList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql = @"
SELECT BarCode,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
      
  FROM [Pos_Product]
  inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID=Pos_Product.ProductID
  order by ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,BarCode
";

            GridView1.DataSource = CommonManager.SQLExec(sql).Tables[0];
            GridView1.DataBind();
        }
    }
}