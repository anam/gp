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

public partial class AdminPos_ProductCostInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadPos_CostType();
            loadProduct();
            if (Request.QueryString["pos_ProductCostID"] != null)
            {
                int pos_ProductCostID = Int32.Parse(Request.QueryString["pos_ProductCostID"]);
                if (pos_ProductCostID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_ProductCostData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_ProductCost pos_ProductCost = new Pos_ProductCost();

        pos_ProductCost.Pos_CostTypeID = Int32.Parse(ddlPos_CostType.SelectedValue);
        pos_ProductCost.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        pos_ProductCost.Amount = Decimal.Parse(txtAmount.Text);
        pos_ProductCost.ExtraField1 = txtExtraField1.Text;
        pos_ProductCost.ExtraField2 = txtExtraField2.Text;
        pos_ProductCost.ExtraField3 = txtExtraField3.Text;
        int resutl = Pos_ProductCostManager.InsertPos_ProductCost(pos_ProductCost);
        Response.Redirect("AdminPos_ProductCostDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_ProductCost pos_ProductCost = new Pos_ProductCost();
        pos_ProductCost = Pos_ProductCostManager.GetPos_ProductCostByID(Int32.Parse(Request.QueryString["pos_ProductCostID"]));
        Pos_ProductCost tempPos_ProductCost = new Pos_ProductCost();
        tempPos_ProductCost.Pos_ProductCostID = pos_ProductCost.Pos_ProductCostID;

        tempPos_ProductCost.Pos_CostTypeID = Int32.Parse(ddlPos_CostType.SelectedValue);
        tempPos_ProductCost.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        tempPos_ProductCost.Amount = Decimal.Parse(txtAmount.Text);
        tempPos_ProductCost.ExtraField1 = txtExtraField1.Text;
        tempPos_ProductCost.ExtraField2 = txtExtraField2.Text;
        tempPos_ProductCost.ExtraField3 = txtExtraField3.Text;
        bool result = Pos_ProductCostManager.UpdatePos_ProductCost(tempPos_ProductCost);
        Response.Redirect("AdminPos_ProductCostDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlPos_CostType.SelectedIndex = 0;
        ddlProduct.SelectedIndex = 0;
        txtAmount.Text = "";
        txtExtraField1.Text = "";
        txtExtraField2.Text = "";
        txtExtraField3.Text = "";
    }
    private void showPos_ProductCostData()
    {
        Pos_ProductCost pos_ProductCost = new Pos_ProductCost();
        pos_ProductCost = Pos_ProductCostManager.GetPos_ProductCostByID(Int32.Parse(Request.QueryString["pos_ProductCostID"]));

        ddlPos_CostType.SelectedValue = pos_ProductCost.Pos_CostTypeID.ToString();
        ddlProduct.SelectedValue = pos_ProductCost.ProductID.ToString();
        txtAmount.Text = pos_ProductCost.Amount.ToString();
        txtExtraField1.Text = pos_ProductCost.ExtraField1;
        txtExtraField2.Text = pos_ProductCost.ExtraField2;
        txtExtraField3.Text = pos_ProductCost.ExtraField3;
    }
    private void loadPos_CostType()
    {
        ListItem li = new ListItem("Select CostType...", "0");
        ddlPos_CostType.Items.Add(li);

        List<Pos_CostType> pos_CostTypes = new List<Pos_CostType>();
        pos_CostTypes = Pos_CostTypeManager.GetAllPos_CostTypes();
        foreach (Pos_CostType pos_CostType in pos_CostTypes)
        {
            ListItem item = new ListItem(pos_CostType.CostTypeName.ToString(), pos_CostType.Pos_CostTypeID.ToString());
            ddlPos_CostType.Items.Add(item);
        }
    }
    private void loadProduct()
    {

        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlProduct.Items.Add(new ListItem("Select Product", "0"));

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 3)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlProduct.Items.Add(item);
            }
        }

    }
}
