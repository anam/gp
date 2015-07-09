using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class Inventory_PurchasePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadLoginInHiddenField();
            initialLoad();
            loadData();
        }
    }
    private void showAlartMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "err_msg",
             "alert('" + message + "');",
             true);
    }

    private void loadLoginInHiddenField()
    {
        if (hfLoginID.Value == "")
        {
            hfLoginID.Value = getLogin().LoginID.ToString();
        }
    }

    private Login getLogin()
    {
        Login login = new Login();

        try
        {
            if (Session["Login"] != null)
            {
                login = (Login)Session["Login"];
            }
            else if (hfLoginID.Value != "")
            {
                login = LoginManager.GetLoginByID(int.Parse(hfLoginID.Value));
            }
            else
            { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }

        }
        catch (Exception ex)
        { }

        return login;
    }
    private void loadData()
    {
        string inventoryID = Request.QueryString["InventoryID"];
        string sql = @"
SELECT Pos_Inventory.[InventoryID]
      ,Pos_Inventory.[WorkStationID]
      ,ACC_ChartOfAccountLabel4.[ChartOfAccountLabel4Text]
      ,Pos_Inventory.[InventoryDate]
      ,Pos_Inventory.[AddedBy]
      ,Pos_Inventory.[AddedDate]
      ,Pos_Inventory.[ExtraField1]
      ,Pos_Inventory.[ExtraField2]
      ,Pos_Inventory.[ExtraField3]
      ,Pos_Inventory.[ExtraField4]
      ,Pos_Inventory.[ExtraField5]
  FROM [Pos_Inventory]
inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID=Pos_Inventory.WorkStationID
where InventoryID="+inventoryID+@"
";
        DataSet ds = CommonManager.SQLExec(sql);


        lblPurchaseDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["InventoryDate"].ToString()).ToString("yyyy-MM-dd");
        //lblParticulars.Text = ds.Tables[0].Rows[0]["InventoryDate"].ToString();
        lblTransactionID.Text = ds.Tables[0].Rows[0]["InventoryID"].ToString();// pos_TransactionMaster.TransactionID.ToString() + (pos_TransactionMaster.RowStatusID == 1 ? (Request.QueryString["Delete"] != null ? " (<a href='TransactionDelete.aspx?Pos_TransactionMasterID=" + Request.QueryString["Pos_TransactionMasterID"] + "'>X</a>)" : "") : "<b style='color:red;'>(Deleted)</b>");
        //lblRefferenceIDs.Text = pos_TransactionMaster.Record;
        lblToOrFromName.Text = ds.Tables[0].Rows[0]["ChartOfAccountLabel4Text"].ToString();

        lblExtraField5.Visible = false;
        //btnReceived.Visible = false;
       
        string NoOfcollum = "9";

        trRefference.Visible = false;
        trToOrFrom.Visible = false;

        lblVoucherType.Text = "Inventory";


        //Item Info
        List<Pos_Product> items = new List<Pos_Product>();
        items = Pos_ProductManager.GetAllPos_ProductsByInventoryID(int.Parse(inventoryID));

        string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Product Code</td>
                            <td>Product Name</td>
                            <td>Status</td>
                            <td>Quantity</td>
                            <td>Unit</td>
                            " + (NoOfcollum == "10" ? "<td>Cost</td><td>Total Cost</td>" : "")
                             + @"<td>Sale Price</td>
                            <td>VAT %</td>
                            <td>Amount</td>
                        </tr>";
        int lastRawMaterialID = 0;
        decimal Total = 0;
        decimal Subtotal = 0;
        decimal TotalAmount = 0;
        decimal SubtotalAmount = 0;
        decimal TotalAmountCost = 0;
        decimal SubtotalAmountCost = 0;
        int serialNo=1;
        foreach (Pos_Product item in items)
        {
            if (item.RowStatusID != 1) continue;
            if (lastRawMaterialID != 0 && lastRawMaterialID != item.ProductID)
            {
                htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td>" + Subtotal.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            " + (NoOfcollum == "10" ? "<td>&nbsp;</td><td>" + SubtotalAmountCost.ToString("0,0.00") + @"</td>" : "")
                             + @"<td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>" + SubtotalAmount.ToString("0,0.00") + @"</td>
                        </tr>";

                Subtotal = 0;
                SubtotalAmount = 0;
                SubtotalAmountCost = 0;
            }

            if (lastRawMaterialID != item.ProductID)
            {
                lastRawMaterialID = item.ProductID;
                htmlTable += @"<tr>
                            <td colspan='" + NoOfcollum + @"' style='padding-left:50px; border-top:1px solid black;border-bottom:1px solid black;font-weight:bold;'>
                                Item: " + item.ExtraField2 +@"
                            </td>
                        </tr>";
            }

            htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + (item.RowStatusID == 1 ? (Request.QueryString["Delete"] != null ? (" (<a href='TransactionDelete.aspx?InventoryDetailsID=" + item.ExtraField10 + "&InventoryID=" + Request.QueryString["InventoryID"] + "'>X</a>)") : "") : "<b style='color:red;'>(Deleted)</b>") + @"</td>
                            <td>" +item.BarCode+@"</td>
                            <td>" + item.ExtraField2 + @"</td>
                            <td>" + item.ExtraField8 + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(item.ExtraField1).ToString("0,0.00")+@"</td>
                            <td >" + item.ExtraField3 + @"</td>
                            " + (NoOfcollum == "10" ? "<td style='text-align:right;'>" + decimal.Parse(item.ExtraField4).ToString("0,0.00") + @"</td><td>" + (decimal.Parse(item.ExtraField1) * decimal.Parse(item.ExtraField4)).ToString("0,0.00") + @"</td>" : "")
                             + @"<td style='text-align:right;'>" + item.SalePrice.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + item.VatPercentage.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + ((decimal.Parse(item.ExtraField1) * item.SalePrice) + ((decimal.Parse(item.ExtraField1) * item.SalePrice)*item.VatPercentage/100)).ToString("0,0.00") + @"</td>
                        </tr>";

            Subtotal += decimal.Parse(item.ExtraField1);
            SubtotalAmount += ((decimal.Parse(item.ExtraField1) * item.SalePrice) + ((decimal.Parse(item.ExtraField1) * item.SalePrice) * item.VatPercentage / 100));
            SubtotalAmountCost += decimal.Parse(item.ExtraField1) * decimal.Parse(item.ExtraField4);

            Total += decimal.Parse(item.ExtraField1);
            TotalAmount += ((decimal.Parse(item.ExtraField1) * item.SalePrice) + ((decimal.Parse(item.ExtraField1) * item.SalePrice) * item.VatPercentage / 100));
            TotalAmountCost += decimal.Parse(item.ExtraField1) * decimal.Parse(item.ExtraField4);
        }

        htmlTable += @"<tr class='subtotalRow'>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Sub Total</td>
                        <td>" + Subtotal.ToString("0,0.00") + @"</td>
                        <td>&nbsp;</td>
                        " + (NoOfcollum == "10" ? "<td>&nbsp;</td><td>" + SubtotalAmountCost.ToString("0,0.00") + @"</td>" : "")
                             + @"<td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>" + SubtotalAmount.ToString("0,0.00") + @"</td>
                    </tr>";

        htmlTable += @"<tr id='lastRow'>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Grand Total</td>
                        <td>" + Total.ToString("0,0.00") + @"</td>
                        <td>&nbsp;</td>
                        " + (NoOfcollum == "10" ? "<td>&nbsp;</td><td>" + TotalAmountCost.ToString("0,0.00") + @"</td>" : "")
                             + @"<td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>" + TotalAmount.ToString("0,0.00") + @"</td>
                    </tr></table>";

        lblItemList.Text = htmlTable;
        
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
   
    private void SaveForRemoteDB(string sql,int toWorkStationID)
    {
        sql = Auto_SQL.add(
             sql //SQLString
           , "1"//Status
           , ConfigurationManager.AppSettings["WorkStationID"] //ForWorkStationID
           , toWorkStationID.ToString()  //[ToWorkStationID]
           , "0" //[FromID]
           , DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") //FromTime
           , ""//<UploadTime, nvarchar(256),>
           , ""//<ExecuteTime, nvarchar(256),>
           , ""//<ExtraField1, nvarchar(256),>
           , ""//<ExtraField2, nvarchar(256),>
           , ""//<ExtraField3, nvarchar(256),>
           , ""//<ExtraField4, nvarchar(256),>
           , ""//<ExtraField5, nvarchar(256),>
            );


        CommonManager.SQLExec(sql);
    }

}