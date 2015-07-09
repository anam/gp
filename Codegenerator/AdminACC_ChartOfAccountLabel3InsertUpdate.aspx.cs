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

public partial class AdminACC_ChartOfAccountLabel3InsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadACC_ChartOfAccountLabel2();
            loadRowStatus();
            if (Request.QueryString["aCC_ChartOfAccountLabel3ID"] != null)
            {
                int aCC_ChartOfAccountLabel3ID = Int32.Parse(Request.QueryString["aCC_ChartOfAccountLabel3ID"]);
                if (aCC_ChartOfAccountLabel3ID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showACC_ChartOfAccountLabel3Data();
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
        ACC_ChartOfAccountLabel3 aCC_ChartOfAccountLabel3 = new ACC_ChartOfAccountLabel3();

        aCC_ChartOfAccountLabel3.Code = txtCode.Text;
        aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel2ID = Int32.Parse(ddlACC_ChartOfAccountLabel2.SelectedValue);
        aCC_ChartOfAccountLabel3.ChartOfAccountLabel3Text = txtChartOfAccountLabel3Text.Text;
        aCC_ChartOfAccountLabel3.ExtraField1 = txtExtraField1.Text;
        aCC_ChartOfAccountLabel3.ExtraField2 = txtExtraField2.Text;
        aCC_ChartOfAccountLabel3.ExtraField3 = txtExtraField3.Text;
        aCC_ChartOfAccountLabel3.AddedBy = getLogin().LoginID;
        aCC_ChartOfAccountLabel3.AddedDate = DateTime.Now;
        aCC_ChartOfAccountLabel3.UpdatedBy = getLogin().LoginID;
        aCC_ChartOfAccountLabel3.UpdatedDate = DateTime.Now;
        aCC_ChartOfAccountLabel3.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = ACC_ChartOfAccountLabel3Manager.InsertACC_ChartOfAccountLabel3(aCC_ChartOfAccountLabel3);
        Response.Redirect("AdminACC_ChartOfAccountLabel3Display.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ACC_ChartOfAccountLabel3 aCC_ChartOfAccountLabel3 = new ACC_ChartOfAccountLabel3();
        aCC_ChartOfAccountLabel3 = ACC_ChartOfAccountLabel3Manager.GetACC_ChartOfAccountLabel3ByID(Int32.Parse(Request.QueryString["aCC_ChartOfAccountLabel3ID"]));
        ACC_ChartOfAccountLabel3 tempACC_ChartOfAccountLabel3 = new ACC_ChartOfAccountLabel3();
        tempACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID = aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID;

        tempACC_ChartOfAccountLabel3.Code = txtCode.Text;
        tempACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel2ID = Int32.Parse(ddlACC_ChartOfAccountLabel2.SelectedValue);
        tempACC_ChartOfAccountLabel3.ChartOfAccountLabel3Text = txtChartOfAccountLabel3Text.Text;
        tempACC_ChartOfAccountLabel3.ExtraField1 = txtExtraField1.Text;
        tempACC_ChartOfAccountLabel3.ExtraField2 = txtExtraField2.Text;
        tempACC_ChartOfAccountLabel3.ExtraField3 = txtExtraField3.Text;
        tempACC_ChartOfAccountLabel3.AddedBy = getLogin().LoginID;
        tempACC_ChartOfAccountLabel3.AddedDate = DateTime.Now;
        tempACC_ChartOfAccountLabel3.UpdatedBy = getLogin().LoginID;
        tempACC_ChartOfAccountLabel3.UpdatedDate = DateTime.Now;
        tempACC_ChartOfAccountLabel3.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = ACC_ChartOfAccountLabel3Manager.UpdateACC_ChartOfAccountLabel3(tempACC_ChartOfAccountLabel3);
        Response.Redirect("AdminACC_ChartOfAccountLabel3Display.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtCode.Text = "";
        ddlACC_ChartOfAccountLabel2.SelectedIndex = 0;
        txtChartOfAccountLabel3Text.Text = "";
        txtExtraField1.Text = "";
        txtExtraField2.Text = "";
        txtExtraField3.Text = "";
        txtAddedBy.Text = "";
        txtUpdatedBy.Text = "";
        txtUpdatedDate.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showACC_ChartOfAccountLabel3Data()
    {
        ACC_ChartOfAccountLabel3 aCC_ChartOfAccountLabel3 = new ACC_ChartOfAccountLabel3();
        aCC_ChartOfAccountLabel3 = ACC_ChartOfAccountLabel3Manager.GetACC_ChartOfAccountLabel3ByID(Int32.Parse(Request.QueryString["aCC_ChartOfAccountLabel3ID"]));

        txtCode.Text = aCC_ChartOfAccountLabel3.Code;
        ddlACC_ChartOfAccountLabel2.SelectedValue = aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel2ID.ToString();
        txtChartOfAccountLabel3Text.Text = aCC_ChartOfAccountLabel3.ChartOfAccountLabel3Text;
        txtExtraField1.Text = aCC_ChartOfAccountLabel3.ExtraField1;
        txtExtraField2.Text = aCC_ChartOfAccountLabel3.ExtraField2;
        txtExtraField3.Text = aCC_ChartOfAccountLabel3.ExtraField3;
        txtAddedBy.Text = aCC_ChartOfAccountLabel3.AddedBy.ToString();
        txtUpdatedBy.Text = aCC_ChartOfAccountLabel3.UpdatedBy.ToString();
        //txtUpdatedDate.Text = aCC_ChartOfAccountLabel3.UpdatedDate;
        ddlRowStatus.SelectedValue = aCC_ChartOfAccountLabel3.RowStatusID.ToString();
    }
    private void loadACC_ChartOfAccountLabel2()
    {
        ListItem li = new ListItem("Select ACC_ChartOfAccountLabel2...", "0");
        ddlACC_ChartOfAccountLabel2.Items.Add(li);

        List<ACC_ChartOfAccountLabel2> aCC_ChartOfAccountLabel2s = new List<ACC_ChartOfAccountLabel2>();
        aCC_ChartOfAccountLabel2s = ACC_ChartOfAccountLabel2Manager.GetAllACC_ChartOfAccountLabel2s();
        foreach (ACC_ChartOfAccountLabel2 aCC_ChartOfAccountLabel2 in aCC_ChartOfAccountLabel2s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel2.ChartOfAccountLabel2Text.ToString(), aCC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel2ID.ToString());
            ddlACC_ChartOfAccountLabel2.Items.Add(item);
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
