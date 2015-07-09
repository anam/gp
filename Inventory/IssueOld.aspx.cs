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

public partial class AdminInv_IssueDetailInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadItem();
            loadACC_ChartOfAccountLabel4();
            
            loadACC_ChartOfAccountLabel4();
            if (Request.QueryString["inv_IssueDetailID"] != null)
            {
                int inv_IssueDetailID = Int32.Parse(Request.QueryString["inv_IssueDetailID"]);
                if (inv_IssueDetailID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_IssueDetailData();
                }
            }

            if (Request.QueryString["IsNonProduction"] != null)
            {
                trProduct.Visible = false;
                trApproximateQuantity.Visible = false;
                trClaculateApproximateQuantity.Visible = false;
            }
            initialLoad();
            
        }
    }

    private void initialLoad()
    {
        txtIssueDate.Text = DateTime.Today.ToString("dd MMM yyyy");
        ddlEmployee.SelectedValue = "622";
        ddlWorkSatation.SelectedValue = "2";
    }
    private void loadACC_ChartOfAccountLabel4()
    {
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlEmployee.Items.Add(new ListItem("Select Employee", "0"));
        ddlWorkSatation.Items.Add(new ListItem("Select WorkStation", "0"));
        ddlProduct.Items.Add(new ListItem("Select Product", "0"));

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
            }
        }

    }
    private Login getLogin()
    {
        Login login = new Login();
        try
        {
            if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }

            login = (Login)Session["Login"];
        }
        catch (Exception ex)
        { }

        return login;
    }

    protected void btnClaculateApproximateQuantity_Click(object sender, EventArgs e)
    {
        List<Inv_ProductionConfiguration> inv_ProductionConfigurations= Inv_ProductionConfigurationManager.GetAllInv_ProductionConfigurationsByProductIDnItemID(int.Parse(ddlProduct.SelectedValue), int.Parse(ddlItem.SelectedValue));

        bool IsfoundConfig = false;
        foreach (Inv_ProductionConfiguration item in inv_ProductionConfigurations)
        {
            if (
                item.QualityValue.ToString("0.00") 
                == ddlItem.SelectedItem.Text.Split('{')[1].Split('}')[0].Split(' ')[0].Trim()
                &&
                item.ExtraField4
                == ddlItem.SelectedItem.Text.Split('{')[1].Split('}')[0].Split(' ')[1].Trim()
                &&
                item.ExtraField3
                == ddlItem.SelectedItem.Text.Split('[')[1].Split(']')[0].Split(' ')[1].Trim()
                )
            {
                IsfoundConfig = true;
                txtApproximateQuantity.Text = (decimal.Parse(txtQuantity.Text) / item.QuantityValue).ToString("0,0");
                break;
            }
        }

        if (!IsfoundConfig)
        {
            trConfigPerProductRequiredQuantity.Visible = true;
            trClaculateApproximateQuantity.Visible = true;
            lblApproximateQuantity.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            trClaculateApproximateQuantity.Visible = false;
            lblApproximateQuantity.ForeColor = System.Drawing.Color.Black;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        hlnkIssuePrint.NavigateUrl = "IssuePrint.aspx?IssueID=" + hfInv_IssueMasterID.Value;
        hlnkIssuePrint.Visible = true;
        hfInv_IssueMasterID.Value = "0";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //if(Request.QueryString["IsNonProduction"] == null)
        //addConfiguration();

        if (Request.QueryString["IsNonProduction"] != null)
        {
            txtApproximateQuantity.Text = txtQuantity.Text;
        }

        if (hfInv_IssueMasterID.Value == "0" || hfInv_IssueMasterID.Value == "")
        {
            Inv_IssueMaster inv_IssueMaster = new Inv_IssueMaster();

            inv_IssueMaster.IssueName = "";
            inv_IssueMaster.IssueDate = DateTime.Parse(txtIssueDate.Text);
            inv_IssueMaster.EmployeeID = Int32.Parse(ddlEmployee.SelectedValue);
            inv_IssueMaster.WorkSatationID = Int32.Parse(ddlWorkSatation.SelectedValue);
            inv_IssueMaster.Particulars = txtParticulars.Text;
            inv_IssueMaster.IsIssue =( Request.QueryString["IsNonProduction"] != null ?false:true);
            inv_IssueMaster.ExtraField1 = "";
            inv_IssueMaster.ExtraField2 = "";
            inv_IssueMaster.ExtraField3 = "";
            inv_IssueMaster.ExtraField4 = "";
            inv_IssueMaster.ExtraField5 = "";
            inv_IssueMaster.AddedBy = getLogin().LoginID;
            inv_IssueMaster.AddedDate = DateTime.Now;
            inv_IssueMaster.UpdatedBy = getLogin().LoginID;
            inv_IssueMaster.UpdatedDate = DateTime.Now;
            inv_IssueMaster.RowStatusID = 1;
            hfInv_IssueMasterID.Value = Inv_IssueMasterManager.InsertInv_IssueMaster(inv_IssueMaster).ToString();
        }

        Inv_IssueDetail inv_IssueDetail = new Inv_IssueDetail();
        inv_IssueDetail.ItemID = Int32.Parse(ddlItem.SelectedValue);
        inv_IssueDetail.Quantity = Decimal.Parse(txtQuantity.Text);
        inv_IssueDetail.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        if (ddlIssuedItem.Items.Count == 0 || ddlIssuedItem.SelectedValue == "0")
        {
            inv_IssueDetail.AdditionalWithIssueDetailID = 0;
            inv_IssueDetail.ApproximateQuantity = Int32.Parse(txtApproximateQuantity.Text);
        }
        else
        {
            inv_IssueDetail.AdditionalWithIssueDetailID = int.Parse(ddlIssuedItem.SelectedValue);
            inv_IssueDetail.ApproximateQuantity = 0;
        }
        
        //inv_IssueDetail.ExtraField1 = ddlItem.SelectedItem.Text.Split(' ')[0]; //Item COde
        //inv_IssueDetail.ExtraField2 = ddlItem.SelectedItem.Text.Split('{')[0].Trim().Replace(inv_IssueDetail.ExtraField1 +" - ","");//item Name
        //inv_IssueDetail.ExtraField3 = ddlItem.SelectedItem.Text.Split('{')[1].Split('}')[0];//quality
        //inv_IssueDetail.ExtraField4 = Request.QueryString["IsNonProduction"] != null ?"":ddlProduct.SelectedItem.Text;//productName
        //inv_IssueDetail.ExtraField5 = Inv_ItemManager.GetInv_ItemByID(Int32.Parse(ddlItem.SelectedValue)).PricePerUnit.ToString("0,0.00");//Price Per unit
        inv_IssueDetail.ExtraField1 = txtQuantity.Text;
        inv_IssueDetail.ExtraField2 = "0";
        inv_IssueDetail.ExtraField3 = "0";
        inv_IssueDetail.ExtraField4 = "0";
        inv_IssueDetail.ExtraField5 = hfInv_IssueMasterID.Value;
        
        inv_IssueDetail.AddedBy =getLogin().LoginID;
        inv_IssueDetail.AddedDate = DateTime.Now;
        inv_IssueDetail.UpdatedBy = getLogin().LoginID;
        inv_IssueDetail.UpdatedDate = DateTime.Now;
        inv_IssueDetail.RowStatusID = 1;
        Inv_IssueDetailManager.InsertInv_IssueDetail(inv_IssueDetail);
        showInv_IssueDetailGrid();
        //btnClear_Click(this, new EventArgs());
    }

    private void addConfiguration()
    {
        if (txtConfigPerProductRequiredQuantity.Text != "0")
        {
            Inv_Item itemSelected = Inv_ItemManager.GetInv_ItemByID(int.Parse(ddlItem.SelectedValue));

            Inv_ProductionConfiguration inv_ProductionConfiguration = new Inv_ProductionConfiguration();

            inv_ProductionConfiguration.ProductID = Int32.Parse(ddlProduct.SelectedValue);
            inv_ProductionConfiguration.QualityValue = itemSelected.QualityValue;
            inv_ProductionConfiguration.QualityUnitID = itemSelected.QualityUnitID;
            inv_ProductionConfiguration.QuantityValue = Decimal.Parse(txtConfigPerProductRequiredQuantity.Text);
            inv_ProductionConfiguration.QuantityUnitID = itemSelected.QuantityUnitID;
            inv_ProductionConfiguration.RawMaterialID = itemSelected.RawMaterialID;
            inv_ProductionConfiguration.ExtraField1 = "";
            inv_ProductionConfiguration.ExtraField2 = "";
            inv_ProductionConfiguration.ExtraField3 = "";
            inv_ProductionConfiguration.ExtraField4 = "";
            inv_ProductionConfiguration.ExtraField5 = "";
            inv_ProductionConfiguration.AddedBy = getLogin().LoginID;
            inv_ProductionConfiguration.AddedDate = DateTime.Now;
            inv_ProductionConfiguration.UpdatedBy = getLogin().LoginID;
            inv_ProductionConfiguration.UpdatedDate = DateTime.Now;
            inv_ProductionConfiguration.RowStatusID = 1;
            int resutl = Inv_ProductionConfigurationManager.InsertInv_ProductionConfiguration(inv_ProductionConfiguration);
        
            trConfigPerProductRequiredQuantity.Visible = false;
        }
    }

    private void showInv_IssueDetailGrid()
    {
        List<Inv_IssueDetail> inv_IssueDetailsFromDB = Inv_IssueDetailManager.GetAllInv_IssueDetailsByIssueMasterID(hfInv_IssueMasterID.Value);

        List<Inv_IssueDetail> inv_IssueDetailsArranged = new List<Inv_IssueDetail>();

        foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
        {
            AddedIssedItem.IsProcessed = false;
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

        loadItem();
        loadPrevioulyIssedItem();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlItem.SelectedIndex = 0;
        txtQuantity.Text = "";
        txtApproximateQuantity.Text = "";
        ddlProduct.SelectedIndex = 0;
        
    }
    private void showInv_IssueDetailData()
    {
        Inv_IssueDetail inv_IssueDetail = new Inv_IssueDetail();
        inv_IssueDetail = Inv_IssueDetailManager.GetInv_IssueDetailByID(Int32.Parse(Request.QueryString["inv_IssueDetailID"]));

        ddlItem.SelectedValue = inv_IssueDetail.ItemID.ToString();
        txtQuantity.Text = inv_IssueDetail.Quantity.ToString();
        txtApproximateQuantity.Text = inv_IssueDetail.ApproximateQuantity.ToString();
        ddlProduct.SelectedValue = inv_IssueDetail.ProductID.ToString();
    }
    private void loadItem()
    {
        ddlItem.Items.Clear();

        ListItem li = new ListItem("Select Item...", "0");
        ddlItem.Items.Add(li);

        List<Inv_Item> items = new List<Inv_Item>();
        items = Inv_ItemManager.GetAllInv_ItemsInStock();
        foreach (Inv_Item item in items)
        {
            ListItem li_item = new ListItem(item.ItemCode.ToString() + " - " + item.ItemName.ToString() + " {" + item.QualityValue.ToString() + " " + item.ExtraField1.ToString() + "}" + " [" + item.ExtraFieldQuantity1.ToString() + " " + item.ExtraField2.ToString() + "]", item.Inv_ItemID.ToString());
            ddlItem.Items.Add(li_item);
        }
    }

    private void loadPrevioulyIssedItem()
    {
        ddlIssuedItem.Items.Clear();

        //trConfigPerProductRequiredQuantity.Visible = true;
        trClaculateApproximateQuantity.Visible = true;
        trApproximateQuantity.Visible = true;
        if (Request.QueryString["IsNonProduction"] != null)
        {
            trProduct.Visible = false;
            trApproximateQuantity.Visible = false;
            trClaculateApproximateQuantity.Visible = false;
            trConfigPerProductRequiredQuantity.Visible = false;
        }


        ListItem li = new ListItem("Select Issued Item...", "0");
        ddlIssuedItem.Items.Add(li);

        List<Inv_IssueDetail> inv_IssueDetails = new List<Inv_IssueDetail>();
        if (Request.QueryString["IsNonProduction"] != null)
        {
            inv_IssueDetails = Inv_IssueDetailManager.GetAllInv_IssueDetailsRootIssueByEmployeeID(int.Parse(ddlEmployee.SelectedValue));
        }
        else
        {
            inv_IssueDetails = Inv_IssueDetailManager.GetAllInv_IssueDetailsRootIssueByEmployeeIDnProductID(int.Parse(ddlEmployee.SelectedValue), int.Parse(ddlProduct.SelectedValue));
        }

        string IDs = "-";
        foreach (Inv_IssueDetail inv_IssueDetail in inv_IssueDetails)
        {
            if (!IDs.Contains("-" + inv_IssueDetail.ItemID + "-"))
            {
                IDs += inv_IssueDetail.ItemID + "-";
            }
        }

        if (IDs != "-")
        {
            //to remove the 1st and last - and then replace with ,
            IDs = IDs.Substring(1, IDs.Length-1);
            IDs = IDs.Substring(0, IDs.Length - 1);
            IDs = IDs.Replace("-",",");
        }

        

        List<Inv_Item> items = new List<Inv_Item>();
        if (IDs != "-")
        items = Inv_ItemManager.GetAllInv_ItemsByIDs(IDs);


        foreach (Inv_IssueDetail inv_IssueDetail in inv_IssueDetails)
        {
            foreach (Inv_Item item in items)
            {
                if (item.Inv_ItemID == inv_IssueDetail.ItemID)
                {

                    foreach (ListItem ddlListItem in ddlProduct.Items)
                    {
                        if (inv_IssueDetail.ProductID.ToString() == ddlListItem.Value)
                        {
                            //ListItem li_item = new ListItem(item.ItemName.ToString() + "(" + item.ItemCode.ToString() + ") for " + ddlListItem.Text + "{" + item.QualityValue.ToString() + " " + item.ExtraField1.ToString() + "}" + " [" + inv_IssueDetail.ExtraField1.ToString() + " " + item.ExtraField2.ToString() + "] Apx. Q: " + inv_IssueDetail.ApproximateQuantity.ToString(), inv_IssueDetail.Inv_IssueDetailID.ToString());
                            ListItem li_item = new ListItem(item.ItemName.ToString() + "(" + item.ItemCode.ToString() + ") IssueID=" + inv_IssueDetail.ExtraField5 + "("+inv_IssueDetail.AddedDate.ToString("dd MMM yyyy hh:mm tt")+") {" + item.QualityValue.ToString() + " " + item.ExtraField1.ToString() + "}" + " [" + inv_IssueDetail.ExtraField1.ToString() + " " + item.ExtraField2.ToString() + "] Apx. Q: " + inv_IssueDetail.ApproximateQuantity.ToString(), inv_IssueDetail.Inv_IssueDetailID.ToString());
                            ddlIssuedItem.Items.Add(li_item);
                            break; 
                        }
                    }

                    break;
                }
            }
        }
    }

    protected void txtConfigPerProductRequiredQuantity_TextChanged(object sender, EventArgs e)
    {
        txtApproximateQuantity.Text = (decimal.Parse(txtQuantity.Text) / decimal.Parse(txtConfigPerProductRequiredQuantity.Text)).ToString("0,0");
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {        
        loadPrevioulyIssedItem();
    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadPrevioulyIssedItem();
    }
    protected void ddlIssuedItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIssuedItem.SelectedValue != "0")
        {
            trConfigPerProductRequiredQuantity.Visible = false;
            trClaculateApproximateQuantity.Visible = false;
            trApproximateQuantity.Visible = false;
        }
        else
        {
            if (Request.QueryString["IsNonProduction"] != null)
            {
                trProduct.Visible = false;
                trApproximateQuantity.Visible = false;
                trClaculateApproximateQuantity.Visible = false;
                trConfigPerProductRequiredQuantity.Visible = false;
            }
            else
            {
                trConfigPerProductRequiredQuantity.Visible = true;
                trClaculateApproximateQuantity.Visible = true;
                trApproximateQuantity.Visible = true;
            }
        }
    }
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        if (Request.QueryString["IsNonProduction"] == null)
        {
            btnClaculateApproximateQuantity_Click(this, new EventArgs());
        }
    }
    protected void txtItemCode_TextChanged(object sender, EventArgs e)
    {
        loadItem();

        ListItem itemText = new ListItem();
        bool isFound = false;

        foreach (ListItem item in ddlItem.Items)
        {
            if (item.Text.Split('-')[0].Trim() == txtItemCode.Text)
            {
                isFound = true;
                itemText = item;
            }
        }

        if (isFound)
        {
            ddlItem.Items.Clear();
            ddlItem.Items.Add(itemText);
        }
    }
}
