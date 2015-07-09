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

public partial class AdminInv_QuantityUnitInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadRowStatus();
            if (Request.QueryString["inv_QuantityUnitID"] != null)
            {
                int inv_QuantityUnitID = Int32.Parse(Request.QueryString["inv_QuantityUnitID"]);
                if (inv_QuantityUnitID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_QuantityUnitData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_QuantityUnit inv_QuantityUnit = new Inv_QuantityUnit();

        inv_QuantityUnit.QuantityUnitName = txtQuantityUnitName.Text;
        inv_QuantityUnit.RowStatusID = 1;
        int resutl = Inv_QuantityUnitManager.InsertInv_QuantityUnit(inv_QuantityUnit);
        Response.Redirect("AdminInv_QuantityUnitDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_QuantityUnit inv_QuantityUnit = new Inv_QuantityUnit();
        inv_QuantityUnit = Inv_QuantityUnitManager.GetInv_QuantityUnitByID(Int32.Parse(Request.QueryString["inv_QuantityUnitID"]));
        Inv_QuantityUnit tempInv_QuantityUnit = new Inv_QuantityUnit();
        tempInv_QuantityUnit.Inv_QuantityUnitID = inv_QuantityUnit.Inv_QuantityUnitID;

        tempInv_QuantityUnit.QuantityUnitName = txtQuantityUnitName.Text;
        tempInv_QuantityUnit.RowStatusID = 1;
        bool result = Inv_QuantityUnitManager.UpdateInv_QuantityUnit(tempInv_QuantityUnit);
        Response.Redirect("AdminInv_QuantityUnitDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtQuantityUnitName.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showInv_QuantityUnitData()
    {
        Inv_QuantityUnit inv_QuantityUnit = new Inv_QuantityUnit();
        inv_QuantityUnit = Inv_QuantityUnitManager.GetInv_QuantityUnitByID(Int32.Parse(Request.QueryString["inv_QuantityUnitID"]));

        txtQuantityUnitName.Text = inv_QuantityUnit.QuantityUnitName;
        ddlRowStatus.SelectedValue = inv_QuantityUnit.RowStatusID.ToString();
    }
    private void loadRowStatus()
    {
        ListItem li = new ListItem("Select RowStatus...", "0");
        ddlRowStatus.Items.Add(li);

        List<RowStatus> rowStatuss = new List<RowStatus>();
        rowStatuss = RowStatusManager.GetAllRowStatuss();
        foreach (RowStatus rowStatus in rowStatuss)
        {
            ListItem item = new ListItem(rowStatus.RowStatusName.ToString(), rowStatus.RowStatusID.ToString());
            ddlRowStatus.Items.Add(item);
        }
    }
}
