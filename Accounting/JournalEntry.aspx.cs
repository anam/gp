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

public partial class AdminACC_JournalDetailInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadLoginInHiddenField();
            loadACC_ChartOfAccountLabel4();
            loadACC_ChartOfAccountLabel3();
            loadACC_HeadType();
            btnJournalSubmit.Visible = false;
            initailaData();
            loadForDiffCases();
        }
    }

    private void loadLoginInHiddenField()
    {
        if (hfLoginID.Value == "")
        {
            hfLoginID.Value = getLogin().LoginID.ToString();
        }
    }

    private Login getLogin()
    {
        Login login = new Login();

        try
        {
            if (Session["Login"] != null)
            {
                login = (Login)Session["Login"];
            }
            else if (hfLoginID.Value != "")
            {
                login = LoginManager.GetLoginByID(int.Parse(hfLoginID.Value));
            }
            else
            { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }

        }
        catch (Exception ex)
        { }

        return login;
    }

    private void initailaData()
    {
        txtJournalMasterDate.Text = DateTime.Now.ToString("dd MMM yyyy");
    }

    private void loadForDiffCases()
    {
        int type=0;

        if (Request.QueryString["Type"] != null)
        { 
            type=int.Parse(Request.QueryString["Type"]);
        }

        switch (type)
        {
            case 1:
                //Receipt Voucher
                lblReceivedOrPayto.Text = "Received From:";
                trDrCr.Visible = false;
                
                break;

            case 2:
                //Payment Voucher
                lblReceivedOrPayto.Text = "Pay to:";
                trDrCr.Visible = false;
                
                if (Request.QueryString["Tmp"] != null)
                {
                    rbtnRootAccounts.Items[0].Enabled = false;
                    rbtnRootAccounts.Items[2].Enabled = false;
                    rbtnRootAccounts.Items[3].Enabled = false;

                    rbtnRootAccounts.Items[1].Selected = true;
                    rbtnRootAccounts_SelectedIndexChanged(this,new EventArgs());
                }
                break;

            case 3:
                //Journal Voucher
                trAddress.Visible = false;
                trReceivedOrPayto.Visible = false;
                break;

            case 4:
                //Contra Voucher
                trAddress.Visible = false;
                trReceivedOrPayto.Visible = false;

                break;
            
            default:
                trAddress.Visible = false;
                trReceivedOrPayto.Visible = false;
                break;
        }
        
    }


   
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlACC_ChartOfAccountLabel3.SelectedValue != "0"
            && ((ddlACC_ChartOfAccountLabel4.Items.Count != 0 && ddlACC_ChartOfAccountLabel4.SelectedValue != "0")
                || (ddlACC_ChartOfAccountLabel4.Items.Count == 0))
            && (ddlWorkSatation.SelectedValue != "0" || rbtnHeadType.SelectedItem.Text == "Work Station")
            && txtAmount.Text != "" && decimal.Parse(txtAmount.Text) != 0
            && (Request.QueryString["Type"] == "1" || Request.QueryString["Type"] == "2" || rbtnDebitOrCredit.SelectedItem.Text == "Credit" || rbtnDebitOrCredit.SelectedItem.Text == "Debit")
            )
        {

            List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
            aCC_JournalDetails = loadFromGrid();

            ACC_JournalDetail aCC_JournalDetail = new ACC_JournalDetail();

            aCC_JournalDetail.JournalMasterID = aCC_JournalDetails.Count;

            try
            {
                aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = Int32.Parse(ddlACC_ChartOfAccountLabel4.SelectedValue);
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = ddlACC_ChartOfAccountLabel4.SelectedItem.Text;

            }
            catch (Exception ex)
            {
                aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = 0;
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = "N/A";
            }

            aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = ddlACC_ChartOfAccountLabel3.SelectedItem.Text;
            aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = Int32.Parse(ddlACC_ChartOfAccountLabel3.SelectedValue);
            
            try
            {
                if (ddlACC_ChartOfAccountLabel4.Items[0].Text.Contains("Work Station"))
                {
                    ddlWorkSatation.SelectedValue = aCC_JournalDetail.ACC_ChartOfAccountLabel4ID.ToString();
                }
               
                aCC_JournalDetail.WorkStation = int.Parse(ddlWorkSatation.SelectedValue);
                aCC_JournalDetail.WorkStationName = ddlWorkSatation.SelectedItem.Text;
                
            }
            catch (Exception ex)
            {
                aCC_JournalDetail.WorkStation = 0;
                aCC_JournalDetail.WorkStationName = "N/A";
            }
            if (Request.QueryString["Type"] == "1")//Reciept Voucher
            {
                if (rbtnRootAccounts.SelectedItem.Text == "Asset")
                {
                    aCC_JournalDetail.Debit = Decimal.Parse(txtAmount.Text);
                    aCC_JournalDetail.Credit = Decimal.Parse("0");
                }
                else
                {
                    aCC_JournalDetail.Debit = Decimal.Parse("0");
                    aCC_JournalDetail.Credit = Decimal.Parse(txtAmount.Text);
                }
            }
            else
                if (Request.QueryString["Type"] == "2")//Payment Voucher
                {
                    if (rbtnRootAccounts.SelectedItem.Text != "Asset")
                    {
                        aCC_JournalDetail.Debit = Decimal.Parse(txtAmount.Text);
                        aCC_JournalDetail.Credit = Decimal.Parse("0");
                    }
                    else
                    {
                        aCC_JournalDetail.Debit = Decimal.Parse("0");
                        aCC_JournalDetail.Credit = Decimal.Parse(txtAmount.Text);
                    }
                }
                else
                {

                    if (rbtnDebitOrCredit.SelectedItem.Text == "Credit")
                    {
                        aCC_JournalDetail.Debit = Decimal.Parse("0");
                        aCC_JournalDetail.Credit = Decimal.Parse(txtAmount.Text);
                    }
                    else
                    {
                        aCC_JournalDetail.Debit = Decimal.Parse(txtAmount.Text);
                        aCC_JournalDetail.Credit = Decimal.Parse("0");
                    }
                }

            aCC_JournalDetail.ExtraField3 = txtChequeNo.Text;
            aCC_JournalDetail.ExtraField2 = txtBankNBranchDetails.Text;
            aCC_JournalDetail.ExtraField1 = txtChequeDate.Text;
            aCC_JournalDetail.AddedBy = getLogin().LoginID;
            aCC_JournalDetail.AddedDate = DateTime.Now;
            aCC_JournalDetail.UpdatedBy = getLogin().LoginID;
            aCC_JournalDetail.UpdatedDate = DateTime.Now;
            aCC_JournalDetail.RowStatusID = 1;
            aCC_JournalDetails.Add(aCC_JournalDetail);

            if (Request.QueryString["Tmp"] != null)
            {

                aCC_JournalDetail = new ACC_JournalDetail();

                try
                {
                    aCC_JournalDetail.WorkStation = int.Parse(ddlWorkSatation.SelectedValue);
                    aCC_JournalDetail.WorkStationName = ddlWorkSatation.SelectedItem.Text;

                }
                catch (Exception ex)
                {
                    aCC_JournalDetail.WorkStation = 0;
                    aCC_JournalDetail.WorkStationName = "N/A";
                }
                
                aCC_JournalDetail.ExtraField3 = txtChequeNo.Text;
                aCC_JournalDetail.ExtraField2 = txtBankNBranchDetails.Text;
                aCC_JournalDetail.ExtraField1 = txtChequeDate.Text;
                aCC_JournalDetail.AddedBy = getLogin().LoginID;
                aCC_JournalDetail.AddedDate = DateTime.Now;
                aCC_JournalDetail.UpdatedBy = getLogin().LoginID;
                aCC_JournalDetail.UpdatedDate = DateTime.Now;
                aCC_JournalDetail.RowStatusID = 1;


                aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = int.Parse(ddlWorkSatation.SelectedValue);
                aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = ddlWorkSatation.SelectedItem.Text;

                aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 1;
                aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = "Petty Cash in Hand";

                aCC_JournalDetail.Debit = 0;
                aCC_JournalDetail.Credit =Decimal.Parse(txtAmount.Text);
                aCC_JournalDetails.Add(aCC_JournalDetail);
            }
            BindJournalGrid(aCC_JournalDetails);

            cleanData();

            trWorkStation.Visible = false;
        }
        else
        {
            showAlartMessage("Please check the Data carefully");            
        }
    }

    private void showAlartMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "err_msg",
             "alert('" + message + "');",
             true);
    }

    private void cleanData()
    {
        txtAmount.Text = "0";
        txtChequeDate.Text = "";
        txtChequeNo.Text = "";
        txtBankNBranchDetails.Text = "";
    }

  
    private List<ACC_JournalDetail> loadFromGrid()
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        int i = 0;

        foreach (GridViewRow gvr in gvACC_JournalDetail.Rows)
        {
            ACC_JournalDetail aCC_JournalDetail = new ACC_JournalDetail();

            aCC_JournalDetail.ACC_JournalDetailID = int.Parse(((HiddenField)gvr.FindControl("hfACC_JournalDetailID")).Value);

            aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = ((Label)gvr.FindControl("lblACC_ChartOfAccountLabel4ID")).Text;
            aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = Int32.Parse(((HiddenField)gvr.FindControl("hfACC_ChartOfAccountLabel4ID")).Value);

            aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = ((Label)gvr.FindControl("lblACC_ChartOfAccountLabel3ID")).Text;
            aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = Int32.Parse(((HiddenField)gvr.FindControl("hfACC_ChartOfAccountLabel3ID")).Value);


            aCC_JournalDetail.WorkStation = Int32.Parse(((HiddenField)gvr.FindControl("hfWorkStation")).Value);
            aCC_JournalDetail.WorkStationName = ((Label)gvr.FindControl("lblWorkStation")).Text; ;
            aCC_JournalDetail.Debit = Decimal.Parse(((Label)gvr.FindControl("lblDebit")).Text);
            aCC_JournalDetail.Credit = Decimal.Parse(((Label)gvr.FindControl("lblCredit")).Text);
            aCC_JournalDetail.ExtraField3 = "";
            aCC_JournalDetail.ExtraField2 = "";
            aCC_JournalDetail.ExtraField1 = ((HiddenField)gvr.FindControl("hfExtraField1")).Value;
            aCC_JournalDetail.ExtraField2 = ((HiddenField)gvr.FindControl("hfExtraField2")).Value;
            aCC_JournalDetail.ExtraField3 = ((HiddenField)gvr.FindControl("hfExtraField3")).Value;
            aCC_JournalDetail.AddedBy = getLogin().LoginID;
            aCC_JournalDetail.AddedDate = DateTime.Now;
            aCC_JournalDetail.UpdatedBy = getLogin().LoginID;
            aCC_JournalDetail.UpdatedDate = DateTime.Now;
            aCC_JournalDetail.RowStatusID = 1;

            aCC_JournalDetails.Add(aCC_JournalDetail);
        }

        return aCC_JournalDetails;
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = ACC_JournalDetailManager.DeleteACC_JournalDetail(Convert.ToInt32(linkButton.CommandArgument));

        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        aCC_JournalDetails = loadFromGrid();
        ACC_JournalDetail aCC_JournalDetailToDelete = new ACC_JournalDetail();
        foreach (ACC_JournalDetail item in aCC_JournalDetails)
        {
            if (item.ACC_JournalDetailID == Convert.ToInt32(linkButton.CommandArgument))
            {
                aCC_JournalDetailToDelete = item;
            }
        }
        aCC_JournalDetails.Remove(aCC_JournalDetailToDelete);
        int i =0;
        foreach (ACC_JournalDetail item in aCC_JournalDetails)
        {
            item.ACC_JournalDetailID = i++;
        }

        BindJournalGrid(aCC_JournalDetails);
    }

    private void BindJournalGrid(List<ACC_JournalDetail> aCC_JournalDetails)
    {
        decimal totalDebit = 0;
        decimal totalCredit = 0;

        foreach (ACC_JournalDetail item in aCC_JournalDetails)
        {
            totalDebit += item.Debit;
            totalCredit += item.Credit;
        }

        lbltotal.Text = "Dr= "+totalDebit.ToString("0.00")+"<br/>Cr= "+totalCredit.ToString("0.00");

        if (totalCredit == totalDebit && totalCredit!=0)
        {
            btnJournalSubmit.Visible = true;
        }
        else
        {
            btnJournalSubmit.Visible = false;
            lbltotal.Text += "<br/><b style='color:Red;'>Your Dr & Cr not Equal. Please complete the Journal Entry.</b>";
        }

        gvACC_JournalDetail.DataSource = aCC_JournalDetails;
        gvACC_JournalDetail.DataBind();
    }

    private void loadACC_ChartOfAccountLabel3()
    {   
        List<ACC_ChartOfAccountLabel3> aCC_ChartOfAccountLabel3s = new List<ACC_ChartOfAccountLabel3>();
        aCC_ChartOfAccountLabel3s = ACC_ChartOfAccountLabel3Manager.GetAllACC_ChartOfAccountLabel3sForJournalEntryForDropDownList();
        foreach (ACC_ChartOfAccountLabel3 aCC_ChartOfAccountLabel3 in aCC_ChartOfAccountLabel3s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel3.ChartOfAccountLabel3Text.ToString(),aCC_ChartOfAccountLabel3.ExtraField1.ToString()+"@"+ aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID.ToString());
            ddlAllACC_ChartOfAccountLabel3.Items.Add(item);
        }
    }

    private void loadACC_ChartOfAccountLabel4()
    {
        
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlWorkSatation.Items.Add(new ListItem("Select WorkStation","0"));

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString()+"@"+aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
            ddlAllACC_ChartOfAccountLabel4.Items.Add(item);

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                   ddlWorkSatation.Items.Add(item);
            }
        }

        
    }

    private void loadACC_HeadType()
    {

        List<ACC_HeadType> aCC_HeadTypes = new List<ACC_HeadType>();
        aCC_HeadTypes = ACC_HeadTypeManager.GetAllACC_HeadTypes();
        foreach (ACC_HeadType aCC_HeadType in aCC_HeadTypes)
        {
            ListItem item = new ListItem(aCC_HeadType.HeadTypeName.ToString(), aCC_HeadType.ACC_HeadTypeID.ToString());
            rbtnHeadType.Items.Add(item);
        }

        rbtnHeadType.Items.Add(new ListItem("N/A","0"));
        rbtnHeadType.Items[rbtnHeadType.Items.Count-1].Selected = true;
    }

    protected void rbtnRootAccounts_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlACC_ChartOfAccountLabel3.Items.Clear();
        ddlACC_ChartOfAccountLabel3.Items.Add(new ListItem("Select Account", "0"));
        foreach (ListItem item in ddlAllACC_ChartOfAccountLabel3.Items)
        {

            if (item.Value.Split('@')[0] == rbtnRootAccounts.SelectedItem.Text)
            {
                ddlACC_ChartOfAccountLabel3.Items.Add(new ListItem(item.Text, item.Value.Split('@')[1]));
            }
        }
    }

    protected void rbtnHeadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlACC_ChartOfAccountLabel4.Items.Clear();
        if (rbtnHeadType.SelectedItem.Text != "N/A")
        {
            ddlACC_ChartOfAccountLabel4.Items.Add(new ListItem("Select " + rbtnHeadType.SelectedItem.Text, "0"));
            if (rbtnHeadType.SelectedItem.Text == "Work Station")
            {
                trWorkStation.Visible = false;
            }
            else
            {
                trWorkStation.Visible = true;
            }
        }
        else
        {
            trWorkStation.Visible = true;
        }

        foreach (ListItem item in ddlAllACC_ChartOfAccountLabel4.Items)
        {

            if (item.Value.Split('@')[0] == rbtnHeadType.SelectedValue)
            {
                ddlACC_ChartOfAccountLabel4.Items.Add(new ListItem(item.Text,item.Value.Split('@')[1]));
            }
        }


    }


    protected void btnJournalSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ACC_JournalMaster aCC_JournalMaster = new ACC_JournalMaster();

            aCC_JournalMaster.JournalMasterName = Request.QueryString["Type"] != null ? Request.QueryString["Type"] : "0";//Voucher Type
            aCC_JournalMaster.ExtraField1 = txtReceivedOrPayto.Text;
            aCC_JournalMaster.ExtraField2 = txtAddress.Text;
            aCC_JournalMaster.ExtraField3 = "";
            aCC_JournalMaster.Note = txtNote.Text;
            aCC_JournalMaster.JournalDate = DateTime.Parse(txtJournalMasterDate.Text);
            aCC_JournalMaster.AddedBy = getLogin().LoginID;
            aCC_JournalMaster.AddedDate = DateTime.Now;
            aCC_JournalMaster.UpdatedBy = getLogin().LoginID;
            aCC_JournalMaster.UpdatedDate = DateTime.Now;
            aCC_JournalMaster.RowStatusID = 1;
            int JournalMasterID = 0;
            if (Request.QueryString["Tmp"] != null)
            {
                JournalMasterID = ACC_JournalMasterManager.InsertACC_JournalMasterTmp(aCC_JournalMaster);
            }
            else
            {
                JournalMasterID = ACC_JournalMasterManager.InsertACC_JournalMaster(aCC_JournalMaster);
            }
            List<ACC_JournalDetail> aCC_Journaldetails = loadFromGrid();

            foreach (ACC_JournalDetail item in aCC_Journaldetails)
            {
                item.JournalMasterID = JournalMasterID;
                if (Request.QueryString["Tmp"] != null)
                {
                    ACC_JournalDetailManager.InsertACC_JournalDetailTmp(item);
                    hlnkPrintVoucher.Visible = false;
                }
                else
                {
                    ACC_JournalDetailManager.InsertACC_JournalDetail(item);
                    hlnkPrintVoucher.Visible = true;
                }
            }

            aCC_Journaldetails= new List<ACC_JournalDetail>();

            BindJournalGrid(aCC_Journaldetails);

            hlnkPrintVoucher.NavigateUrl = "Voucherprint.aspx?JournalMasterID=" + JournalMasterID;
            

            showAlartMessage("Successfully Done.");
            cleanDataJournalMaster();
        }
        catch (Exception ex)
        {
            showAlartMessage("Error occured");
        }
    }

    private void cleanDataJournalMaster()
    {
        txtReceivedOrPayto.Text = "";
        txtAddress.Text = "";
        txtNote.Text = "";
        lbltotal.Text = "";
    }
}
