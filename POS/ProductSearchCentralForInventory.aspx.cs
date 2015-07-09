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
            loadLoginInHiddenField();
            loadACC_ChartOfAccountLabel4();
            loadPos_Color();
            loadPos_Size();
            loadProductStatus();
            initialLoad();
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

    private void initialLoad()
    {
        txtDate.Text = DateTime.Today.ToString("dd MMM yyyy");
        

        try
        {
            lblVoucherType.Text = "Inventory";
            string sql = @"SELECT top 1 [InventoryID]
                                FROM [Pos_Inventory]
                                order by InventoryID desc";
            DataSet ds = CommonManager.SQLExec(sql);
            lblVoucherType.Text+="("+(ds.Tables[0].Rows.Count==0?"1":(int.Parse(ds.Tables[0].Rows[0][0].ToString())+1).ToString())+")";
        }
        catch (Exception ex)
        {
            lblVoucherType.Text = "Barcode Generate";
        }
    }

    private void loadACC_ChartOfAccountLabel4()
    {
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlProduct.Items.Add(new ListItem("Any Product", "0"));
        ddlWorkStation.Items.Add(new ListItem("Select WorkStation", "0"));
        ddlRefference.Items.Add(new ListItem("Select Ref", "0"));
        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            
            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 7) // share holder
            {

                item = new ListItem("00-" + aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlRefference.Items.Add(item);
            }

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

        }

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 4
                &&
                aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.Contains("H1")
                ) //Head Office
            {
                ListItem item = new ListItem();
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlRefference.Items.Add(item);
            }
        }


        //if (int.Parse(Request.QueryString["TransactionTypeID"]) == 0)
        //{
           
           // UpdatePanel2.Visible = false;
        //}
    }
    
    private void loadProductStatus()
    {
        ListItem li = new ListItem("Select Status", "0");
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
        ListItem li = new ListItem("Select Pos_Color...", "0");
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
        lblProductPreview.Text = processProductPreview(ds);

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
        { 
        }
        txtBarCode.Focus();
    }

    private string processProductPreview(DataSet ds)
    {
        string html = "<div style='overflow:scroll;height:250px;'><table width='100%' border='1' cellspacing='0' cellpadding='5'><tr><td width='20px' >Serial</td><td>BarCode</td><td>Product Name</td><td width='70px'>Qty</td><td width='70px'>Sub total</td><td width='70px'>Grand Total</td></tr>";

        int qtySubtotal = 0;
        int qtyGrandTotal = 0;
        int lastProductID = int.Parse(ds.Tables[0].Rows[0]["ProductID"].ToString());
        int rows=1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if ( dr["ProductID"].ToString() != lastProductID.ToString())
            {
                html += @"<tr><td colspan='4' style='text-align:right;'>Sub total</td><td style='text-align:right;'>" + decimal.Parse(qtySubtotal.ToString()).ToString("0,0") + "</td><td></td></tr>";
                qtySubtotal = 0;
                lastProductID = int.Parse(dr["ProductID"].ToString());
            }
            int qty = 0;
            
            foreach (GridViewRow gvr in gvPos_Product.Rows)
            {
                Label lblBarCode = (Label)gvr.FindControl("lblBarCode");
                Label lblStock=(Label)gvr.FindControl("lblStock");
                TextBox txtQty = (TextBox)gvr.FindControl("txtQty");
                if (lblBarCode.Text.ToString() == dr["BarCode"].ToString())
                {
                    try
                    {
                        qty = int.Parse(txtQty.Text);
                    }
                    catch (Exception ex)
                    { 
                    }

                    break;
                }
            }


            html += @"<tr><td>" + (rows++) + "</td><td>" + dr["BarCode"].ToString() + "</td><td>" + dr["ProductName"].ToString() + "</td><td style='text-align:right;'>" + qty + "</td><td></td><td></td></tr>";
            qtySubtotal += qty;
            qtyGrandTotal += qty;
        }

        html += @"<tr><td colspan='4' style='text-align:right;'>Sub total</td><td style='text-align:right;'>" + decimal.Parse(qtySubtotal.ToString()).ToString("0,0") + "</td><td></td></tr>";
        html += @"<tr><td colspan='5' style='text-align:right;'>Grand total</td><td style='text-align:right;'>" + decimal.Parse(qtyGrandTotal.ToString()).ToString("0,0") + "</td></tr></table></div>";
                

        return html;
    }

    private void loadQyantity()
    {
        if (hfBarcodeSearch.Value == "" && gvPos_Product.Rows.Count > 0)
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
            Label lblStock=(Label)gvr.FindControl("lblStock");
            TextBox txtQty = (TextBox)gvr.FindControl("txtQty");

            if (hfBarcodeSearch.Value.Contains(','))
            {
                foreach (string barcodeNqty in hfBarcodeSearch.Value.Split(','))
                {
                    if (barcodeNqty.Replace("'", "#").Split('#')[1].Trim() == lblBarCode.Text.Trim())
                    {
                        //if (decimal.Parse(lblStock.Text) >= decimal.Parse(barcodeNqty.Split('-')[1]))
                            txtQty.Text = barcodeNqty.Split('-')[1];
                        //else
                        //{
                        //    txtQty.Text = decimal.Parse(lblStock.Text).ToString("0");
                        //}
                        break;
                    }
                }
            }
            else //for same product
            {
                //if (decimal.Parse(lblStock.Text) >= decimal.Parse(hfBarcodeSearch.Value.Split('-')[1])) 
                    txtQty.Text = hfBarcodeSearch.Value.Split('-')[1];
                //else
                //{
                //    txtQty.Text = decimal.Parse(lblStock.Text).ToString("0");
                //}
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
              ,cast([Pos_Product].ExtraField1 as Decimal(10,2)) as Stock
              ,(cast([Pos_Product].ExtraField1 as Decimal(10,2)) *[Pos_Product].[SalePrice]) as StockSalePrice
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
          --inner join Pos_Transaction on [Pos_Product].Pos_ProductID = Pos_Transaction.Pos_ProductID
          inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID=Pos_Product.ProductID
          inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID = Pos_Product.Inv_QuantityUnitID
          inner join Pos_Color on Pos_Color.Pos_ColorID = Pos_Product.Pos_ColorID
          inner join Pos_Size on Pos_Size.Pos_SizeID = Pos_Product.Pos_SizeID
          inner join Pos_ProductType on Pos_ProductType.Pos_ProductTypeID = Pos_Product.Pos_ProductTypeID
          inner join Pos_ProductStatus on Pos_ProductStatus.Pos_ProductStatusID = Pos_Product.ProductStatusID
          inner join Pos_Brand on Pos_Brand.Pos_BrandID = Pos_Product.Pos_BrandID
        where 
" + (Request.QueryString["Barcode"] == null ? "-- cast([Pos_Product].ExtraField1 as decimal(10,2))>0 " : " 1=1 ") + @"
        ";

        if (ddlProduct.SelectedValue != "0")
        {
            sql += " and [Pos_Product].ProductID=" + ddlProduct.SelectedValue;
        }

        if (txtDesignCode.Text != "")
        {
            sql += " and [Pos_Product].DesignCode='" + txtDesignCode.Text.Trim() + "'";
        }

        if (hfBarcodeSearch.Value != "")
        {
            sql += @"-- and 
[Pos_Product].BarCode in (" + processBarcodeForSearch(hfBarcodeSearch.Value) + ")";
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
                if (barcodes != "")
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
            hfBarcodeSearch.Value = "";
        }
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        hfBarcodeSearch.Value = "";
        txtBarCode.Text = "";
        doSearch(true);
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
        if (ddlWorkStation.SelectedValue == "0")
        {
            showAlartMessage("Please select the Work Station");
            return;
        }
        
        if (gvPos_Product.Rows.Count == 0)
        {
            showAlartMessage("Plz add some product");
            return;
        }
        //foreach (GridViewRow gvr in gvPos_Product.Rows)
        //{
        //    Label lblBarCode = (Label)gvr.FindControl("lblBarCode");
        //    TextBox txtQty = (TextBox)gvr.FindControl("txtQty");
        //    Label lblStock = (Label)gvr.FindControl("lblStock");

        //}
        btnSubmit.Visible = false;


        
        string sql = @"Declare @InventoryID int;
        INSERT INTO [Pos_Inventory]
           ([WorkStationID]
           ,[InventoryDate]
           ,[AddedBy]
           ,[AddedDate]
           ,[ExtraField1]
           ,[ExtraField2]
           ,[ExtraField3]
           ,[ExtraField4]
           ,[ExtraField5])
     VALUES
           (
           "+ddlWorkStation.SelectedValue+@"--<WorkStationID, int,>
           ,'"+DateTime.Parse(txtDate.Text).ToString("yyyy-MM-dd hh:mm tt")+@"'--<InventoryDate, datetime,>
           ,"+getLogin().LoginID+ @"--<AddedBy, int,>
           ,Getdate()--<AddedDate, datetime,>
           ,''--<ExtraField1, nvarchar(256),>
           ,''--<ExtraField2, nvarchar(256),>
           ,''--<ExtraField3, nvarchar(256),>
           ,''--<ExtraField4, nvarchar(256),>
           ,''--<ExtraField5, nvarchar(256),>
           );
                    SET @InventoryID =SCOPE_IDENTITY();

        ";

        foreach (GridViewRow gvr in gvPos_Product.Rows)
        {
            HiddenField hfPos_ProductID = (HiddenField)gvr.FindControl("hfPos_ProductID");
            TextBox txtQty = (TextBox)gvr.FindControl("txtQty");
            Label lblStock = (Label)gvr.FindControl("lblStock");

            if (txtQty.Text == "")
                continue;
            if (decimal.Parse(txtQty.Text) == 0)
                continue;
            if (decimal.Parse(txtQty.Text) < 0)
                continue;

            sql += @"
                    INSERT INTO [Pos_InventoryDetails]
                               ([InventoryID]
                               ,[Pos_ProductID]
                                ,[Quantity]
                               ,[AddedBy]
                               ,[AddedDate]
                               ,[UpdatedBy]
                               ,[UpdatedDate],RowStatusID)
                         VALUES
                               (@InventoryID--<InventoryID, int,>
                               ," + hfPos_ProductID.Value + @"--<Pos_ProductID, int,>
                               ," + txtQty.Text.Trim() + @"--<Quantity, int,>
                               ," + getLogin().LoginID + @"--<AddedBy, int,>
                               ,Getdate()--<AddedDate, datetime,>
                               ," + getLogin().LoginID + @"--<UpdatedBy, int,>
                               ,Getdate()--<UpdatedDate, datetime,>
                               ,1);
                    ";
            
        }

        //saveForRemoteDatabase(TransactionMasterID);

        DataSet ds=  CommonManager.SQLExec(sql+";select @InventoryID");
        btnSearch_Click(this, new EventArgs());
        hlnkProductionPrint.Visible = true;
        hlnkProductionPrint.NavigateUrl = "TransactionPrint_inventory.aspx?InventoryID="+ds.Tables[0].Rows[0][0].ToString();

    }

    private void saveForRemoteDatabase(int TransactionMasterID)
    {
        string sql = "Select * from Pos_TransactionMaster where Pos_TransactionMasterID="+TransactionMasterID.ToString()+";";
        sql += "Select * from Pos_Transaction where Pos_ProductTransactionMasterID=" + TransactionMasterID + ";";

        DataSet ds = CommonManager.SQLExec(sql);
        sql = @"Declare @Pos_TransactionMasterID int";
        
        sql += @"
                INSERT INTO [Pos_TransactionMaster]
                           ([TransactionDate]
                           ,[Pos_TransactionTypeID]
                           ,[TransactionID]
                           ,[ToOrFromID]
                           ,[Record]
                           ,[Particulars]
                           ,[WorkSatationID]
                           ,[ExtraField1]
                           ,[ExtraField2]
                           ,[ExtraField3]
                           ,[ExtraField4]
                           ,[ExtraField5]
                           ,[AddedBy]
                           ,[AddedDate]
                           ,[UpdatedBy]
                           ,[UpdatedDate]
                           ,[RowStatusID])
                     VALUES
                           ('"+ds.Tables[0].Rows[0]["TransactionDate"].ToString()+@"'
                           ,"+ds.Tables[0].Rows[0]["Pos_TransactionTypeID"].ToString()+@"
                           ,"+ds.Tables[0].Rows[0]["TransactionID"].ToString()+@"
                           ,"+ds.Tables[0].Rows[0]["ToOrFromID"].ToString()+@"
                           ,'"+ds.Tables[0].Rows[0]["Record"].ToString()+@"'
                           ,'"+ds.Tables[0].Rows[0]["Particulars"].ToString()+@"'
                           ,"+ds.Tables[0].Rows[0]["WorkSatationID"].ToString()+@"
                           ,'"+ds.Tables[0].Rows[0]["ExtraField1"].ToString()+@"'
                           ,'"+ds.Tables[0].Rows[0]["ExtraField2"].ToString()+@"'
                           ,'"+ds.Tables[0].Rows[0]["ExtraField3"].ToString()+@"'
                           ,'"+ds.Tables[0].Rows[0]["ExtraField4"].ToString()+@"'
                           ,'"+ds.Tables[0].Rows[0]["ExtraField5"].ToString()+@"'
                           ,"+ds.Tables[0].Rows[0]["AddedBy"].ToString()+@"
                           ,'"+ds.Tables[0].Rows[0]["AddedDate"].ToString()+@"'
                           ,"+ds.Tables[0].Rows[0]["UpdatedBy"].ToString()+@"
                           ,'"+ds.Tables[0].Rows[0]["UpdatedDate"].ToString()+@"'
                           ,"+ds.Tables[0].Rows[0]["RowStatusID"].ToString()+ @");

                    SET @Pos_TransactionMasterID =SCOPE_IDENTITY();
                ";
        
        foreach (DataRow dr in ds.Tables[1].Rows)
        {
            sql += @"
                INSERT INTO [Pos_Transaction]
                           ([Pos_ProductID]
                           ,[Quantity]
                           ,[Pos_ProductTrasactionTypeID]
                           ,[Pos_ProductTransactionMasterID]
                           ,[WorkStationID]
                           ,[ExtraField1]
                           ,[ExtraField2]
                           ,[ExtraField3]
                           ,[ExtraField4]
                           ,[ExtraField5]
                           ,[AddedBy]
                           ,[AddedDate]
                           ,[UpdatedBy]
                           ,[UpdatedDate]
                           ,[RowStatusID])
                     VALUES
                           ("+dr["Pos_ProductID"].ToString()+@"
                           ,"+dr["Quantity"].ToString()+@"
                           ,"+dr["Pos_ProductTrasactionTypeID"].ToString()+ @"
                           ,@Pos_TransactionMasterID
                           ," + dr["WorkStationID"].ToString()+@"
                           ,'"+dr["ExtraField1"].ToString()+@"'
                           ,'"+dr["ExtraField2"].ToString()+@"'
                           ,'"+dr["ExtraField3"].ToString()+@"'
                           ,'"+dr["ExtraField4"].ToString()+@"'
                           ,'"+dr["ExtraField5"].ToString()+@"'
                           ,"+dr["AddedBy"].ToString()+@"
                           ,'"+dr["AddedDate"].ToString()+@"'
                           ,"+dr["UpdatedBy"].ToString()+@"
                           ,'"+dr["UpdatedDate"].ToString()+@"'
                           ,"+dr["RowStatusID"].ToString()+@");
                ";
        }

        sql = Auto_SQL.add(sql
            , "1"
            , ConfigurationManager.AppSettings["WorkStationID"]
            , ddlWorkStation.SelectedValue
            , "0"
            , DateTime.Now.ToString("yyyy-MM-dd hh:mm tt")
            , ""
            , ""
            , ""
            , ""
            , ""
            , ""
            , "");
        
        CommonManager.SQLExec(sql);
        
    }
    protected void btnGenerateBarcode_Click(object sender, EventArgs e)
    {
        string barcode = "";
        foreach (GridViewRow gvr in gvPos_Product.Rows)
        {
            HiddenField hfPos_ProductID = (HiddenField)gvr.FindControl("hfPos_ProductID");
            TextBox txtQty = (TextBox)gvr.FindControl("txtQty");

            if (txtQty.Text == "")
                continue;
            if (decimal.Parse(txtQty.Text) == 0)
                continue;

            Label lblBarCode = (Label)gvr.FindControl("lblBarCode");
            barcode +=(barcode==""?"":",")+ lblBarCode.Text.Trim()+"-"+txtQty.Text.Trim();
            
        }

        hlnkBarCodePrint.NavigateUrl = "BarCodeGeneratePrint.aspx?barcodes=" + barcode ;
        hlnkBarCodePrint.Visible = true;
    
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
