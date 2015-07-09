using System;
using System.Collections;
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
using System.Collections.Generic;

public partial class AdminInv_IssueDetailDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadACC_ChartOfAccountLabel4();
            showInv_IssueDetailGrid();
        }
    }

    private void loadACC_ChartOfAccountLabel4()
    {

        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlSupplier.Items.Add(new ListItem("Select Supplier", "0"));

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID ==6)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlSupplier.Items.Add(item);
            }
        }


    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminInv_IssueDetailInsertUpdate.aspx?inv_IssueDetailID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("AdminInv_IssueDetailInsertUpdate.aspx?inv_IssueDetailID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = Inv_IssueDetailManager.DeleteInv_IssueDetail(Convert.ToInt32(linkButton.CommandArgument));
        showInv_IssueDetailGrid();
    }

    private void showInv_IssueDetailGrid()
    {
        Inv_Purchase purchase = Inv_PurchaseManager.GetInv_PurchaseByID(int.Parse(Request.QueryString["PurchaseID"]));
        txtParticulars.Text = purchase.Particulars;
        txtInvoiceNo.Text = purchase.InvoiceNo;
        txtPurchseDate.Text = purchase.PurchseDate.ToString("dd MMM yyyy");
        lblPurchaseID.Text = purchase.Inv_PurchaseID.ToString();
        ddlSupplier.SelectedValue = purchase.SuppierID.ToString();
        hfSupplier.Value = purchase.SuppierID.ToString();
        hfJournalMasterID.Value = purchase.ExtraField2;
    }
    protected void btnSaveIssueMaster_Click(object sender, EventArgs e)
    {
        string SQL = @"Update Inv_Purchase set PurchseDate='" + DateTime.Parse(txtPurchseDate.Text).ToString("yyyy-MM-dd") + @"',InvoiceNo='" + txtInvoiceNo.Text + "',Particulars='" + txtParticulars.Text + @"'
,SuppierID=" + ddlSupplier.SelectedValue

            + @" where Inv_PurchaseID=" + Request.QueryString["PurchaseID"];

        if (ddlSupplier.SelectedValue != hfSupplier.Value)
        {
            SQL += @"
    Update ACC_JournalDetail set ACC_ChartOfAccountLabel4ID="+ddlSupplier.SelectedValue+ @"
where JournalMasterID=" + hfJournalMasterID.Value + @" and  ACC_ChartOfAccountLabel4ID=" + hfSupplier.Value + @";
";
        }
        CommonManager.SQLExec(SQL);

        a_IssueMasterPreview.HRef = "PurchasePrint.aspx?PurchaseID=" + Request.QueryString["PurchaseID"];
        a_IssueMasterPreview.Visible = true;
    }
}
