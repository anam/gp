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

public partial class AdminACC_ChartOfAccountLabel1InsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadRowStatus();
            if (Request.QueryString["aCC_ChartOfAccountLabel1ID"] != null)
            {
                int aCC_ChartOfAccountLabel1ID = Int32.Parse(Request.QueryString["aCC_ChartOfAccountLabel1ID"]);
                if (aCC_ChartOfAccountLabel1ID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showACC_ChartOfAccountLabel1Data();
                }
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ACC_ChartOfAccountLabel1 aCC_ChartOfAccountLabel1 = new ACC_ChartOfAccountLabel1();

        aCC_ChartOfAccountLabel1.Code = txtCode.Text;
        aCC_ChartOfAccountLabel1.ChartOfAccountLabel1Text = txtChartOfAccountLabel1Text.Text;
        aCC_ChartOfAccountLabel1.ExtraField1 = txtExtraField1.Text;
        aCC_ChartOfAccountLabel1.ExtraField2 = txtExtraField2.Text;
        aCC_ChartOfAccountLabel1.ExtraField3 = txtExtraField3.Text;
        aCC_ChartOfAccountLabel1.AddedBy = getLogin().LoginID;
        aCC_ChartOfAccountLabel1.AddedDate = DateTime.Now;
        aCC_ChartOfAccountLabel1.UpdatedBy = getLogin().LoginID;
        aCC_ChartOfAccountLabel1.UpdatedDate = DateTime.Now;
        aCC_ChartOfAccountLabel1.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = ACC_ChartOfAccountLabel1Manager.InsertACC_ChartOfAccountLabel1(aCC_ChartOfAccountLabel1);
        Response.Redirect("AdminACC_ChartOfAccountLabel1Display.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ACC_ChartOfAccountLabel1 aCC_ChartOfAccountLabel1 = new ACC_ChartOfAccountLabel1();
        aCC_ChartOfAccountLabel1 = ACC_ChartOfAccountLabel1Manager.GetACC_ChartOfAccountLabel1ByID(Int32.Parse(Request.QueryString["aCC_ChartOfAccountLabel1ID"]));
        ACC_ChartOfAccountLabel1 tempACC_ChartOfAccountLabel1 = new ACC_ChartOfAccountLabel1();
        tempACC_ChartOfAccountLabel1.ACC_ChartOfAccountLabel1ID = aCC_ChartOfAccountLabel1.ACC_ChartOfAccountLabel1ID;

        tempACC_ChartOfAccountLabel1.Code = txtCode.Text;
        tempACC_ChartOfAccountLabel1.ChartOfAccountLabel1Text = txtChartOfAccountLabel1Text.Text;
        tempACC_ChartOfAccountLabel1.ExtraField1 = txtExtraField1.Text;
        tempACC_ChartOfAccountLabel1.ExtraField2 = txtExtraField2.Text;
        tempACC_ChartOfAccountLabel1.ExtraField3 = txtExtraField3.Text;
        tempACC_ChartOfAccountLabel1.AddedBy = getLogin().LoginID;
        tempACC_ChartOfAccountLabel1.AddedDate = DateTime.Now;
        tempACC_ChartOfAccountLabel1.UpdatedBy = getLogin().LoginID;
        tempACC_ChartOfAccountLabel1.UpdatedDate = DateTime.Now;
        tempACC_ChartOfAccountLabel1.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = ACC_ChartOfAccountLabel1Manager.UpdateACC_ChartOfAccountLabel1(tempACC_ChartOfAccountLabel1);
        Response.Redirect("AdminACC_ChartOfAccountLabel1Display.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtCode.Text = "";
        txtChartOfAccountLabel1Text.Text = "";
        txtExtraField1.Text = "";
        txtExtraField2.Text = "";
        txtExtraField3.Text = "";
        txtAddedBy.Text = "";
        txtUpdatedBy.Text = "";
        txtUpdatedDate.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showACC_ChartOfAccountLabel1Data()
    {
        ACC_ChartOfAccountLabel1 aCC_ChartOfAccountLabel1 = new ACC_ChartOfAccountLabel1();
        aCC_ChartOfAccountLabel1 = ACC_ChartOfAccountLabel1Manager.GetACC_ChartOfAccountLabel1ByID(Int32.Parse(Request.QueryString["aCC_ChartOfAccountLabel1ID"]));

        txtCode.Text = aCC_ChartOfAccountLabel1.Code;
        txtChartOfAccountLabel1Text.Text = aCC_ChartOfAccountLabel1.ChartOfAccountLabel1Text;
        txtExtraField1.Text = aCC_ChartOfAccountLabel1.ExtraField1;
        txtExtraField2.Text = aCC_ChartOfAccountLabel1.ExtraField2;
        txtExtraField3.Text = aCC_ChartOfAccountLabel1.ExtraField3;
        txtAddedBy.Text = aCC_ChartOfAccountLabel1.AddedBy.ToString();
        txtUpdatedBy.Text = aCC_ChartOfAccountLabel1.UpdatedBy.ToString();
        //txtUpdatedDate.Text = aCC_ChartOfAccountLabel1.UpdatedDate;
        ddlRowStatus.SelectedValue = aCC_ChartOfAccountLabel1.RowStatusID.ToString();
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
