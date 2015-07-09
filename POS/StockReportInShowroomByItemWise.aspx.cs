using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Inventory_PurchasePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initialLoad();
            loadData();
        }
    }

    private void loadData()
    {
        lblStockDate.Text = DateTime.Today.ToString("dd/MM/yyyy") ;
        int workStationID = 0;
        try
        {
            workStationID = Int32.Parse(Request.QueryString["WorkStationID"]);
            lblStockDate.Text += "<br/>" + Request.QueryString["WorkStationName"];
        }
        catch (Exception ex)
        {
            workStationID = 0;
        }
        //purchase info
        string sql = @"SELECT 
Pos_Product.StyleCode as Style,Pos_Product.BarCode
,Pos_Size.SizeName as Size,Pos_Color.ColorName as Color
,Pos_Product.ProductName as Product
,Pos_WorkStationStock.[Stock]
,Pos_Product.SalePrice 
      ,Pos_WorkStationStock.[Stock] * Pos_Product.SalePrice as Amount
  FROM [Pos_WorkStationStock]
  inner join Pos_Product on Pos_Product.Pos_ProductID=Pos_WorkStationStock.ProductID
  inner join Pos_Size on Pos_Product.Pos_SizeID=Pos_Size.Pos_SizeID
  inner join Pos_Color on Pos_Product.Pos_ColorID=Pos_Color.Pos_ColorID
 " + (workStationID != 0 ? " where WorkStationID=" + workStationID.ToString() : "") + @"
order by Pos_Product.StyleCode,Pos_Product.BarCode
";

        DataSet ds = CommonManager.SQLExec(sql);
            
        gvItem.DataSource = ds.Tables[0];
        gvItem.DataBind();

    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvItem.Rows)
        {
            Label lblStock = (Label)gvr.FindControl("lblStock");
            TextBox txtInventory = (TextBox)gvr.FindControl("txtInventory");
            Label lblAdjustment = (Label)gvr.FindControl("lblAdjustment");
            try
            {
                lblAdjustment.Text = (int.Parse(txtInventory.Text) - int.Parse(lblStock.Text)).ToString();
            }
            catch (Exception ex)
            { }
        }
    }
}