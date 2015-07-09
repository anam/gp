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
using System.Collections.Generic;

public partial class AdminPos_ProductDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadACC_ChartOfAccountLabel4();
            loadPos_Color();
            loadPos_Size();
            loadProductStatus();
            initialLoad();
        }
    }

    private void initialLoad()
    {
        txtDate.Text = DateTime.Today.ToString("dd MMM yyyy");
        int transactionTypeID = int.Parse(Request.QueryString["TransactionTypeID"]);

        if (transactionTypeID == 10)
        {
            ddlWorkStation.SelectedValue = "1";
            ddlWorkStation.Enabled = false;
        }

        switch (transactionTypeID)
        {
            case 10: //Issue Return
                ddlWorkStation.SelectedValue = "1";
                ddlWorkStation.Enabled = false;
                break;

            case 23: //Adjustment +
                ddlWorkStation.Enabled = false;
                break;
            case 24: //Adjustment +
                ddlWorkStation.Enabled = false;
                break;
            default:
                break;
        }

        try
        {
            lblVoucherType.Text = Pos_TransactionTypeManager.GetPos_TransactionTypeByID(transactionTypeID).TransactionTypeName;
        }
        catch(Exception ex)
        {
            lblVoucherType.Text = "Product Search";
        }
    }

    private void loadACC_ChartOfAccountLabel4()
    {
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlProduct.Items.Add(new ListItem("Any Product", "0"));
        ddlWorkStation.Items.Add(new ListItem("Select WorkStation", "0"));
        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 3)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlProduct.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1
                &&
                (
                    aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.Contains("Head")
                    ||
                    aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.Contains("Show")
                )
                )
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlWorkStation.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1
                &&
                (
                    aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.Contains("Head")
                    ||
                    aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.Contains("Show")
                )
                )
            {
                //if (aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.Contains("Show"))
                //{
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlWorkStationSearch.Items.Add(item);
                //}
            }

        }

        if (int.Parse(Request.QueryString["TransactionTypeID"]) != 0)
        {
            if (getLogin().ExtraField5 != "1")
            {
                ddlWorkStationSearch.SelectedValue = getLogin().ExtraField5;
                ddlWorkStationSearch.Enabled = false;
            }
            else
            {
                ddlWorkStationSearch.Enabled = true;
            }
        }
        else
        {
            ddlWorkStationSearch.Enabled = true;
            UpdatePanel2.Visible = false;
        }
    }
    
    private void loadProductStatus()
    {
        ListItem li = new ListItem("Any Status", "0");
        ddlPos_ProductStatus.Items.Add(li);

        List<Pos_ProductStatus> productStatuss = new List<Pos_ProductStatus>();
        productStatuss = Pos_ProductStatusManager.GetAllPos_ProductStatuss();
        foreach (Pos_ProductStatus productStatus in productStatuss)
        {
            ListItem item = new ListItem(productStatus.ProductStatusName.ToString(), productStatus.Pos_ProductStatusID.ToString());
            ddlPos_ProductStatus.Items.Add(item);
        }
    }
    private void loadPos_Size()
    {
        ListItem li = new ListItem("Any Size", "0");
        ddlPos_Size.Items.Add(li);

        List<Pos_Size> pos_Sizes = new List<Pos_Size>();
        pos_Sizes = Pos_SizeManager.GetAllPos_Sizes();
        foreach (Pos_Size pos_Size in pos_Sizes)
        {
            ListItem item = new ListItem(pos_Size.SizeName.ToString(), pos_Size.Pos_SizeID.ToString());
            ddlPos_Size.Items.Add(item);
        }
    }
    
    private void loadPos_Color()
    {
        ListItem li = new ListItem("Any Color...", "0");
        ddlColorSearch.Items.Add(li);

        List<Pos_Color> pos_Colors = new List<Pos_Color>();
        pos_Colors = Pos_ColorManager.GetAllPos_Colors();
        foreach (Pos_Color pos_Color in pos_Colors)
        {
            ListItem item = new ListItem(pos_Color.ColorName.ToString(), pos_Color.Pos_ColorID.ToString());
            ddlColorSearch.Items.Add(item);
        }
    }

    private void showPos_ProductGrid()
    {
        gvPos_Product.DataSource = Pos_ProductManager.GetAllPos_Products();
        gvPos_Product.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!IsEmptySearch())
        {
            processBarCodeForSearch();
            doSearch(false);
        }       
    }

    private void doSearch(bool IsNull)
    {
        string sql = processSQLString(IsNull);

        DataSet ds = CommonManager.SQLExec(sql);

        gvPos_Product.DataSource = ds.Tables[0];
        gvPos_Product.DataBind();

        loadQyantity();

        decimal totalStock = 0;
        decimal totalAmount = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            totalAmount += decimal.Parse(dr["StockSalePrice"].ToString());
            totalStock += decimal.Parse(dr["Stock"].ToString());
        }

        try
        {
            ((Label)gvPos_Product.FooterRow.FindControl("lblStockFooter")).Text = totalStock.ToString("0,0");
            ((Label)gvPos_Product.FooterRow.FindControl("lblStockSalePriceFooter")).Text = totalAmount.ToString("0,0.00");
        }
        catch (Exception ex)
        { }
        txtBarCode.Focus();
    }

    private void loadQyantity()
    {
        if (hfBarcodeSearch.Value == "" && gvPos_Product.Rows.Count >0)
        {
            foreach (GridViewRow gvr in gvPos_Product.Rows)
            {
                Label lblBarCode = (Label)gvr.FindControl("lblBarCode");
                TextBox txtQty = (TextBox)gvr.FindControl("txtQty");
                txtQty.Text = "0";
            }
            return;
        }

        foreach (GridViewRow gvr in gvPos_Product.Rows)
        {
            Label lblBarCode = (Label)gvr.FindControl("lblBarCode");
            Label lblStock = (Label)gvr.FindControl("lblStock");
            TextBox txtQty = (TextBox)gvr.FindControl("txtQty");

            if (hfBarcodeSearch.Value.Contains(','))
            {                
                foreach (string barcodeNqty in hfBarcodeSearch.Value.Split(','))
                {
                    if (barcodeNqty.Replace("'", "#").Split('#')[1] == lblBarCode.Text)
                    {
                        if (decimal.Parse(lblStock.Text) >= decimal.Parse(barcodeNqty.Split('-')[1]))
                            txtQty.Text = barcodeNqty.Split('-')[1];
                        else
                        {
                            txtQty.Text = decimal.Parse(lblStock.Text).ToString("0");
                        } 
                        break;
                    }
                }
            }
            else //for same product
            {
                if (decimal.Parse(lblStock.Text) >= decimal.Parse(hfBarcodeSearch.Value.Split('-')[1]))
                    txtQty.Text = hfBarcodeSearch.Value.Split('-')[1];
                else
                {
                    txtQty.Text = decimal.Parse(lblStock.Text).ToString("0");
                }
            }
        }
    }

    private string processSQLString(bool isNull)
    {
        string sql = @"SELECT [Pos_Product].[Pos_ProductID]
      ,[Pos_Product].[ProductID]
      ,[Pos_Product].[ReferenceID]
      ,[Pos_Product].[Pos_ProductTypeID]
      ,[Pos_Product].[Inv_UtilizationDetailsIDs]
      ,[Pos_Product].[ProductStatusID]
      ,[Pos_Product].[DesignCode]
      ,[Pos_Product].[Pos_SizeID]
      ,[Pos_Product].[Pos_BrandID]
      ,[Pos_Product].[Inv_QuantityUnitID]
      ,[Pos_Product].[FabricsCost]
      ,[Pos_Product].[AccesoriesCost]
      ,[Pos_Product].[Overhead]
      ,[Pos_Product].[OthersCost]
      ,[Pos_Product].[PurchasePrice]
      ,[Pos_Product].[SalePrice]
      ,[Pos_Product].[OldSalePrice]
      ,[Pos_Product].[Note]
      ,[Pos_Product].[BarCode]
      ,[Pos_Product].[Pos_ColorID]
      ,[Pos_Product].[Pos_FabricsTypeID]
      ,[Pos_Product].[StyleCode]
      ,[Pos_Product].[Pic1]
      ,[Pos_Product].[Pic2]
      ,[Pos_Product].[Pic3]
      ,[Pos_Product].[VatPercentage]
      ,[Pos_Product].[IsVatExclusive]
      ,[Pos_Product].[DiscountPercentage]
      ,[Pos_Product].[DiscountAmount]
      ,[Pos_Product].[FabricsNo]
      ,Pos_WorkStationStock.Stock
      ,(Pos_WorkStationStock.Stock *[Pos_Product].[SalePrice]) as StockSalePrice
      ,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text as ProductName
      ,Inv_QuantityUnit.QuantityUnitName
      ,cast(([Pos_Product].[FabricsCost]
      +[Pos_Product].[AccesoriesCost]
      +[Pos_Product].[Overhead]
      +[Pos_Product].[OthersCost]
      +[Pos_Product].[PurchasePrice]) as nvarchar(256)) as Cost
      ,Pos_Color.ColorName
      ,Pos_Size.SizeName
      ,Pos_ProductType.ProductTypeName
      ,Pos_ProductStatus.ProductStatusName
      ,Pos_Brand.BrandName
      ,[Pos_Product].[ExtraField10]
      ,[Pos_Product].[AddedBy]
      ,[Pos_Product].[AddedDate]
      ,[Pos_Product].[UpdatedBy]
      ,[Pos_Product].[UpdatedDate]
      ,[Pos_Product].[RowStatusID]
  FROM [Pos_Product]
  inner join Pos_WorkStationStock on [Pos_Product].Pos_ProductID = Pos_WorkStationStock.ProductID
  inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID=Pos_Product.ProductID
  inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID = Pos_Product.Inv_QuantityUnitID
  inner join Pos_Color on Pos_Color.Pos_ColorID = Pos_Product.Pos_ColorID
  inner join Pos_Size on Pos_Size.Pos_SizeID = Pos_Product.Pos_SizeID
  inner join Pos_ProductType on Pos_ProductType.Pos_ProductTypeID = Pos_Product.Pos_ProductTypeID
  inner join Pos_ProductStatus on Pos_ProductStatus.Pos_ProductStatusID = Pos_Product.ProductStatusID
  inner join Pos_Brand on Pos_Brand.Pos_BrandID = Pos_Product.Pos_BrandID
where ";

        if (isNull)
        {
            sql += "1=0";
            return sql;
        }

        sql += @"Pos_WorkStationStock.Stock >0 and WorkStationID = " + ddlWorkStationSearch.SelectedValue + @"
            "; 
        if (ddlProduct.SelectedValue != "0")
        {
            sql += " and [Pos_Product].ProductID=" + ddlProduct.SelectedValue;
        }

        if (txtDesignCode.Text != "")
        {
            sql += " and [Pos_Product].DesignCode=" + txtDesignCode.Text;
        }

        if (hfBarcodeSearch.Value != "")
        {
            sql += " and [Pos_Product].BarCode in (" + processBarcodeForSearch(hfBarcodeSearch.Value) + ")";
        }

        if (ddlPos_Size.SelectedValue != "0")
        {
            sql += " and [Pos_Product].Pos_SizeID=" + ddlPos_Size.SelectedValue;
        }


        if (ddlColorSearch.SelectedValue != "0")
        {
            sql += " and [Pos_Product].Pos_ColorID=" + ddlColorSearch.SelectedValue;
        }


        if (ddlPos_ProductStatus.SelectedValue != "0")
        {
            sql += " and [Pos_Product].ProductStatusID=" + ddlPos_ProductStatus.SelectedValue;
        }



        sql += " order by ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,Pos_Size.SizeName,[Pos_Product].[BarCode],Pos_Color.ColorName";

        return sql;
    }

    private string processBarcodeForSearch(string jsutFornoting)
    {
        string ids = "";
        if (hfBarcodeSearch.Value.Contains(','))
        {

            foreach (string barcodeNqty in hfBarcodeSearch.Value.Split(','))
            {
                ids += (ids == "" ? "" : ",") + barcodeNqty.Split('-')[0];
            }
        }
        else //for same product
        {
            ids = hfBarcodeSearch.Value.Split('-')[0];
        }

        return ids;
    }

    private void processBarCodeForSearch()
    {
        if (txtBarCode.Text != "")
        {
            txtBarCode.Text = txtBarCode.Text.Trim();
            
            if (txtBarCode.Text.Substring(txtBarCode.Text.Length - 1, 1) != ",")
            {
                txtBarCode.Text += ",";
            }

            

            foreach (string barcodes in txtBarCode.Text.Split(','))
            {
                if(barcodes!="")
                {
                    string barcode_Trimed = barcodes.Trim();
                    if (!hfBarcodeSearch.Value.Contains("'" + barcode_Trimed + "'"))
                    {
                        hfBarcodeSearch.Value += (hfBarcodeSearch.Value == "" ? "" : ",") + "'" + barcode_Trimed + "'-1";
                    }
                    else
                    {
                        if (hfBarcodeSearch.Value.Contains(','))
                        {
                            string tmp = "";

                            foreach (string barcodeNqty in hfBarcodeSearch.Value.Split(','))
                            {
                                if (barcodeNqty.Replace("'", "#").Split('#')[1] == barcode_Trimed)
                                {
                                    tmp += (tmp == "" ? "" : ",") + "'" + barcodeNqty.Replace("'", "#").Split('#')[1] + "'-" + (int.Parse(barcodeNqty.Split('-')[1]) + 1).ToString();
                                }
                                else
                                {
                                    tmp += (tmp == "" ? "" : ",") + barcodeNqty;
                                }
                            }
                            hfBarcodeSearch.Value = tmp;
                        }
                        else //for same product
                        {
                            hfBarcodeSearch.Value = hfBarcodeSearch.Value.Split('-')[0] + "-" + (int.Parse(hfBarcodeSearch.Value.Split('-')[1]) + 1).ToString();
                        }
                    }
                }
            }
        }
        else
        {
            hfBarcodeSearch.Value="";
        }
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        hfBarcodeSearch.Value = "";
        txtBarCode.Text = "";
        doSearch(true);
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
    private string GetTransactionMasterID()
    {
        txtOldTransactionID.Text = txtOldTransactionID.Text.Trim();

        string transactionMasterID = "0";
        string sqlTransaction = "select Pos_TransactionMasterID from Pos_TransactionMaster where TransactionID=" + txtOldTransactionID.Text
                   + " and Pos_TransactionTypeID =" + Request.QueryString["TransactionTypeID"] + ";";

        DataSet ds = CommonManager.SQLExec(sqlTransaction);

        if (ds.Tables[0].Rows.Count == 0)
        {
            return "0";
        }
        else
        {
            transactionMasterID = ds.Tables[0].Rows[0][0].ToString();
        }

        return transactionMasterID;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int transactionTypeID = int.Parse(Request.QueryString["TransactionTypeID"]);

        int TransactionMasterID = 0;

        bool IsIssueReturn = false;
        bool isTransferToanotherShowroom = false;
        bool isAdjustment = false;
        string Pending = "Pending";
        switch (transactionTypeID)
        {
            case 10: //Issue Return
                IsIssueReturn = true;
                break;

            case 11: //Transfer to another showroom
                isTransferToanotherShowroom = true;
                break;

            case 23: //Adjustment +
                isAdjustment = true;
                Pending = "";
                break;
            case 24: //Adjustment +
                isAdjustment = true;
                Pending = "";
                break;
            default:
                break;
        }

        if (txtOldTransactionID.Text != "")
        {
            if (txtOldTransactionID.Text  != "0")
            {
                TransactionMasterID = int.Parse(GetTransactionMasterID());
            }
            else
            {
                TransactionMasterID = int.Parse(txtOldTransactionID.Text);
            }

            if (TransactionMasterID == 0)
            {
                showAlartMessage("Wrong old ID");
                return;
            }
        }
        else
        {
            Pos_TransactionMaster pos_TransactionMaster = new Pos_TransactionMaster();
            Pos_TransactionMaster pos_TransactionMasterOposite = new Pos_TransactionMaster();

            pos_TransactionMaster.TransactionDate = DateTime.Parse(txtDate.Text);
            pos_TransactionMaster.TransactionID = 0;
            pos_TransactionMaster.Pos_TransactionTypeID = transactionTypeID;
            pos_TransactionMaster.ToOrFromID =(isAdjustment?int.Parse(ddlWorkStationSearch.SelectedValue): int.Parse(ddlWorkStation.SelectedValue));
            pos_TransactionMaster.Record = "";
            pos_TransactionMaster.Particulars = "";
            pos_TransactionMaster.WorkSatationID = int.Parse(ddlWorkStationSearch.SelectedValue);
            pos_TransactionMaster.ExtraField1 = "";
            pos_TransactionMaster.ExtraField2 = "";
            pos_TransactionMaster.ExtraField3 = "";
            pos_TransactionMaster.ExtraField4 = "";
            pos_TransactionMaster.ExtraField5 = Pending;
            pos_TransactionMaster.AddedBy = getLogin().LoginID;
            pos_TransactionMaster.AddedDate = DateTime.Now;
            pos_TransactionMaster.UpdatedBy = getLogin().LoginID;
            pos_TransactionMaster.UpdatedDate = DateTime.Now;
            pos_TransactionMaster.RowStatusID = 1;
            if (isTransferToanotherShowroom)
            {
                TransactionMasterID = Pos_TransactionMasterManager.InsertPos_TransactionMaster(pos_TransactionMaster,true);
            }
            else
            {
                TransactionMasterID = Pos_TransactionMasterManager.InsertPos_TransactionMaster(pos_TransactionMaster);
            }

            pos_TransactionMaster = Pos_TransactionMasterManager.GetPos_TransactionMasterByID(TransactionMasterID);
            
            txtOldTransactionID.Text = pos_TransactionMaster.TransactionID.ToString();
        }

        string sql = "Declare @Count int; ";

        foreach (GridViewRow gvr in gvPos_Product.Rows)
        {
            HiddenField hfPos_ProductID = (HiddenField)gvr.FindControl("hfPos_ProductID");
            TextBox txtQty = (TextBox)gvr.FindControl("txtQty");

            if (txtQty.Text == "")
                continue;
            if (decimal.Parse(txtQty.Text) == 0)
                continue;

            Pos_Transaction pos_Transaction = new Pos_Transaction();

            pos_Transaction.Pos_ProductID = Int32.Parse(hfPos_ProductID.Value);
            pos_Transaction.Quantity = Decimal.Parse(txtQty.Text);
            pos_Transaction.Pos_ProductTrasactionTypeID = transactionTypeID;
            pos_Transaction.Pos_ProductTransactionMasterID = TransactionMasterID;
            pos_Transaction.WorkStationID = Int32.Parse(ddlWorkStationSearch.SelectedValue);
            pos_Transaction.ExtraField1 = "";
            pos_Transaction.ExtraField2 = "";
            pos_Transaction.ExtraField3 = "";
            pos_Transaction.ExtraField4 = "";
            pos_Transaction.ExtraField5 = "";
            pos_Transaction.AddedBy = getLogin().LoginID;
            pos_Transaction.AddedDate = DateTime.Now;
            pos_Transaction.UpdatedBy = getLogin().LoginID;
            pos_Transaction.UpdatedDate = DateTime.Now;
            pos_Transaction.RowStatusID = 1;

            if (isTransferToanotherShowroom)
            {
                Pos_TransactionManager.InsertPos_TransactionWithOpositeEntry(pos_Transaction);
            }
            else
            {
                Pos_TransactionManager.InsertPos_Transaction(pos_Transaction);
            }



            if (IsIssueReturn)
            {
                Pos_TransactionType transactionType = Pos_TransactionTypeManager.GetPos_TransactionTypeByID(transactionTypeID);

                if (transactionType.CentralStockFormula != "0")
                {
                    //sql += "Update Pos_Product set ExtraField1 =(cast(ExtraField1 as decimal(10,2)) + ((" + transactionType.CentralStockFormula + ")*" + txtQty.Text + ")) where Pos_ProductID=" + pos_Transaction.Pos_ProductID.ToString() + ";";
                }

                if (transactionType.ShowRoomFormula != "0")
                {
                    sql += @"
                            set @Count=
                            (
                            select COUNT(*) from Pos_WorkStationStock
                            where ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @" and WorkStationID="+ddlWorkStationSearch.SelectedValue+@"
                            )

                            if @Count = 0
                            BEGIN
	                            INSERT INTO [Pos_WorkStationStock]
                                       ([WorkStationID]
                                       ,[ProductID]
                                       ,[Stock])
                                       VALUES(" + ddlWorkStationSearch.SelectedValue + @"," + pos_Transaction.Pos_ProductID.ToString() + @"," + txtQty.Text + @");
                            END
                            ELSE
                            BEGIN
                                Update Pos_WorkStationStock set Stock += ((" + transactionType.ShowRoomFormula + @")*" + txtQty.Text + @") where  WorkStationID=" + ddlWorkStationSearch.SelectedValue + @" and ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @";
                            END;";
                }
            }

            if (isTransferToanotherShowroom)
            {
                Pos_TransactionType transactionType = Pos_TransactionTypeManager.GetPos_TransactionTypeByID(transactionTypeID);
                
                if (transactionType.ShowRoomFormula != "0")
                {
                    sql += @"/*
                            set @Count=
                            (
                            select COUNT(*) from Pos_WorkStationStock
                            where ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @" and WorkStationID=" + ddlWorkStationSearch.SelectedValue + @"
                            )

                            if @Count = 0
                            BEGIN
	                            INSERT INTO [Pos_WorkStationStock]
                                       ([WorkStationID]
                                       ,[ProductID]
                                       ,[Stock])
                                       VALUES(" + ddlWorkStationSearch.SelectedValue + @"," + pos_Transaction.Pos_ProductID.ToString() + @"," + txtQty.Text + @");
                            END
                            ELSE
                            BEGIN
                                Update Pos_WorkStationStock set Stock += ((+1)*" + txtQty.Text + @") where  WorkStationID=" + ddlWorkStationSearch.SelectedValue + @" and  ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @";
                            END;
                            */
                                Update Pos_WorkStationStock set Stock += ((-1)*" + txtQty.Text + @") where WorkStationID=" + ddlWorkStationSearch.SelectedValue + @" and  ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @";

                            ";
                }
            }

            if (isAdjustment)
            {
                Pos_TransactionType transactionType = Pos_TransactionTypeManager.GetPos_TransactionTypeByID(transactionTypeID);

                if (transactionType.ShowRoomFormula != "0")
                {
                    sql += @"
                            set @Count=
                            (
                            select COUNT(*) from Pos_WorkStationStock
                            where ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @" and WorkStationID=" + ddlWorkStationSearch.SelectedValue + @"
                            )

                            if @Count = 0
                            BEGIN
	                            INSERT INTO [Pos_WorkStationStock]
                                       ([WorkStationID]
                                       ,[ProductID]
                                       ,[Stock])
                                       VALUES(" + ddlWorkStationSearch.SelectedValue + @"," + pos_Transaction.Pos_ProductID.ToString() + @"," + txtQty.Text + @");
                            END
                            ELSE
                            BEGIN
                                Update Pos_WorkStationStock set Stock += ((" + (transactionTypeID==23?"":"-" )+ @"1)*" + txtQty.Text + @") where  WorkStationID=" + ddlWorkStationSearch.SelectedValue + @" and  ProductID=" + pos_Transaction.Pos_ProductID.ToString() + @";
                            END;
                            ";
                }
            }
        }

        CommonManager.SQLExec(sql);
        btnSearch_Click(this, new EventArgs());
        hlnkProductionPrint.Visible = true;
        hlnkProductionPrint.NavigateUrl = "TransactionPrint.aspx?Pos_TransactionMasterID=" + TransactionMasterID.ToString();
        doSearch(true);
    }
    protected void txtBarCode_TextChanged(object sender, EventArgs e)
    {
        btnSearch_Click(this, new EventArgs());
        if (!chkComaSystem.Checked)
        {
            txtBarCode.Text = "";
        }
    }

    private bool IsEmptySearch()
    {
        bool IsEmptySearchValue = true;
        if (ddlProduct.SelectedValue != "0")
        {
            IsEmptySearchValue = false;
        }

        if (txtDesignCode.Text != "")
        {
            IsEmptySearchValue = false;

        }

        if (txtBarCode.Text != "")
        {
            IsEmptySearchValue = false;
        }

        if (ddlPos_Size.SelectedValue != "0")
        {
            IsEmptySearchValue = false;
        }


        if (ddlColorSearch.SelectedValue != "0")
        {
            IsEmptySearchValue = false;
        }


        if (ddlPos_ProductStatus.SelectedValue != "0")
        {
            IsEmptySearchValue = false;
        }

        return IsEmptySearchValue;
    }
}
