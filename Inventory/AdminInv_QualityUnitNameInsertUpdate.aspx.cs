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

public partial class AdminInv_QualityUnitNameInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadRowStatus();
            if (Request.QueryString["inv_QualityUnitNameID"] != null)
            {
                int inv_QualityUnitNameID = Int32.Parse(Request.QueryString["inv_QualityUnitNameID"]);
                if (inv_QualityUnitNameID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showInv_QualityUnitNameData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_QualityUnitName inv_QualityUnitName = new Inv_QualityUnitName();

        inv_QualityUnitName.QualityUnitName = txtQualityUnitName.Text;
        inv_QualityUnitName.RowStatusID = 1;
        int resutl = Inv_QualityUnitNameManager.InsertInv_QualityUnitName(inv_QualityUnitName);
        Response.Redirect("AdminInv_QualityUnitNameDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Inv_QualityUnitName inv_QualityUnitName = new Inv_QualityUnitName();
        inv_QualityUnitName = Inv_QualityUnitNameManager.GetInv_QualityUnitNameByID(Int32.Parse(Request.QueryString["inv_QualityUnitNameID"]));
        Inv_QualityUnitName tempInv_QualityUnitName = new Inv_QualityUnitName();
        tempInv_QualityUnitName.Inv_QualityUnitNameID = inv_QualityUnitName.Inv_QualityUnitNameID;

        tempInv_QualityUnitName.QualityUnitName = txtQualityUnitName.Text;
        tempInv_QualityUnitName.RowStatusID = 1;
        bool result = Inv_QualityUnitNameManager.UpdateInv_QualityUnitName(tempInv_QualityUnitName);
        Response.Redirect("AdminInv_QualityUnitNameDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtQualityUnitName.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showInv_QualityUnitNameData()
    {
        Inv_QualityUnitName inv_QualityUnitName = new Inv_QualityUnitName();
        inv_QualityUnitName = Inv_QualityUnitNameManager.GetInv_QualityUnitNameByID(Int32.Parse(Request.QueryString["inv_QualityUnitNameID"]));

        txtQualityUnitName.Text = inv_QualityUnitName.QualityUnitName;
        ddlRowStatus.SelectedValue = inv_QualityUnitName.RowStatusID.ToString();
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
