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

public partial class AdminPos_BranchWiseProductStockCapacityInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadACC_ChartOfAccountLabel4();
            if (Request.QueryString["pos_BranchWiseProductStockCapacityID"] != null)
            {
                int pos_BranchWiseProductStockCapacityID = Int32.Parse(Request.QueryString["pos_BranchWiseProductStockCapacityID"]);
                if (pos_BranchWiseProductStockCapacityID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_BranchWiseProductStockCapacityData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_BranchWiseProductStockCapacity pos_BranchWiseProductStockCapacity = new Pos_BranchWiseProductStockCapacity();

        pos_BranchWiseProductStockCapacity.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        pos_BranchWiseProductStockCapacity.WorkStationID = Int32.Parse(ddlWorkStation.SelectedValue);
        pos_BranchWiseProductStockCapacity.StockAmount = Int64.Parse(txtStockAmount.Text);
        int resutl = Pos_BranchWiseProductStockCapacityManager.InsertPos_BranchWiseProductStockCapacity(pos_BranchWiseProductStockCapacity);
        Response.Redirect("AdminPos_BranchWiseProductStockCapacityDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_BranchWiseProductStockCapacity pos_BranchWiseProductStockCapacity = new Pos_BranchWiseProductStockCapacity();
        pos_BranchWiseProductStockCapacity = Pos_BranchWiseProductStockCapacityManager.GetPos_BranchWiseProductStockCapacityByID(Int32.Parse(Request.QueryString["pos_BranchWiseProductStockCapacityID"]));
        Pos_BranchWiseProductStockCapacity tempPos_BranchWiseProductStockCapacity = new Pos_BranchWiseProductStockCapacity();
        tempPos_BranchWiseProductStockCapacity.Pos_BranchWiseProductStockCapacityID = pos_BranchWiseProductStockCapacity.Pos_BranchWiseProductStockCapacityID;

        tempPos_BranchWiseProductStockCapacity.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        tempPos_BranchWiseProductStockCapacity.WorkStationID = Int32.Parse(ddlWorkStation.SelectedValue);
        tempPos_BranchWiseProductStockCapacity.StockAmount = Int64.Parse(txtStockAmount.Text);
        bool result = Pos_BranchWiseProductStockCapacityManager.UpdatePos_BranchWiseProductStockCapacity(tempPos_BranchWiseProductStockCapacity);
        Response.Redirect("AdminPos_BranchWiseProductStockCapacityDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlProduct.SelectedIndex = 0;
        ddlWorkStation.SelectedIndex = 0;
        txtStockAmount.Text = "";
    }
    private void showPos_BranchWiseProductStockCapacityData()
    {
        Pos_BranchWiseProductStockCapacity pos_BranchWiseProductStockCapacity = new Pos_BranchWiseProductStockCapacity();
        pos_BranchWiseProductStockCapacity = Pos_BranchWiseProductStockCapacityManager.GetPos_BranchWiseProductStockCapacityByID(Int32.Parse(Request.QueryString["pos_BranchWiseProductStockCapacityID"]));

        ddlProduct.SelectedValue = pos_BranchWiseProductStockCapacity.ProductID.ToString();
        ddlWorkStation.SelectedValue = pos_BranchWiseProductStockCapacity.WorkStationID.ToString();
        txtStockAmount.Text = pos_BranchWiseProductStockCapacity.StockAmount.ToString();
    }
    

    private void loadACC_ChartOfAccountLabel4()
    {
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlProduct.Items.Add(new ListItem("Any Product", "0"));
        ddlWorkStation.Items.Add(new ListItem("Select ShowRoom", "0"));
        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 3)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlProduct.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1 && aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.Contains("Room"))
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlWorkStation.Items.Add(item);
            }

        }
    }
   
}
