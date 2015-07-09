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
            loadACC_ChartOfAccountLabel4();
            loadACC_ChartOfAccountLabel1();
            loadACC_ChartOfAccountLabel2();
            loadACC_ChartOfAccountLabel3();
            loadACC_HeadType();
            initailaData();
        }
    }

    private void initailaData()
    {
        txtFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        txtTrialBanaceDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        txtTrialBanaceFromDate.Text = DateTime.Now.ToString("01 Jan yyyy");
        txtFromDateTransactionSummary.Text = DateTime.Now.ToString("dd MMM yyyy");
        txtShowroomSummaryToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        txtShowroomSummaryFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        txtToDateTransactionSummary.Text = DateTime.Now.ToString("dd MMM yyyy");
        rbtnRootAccounts.SelectedValue = "0";
        rbtnRootAccounts_SelectedIndexChanged(this,new EventArgs());
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
    protected void btnShowLedger_Click(object sender, EventArgs e)
    {
        string ACC_ChartOfAccountLabel4ID = "";
        try
        {
            if (ddlACC_ChartOfAccountLabel4.SelectedValue == "0" && ddlACC_ChartOfAccountLabel4.Items.Count > 1)
            {
                foreach (ListItem item in ddlACC_ChartOfAccountLabel4.Items)
                {
                    ACC_ChartOfAccountLabel4ID += (ACC_ChartOfAccountLabel4ID != "" ? "," : "") + item.Value;
                }   
            }
            else if (ddlACC_ChartOfAccountLabel4.SelectedValue != "0" && ddlACC_ChartOfAccountLabel4.Items.Count > 1)
            {
                ACC_ChartOfAccountLabel4ID = ddlACC_ChartOfAccountLabel4.SelectedValue;
            }

        }
        catch (Exception ex)
        { }

        string ACC_ChartOfAccountLabel3ID = "";
        try
        {
            if (ddlACC_ChartOfAccountLabel3.SelectedValue == "0" && ddlACC_ChartOfAccountLabel3.Items.Count > 1)
            {
                foreach (ListItem item in ddlACC_ChartOfAccountLabel3.Items)
                {
                    ACC_ChartOfAccountLabel3ID += (ACC_ChartOfAccountLabel3ID != "" ? "," : "") + item.Value;
                }
            }
            else if (ddlACC_ChartOfAccountLabel3.SelectedValue != "0" && ddlACC_ChartOfAccountLabel3.Items.Count > 1)
            {
                ACC_ChartOfAccountLabel3ID = ddlACC_ChartOfAccountLabel3.SelectedValue;
            }

        }
        catch (Exception ex)
        { }


        string ACC_ChartOfAccountLabel2ID = "";
        try
        {
            if (ddlACC_ChartOfAccountLabel2.SelectedValue == "0" && ddlACC_ChartOfAccountLabel2.Items.Count > 1)
            {
                foreach (ListItem item in ddlACC_ChartOfAccountLabel2.Items)
                {
                    ACC_ChartOfAccountLabel2ID += (ACC_ChartOfAccountLabel2ID != "" ? "," : "") + item.Value;
                }
            }
            else if (ddlACC_ChartOfAccountLabel2.SelectedValue != "0" && ddlACC_ChartOfAccountLabel2.Items.Count > 1)
            {
                ACC_ChartOfAccountLabel2ID = ddlACC_ChartOfAccountLabel2.SelectedValue;
            }

        }
        catch (Exception ex)
        { }



        string ACC_ChartOfAccountLabel1ID = "";
        try
        {
            if (ddlACC_ChartOfAccountLabel1.SelectedValue == "0" && ddlACC_ChartOfAccountLabel1.Items.Count > 1)
            {
                foreach (ListItem item in ddlACC_ChartOfAccountLabel1.Items)
                {
                    ACC_ChartOfAccountLabel1ID += (ACC_ChartOfAccountLabel1ID != "" ? "," : "") + item.Value;
                }
            }
            else if (ddlACC_ChartOfAccountLabel1.SelectedValue != "0" && ddlACC_ChartOfAccountLabel1.Items.Count > 1)
            {
                ACC_ChartOfAccountLabel1ID = ddlACC_ChartOfAccountLabel1.SelectedValue;
            }

        }
        catch (Exception ex)
        { }


        int WorkStationID = 0;
        try
        {
            WorkStationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        }
        catch (Exception ex)
        {
        }

        string FromDate = DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + " 12:00:00 AM";
        string ToDate = DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59:59 PM";

        //hlnkPrintGeneralLedger.NavigateUrl = "GeneralLedgerPrint.aspx?ACC_ChartOfAccountLabel4ID=" + ACC_ChartOfAccountLabel4ID
        //    + "&ACC_ChartOfAccountLabel1ID=" + ACC_ChartOfAccountLabel1ID
        //    + "&ACC_ChartOfAccountLabel2ID=" + ACC_ChartOfAccountLabel2ID
        //    + "&ACC_ChartOfAccountLabel3ID=" + ACC_ChartOfAccountLabel3ID
        //    + "&WorkStationID=" + WorkStationID
        //    + "&FromDate=" + FromDate
        //    + "&ToDate=" + ToDate;

        hlnkPrintGeneralLedger.NavigateUrl = "TransactionSearchPrint.aspx?L4=" + ACC_ChartOfAccountLabel4ID
            + "&L1=" + ACC_ChartOfAccountLabel1ID
            + "&L2=" + ACC_ChartOfAccountLabel2ID
            + "&L3=" + ACC_ChartOfAccountLabel3ID
            + "&JournalMasterName=" + getJournalType()
            + "&WorkStationID=" + WorkStationID
            + "&FromDate=" + FromDate
            + "&Date=" + ToDate;
        hlnkPrintGeneralLedger.Visible = true;
    }


    protected void btnSummary_Click(object sender, EventArgs e)
    {
        string ACC_ChartOfAccountLabel4ID = "";
        try
        {
            if (ddlACC_ChartOfAccountLabel4.SelectedValue == "0" && ddlACC_ChartOfAccountLabel4.Items.Count > 1)
            {
                foreach (ListItem item in ddlACC_ChartOfAccountLabel4.Items)
                {
                    ACC_ChartOfAccountLabel4ID += (ACC_ChartOfAccountLabel4ID != "" ? "," : "") + item.Value;
                }
            }
            else if (ddlACC_ChartOfAccountLabel4.SelectedValue != "0" && ddlACC_ChartOfAccountLabel4.Items.Count > 1)
            {
                ACC_ChartOfAccountLabel4ID = ddlACC_ChartOfAccountLabel4.SelectedValue;
            }

        }
        catch (Exception ex)
        { }

        string ACC_ChartOfAccountLabel3ID = "";
        try
        {
            if (ddlACC_ChartOfAccountLabel3.SelectedValue == "0" && ddlACC_ChartOfAccountLabel3.Items.Count > 1)
            {
                foreach (ListItem item in ddlACC_ChartOfAccountLabel3.Items)
                {
                    ACC_ChartOfAccountLabel3ID += (ACC_ChartOfAccountLabel3ID != "" ? "," : "") + item.Value;
                }
            }
            else if (ddlACC_ChartOfAccountLabel3.SelectedValue != "0" && ddlACC_ChartOfAccountLabel3.Items.Count > 1)
            {
                ACC_ChartOfAccountLabel3ID = ddlACC_ChartOfAccountLabel3.SelectedValue;
            }

        }
        catch (Exception ex)
        { }


        string ACC_ChartOfAccountLabel2ID = "";
        try
        {
            if (ddlACC_ChartOfAccountLabel2.SelectedValue == "0" && ddlACC_ChartOfAccountLabel2.Items.Count > 1)
            {
                foreach (ListItem item in ddlACC_ChartOfAccountLabel2.Items)
                {
                    ACC_ChartOfAccountLabel2ID += (ACC_ChartOfAccountLabel2ID != "" ? "," : "") + item.Value;
                }
            }
            else if (ddlACC_ChartOfAccountLabel2.SelectedValue != "0" && ddlACC_ChartOfAccountLabel2.Items.Count > 1)
            {
                ACC_ChartOfAccountLabel2ID = ddlACC_ChartOfAccountLabel2.SelectedValue;
            }

        }
        catch (Exception ex)
        { }



        string ACC_ChartOfAccountLabel1ID = "";
        try
        {
            if (ddlACC_ChartOfAccountLabel1.SelectedValue == "0" && ddlACC_ChartOfAccountLabel1.Items.Count > 1)
            {
                foreach (ListItem item in ddlACC_ChartOfAccountLabel1.Items)
                {
                    ACC_ChartOfAccountLabel1ID += (ACC_ChartOfAccountLabel1ID != "" ? "," : "") + item.Value;
                }
            }
            else if (ddlACC_ChartOfAccountLabel1.SelectedValue != "0" && ddlACC_ChartOfAccountLabel1.Items.Count > 1)
            {
                ACC_ChartOfAccountLabel1ID = ddlACC_ChartOfAccountLabel1.SelectedValue;
            }

        }
        catch (Exception ex)
        { }


        int WorkStationID = 0;
        try
        {
            WorkStationID = Int32.Parse(ddlWorkSatation.SelectedValue);
        }
        catch (Exception ex)
        {
        }

        string FromDate = DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + " 12:00:00 AM";
        string ToDate = DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59:59 PM";

        //hlnkPrintGeneralLedger.NavigateUrl = "GeneralLedgerPrint.aspx?ACC_ChartOfAccountLabel4ID=" + ACC_ChartOfAccountLabel4ID
        //    + "&ACC_ChartOfAccountLabel1ID=" + ACC_ChartOfAccountLabel1ID
        //    + "&ACC_ChartOfAccountLabel2ID=" + ACC_ChartOfAccountLabel2ID
        //    + "&ACC_ChartOfAccountLabel3ID=" + ACC_ChartOfAccountLabel3ID
        //    + "&WorkStationID=" + WorkStationID
        //    + "&FromDate=" + FromDate
        //    + "&ToDate=" + ToDate;

        hlnkSummary.NavigateUrl = "Summary.aspx?L4=" + ACC_ChartOfAccountLabel4ID
            + "&L1=" + ACC_ChartOfAccountLabel1ID
            + "&L2=" + ACC_ChartOfAccountLabel2ID
            + "&L3=" + ACC_ChartOfAccountLabel3ID
            + "&JournalMasterName=" + getJournalType()
            + "&WorkStationID=" + WorkStationID
            + "&FromDate=" + FromDate
            + "&Date=" + ToDate;
        hlnkSummary.Visible = true;
    }

    

    private string getJournalType()
    {
        string JournalType = "";

        if (chkJournalTypeRV.Checked) JournalType += (JournalType == "" ? "" : ",") + "'1'";
        if (chkJournalTypePV.Checked) JournalType += (JournalType == "" ? "" : ",") + "'2'";
        if (chkJournalTypeJV.Checked) JournalType += (JournalType == "" ? "" : ",") + "'3'";
        if (chkJournalTypeCV.Checked) JournalType += (JournalType == "" ? "" : ",") + "'4'";
        return JournalType;
    }
    private string getJournalTypeForShowRoom()
    {
        string JournalType = "";

        if (chkShowroomRV.Checked) JournalType += (JournalType == "" ? "" : ",") + "'1'";
        if (chkShowroomPV.Checked) JournalType += (JournalType == "" ? "" : ",") + "'2'";
        if (chkShowroomJV.Checked) JournalType += (JournalType == "" ? "" : ",") + "'3'";
        if (chkShowroomCV.Checked) JournalType += (JournalType == "" ? "" : ",") + "'4'";
        return JournalType;
    }

    protected void btnTrialBanalce_Click(object sender, EventArgs e)
    {
        hlnkTrialBanalce.NavigateUrl = "TrialBalancePrintL2.aspx?TrialBalance=1&FromDate="+txtTrialBanaceFromDate.Text+"&Date=" + txtTrialBanaceDate.Text;
        hlnkTrialBanalce.Visible = true;
    }


    protected void btnBalanceSheet_Click(object sender, EventArgs e)
    {
        hlnkBalanceSheet.NavigateUrl = "BalanceSheetPrint.aspx?FromDate=" + txtTrialBanaceFromDate.Text + "&Date=" + txtTrialBanaceDate.Text;
        hlnkBalanceSheet.Visible = true;
    }


    protected void btnIncomeStatement_Click(object sender, EventArgs e)
    {
        hlnkIncomeStatement.NavigateUrl = "IncomeStatementPrint.aspx?FromDate=" + txtTrialBanaceFromDate.Text + "&Date=" + txtTrialBanaceDate.Text;
        hlnkIncomeStatement.Visible = true;
    }
    protected void btnTransactionSummary_Click(object sender, EventArgs e)
    {
        hlnkTransactionSummary.NavigateUrl = "TrialBalancePrint.aspx?FromDate=" + txtFromDateTransactionSummary.Text + "&Date=" + txtToDateTransactionSummary.Text;
        hlnkTransactionSummary.Visible = true;
    }

    protected void btnShowroomSummary_Click(object sender, EventArgs e)
    {
        string FromDate = DateTime.Parse(txtShowroomSummaryFromDate.Text).ToString("yyyy-MM-dd") + " 12:00:00 AM";
        string ToDate = DateTime.Parse(txtShowroomSummaryToDate.Text).ToString("yyyy-MM-dd") + " 11:59:59 PM";
        string WorkStationID = "";
        if (ddlShowRoom.SelectedValue != "0")
        {
            WorkStationID += ddlShowRoom.SelectedValue ;
        }
        else
        {
            foreach (ListItem item in ddlShowRoom.Items)
            {
                if (item.Value != "0" && item.Value !="1")
                {
                    WorkStationID += (WorkStationID == "" ? "" : ",") + item.Value;
                }
            }
        }


        hlnkShowroomSummary.NavigateUrl = "ShowRoomWiseIncomeExpence.aspx?ShowRoomName="+ddlShowRoom.SelectedItem.Text+"&L1=0&L3=0&L4=0&WorkStationID= in (" +WorkStationID  + ")&FromDate=" + FromDate
            + "&JournalMasterName=" + getJournalTypeForShowRoom() + "&Date=" + ToDate;
        hlnkShowroomSummary.Visible = true;
    }

    protected void btnDateVoucherListByPostedDate_Click(object sender, EventArgs e)
    {

        string searchString = " where AddedDate >= '" + DateTime.Parse(txtFromDateTransactionSummary.Text).ToString("yyyy-MM-dd") + " 00:00:00' and  AddedDate <= '" + DateTime.Parse(txtToDateTransactionSummary.Text).AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00'  order by AddedDate desc";
        List<ACC_JournalMaster> journalMasters = ACC_JournalMasterManager.GetAllACC_JournalMastersByDateRange(searchString);

        string JournalMasterIDs = "";
        foreach (ACC_JournalMaster item in journalMasters)
        {
            JournalMasterIDs += item.ACC_JournalMasterID.ToString()+",";
        }

        hlnkDateVoucherListByPostedDate.NavigateUrl = "DateRangeVouchersPrint.aspx?JournalMasterIDs="+ JournalMasterIDs;
        hlnkDateVoucherListByPostedDate.Visible = true;
    }

    protected void btnDateVoucherListByVoucherDate_Click(object sender, EventArgs e)
    {

        string searchString = " where JournalDate >= '" + DateTime.Parse(txtFromDateTransactionSummary.Text).ToString("yyyy-MM-dd") + "' and  JournalDate <= '" + DateTime.Parse(txtToDateTransactionSummary.Text).ToString("yyyy-MM-dd") + "' order by JournalDate desc";
        List<ACC_JournalMaster> journalMasters = ACC_JournalMasterManager.GetAllACC_JournalMastersByDateRange(searchString);

        string JournalMasterIDs = "";
        foreach (ACC_JournalMaster item in journalMasters)
        {
            JournalMasterIDs += item.ACC_JournalMasterID.ToString() + ",";
        }

        hlnkDateVoucherListByVoucherDate.NavigateUrl = "DateRangeVouchersPrint.aspx?JournalMasterIDs=" + JournalMasterIDs;
        hlnkDateVoucherListByVoucherDate.Visible = true;
    }

    private void showAlartMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "err_msg",
             "alert('" + message + "');",
             true);
    }

    
    private void loadACC_ChartOfAccountLabel1()
    {   
        List<ACC_ChartOfAccountLabel1> aCC_ChartOfAccountLabel1s = new List<ACC_ChartOfAccountLabel1>();
        aCC_ChartOfAccountLabel1s = ACC_ChartOfAccountLabel1Manager.GetAllACC_ChartOfAccountLabel1s();
        foreach (ACC_ChartOfAccountLabel1 aCC_ChartOfAccountLabel1 in aCC_ChartOfAccountLabel1s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel1.ChartOfAccountLabel1Text.ToString(), aCC_ChartOfAccountLabel1.ExtraField1.ToString() + "@" + aCC_ChartOfAccountLabel1.ACC_ChartOfAccountLabel1ID.ToString());
            ddlAllACC_ChartOfAccountLabel1.Items.Add(item);
        }
    }



    private void loadACC_ChartOfAccountLabel2()
    {
        List<ACC_ChartOfAccountLabel2> aCC_ChartOfAccountLabel2s = new List<ACC_ChartOfAccountLabel2>();
        aCC_ChartOfAccountLabel2s = ACC_ChartOfAccountLabel2Manager.GetAllACC_ChartOfAccountLabel2s();
        foreach (ACC_ChartOfAccountLabel2 aCC_ChartOfAccountLabel2 in aCC_ChartOfAccountLabel2s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel2.ChartOfAccountLabel2Text.ToString(), aCC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel1ID.ToString() + "@" + aCC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel2ID.ToString());
            ddlAllACC_ChartOfAccountLabel2.Items.Add(item);
        }
    }



    private void loadACC_ChartOfAccountLabel3()
    {
        List<ACC_ChartOfAccountLabel3> aCC_ChartOfAccountLabel3s = new List<ACC_ChartOfAccountLabel3>();
        aCC_ChartOfAccountLabel3s = ACC_ChartOfAccountLabel3Manager.GetAllACC_ChartOfAccountLabel3sForJournalEntryForDropDownList();
        foreach (ACC_ChartOfAccountLabel3 aCC_ChartOfAccountLabel3 in aCC_ChartOfAccountLabel3s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel3.ChartOfAccountLabel3Text.ToString(), aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel2ID.ToString() + "@" + aCC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID.ToString());
            ddlAllACC_ChartOfAccountLabel3.Items.Add(item);
        }
    }

    private void loadACC_ChartOfAccountLabel4()
    {
        string sql = @"
SELECT ACC_ChartOfAccountLabel4.[ACC_ChartOfAccountLabel4ID]
      ,ACC_ChartOfAccountLabel4.[Code]
      ,ACC_ChartOfAccountLabel4.[ACC_HeadTypeID]
      ,ACC_HeadType.HeadTypeName +' -> '+ACC_ChartOfAccountLabel4.[ChartOfAccountLabel4Text] as ChartOfAccountLabel4Text
      ,ACC_ChartOfAccountLabel4.[ExtraField1]
      ,ACC_ChartOfAccountLabel4.[ExtraField2]
      ,ACC_ChartOfAccountLabel4.[ExtraField3]
      ,ACC_ChartOfAccountLabel4.[AddedBy]
      ,ACC_ChartOfAccountLabel4.[AddedDate]
      ,ACC_ChartOfAccountLabel4.[UpdatedBy]
      ,ACC_ChartOfAccountLabel4.[UpdatedDate]
      ,ACC_ChartOfAccountLabel4.[RowStatusID] 
        ,ACC_ChartOfAccountLabel4Visibility.IsVisible
FROM ACC_ChartOfAccountLabel4
    inner join ACC_HeadType on ACC_HeadType.ACC_HeadTypeID=ACC_ChartOfAccountLabel4.ACC_HeadTypeID
left outer join ACC_ChartOfAccountLabel4Visibility on ACC_ChartOfAccountLabel4Visibility.ACC_ChartOfAccountLabel4ID
=ACC_ChartOfAccountLabel4.[ACC_ChartOfAccountLabel4ID]
where ACC_ChartOfAccountLabel4.[RowStatusID] =1	
order by ACC_HeadType.ACC_HeadTypeID,ACC_ChartOfAccountLabel4.[ChartOfAccountLabel4Text]
";

       
        DataSet ds = CommonManager.SQLExec(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            try
            {
                dr["IsVisible"] = bool.Parse(dr["IsVisible"].ToString());
            }
            catch (Exception ex)
            {
                dr["IsVisible"] = false;
            }
        }
       

        //List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        //aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlWorkSatation.Items.Add(new ListItem("Select WorkStation", "0"));
        ddlShowRoom.Items.Add(new ListItem("All Show Room", "0"));
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (!bool.Parse(dr["IsVisible"].ToString()))
            {
                ListItem item = new ListItem(dr["ChartOfAccountLabel4Text"].ToString(), dr["ACC_HeadTypeID"].ToString() + "@" + dr["ACC_ChartOfAccountLabel4ID"].ToString());
                ddlAllACC_ChartOfAccountLabel4.Items.Add(item);

                if (dr["ACC_HeadTypeID"].ToString() == "1")
                {
                    item = new ListItem( dr["ChartOfAccountLabel4Text"].ToString(), dr["ACC_ChartOfAccountLabel4ID"].ToString());
                    ddlWorkSatation.Items.Add(item);
                }

                if ((dr["ACC_ChartOfAccountLabel4ID"].ToString() == "1" || dr["ChartOfAccountLabel4Text"].ToString().ToLower().Contains("show room")) &&
                    dr["ACC_HeadTypeID"].ToString() == "1")
                {
                    item = new ListItem(dr["ChartOfAccountLabel4Text"].ToString(), dr["ACC_ChartOfAccountLabel4ID"].ToString());
                    ddlShowRoom.Items.Add(item);
                }
            }
        }
        //foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        //{
        //    ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
        //    ddlAllACC_ChartOfAccountLabel4.Items.Add(item);

        //    if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1)
        //    {
        //        item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
        //        ddlWorkSatation.Items.Add(item);
        //    }

        //    if ((aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID==1 || aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToLower().Contains("show room")) &&
        //        aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1)
        //    {
        //        item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
        //        ddlShowRoom.Items.Add(item);
        //    }
        //}
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
        ddlACC_ChartOfAccountLabel1.Items.Clear();
        ddlACC_ChartOfAccountLabel1.Items.Add(new ListItem("Select Lable-1", "0"));
            
        if (rbtnRootAccounts.SelectedValue != "0")
        {
            foreach (ListItem item in ddlAllACC_ChartOfAccountLabel1.Items)
            {

                if (item.Value.Split('@')[0] == rbtnRootAccounts.SelectedItem.Text)
                {
                    ddlACC_ChartOfAccountLabel1.Items.Add(new ListItem(item.Text, item.Value.Split('@')[1]));
                }
            }            
        }
        ddlACC_ChartOfAccountLabel1_SelectedIndexChanged(this, new EventArgs());
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
                ddlACC_ChartOfAccountLabel4.Items.Add(new ListItem(item.Text, item.Value.Split('@')[1]));
            }
        }


    }


    protected void ddlACC_ChartOfAccountLabel1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlACC_ChartOfAccountLabel2.Items.Clear();
        ddlACC_ChartOfAccountLabel2.Items.Add(new ListItem("Select Label-2", "0"));
        foreach (ListItem item in ddlAllACC_ChartOfAccountLabel2.Items)
        {

            if (item.Value.Split('@')[0] == ddlACC_ChartOfAccountLabel1.SelectedValue)
            {
                ddlACC_ChartOfAccountLabel2.Items.Add(new ListItem(item.Text, item.Value.Split('@')[1]));
            }
        }

        ddlACC_ChartOfAccountLabel2_SelectedIndexChanged(this, new EventArgs());
    }
    protected void ddlACC_ChartOfAccountLabel2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlACC_ChartOfAccountLabel3.Items.Clear();
        ddlACC_ChartOfAccountLabel3.Items.Add(new ListItem("Select Label-3", "0"));
        foreach (ListItem item in ddlAllACC_ChartOfAccountLabel3.Items)
        {

            if (item.Value.Split('@')[0] == ddlACC_ChartOfAccountLabel2.SelectedValue)
            {
                ddlACC_ChartOfAccountLabel3.Items.Add(new ListItem(item.Text, item.Value.Split('@')[1]));
            }
        }

    }


    protected void btnBackup_Click(object sender, EventArgs e)
    {
        try
        {
            DeleteFileFromFolder("GentleParkHO_Old.rar");
        }
        catch (Exception ex)
        { }

        string sql = @"
DECLARE @DBName VARCHAR(50)
	DECLARE @path VARCHAR(256) 
	DECLARE @file_Name VARCHAR(256) -- filename for backup 
	
	SET @path = '" + Server.MapPath("..\\") + @"'
	
	set @DBName='GentleParkHO_Old'
			
	SET @file_Name = @path + @DBName + '.rar'
	BACKUP DATABASE @DBName TO DISK = @file_Name 
";

        CommonManager.SQLExec(sql);

        Response.Redirect("../GentleParkHO_Old.rar");
    }

    public void DeleteFileFromFolder(string StrFilename)
    {

        try
        {
            string strPhysicalFolder = Server.MapPath("..\\");
            string strFileFullPath = strPhysicalFolder + StrFilename;

            if (System.IO.File.Exists(strFileFullPath))
            {
                System.IO.File.Delete(strFileFullPath);
            }
        }
        catch (Exception ex)
        {
        }
    }
}
