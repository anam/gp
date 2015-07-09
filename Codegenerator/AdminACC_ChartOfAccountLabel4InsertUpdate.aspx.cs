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

public partial class AdminACC_ChartOfAccountLabel4InsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadACC_HeadType();
            loadRowStatus();
            if (Request.QueryString["aCC_ChartOfAccountLabel4ID"] != null)
            {
                int aCC_ChartOfAccountLabel4ID = Int32.Parse(Request.QueryString["aCC_ChartOfAccountLabel4ID"]);
                if (aCC_ChartOfAccountLabel4ID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showACC_ChartOfAccountLabel4Data();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 = new ACC_ChartOfAccountLabel4();

        aCC_ChartOfAccountLabel4.Code = txtCode.Text;
        aCC_ChartOfAccountLabel4.ACC_HeadTypeID = Int32.Parse(ddlACC_HeadType.SelectedValue);
        aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text = txtChartOfAccountLabel4Text.Text;
        aCC_ChartOfAccountLabel4.ExtraField1 = txtExtraField1.Text;
        aCC_ChartOfAccountLabel4.ExtraField2 = txtExtraField2.Text;
        aCC_ChartOfAccountLabel4.ExtraField3 = txtExtraField3.Text;
        aCC_ChartOfAccountLabel4.AddedBy = Int32.Parse(txtAddedBy.Text);
        aCC_ChartOfAccountLabel4.AddedDate = DateTime.Now;
        aCC_ChartOfAccountLabel4.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        aCC_ChartOfAccountLabel4.UpdatedDate = DateTime.Now;
        aCC_ChartOfAccountLabel4.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = ACC_ChartOfAccountLabel4Manager.InsertACC_ChartOfAccountLabel4(aCC_ChartOfAccountLabel4);
        Response.Redirect("AdminACC_ChartOfAccountLabel4Display.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 = new ACC_ChartOfAccountLabel4();
        aCC_ChartOfAccountLabel4 = ACC_ChartOfAccountLabel4Manager.GetACC_ChartOfAccountLabel4ByID(Int32.Parse(Request.QueryString["aCC_ChartOfAccountLabel4ID"]));
        ACC_ChartOfAccountLabel4 tempACC_ChartOfAccountLabel4 = new ACC_ChartOfAccountLabel4();
        tempACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID = aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID;

        tempACC_ChartOfAccountLabel4.Code = txtCode.Text;
        tempACC_ChartOfAccountLabel4.ACC_HeadTypeID = Int32.Parse(ddlACC_HeadType.SelectedValue);
        tempACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text = txtChartOfAccountLabel4Text.Text;
        tempACC_ChartOfAccountLabel4.ExtraField1 = txtExtraField1.Text;
        tempACC_ChartOfAccountLabel4.ExtraField2 = txtExtraField2.Text;
        tempACC_ChartOfAccountLabel4.ExtraField3 = txtExtraField3.Text;
        tempACC_ChartOfAccountLabel4.AddedBy = Int32.Parse(txtAddedBy.Text);
        tempACC_ChartOfAccountLabel4.AddedDate = DateTime.Now;
        tempACC_ChartOfAccountLabel4.UpdatedBy = Int32.Parse(txtUpdatedBy.Text);
        tempACC_ChartOfAccountLabel4.UpdatedDate = DateTime.Now;
        tempACC_ChartOfAccountLabel4.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = ACC_ChartOfAccountLabel4Manager.UpdateACC_ChartOfAccountLabel4(tempACC_ChartOfAccountLabel4);
        Response.Redirect("AdminACC_ChartOfAccountLabel4Display.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtCode.Text = "";
        ddlACC_HeadType.SelectedIndex = 0;
        txtChartOfAccountLabel4Text.Text = "";
        txtExtraField1.Text = "";
        txtExtraField2.Text = "";
        txtExtraField3.Text = "";
        txtAddedBy.Text = "";
        txtUpdatedBy.Text = "";
        txtUpdatedDate.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showACC_ChartOfAccountLabel4Data()
    {
        ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 = new ACC_ChartOfAccountLabel4();
        aCC_ChartOfAccountLabel4 = ACC_ChartOfAccountLabel4Manager.GetACC_ChartOfAccountLabel4ByID(Int32.Parse(Request.QueryString["aCC_ChartOfAccountLabel4ID"]));

        txtCode.Text = aCC_ChartOfAccountLabel4.Code;
        ddlACC_HeadType.SelectedValue = aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString();
        txtChartOfAccountLabel4Text.Text = aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text;
        txtExtraField1.Text = aCC_ChartOfAccountLabel4.ExtraField1;
        txtExtraField2.Text = aCC_ChartOfAccountLabel4.ExtraField2;
        txtExtraField3.Text = aCC_ChartOfAccountLabel4.ExtraField3;
        txtAddedBy.Text = aCC_ChartOfAccountLabel4.AddedBy.ToString();
        txtUpdatedBy.Text = aCC_ChartOfAccountLabel4.UpdatedBy.ToString();
        txtUpdatedDate.Text = aCC_ChartOfAccountLabel4.UpdatedDate;
        ddlRowStatus.SelectedValue = aCC_ChartOfAccountLabel4.RowStatusID.ToString();
    }
    private void loadACC_HeadType()
    {
        ListItem li = new ListItem("Select ACC_HeadType...", "0");
        ddlACC_HeadType.Items.Add(li);

        List<ACC_HeadType> aCC_HeadTypes = new List<ACC_HeadType>();
        aCC_HeadTypes = ACC_HeadTypeManager.GetAllACC_HeadTypes();
        foreach (ACC_HeadType aCC_HeadType in aCC_HeadTypes)
        {
            ListItem item = new ListItem(aCC_HeadType.ACC_HeadTypeName.ToString(), aCC_HeadType.ACC_HeadTypeID.ToString());
            ddlACC_HeadType.Items.Add(item);
        }
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
