using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class POS_ProductSearchByBarcode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = @"
Select ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text as Showroom,Pos_WorkStationStock.Stock as Qty from Pos_WorkStationStock
inner join Pos_Product on Pos_Product.Pos_ProductID= Pos_WorkStationStock.ProductID
inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID= Pos_WorkStationStock.WorkStationID
where Pos_Product.BarCode='"+txtBarCode.Text.Trim()+ @"';

Select 'Central Stock' as HeadOffice ,Pos_Product.ExtraField1 as Qty from Pos_Product
where Pos_Product.BarCode='" + txtBarCode.Text.Trim() + @"';
";

        DataSet ds = CommonManager.SQLExec(sql);

        gvProductSearchBranch.DataSource = ds.Tables[0];
        gvProductSearchBranch.DataBind();


        gvProductSearchCentral.DataSource = ds.Tables[1];
        gvProductSearchCentral.DataBind();
        
    }
}