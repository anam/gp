using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class AdminPos_BranchWiseProductStockCapacityDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showPos_BranchWiseProductStockCapacityGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminPos_BranchWiseProductStockCapacityInsertUpdate.aspx?pos_BranchWiseProductStockCapacityID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminPos_BranchWiseProductStockCapacityInsertUpdate.aspx?pos_BranchWiseProductStockCapacityID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = Pos_BranchWiseProductStockCapacityManager.DeletePos_BranchWiseProductStockCapacity(Convert.ToInt32(linkButton.CommandArgument));
        showPos_BranchWiseProductStockCapacityGrid();
    }

    private void showPos_BranchWiseProductStockCapacityGrid()
    {
        string sql = @"
SELECT     Pos_BranchWiseProductStockCapacity.Pos_BranchWiseProductStockCapacityID,  
                      Pos_BranchWiseProductStockCapacity.StockAmount, ACC_ChartOfAccountLabel4_1.ChartOfAccountLabel4Text as WorkStation, 
                      ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text AS ProductName
FROM         ACC_ChartOfAccountLabel4 INNER JOIN
                      Pos_BranchWiseProductStockCapacity ON 
                      ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID = Pos_BranchWiseProductStockCapacity.ProductID INNER JOIN
                      ACC_ChartOfAccountLabel4 AS ACC_ChartOfAccountLabel4_1 ON 
                      Pos_BranchWiseProductStockCapacity.WorkStationID = ACC_ChartOfAccountLabel4_1.ACC_ChartOfAccountLabel4ID
order by ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4_1.ChartOfAccountLabel4Text
";

        gvPos_BranchWiseProductStockCapacity.DataSource = CommonManager.SQLExec(sql).Tables[0];
        gvPos_BranchWiseProductStockCapacity.DataBind();
    }
}
