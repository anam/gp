using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Accounting_Voucher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            loadData();
        }
    }

    private void loadData()
    {
        try
        {
            ACC_JournalMaster journalMaster = ACC_JournalMasterManager.GetACC_JournalMasterByID(int.Parse(Request.QueryString["JournalMasterID"]));

            
            List<ACC_JournalDetail> journalDetails = ACC_JournalDetailManager.GetAllACC_JournalDetailByJournalMasterID(int.Parse(Request.QueryString["JournalMasterID"]));

            string journalDetailsHTML = @"<table id='tblJournalDetails' class='tdBorder' border='0' cellspacing='0' cellpadding='0' style='margin:20px 0;'>
                    <tr style='font-weight:bold;font-size:15px;'>
                        <td width='100px'>
                            Account Code
                        </td>
                        <td width='487px'>
                            Account Title
                        </td>
                        <td width='80px'>
                            Debit
                        </td>
                        <td width='80px'>
                            Credit
                        </td>
                    </tr>";

            decimal totalDebit = 0;
            decimal totalCredit = 0;
            trAddress.Visible = false;
            trCheck.Visible = false;
            bool defaultJournal = false;

            switch (journalMaster.JournalMasterName)
            {
                case "1"://Receipt Voucher
                    lblVoucherName.Text = "RECEIPT";
                    lblVoucherType.Text = "RV";
                    lblReceivedfromOrPayto.Text = "Received from";
                    trAddress.Visible = true;
                    trCheck.Visible = true;
                //generate table
                    
                    foreach (ACC_JournalDetail journalDetail in journalDetails)
                    {
                        try
                            {
                                if (journalDetail.Credit == 0)
                                {
                                    journalDetailsHTML += @"<tr>
                                    <td>
                                        " + journalDetail.ACC_ChartOfAccountLabel3Code + @"
                                    </td>
                                    <td>
                                        " + journalDetail.ACC_ChartOfAccountLabel3Text + " - " + journalDetail.ACC_ChartOfAccountLabel4Text + " (" + journalDetail.WorkStationName + @")
                                    </td>
                                    <td style='text-align:right;'>
                                        " + journalDetail.Debit.ToString("0,0.00") + @"
                                    </td>
                                    <td style='text-align:right;'>
                                       " + journalDetail.Credit.ToString("0,0.00") + @"
                                    </td>
                                </tr>";
                                    totalCredit += journalDetail.Credit;
                                    totalDebit += journalDetail.Debit;
                                }
                            }
                            catch (Exception ex) { }
                        
                    }

                    foreach (ACC_JournalDetail journalDetail in journalDetails)
                    {
                        try
                        {
                            if (journalDetail.Credit != 0)
                            {
                                journalDetailsHTML += @"<tr>
                                    <td>
                                        " + journalDetail.ACC_ChartOfAccountLabel3Code + @"
                                    </td>
                                    <td>
                                        " + journalDetail.ACC_ChartOfAccountLabel3Text + " - " + journalDetail.ACC_ChartOfAccountLabel4Text + " (" + journalDetail.WorkStationName + @")
                                    </td>
                                    <td style='text-align:right;'>
                                        " + journalDetail.Debit.ToString("0,0.00") + @"
                                    </td>
                                    <td style='text-align:right;'>
                                       " + journalDetail.Credit.ToString("0,0.00") + @"
                                    </td>
                                </tr>";
                                totalCredit += journalDetail.Credit;
                                totalDebit += journalDetail.Debit;
                            }
                        }
                        catch (Exception ex) { }

                    }

                    foreach (ACC_JournalDetail journalDetail in journalDetails)
                    {
                        if (journalDetail.Credit == 0)
                        {
                            lblChequeDate.Text = journalDetail.ExtraField1;
                            lblChequeNo.Text = journalDetail.ExtraField3;
                            try
                            {
                                lblChequeBank.Text = journalDetail.ExtraField2.Split(',')[1];
                            }
                            catch (Exception ex) { }
                            try
                            {
                                lblChequeBranch.Text = journalDetail.ExtraField2.Split(',')[0];
                            }
                            catch (Exception ex) { }
                            break;
                        }
                    }

                    lblJournalDetials.Text = journalDetailsHTML + "<tr><td></td><td><b>Total:</b></td><td style='text-align:right;'>" + totalDebit.ToString("0,0.00") + "</td><td style='text-align:right;'>" + totalCredit.ToString("0,0.00") + "</td></tr><tr><td  colspan='4'><b>Taka in words:</b> " + NumberToWords(int.Parse(totalCredit.ToString("0"))) + " taka only.</td></tr></table>";

                    break;

                case "2"://Payment Voucher
                    lblVoucherName.Text = "PAYMENT";
                    lblVoucherType.Text = "PV";
                    lblReceivedfromOrPayto.Text = "Pay To";
                    trAddress.Visible = true;
                    trCheck.Visible = true;
                    //generate table
                    

                    foreach (ACC_JournalDetail journalDetail in journalDetails)
                    {
                        try
                        {
                            if (journalDetail.Credit == 0)
                            {
                                journalDetailsHTML += @"<tr>
                                    <td>
                                        " + journalDetail.ACC_ChartOfAccountLabel3Code + @"
                                    </td>
                                    <td>
                                        " + journalDetail.ACC_ChartOfAccountLabel3Text + " - " + journalDetail.ACC_ChartOfAccountLabel4Text + " (" + journalDetail.WorkStationName + @")
                                    </td>
                                    <td style='text-align:right;'>
                                        " + journalDetail.Debit.ToString("0,0.00") + @"
                                    </td>
                                    <td style='text-align:right;'>
                                       " + journalDetail.Credit.ToString("0,0.00") + @"
                                    </td>
                                </tr>";
                                totalCredit += journalDetail.Credit;
                                totalDebit += journalDetail.Debit;
                            }
                        }
                        catch (Exception ex) { }

                    }

                    foreach (ACC_JournalDetail journalDetail in journalDetails)
                    {
                        try
                        {
                            if (journalDetail.Credit != 0)
                            {
                                journalDetailsHTML += @"<tr>
                                    <td>
                                        " + journalDetail.ACC_ChartOfAccountLabel3Code + @"
                                    </td>
                                    <td>
                                        " + journalDetail.ACC_ChartOfAccountLabel3Text + " - " + journalDetail.ACC_ChartOfAccountLabel4Text + " (" + journalDetail.WorkStationName + @")
                                    </td>
                                    <td style='text-align:right;'>
                                        " + journalDetail.Debit.ToString("0,0.00") + @"
                                    </td>
                                    <td style='text-align:right;'>
                                       " + journalDetail.Credit.ToString("0,0.00") + @"
                                    </td>
                                </tr>";
                                totalCredit += journalDetail.Credit;
                                totalDebit += journalDetail.Debit;
                            }
                        }
                        catch (Exception ex) { }

                    }

                    foreach (ACC_JournalDetail journalDetail in journalDetails)
                    {
                        if (journalDetail.Debit == 0)
                        {
                            lblChequeDate.Text = journalDetail.ExtraField1;
                            lblChequeNo.Text = journalDetail.ExtraField3;
                            try
                            {
                                lblChequeBank.Text = journalDetail.ExtraField2.Split(',')[1];
                            }
                            catch (Exception ex) { }
                            try
                            {
                                lblChequeBranch.Text = journalDetail.ExtraField2.Split(',')[0];
                            }
                            catch (Exception ex) { }
                            break;
                        }
                    }

                    lblJournalDetials.Text = journalDetailsHTML + "<tr><td></td><td><b>Total:</b></td><td style='text-align:right;'>" + totalDebit.ToString("0,0.00") + "</td><td style='text-align:right;'>" + totalCredit.ToString("0,0.00") + "</td></tr><tr><td  colspan='4'><b>Taka in words:</b> " + NumberToWords(int.Parse(totalDebit.ToString("0"))) + " taka only.</td></tr></table>";

                    break;

                case "3"://Journal Voucher
                    lblVoucherName.Text = "JOURNAL";
                    lblVoucherType.Text = "JV";
                    defaultJournal = true;
                    
                    break;

                case "4"://Contra Voucher
                    lblVoucherName.Text = "CONTRA";
                    lblVoucherType.Text = "CV";
                    trCheck.Visible = true;
                    defaultJournal = true;
                    foreach (ACC_JournalDetail journalDetail in journalDetails)
                    {
                        if (journalDetail.ExtraField3 != "")
                        {
                            lblChequeDate.Text = journalDetail.ExtraField1;
                            lblChequeNo.Text = journalDetail.ExtraField3;
                            try
                            {
                                lblChequeBank.Text = journalDetail.ExtraField2.Split(',')[1];
                            }
                            catch (Exception ex) { }
                            try
                            {
                                lblChequeBranch.Text = journalDetail.ExtraField2.Split(',')[0];
                            }
                            catch (Exception ex) { }
                            break;
                        }
                    }
                    break;

                default:
                    break;
            }

            if (defaultJournal)
            {
                foreach (ACC_JournalDetail journalDetail in journalDetails)
                {
                    try
                    {
                        journalDetailsHTML += @"<tr>
                                <td>
                                    " + journalDetail.ACC_ChartOfAccountLabel3Code + @"
                                </td>
                                <td>
                                    " + journalDetail.ACC_ChartOfAccountLabel3Text + " - " + journalDetail.ACC_ChartOfAccountLabel4Text + " (" + journalDetail.WorkStationName + @")
                                </td>
                                <td style='text-align:right;'>
                                    " + journalDetail.Debit.ToString("0,0.00") + @"
                                </td>
                                <td style='text-align:right;'>
                                    " + journalDetail.Credit.ToString("0,0.00") + @"
                                </td>
                            </tr>";
                        totalCredit += journalDetail.Credit;
                        totalDebit += journalDetail.Debit;
                    }
                    catch (Exception ex) { }

                }
               

                lblJournalDetials.Text = journalDetailsHTML + "<tr><td></td><td><b>Total:</b></td><td style='text-align:right;'>" + totalDebit.ToString("0,0.00") + "</td><td style='text-align:right;'>" + totalCredit.ToString("0,0.00") + "</td></tr><tr><td  colspan='4'><b>Taka in words:</b> " + NumberToWords(int.Parse(totalCredit.ToString("0"))) + " taka only.</td></tr></table>";

            }

            lblOfficeName.Text= journalDetails[0].WorkStationName;
            lblJournalMasterID.Text = Request.QueryString["JournalMasterID"];
            
            if (journalMaster.RowStatusID != 1)
            {
                lblJournalMasterID.BackColor = System.Drawing.Color.Red;
            }

            lblDate.Text = journalMaster.JournalDate.ToString("dd MMM yyyy");
            lblCustomerName.Text = journalMaster.ExtraField1;
            lblAddress.Text = journalMaster.ExtraField2;
            if (journalMaster.Note.Contains("Inventory Purchase-"))
            {
                lblExplanation.Text = "<a href='../Inventory/PurchasePrint.aspx?PurchaseID=" + journalMaster.Note.Replace("Inventory Purchase-","") + "' target='_blank'>" + journalMaster.Note+"</a>";
            }
            else
            {
                lblExplanation.Text = journalMaster.Note;
            }

            gvJournal.DataSource = journalDetails;
            gvJournal.DataBind();

            List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
            aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

            DataSet ds = CommonManager.SQLExec(
                @"
SELECT
ACC_ChartOfAccountLabel3.ChartOfAccountLabel3Text +'('+ ACC_ChartOfAccountLabel2.ChartOfAccountLabel2Text +')('+ 
                       ACC_ChartOfAccountLabel1.ChartOfAccountLabel1Text+')' as Head,
ACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID
FROM         ACC_ChartOfAccountLabel1 INNER JOIN
                      ACC_ChartOfAccountLabel2 ON 
                      ACC_ChartOfAccountLabel1.ACC_ChartOfAccountLabel1ID = ACC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel1ID INNER JOIN
                      ACC_ChartOfAccountLabel3 ON ACC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel2ID = ACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel2ID
where ACC_ChartOfAccountLabel3.RowStatusID=1
order by ACC_ChartOfAccountLabel1.ChartOfAccountLabel1Text,ACC_ChartOfAccountLabel2.ChartOfAccountLabel2Text,ACC_ChartOfAccountLabel3.ChartOfAccountLabel3Text
"
                );
            ddlL4.Items.Clear();
            ddlL3.Items.Clear();
            ddlWorkStation.Items.Clear();
            ddlL4.Items.Add(new ListItem("N/A", "0"));
            ddlL3.Items.Add(new ListItem("N/A", "0"));
            ddlWorkStation.Items.Add(new ListItem("N/A", "0"));
            foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
            {
                ddlL4.Items.Add(new ListItem(
                    (
                    aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 9
                ? "Access(Prod)" :
                (
                aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 10
                ? "Access (Non-Prod)" :
                (
                aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 2
                ? "Fabrics" : 
                (
                aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 3
                ? "Product" : aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString()
                )
                
                )
                )
                    )
                    + "-" + aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString()));
                if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1)
                {
                    ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                    ddlWorkStation.Items.Add(item);
                }
            }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ddlL3.Items.Add(new ListItem(dr["Head"].ToString(), dr["ACC_ChartOfAccountLabel3ID"].ToString()));
            }

            /*
            //List<ACC_ChartOfAccountLabel3> aCC_ChartOfAccountLabel3s = new List<ACC_ChartOfAccountLabel3>();
            //aCC_ChartOfAccountLabel3s = ACC_ChartOfAccountLabel3Manager.GetAllACC_ChartOfAccountLabel3sForJournalEntryForDropDownList();
        
            foreach (GridViewRow gvr in gvJournal.Rows)
            {
                HiddenField hfWorkStationID = (HiddenField)gvr.FindControl("hfWorkStationID");
                HiddenField hfACC_ChartOfAccountLabel4ID = (HiddenField)gvr.FindControl("hfACC_ChartOfAccountLabel4ID");
                HiddenField hfACC_ChartOfAccountLabel3ID = (HiddenField)gvr.FindControl("hfACC_ChartOfAccountLabel3ID");
                DropDownList ddlWorkStation = (DropDownList)gvr.FindControl("ddlWorkStation");
                DropDownList ddlL3 = (DropDownList)gvr.FindControl("ddlL3");
                DropDownList ddlL4 = (DropDownList)gvr.FindControl("ddlL4");
                foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
                {
                    ddlL4.Items.Add(new ListItem(
                        (
                        aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 9
                    ? "Access(Prod)" : 
                    (
                    aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 10
                    ? "Access (Non-Prod)" :
                    (
                    aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 2
                    ? "Fabrics" : aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString()
                    )
                    )
                        )
                        + "-" + aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString()));
                    if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1)
                    {
                        ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                        ddlWorkStation.Items.Add(item);
                    }
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ddlL3.Items.Add(new ListItem(dr["Head"].ToString(), dr["ACC_ChartOfAccountLabel3ID"].ToString()));
                }

                ddlL4.SelectedValue = hfACC_ChartOfAccountLabel4ID.Value;
                ddlL3.SelectedValue = hfACC_ChartOfAccountLabel3ID.Value;
                ddlWorkStation.SelectedValue = hfWorkStationID.Value;
            }
             */ 
            
        }
        catch (Exception ex)
        { }
    }

    

    public static string NumberToWords(int number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWords(number / 1000000) + " Million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " Thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " Hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";

            var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Response.Redirect("JournalModification.aspx?JournalMasterID="+txtJournalID.Text);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sql = "";

        foreach (GridViewRow gvr in gvJournal.Rows)
        {
            HiddenField hfJournalDetailsID = (HiddenField)gvr.FindControl("hfJournalDetailsID");
            CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");

            if (
                chkSelect.Checked
                )
            {
                sql = "";
                if (ddlL4.SelectedValue != "0")
                {
                    sql = @" update ACC_JournalDetail set 
                ACC_ChartOfAccountLabel4ID=" + ddlL4.SelectedValue + @"
                 where ACC_JournalDetailID=" + hfJournalDetailsID.Value + @";
                ";
                }

                if (ddlL3.SelectedValue != "0")
                {
                    sql += @" update ACC_JournalDetail set 
                ACC_ChartOfAccountLabel3ID=" + ddlL3.SelectedValue + @"
                where ACC_JournalDetailID=" + hfJournalDetailsID.Value + @";
                ";
                }

                if (ddlWorkStation.SelectedValue != "0")
                {
                    sql += @" update ACC_JournalDetail set 
                WorkStation=" + ddlWorkStation.SelectedValue + " where ACC_JournalDetailID=" + hfJournalDetailsID.Value + @";
                ";
                }
                
                CommonManager.SQLExec(sql);
            }
        }

       
        loadData();
    }
    protected void btnJournalMasterSave_Click(object sender, EventArgs e)
    {
        CommonManager.SQLExec("update ACC_JournalMaster set JournalMasterName='" + ddlJournalType.SelectedValue + @"' where ACC_JournalMasterID=" + Request.QueryString["JournalMasterID"]);
        Response.Redirect("JournalModification.aspx?JournalMasterID=" + txtJournalID.Text);
    }
}