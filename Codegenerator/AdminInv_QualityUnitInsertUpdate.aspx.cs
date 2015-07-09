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

public partial class AdminInv_QualityUnitInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadRowStatus();
            if (Request.QueryString["inv_QualityUnitID"] != null)
            {
                int inv_QualityUnitID = Int32.Parse(Request.QueryString["inv_QualityUnitID"]);
                if (inv_QualityUnitID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_QualityUnitData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_QualityUnit inv_QualityUnit = new Inv_QualityUnit();

        inv_QualityUnit.QualityUnitName = txtQualityUnitName.Text;
        inv_QualityUnit.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = Inv_QualityUnitManager.InsertInv_QualityUnit(inv_QualityUnit);
        Response.Redirect("AdminInv_QualityUnitDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_QualityUnit inv_QualityUnit = new Inv_QualityUnit();
        inv_QualityUnit = Inv_QualityUnitManager.GetInv_QualityUnitByID(Int32.Parse(Request.QueryString["inv_QualityUnitID"]));
        Inv_QualityUnit tempInv_QualityUnit = new Inv_QualityUnit();
        tempInv_QualityUnit.Inv_QualityUnitID = inv_QualityUnit.Inv_QualityUnitID;

        tempInv_QualityUnit.QualityUnitName = txtQualityUnitName.Text;
        tempInv_QualityUnit.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = Inv_QualityUnitManager.UpdateInv_QualityUnit(tempInv_QualityUnit);
        Response.Redirect("AdminInv_QualityUnitDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtQualityUnitName.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showInv_QualityUnitData()
    {
        Inv_QualityUnit inv_QualityUnit = new Inv_QualityUnit();
        inv_QualityUnit = Inv_QualityUnitManager.GetInv_QualityUnitByID(Int32.Parse(Request.QueryString["inv_QualityUnitID"]));

        txtQualityUnitName.Text = inv_QualityUnit.QualityUnitName;
        ddlRowStatus.SelectedValue = inv_QualityUnit.RowStatusID.ToString();
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
