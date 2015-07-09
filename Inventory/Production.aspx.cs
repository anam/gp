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

public partial class AdminInv_ProductInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadLoginInHiddenField();
            loadACC_ChartOfAccountLabel4();
            if (Request.QueryString["inv_ProductID"] != null)
            {
                int inv_ProductID = Int32.Parse(Request.QueryString["inv_ProductID"]);
                if (inv_ProductID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_ProductData();
                }
            }
            loadSizeGrid();
            initialLoad();
            loadPos_Color();
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

    private void initialLoad()
    {
        ddlEmployee.SelectedValue = "622";
        ddlWorkSatation.SelectedValue = "2";
        txtDate.Text = DateTime.Today.ToString("dd MMM yyyy");
    }

    private void loadPos_Color()
    {
        ListItem li = new ListItem("Select Pos_Color...", "0");
        ddlPos_Color.Items.Add(li);

        List<Pos_Color> pos_Colors = new List<Pos_Color>();
        pos_Colors = Pos_ColorManager.GetAllPos_Colors();
        foreach (Pos_Color pos_Color in pos_Colors)
        {
            ListItem item = new ListItem(pos_Color.ColorName.ToString(), pos_Color.Pos_ColorID.ToString());
            ddlPos_Color.Items.Add(item);
        }

        ddlPos_Color.SelectedValue = "1";
    }

    private void loadACC_ChartOfAccountLabel4()
    {

        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlEmployee.Items.Add(new ListItem("Select Employee", "0"));
        ddlWorkSatation.Items.Add(new ListItem("Select WorkStation", "0"));
        ddlProduct.Items.Add(new ListItem("-- any ---", "-2"));
        ddlProduct.Items.Add(new ListItem("Non-Productive", "-1"));
        ddlProduct.Items.Add(new ListItem("Any Product", "0"));

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 4)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlEmployee.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlWorkSatation.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 3)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlProduct.Items.Add(item);
                if (aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID != 914)
                {
                    ddlFinalProduct.Items.Add(item);
                }
            }

        }


    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        showInv_IssueDetailGrid();
    }



    protected void btnCostCalculation_Click(object sender, EventArgs e)
    {
        //calculate fabrics cost
        decimal totalFabricsCost = 0;
        decimal totalAccesoriesCost = 0;
        decimal totalQty = 0;
        decimal totalQtyGood = 0;
        decimal totalQtyReject = 0;
        foreach (DataListItem item in dlSize.Items)
        {
            TextBox txtQty = (TextBox)item.FindControl("txtQty");
            if (txtQty.Text != "")
            {
                totalQtyGood += decimal.Parse(txtQty.Text);
            }
        }

        foreach (DataListItem item in dlSizeRejected.Items)
        {
            TextBox txtQty = (TextBox)item.FindControl("txtQty");
            if (txtQty.Text != "")
            {
                totalQtyReject += decimal.Parse(txtQty.Text);
            }
        }

        totalQty = totalQtyGood + totalQtyReject;

        //Process accessories list
        List<Inv_ProductionConfiguration> inv_ProductionConfiguration= Inv_ProductionConfigurationManager.GetAllInv_ProductionConfigurationsByProductIDnRawmaterialID(int.Parse(ddlFinalProduct.SelectedValue), 0);
        //for all product need to multiply the single product required qty
        foreach (Inv_ProductionConfiguration item in inv_ProductionConfiguration)
        {
            item.QuantityValue *= totalQty;
        }

        foreach (GridViewRow gvr in gvInv_IssueDetail.Rows)
        {
            TextBox txtUtilization = (TextBox)gvr.FindControl("txtUtilization");
            TextBox txtWasted = (TextBox)gvr.FindControl("txtWasted");
            TextBox txtProductionQuantity = (TextBox)gvr.FindControl("txtProductionQuantity");
            Label lblExtraField1 = (Label)gvr.FindControl("lblExtraField1");//stock
            Label lblItemCode = (Label)gvr.FindControl("lblItemCode");
            Label lblPricePerUnit = (Label)gvr.FindControl("lblPricePerUnit");
            Label lblProductName = (Label)gvr.FindControl("lblProductName");
            CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");
            CheckBox chkMain = (CheckBox)gvr.FindControl("chkMain");

            HiddenField hfItemID = (HiddenField)gvr.FindControl("hfItemID");
            HiddenField hfRawMaterialID = (HiddenField)gvr.FindControl("hfRawMaterialID");

            if (chkSelect.Checked)
            {
                if (decimal.Parse(lblExtraField1.Text) < (decimal.Parse(txtUtilization.Text) + decimal.Parse(txtWasted.Text)))
                {
                    showAlartMessage("Stock can not be less than utilization and wastage for " + lblItemCode.Text);
                    return;
                }
                
                //if (lblProductName.Text != "N/A")
                //{
                if (chkSelect.Enabled)
                    totalFabricsCost += ((decimal.Parse(txtUtilization.Text) + decimal.Parse(txtWasted.Text)) * decimal.Parse(lblPricePerUnit.Text));
                //}

                if (chkMain.Checked)
                {
                    txtProductionQuantity.Text = totalQty.ToString("0");
                }
                else
                { 
                txtProductionQuantity.Text="0";
                }
            }

            if (!chkSelect.Enabled)
            { 
                foreach (Inv_ProductionConfiguration item in inv_ProductionConfiguration)
                {
                    if (item.RawMaterialID.ToString() == hfRawMaterialID.Value && item.QuantityValue >0)
                    {
                        if (decimal.Parse(lblExtraField1.Text) >= item.QuantityValue)
                        {
                            txtUtilization.Text = item.QuantityValue.ToString("0.00");
                            item.QuantityValue = 0;

                            totalAccesoriesCost += ((decimal.Parse(txtUtilization.Text) + decimal.Parse(txtWasted.Text)) * decimal.Parse(lblPricePerUnit.Text));

                        }
                        else if (decimal.Parse(lblExtraField1.Text) < item.QuantityValue)
                        {
                            txtUtilization.Text = lblExtraField1.Text;
                            item.QuantityValue -= decimal.Parse(lblExtraField1.Text);
                        }
                        break;
                    }
                }

                
            }

        }

        String sql = @"select Pos_ProductCost.Amount,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
                        ,Pos_CostType.Pos_CostTypeID
                         from Pos_ProductCost 
                        inner join Pos_CostType on Pos_CostType.Pos_CostTypeID =Pos_ProductCost.Pos_CostTypeID
                        inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID=Pos_ProductCost.ProductID
                        Where ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID=" + ddlFinalProduct.SelectedValue;

        DataSet ds = CommonManager.SQLExec(sql);
        decimal overheadcost = 0;
        decimal labourecost = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["Pos_CostTypeID"].ToString() == "1") labourecost = decimal.Parse(dr["Amount"].ToString());
            else
                if (dr["Pos_CostTypeID"].ToString() != "1") overheadcost += decimal.Parse(dr["Amount"].ToString());
        }

        txtFabricsCost.Text = (totalFabricsCost / totalQty).ToString("0.00");
        txtAccessoriesCost.Text = (totalAccesoriesCost / totalQty).ToString("0.00");
        //txtAccessoriesCost.Text = "0";
        txtLabourCost.Text = labourecost.ToString("0.00") ;
        txtOverheadCost.Text = overheadcost.ToString("0.00");
        lblTotal.Text = "Total Good Qty = " + totalQtyGood.ToString("0") + "<br/>" + "Total Rejected Qty = " + totalQtyReject.ToString("0") + "<br/>" + "Total Cost/unit = " + (decimal.Parse(txtFabricsCost.Text) + decimal.Parse(txtAccessoriesCost.Text) + decimal.Parse(txtLabourCost.Text) + decimal.Parse(txtOverheadCost.Text));
    }

    private void showInv_IssueDetailGrid()
    {

        string codeString = "";

        if (txtCodes.Text != "")
        {
            foreach (string item in txtCodes.Text.Split(','))
            {
                if (item.Trim() != "")
                {
                    if (codeString != "")
                    {
                        codeString += " or ";
                    }

                    codeString += " Inv_Item.ItemCode like '%" + item.Trim() + "%' ";
                }
            }

            if (codeString != "")
            {
                codeString = " and ("+codeString+") ";
            }
        }


        List<Inv_IssueDetail> inv_IssueDetailsFromDB = Inv_IssueDetailManager.GetAllInv_IssueDetailsByEmpoyeeIDnWorkStationIDnProductID(int.Parse(ddlEmployee.SelectedValue), int.Parse(ddlWorkSatation.SelectedValue), int.Parse(ddlProduct.SelectedValue),codeString,chkWithAccsoriesUtilization.Checked,ddlFinalProduct.SelectedValue);

        List<Inv_IssueDetail> inv_IssueDetailsArranged = new List<Inv_IssueDetail>();

        foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
        {
            AddedIssedItem.IsProcessed = false;
            
            if (AddedIssedItem.ProductID ==0)
            {
                AddedIssedItem.ProductName = "N/A";
                AddedIssedItem.ApproximateQuantity = 0;
            }

            if (AddedIssedItem.AdditionalWithIssueDetailID != 0)
            {
                AddedIssedItem.ApproximateQuantity = 0;
            }
        }

        foreach (Inv_IssueDetail rootIssedItem in inv_IssueDetailsFromDB)
        {
            if (rootIssedItem.AdditionalWithIssueDetailID == 0)
            {
                rootIssedItem.ParentChildGap = "";
                rootIssedItem.IsProcessed = true;
                inv_IssueDetailsArranged.Add(rootIssedItem);
                foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
                {
                    if (AddedIssedItem.AdditionalWithIssueDetailID == rootIssedItem.Inv_IssueDetailID)
                    {
                        AddedIssedItem.IsProcessed = true;
                        AddedIssedItem.ParentChildGap = "----";
                        inv_IssueDetailsArranged.Add(AddedIssedItem);
                    }
                }
            }
        }

        foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
        {
            if (!AddedIssedItem.IsProcessed)
            {
                AddedIssedItem.IsProcessed = true;
                AddedIssedItem.ParentChildGap = "";
                inv_IssueDetailsArranged.Add(AddedIssedItem);
            }
        }

        gvInv_IssueDetail.DataSource = inv_IssueDetailsArranged;
        gvInv_IssueDetail.DataBind();

        if (chkWithAccsoriesUtilization.Checked)
        {
            foreach (GridViewRow gvr in gvInv_IssueDetail.Rows)
            {
                HiddenField hfACC_HeadTypeID = (HiddenField)gvr.FindControl("hfACC_HeadTypeID");
                if (hfACC_HeadTypeID.Value == "9")
                {
                    ((CheckBox)gvr.FindControl("chkSelect")).Enabled = false;
                    ((CheckBox)gvr.FindControl("chkSelect")).Checked = true;
                    ((Label)gvr.FindControl("lblItemCode")).ForeColor = System.Drawing.Color.Red;
                    ((Label)gvr.FindControl("lblItemName")).ForeColor = System.Drawing.Color.Red;
                    ((TextBox)gvr.FindControl("txtUtilization")).Text = "0.00";
                }
            }
        }

        decimal totalStock = 0;
        decimal totalApproximateQuantity = 0;
        foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsArranged)
        {
            totalStock += decimal.Parse(AddedIssedItem.ExtraField1);
            totalApproximateQuantity +=AddedIssedItem.ApproximateQuantity;
        }

        try
        {
            ((Label)gvInv_IssueDetail.FooterRow.FindControl("lblTotalAppxQty")).Text = totalApproximateQuantity.ToString("0,0.00");
            ((Label)gvInv_IssueDetail.FooterRow.FindControl("lblStockQuantity")).Text = totalStock.ToString("0,0.00");
        }
        catch (Exception ex) { }
    }



    protected void btnAdd_Click(object sender, EventArgs e)
    {
        String sql = "";
        int utilizationID = 0;
        int JournalMasterID = 0;
        int wastageID = 0;
        if (txtUtilizationID.Text == "0")
        {

            Inv_Wastage inv_Wastage = new Inv_Wastage();

            inv_Wastage.WastageDate = DateTime.Parse(txtDate.Text);
            inv_Wastage.IssueIDs = "";
            inv_Wastage.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
            inv_Wastage.ExtraField1 = "";
            inv_Wastage.ExtraField2 = "";
            inv_Wastage.ExtraField3 = "";
            inv_Wastage.ExtraField4 = "";
            inv_Wastage.ExtraField5 = "";
            inv_Wastage.AddedBy = getLogin().LoginID;
            inv_Wastage.AddedDate = DateTime.Now;
            inv_Wastage.UpdatedBy = getLogin().LoginID;
            inv_Wastage.UpdatedDate = DateTime.Now;
            inv_Wastage.RowStatusID = 1;
            bool isWasted = false;
            /*
            foreach (GridViewRow gvr in gvInv_IssueDetail.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");
                TextBox txtWasted = (TextBox)gvr.FindControl("txtWasted");
                if (chkSelect.Checked && txtWasted.Text != "0")
                {
                    isWasted = true;
                }
            }
            */
            if (isWasted)
            {
                wastageID = Inv_WastageManager.InsertInv_Wastage(inv_Wastage);
                hlnkWastagePrint.NavigateUrl = "WastagePrint.aspx?WastageID="+wastageID;
                hlnkWastagePrint.Visible = true;
            }

            ACC_JournalMaster aCC_JournalMaster = new ACC_JournalMaster();

            aCC_JournalMaster.JournalMasterName = "3";//Voucher Type
            aCC_JournalMaster.ExtraField1 = "";
            aCC_JournalMaster.ExtraField2 = "";
            aCC_JournalMaster.ExtraField3 = "";
            aCC_JournalMaster.Note = txtParticulars.Text;
            aCC_JournalMaster.JournalDate = DateTime.Parse(txtDate.Text);
            aCC_JournalMaster.AddedBy = getLogin().LoginID;
            aCC_JournalMaster.AddedDate = DateTime.Now;
            aCC_JournalMaster.UpdatedBy = getLogin().LoginID;
            aCC_JournalMaster.UpdatedDate = DateTime.Now;
            aCC_JournalMaster.RowStatusID = 1;
            JournalMasterID = ACC_JournalMasterManager.InsertACC_JournalMaster(aCC_JournalMaster);

            Inv_Utilization inv_Utilization = new Inv_Utilization();

            inv_Utilization.UtilizationDate = DateTime.Parse(txtDate.Text);
            inv_Utilization.IssueIDs = "";
            inv_Utilization.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
            inv_Utilization.ExtraField1 = JournalMasterID.ToString();
            inv_Utilization.ExtraField2 = wastageID.ToString();
            inv_Utilization.ExtraField3 = "";
            inv_Utilization.ExtraField4 = "";
            inv_Utilization.ExtraField5 = "";
            inv_Utilization.AddedBy = getLogin().LoginID;
            inv_Utilization.AddedDate = DateTime.Now;
            inv_Utilization.UpdatedBy = getLogin().LoginID;
            inv_Utilization.UpdatedDate = DateTime.Now;
            inv_Utilization.RowStatusID = 1;
            utilizationID = Inv_UtilizationManager.InsertInv_Utilization(inv_Utilization);
            txtUtilizationID.Text = utilizationID.ToString();
            hlnkUtilizationPrint.NavigateUrl = "UtilizationPrint.aspx?UtilizationID="+utilizationID.ToString();
            hlnkUtilizationPrint.Visible = true;

            sql += "Update ACC_JournalMaster set Note='Utilization ID = " + utilizationID.ToString() + "' where ACC_JournalMasterID="+JournalMasterID.ToString()+";";
        }
        else
        {
            utilizationID = int.Parse(txtUtilizationID.Text);
            DataSet ds = CommonManager.SQLExec("select ExtraField1,ExtraField2 from Inv_Utilization where Inv_UtilizationID="+txtUtilizationID.Text);
            if (ds.Tables[0].Rows.Count == 0)
            {
                showAlartMessage("Wrong old utilization ID");
                return;
            }
            JournalMasterID = int.Parse(ds.Tables[0].Rows[0]["ExtraField1"].ToString());
            wastageID = int.Parse(ds.Tables[0].Rows[0]["ExtraField2"].ToString());
        }

        
        string inv_itemID="";
        //update item
        foreach (GridViewRow gvr in gvInv_IssueDetail.Rows)
        {
            TextBox txtUtilization = (TextBox)gvr.FindControl("txtUtilization");
            TextBox txtWasted = (TextBox)gvr.FindControl("txtWasted");
            TextBox txtProductionQuantity = (TextBox)gvr.FindControl("txtProductionQuantity");
            TextBox txtProductionQuantityFresh = (TextBox)gvr.FindControl("txtProductionQuantityFresh");
            TextBox txtProductionQuantityReject = (TextBox)gvr.FindControl("txtProductionQuantityReject");
            Label lblApproximateQuantity = (Label)gvr.FindControl("lblApproximateQuantity");
            Label lblExtraField1 = (Label)gvr.FindControl("lblExtraField1");
            Label lblItemCode = (Label)gvr.FindControl("lblItemCode");
            Label lblPricePerUnit = (Label)gvr.FindControl("lblPricePerUnit");
            Label lblProductName = (Label)gvr.FindControl("lblProductName");
            CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");
            CheckBox chkMain = (CheckBox)gvr.FindControl("chkMain");

            
            HiddenField hfItemID = (HiddenField)gvr.FindControl("hfItemID");
            HiddenField hfRawMaterialID = (HiddenField)gvr.FindControl("hfRawMaterialID");
            HiddenField hfInv_IssueDetailID = (HiddenField)gvr.FindControl("hfInv_IssueDetailID");
            HiddenField hfACC_HeadTypeID = (HiddenField)gvr.FindControl("hfACC_HeadTypeID");
            txtWasted.Text = "0";
            decimal totalmoney = (decimal.Parse(txtUtilization.Text) + decimal.Parse(txtWasted.Text));

            if (chkSelect.Checked && decimal.Parse(txtUtilization.Text)!=0)
            {

                if (decimal.Parse(lblExtraField1.Text) < (decimal.Parse(txtUtilization.Text) + decimal.Parse(txtWasted.Text)))
                {
                    showAlartMessage("Stock can not be less than utilization and wastage for " + lblItemCode.Text);
                    return;
                }

                if (chkMain.Checked)
                {
                    inv_itemID=hfItemID.Value;
                }

                //update Inv_IssueDetail
                sql += "Update Inv_IssueDetail set ExtraField1 =(cast ((cast(ExtraField1 as decimal(18,2))-" + txtUtilization.Text + ") as nvarchar)) " +
                        @", ExtraField3 =(cast ((cast(ExtraField3 as decimal(18,2))+" + txtUtilization.Text + ") as nvarchar)) "
                        + (txtWasted.Text == "0" ? "" : ", ExtraField4 =(cast ((cast(ExtraField4 as decimal(18,2))+" + txtWasted.Text + ") as nvarchar)) ")
                        + (lblProductName.Text == "N/A" ? "" : ", AdditionalWithIssueDetailID+=" + txtProductionQuantity.Text )                
                        +"  where Inv_IssueDetailID=" + hfInv_IssueDetailID.Value + ";";
                
                //update Inv_Item
                sql += "Update Inv_Item set UtilizedQuantity+= "+txtUtilization.Text
                        + (txtWasted.Text == "0" ? "" : ", ExtraFieldQuantity4 +=" + txtWasted.Text + " ")
                    + "  where Inv_ItemID=" + hfItemID.Value + ";";

                //Inv_ItemTransaction
                sql += @"INSERT INTO [Inv_ItemTransaction]
                       ([ItemID]
                       ,[Quantity]
                       ,[ItemTrasactionTypeID]
                       ,[ReferenceID]
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
                        VALUES (" + hfItemID.Value + "," + txtUtilization.Text + ",4," + utilizationID
                                  + ",'" + (chkMain.Checked ? txtProductionQuantity.Text : "0") + @"','" + (chkMain.Checked ? lblApproximateQuantity.Text : "0") + @"','" + hfInv_IssueDetailID.Value + @"','" + (chkMain.Checked ? txtProductionQuantityFresh.Text : "0") + @"','" + (chkMain.Checked ? txtProductionQuantityReject.Text : "0") + @"'," + getLogin().LoginID + ",GETDATE()," + getLogin().LoginID + ",'" + DateTime.Parse(txtDate.Text).ToString("yyyy-MM-dd") + "',1);";

                sql += @"INSERT INTO [ACC_JournalDetail]
                               ([JournalMasterID]
                               ,[ACC_ChartOfAccountLabel4ID]
                               ,[ACC_ChartOfAccountLabel3ID]
                               ,[WorkStation]
                               ,[Debit]
                               ,[Credit]
                               ,[ExtraField3]
                               ,[ExtraField2]
                               ,[ExtraField1]
                               ,[AddedBy]
                               ,[AddedDate]
                               ,[UpdatedBy]
                               ,[UpdatedDate]
                               ,[RowStatusID])
                         VALUES("
                    + JournalMasterID + ","
                    + ddlFinalProduct.SelectedValue + ","
                    + "9" + ","
                    + ddlWorkSatation.SelectedValue + ","
                    + (totalmoney * decimal.Parse(lblPricePerUnit.Text)).ToString("0.00") + ","
                    + "0" + ","
                    + "''" + ","
                    + "''" + ","
                    + "'" + hfItemID.Value + "'" + ","
                    + getLogin().LoginID + ","
                             + "GETDATE()" + ","
                             + getLogin().LoginID + ","
                             + "GETDATE()" + ","
                             + "1);"
                             ;

                sql += @"INSERT INTO [ACC_JournalDetail]
                               ([JournalMasterID]
                               ,[ACC_ChartOfAccountLabel4ID]
                               ,[ACC_ChartOfAccountLabel3ID]
                               ,[WorkStation]
                               ,[Debit]
                               ,[Credit]
                               ,[ExtraField3]
                               ,[ExtraField2]
                               ,[ExtraField1]
                               ,[AddedBy]
                               ,[AddedDate]
                               ,[UpdatedBy]
                               ,[UpdatedDate]
                               ,[RowStatusID])
                         VALUES("
                    + JournalMasterID + ","
                    + hfRawMaterialID.Value + ","
                    + (hfACC_HeadTypeID.Value=="2"? "5":"7") + ","
                    + ddlWorkSatation.SelectedValue + ","
                    + "0" + ","
                    + (totalmoney * decimal.Parse(lblPricePerUnit.Text)).ToString("0.00") + ","
                    + "''" + ","
                    + "''" + ","
                    + "'" + hfItemID.Value + "'" + ","
                    + getLogin().LoginID + ","
                             + "GETDATE()" + ","
                             + getLogin().LoginID + ","
                             + "GETDATE()" + ","
                             + "1);"
                             ;

                /*
                if (txtWasted.Text != "0")
                {
                    sql += @"INSERT INTO [Inv_ItemTransaction]
                       ([ItemID]
                       ,[Quantity]
                       ,[ItemTrasactionTypeID]
                       ,[ReferenceID]
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
                        VALUES (" + hfItemID.Value + "," + txtWasted.Text + ",6," + wastageID
                                  + ",'','','','',''," + getLogin().LoginID + ",GETDATE()," + getLogin().LoginID + ",'" + DateTime.Parse(txtDate.Text).ToString("yyyy-MM-dd") + "',1);";

                    sql += @"INSERT INTO [ACC_JournalDetail]
                               ([JournalMasterID]
                               ,[ACC_ChartOfAccountLabel4ID]
                               ,[ACC_ChartOfAccountLabel3ID]
                               ,[WorkStation]
                               ,[Debit]
                               ,[Credit]
                               ,[ExtraField3]
                               ,[ExtraField2]
                               ,[ExtraField1]
                               ,[AddedBy]
                               ,[AddedDate]
                               ,[UpdatedBy]
                               ,[UpdatedDate]
                               ,[RowStatusID])
                         VALUES("
                        + JournalMasterID + ","
                        + ddlFinalProduct.SelectedValue + ","
                        + "9" + ","
                        + ddlWorkSatation.SelectedValue + ","
                        + (totalmoney * decimal.Parse(lblPricePerUnit.Text)).ToString("0.00") + ","
                        + "0" + ","
                        + "''" + ","
                        + "''" + ","
                        + "'" + hfItemID.Value + "'" + ","
                        + getLogin().LoginID + ","
                                 + "GETDATE()" + ","
                                 + getLogin().LoginID + ","
                                 + "GETDATE()" + ","
                                 + "1);"
                                 ;

                    sql += @"INSERT INTO [ACC_JournalDetail]
                               ([JournalMasterID]
                               ,[ACC_ChartOfAccountLabel4ID]
                               ,[ACC_ChartOfAccountLabel3ID]
                               ,[WorkStation]
                               ,[Debit]
                               ,[Credit]
                               ,[ExtraField3]
                               ,[ExtraField2]
                               ,[ExtraField1]
                               ,[AddedBy]
                               ,[AddedDate]
                               ,[UpdatedBy]
                               ,[UpdatedDate]
                               ,[RowStatusID])
                         VALUES("
                        + JournalMasterID + ","
                        + hfRawMaterialID.Value + ","
                        +  "7" + ","
                        + ddlWorkSatation.SelectedValue + ","
                        + "0" + ","
                        + (totalmoney * decimal.Parse(lblPricePerUnit.Text)).ToString("0.00") + ","
                        + "''" + ","
                        + "''" + ","
                        + "'" + hfItemID.Value + "'" + ","
                        + getLogin().LoginID + ","
                                 + "GETDATE()" + ","
                                 + getLogin().LoginID + ","
                                 + "GETDATE()" + ","
                                 + "1);"
                                 ;

                }
                */
            }
        }

        foreach (DataListItem item in dlSize.Items)
        {
            TextBox txtQty = (TextBox)item.FindControl("txtQty");
            if (txtQty.Text != "")
            {
                sql += @"INSERT INTO [Inv_UtilizationDetails]
                           ([Pos_SizeID]
                           ,[ProductID]
                           ,[Inv_ItemID]
                           ,[Inv_UtilizationID]
                           ,[Inv_ItemTransactionID]
                           ,[FabricsCost]
                           ,[AccesoriesCost]
                           ,[Overhead]
                           ,[OthersCost]
                           ,[ProductionQuantity]
                           ,[ProcessedQuantity]
                           ,[IsReject]
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
                           ("+txtQty.ValidationGroup+","
                             +ddlFinalProduct.SelectedValue+","
                             + inv_itemID + ","
                             + utilizationID + ","
                             + "0" + ","
                             + txtFabricsCost.Text + ","
                             + txtAccessoriesCost.Text + ","
                             + txtOverheadCost.Text + ","
                             + txtLabourCost.Text + ","
                             + txtQty.Text + ","
                             + "0" + ","
                             + "0" + ","
                             + ddlPos_Color.SelectedValue + ","
                             + "''" + ","
                             + "''" + ","
                             + "''" + ","
                             + "''" + ","
                             + getLogin().LoginID + ","
                             +"GETDATE()"+","
                             + getLogin().LoginID + ","
                             + "GETDATE()" + ","
                             +"1);"
                             ;
            }
        }

        foreach (DataListItem item in dlSizeRejected.Items)
        {
            TextBox txtQty = (TextBox)item.FindControl("txtQty");
            if (txtQty.Text != "")
            {
                sql += @"INSERT INTO [Inv_UtilizationDetails]
                           ([Pos_SizeID]
                           ,[ProductID]
                           ,[Inv_ItemID]
                           ,[Inv_UtilizationID]
                           ,[Inv_ItemTransactionID]
                           ,[FabricsCost]
                           ,[AccesoriesCost]
                           ,[Overhead]
                           ,[OthersCost]
                           ,[ProductionQuantity]
                           ,[ProcessedQuantity]
                           ,[IsReject]
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
                           (" + txtQty.ValidationGroup + ","
                             + ddlFinalProduct.SelectedValue + ","
                             + inv_itemID + ","
                              + utilizationID + ","
                             + "0" + ","
                             + txtFabricsCost.Text + ","
                             + txtAccessoriesCost.Text + ","
                             + txtOverheadCost.Text + ","
                             + txtLabourCost.Text + ","
                             + txtQty.Text + ","
                             + "0" + ","
                             + "1" + ","
                             + ddlPos_Color.SelectedValue + ","
                             + "''" + ","
                             + "''" + ","
                             + "''" + ","
                             + "''" + ","
                             + getLogin().LoginID + ","
                             + "GETDATE()" + ","
                             + getLogin().LoginID + ","
                             + "GETDATE()" + ","
                             + "1);"
                             ;
            }
        }
        try
        {
            if (CommonManager.SQLExec(sql + "select 1;").Tables[0].Rows[0][0].ToString() == "1")
            {
                showAlartMessage("Successfully added the products");
                cearlAlltheControl();
                //btnSearch_Click(this, new EventArgs());
            }
        }
        catch (Exception ex)
        {
            showAlartMessage("Someting wrong");
        }
    }

    protected void btnWastage_Click(object sender, EventArgs e)
    {
        String sql = "";
        int utilizationID = 0;
        int JournalMasterID = 0;
        int wastageID = 0;
        if (txtUtilizationID.Text == "0")
        {
            ACC_JournalMaster aCC_JournalMaster = new ACC_JournalMaster();

            aCC_JournalMaster.JournalMasterName = "3";//Voucher Type
            aCC_JournalMaster.ExtraField1 = "";
            aCC_JournalMaster.ExtraField2 = "";
            aCC_JournalMaster.ExtraField3 = "";
            aCC_JournalMaster.Note = txtParticulars.Text;
            aCC_JournalMaster.JournalDate = DateTime.Parse(txtDate.Text);
            aCC_JournalMaster.AddedBy = getLogin().LoginID;
            aCC_JournalMaster.AddedDate = DateTime.Now;
            aCC_JournalMaster.UpdatedBy = getLogin().LoginID;
            aCC_JournalMaster.UpdatedDate = DateTime.Now;
            aCC_JournalMaster.RowStatusID = 1;
            JournalMasterID = 0;//ACC_JournalMasterManager.InsertACC_JournalMaster(aCC_JournalMaster);


            Inv_Wastage inv_Wastage = new Inv_Wastage();

            inv_Wastage.WastageDate = DateTime.Parse(txtDate.Text);
            inv_Wastage.IssueIDs = "";
            inv_Wastage.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
            inv_Wastage.ExtraField1 = JournalMasterID.ToString();
            inv_Wastage.ExtraField2 = "";
            inv_Wastage.ExtraField3 = "";
            inv_Wastage.ExtraField4 = "";
            inv_Wastage.ExtraField5 = "";
            inv_Wastage.AddedBy = getLogin().LoginID;
            inv_Wastage.AddedDate = DateTime.Now;
            inv_Wastage.UpdatedBy = getLogin().LoginID;
            inv_Wastage.UpdatedDate = DateTime.Now;
            inv_Wastage.RowStatusID = 1;
            bool isWasted = false;
            foreach (GridViewRow gvr in gvInv_IssueDetail.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");
                TextBox txtWasted = (TextBox)gvr.FindControl("txtWasted");
                if (chkSelect.Checked && txtWasted.Text != "0")
                {
                    isWasted = true;
                }
            }
            if (isWasted)
            {
                wastageID = Inv_WastageManager.InsertInv_Wastage(inv_Wastage);
                hlnkWastagePrint.NavigateUrl = "WastagePrint.aspx?WastageID=" + wastageID;
                hlnkWastagePrint.Visible = true;
            }



            sql += "Update ACC_JournalMaster set Note='Wastage ID = " + wastageID.ToString() + "' where ACC_JournalMasterID=" + JournalMasterID.ToString() + ";";
        }
        else
        {
            wastageID = int.Parse(txtUtilizationID.Text);
            DataSet ds = CommonManager.SQLExec("select ExtraField1 from Inv_Wastage where Inv_WastageID=" + txtUtilizationID.Text);
            if (ds.Tables[0].Rows.Count == 0)
            {
                showAlartMessage("Wrong old Wastage ID");
                return;
            }
            JournalMasterID = int.Parse(ds.Tables[0].Rows[0]["ExtraField1"].ToString());
        }


        string inv_itemID = "";
        //update item
        foreach (GridViewRow gvr in gvInv_IssueDetail.Rows)
        {
            TextBox txtUtilization = (TextBox)gvr.FindControl("txtUtilization");
            TextBox txtWasted = (TextBox)gvr.FindControl("txtWasted");
            TextBox txtProductionQuantity = (TextBox)gvr.FindControl("txtProductionQuantity");
            TextBox txtProductionQuantityFresh = (TextBox)gvr.FindControl("txtProductionQuantityFresh");
            TextBox txtProductionQuantityReject = (TextBox)gvr.FindControl("txtProductionQuantityReject");
            Label lblApproximateQuantity = (Label)gvr.FindControl("lblApproximateQuantity");
            Label lblExtraField1 = (Label)gvr.FindControl("lblExtraField1");
            Label lblItemCode = (Label)gvr.FindControl("lblItemCode");
            Label lblPricePerUnit = (Label)gvr.FindControl("lblPricePerUnit");
            Label lblProductName = (Label)gvr.FindControl("lblProductName");
            CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");
            CheckBox chkMain = (CheckBox)gvr.FindControl("chkMain");


            HiddenField hfItemID = (HiddenField)gvr.FindControl("hfItemID");
            HiddenField hfRawMaterialID = (HiddenField)gvr.FindControl("hfRawMaterialID");
            HiddenField hfInv_IssueDetailID = (HiddenField)gvr.FindControl("hfInv_IssueDetailID");
            HiddenField hfACC_HeadTypeID = (HiddenField)gvr.FindControl("hfACC_HeadTypeID");

            decimal totalmoney = decimal.Parse(txtWasted.Text);

            if (chkSelect.Checked && decimal.Parse(txtWasted.Text) != 0)
            {

                if (
                    decimal.Parse(lblExtraField1.Text)
                    <
                    (decimal.Parse(txtWasted.Text))
                    )
                {
                    showAlartMessage("Stock can not be less than utilization and wastage for " + lblItemCode.Text);
                    return;
                }

                if (chkMain.Checked)
                {
                    inv_itemID = hfItemID.Value;
                }

                //update Inv_IssueDetail
                //update Inv_IssueDetail
                sql += "Update Inv_IssueDetail set ExtraField1 =(cast ((cast(ExtraField1 as decimal(18,2))-" + txtWasted.Text + ") as nvarchar)) " 
                        + (txtWasted.Text == "0" ? "" : ", ExtraField4 =(cast ((cast(ExtraField4 as decimal(18,2))+" + txtWasted.Text + ") as nvarchar)) ")
                        + "  where Inv_IssueDetailID=" + hfInv_IssueDetailID.Value + ";";
                

                //update Inv_Item
                sql += "Update Inv_Item set "
                        + (txtWasted.Text == "0" ? "" : "ExtraFieldQuantity4 +=" + txtWasted.Text + " ")
                    + "  where Inv_ItemID=" + hfItemID.Value + ";";


                if (txtWasted.Text != "0")
                {
                    sql += @"INSERT INTO [Inv_ItemTransaction]
                       ([ItemID]
                       ,[Quantity]
                       ,[ItemTrasactionTypeID]
                       ,[ReferenceID]
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
                        VALUES (" + hfItemID.Value + "," + txtWasted.Text + ",6," + wastageID
                                  + ",'','','','',''," + getLogin().LoginID + ",GETDATE()," + getLogin().LoginID + ",'" + DateTime.Parse(txtDate.Text).ToString("yyyy-MM-dd") + "',1);";
                    /*
                    sql += @"INSERT INTO [ACC_JournalDetail]
                               ([JournalMasterID]
                               ,[ACC_ChartOfAccountLabel4ID]
                               ,[ACC_ChartOfAccountLabel3ID]
                               ,[WorkStation]
                               ,[Debit]
                               ,[Credit]
                               ,[ExtraField3]
                               ,[ExtraField2]
                               ,[ExtraField1]
                               ,[AddedBy]
                               ,[AddedDate]
                               ,[UpdatedBy]
                               ,[UpdatedDate]
                               ,[RowStatusID])
                         VALUES("
                        + JournalMasterID + ","
                        + ddlFinalProduct.SelectedValue + ","
                        + "9" + ","
                        + ddlWorkSatation.SelectedValue + ","
                        + (totalmoney * decimal.Parse(lblPricePerUnit.Text)).ToString("0.00") + ","
                        + "0" + ","
                        + "''" + ","
                        + "''" + ","
                        + "'" + hfItemID.Value + "'" + ","
                        + getLogin().LoginID + ","
                                 + "GETDATE()" + ","
                                 + getLogin().LoginID + ","
                                 + "GETDATE()" + ","
                                 + "1);"
                                 ;

                    sql += @"INSERT INTO [ACC_JournalDetail]
                               ([JournalMasterID]
                               ,[ACC_ChartOfAccountLabel4ID]
                               ,[ACC_ChartOfAccountLabel3ID]
                               ,[WorkStation]
                               ,[Debit]
                               ,[Credit]
                               ,[ExtraField3]
                               ,[ExtraField2]
                               ,[ExtraField1]
                               ,[AddedBy]
                               ,[AddedDate]
                               ,[UpdatedBy]
                               ,[UpdatedDate]
                               ,[RowStatusID])
                         VALUES("
                        + JournalMasterID + ","
                        + hfRawMaterialID.Value + ","
                        + "7" + ","
                        + ddlWorkSatation.SelectedValue + ","
                        + "0" + ","
                        + (totalmoney * decimal.Parse(lblPricePerUnit.Text)).ToString("0.00") + ","
                        + "''" + ","
                        + "''" + ","
                        + "'" + hfItemID.Value + "'" + ","
                        + getLogin().LoginID + ","
                                 + "GETDATE()" + ","
                                 + getLogin().LoginID + ","
                                 + "GETDATE()" + ","
                                 + "1);"
                                 ;
                     */ 

                }
            }
        }


        try
        {
            if (CommonManager.SQLExec(sql + "select 1;").Tables[0].Rows[0][0].ToString() == "1")
            {
                showAlartMessage("Successfully Submitted the wastage");
                cearlAlltheControl();
                //btnSearch_Click(this, new EventArgs());
            }
        }
        catch (Exception ex)
        {
            showAlartMessage("Someting wrong");
        }
    }

    private void cearlAlltheControl()
    {
        txtCodes.Text = "";
        //ddlFinalProduct.SelectedValue = "0";
        List<Inv_IssueDetail> issuseDetails = new List<Inv_IssueDetail>();
        gvInv_IssueDetail.DataSource = issuseDetails;
        gvInv_IssueDetail.DataBind();

        txtFabricsCost.Text = "";
        txtLabourCost.Text = "";
        txtOverheadCost.Text = "";
        txtAccessoriesCost.Text = "";
        loadSizeGrid();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlProduct.SelectedIndex = 0;
        
    }
    private void showInv_ProductData()
    {
        
    }

   

    protected void btnIssueReturn_Click(object sender, EventArgs e)
    {
        bool hasReturnItem = false;

        foreach (GridViewRow gvr in gvInv_IssueDetail.Rows)
        {
            TextBox txtIssueReturn = (TextBox)gvr.FindControl("txtIssueReturn");
            if (txtIssueReturn.Text != "0" || txtIssueReturn.Text != "")
            {
                hasReturnItem = true;
                break;
            }
        }

        if (hasReturnItem)
        {
            Inv_IssueMasterReturn inv_IssueMasterReturn = new Inv_IssueMasterReturn();

            inv_IssueMasterReturn.IssueReturnName = "";
            inv_IssueMasterReturn.IssueReturnDate = DateTime.Parse(txtDate.Text);
            inv_IssueMasterReturn.EmployeeID = Int32.Parse(ddlEmployee.SelectedValue);
            inv_IssueMasterReturn.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
            inv_IssueMasterReturn.Particulars = txtParticulars.Text;
            inv_IssueMasterReturn.IsIssue = ddlProduct.SelectedValue =="0"?false:true;
            inv_IssueMasterReturn.ExtraField1 = "";
            inv_IssueMasterReturn.ExtraField2 = "";
            inv_IssueMasterReturn.ExtraField3 = "";
            inv_IssueMasterReturn.ExtraField4 = "";
            inv_IssueMasterReturn.ExtraField5 = "";
            inv_IssueMasterReturn.AddedBy = getLogin().LoginID;
            inv_IssueMasterReturn.AddedDate = DateTime.Now;
            inv_IssueMasterReturn.UpdatedBy = getLogin().LoginID;
            inv_IssueMasterReturn.UpdatedDate = DateTime.Now;
            inv_IssueMasterReturn.RowStatusID = 1;
            inv_IssueMasterReturn.Inv_IssueMasterReturnID = Inv_IssueMasterReturnManager.InsertInv_IssueMasterReturn(inv_IssueMasterReturn);

            foreach (GridViewRow gvr in gvInv_IssueDetail.Rows)
            {
                TextBox txtIssueReturn = (TextBox)gvr.FindControl("txtIssueReturn");
                HiddenField hfItemID = (HiddenField)gvr.FindControl("hfItemID");
                HiddenField hfInv_IssueDetailID = (HiddenField)gvr.FindControl("hfInv_IssueDetailID");
                
                if (txtIssueReturn.Text != "0" && txtIssueReturn.Text != "")
                {
                    Inv_ItemTransaction inv_ItemTransaction = new Inv_ItemTransaction();

                    inv_ItemTransaction.ItemID = Int32.Parse(hfItemID.Value);
                    inv_ItemTransaction.Quantity = Decimal.Parse(txtIssueReturn.Text);
                    inv_ItemTransaction.ItemTrasactionTypeID = 3;//issue return
                    inv_ItemTransaction.ReferenceID = inv_IssueMasterReturn.Inv_IssueMasterReturnID;
                    inv_ItemTransaction.ExtraField1 = hfInv_IssueDetailID.Value;
                    inv_ItemTransaction.ExtraField2 = "";
                    inv_ItemTransaction.ExtraField3 = "";
                    inv_ItemTransaction.ExtraField4 = "";
                    inv_ItemTransaction.ExtraField5 = inv_IssueMasterReturn.Inv_IssueMasterReturnID.ToString();
                    inv_ItemTransaction.AddedBy = getLogin().LoginID;
                    inv_ItemTransaction.AddedDate = DateTime.Now;
                    inv_ItemTransaction.UpdatedBy = getLogin().LoginID;
                    inv_ItemTransaction.UpdatedDate = DateTime.Parse(txtDate.Text);
                    inv_ItemTransaction.RowStatusID = 1;
                    int resutl = Inv_ItemTransactionManager.InsertInv_ItemTransaction(inv_ItemTransaction);
                }
            }

            hlnkIssueReturnPrint.NavigateUrl = "IssueReturnPrint.aspx?IssueReturnID=" + inv_IssueMasterReturn.Inv_IssueMasterReturnID;
            hlnkIssueReturnPrint.Visible = true;
        }
    }
    protected void ddlFinalProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadSizeGrid(ddlFinalProduct.SelectedValue);
    }

    private void loadSizeGrid()
    {      
        string sql = @"Select Pos_Size.* from Pos_Size";


        DataSet ds = CommonManager.SQLExec(sql);

        dlSize.DataSource = ds.Tables[0];
        dlSize.DataBind();

        dlSizeRejected.DataSource = ds.Tables[0];
        dlSizeRejected.DataBind();
    }

    private void showAlartMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "err_msg",
             "alert('" + message + "');",
             true);
    }
}
