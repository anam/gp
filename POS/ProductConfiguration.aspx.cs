using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class AdminPos_ProductInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadLoginInHiddenField();
            initialLoad();
            loadACC_ChartOfAccountLabel4();
            loadReference();
            loadPos_ProductType();
            loadInv_UtilizationDetails();
            loadProductStatus();
            loadPos_Size();
            loadPos_Brand();
            loadInv_QuantityUnit();
            loadPos_Color();
            loadPos_FabricsType();
            loadRowStatus();
            if (Request.QueryString["pos_ProductID"] != null)
            {
                int pos_ProductID = Int32.Parse(Request.QueryString["pos_ProductID"]);
                if (pos_ProductID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_ProductData();
                }
            }
        }
    }

    private void initialLoad()
    {
        txtProductionDate.Text = DateTime.Today.ToString("dd MMM yyyy");
        String sql = @"select MAX(TransactionID) from Pos_TransactionMaster 
                        where Pos_TransactionTypeID =";
        if (Request.QueryString["IsReGenerateBarcode"] != null)
        {
            tblProduction1.Visible = false;
            tblProduction2.Visible = false;
            tblPurchase1.Visible = true;
            btnSearch.Visible = false;
            btnPurchaseProcess.Visible = true;
            tr_SuppierID.Visible = false;
            sql+="25";
        }
        else if (Request.QueryString["IsPurchased"] != null)
        {
            tblProduction1.Visible = false;
            tblProduction2.Visible = false;
            tblPurchase1.Visible = true;
            btnSearch.Visible = false;
            btnPurchaseProcess.Visible = true;
            sql+="25";
        }
        else
        {
            btnSearch.Visible = true;
            btnPurchaseProcess.Visible = false;
            tblProduction1.Visible = true;
            tblProduction2.Visible = true;
            tblPurchase1.Visible = false;
            sql += "1";
        }

        DataSet ds = CommonManager.SQLExec(sql);

        if (ds.Tables[0].Rows.Count == 0)
        {
            txtLastTransactionID.Text = "0";
        }
        else
        {
            txtLastTransactionID.Text = ds.Tables[0].Rows[0][0].ToString();
        }

        int transactionTypeID = (Request.QueryString["IsPurchased"] == null ? 1 : 25);

        try
        {
            lblVoucherType.Text = Pos_TransactionTypeManager.GetPos_TransactionTypeByID(transactionTypeID).TransactionTypeName;
            sql = "select case Count(TransactionID) when 0 then 1 Else (MAX(TransactionID)+1) end from Pos_TransactionMaster where Pos_TransactionTypeID=" + transactionTypeID.ToString() + " and WorkSatationID=1";
            lblVoucherType.Text += "(" + CommonManager.SQLExec(sql).Tables[0].Rows[0][0].ToString() + ")";
        }
        catch (Exception ex)
        {
            lblVoucherType.Text = "Barcode Generate";
        }
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
    protected void txtOldTrasactionID_TextChanged(object sender, EventArgs e)
    {
        if (txtOldTrasactionID.Text == "0" || txtOldTrasactionID.Text == "")
        {
            hfTransactionMasterID.Value = "0";
            hfJournalMasterID.Value = "0";
        }
        else
        {
            hfTransactionMasterID.Value = GetTransactionMasterID().Split('@')[0];
            hfJournalMasterID.Value = GetTransactionMasterID().Split('@')[1];
        }
    }

    private string GetTransactionMasterID()
    {
        txtOldTrasactionID.Text = txtOldTrasactionID.Text.Trim();

        string transactionMasterID = "0";
        string sqlTransaction = "select Pos_TransactionMasterID from Pos_TransactionMaster where TransactionID=" + txtOldTrasactionID.Text
                   + " and Pos_TransactionTypeID =" + (Request.QueryString["IsPurchased"] == null ? "1;Select 0;" : "25;Select ACC_JournalMasterID from ACC_JournalMaster where Note='Product Purchase-"+txtOldTrasactionID.Text+"';");

        
        DataSet ds = CommonManager.SQLExec(sqlTransaction);
        if (ds.Tables[0].Rows.Count == 0)
        {
            return "0@0";
        }
        else
        {
            transactionMasterID = ds.Tables[0].Rows[0][0].ToString() + "@" + ds.Tables[1].Rows[0][0].ToString();
        }

        return transactionMasterID;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (barCodeChecking())
        {
            return;
        }

        int TransactionMasterID = 0;
        int JournalMasterID = 0;
        if (txtOldTrasactionID.Text != "")
        {
            if (hfTransactionMasterID.Value == "0")
            {
                
                TransactionMasterID = int.Parse(GetTransactionMasterID().Split('@')[0]);
                JournalMasterID = int.Parse(GetTransactionMasterID().Split('@')[1]);
            }
            else
            {
                TransactionMasterID = int.Parse(hfTransactionMasterID.Value);
                JournalMasterID = int.Parse(hfJournalMasterID.Value);
            }

            if (TransactionMasterID == 0)
            {
                showAlartMessage("Wrong old ID");
                return;
            }
        }
        else
        {
            //Regenerate ta kon transaction master er under e probe?

            Pos_TransactionMaster pos_TransactionMaster = new Pos_TransactionMaster();

            pos_TransactionMaster.TransactionDate = DateTime.Parse(txtProductionDate.Text);
            pos_TransactionMaster.TransactionID = 0;
            pos_TransactionMaster.Pos_TransactionTypeID = (Request.QueryString["IsPurchased"] == null ? 1 : 25);
            pos_TransactionMaster.ToOrFromID = (Request.QueryString["IsPurchased"] != null ? int.Parse(ddlSuppier.SelectedValue) : int.Parse(ddlProductionUnit.SelectedValue));
            pos_TransactionMaster.Record = "";
            pos_TransactionMaster.Particulars = txtNote.Text;
            pos_TransactionMaster.WorkSatationID = (Request.QueryString["IsPurchased"] != null ? 1 : int.Parse(ddlProductionUnit.SelectedValue)); ;
            pos_TransactionMaster.ExtraField1 = "";
            pos_TransactionMaster.ExtraField2 = "";
            pos_TransactionMaster.ExtraField3 = "";
            pos_TransactionMaster.ExtraField4 = "";
            pos_TransactionMaster.ExtraField5 = "";
            pos_TransactionMaster.AddedBy = getLogin().LoginID;
            pos_TransactionMaster.AddedDate = DateTime.Now;
            pos_TransactionMaster.UpdatedBy = getLogin().LoginID;
            pos_TransactionMaster.UpdatedDate = DateTime.Now;
            pos_TransactionMaster.RowStatusID = 1;
            TransactionMasterID = Pos_TransactionMasterManager.InsertPos_TransactionMaster(pos_TransactionMaster);
            pos_TransactionMaster = Pos_TransactionMasterManager.GetPos_TransactionMasterByID(TransactionMasterID);
            
            txtOldTrasactionID.Text = pos_TransactionMaster.TransactionID.ToString();
            hfTransactionMasterID.Value = TransactionMasterID.ToString();

            if (Request.QueryString["IsPurchased"] != null)
            {
                //Journal Entry
                ACC_JournalMaster aCC_JournalMaster = new ACC_JournalMaster();

                aCC_JournalMaster.JournalMasterName = "3";//Journal Voucher
                aCC_JournalMaster.ExtraField1 = ddlSuppier.SelectedItem.Text;
                aCC_JournalMaster.ExtraField2 = "";
                aCC_JournalMaster.ExtraField3 = "";
                aCC_JournalMaster.Note = "Product Purchase-" + pos_TransactionMaster.TransactionID.ToString();
                aCC_JournalMaster.JournalDate = pos_TransactionMaster.TransactionDate;
                aCC_JournalMaster.AddedBy = getLogin().LoginID;
                aCC_JournalMaster.AddedDate = DateTime.Now;
                aCC_JournalMaster.UpdatedBy = getLogin().LoginID;
                aCC_JournalMaster.UpdatedDate = DateTime.Now;
                aCC_JournalMaster.RowStatusID = 1;

                JournalMasterID = ACC_JournalMasterManager.InsertACC_JournalMaster(aCC_JournalMaster);
                hfJournalMasterID.Value = JournalMasterID.ToString();
            }
        }
        Pos_Product pos_Product = new Pos_Product();
        pos_Product.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        pos_Product.ReferenceID = TransactionMasterID;
        pos_Product.Pos_ProductTypeID = 1;
        pos_Product.ProductStatusID = Int32.Parse(ddlProductStatus.SelectedValue);
        pos_Product.ProductName = ddlProduct.SelectedItem.Text;
        pos_Product.Pos_BrandID = Int32.Parse(ddlPos_Brand.SelectedValue);
        pos_Product.Inv_QuantityUnitID = Int32.Parse(ddlInv_QuantityUnit.SelectedValue);
        
        pos_Product.Note = txtNote.Text;
        
        pos_Product.Pic1 = txtPicture.Value;
        pos_Product.Pic2 = "";
        pos_Product.Pic3 = "";
        pos_Product.VatPercentage = Decimal.Parse(txtVatPercentage.Text);
        pos_Product.IsVatExclusive = cbIsVatExclusive.Checked;
        pos_Product.DiscountPercentage = Decimal.Parse(txtDiscountPercentage.Text);
        pos_Product.FabricsNo = ddlProductionUnit.SelectedValue;
        pos_Product.ExtraField1 = "";
        pos_Product.ExtraField2 = "";
        pos_Product.ExtraField3 = "";
        pos_Product.ExtraField4 = "";
        pos_Product.ExtraField5 = "";
        pos_Product.ExtraField6 = "";
        pos_Product.ExtraField7 = "";
        pos_Product.ExtraField8 = "";
        pos_Product.ExtraField9 = ddlSuppier.SelectedValue;
        pos_Product.ExtraField10 = JournalMasterID.ToString();
        pos_Product.AddedBy = getLogin().LoginID;
        pos_Product.AddedDate = DateTime.Now;
        pos_Product.UpdatedBy = getLogin().LoginID;
        pos_Product.UpdatedDate = DateTime.Now;
        pos_Product.RowStatusID = 1;
        string ids = "";
        foreach (GridViewRow gvr in gvProductConfiguration.Rows)
        {
            /*
            <asp:HiddenField ID="hfColorID" runat="server" Value='<%#Eval("Pos_ColorID") %>'/>
                                <asp:HiddenField ID="hfPos_SizeID" runat="server" Value='<%#Eval("Pos_SizeID") %>'/>
                                <asp:HiddenField ID="hfFabricsCost" runat="server" Value='<%#Eval("FabricsCost") %>'/>
                                <asp:HiddenField ID="hfAccesoriesCost" runat="server" Value='<%#Eval("AccesoriesCost") %>'/>
                                <asp:HiddenField ID="hfOverhead" runat="server" Value='<%#Eval("Overhead") %>'/>
                                <asp:HiddenField ID="hfOthersCost" runat="server" Value='<%#Eval("OthersCost") %>'/>
                                <asp:HiddenField ID="hfUtilizationIDs" runat="server" Value='<%#Eval("Inv_UtilizationDetailsIDs") %>'/>
            */
            HiddenField hfColorID = (HiddenField)gvr.FindControl("hfColorID");
            HiddenField hfPos_SizeID = (HiddenField)gvr.FindControl("hfPos_SizeID");
            HiddenField hfFabricsCost = (HiddenField)gvr.FindControl("hfFabricsCost");
            HiddenField hfAccesoriesCost = (HiddenField)gvr.FindControl("hfAccesoriesCost");
            HiddenField hfOverhead = (HiddenField)gvr.FindControl("hfOverhead");
            HiddenField hfOthersCost = (HiddenField)gvr.FindControl("hfOthersCost");
            HiddenField hfUtilizationIDs = (HiddenField)gvr.FindControl("hfUtilizationIDs");
            HiddenField hfProductCode = (HiddenField)gvr.FindControl("hfProductCode");
            HiddenField hfDesignCode = (HiddenField)gvr.FindControl("hfDesignCode");
            HiddenField hfSizeCode = (HiddenField)gvr.FindControl("hfSizeCode");

            DropDownList ddlColor_Config = (DropDownList)gvr.FindControl("ddlColor_Config");
            DropDownList ddlPos_FabricsType = (DropDownList)gvr.FindControl("ddlPos_FabricsType");

            TextBox txtSalesPrice_Config = (TextBox)gvr.FindControl("txtSalesPrice_Config");
            TextBox txtCurrentProcessing = (TextBox)gvr.FindControl("txtCurrentProcessing");
            TextBox lblAgvCosting = (TextBox)gvr.FindControl("lblAgvCosting");

            TextBox lblBarCode = (TextBox)gvr.FindControl("lblBarCode");

            pos_Product.Inv_UtilizationDetailsIDs = hfUtilizationIDs.Value;
            pos_Product.Pos_SizeID = Int32.Parse(hfPos_SizeID.Value);

            pos_Product.FabricsCost = Decimal.Parse(hfFabricsCost.Value);
            pos_Product.AccesoriesCost = Decimal.Parse(hfAccesoriesCost.Value);
            pos_Product.Overhead = Decimal.Parse(hfOverhead.Value);
            pos_Product.OthersCost = Decimal.Parse(hfOthersCost.Value);
            pos_Product.PurchasePrice = (Request.QueryString["IsPurchased"] == null?0:Decimal.Parse(lblAgvCosting.Text));
            pos_Product.SalePrice = Decimal.Parse(txtSalesPrice_Config.Text);
            pos_Product.OldSalePrice =0;

            pos_Product.DiscountAmount =((pos_Product.SalePrice * Decimal.Parse(txtDiscountPercentage.Text))/100);
        
            pos_Product.Pos_ColorID = Int32.Parse(ddlColor_Config.SelectedValue);
            pos_Product.Pos_FabricsTypeID = Int32.Parse(ddlPos_FabricsType.SelectedValue);
            pos_Product.ExtraField1 = txtCurrentProcessing.Text;
            pos_Product.DesignCode = hfDesignCode.Value;
            pos_Product.BarCode = lblBarCode.Text;

            pos_Product.StyleCode = txtStyle.Text;//getStyleCode(pos_Product);
            int productID = Pos_ProductManager.InsertPos_Product(pos_Product);

            ids += (ids != "" ? "," : "") + productID.ToString();
        }

        SaveForRemoteDB(ids);

        processUtilizationDetails(TransactionMasterID);

        hlnkProductionPrint.Visible = true;
        hlnkProductionPrint.NavigateUrl = "TransactionPrint.aspx?Pos_TransactionMasterID="+TransactionMasterID.ToString();
        btnClear_Click(this, new EventArgs());
    }

    private bool barCodeChecking()
    {
        bool isFound = false;
        string barcode = "";
        foreach (GridViewRow gvr in gvProductConfiguration.Rows)
        {
            TextBox lblBarCode = (TextBox)gvr.FindControl("lblBarCode");
            barcode += (barcode == "" ? "" : ",") + "'"+lblBarCode.Text.Trim()+"'";

        }

        string sql = "select Distinct BarCode from Pos_Product where BarCode in ("+barcode+")";

        DataSet ds = CommonManager.SQLExec(sql);
        barcode = "";
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            barcode += (barcode == "" ? "" : ",") + dr["BarCode"].ToString();
            isFound = true;
        }
        if (isFound)
        {
            //showAlartMessage(barcode + " already exists in Database");
        }
        return false;// isFound;
    }

    private void SaveForRemoteDB(string ids)
    {
        string sql = @"select * from Pos_Product where Pos_ProductID in ("+ids+ @");
        Select * from ACC_ChartOfAccountLabel4 where ChartOfAccountLabel4Text like 'Show Room%' and ACC_HeadTypeID=1;
        ";

        DataSet ds = CommonManager.SQLExec(sql);
        sql = @"SET IDENTITY_INSERT [Pos_Product] ON";
        
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            sql += @"
            INSERT INTO [Pos_Product]
                       ([Pos_ProductID]
                       ,[ProductID]
                       ,[ReferenceID]
                       ,[Pos_ProductTypeID]
                       ,[Inv_UtilizationDetailsIDs]
                       ,[ProductStatusID]
                       ,[ProductName]
                       ,[DesignCode]
                       ,[Pos_SizeID]
                       ,[Pos_BrandID]
                       ,[Inv_QuantityUnitID]
                       ,[FabricsCost]
                       ,[AccesoriesCost]
                       ,[Overhead]
                       ,[OthersCost]
                       ,[PurchasePrice]
                       ,[SalePrice]
                       ,[OldSalePrice]
                       ,[Note]
                       ,[BarCode]
                       ,[Pos_ColorID]
                       ,[Pos_FabricsTypeID]
                       ,[StyleCode]
                       ,[Pic1]
                       ,[Pic2]
                       ,[Pic3]
                       ,[VatPercentage]
                       ,[IsVatExclusive]
                       ,[DiscountPercentage]
                       ,[DiscountAmount]
                       ,[FabricsNo]
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
                       ,[AddedBy]
                       ,[AddedDate]
                       ,[UpdatedBy]
                       ,[UpdatedDate]
                       ,[RowStatusID])
                 VALUES
                       ("+dr["Pos_ProductID"].ToString()+@"
                       ,"+dr["ProductID"].ToString()+@"
                       ,"+dr["ReferenceID"].ToString()+@"
                       ,"+dr["Pos_ProductTypeID"].ToString()+@"
                       ,'"+dr["Inv_UtilizationDetailsIDs"].ToString()+@"'
                       ,"+dr["ProductStatusID"].ToString()+@"
                       ,'"+dr["ProductName"].ToString()+@"'
                       ,'"+dr["DesignCode"].ToString()+@"'
                       ,"+dr["Pos_SizeID"].ToString()+@"
                       ,"+dr["Pos_BrandID"].ToString()+@"
                       ,"+dr["Inv_QuantityUnitID"].ToString()+@"
                       ,"+dr["FabricsCost"].ToString()+@"
                       ,"+dr["AccesoriesCost"].ToString()+@"
                       ,"+dr["Overhead"].ToString()+@"
                       ,"+dr["OthersCost"].ToString()+@"
                       ,"+dr["PurchasePrice"].ToString()+@"
                       ,"+dr["SalePrice"].ToString()+@"
                       ,"+dr["OldSalePrice"].ToString()+@"
                       ,'"+dr["Note"].ToString()+@"'
                       ,'"+dr["BarCode"].ToString()+@"'
                       ,"+dr["Pos_ColorID"].ToString()+@"
                       ,"+dr["Pos_FabricsTypeID"].ToString()+@"
                       ,'"+dr["StyleCode"].ToString()+@"'
                       ,'"+dr["Pic1"].ToString()+@"'
                       ,'"+dr["Pic2"].ToString()+@"'
                       ,'"+dr["Pic3"].ToString()+@"'
                       ,"+dr["VatPercentage"].ToString()+@"
                       ,"+ (bool.Parse(dr["IsVatExclusive"].ToString())?"1":"0")+@"
                       ,"+dr["DiscountPercentage"].ToString()+@"
                       ,"+dr["DiscountAmount"].ToString()+@"
                       ,'"+dr["FabricsNo"].ToString()+@"'
                       ,'"+dr["ExtraField1"].ToString()+@"'
                       ,'"+dr["ExtraField2"].ToString()+@"'
                       ,'"+dr["ExtraField3"].ToString()+@"'
                       ,'"+dr["ExtraField4"].ToString()+@"'
                       ,'"+dr["ExtraField5"].ToString()+@"'
                       ,'"+dr["ExtraField6"].ToString()+@"'
                       ,'"+dr["ExtraField7"].ToString()+@"'
                       ,'"+dr["ExtraField8"].ToString()+@"'
                       ,'"+dr["ExtraField9"].ToString()+@"'
                       ,'"+dr["ExtraField10"].ToString()+@"'
                       ,"+dr["AddedBy"].ToString()+@"
                       ,'"+dr["AddedDate"].ToString()+@"'
                       ,"+dr["UpdatedBy"].ToString()+@"
                       ,'"+dr["UpdatedDate"].ToString()+@"'
                       ,"+dr["RowStatusID"].ToString()+@");";

        }

        sql+=@"SET IDENTITY_INSERT [Pos_Product] OFF";

        string sqlFinal = "";
        foreach (DataRow dr in ds.Tables[1].Rows)
        {
            sqlFinal +=Auto_SQL.add(
                sql //Sql
               ,"1" //Status
               , ConfigurationManager.AppSettings["WorkStationID"] //For WorkStation
               ,dr["ACC_ChartOfAccountLabel4ID"].ToString() // to workstation
               ,"0" //FromID
               , DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") //From Time
               ,""//<UploadTime, nvarchar(256),>
               ,""//<ExecuteTime, nvarchar(256),>
               ,""//<ExtraField1, nvarchar(256),>
               ,""//<ExtraField2, nvarchar(256),>
               ,""//<ExtraField3, nvarchar(256),>
               ,""//<ExtraField4, nvarchar(256),>
               ,""//<ExtraField5, nvarchar(256),>
            );
            
        }
        
        CommonManager.SQLExec(sqlFinal);        
    }

    private void processUtilizationDetails(int TransactionMasterID)
    {
        string sql = "";
        foreach (GridViewRow gvr in gvInv_UtilizationDetails.Rows)
        {
            TextBox txtCurrentProcess = (TextBox)gvr.FindControl("txtCurrentProcess");

            CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");
            if (txtCurrentProcess.Text == "")
            {
                continue;
            }

            if (chkSelect.Checked && decimal.Parse(txtCurrentProcess.Text) > 0)
            {
                HiddenField hfColorID = (HiddenField)gvr.FindControl("hfColorID");
                HiddenField hfPos_SizeID = (HiddenField)gvr.FindControl("hfPos_SizeID");
                HiddenField hfRemainingQty = (HiddenField)gvr.FindControl("hfRemainingQty");
                Label lblSizeName = (Label)gvr.FindControl("lblSizeName");
                Label lblFabricsCost = (Label)gvr.FindControl("lblFabricsCost");
                Label lblAccesoriesCost = (Label)gvr.FindControl("lblAccesoriesCost");
                Label lblOverhead = (Label)gvr.FindControl("lblOverhead");
                Label lblOthersCost = (Label)gvr.FindControl("lblOthersCost");

                if (decimal.Parse(hfRemainingQty.Value) < decimal.Parse(txtCurrentProcess.Text))
                {
                    showAlartMessage("Current processing qty can not be more than available qty");
                    return;
                }

                sql+=@"INSERT INTO [Inv_UtilizationDetailsTransaction]
                           ([UtilizationDetailsID]
                           ,[Qty]
                           ,[Pos_TransactionMasterID])
                     VALUES
                           ("+chkSelect.ToolTip+@"
                           ,"+txtCurrentProcess.Text+@"
                           ," + TransactionMasterID + ");";
                sql += @"Update Inv_UtilizationDetails set ProcessedQuantity+=" + txtCurrentProcess.Text + " where Inv_UtilizationDetailsID="+chkSelect.ToolTip+";";
            }
        }
        sql += "Update  ACC_ChartOfAccountLabel4 set ExtraField2='"+(decimal.Parse(txtDesignCode.Text)+1).ToString("0")+"' where ACC_ChartOfAccountLabel4ID=" + ddlProduct.SelectedValue + ";";
        //sql += "Select TransactionID from Pos_TransactionMaster where Pos_TransactionMasterID=" + TransactionMasterID+";";
        
        CommonManager.SQLExec(sql);
    
    }

    private string getStyleCode(Pos_Product prod)
    {
        string StyleCode = "";

        foreach (string  item in prod.ProductName.Split(' '))
        {
            StyleCode += item.Substring(0,1).ToUpper();
        }

        return StyleCode +"-"+prod.DesignCode;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //Pos_Product pos_Product = new Pos_Product();
        //pos_Product = Pos_ProductManager.GetPos_ProductByID(Int32.Parse(Request.QueryString["pos_ProductID"]));
        //Pos_Product tempPos_Product = new Pos_Product();
        //tempPos_Product.Pos_ProductID = pos_Product.Pos_ProductID;

        //tempPos_Product.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        //tempPos_Product.ReferenceID = Int32.Parse(ddlReference.SelectedValue);
        //tempPos_Product.Pos_ProductTypeID = Int32.Parse(ddlPos_ProductType.SelectedValue);
        //tempPos_Product.Inv_UtilizationDetailsIDs = "";
        //tempPos_Product.ProductStatusID = Int32.Parse(ddlProductStatus.SelectedValue);
        //tempPos_Product.ProductName = txtProductName.Text;
        //tempPos_Product.DesignCode = txtDesignCode.Text;
        //tempPos_Product.Pos_SizeID = Int32.Parse(ddlPos_Size.SelectedValue);
        //tempPos_Product.Pos_BrandID = Int32.Parse(ddlPos_Brand.SelectedValue);
        //tempPos_Product.Inv_QuantityUnitID = Int32.Parse(ddlInv_QuantityUnit.SelectedValue);
        //tempPos_Product.FabricsCost = Decimal.Parse(txtFabricsCost.Text);
        //tempPos_Product.AccesoriesCost = Decimal.Parse(txtAccesoriesCost.Text);
        //tempPos_Product.Overhead = Decimal.Parse(txtOverhead.Text);
        //tempPos_Product.OthersCost = Decimal.Parse(txtOthersCost.Text);
        //tempPos_Product.PurchasePrice = Decimal.Parse(txtPurchasePrice.Text);
        //tempPos_Product.SalePrice = Decimal.Parse(txtSalePrice.Text);
        //tempPos_Product.OldSalePrice = Decimal.Parse(txtOldSalePrice.Text);
        //tempPos_Product.Note = txtNote.Text;
        //tempPos_Product.BarCode = txtBarCode.Text;
        //tempPos_Product.Pos_ColorID = Int32.Parse(ddlPos_Color.SelectedValue);
        //tempPos_Product.Pos_FabricsTypeID = Int32.Parse(ddlPos_FabricsType.SelectedValue);
        //tempPos_Product.StyleCode = txtStyleCode.Text;
        //tempPos_Product.Pic1 = txtPic1.Text;
        //tempPos_Product.Pic2 = txtPic2.Text;
        //tempPos_Product.Pic3 = txtPic3.Text;
        //tempPos_Product.VatPercentage = Decimal.Parse(txtVatPercentage.Text);
        //tempPos_Product.IsVatExclusive = cbIsVatExclusive.Checked;
        //tempPos_Product.DiscountPercentage = Decimal.Parse(txtDiscountPercentage.Text);
        //tempPos_Product.DiscountAmount = Decimal.Parse(txtDiscountAmount.Text);
        //tempPos_Product.FabricsNo = txtFabricsNo.Text;
        //tempPos_Product.ExtraField1 = txtExtraField1.Text;
        //tempPos_Product.ExtraField2 = txtExtraField2.Text;
        //tempPos_Product.ExtraField3 = txtExtraField3.Text;
        //tempPos_Product.ExtraField4 = txtExtraField4.Text;
        //tempPos_Product.ExtraField5 = txtExtraField5.Text;
        //tempPos_Product.ExtraField6 = txtExtraField6.Text;
        //tempPos_Product.ExtraField7 = txtExtraField7.Text;
        //tempPos_Product.ExtraField8 = txtExtraField8.Text;
        //tempPos_Product.ExtraField9 = txtExtraField9.Text;
        //tempPos_Product.ExtraField10 = txtExtraField10.Text;
        //tempPos_Product.AddedBy = Int32.Parse(txtAddedBy.Text);
        //tempPos_Product.AddedDate = DateTime.Now;
        //tempPos_Product.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        //tempPos_Product.UpdatedDate = DateTime.Now;
        //tempPos_Product.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        //bool result = Pos_ProductManager.UpdatePos_Product(tempPos_Product);
        //Response.Redirect("AdminPos_ProductDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //ddlProduct.SelectedIndex = 0;
        //ddlProductStatus.SelectedIndex = 0;
        txtDesignCode.Text = "";
        txtPicture.Value = "";
        ddlColorSearch.SelectedValue = "0";
        ddlPos_Brand.SelectedIndex = 0;
        //ddlInv_QuantityUnit.SelectedIndex = 0;
        
        txtNote.Text = "";
        
        txtVatPercentage.Text = "0";
        cbIsVatExclusive.Checked = false;
        txtDiscountPercentage.Text = "0";
        
        List<Inv_UtilizationDetails> nullUtilization = new List<Inv_UtilizationDetails>();
        gvInv_UtilizationDetails.DataSource = nullUtilization;
        gvInv_UtilizationDetails.DataBind();

        List<Pos_Product> nullProduction = new List<Pos_Product>();
        gvProductConfiguration.DataSource = nullProduction;
        gvProductConfiguration.DataBind();


    }
    private void showPos_ProductData()
    {
        //Pos_Product pos_Product = new Pos_Product();
        //pos_Product = Pos_ProductManager.GetPos_ProductByID(Int32.Parse(Request.QueryString["pos_ProductID"]));

        //ddlProduct.SelectedValue = pos_Product.ProductID.ToString();
        //ddlReference.SelectedValue = pos_Product.ReferenceID.ToString();
        //ddlPos_ProductType.SelectedValue = pos_Product.Pos_ProductTypeID.ToString();
        //ddlInv_UtilizationDetails.SelectedValue = pos_Product.Inv_UtilizationDetailsIDs.ToString();
        //ddlProductStatus.SelectedValue = pos_Product.ProductStatusID.ToString();
        //txtProductName.Text = pos_Product.ProductName;
        //txtDesignCode.Text = pos_Product.DesignCode;
        //ddlPos_Size.SelectedValue = pos_Product.Pos_SizeID.ToString();
        //ddlPos_Brand.SelectedValue = pos_Product.Pos_BrandID.ToString();
        //ddlInv_QuantityUnit.SelectedValue = pos_Product.Inv_QuantityUnitID.ToString();
        //txtFabricsCost.Text = pos_Product.FabricsCost.ToString();
        //txtAccesoriesCost.Text = pos_Product.AccesoriesCost.ToString();
        //txtOverhead.Text = pos_Product.Overhead.ToString();
        //txtOthersCost.Text = pos_Product.OthersCost.ToString();
        //txtPurchasePrice.Text = pos_Product.PurchasePrice.ToString();
        //txtSalePrice.Text = pos_Product.SalePrice.ToString();
        //txtOldSalePrice.Text = pos_Product.OldSalePrice.ToString();
        //txtNote.Text = pos_Product.Note;
        //txtBarCode.Text = pos_Product.BarCode;
        //ddlPos_Color.SelectedValue = pos_Product.Pos_ColorID.ToString();
        //ddlPos_FabricsType.SelectedValue = pos_Product.Pos_FabricsTypeID.ToString();
        //txtStyleCode.Text = pos_Product.StyleCode;
        //txtPic1.Text = pos_Product.Pic1;
        //txtPic2.Text = pos_Product.Pic2;
        //txtPic3.Text = pos_Product.Pic3;
        //txtVatPercentage.Text = pos_Product.VatPercentage.ToString();
        //cbIsVatExclusive.Checked = pos_Product.IsVatExclusive;
        //txtDiscountPercentage.Text = pos_Product.DiscountPercentage.ToString();
        //txtDiscountAmount.Text = pos_Product.DiscountAmount.ToString();
        //txtFabricsNo.Text = pos_Product.FabricsNo;
        //txtExtraField1.Text = pos_Product.ExtraField1;
        //txtExtraField2.Text = pos_Product.ExtraField2;
        //txtExtraField3.Text = pos_Product.ExtraField3;
        //txtExtraField4.Text = pos_Product.ExtraField4;
        //txtExtraField5.Text = pos_Product.ExtraField5;
        //txtExtraField6.Text = pos_Product.ExtraField6;
        //txtExtraField7.Text = pos_Product.ExtraField7;
        //txtExtraField8.Text = pos_Product.ExtraField8;
        //txtExtraField9.Text = pos_Product.ExtraField9;
        //txtExtraField10.Text = pos_Product.ExtraField10;
        //ddlRowStatus.SelectedValue = pos_Product.RowStatusID.ToString();
    }
    private void loadACC_ChartOfAccountLabel4()
    {
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlProduct.Items.Add(new ListItem("Any Product", "0"));
        ddlSuppier.Items.Add(new ListItem("Select Supplier", "0"));
        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 3)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlProduct.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 6)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlSuppier.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1 & aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString().Contains("Factory"))
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlProductionUnit.Items.Add(item);
            }

        }
    }
    private void loadReference()
    {
        
    }
    private void loadPos_ProductType()
    {
        //ListItem li = new ListItem("Select Pos_ProductType...", "0");
        //ddlPos_ProductType.Items.Add(li);

        //List<Pos_ProductType> pos_ProductTypes = new List<Pos_ProductType>();
        //pos_ProductTypes = Pos_ProductTypeManager.GetAllPos_ProductTypes();
        //foreach (Pos_ProductType pos_ProductType in pos_ProductTypes)
        //{
        //    ListItem item = new ListItem(pos_ProductType.ProductTypeName.ToString(), pos_ProductType.Pos_ProductTypeID.ToString());
        //    ddlPos_ProductType.Items.Add(item);
        //}
    }
    private void loadInv_UtilizationDetails()
    {
        
    }
    private void loadProductStatus()
    {
        ListItem li = new ListItem("Select ProductStatus...", "0");
        //ddlProductStatus.Items.Add(li);

        List<Pos_ProductStatus> productStatuss = new List<Pos_ProductStatus>();
        productStatuss = Pos_ProductStatusManager.GetAllPos_ProductStatuss();
        foreach (Pos_ProductStatus productStatus in productStatuss)
        {
            ListItem item = new ListItem(productStatus.ProductStatusName.ToString(), productStatus.Pos_ProductStatusID.ToString());
            ddlProductStatus.Items.Add(item);
        }
    }
    private void loadPos_Size()
    {
        string sql = @"Select Pos_Size.* from Pos_Size";


        DataSet ds = CommonManager.SQLExec(sql);

        dlSize.DataSource = ds.Tables[0];
        dlSize.DataBind();
    }
    private void loadPos_Brand()
    {
        //ListItem li = new ListItem("Select Pos_Brand...", "0");
        //ddlPos_Brand.Items.Add(li);

        List<Pos_Brand> pos_Brands = new List<Pos_Brand>();
        pos_Brands = Pos_BrandManager.GetAllPos_Brands();
        foreach (Pos_Brand pos_Brand in pos_Brands)
        {
            ListItem item = new ListItem(pos_Brand.BrandName.ToString(), pos_Brand.Pos_BrandID.ToString());
            ddlPos_Brand.Items.Add(item);
        }
    }
    private void loadInv_QuantityUnit()
    {
        ListItem li = new ListItem("Select Inv_QuantityUnit...", "0");
        //ddlInv_QuantityUnit.Items.Add(li);

        List<Inv_QuantityUnit> inv_QuantityUnits = new List<Inv_QuantityUnit>();
        inv_QuantityUnits = Inv_QuantityUnitManager.GetAllInv_QuantityUnits();
        foreach (Inv_QuantityUnit inv_QuantityUnit in inv_QuantityUnits)
        {
            ListItem item = new ListItem(inv_QuantityUnit.QuantityUnitName.ToString(), inv_QuantityUnit.Inv_QuantityUnitID.ToString());
            ddlInv_QuantityUnit.Items.Add(item);
        }

        ddlInv_QuantityUnit.SelectedValue = "3";
    }
    private void loadPos_Color()
    {
        ListItem li = new ListItem("Select Color", "0");
        ddlPos_ColorAll.Items.Add(li);
        ddlColorSearch.Items.Add(new ListItem("Any Color", "0"));

        List<Pos_Color> pos_Colors = new List<Pos_Color>();
        pos_Colors = Pos_ColorManager.GetAllPos_Colors();
        foreach (Pos_Color pos_Color in pos_Colors)
        {
            ListItem item = new ListItem(pos_Color.ColorName.ToString(), pos_Color.Pos_ColorID.ToString());
            ddlPos_ColorAll.Items.Add(item);
            ddlColorSearch.Items.Add(item);
        }

        ddlPos_ColorAll.SelectedValue = "1";
    }
    private void loadPos_FabricsType()
    {
        ListItem li = new ListItem("Select Fabrics Type", "0");
        ddlPos_FabricsTypeAll.Items.Add(li);

        List<Pos_FabricsType> pos_FabricsTypes = new List<Pos_FabricsType>();
        pos_FabricsTypes = Pos_FabricsTypeManager.GetAllPos_FabricsTypes();
        foreach (Pos_FabricsType pos_FabricsType in pos_FabricsTypes)
        {
            ListItem item = new ListItem(pos_FabricsType.FabricsTypeName.ToString(), pos_FabricsType.Pos_FabricsTypeID.ToString());
            ddlPos_FabricsTypeAll.Items.Add(item);
        }
        ddlPos_FabricsTypeAll.SelectedValue = "4";
    }
    private void loadRowStatus()
    {
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = @"SELECT    Inv_UtilizationDetails.ProductID,
                        Pos_Color.ColorName, 
                        Pos_Size.SizeName, 
                        ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,
                        Inv_Item.ItemName, 
                        Inv_Item.ItemCode, 
                        Inv_UtilizationDetails.Inv_ItemID, 
                        Inv_UtilizationDetails.FabricsCost, 
                        Inv_UtilizationDetails.AccesoriesCost, 
                        Inv_UtilizationDetails.Overhead, 
                        Inv_UtilizationDetails.OthersCost, 
                        Inv_UtilizationDetails.ProductionQuantity,
                        Inv_UtilizationDetails.ProcessedQuantity, 
                        (Inv_UtilizationDetails.ProductionQuantity -
                        Inv_UtilizationDetails.ProcessedQuantity ) as Remaining,
                        (case Inv_UtilizationDetails.IsReject when 0 then 'Fresh' else 'Reject' end) as IsReject, 
                        Inv_UtilizationDetails.AddedDate, 
                        Inv_UtilizationDetails.Inv_UtilizationDetailsID,
                        Inv_UtilizationDetails.ProductID,
                        Inv_UtilizationDetails.Pos_SizeID, 
                        Inv_UtilizationDetails.ExtraField1 as ColorID
                        
                    FROM  Inv_UtilizationDetails 
	                    INNER JOIN ACC_ChartOfAccountLabel4 ON Inv_UtilizationDetails.ProductID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID 
	                    INNER JOIN Pos_Size ON Inv_UtilizationDetails.Pos_SizeID = Pos_Size.Pos_SizeID 
	                    left JOIN Pos_Color ON Inv_UtilizationDetails.ExtraField1 = Pos_Color.Pos_ColorID
	                    INNER JOIN Inv_Item ON Inv_UtilizationDetails.Inv_ItemID = Inv_Item.Inv_ItemID
	                    INNER JOIN Inv_Utilization ON Inv_UtilizationDetails.Inv_UtilizationID = Inv_Utilization.Inv_UtilizationID
                    where Inv_UtilizationDetails.RowStatusID=1 and  Inv_Utilization.WorkSatationID=" + ddlProductionUnit.SelectedValue + @" and
                        (Inv_UtilizationDetails.ProductionQuantity -
                        Inv_UtilizationDetails.ProcessedQuantity )>0";

        if (ddlProduct.SelectedValue != "0")
        {
            sql += " and Inv_UtilizationDetails.ProductID="+ddlProduct.SelectedValue; 
        }

        if (ddlColorSearch.SelectedValue != "0")
        {
            sql += " and Inv_UtilizationDetails.ExtraField1='" + ddlColorSearch.SelectedValue +"'";
        }

        if (rbtnProductType.SelectedValue != "All")
        {
            sql += " and Inv_UtilizationDetails.IsReject=" + rbtnProductType.SelectedValue ;
        }
        sql += @" order by Inv_Item.ItemCode";

        DataSet ds = CommonManager.SQLExec(sql);

        gvInv_UtilizationDetails.DataSource = ds.Tables[0];//Inv_UtilizationDetailsManager.GetAllInv_UtilizationDetailss();
        gvInv_UtilizationDetails.DataBind();
    }
    protected void btnProcessProduction_Click(object sender, EventArgs e)
    {
        if (ddlProduct.SelectedValue != "0" && rbtnProductType.SelectedValue != "All")
        {
            
            List<Pos_Product> configuringProdut = new List<Pos_Product>();
            
            foreach (GridViewRow gvr in gvInv_UtilizationDetails.Rows)
            {
                TextBox txtCurrentProcess = (TextBox)gvr.FindControl("txtCurrentProcess");

                CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");
                if(txtCurrentProcess.Text=="")
                {
                    continue;                    
                }

                if (chkSelect.Checked && decimal.Parse(txtCurrentProcess.Text)>0)
                {
                    HiddenField hfColorID = (HiddenField)gvr.FindControl("hfColorID");
                    HiddenField hfPos_SizeID = (HiddenField)gvr.FindControl("hfPos_SizeID");
                    HiddenField hfRemainingQty = (HiddenField)gvr.FindControl("hfRemainingQty");
                    Label lblSizeName=(Label)gvr.FindControl("lblSizeName");
                    Label lblFabricsCost = (Label)gvr.FindControl("lblFabricsCost");
                    Label lblAccesoriesCost = (Label)gvr.FindControl("lblAccesoriesCost");
                    Label lblOverhead = (Label)gvr.FindControl("lblOverhead");
                    Label lblOthersCost = (Label)gvr.FindControl("lblOthersCost");
                    
                    if (decimal.Parse(hfRemainingQty.Value) < decimal.Parse(txtCurrentProcess.Text))
                    {
                        showAlartMessage("Current processing qty can not be more than available qty");
                        return;
                    }

                    bool isFound = false;
                    foreach (Pos_Product prod in configuringProdut)
                    {
                        if (prod.Pos_SizeID.ToString() == hfPos_SizeID.Value)
                        {
                            isFound = true;
                            prod.Inv_UtilizationDetailsIDs = "," + chkSelect.ToolTip;
                            prod.ExtraField1 = (decimal.Parse(prod.ExtraField1)+decimal.Parse(txtCurrentProcess.Text)).ToString("0.00");
                            prod.FabricsCost += decimal.Parse(lblFabricsCost.Text) * decimal.Parse(txtCurrentProcess.Text);
                            prod.AccesoriesCost += decimal.Parse(lblAccesoriesCost.Text) * decimal.Parse(txtCurrentProcess.Text);
                            prod.Overhead += decimal.Parse(lblOverhead.Text) * decimal.Parse(txtCurrentProcess.Text);
                            prod.OthersCost += decimal.Parse(lblOthersCost.Text) * decimal.Parse(txtCurrentProcess.Text);
                            prod.Pos_ColorID = int.Parse(hfColorID.Value);
                            break;
                        }
                    }

                    if (!isFound)
                    {
                        Pos_Product prod = new Pos_Product();
                        prod.Inv_UtilizationDetailsIDs = chkSelect.ToolTip;
                        prod.Pos_SizeID = int.Parse(hfPos_SizeID.Value);
                        prod.ExtraField2 = lblSizeName.Text;
                        prod.ExtraField1 = txtCurrentProcess.Text;
                        prod.FabricsCost = decimal.Parse(lblFabricsCost.Text) * decimal.Parse(txtCurrentProcess.Text);
                        prod.AccesoriesCost = decimal.Parse(lblAccesoriesCost.Text) * decimal.Parse(txtCurrentProcess.Text);
                        prod.Overhead = decimal.Parse(lblOverhead.Text) * decimal.Parse(txtCurrentProcess.Text);
                        prod.OthersCost = decimal.Parse(lblOthersCost.Text) * decimal.Parse(txtCurrentProcess.Text);
                        prod.Pos_ColorID = int.Parse(hfColorID.Value);

                        configuringProdut.Add(prod);
                    }
                }
            }
            string sizeIDs = "";
            foreach (Pos_Product prod in configuringProdut)
            {
                prod.ExtraField3 = ((prod.FabricsCost + prod.AccesoriesCost + prod.Overhead + prod.OthersCost) / (decimal.Parse(prod.ExtraField1))).ToString("0.00");
                prod.FabricsCost = prod.FabricsCost / decimal.Parse(prod.ExtraField1);
                prod.AccesoriesCost = prod.AccesoriesCost / decimal.Parse(prod.ExtraField1);
                prod.Overhead = prod.Overhead / decimal.Parse(prod.ExtraField1);
                prod.OthersCost = prod.OthersCost / decimal.Parse(prod.ExtraField1);

                if (sizeIDs != "") sizeIDs += ",";
                sizeIDs += prod.Pos_SizeID.ToString();
            }

            string sql = "Select ExtraField1,ExtraField2 from ACC_ChartOfAccountLabel4 where ACC_ChartOfAccountLabel4ID=" + ddlProduct.SelectedValue + ";";
            sql += @"SELECT [Pos_SizeID]
                          ,[SizeName]
                          ,[Code]
                      FROM [Pos_Size]
                      where Pos_SizeID in ("+sizeIDs+");";

            DataSet ds = CommonManager.SQLExec(sql);

            foreach (Pos_Product prod in configuringProdut)
            {
                prod.DesignCode = decimal.Parse((ds.Tables[0].Rows[0]["ExtraField2"].ToString() ==""?"1":ds.Tables[0].Rows[0]["ExtraField2"].ToString())).ToString("00000");
                txtDesignCode.Text = prod.DesignCode;
                prod.ExtraField4 = ds.Tables[0].Rows[0]["ExtraField1"].ToString();
                foreach (DataRow dr in ds.Tables[1].Rows)
	            {
                    if (prod.Pos_SizeID.ToString() == dr["Pos_SizeID"].ToString())
                    {
                        prod.ExtraField5 = dr["Code"].ToString();
                    }
	            }
                prod.BarCode = prod.ExtraField4 + prod.DesignCode + prod.ExtraField5;
            }

            gvProductConfiguration.DataSource=configuringProdut;
            gvProductConfiguration.DataBind();

            foreach (GridViewRow gvr in gvProductConfiguration.Rows)
            {
                DropDownList ddlColor_Config = (DropDownList)gvr.FindControl("ddlColor_Config");
                DropDownList ddlPos_FabricsType = (DropDownList)gvr.FindControl("ddlPos_FabricsType");
                HiddenField hfColorID = (HiddenField)gvr.FindControl("hfColorID");
                
                TextBox lblAgvCosting = (TextBox)gvr.FindControl("lblAgvCosting");
                TextBox txtCurrentProcessing = (TextBox)gvr.FindControl("txtCurrentProcessing");
                lblAgvCosting.Enabled = false;
                txtCurrentProcessing.Enabled = false;
                
                foreach (ListItem item in ddlPos_FabricsTypeAll.Items)
                {
                    ddlPos_FabricsType.Items.Add(item);
                }
                ddlPos_FabricsType.SelectedValue = "4";
                foreach (ListItem item in ddlPos_ColorAll.Items)
                {
                    ddlColor_Config.Items.Add(item);
                }
                if (hfColorID.Value != "")
                {
                    ddlColor_Config.SelectedValue = hfColorID.Value;
                }
                else
                {
                    ddlColor_Config.SelectedValue = "1";
                
                }
            }

            
        }
        else
        {
            showAlartMessage("Please select the Product and/or Product type");
        }
    }

    private void showAlartMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "err_msg",
             "alert('" + message + "');",
             true);
    }
    protected void btnPurchaseProcess_Click(object sender, EventArgs e)
    {
        if (ddlSuppier.SelectedValue == "0" && Request.QueryString["IsReGenerateBarcode"]==null)
        {
            showAlartMessage("Select the supplier");
            return;
        }


        if (ddlProduct.SelectedValue != "0")
        {

            List<Pos_Product> configuringProdut = new List<Pos_Product>();
            bool anySizeSelected = false;
            foreach (DataListItem gvr in dlSize.Items)
            {
                CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");
                
                if (chkSelect.Checked)
                {
                    anySizeSelected = true;
                    Pos_Product prod = new Pos_Product();
                    prod.Pos_SizeID = int.Parse(chkSelect.ToolTip);
                    prod.ExtraField2 = chkSelect.Text;
                    prod.Inv_UtilizationDetailsIDs = "";
                    prod.ExtraField3 = "0";
                    prod.FabricsCost = 0;
                    prod.AccesoriesCost = 0;
                    prod.Overhead = 0;
                    prod.OthersCost = 0;
                    configuringProdut.Add(prod);
                }
            }

            if (!anySizeSelected)
            {
                showAlartMessage("Please select the size");
                return;
            }

            string sizeIDs = "";
            foreach (Pos_Product prod in configuringProdut)
            {
                if (sizeIDs != "") sizeIDs += ",";
                sizeIDs += prod.Pos_SizeID.ToString();
            }

            string sql = "Select ExtraField1,ExtraField2 from ACC_ChartOfAccountLabel4 where ACC_ChartOfAccountLabel4ID=" + ddlProduct.SelectedValue + ";";
            sql += @"SELECT [Pos_SizeID]
                          ,[SizeName]
                          ,[Code]
                      FROM [Pos_Size]
                      where Pos_SizeID in (" + sizeIDs + ");";

            DataSet ds = CommonManager.SQLExec(sql);

            foreach (Pos_Product prod in configuringProdut)
            {
                prod.DesignCode = decimal.Parse((ds.Tables[0].Rows[0]["ExtraField2"].ToString() == "" ? "1" : ds.Tables[0].Rows[0]["ExtraField2"].ToString())).ToString("00000");
                txtDesignCode.Text = prod.DesignCode;
                prod.ExtraField4 = ds.Tables[0].Rows[0]["ExtraField1"].ToString();
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    if (prod.Pos_SizeID.ToString() == dr["Pos_SizeID"].ToString())
                    {
                        prod.ExtraField5 = dr["Code"].ToString();
                    }
                }
                prod.BarCode = prod.ExtraField4 + prod.DesignCode + prod.ExtraField5;
            }

            gvProductConfiguration.DataSource = configuringProdut;
            gvProductConfiguration.DataBind();

            foreach (GridViewRow gvr in gvProductConfiguration.Rows)
            {
                DropDownList ddlColor_Config = (DropDownList)gvr.FindControl("ddlColor_Config");
                DropDownList ddlPos_FabricsType = (DropDownList)gvr.FindControl("ddlPos_FabricsType");
                HiddenField hfColorID = (HiddenField)gvr.FindControl("hfColorID");

                foreach (ListItem item in ddlPos_FabricsTypeAll.Items)
                {
                    ddlPos_FabricsType.Items.Add(item);
                }
                ddlPos_FabricsType.SelectedValue = "4";
                foreach (ListItem item in ddlPos_ColorAll.Items)
                {
                    ddlColor_Config.Items.Add(item);
                }
                ddlColor_Config.SelectedValue = "1";

            }
        }
        else
        {
            showAlartMessage("Please select the Product ");
        }
    }
    
}
