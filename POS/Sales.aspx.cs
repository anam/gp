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

    private void initialLoad()
    {
        //txtDate.Text = DateTime.Today.ToString("dd MMM yyyy");
        int transactionTypeID = int.Parse(Request.QueryString["TransactionTypeID"]);
        txtBackDatedSale.Text = DateTime.Today.ToString("dd MMM yyyy");
        if (transactionTypeID == 13)
        {
            UpdatePanel2.Visible = false;
            lblVoucherType.Text = "Sales";
        }
        else
        { 
        lblVoucherType.Text = "Sales Return Out";
        }

        
        try
        {
           string sql = "select case Count(TransactionID) when 0 then 1 Else (MAX(TransactionID)+1) end from Pos_TransactionMaster where Pos_TransactionTypeID=" + transactionTypeID.ToString() + " and WorkSatationID="+ddlWorkStationSearch.SelectedValue;
            lblVoucherType.Text += "(" + CommonManager.SQLExec(sql).Tables[0].Rows[0][0].ToString() + ")";
        }
        catch (Exception ex)
        {
        }
    }

    private void loadACC_ChartOfAccountLabel4()
    {
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlProduct.Items.Add(new ListItem("Any Product", "0"));
        ddlRefference.Items.Add(new ListItem("No Refference", "0"));
        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 3)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlProduct.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1)
            {
                if (aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.Contains("Show"))
                {
                    item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                    ddlWorkStationSearch.Items.Add(item);
                }
            }
            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 7) // share holder
            {
                
                    item = new ListItem("00-"+aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlRefference.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 4)
            {
                try
                {
                    if (int.Parse(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.Split('-')[0])>0)
                    {
                        item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                        ddlSalesPersonAll.Items.Add(item);
                    }
                }
                catch(Exception ex)
                {}
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

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            try
            {
                if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 4
                    &&
                    !aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.Contains("H1")
                    && aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.Contains(ddlWorkStationSearch.SelectedItem.Value.Split('-')[1] + "-")
                    ) //Show room employee
                {
                    ListItem item = new ListItem();
                    item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                    ddlRefference.Items.Add(item);
                }
            }
            catch (Exception ex)
            { }
        }

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
        ListItem li = new ListItem("Select Color...", "0");
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
      ,[TotalCostPerProduct] =([Pos_Product].[FabricsCost] + [Pos_Product].[AccesoriesCost] + [Pos_Product].[Overhead]+[Pos_Product].[OthersCost]+[Pos_Product].[PurchasePrice])
      ,((Pos_Product.SalePrice * 100) /105) as [SalePrice]
      ,[Pos_Product].[OldSalePrice]
      ,[Pos_Product].[Note]
      ,[Pos_Product].[BarCode]
      ,[Pos_Product].[Pos_ColorID]
      ,[Pos_Product].[Pos_FabricsTypeID]
      ,[Pos_Product].[StyleCode]
      ,[Pos_Product].[Pic1]
      ,[Pos_Product].[Pic2]
      ,[Pos_Product].[Pic3]
      ,[VatPercentage]=5.00
      ,[VatPercentageAlways] =5.00

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
  inner join ACC_ChartOfAccountLabel4 as WorkStation on WorkStation.ACC_ChartOfAccountLabel4ID=Pos_WorkStationStock.WorkStationID
  inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID = Pos_Product.Inv_QuantityUnitID
  inner join Pos_Color on Pos_Color.Pos_ColorID = Pos_Product.Pos_ColorID
  inner join Pos_Size on Pos_Size.Pos_SizeID = Pos_Product.Pos_SizeID
  inner join Pos_ProductType on Pos_ProductType.Pos_ProductTypeID = Pos_Product.Pos_ProductTypeID
  inner join Pos_ProductStatus on Pos_ProductStatus.Pos_ProductStatusID = Pos_Product.ProductStatusID
  inner join Pos_Brand on Pos_Brand.Pos_BrandID = Pos_Product.Pos_BrandID
