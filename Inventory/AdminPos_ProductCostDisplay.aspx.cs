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

public partial class AdminPos_ProductCostDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showPos_ProductCostGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminPos_ProductCostInsertUpdate.aspx?pos_ProductCostID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminPos_ProductCostInsertUpdate.aspx?pos_ProductCostID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = Pos_ProductCostManager.DeletePos_ProductCost(Convert.ToInt32(linkButton.CommandArgument));
        showPos_ProductCostGrid();
    }

    private void showPos_ProductCostGrid()
    {
        String sql = @"select Pos_ProductCost.*,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
,Pos_CostType.CostTypeName
 from Pos_ProductCost 
inner join Pos_CostType on Pos_CostType.Pos_CostTypeID =Pos_ProductCost.Pos_CostTypeID
inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID=Pos_ProductCost.ProductID
order by ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
";

        gvPos_ProductCost.DataSource = CommonManager.SQLExec(sql).Tables[0]; //Pos_ProductCostManager.GetAllPos_ProductCosts();
        gvPos_ProductCost.DataBind();
    }
}