where Pos_WorkStationStock.Stock >0 and Pos_WorkStationStock.WorkStationID = " + ddlWorkStationSearch.SelectedValue + @"
";

        if (ddlProduct.SelectedValue != "0")
        {
            sql += " and [Pos_Product].ProductID="+ddlProduct.SelectedValue;
        }

        if (txtDesignCode.Text != "")
        {
            sql += " and [Pos_Product].DesignCode=" + txtDesignCode.Text;
        }

        if (txtBarCode.Text != "")
        {
            sql += " and [Pos_Product].BarCode='" + txtBarCode.Text+"'";
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
        DataSet ds = CommonManager.SQLExec(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (bool.Parse(dr["IsVatExclusive"].ToString()))
            {
                //((Pos_Product.SalePrice * 100) /105)
                decimal salesprice = decimal.Parse(dr["SalePrice"].ToString()) ;
                dr["SalePrice"] = salesprice * decimal.Parse("1.05");
                dr["VatPercentage"]=0;
                dr["VatPercentageAlways"] = 0;
            }
        }


        gvPos_Product.DataSource = ds.Tables[0];
        gvPos_Product.DataBind();

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
        //loadSales person
        ddlSalesPerson.Items.Clear();
        foreach (ListItem item in ddlSalesPersonAll.Items)
        {
            if (int.Parse(ddlWorkStationSearch.SelectedItem.Text.Split('-')[1]) == int.Parse(item.Text.Split('-')[0]))
            {
                ddlSalesPerson.Items.Add(new ListItem(item.Text, item.Value));
            }
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



        foreach (GridViewRow gvr in gvPreview.Rows)
        {
            HiddenField hfPos_ProductID = (HiddenField)gvr.FindControl("hfPos_ProductID");
            TextBox txtQty = (TextBox)gvr.FindControl("txtQty");
            TextBox txtDiscountAmountTotal = (TextBox)gvr.FindControl("txtDiscountAmountTotal");
            TextBox txtVatGridTotal = (TextBox)gvr.FindControl("txtVatGridTotal");
            Label lblSubTotalSalePrice = (Label)gvr.FindControl("lblSubTotalSalePrice");
            Label lblStock = (Label)gvr.FindControl("lblStock");
            HiddenField hfVatPercentageAlways = (HiddenField)gvr.FindControl("hfVatPercentageAlways");
            HiddenField hfTotalCostPerProduct = (HiddenField)gvr.FindControl("hfTotalCostPerProduct");

            try
            {
                if (decimal.Parse(txtQty.Text) > decimal.Parse(lblStock.Text))
                {
                    showAlartMessage("Sale quantity can not be more than stock");
                }
            }
            catch (Exception dex)
            {
                showAlartMessage("Sale quantity can not be more than stock");
                return;
            }
        }
        if (txtDiscountPercentage.Text == "")
        {
            showAlartMessage("Discount amount can not be blank. if no discount then keep 0");
            return;
        
        }

        //this will be applied by card
        if (txtDiscountPercentage.Enabled && ddlRefference.SelectedValue == "0")
        {
            if (txtDiscountPercentage.Text != "" && decimal.Parse(txtDiscountPercentage.Text) > 0)
            {
                //showAlartMessage("Without refference the discount can not be applied");
                //return;
            }
            else
            {
                if (!ddlRefference.SelectedItem.Text.Contains("00"))
                {
                    if (decimal.Parse(txtDiscountPercentage.Text) > 35)
                    {
                        showAlartMessage("Employee can not give more than 35%");
                        return;
                    }
                }
            }
        }



        CompleteTheAutoCalculation();

        if (decimal.Parse(txtReturnAmount.Text) > decimal.Parse(txtPayableAmount.Text))
        {
            showAlartMessage("We can not Refund the money please buy other more product.");
            return;
        }

        if (decimal.Parse(txtRefundOrDue.Text) < 0)
        {
            showAlartMessage("Payment amount is less than the billing Amount");
            return;
        }

        
        if (decimal.Parse(txtRefundOrDue.Text) > decimal.Parse(txtCashAmount.Text)
            )
        {
            showAlartMessage("Your cash payment is less than refund amount");
            return;
        }

        string SQL_new = @"Declare @Pos_SalesReturnMasterID int;
               Set  @Pos_SalesReturnMasterID= "+getLogin().LoginID+@"    
        ";
        bool isRetrun = false;
            
        if (Request.QueryString["TransactionTypeID"] =="14")
        {
            
            
            foreach (GridViewRow gvr in gvReturnInvoice.Rows)
            {
                TextBox txtRtnQty = (TextBox)gvr.FindControl("txtRtnQty");
                Label lblSoldQty = (Label)gvr.FindControl("lblSoldQty");
                Label lblInvoiceNo = (Label)gvr.FindControl("lblInvoiceNo");
                Label lblBarCode = (Label)gvr.FindControl("lblBarCode");
                Label lblDiscountAmount = (Label)gvr.FindControl("lblDiscountAmount");
                Label lblVat = (Label)gvr.FindControl("lblVat");

                Label lblSalePrice = (Label)gvr.FindControl("lblSalePrice");
                Label lblStockSalePrice = (Label)gvr.FindControl("lblStockSalePrice");
                Label lblTotal = (Label)gvr.FindControl("lblTotal");
           
                HiddenField hfPos_TransactionID = (HiddenField)gvr.FindControl("hfPos_TransactionID");
                HiddenField hfPos_TransactionMasterID = (HiddenField)gvr.FindControl("hfPos_TransactionMasterID");
                HiddenField hfPos_ProductID = (HiddenField)gvr.FindControl("hfPos_ProductID");

                HiddenField hfVatPercentageAlways = (HiddenField)gvr.FindControl("hfVatPercentageAlways");
                HiddenField hfTotalCostPerProduct = (HiddenField)gvr.FindControl("hfTotalCostPerProduct");


                if (txtRtnQty.Text != "0" && txtRtnQty.Text != "")
                {
                    if (!isRetrun)
                    {
                        #region Sales Return Master
                        SQL_new += @"
                        INSERT INTO [Pos_SalesReturnMaster]
                           ([LocalSalesReturnMasterID]
                           ,[InvoiceNo]
                           ,[WorkStationID]
                           ,[SalesDate]
                           ,[SalesReturnID]
                           ,[CashSales]
                           ,[CashSalesJournalDetailID]
                           ,[DBBLAmount]
                           ,[CityAmount]
                           ,[VATPercentage]
                           ,[VATAmount]
                           ,[DiscountPercentage]
                           ,[DiscountAmount]
                           ,[RefundAmount]
                           ,[SalesPerson]
                           ,[Referrence]
                           ,[Customer]
                           ,[MobileNo]
                           ,[CustomerName]
                           ,[Note]
                           ,[LocalJournalMasterID]
                           ,[LocalTransactionMasterID]
                           ,[LocalReturnTransactionMasterID]
                           ,[ServerJournalMasterID]
                           ,[ServerTransactionMasterID]
                           ,[ServerReturnTransactionMasterID]
                           ,[StatusID]
                           ,[ExtraField1]
                           ,[ExtraField2]
                           ,[ExtraField3]
                           ,[ExtraField4]
                           ,[ExtraField5]
                           ,[ExtraField6]
                           ,[ExtraField7]
                           ,[ExtraField8]
                           ,[ExtraField9]
                           ,[ExtraField10])
                     VALUES
                           (
                            0--<LocalSalesReturnMasterID, int,>
                           ," + lblInvoiceNo.Text + @"--<InvoiceNo, int,>
                           ," +ddlWorkStationSearch.SelectedValue +@"--<WorkStationID, int,>
                           ,'" + (txtBackDatedSale.Text == DateTime.Now.ToString("dd MMM yyyy") ? DateTime.Now : DateTime.Parse(txtBackDatedSale.Text)) + @"'--<SalesDate, datetime,>
                           ,0--<SalesReturnID, int,>
                           ,0--<CashSales, decimal(18,2),>
                           ,0--<CashSalesJournalDetailID, int,>
                           ,0--<DBBLAmount, decimal(18,2),>
                           ,0--<CityAmount, decimal(18,2),>
                           ,0--<VATPercentage, decimal(18,2),>
                           ,0--<VATAmount, decimal(18,2),>
                           ,0--<DiscountPercentage, decimal(18,2),>
                           ,0--<DiscountAmount, decimal(18,2),>
                           ,0--<RefundAmount, decimal(18,2),>
                           ," + ddlSalesPerson.SelectedValue + @"--<SalesPerson, int,>
                           ,0--<Referrence, int,>
                           ," + (txtCustomerID.Text == "" ? "0" : txtCustomerID.Text) + @"--<Customer, int,>
                           ,'" + txtContactNo.Text + @"'--<MobileNo, nvarchar(50),>
                           ,'" + txtCustomerName.Text + @"'--<CustomerName, nvarchar(50),>
                           ,''--<Note, ntext,>
                           ,0--<LocalJournalMasterID, int,>
                           ,0--<LocalTransactionMasterID, int,>
                           ,0--<LocalReturnTransactionMasterID, int,>
                           ,0--<ServerJournalMasterID, int,>
                           ,0--<ServerTransactionMasterID, int,>
                           ,0--<ServerReturnTransactionMasterID, int,>
                           ,0--<StatusID, int,>
                           ,'" + decimal.Parse(txtReturnAmount.Text ).ToString("0.00")+ @"'--<ExtraField1, nvarchar(256),> 
                           ,''--<ExtraField2, nvarchar(256),>
                           ,''--<ExtraField3, nvarchar(256),>
                           ,''--<ExtraField4, nvarchar(256),>
                           ,'" + txtCardNo.Text + @"'--<ExtraField5, nvarchar(256),>
                           ,''--<ExtraField6, nvarchar(256),>
                           ,''--<ExtraField7, nvarchar(256),>
                           ,''--<ExtraField8, nvarchar(256),>
                           ,''--<ExtraField9, nvarchar(256),>
                           ,''--<ExtraField10, nvarchar(256),>
                    );
                    set @Pos_SalesReturnMasterID =SCOPE_IDENTITY();
                   
                    ";

                        #endregion

                        isRetrun = true;
                    }

                    #region Sales Return Details
                     SQL_new += @"
                        INSERT INTO [Pos_SalesReturnDetail]
                           ([Pos_SalesReturnMasterID]
                            ,[LocalSalesDetailID]
                           ,[RemoteSalesDetailID]
                           ,[ProductID]
                           ,[Quantity]
                           ,[WorkStationID]
                           ,[CostPrice]
                           ,[SalesPrice]
                           ,[DiscountPercentage]
                           ,[DiscountAmount]
                           ,[VATPercentage]
                           ,[VATAmount]
                           ,[LocalPos_TransactionMasterID]
                           ,[SererPos_TransactionMasterID]
                           ,[LocalTransactionID]
                           ,[ServerTransactionID]
                           ,[LocalPos_SalesMasterID]
                           ,[ServerPos_SalesMasterID]
                           ,[LocalFinishedGoodAssetJournalDetailID]
                           ,[LocalCostOfGoodsSoldJournalDetailID]
                           ,[LocalSalesRevinueJournalDetailID]
                           ,[LocalVATIncludingJournalDetailID]
                           ,[LocalVATExcludingJournalDetailID]
                           ,[LocalDiscountJournalDetailID]
                           ,[ServerFinishedGoodAssetJournalDetailID]
                           ,[ServerCostOfGoodsSoldJournalDetailID]
                           ,[ServerSalesRevinueJournalDetailID]
                           ,[ServerVATIncludingJournalDetailID]
                           ,[ServerVATExcludingJournalDetailID]
                           ,[ServerDiscountJournalDetailID]
                           ,[StatusID]
                           ,[ExtraField1]
                           ,[ExtraField2]
                           ,[ExtraField3]
                           ,[ExtraField4]
                           ,[ExtraField5]
                           ,[ExtraField6]
                           ,[ExtraField7]
                           ,[ExtraField8]
                           ,[ExtraField9]
                           ,[ExtraField10])
                     VALUES
                           (@Pos_SalesReturnMasterID
                            ,"+"0"+@"--LocalSalesDetailID, int,>
                           ,"+"0"+@"--RemoteSalesDetailID, int,>
                           ,"+hfPos_ProductID.Value+@"--ProductID, int,>
                           ," + txtRtnQty.Text + @"--Quantity, int,>
                           ," + ddlWorkStationSearch.SelectedValue + @"--WorkStationID, int,>
                           ," +"0"+@"--CostPrice, decimal(18,2),>
                           ," + (decimal.Parse(lblSalePrice.Text)).ToString("0") + @"--SalesPrice, decimal(18,2),>
                           ," +"0"+@"--DiscountPercentage, decimal(18,2),>
                           ," + (lblDiscountAmount.Text == "" ? "0" : lblDiscountAmount.Text) + @"--DiscountAmount, decimal(18,2),>
                           ," + hfVatPercentageAlways.Value + @"--VATPercentage, decimal(18,2),>
                           ," + (lblVat.Text == "" ? "0" : lblVat.Text) + @"--VATAmount, decimal(18,2),>
                           ," +"0"+@"--LocalPos_TransactionMasterID, int,>
                           ,"+"0"+@"--SererPos_TransactionMasterID, int,>
                           ,"+"0"+@"--LocalTransactionID, int,>
                           ,"+"0"+@"--ServerTransactionID, int,>
                           ,"+"0"+@"--LocalPos_SalesMasterID, int,>
                           ,"+"0"+@"--ServerPos_SalesMasterID, int,>
                           ,"+"0"+@"--LocalFinishedGoodAssetJournalDetailID, int,>
                           ,"+"0"+@"--LocalCostOfGoodsSoldJournalDetailID, int,>
                           ,"+"0"+@"--LocalSalesRevinueJournalDetailID, int,>
                           ,"+"0"+@"--LocalVATIncludingJournalDetailID, int,>
                           ,"+"0"+@"--LocalVATExcludingJournalDetailID, int,>
                           ,"+"0"+@"--LocalDiscountJournalDetailID, int,>
                           ,"+"0"+@"--ServerFinishedGoodAssetJournalDetailID, int,>
                           ,"+"0"+@"--ServerCostOfGoodsSoldJournalDetailID, int,>
                           ,"+"0"+@"--ServerSalesRevinueJournalDetailID, int,>
                           ,"+"0"+@"--ServerVATIncludingJournalDetailID, int,>
                           ,"+"0"+@"--ServerVATExcludingJournalDetailID, int,>
                           ,"+"0"+@"--ServerDiscountJournalDetailID, int,>
                           ,"+"0"+@"--StatusID, int,>
                           ,'" + ((decimal.Parse(lblDiscountAmount.Text) / decimal.Parse(lblSoldQty.Text)) * decimal.Parse(txtRtnQty.Text)).ToString("0,0.00") + @"'--ExtraField1, nvarchar(256),>
                           ,'" + lblTotal.Text + @"'--ExtraField2, nvarchar(256),> total
                           ,'" + ((decimal.Parse(lblVat.Text) / decimal.Parse(lblSoldQty.Text)) * decimal.Parse(txtRtnQty.Text)).ToString("0,0.00") + @"'--ExtraField3, nvarchar(256),>
                           ,'" + hfPos_TransactionID.Value + @"'--ExtraField4, nvarchar(256),>
                           ,'" + ddlRefference.SelectedValue + @"'--ExtraField5, nvarchar(256),>
                           ,'" + lblBarCode.Text.Trim() + @"'--ExtraField6, nvarchar(256),> BarCode
                           ,'" + hfPos_TransactionID.Value + @"'--ExtraField7, nvarchar(256),> TransactionID
                           ,'" + lblStockSalePrice.Text + @"'--ExtraField8, nvarchar(256),> StockSalePrice
                           ,'" + lblSoldQty.Text + @"'--ExtraField9, nvarchar(256),>SoldQty
                           ,'" + hfPos_TransactionMasterID.Value + @"'--ExtraField10, nvarchar(256),>
                );

                ";
                    #endregion
                    
                   
                }
            }
        }

        int transactionTypeID = 13;//int.Parse(Request.QueryString["TransactionTypeID"]);

        SQL_new += @"
                Declare @Pos_SalesMasterID int; ";

        int TransactionMasterID = 0;
        if (txtOldTransactionID.Text != "")
        {
            if (txtOldTransactionID.Text != "0")
            {
                TransactionMasterID = int.Parse(GetTransactionMasterID());
                SQL_new += @"
                Set  @Pos_SalesMasterID =" + TransactionMasterID.ToString() + "; ";
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
            Pos_Product PaymentGrid_cardPayment = new Pos_Product();
            PaymentGrid_cardPayment.ExtraField1 = "";
            PaymentGrid_cardPayment.ExtraField2 = "";
            PaymentGrid_cardPayment.ExtraField3 = "0";
            PaymentGrid_cardPayment.ExtraField4 = "0";
            PaymentGrid_cardPayment.ExtraField5 = "0";  
            foreach (GridViewRow gvr in gvPayment.Rows)
            {
                HiddenField hfSerial = (HiddenField)gvr.FindControl("hfSerial");
                HiddenField hfIssueFromID = (HiddenField)gvr.FindControl("hfIssueFromID");
                Label lblCardNo = (Label)gvr.FindControl("lblCardNo");
                Label lblCardType = (Label)gvr.FindControl("lblCardType");
                Label lblIssueFrom = (Label)gvr.FindControl("lblIssueFrom");
                Label lblAmount = (Label)gvr.FindControl("lblAmount");

                PaymentGrid_cardPayment.ExtraField1 = lblCardNo.Text;
                PaymentGrid_cardPayment.ExtraField2 = lblCardType.Text;
                PaymentGrid_cardPayment.ExtraField3 = lblIssueFrom.Text;
                PaymentGrid_cardPayment.ExtraField4 = hfIssueFromID.Value;
                PaymentGrid_cardPayment.ExtraField5 = lblAmount.Text;


            }
           
            #region sql_SalesMaster

            SQL_new += @"
                INSERT INTO [Pos_SalesMaster]
                ([LocalSalesMasterID]
                ,[InvoiceNo]
                ,[WorkStationID]
                ,[SalesDate]
                ,[SalesReturnID]
                ,[CashSales]
                ,[CashSalesJournalDetailID]
                ,[DBBLAmount]
                ,[CityAmount]
                ,[VATPercentage]
                ,[VATAmount]
                ,[DiscountPercentage]
                ,[DiscountAmount]
                ,[RefundAmount]
                ,[SalesPerson]
                ,[Referrence]
                ,[Customer]
                ,[MobileNo]
                ,[CustomerName]
                ,[Note]
                ,[LocalJournalMasterID]
                ,[LocalTransactionMasterID]
                ,[LocalReturnTransactionMasterID]
                ,[ServerJournalMasterID]
                ,[ServerTransactionMasterID]
                ,[ServerReturnTransactionMasterID]
                ,[StatusID]
                ,[ExtraField1]
                ,[ExtraField2]
                ,[ExtraField3]
                ,[ExtraField4]
                ,[ExtraField5]
                ,[ExtraField6]
                ,[ExtraField7]
                ,[ExtraField8]
                ,[ExtraField9]
                ,[ExtraField10]
                ,[LocalCashAmountJournalDetailID]
      ,[LocalCard1AmountJournalDetailID]
      ,[LocalCard2AmountJournalDetailID]
      ,[LocalCard3AmountJournalDetailID]
      ,[ServerCashAmountJournalDetailID]
,[ServerCard1AmountJournalDetailID]
      ,[ServerCard2AmountJournalDetailID]
      ,[ServerCard3AmountJournalDetailID])
                VALUES
                (
                " + "0" + @"--<LocalSalesMasterID, int,>
                ," + "0" + @"--<InvoiceNo, int,>
                ," + ddlWorkStationSearch.SelectedValue + @"--<WorkStationID, int,>
                ,'" + (txtBackDatedSale.Text == DateTime.Now.ToString("dd MMM yyyy") ? DateTime.Now : DateTime.Parse(txtBackDatedSale.Text)) + @"'--<SalesDate, datetime,>
                ," + "0" + @"--<SalesReturnID, int,>
                ," + txtCashAmount.Text + @"--<CashSales, decimal(18,2),>
                ," + "0" + @"--<CashSalesJournalDetailID, int,>
                ," + "0" + @"--<DBBLAmount, decimal(18,2),>
                ," + "0" + @"--<CityAmount, decimal(18,2),>
                ," + "0" + @"--<VATPercentage, decimal(18,2),>
                ," + "0" + @"--<VATAmount, decimal(18,2),>
                ," + "0" + @"--<DiscountPercentage, decimal(18,2),>
                ," + "0" + @"--<DiscountAmount, decimal(18,2),>
                ," + decimal.Parse(txtRefundOrDue.Text).ToString("0.00") + @"--<RefundAmount, decimal(18,2),>
                ," + ddlSalesPerson.SelectedValue + @"--<SalesPerson, int,>
                ," + "0" + @"--<Referrence, int,>
                ," + (txtCustomerID.Text == "" ? "0" : txtCustomerID.Text) + @"--<Customer, int,>
                ,'" + txtContactNo.Text + @"'--<MobileNo, nvarchar(50),>
                ,'" + txtCustomerName.Text + @"'--<CustomerName, nvarchar(50),>
                ,'" + txtNote.Text + @"'--<Note, ntext,>
                ," + "0" + @"--<LocalJournalMasterID, int,>
                ," + "0" + @"--<LocalTransactionMasterID, int,>
                ," + "0" + @"--<LocalReturnTransactionMasterID, int,>
                ," + "0" + @"--<ServerJournalMasterID, int,>
                ," + "0" + @"--<ServerTransactionMasterID, int,>
                ," + "0" + @"--<ServerReturnTransactionMasterID, int,>
                ," + "0" + @"--<StatusID, int,>
                ,'" + decimal.Parse(txtCashAmount.Text).ToString("0.00") + @"'--<ExtraField1, nvarchar(256),>
                ,'" + decimal.Parse(txtCardAmount.Text).ToString("0.00") + @"'--<ExtraField2, nvarchar(256),>
                ,'" + ddlWorkStationSearch.SelectedItem.Text + @"'--<ExtraField3, nvarchar(256),>
                ,''--<ExtraField4, nvarchar(256),>
                ,'" + decimal.Parse(txtRefundOrDue.Text).ToString("0.00") + @"'--<ExtraField5, nvarchar(256),>
                ," + (isRetrun? "cast(@Pos_SalesReturnMasterID as nvarchar(256))" :"''") + @"--<ExtraField6, nvarchar(256),> updatedBy
                ,'" + PaymentGrid_cardPayment.ExtraField3 + @"'--<ExtraField7, nvarchar(256),> Issue From 
                ,'" + PaymentGrid_cardPayment.ExtraField5 + @"'--<ExtraField8, nvarchar(256),> Card Amount
                ,'" + PaymentGrid_cardPayment.ExtraField2 + @"'--<ExtraField9, nvarchar(256),> Card Type
                ,'" + PaymentGrid_cardPayment.ExtraField1 + @"'--<ExtraField10, nvarchar(256),> Card No
                ,0,0,0,0,0,0,0,0);
                  
                Set @Pos_SalesMasterID =SCOPE_IDENTITY();

                ";
            #endregion
            
        }


        foreach (GridViewRow gvr in gvPreview.Rows)
        {
            HiddenField hfPos_ProductID = (HiddenField)gvr.FindControl("hfPos_ProductID");
            TextBox txtQty = (TextBox)gvr.FindControl("txtQty");
            TextBox txtDiscountAmountTotal = (TextBox)gvr.FindControl("txtDiscountAmountTotal");
            TextBox txtVatGridTotal = (TextBox)gvr.FindControl("txtVatGridTotal");
            Label lblSubTotalSalePrice = (Label)gvr.FindControl("lblSubTotalSalePrice");
            HiddenField hfVatPercentageAlways = (HiddenField)gvr.FindControl("hfVatPercentageAlways");
            HiddenField hfTotalCostPerProduct = (HiddenField)gvr.FindControl("hfTotalCostPerProduct");
            Label lblStockSalePrice = (Label)gvr.FindControl("lblStockSalePrice");
            Label lblSalePrice = (Label)gvr.FindControl("lblSalePrice");
           
            if (txtQty.Text == "")
                continue;
            if (decimal.Parse(txtQty.Text) == 0)
                continue;


            #region sql_SalesDetails
           SQL_new += @"
            INSERT INTO [Pos_SalesDetail]
                       ([Pos_SalesMasterID]
                       ,[LocalSalesDetailID]
                       ,[RemoteSalesDetailID]
                       ,[ProductID]
                       ,[Quantity]
                       ,[WorkStationID]
                       ,[CostPrice]
                       ,[SalesPrice]
                       ,[DiscountPercentage]
                       ,[DiscountAmount]
                       ,[VATPercentage]
                       ,[VATAmount]
                       ,[LocalPos_TransactionMasterID]
                       ,[SererPos_TransactionMasterID]
                       ,[LocalTransactionID]
                       ,[ServerTransactionID]
                       ,[LocalPos_SalesMasterID]
                       ,[ServerPos_SalesMasterID]
                       ,[LocalFinishedGoodAssetJournalDetailID]
                       ,[LocalCostOfGoodsSoldJournalDetailID]
                       ,[LocalSalesRevinueJournalDetailID]
                       ,[LocalVATIncludingJournalDetailID]
                       ,[LocalVATExcludingJournalDetailID]
                       ,[LocalDiscountJournalDetailID]
                       ,[ServerFinishedGoodAssetJournalDetailID]
                       ,[ServerCostOfGoodsSoldJournalDetailID]
                       ,[ServerSalesRevinueJournalDetailID]
                       ,[ServerVATIncludingJournalDetailID]
                       ,[ServerVATExcludingJournalDetailID]
                       ,[ServerDiscountJournalDetailID]
                       ,[StatusID]
                       ,[ExtraField1]
                       ,[ExtraField2]
                       ,[ExtraField3]
                       ,[ExtraField4]
                       ,[ExtraField5]
                       ,[ExtraField6]
                       ,[ExtraField7]
                       ,[ExtraField8]
                       ,[ExtraField9]
                       ,[ExtraField10])
                 VALUES
                       (@Pos_SalesMasterID--Pos_SalesMasterID, int,>
                       ," + "0" + @"--LocalSalesDetailID, int,>
                       ," + "0" + @"--RemoteSalesDetailID, int,>
                       ," + hfPos_ProductID.Value + @"--ProductID, int,>
                       ," + txtQty.Text + @"--Quantity, int,>
                       ," + ddlWorkStationSearch.SelectedValue + @"--WorkStationID, int,>
                       ," + "0" + @"--CostPrice, decimal(18,2),>
                       ," + decimal.Parse(lblSalePrice.Text).ToString("0") + @"--SalesPrice, decimal(18,2),>
                       ," + decimal.Parse(hfVatPercentageAlways.Value).ToString("0") + @"--DiscountPercentage, decimal(18,2),>
                       ," + decimal.Parse(txtDiscountAmountTotal.Text).ToString("0") + @"--DiscountAmount, decimal(18,2),>
                       ," + "5" + @"--VATPercentage, decimal(18,2),>
                       ," + decimal.Parse(txtVatGridTotal.Text).ToString("0") + @"--VATAmount, decimal(18,2),>
                       ," + "0" + @"--LocalPos_TransactionMasterID, int,>
                       ," + "0" + @"--SererPos_TransactionMasterID, int,>
                       ," + "0" + @"--LocalTransactionID, int,>
                       ," + "0" + @"--ServerTransactionID, int,>
                       ," + "0" + @"--LocalPos_SalesMasterID, int,>
                       ," + "0" + @"--ServerPos_SalesMasterID, int,>
                       ," + "0" + @"--LocalFinishedGoodAssetJournalDetailID, int,>
                       ," + "0" + @"--LocalCostOfGoodsSoldJournalDetailID, int,>
                       ," + "0" + @"--LocalSalesRevinueJournalDetailID, int,>
                       ," + "0" + @"--LocalVATIncludingJournalDetailID, int,>
                       ," + "0" + @"--LocalVATExcludingJournalDetailID, int,>
                       ," + "0" + @"--LocalDiscountJournalDetailID, int,>
                       ," + "0" + @"--ServerFinishedGoodAssetJournalDetailID, int,>
                       ," + "0" + @"--ServerCostOfGoodsSoldJournalDetailID, int,>
                       ," + "0" + @"--ServerSalesRevinueJournalDetailID, int,>
                       ," + "0" + @"--ServerVATIncludingJournalDetailID, int,>
                       ," + "0" + @"--ServerVATExcludingJournalDetailID, int,>
                       ," + "0" + @"--ServerDiscountJournalDetailID, int,>
                       ," + "0" + @"--StatusID, int,>
                       ,'" + ddlRefference.SelectedValue + @"'----ExtraField1, nvarchar(256),>ddlRefference
                       ,'" + "" + @"'----ExtraField2, nvarchar(256),>
                       ,'" + "" + @"'----ExtraField3, nvarchar(256),>
                       ,'" + "" + @"'----ExtraField4, nvarchar(256),>
                       ,'" + "" + @"'----ExtraField5, nvarchar(256),>
                       ,'" + "" + @"'----ExtraField6, nvarchar(256),>
                       ,'" + "" + @"'----ExtraField7, nvarchar(256),>
                       ,'" + "" + @"'----ExtraField8, nvarchar(256),>
                       ,'" + "" + @"'----ExtraField9, nvarchar(256),>
                       ,'" + "" + @"'----ExtraField10, nvarchar(256),>
                       );

";
            #endregion

        }

        DataSet ds= CommonManager.SQLExec(SQL_new + @"
             select  @Pos_SalesMasterID ;");
        ExecuteTransactionNJournalEntry(ds.Tables[0].Rows[0][0].ToString());

    }

    private void ExecuteTransactionNJournalEntry(string Pos_SalesMasterID)
    {
        int invoiceNo = CommonManager.ProcessSales(Pos_SalesMasterID, getLogin().LoginID);
        SaveForRemoteDB(processSQLforRemoteDB(Pos_SalesMasterID));
        txtOldTransactionID.Text = invoiceNo.ToString();
        Response.Redirect("SalesPrint.aspx?Pos_TransactionMasterID=" + invoiceNo.ToString());
    }

    private string processSQLforRemoteDB(string Pos_SalesMasterID)
    {
        string sql_update = "";
        string sql_sales = @"
            Select * from Pos_SalesMaster where Pos_SalesMasterID=" + Pos_SalesMasterID + @"
            Select * from Pos_SalesDetail where Pos_SalesMasterID=" + Pos_SalesMasterID + @"
            ";
        DataSet ds_Pos_Sales = CommonManager.SQLExec(sql_sales);
        DataSet ds_Pos_SalesReturn = new DataSet();
        if (ds_Pos_Sales.Tables[0].Rows[0]["ExtraField6"].ToString() != "") //sales return
        {
            string sql_SalesReturn = @"
                Select * from Pos_SalesReturnMaster where Pos_SalesReturnMasterID=" + ds_Pos_Sales.Tables[0].Rows[0]["ExtraField6"].ToString() + @"
                Select * from Pos_SalesReturnDetail where Pos_SalesReturnMasterID=" + ds_Pos_Sales.Tables[0].Rows[0]["ExtraField6"].ToString() + @"
                ";
            ds_Pos_SalesReturn = CommonManager.SQLExec(sql_SalesReturn);

            #region Pos_SalesReturnMaster
            
           
            sql_update += @"
                INSERT INTO [Pos_SalesReturnMaster]
           ([LocalSalesReturnMasterID]
           ,[InvoiceNo]
           ,[WorkStationID]
           ,[SalesDate]
           ,[SalesReturnID]
           ,[CashSales]
           ,[CashSalesJournalDetailID]
           ,[DBBLAmount]
           ,[CityAmount]
           ,[VATPercentage]
           ,[VATAmount]
           ,[DiscountPercentage]
           ,[DiscountAmount]
           ,[RefundAmount]
           ,[SalesPerson]
           ,[Referrence]
           ,[Customer]
           ,[MobileNo]
           ,[CustomerName]
           ,[Note]
           ,[LocalJournalMasterID]
           ,[LocalTransactionMasterID]
           ,[LocalReturnTransactionMasterID]
           ,[ServerJournalMasterID]
           ,[ServerTransactionMasterID]
           ,[ServerReturnTransactionMasterID]
           ,[StatusID]
           ,[ExtraField1]
           ,[ExtraField2]
           ,[ExtraField3]
           ,[ExtraField4]
           ,[ExtraField5]
           ,[ExtraField6]
           ,[ExtraField7]
           ,[ExtraField8]
           ,[ExtraField9]
           ,[ExtraField10])
     VALUES
           (" + ds_Pos_SalesReturn.Tables[0].Rows[0]["LocalSalesReturnMasterID"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["InvoiceNo"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["WorkStationID"].ToString() + @"
           ,'" + ds_Pos_SalesReturn.Tables[0].Rows[0]["SalesDate"].ToString() + @"'
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["SalesReturnID"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["CashSales"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["CashSalesJournalDetailID"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["DBBLAmount"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["CityAmount"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["VATPercentage"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["VATAmount"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["DiscountPercentage"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["DiscountAmount"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["RefundAmount"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["SalesPerson"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["Referrence"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["Customer"].ToString() + @"
           ,'" + ds_Pos_SalesReturn.Tables[0].Rows[0]["MobileNo"].ToString() + @"'
           ,'" + ds_Pos_SalesReturn.Tables[0].Rows[0]["CustomerName"].ToString() + @"'
           ,'" + ds_Pos_SalesReturn.Tables[0].Rows[0]["Note"].ToString() + @"'
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["LocalJournalMasterID"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["LocalTransactionMasterID"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["LocalReturnTransactionMasterID"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["ServerJournalMasterID"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["ServerTransactionMasterID"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["ServerReturnTransactionMasterID"].ToString() + @"
           ," + ds_Pos_SalesReturn.Tables[0].Rows[0]["StatusID"].ToString() + @"
           ,'" + ds_Pos_SalesReturn.Tables[0].Rows[0]["ExtraField1"].ToString() + @"'
           ,'" + ds_Pos_SalesReturn.Tables[0].Rows[0]["ExtraField2"].ToString() + @"'
           ,'" + ds_Pos_SalesReturn.Tables[0].Rows[0]["ExtraField3"].ToString() + @"'
           ,'" + ds_Pos_SalesReturn.Tables[0].Rows[0]["ExtraField4"].ToString() + @"'
           ,'" + ds_Pos_SalesReturn.Tables[0].Rows[0]["ExtraField5"].ToString() + @"'
           ,'" + ds_Pos_SalesReturn.Tables[0].Rows[0]["ExtraField6"].ToString() + @"'
           ,'" + ds_Pos_SalesReturn.Tables[0].Rows[0]["ExtraField7"].ToString() + @"'
           ,'" + ds_Pos_SalesReturn.Tables[0].Rows[0]["ExtraField8"].ToString() + @"'
           ,'" + ds_Pos_SalesReturn.Tables[0].Rows[0]["ExtraField9"].ToString() + @"'
           ,'" + ds_Pos_SalesReturn.Tables[0].Rows[0]["ExtraField10"].ToString() + @"');
            ";

            #endregion

            foreach (DataRow dr_SalesReturn in ds_Pos_SalesReturn.Tables[1].Rows)
            {
                #region Pos_SalesReturnDetail
                sql_update += @"
INSERT INTO [Pos_SalesReturnDetail]
           ([Pos_SalesReturnMasterID]
           ,[LocalSalesDetailID]
           ,[RemoteSalesDetailID]
           ,[ProductID]
           ,[Quantity]
           ,[WorkStationID]
           ,[CostPrice]
           ,[SalesPrice]
           ,[DiscountPercentage]
           ,[DiscountAmount]
           ,[VATPercentage]
           ,[VATAmount]
           ,[LocalPos_TransactionMasterID]
           ,[SererPos_TransactionMasterID]
           ,[LocalTransactionID]
           ,[ServerTransactionID]
           ,[LocalPos_SalesMasterID]
           ,[ServerPos_SalesMasterID]
           ,[LocalFinishedGoodAssetJournalDetailID]
           ,[LocalCostOfGoodsSoldJournalDetailID]
           ,[LocalSalesRevinueJournalDetailID]
           ,[LocalVATIncludingJournalDetailID]
           ,[LocalVATExcludingJournalDetailID]
           ,[LocalDiscountJournalDetailID]
           ,[ServerFinishedGoodAssetJournalDetailID]
           ,[ServerCostOfGoodsSoldJournalDetailID]
           ,[ServerSalesRevinueJournalDetailID]
           ,[ServerVATIncludingJournalDetailID]
           ,[ServerVATExcludingJournalDetailID]
           ,[ServerDiscountJournalDetailID]
           ,[StatusID]
           ,[ExtraField1]
           ,[ExtraField2]
           ,[ExtraField3]
           ,[ExtraField4]
           ,[ExtraField5]
           ,[ExtraField6]
           ,[ExtraField7]
           ,[ExtraField8]
           ,[ExtraField9]
           ,[ExtraField10])
     VALUES
           (" + dr_SalesReturn["Pos_SalesReturnMasterID"].ToString() + @"
           ," + dr_SalesReturn["LocalSalesDetailID"].ToString() + @"
           ," + dr_SalesReturn["RemoteSalesDetailID"].ToString() + @"
           ," + dr_SalesReturn["ProductID"].ToString() + @"
           ," + dr_SalesReturn["Quantity"].ToString() + @"
           ," + dr_SalesReturn["WorkStationID"].ToString() + @"
           ," + dr_SalesReturn["CostPrice"].ToString() + @"
           ," + dr_SalesReturn["SalesPrice"].ToString() + @"
           ," + dr_SalesReturn["DiscountPercentage"].ToString() + @"
           ," + dr_SalesReturn["DiscountAmount"].ToString() + @"
           ," + dr_SalesReturn["VATPercentage"].ToString() + @"
           ," + dr_SalesReturn["VATAmount"].ToString() + @"
           ," + dr_SalesReturn["LocalPos_TransactionMasterID"].ToString() + @"
           ," + dr_SalesReturn["SererPos_TransactionMasterID"].ToString() + @"
           ," + dr_SalesReturn["LocalTransactionID"].ToString() + @"
           ," + dr_SalesReturn["ServerTransactionID"].ToString() + @"
           ," + dr_SalesReturn["LocalPos_SalesMasterID"].ToString() + @"
           ," + dr_SalesReturn["ServerPos_SalesMasterID"].ToString() + @"
           ," + dr_SalesReturn["LocalFinishedGoodAssetJournalDetailID"].ToString() + @"
           ," + dr_SalesReturn["LocalCostOfGoodsSoldJournalDetailID"].ToString() + @"
           ," + dr_SalesReturn["LocalSalesRevinueJournalDetailID"].ToString() + @"
           ," + dr_SalesReturn["LocalVATIncludingJournalDetailID"].ToString() + @"
           ," + dr_SalesReturn["LocalVATExcludingJournalDetailID"].ToString() + @"
           ," + dr_SalesReturn["LocalDiscountJournalDetailID"].ToString() + @"
           ," + dr_SalesReturn["ServerFinishedGoodAssetJournalDetailID"].ToString() + @"
           ," + dr_SalesReturn["ServerCostOfGoodsSoldJournalDetailID"].ToString() + @"
           ," + dr_SalesReturn["ServerSalesRevinueJournalDetailID"].ToString() + @"
           ," + dr_SalesReturn["ServerVATIncludingJournalDetailID"].ToString() + @"
           ," + dr_SalesReturn["ServerVATExcludingJournalDetailID"].ToString() + @"
           ," + dr_SalesReturn["ServerDiscountJournalDetailID"].ToString() + @"
           ," + dr_SalesReturn["StatusID"].ToString() + @"
           ,'" + dr_SalesReturn["ExtraField1"].ToString() + @"'
           ,'" + dr_SalesReturn["ExtraField2"].ToString() + @"'
           ,'" + dr_SalesReturn["ExtraField3"].ToString() + @"'
           ,'" + dr_SalesReturn["ExtraField4"].ToString() + @"'
           ,'" + dr_SalesReturn["ExtraField5"].ToString() + @"'
           ,'" + dr_SalesReturn["ExtraField6"].ToString() + @"'
           ,'" + dr_SalesReturn["ExtraField7"].ToString() + @"'
           ,'" + dr_SalesReturn["ExtraField8"].ToString() + @"'
           ,'" + dr_SalesReturn["ExtraField9"].ToString() + @"'
           ,'" + dr_SalesReturn["ExtraField10"].ToString() + @"');
";
                #endregion
            }
        }

         #region Pos_SalesMaster
        sql_update += @"
INSERT INTO [Pos_SalesMaster]
           ([LocalSalesMasterID]
           ,[InvoiceNo]
           ,[WorkStationID]
           ,[SalesDate]
           ,[SalesReturnID]
           ,[CashSales]
           ,[CashSalesJournalDetailID]
           ,[DBBLAmount]
           ,[CityAmount]
           ,[VATPercentage]
           ,[VATAmount]
           ,[DiscountPercentage]
           ,[DiscountAmount]
           ,[RefundAmount]
           ,[SalesPerson]
           ,[Referrence]
           ,[Customer]
           ,[MobileNo]
           ,[CustomerName]
           ,[Note]
           ,[LocalJournalMasterID]
           ,[LocalTransactionMasterID]
           ,[LocalReturnTransactionMasterID]
           ,[LocalCashAmountJournalDetailID]
           ,[LocalCard1AmountJournalDetailID]
           ,[LocalCard2AmountJournalDetailID]
           ,[LocalCard3AmountJournalDetailID]
           ,[ServerJournalMasterID]
           ,[ServerTransactionMasterID]
           ,[ServerReturnTransactionMasterID]
           ,[ServerCashAmountJournalDetailID]
           ,[ServerCard1AmountJournalDetailID]
           ,[ServerCard2AmountJournalDetailID]
           ,[ServerCard3AmountJournalDetailID]
           ,[StatusID]
           ,[ExtraField1]
           ,[ExtraField2]
           ,[ExtraField3]
           ,[ExtraField4]
           ,[ExtraField5]
           ,[ExtraField6]
           ,[ExtraField7]
           ,[ExtraField8]
           ,[ExtraField9]
           ,[ExtraField10])
     VALUES
           (" + ds_Pos_Sales.Tables[0].Rows[0]["LocalSalesMasterID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["InvoiceNo"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["WorkStationID"].ToString() + @"
           ,'" + ds_Pos_Sales.Tables[0].Rows[0]["SalesDate"].ToString() + @"'
           ," + ds_Pos_Sales.Tables[0].Rows[0]["SalesReturnID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["CashSales"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["CashSalesJournalDetailID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["DBBLAmount"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["CityAmount"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["VATPercentage"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["VATAmount"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["DiscountPercentage"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["DiscountAmount"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["RefundAmount"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["SalesPerson"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["Referrence"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["Customer"].ToString() + @"
           ,'" + ds_Pos_Sales.Tables[0].Rows[0]["MobileNo"].ToString() + @"'
           ,'" + ds_Pos_Sales.Tables[0].Rows[0]["CustomerName"].ToString() + @"'
           ,'" + ds_Pos_Sales.Tables[0].Rows[0]["Note"].ToString() + @"'
           ," + ds_Pos_Sales.Tables[0].Rows[0]["LocalJournalMasterID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["LocalTransactionMasterID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["LocalReturnTransactionMasterID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["LocalCashAmountJournalDetailID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["LocalCard1AmountJournalDetailID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["LocalCard2AmountJournalDetailID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["LocalCard3AmountJournalDetailID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["ServerJournalMasterID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["ServerTransactionMasterID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["ServerReturnTransactionMasterID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["ServerCashAmountJournalDetailID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["ServerCard1AmountJournalDetailID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["ServerCard2AmountJournalDetailID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["ServerCard3AmountJournalDetailID"].ToString() + @"
           ," + ds_Pos_Sales.Tables[0].Rows[0]["StatusID"].ToString() + @"
           ,'" + ds_Pos_Sales.Tables[0].Rows[0]["ExtraField1"].ToString() + @"'
           ,'" + ds_Pos_Sales.Tables[0].Rows[0]["ExtraField2"].ToString() + @"'
           ,'" + ds_Pos_Sales.Tables[0].Rows[0]["ExtraField3"].ToString() + @"'
           ,'" + ds_Pos_Sales.Tables[0].Rows[0]["ExtraField4"].ToString() + @"'
           ,'" + ds_Pos_Sales.Tables[0].Rows[0]["ExtraField5"].ToString() + @"'
           ,'" + ds_Pos_Sales.Tables[0].Rows[0]["ExtraField6"].ToString() + @"'
           ,'" + ds_Pos_Sales.Tables[0].Rows[0]["ExtraField7"].ToString() + @"'
           ,'" + ds_Pos_Sales.Tables[0].Rows[0]["ExtraField8"].ToString() + @"'
           ,'" + ds_Pos_Sales.Tables[0].Rows[0]["ExtraField9"].ToString() + @"'
           ,'" + ds_Pos_Sales.Tables[0].Rows[0]["ExtraField10"].ToString() + @"');

";
         #endregion
        foreach (DataRow dr_Sales in ds_Pos_Sales.Tables[1].Rows)
        {
            #region Pos_SalesDetail
            sql_update += @"
INSERT INTO [Pos_SalesDetail]
           ([Pos_SalesMasterID]
           ,[LocalSalesDetailID]
           ,[RemoteSalesDetailID]
           ,[ProductID]
           ,[Quantity]
           ,[WorkStationID]
           ,[CostPrice]
           ,[SalesPrice]
           ,[DiscountPercentage]
           ,[DiscountAmount]
           ,[VATPercentage]
           ,[VATAmount]
           ,[LocalPos_TransactionMasterID]
           ,[SererPos_TransactionMasterID]
           ,[LocalTransactionID]
           ,[ServerTransactionID]
           ,[LocalPos_SalesMasterID]
           ,[ServerPos_SalesMasterID]
           ,[LocalFinishedGoodAssetJournalDetailID]
           ,[LocalCostOfGoodsSoldJournalDetailID]
           ,[LocalSalesRevinueJournalDetailID]
           ,[LocalVATIncludingJournalDetailID]
           ,[LocalVATExcludingJournalDetailID]
           ,[LocalDiscountJournalDetailID]
           ,[ServerFinishedGoodAssetJournalDetailID]
           ,[ServerCostOfGoodsSoldJournalDetailID]
           ,[ServerSalesRevinueJournalDetailID]
           ,[ServerVATIncludingJournalDetailID]
           ,[ServerVATExcludingJournalDetailID]
           ,[ServerDiscountJournalDetailID]
           ,[StatusID]
           ,[ExtraField1]
           ,[ExtraField2]
           ,[ExtraField3]
           ,[ExtraField4]
           ,[ExtraField5]
           ,[ExtraField6]
           ,[ExtraField7]
           ,[ExtraField8]
           ,[ExtraField9]
           ,[ExtraField10])
     VALUES
           (" + dr_Sales["Pos_SalesMasterID"].ToString() + @"
           ," + dr_Sales["LocalSalesDetailID"].ToString() + @"
           ," + dr_Sales["RemoteSalesDetailID"].ToString() + @"
           ," + dr_Sales["ProductID"].ToString() + @"
           ," + dr_Sales["Quantity"].ToString() + @"
           ," + dr_Sales["WorkStationID"].ToString() + @"
           ," + dr_Sales["CostPrice"].ToString() + @"
           ," + dr_Sales["SalesPrice"].ToString() + @"
           ," + dr_Sales["DiscountPercentage"].ToString() + @"
           ," + dr_Sales["DiscountAmount"].ToString() + @"
           ," + dr_Sales["VATPercentage"].ToString() + @"
           ," + dr_Sales["VATAmount"].ToString() + @"
           ," + dr_Sales["LocalPos_TransactionMasterID"].ToString() + @"
           ," + dr_Sales["SererPos_TransactionMasterID"].ToString() + @"
           ," + dr_Sales["LocalTransactionID"].ToString() + @"
           ," + dr_Sales["ServerTransactionID"].ToString() + @"
           ," + dr_Sales["LocalPos_SalesMasterID"].ToString() + @"
           ," + dr_Sales["ServerPos_SalesMasterID"].ToString() + @"
           ," + dr_Sales["LocalFinishedGoodAssetJournalDetailID"].ToString() + @"
           ," + dr_Sales["LocalCostOfGoodsSoldJournalDetailID"].ToString() + @"
           ," + dr_Sales["LocalSalesRevinueJournalDetailID"].ToString() + @"
           ," + dr_Sales["LocalVATIncludingJournalDetailID"].ToString() + @"
           ," + dr_Sales["LocalVATExcludingJournalDetailID"].ToString() + @"
           ," + dr_Sales["LocalDiscountJournalDetailID"].ToString() + @"
           ," + dr_Sales["ServerFinishedGoodAssetJournalDetailID"].ToString() + @"
           ," + dr_Sales["ServerCostOfGoodsSoldJournalDetailID"].ToString() + @"
           ," + dr_Sales["ServerSalesRevinueJournalDetailID"].ToString() + @"
           ," + dr_Sales["ServerVATIncludingJournalDetailID"].ToString() + @"
           ," + dr_Sales["ServerVATExcludingJournalDetailID"].ToString() + @"
           ," + dr_Sales["ServerDiscountJournalDetailID"].ToString() + @"
           ," + dr_Sales["StatusID"].ToString() + @"
           ,'" + dr_Sales["ExtraField1"].ToString() + @"'
           ,'" + dr_Sales["ExtraField2"].ToString() + @"'
           ,'" + dr_Sales["ExtraField3"].ToString() + @"'
           ,'" + dr_Sales["ExtraField4"].ToString() + @"'
           ,'" + dr_Sales["ExtraField5"].ToString() + @"'
           ,'" + dr_Sales["ExtraField6"].ToString() + @"'
           ,'" + dr_Sales["ExtraField7"].ToString() + @"'
           ,'" + dr_Sales["ExtraField8"].ToString() + @"'
           ,'" + dr_Sales["ExtraField9"].ToString() + @"'
           ,'" + dr_Sales["ExtraField10"].ToString() + @"');
";
             #endregion

        }
        return sql_update;
    }

    private void SaveForRemoteDB(string sql)
    {
        sql = Auto_SQL.add(
             sql //SQLString
           , "1"//Status
           , ConfigurationManager.AppSettings["WorkStationID"] //ForWorkStationID
           , "1" //[ToWorkStationID]
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
    
    private void CompleteTheAutoCalculation()
    {
        //credit card calculation
        //if (txtAmount.Text != "")
        //{
        //    btnAddCardPayment_Click(this, new EventArgs());
        //}
        //cash amount calculation
        //it has been calculated autometically
        //if (txtCashAmount.Text != "")
        //{
        //    if (decimal.Parse(txtCashAmount.Text) != 0)
        //    {
        //        txtCashAmount_TextChanged(this, new EventArgs());
        //    }
        //}
    }
    protected void txtContactNo_TextChanged(object sender, EventArgs e)
    {
        txtCustomerID.Text = "";
        txtCardNo.Text = "";

        SearchCustomer();
    }

    protected void txtCustomerID_TextChanged(object sender, EventArgs e)
    {
        txtContactNo.Text = "";
        txtCardNo.Text = "";
        SearchCustomer();
    }
    protected void txtCardNo_TextChanged(object sender, EventArgs e)
    {
        txtContactNo.Text = "";
        txtCustomerID.Text = "";

        SearchCustomer();
    }

    private void SearchCustomer()
    {
        string sql = "Select Pos_CustomerID,CardNo,CustomerName,Address,Mobile,DiscountPersent from Pos_Customer where RowSatatusID=1 ";
        if (txtCardNo.Text != "")
        {
            sql += " and CardNo='" + txtCardNo.Text+"'";
        }

        if (txtCustomerID.Text != "")
        {
            sql += " and Pos_CustomerID=" + txtCustomerID.Text;
        }

        if (txtContactNo.Text != "")
        {
            sql += " and Mobile='" + txtContactNo.Text + "'";
        }


        DataSet ds = CommonManager.SQLExec(sql);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtCustomerName.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
            txtCustomerAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            txtCardNo.Text = ds.Tables[0].Rows[0]["CardNo"].ToString();
            txtCustomerID.Text = ds.Tables[0].Rows[0]["Pos_CustomerID"].ToString();
            txtContactNo.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
            txtDiscountPercentage.Text = ds.Tables[0].Rows[0]["DiscountPersent"].ToString();
            txtDiscountPercentage.Enabled = false;

            if (txtDiscountPercentage.Text != "")
            {
                btnApplyDiscount_Click(this, new EventArgs());
            }
        }
        else
            txtDiscountPercentage.Enabled = true;

    }
    protected void btnAddToPreview_Click(object sender, EventArgs e)
    {
        List<Pos_Product> takenFromSearchProduct = new List<Pos_Product>();

        foreach (GridViewRow gvr in gvPos_Product.Rows)
        {
            Pos_Product prod = new Pos_Product();

            HiddenField hfPos_ProductID = (HiddenField)gvr.FindControl("hfPos_ProductID");
            
            Label lblProductName = (Label)gvr.FindControl("lblProductName");
            Label lblBarCode = (Label)gvr.FindControl("lblBarCode");
            Label lblSizeName = (Label)gvr.FindControl("lblSizeName");
            Label lblColorName = (Label)gvr.FindControl("lblColorName");
            Label lblProductStatusName = (Label)gvr.FindControl("lblProductStatusName");
            Label lblStock = (Label)gvr.FindControl("lblStock");
            Label lblSalePrice = (Label)gvr.FindControl("lblSalePrice");
            Label lblStockSalePrice = (Label)gvr.FindControl("lblStockSalePrice");

            TextBox txtQty = (TextBox)gvr.FindControl("txtQty");
            TextBox txtVatGrid = (TextBox)gvr.FindControl("txtVatGrid");
            TextBox txtDiscountGrid = (TextBox)gvr.FindControl("txtDiscountGrid");
            TextBox txtDiscountAmountGrid = (TextBox)gvr.FindControl("txtDiscountAmountGrid");
            HiddenField hfVatPercentageAlways = (HiddenField)gvr.FindControl("hfVatPercentageAlways");
            HiddenField hfTotalCostPerProduct = (HiddenField)gvr.FindControl("hfTotalCostPerProduct");

            prod.Pos_ProductID = int.Parse(hfPos_ProductID.Value);
            prod.ProductName = lblProductName.Text;
            prod.BarCode = lblBarCode.Text;
            prod.ExtraField1 = txtQty.Text;
            prod.ExtraField10 = lblSizeName.Text;
            prod.ExtraField2 = lblColorName.Text;
            prod.ExtraField3 = lblProductStatusName.Text;
            prod.ExtraField4 = lblStock.Text;
            decimal acctualSalesAmount = decimal.Parse(lblSalePrice.Text);
            decimal acctualSalesAmountTotal = decimal.Parse(lblSalePrice.Text) * decimal.Parse(txtQty.Text);

            prod.ExtraField5 = (acctualSalesAmount).ToString("0,0");
            prod.ExtraField6 = (decimal.Parse(prod.ExtraField5) * decimal.Parse(txtQty.Text)).ToString("0,0");
            prod.DiscountAmount = decimal.Parse(txtDiscountAmountGrid.Text) != 0 ? (decimal.Parse(txtDiscountAmountGrid.Text) * decimal.Parse(txtQty.Text)) : ((decimal.Parse(prod.ExtraField6)) * (decimal.Parse(txtDiscountGrid.Text) / 100));
            prod.ExtraField8 = (acctualSalesAmountTotal * (decimal.Parse(hfVatPercentageAlways.Value)/100)).ToString("0,0");//  (acctualSalesAmountTotal * decimal.Parse("0.05")).ToString("0,0");// decimal.Parse(txtVatGrid.Text) == 0 ? "0.00" : ((acctualSalesAmountTotal) * (decimal.Parse(txtVatGrid.Text) / 100)).ToString("0,0");
            prod.ExtraField9 = (decimal.Parse(prod.ExtraField6) - prod.DiscountAmount + decimal.Parse(prod.ExtraField8)).ToString("0,0");
            prod.ExtraField7 = "0";//not fund 
            prod.AccesoriesCost = decimal.Parse(prod.ExtraField8);
            prod.FabricsCost = prod.DiscountAmount;
            //rewrite
            prod.ExtraField8 = txtVatGrid.Text;
            prod.DiscountAmount = decimal.Parse(txtDiscountAmountGrid.Text);
            prod.DiscountPercentage = decimal.Parse(txtDiscountGrid.Text);

            if (decimal.Parse(lblStock.Text) < decimal.Parse(txtQty.Text))
            {
                showAlartMessage("For "+lblBarCode.Text+"the Qty is more than the stock");
                continue;
            }

            prod.VatPercentage = decimal.Parse(hfVatPercentageAlways.Value);
            prod.OthersCost = decimal.Parse(hfTotalCostPerProduct.Value); //total cost 
            if (txtQty.Text != "0")
            takenFromSearchProduct.Add(prod);
        }

        List<Pos_Product> takenFromPreviewProduct = new List<Pos_Product>();

        foreach (GridViewRow gvr in gvPreview.Rows)
        {
            Pos_Product prod = new Pos_Product();

            HiddenField hfPos_ProductID = (HiddenField)gvr.FindControl("hfPos_ProductID");

            Label lblProductName = (Label)gvr.FindControl("lblProductName");
            Label lblBarCode = (Label)gvr.FindControl("lblBarCode");
            Label lblSizeName = (Label)gvr.FindControl("lblSizeName");
            Label lblColorName = (Label)gvr.FindControl("lblColorName");
            Label lblProductStatusName = (Label)gvr.FindControl("lblProductStatusName");
            Label lblStock = (Label)gvr.FindControl("lblStock");
            Label lblSalePrice = (Label)gvr.FindControl("lblSalePrice");
            Label lblStockSalePrice = (Label)gvr.FindControl("lblStockSalePrice");

            TextBox txtQty = (TextBox)gvr.FindControl("txtQty");
            TextBox txtVatGrid = (TextBox)gvr.FindControl("txtVatGrid");
            TextBox txtDiscountGrid = (TextBox)gvr.FindControl("txtDiscountGrid");
            TextBox txtDiscountAmountGrid = (TextBox)gvr.FindControl("txtDiscountAmountGrid");
            HiddenField hfVatPercentageAlways = (HiddenField)gvr.FindControl("hfVatPercentageAlways");
            HiddenField hfTotalCostPerProduct = (HiddenField)gvr.FindControl("hfTotalCostPerProduct");

            prod.Pos_ProductID = int.Parse(hfPos_ProductID.Value);
            prod.ProductName = lblProductName.Text;
            prod.BarCode = lblBarCode.Text;
            prod.ExtraField1 = txtQty.Text;
            prod.ExtraField10 = lblSizeName.Text;
            prod.ExtraField2 = lblColorName.Text;
            prod.ExtraField3 = lblProductStatusName.Text;
            prod.ExtraField4 = lblStock.Text;
            decimal acctualSalesAmount = decimal.Parse(lblSalePrice.Text);
            decimal acctualSalesAmountTotal = decimal.Parse(lblSalePrice.Text) * decimal.Parse(txtQty.Text);

            prod.ExtraField5 = (acctualSalesAmount).ToString("0,0");
            prod.ExtraField6 = (decimal.Parse(prod.ExtraField5) * decimal.Parse(txtQty.Text)).ToString("0,0");
            prod.DiscountAmount = decimal.Parse(txtDiscountAmountGrid.Text) != 0 ? (decimal.Parse(txtDiscountAmountGrid.Text) * decimal.Parse(txtQty.Text)) : ((decimal.Parse(prod.ExtraField6)) * (decimal.Parse(txtDiscountGrid.Text) / 100));
            prod.ExtraField8 = (acctualSalesAmountTotal * decimal.Parse("0.05")).ToString("0,0");// decimal.Parse(txtVatGrid.Text) == 0 ? "0.00" : ((acctualSalesAmountTotal) * (decimal.Parse(txtVatGrid.Text) / 100)).ToString("0,0");
            prod.ExtraField9 = (decimal.Parse(prod.ExtraField6) - prod.DiscountAmount + decimal.Parse(prod.ExtraField8)).ToString("0,0");
            
            prod.ExtraField7 = "0";//not fund 
            
            prod.AccesoriesCost = decimal.Parse(prod.ExtraField8);
            prod.FabricsCost = prod.DiscountAmount;
            
            //Rewrite
            prod.ExtraField8 = txtVatGrid.Text;
            prod.DiscountAmount = decimal.Parse(txtDiscountAmountGrid.Text);
            prod.DiscountPercentage = decimal.Parse(txtDiscountGrid.Text);

            prod.VatPercentage = decimal.Parse(hfVatPercentageAlways.Value);
            prod.OthersCost = decimal.Parse(hfTotalCostPerProduct.Value); //total cost 

            if (txtQty.Text!="0")
                takenFromPreviewProduct.Add(prod);
        }

        foreach (Pos_Product previewProd in takenFromPreviewProduct)
        {
            foreach (Pos_Product searchProd in takenFromSearchProduct)
            {
                if (searchProd.Pos_ProductID == previewProd.Pos_ProductID)
                {
                    previewProd.ExtraField6 = searchProd.ExtraField6;
                    previewProd.DiscountAmount = searchProd.DiscountAmount;
                    previewProd.ExtraField8 = searchProd.ExtraField8;
                    previewProd.ExtraField9 = searchProd.ExtraField9;
                    //previewProd.AccesoriesCost = searchProd.AccesoriesCost;
                    previewProd.FabricsCost = decimal.Parse(searchProd.FabricsCost.ToString("0"));

                    previewProd.VatPercentage = searchProd.VatPercentage;
                    previewProd.OthersCost = searchProd.OthersCost;
                    
                    searchProd.ExtraField7 = "1";
                    if (int.Parse(previewProd.ExtraField4) >= int.Parse(previewProd.ExtraField1))
                    {
                        previewProd.AccesoriesCost = ((previewProd.AccesoriesCost / int.Parse(previewProd.ExtraField1)) * (int.Parse(previewProd.ExtraField1) + 1));
                        previewProd.ExtraField1 = (int.Parse(previewProd.ExtraField1) + 1).ToString();
                    }
                }
            }
        }

        foreach (Pos_Product searchProd in takenFromSearchProduct)
        {
            if (searchProd.ExtraField7 == "0")
            {
                takenFromPreviewProduct.Add(searchProd);
            }
        }

        //foreach (Pos_Product prod in takenFromPreviewProduct)
        //{
        //    prod.ExtraField6 = (decimal.Parse(prod.ExtraField5) * decimal.Parse(prod.ExtraField1)).ToString("0,0.00");

        //}

        gvPreview.DataSource = takenFromPreviewProduct;
        gvPreview.DataBind();


        decimal totalStock = 0;
        decimal qty = 0;
        decimal totalAmount = 0;
        decimal SubtotalAmount = 0;
        decimal discountSum = 0;
        decimal vatSum = 0;
        foreach (Pos_Product prod in takenFromPreviewProduct)
        {
            totalAmount += decimal.Parse(prod.ExtraField6);
            SubtotalAmount += decimal.Parse(prod.ExtraField9);
            totalStock += decimal.Parse(prod.ExtraField4);
            qty += decimal.Parse(prod.ExtraField1);
            vatSum += prod.AccesoriesCost;
            discountSum += prod.FabricsCost;
        }

        try
        {
            ((Label)gvPreview.FooterRow.FindControl("lblStockFooter")).Text = totalStock.ToString("0,0");
            ((Label)gvPreview.FooterRow.FindControl("lblStockSalePriceFooter")).Text = totalAmount.ToString("0,0.00");
            ((Label)gvPreview.FooterRow.FindControl("lblQtyFooter")).Text = qty.ToString("0,0");
            ((Label)gvPreview.FooterRow.FindControl("lblSubTotalSalePriceFooter")).Text = SubtotalAmount.ToString("0,0.00");
            ((Label)gvPreview.FooterRow.FindControl("lblDiscountAmountTotalFooter")).Text = discountSum.ToString("0,0.00");
            ((Label)gvPreview.FooterRow.FindControl("lblVatGridTotalFooter")).Text = vatSum.ToString("0,0.00");
        }
        catch (Exception ex)
        { }
        txtSubTotal.Text = totalAmount.ToString("0,0.00");
        txtVat.Text = vatSum.ToString("0,0.00");
        txtDiscount.Text = discountSum.ToString("0,0.00");
        
        lblTotalQty.Text = qty.ToString("0,0");

        txtCashAmount_TextChanged(this, new EventArgs());
        btnApplyDiscount_Click(this, new EventArgs());
    }
    protected void btnAddCardPayment_Click(object sender, EventArgs e)
    {
        List<Pos_Product> paymentList = new List<Pos_Product>();
        decimal totalAmount = 0;
        foreach (GridViewRow gvr in gvPayment.Rows)
        {
            HiddenField hfSerial = (HiddenField)gvr.FindControl("hfSerial");
            HiddenField hfIssueFromID = (HiddenField)gvr.FindControl("hfIssueFromID");
            Label lblCardNo = (Label)gvr.FindControl("lblCardNo");
            Label lblCardType = (Label)gvr.FindControl("lblCardType");
            Label lblIssueFrom = (Label)gvr.FindControl("lblIssueFrom");
            Label lblAmount = (Label)gvr.FindControl("lblAmount");

            Pos_Product PaymentGrid = new Pos_Product();
            PaymentGrid.Pos_ProductID = int.Parse(hfSerial.Value);
            PaymentGrid.ExtraField1 = lblCardNo.Text;
            PaymentGrid.ExtraField2 = lblCardType.Text;
            PaymentGrid.ExtraField3 = lblIssueFrom.Text;
            PaymentGrid.ExtraField4 = hfIssueFromID.Value;
            PaymentGrid.ExtraField5 = lblAmount.Text;
            totalAmount += decimal.Parse(lblAmount.Text);

            paymentList.Add(PaymentGrid);
        }

        Pos_Product newPayment = new Pos_Product();
        newPayment.Pos_ProductID = paymentList.Count;
        newPayment.ExtraField1 = txtCardNoPayment.Text;
        newPayment.ExtraField2 = ddlCardType.SelectedValue;
        newPayment.ExtraField3 = ddlIssueBank.SelectedItem.Text;
        newPayment.ExtraField4 = ddlIssueBank.SelectedValue;
        newPayment.ExtraField5 = txtAmount.Text;
        totalAmount += decimal.Parse(txtAmount.Text);
       
        paymentList.Add(newPayment);

        gvPayment.DataSource = paymentList;
        gvPayment.DataBind();

        txtCardAmount.Text = totalAmount.ToString("0,0.00"); 
        ((Label)gvPayment.FooterRow.FindControl("lblTotalCardAmountFooter")).Text = totalAmount.ToString("0,0.00") ;
        txtCashAmount_TextChanged(this, new EventArgs());
    }

    protected void lbDeletePayment_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id= Convert.ToInt32(linkButton.CommandArgument);

        List<Pos_Product> paymentList = new List<Pos_Product>();
        foreach (GridViewRow gvr in gvPayment.Rows)
        {
            HiddenField hfSerial = (HiddenField)gvr.FindControl("hfSerial");
            HiddenField hfIssueFromID = (HiddenField)gvr.FindControl("hfIssueFromID");
            Label lblCardNo = (Label)gvr.FindControl("lblCardNo");
            Label lblCardType = (Label)gvr.FindControl("lblCardType");
            Label lblIssueFrom = (Label)gvr.FindControl("lblIssueFrom");
            Label lblAmount = (Label)gvr.FindControl("lblAmount");

            Pos_Product PaymentGrid = new Pos_Product();
            PaymentGrid.Pos_ProductID = int.Parse(hfSerial.Value);
            PaymentGrid.ExtraField1 = lblCardNo.Text;
            PaymentGrid.ExtraField2 = lblCardType.Text;
            PaymentGrid.ExtraField3 = lblIssueFrom.Text;
            PaymentGrid.ExtraField4 = hfIssueFromID.Value;
            PaymentGrid.ExtraField5 = lblAmount.Text;

            paymentList.Add(PaymentGrid);
        }

        
        int deleted_i = -1;
        for (int i = 0; i < paymentList.Count; i++)
        {
            if (id == paymentList[i].Pos_ProductID)
            {
                deleted_i = i;
                break;
            }
        }
        if (deleted_i != -1)
        {
            paymentList.Remove(paymentList[deleted_i]);
        }

        gvPayment.DataSource = paymentList;
        gvPayment.DataBind();

        decimal totalAmount = 0;
        foreach (GridViewRow gvr in gvPayment.Rows)
        {
            Label lblAmount = (Label)gvr.FindControl("lblAmount");
            totalAmount += decimal.Parse(lblAmount.Text);
        }

        txtCardAmount.Text = totalAmount.ToString("0,0.00");
        ((Label)gvPayment.FooterRow.FindControl("lblTotalCardAmountFooter")).Text = totalAmount.ToString("0,0.00");
        
        txtCashAmount_TextChanged(this, new EventArgs());   
        
    }

    protected void lbDeletePreviewGrid_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id = Convert.ToInt32(linkButton.CommandArgument);

        List<Pos_Product> takenFromPreviewProduct = new List<Pos_Product>();

        foreach (GridViewRow gvr in gvPreview.Rows)
        {
            Pos_Product prod = new Pos_Product();

            HiddenField hfPos_ProductID = (HiddenField)gvr.FindControl("hfPos_ProductID");

            Label lblProductName = (Label)gvr.FindControl("lblProductName");
            Label lblBarCode = (Label)gvr.FindControl("lblBarCode");
            Label lblSizeName = (Label)gvr.FindControl("lblSizeName");
            Label lblColorName = (Label)gvr.FindControl("lblColorName");
            Label lblProductStatusName = (Label)gvr.FindControl("lblProductStatusName");
            Label lblStock = (Label)gvr.FindControl("lblStock");
            Label lblSalePrice = (Label)gvr.FindControl("lblSalePrice");
            Label lblStockSalePrice = (Label)gvr.FindControl("lblStockSalePrice");

            TextBox txtQty = (TextBox)gvr.FindControl("txtQty");
            TextBox txtVatGrid = (TextBox)gvr.FindControl("txtVatGrid");
            TextBox txtDiscountGrid = (TextBox)gvr.FindControl("txtDiscountGrid");
            TextBox txtDiscountAmountGrid = (TextBox)gvr.FindControl("txtDiscountAmountGrid");

            prod.Pos_ProductID = int.Parse(hfPos_ProductID.Value);
            prod.ProductName = lblProductName.Text;
            prod.BarCode = lblBarCode.Text;
            prod.ExtraField1 = txtQty.Text;
            prod.ExtraField10 = lblSizeName.Text;
            prod.ExtraField2 = lblColorName.Text;
            prod.ExtraField3 = lblProductStatusName.Text;
            prod.ExtraField4 = lblStock.Text;
            prod.ExtraField5 = lblSalePrice.Text;
            prod.ExtraField6 = (decimal.Parse(lblSalePrice.Text) * decimal.Parse(txtQty.Text)).ToString("0,0.00");
            prod.ExtraField7 = "0";//not fund 
            prod.DiscountAmount = decimal.Parse(txtDiscountAmountGrid.Text) != 0 ? (decimal.Parse(txtDiscountAmountGrid.Text)) : ((decimal.Parse(lblSalePrice.Text) * decimal.Parse(txtQty.Text)) * (decimal.Parse(txtDiscountGrid.Text) / 100));
            prod.ExtraField8 = decimal.Parse(txtVatGrid.Text) == 0 ? "0.00" : ((decimal.Parse(prod.ExtraField6) - prod.DiscountAmount) * (decimal.Parse(txtVatGrid.Text) / 100)).ToString("0,0.00");
            prod.ExtraField9 = (decimal.Parse(prod.ExtraField6) - prod.DiscountAmount + decimal.Parse(prod.ExtraField8)).ToString("0,0.00");

            prod.AccesoriesCost = decimal.Parse(prod.ExtraField8);
            prod.FabricsCost = prod.DiscountAmount;

            //Rewrite
            prod.ExtraField8 = txtVatGrid.Text;
            prod.DiscountAmount = decimal.Parse(txtDiscountAmountGrid.Text);
            prod.DiscountPercentage = decimal.Parse(txtDiscountGrid.Text);

            if (txtQty.Text != "0")
                takenFromPreviewProduct.Add(prod);
        }

        int deleted_i = -1;
        for (int i = 0; i < takenFromPreviewProduct.Count; i++)
        {
            if (id == takenFromPreviewProduct[i].Pos_ProductID)
            {
                deleted_i = i;
                break;
            }
        }
        if (deleted_i != -1)
        {
            takenFromPreviewProduct.Remove(takenFromPreviewProduct[deleted_i]);
        }

        gvPreview.DataSource = takenFromPreviewProduct;
        gvPreview.DataBind();

        decimal totalStock = 0;
        decimal qty = 0;
        decimal totalAmount = 0;
        decimal SubtotalAmount = 0;
        decimal discountSum = 0;
        decimal vatSum = 0;
        foreach (Pos_Product prod in takenFromPreviewProduct)
        {
            totalAmount += decimal.Parse(prod.ExtraField6);
            SubtotalAmount += decimal.Parse(prod.ExtraField9);
            totalStock += decimal.Parse(prod.ExtraField4);
            qty += decimal.Parse(prod.ExtraField1);
            vatSum += prod.AccesoriesCost;
            discountSum += prod.FabricsCost;
        }

        ((Label)gvPreview.FooterRow.FindControl("lblStockFooter")).Text = totalStock.ToString("0,0");
        ((Label)gvPreview.FooterRow.FindControl("lblStockSalePriceFooter")).Text = totalAmount.ToString("0,0.00");
        ((Label)gvPreview.FooterRow.FindControl("lblQtyFooter")).Text = qty.ToString("0,0");
        ((Label)gvPreview.FooterRow.FindControl("lblSubTotalSalePriceFooter")).Text = SubtotalAmount.ToString("0,0.00");
        ((Label)gvPreview.FooterRow.FindControl("lblDiscountAmountTotalFooter")).Text = discountSum.ToString("0,0.00");
        ((Label)gvPreview.FooterRow.FindControl("lblVatGridTotalFooter")).Text = vatSum.ToString("0,0.00");

        txtSubTotal.Text = totalAmount.ToString("0,0.00");
        txtVat.Text = vatSum.ToString("0,0.00");
        txtDiscount.Text = discountSum.ToString("0,0.00");
        
        lblTotalQty.Text = qty.ToString("0,0");

        txtCashAmount_TextChanged(this, new EventArgs());
    }

    private void recalculate()
    {
        List<Pos_Product> takenFromPreviewProduct = new List<Pos_Product>();

        foreach (GridViewRow gvr in gvPreview.Rows)
        {
            Pos_Product prod = new Pos_Product();

            HiddenField hfPos_ProductID = (HiddenField)gvr.FindControl("hfPos_ProductID");

            Label lblProductName = (Label)gvr.FindControl("lblProductName");
            Label lblBarCode = (Label)gvr.FindControl("lblBarCode");
            Label lblSizeName = (Label)gvr.FindControl("lblSizeName");
            Label lblColorName = (Label)gvr.FindControl("lblColorName");
            Label lblProductStatusName = (Label)gvr.FindControl("lblProductStatusName");
            Label lblStock = (Label)gvr.FindControl("lblStock");
            Label lblSalePrice = (Label)gvr.FindControl("lblSalePrice");
            Label lblStockSalePrice = (Label)gvr.FindControl("lblStockSalePrice");

            TextBox txtQty = (TextBox)gvr.FindControl("txtQty");
            TextBox txtVatGrid = (TextBox)gvr.FindControl("txtVatGrid");
            TextBox txtDiscountGrid = (TextBox)gvr.FindControl("txtDiscountGrid");
            TextBox txtDiscountAmountGrid = (TextBox)gvr.FindControl("txtDiscountAmountGrid");
            TextBox txtVatGridTotal = (TextBox)gvr.FindControl("txtVatGridTotal");

            if (decimal.Parse(lblStock.Text) < decimal.Parse(txtQty.Text))
            {
                showAlartMessage(lblBarCode.Text +" Stock is less than your given Quantity");
                continue;
            }


            prod.Pos_ProductID = int.Parse(hfPos_ProductID.Value);
            prod.ProductName = lblProductName.Text;
            prod.BarCode = lblBarCode.Text;
            prod.ExtraField1 = txtQty.Text;
            prod.ExtraField10 = lblSizeName.Text;
            prod.ExtraField2 = lblColorName.Text;
            prod.ExtraField3 = lblProductStatusName.Text;
            prod.ExtraField4 = lblStock.Text;

            decimal acctualSalesAmount = decimal.Parse(lblSalePrice.Text);
            decimal acctualSalesAmountTotal = decimal.Parse(lblSalePrice.Text) * decimal.Parse(txtQty.Text);

            prod.ExtraField5 = (acctualSalesAmount).ToString("0,0");
            prod.ExtraField6 = (decimal.Parse(prod.ExtraField5) * decimal.Parse(txtQty.Text)).ToString("0,0");
            prod.DiscountAmount = decimal.Parse(txtDiscountAmountGrid.Text) != 0 ? (decimal.Parse(txtDiscountAmountGrid.Text) * decimal.Parse(txtQty.Text)) : ((decimal.Parse(prod.ExtraField6)) * (decimal.Parse(txtDiscountGrid.Text) / 100));
            prod.DiscountAmount = decimal.Parse(prod.DiscountAmount.ToString("0,0"));
            prod.ExtraField8 = decimal.Parse(txtVatGridTotal.Text).ToString("0,0");
            prod.ExtraField9 = (decimal.Parse(prod.ExtraField6) - prod.DiscountAmount + decimal.Parse(prod.ExtraField8)).ToString("0,0");
            prod.ExtraField7 = "0";//not fund 
            
            
            
            prod.AccesoriesCost = decimal.Parse(prod.ExtraField8);
            prod.ExtraField8 = txtVatGrid.Text;
            prod.FabricsCost = prod.DiscountAmount;

            //Rewrite
            //prod.ExtraField8 = txtVatGrid.Text;
            prod.DiscountAmount = decimal.Parse(txtDiscountAmountGrid.Text);
            prod.DiscountPercentage = decimal.Parse(txtDiscountGrid.Text);

            if (txtQty.Text != "0")
                takenFromPreviewProduct.Add(prod);
        }


        gvPreview.DataSource = takenFromPreviewProduct;
        gvPreview.DataBind();

        decimal totalStock = 0;
        decimal qty = 0;
        decimal totalAmount = 0;
        decimal SubtotalAmount = 0;
        decimal discountSum = 0;
        decimal vatSum = 0;
        foreach (Pos_Product prod in takenFromPreviewProduct)
        {
            totalAmount += decimal.Parse(prod.ExtraField6);
            SubtotalAmount += decimal.Parse(prod.ExtraField9);
            totalStock += decimal.Parse(prod.ExtraField4);
            qty += decimal.Parse(prod.ExtraField1);
            vatSum += prod.AccesoriesCost;
            discountSum += prod.FabricsCost;
        }

        ((Label)gvPreview.FooterRow.FindControl("lblStockFooter")).Text = totalStock.ToString("0,0");
        ((Label)gvPreview.FooterRow.FindControl("lblStockSalePriceFooter")).Text = totalAmount.ToString("0,0.00");
        ((Label)gvPreview.FooterRow.FindControl("lblQtyFooter")).Text = qty.ToString("0,0");
        ((Label)gvPreview.FooterRow.FindControl("lblSubTotalSalePriceFooter")).Text = SubtotalAmount.ToString("0,0.00");
        ((Label)gvPreview.FooterRow.FindControl("lblDiscountAmountTotalFooter")).Text = discountSum.ToString("0,0.00");
        ((Label)gvPreview.FooterRow.FindControl("lblVatGridTotalFooter")).Text = vatSum.ToString("0,0.00");

        txtSubTotal.Text = totalAmount.ToString("0,0.00");
        txtVat.Text = vatSum.ToString("0,0.00");
        txtDiscount.Text = discountSum.ToString("0,0.00");
        

        lblTotalQty.Text = qty.ToString("0,0");

        txtCashAmount_TextChanged(this, new EventArgs());
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        recalculate();
    }

    protected void ddlPaymentMood_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentMood.SelectedValue == "Cash")
        {
            btnAddCardPayment.Enabled = false;
            txtCashAmount.Enabled = true;
            
        }
        else if (ddlPaymentMood.SelectedValue == "Credit")
        {
            btnAddCardPayment.Enabled = true;
            txtCashAmount.Enabled = false;
        
        }
        else if (ddlPaymentMood.SelectedValue == "Cash & Credit")
        {
            btnAddCardPayment.Enabled = true;
            txtCashAmount.Enabled = true;

        }
    }

    protected void txtCashAmount_TextChanged(object sender, EventArgs e)
    {
        if ((decimal.Parse(txtCashAmount.Text) + decimal.Parse(txtReturnAmount.Text) + decimal.Parse(txtCardAmount.Text) - decimal.Parse(txtPaidAmount.Text)) >= 0)
        {
            txtRefundOrDue.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            txtRefundOrDue.ForeColor = System.Drawing.Color.Red;
        }

        txtPayableAmount.Text = (decimal.Parse(txtSubTotal.Text) - decimal.Parse(txtDiscount.Text) + decimal.Parse(txtVat.Text)).ToString("0,0.00");
        txtPaidAmount.Text = txtPayableAmount.Text;

        txtRefundOrDue.Text = (decimal.Parse(txtCashAmount.Text) + decimal.Parse(txtReturnAmount.Text) + decimal.Parse(txtCardAmount.Text) - decimal.Parse(txtPaidAmount.Text)).ToString("0,0.00");
    }

    protected void btnApplyDiscount_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvPreview.Rows)
        {
            TextBox txtVatGrid = (TextBox)gvr.FindControl("txtVatGrid");
            TextBox txtDiscountGrid = (TextBox)gvr.FindControl("txtDiscountGrid");
            TextBox txtDiscountAmountGrid = (TextBox)gvr.FindControl("txtDiscountAmountGrid");

            HiddenField hfVatGrid = (HiddenField)gvr.FindControl("hfVatGrid");
            HiddenField hfDiscountGrid = (HiddenField)gvr.FindControl("hfDiscountGrid");
            HiddenField hfDiscountAmountGrid = (HiddenField)gvr.FindControl("hfDiscountAmountGrid");

            hfVatGrid.Value = txtVatGrid.Text;
            hfDiscountGrid.Value = txtDiscountGrid.Text;
            hfDiscountAmountGrid.Value = txtDiscountAmountGrid.Text;

            txtDiscountGrid.Text = txtDiscountPercentage.Text;
            txtDiscountAmountGrid.Text = "0.00";
        }

        recalculate();
    }
    protected void btnAuto_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvPreview.Rows)
        {
            TextBox txtVatGrid = (TextBox)gvr.FindControl("txtVatGrid");
            TextBox txtDiscountGrid = (TextBox)gvr.FindControl("txtDiscountGrid");
            TextBox txtDiscountAmountGrid = (TextBox)gvr.FindControl("txtDiscountAmountGrid");

            HiddenField hfVatGrid = (HiddenField)gvr.FindControl("hfVatGrid");
            HiddenField hfDiscountGrid = (HiddenField)gvr.FindControl("hfDiscountGrid");
            HiddenField hfDiscountAmountGrid = (HiddenField)gvr.FindControl("hfDiscountAmountGrid");

            txtVatGrid.Text=hfVatGrid.Value;
            txtDiscountGrid.Text=hfDiscountGrid.Value ;
            txtDiscountAmountGrid.Text=hfDiscountAmountGrid.Value;
        }

        recalculate();
    }
    protected void txtBarCode_TextChanged(object sender, EventArgs e)
    {
        txtBarCode.Text = txtBarCode.Text.Trim();
        btnSearch_Click(this, new EventArgs());
        if (txtBarCode.Text != "")
        {
            btnAddToPreview_Click(this, new EventArgs());
            txtBarCode.Text = "";
        }
    }
    protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
    {
        string sql = @"Select Pos_TransactionMasterID from Pos_TransactionMaster where TransactionID=" + txtInvoiceNo.Text + " and Pos_TransactionTypeID=13"
         +" and WorkSatationID=" + ddlWorkStationSearch.SelectedValue;
        DataSet ds = CommonManager.SQLExec(sql);
        if (ds.Tables[0].Rows.Count == 0)
        {
            showAlartMessage("Invalid Invoice # for this branch");
            return;
        }

        hfInvoiceNo.Value = ds.Tables[0].Rows[0][0].ToString();
        
        sql = @"
SELECT Pos_Transaction.Pos_TransactionID,
Pos_TransactionMaster.TransactionID,
Pos_TransactionMaster.ExtraField4 as CustomerMobileNo,
Pos_TransactionMaster.Pos_TransactionMasterID,
Pos_Product.StyleCode as Style,Pos_Product.BarCode
,Pos_Size.SizeName,Pos_Color.ColorName
,Pos_Product.ProductName,Pos_Product.Pos_ProductID
,(Pos_Transaction.Quantity - cast(Pos_Transaction.ExtraField4  as decimal(10,2))) as [Stock]
,((Pos_Product.SalePrice * 100) /105) as SalePrice
,((cast(Pos_Transaction.ExtraField1 as decimal(10,2))/Pos_Transaction.Quantity)*(Pos_Transaction.Quantity - cast(Pos_Transaction.ExtraField4  as decimal(10,2)))) as DiscountAmount
,((cast(Pos_Transaction.ExtraField2 as decimal(10,2))/Pos_Transaction.Quantity)*(Pos_Transaction.Quantity - cast(Pos_Transaction.ExtraField4  as decimal(10,2)))) as VatAmount
,(((Pos_Transaction.Quantity - cast(Pos_Transaction.ExtraField4  as decimal(10,2))) * ((Pos_Product.SalePrice * 100) /105)) - ((cast(Pos_Transaction.ExtraField1 as decimal(10,2))/Pos_Transaction.Quantity)*(Pos_Transaction.Quantity - cast(Pos_Transaction.ExtraField4  as decimal(10,2)))) +((cast(Pos_Transaction.ExtraField2 as decimal(10,2))/Pos_Transaction.Quantity)*(Pos_Transaction.Quantity - cast(Pos_Transaction.ExtraField4  as decimal(10,2))))) as Total
      ,((Pos_Transaction.Quantity - cast(Pos_Transaction.ExtraField4  as decimal(10,2))) * ((Pos_Product.SalePrice * 100) /105)) as StockSalePrice
,[VatPercentageAlways] = cast(ACC_ChartOfAccountLabel4.ExtraField3 as decimal(10,2))  
,[TotalCostPerProduct] =([Pos_Product].[FabricsCost] + [Pos_Product].[AccesoriesCost] + [Pos_Product].[Overhead]+[Pos_Product].[OthersCost]+[Pos_Product].[PurchasePrice])
      
FROM Pos_Product
  inner join ACC_ChartOfAccountLabel4 on Pos_Product.ProductID=ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
  inner join Pos_Size on Pos_Product.Pos_SizeID=Pos_Size.Pos_SizeID
  inner join Pos_Color on Pos_Product.Pos_ColorID=Pos_Color.Pos_ColorID
inner join Pos_Transaction on Pos_Transaction.Pos_ProductID =Pos_Product.Pos_ProductID
inner join Pos_TransactionMaster on Pos_TransactionMaster.Pos_TransactionMasterID = Pos_Transaction.Pos_ProductTransactionMasterID
 where   Pos_TransactionMaster.Pos_TransactionMasterID =" + ds.Tables[0].Rows[0][0].ToString() + @"
--and  Pos_Transaction.Quantity > cast(Pos_Transaction.ExtraField4  as decimal(10,2))
and  Pos_Transaction.ExtraField4='0'
order by Pos_Product.StyleCode,Pos_Product.BarCode
";

        ds = CommonManager.SQLExec(sql);
        gvReturnInvoice.DataSource = CommonManager.SQLExec(sql).Tables[0];
        gvReturnInvoice.DataBind();

        decimal soldQty = 0;
        decimal salePrice = 0;
        decimal vat = 0;
        decimal discount = 0;
        decimal total = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            soldQty += decimal.Parse(dr["Stock"].ToString());
            salePrice += decimal.Parse(decimal.Parse(dr["StockSalePrice"].ToString()).ToString("0"));
            vat += decimal.Parse(dr["VatAmount"].ToString());
            discount += decimal.Parse(dr["DiscountAmount"].ToString());
            total += decimal.Parse(decimal.Parse(dr["Total"].ToString()).ToString("0"));
        }
        try
        {
            ((Label)gvReturnInvoice.FooterRow.FindControl("lblSoldQtyFooter")).Text = soldQty.ToString("0");
            ((Label)gvReturnInvoice.FooterRow.FindControl("lblStockSalePriceFooter")).Text = salePrice.ToString("0,0");
            ((Label)gvReturnInvoice.FooterRow.FindControl("lblVatFooter")).Text = vat.ToString("0,0");
            ((Label)gvReturnInvoice.FooterRow.FindControl("lblDiscountFooter")).Text = discount.ToString("0,0");
            ((Label)gvReturnInvoice.FooterRow.FindControl("lblTotalFooter")).Text = total.ToString("0,0");
        }
        catch (Exception ex)
        { }
    }

    protected void btnClearReturn_Click(object sender, EventArgs e)
    {
        txtBarCodeSalesReturn.Text = "";
        txtInvoiceNo.Text = "";
        loadReturnGrid("''");
    }

    protected void txtBarCodeSalesReturn_TextChanged(object sender, EventArgs e)
    {
        if (txtBarCodeSalesReturn.Text == "")
        {
            return;
        }

        if (!hfBarcodeReturn.Value.Contains("'" + txtBarCodeSalesReturn.Text + "'"))
        {
            hfBarcodeReturn.Value += (hfBarcodeReturn.Value == "" ? "" : ",") + "'" + txtBarCodeSalesReturn.Text + "'";
        }

        loadReturnGrid(hfBarcodeReturn.Value);
    }

    private void loadReturnGrid(string barcode)
    {

        string sql = @"
            SELECT Pos_Transaction.Pos_TransactionID,
Pos_TransactionMaster.TransactionID,
Pos_TransactionMaster.ExtraField4 as CustomerMobileNo,
Pos_TransactionMaster.Pos_TransactionMasterID,
            Pos_Product.StyleCode as Style,Pos_Product.BarCode
            ,Pos_Size.SizeName,Pos_Color.ColorName
            ,Pos_Product.ProductName,Pos_Product.Pos_ProductID
           ,(Pos_Transaction.Quantity - cast(Pos_Transaction.ExtraField4  as decimal(10,2))) as [Stock]
,((Pos_Product.SalePrice * 100) /105) as  SalePrice 
,((cast(Pos_Transaction.ExtraField1 as decimal(10,2))/Pos_Transaction.Quantity)*(Pos_Transaction.Quantity - cast(Pos_Transaction.ExtraField4  as decimal(10,2)))) as DiscountAmount
,((cast(Pos_Transaction.ExtraField2 as decimal(10,2))/Pos_Transaction.Quantity)*(Pos_Transaction.Quantity - cast(Pos_Transaction.ExtraField4  as decimal(10,2)))) as VatAmount
,(((Pos_Transaction.Quantity - cast(Pos_Transaction.ExtraField4  as decimal(10,2))) * (((Pos_Product.SalePrice * 100) /105))) - ((cast(Pos_Transaction.ExtraField1 as decimal(10,2))/Pos_Transaction.Quantity)*(Pos_Transaction.Quantity - cast(Pos_Transaction.ExtraField4  as decimal(10,2)))) +((cast(Pos_Transaction.ExtraField2 as decimal(10,2))/Pos_Transaction.Quantity)*(Pos_Transaction.Quantity - cast(Pos_Transaction.ExtraField4  as decimal(10,2))))) as Total
      ,((Pos_Transaction.Quantity - cast(Pos_Transaction.ExtraField4  as decimal(10,2))) * (((Pos_Product.SalePrice * 100) /105))) as StockSalePrice
,[VatPercentageAlways] =cast(ACC_ChartOfAccountLabel4.ExtraField3 as decimal(10,2))  
,[TotalCostPerProduct] =([Pos_Product].[FabricsCost] + [Pos_Product].[AccesoriesCost] + [Pos_Product].[Overhead]+[Pos_Product].[OthersCost]+[Pos_Product].[PurchasePrice])
         FROM Pos_Product
           inner join ACC_ChartOfAccountLabel4 on Pos_Product.ProductID=ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
     inner join Pos_Size on Pos_Product.Pos_SizeID=Pos_Size.Pos_SizeID
              inner join Pos_Color on Pos_Product.Pos_ColorID=Pos_Color.Pos_ColorID
            inner join Pos_Transaction on Pos_Transaction.Pos_ProductID =Pos_Product.Pos_ProductID
            inner join Pos_TransactionMaster on Pos_TransactionMaster.Pos_TransactionMasterID = Pos_Transaction.Pos_ProductTransactionMasterID
             where Pos_Product.BarCode in (" + barcode + @") and Pos_TransactionMaster.Pos_TransactionTypeID=13 and Pos_TransactionMaster.WorkSatationID =" + ddlWorkStationSearch.SelectedValue + @"
--and  Pos_Transaction.Quantity > cast(Pos_Transaction.ExtraField4  as decimal(10,2))
and  Pos_Transaction.ExtraField4='0'
order by Pos_Product.StyleCode,Pos_Product.BarCode
            ";

        DataSet ds = CommonManager.SQLExec(sql);
        gvReturnInvoice.DataSource = CommonManager.SQLExec(sql).Tables[0];
        gvReturnInvoice.DataBind();

        decimal soldQty = 0;
        decimal salePrice = 0;
        decimal vat = 0;
        decimal discount = 0;
        decimal total = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            soldQty += decimal.Parse(dr["Stock"].ToString());
            salePrice += decimal.Parse(dr["StockSalePrice"].ToString());
            vat += decimal.Parse(dr["VatAmount"].ToString());
            discount += decimal.Parse(dr["DiscountAmount"].ToString());
            total += decimal.Parse(dr["Total"].ToString());
        }

        foreach (GridViewRow gvr in gvReturnInvoice.Rows)
        {
            TextBox txtRtnQty = (TextBox)gvr.FindControl("txtRtnQty");

            txtRtnQty.Text = "1";
        }
        //txtRtnQty
        try
        {
            ((Label)gvReturnInvoice.FooterRow.FindControl("lblSoldQtyFooter")).Text = soldQty.ToString("0");
            ((Label)gvReturnInvoice.FooterRow.FindControl("lblStockSalePriceFooter")).Text = salePrice.ToString("0,0.00");
            ((Label)gvReturnInvoice.FooterRow.FindControl("lblVatFooter")).Text = vat.ToString("0,0.00");
            ((Label)gvReturnInvoice.FooterRow.FindControl("lblDiscountFooter")).Text = discount.ToString("0,0.00");
            ((Label)gvReturnInvoice.FooterRow.FindControl("lblTotalFooter")).Text = total.ToString("0,0.00");
        }
        catch (Exception ex)
        { }
    }

    protected void btnProcessReturn_Click(object sender, EventArgs e)
    {
        //Process return transaction master and transaction
        //increatse the qty in showroom
        //Journla process
        
        decimal totalReturnAmount = 0;
        
        foreach (GridViewRow gvr in gvReturnInvoice.Rows)
        {
            TextBox txtRtnQty = (TextBox)gvr.FindControl("txtRtnQty");
            Label lblSoldQty = (Label)gvr.FindControl("lblSoldQty");
            
            
            if (txtRtnQty.Text != "0" && txtRtnQty.Text != "")
            {
                Label lblTotal = (Label)gvr.FindControl("lblTotal");
                totalReturnAmount += ((decimal.Parse(lblTotal.Text) / decimal.Parse(lblSoldQty.Text)) * decimal.Parse(txtRtnQty.Text));
                txtRtnQty.Enabled = false;
            }
        }

        txtReturnAmount.Text = decimal.Parse(totalReturnAmount.ToString("0,0")).ToString("0,0.00");
        txtCashAmount_TextChanged(this, new EventArgs());
    }

    protected void ddlCardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCardType.SelectedValue == "AMEX")
        {

            if (txtDiscountPercentage.Text.Trim() == "")
            {
                txtDiscountPercentage.Text = "0";
            }

            if (decimal.Parse(txtDiscountPercentage.Text) == 0)
            {
                txtDiscountPercentage.Text = "10.00";
                btnApplyDiscount_Click(this, new EventArgs());
            }
        }
    }
}
