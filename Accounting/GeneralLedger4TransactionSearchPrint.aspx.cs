using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            //int ACC_ChartOfAccountLabel4ID = Int32.Parse(Request.QueryString["ACC_ChartOfAccountLabel4ID"]);
            int ACC_ChartOfAccountLabel3ID = 0;
            int ACC_ChartOfAccountLabel2ID = 0;
            try { ACC_ChartOfAccountLabel3ID = Int32.Parse(Request.QueryString["ACC_ChartOfAccountLabel3ID"]); }
            catch (Exception ex)
            { ACC_ChartOfAccountLabel3ID = 0; }
            try { ACC_ChartOfAccountLabel2ID = Int32.Parse(Request.QueryString["ACC_ChartOfAccountLabel2ID"]); }
            catch (Exception ex)
            { ACC_ChartOfAccountLabel2ID = 0; }
            int WorkStationID = Int32.Parse(Request.QueryString["WorkStationID"]);

            string WorkStationIDs = "";
            try
            {
                WorkStationIDs = Request.QueryString["WorkStationIDs"];
            }
            catch (Exception ex)
            {
                WorkStationIDs = "";
                }
            string FromDate = Request.QueryString["FromDate"];
            string ToDate = Request.QueryString["ToDate"];
            
            lblDate.Text = "FOR THE PERIOD FROM " + DateTime.Parse(Request.QueryString["FromDate"]).ToString("dd/MM/yyyy") + " TO " + DateTime.Parse(Request.QueryString["ToDate"]).ToString("dd/MM/yyyy") + "";
            List<ACC_JournalDetail> journalDetails = new List<ACC_JournalDetail>();

            try
            {
            if (ACC_ChartOfAccountLabel2ID != 0)
            {
                journalDetails = ACC_JournalDetailManager.GP_GetAllACC_JournalDetailForTransactionSearchByL2
                        (
                            Request.QueryString["ACC_ChartOfAccountLabel4ID"],
                            ACC_ChartOfAccountLabel2ID,
                            Request.QueryString["JournalMasterName"],
                            WorkStationID,
                            FromDate,
                            ToDate
                        );
            }
            else if (WorkStationIDs == "" || WorkStationIDs== null)
            {
                journalDetails = ACC_JournalDetailManager.GP_GetAllACC_JournalDetailForTransactionSearch_L2
                    (
                        Request.QueryString["ACC_ChartOfAccountLabel4ID"],
                        ACC_ChartOfAccountLabel3ID,
                        WorkStationID,
                        FromDate,
                        ToDate,
                        Request.QueryString["JournalMasterName"]
                    );
            }
            else if (WorkStationIDs != "")
            {
                journalDetails = ACC_JournalDetailManager.GP_GetAllACC_JournalDetailForTransactionSearchByWorkStationIDs
                    (
                        Request.QueryString["ACC_ChartOfAccountLabel4ID"],
                        ACC_ChartOfAccountLabel3ID,
                        WorkStationIDs,
                        FromDate,
                        ToDate,
                        Request.QueryString["JournalMasterName"]
                    );
            }

            
                if (ACC_ChartOfAccountLabel3ID != 0)
                {
                    tableAccountTitle.Visible = true;
                    lblAccountTitle.Text = journalDetails[0].ACC_ChartOfAccountLabel3Text;
                    lblAccountCode.Text = journalDetails[0].ACC_ChartOfAccountLabel3Code;
                    lblTitle.Text = "GENERAL LEDGER";
                }
                else
                {
                    tableAccountTitle.Visible = false;
                    lblTitle.Text = "Transaction summary";
                }
            }
            catch (Exception ex) { }

            string journalDetailsHTML = @"<table width='100%'  id='tblJournalDetails' class='tdBorder' border='0' cellspacing='0' cellpadding='0' style='margin:20px 0;'><tr style='font-weight:bold;font-size:15px;'>
                <td rowspan='2'>
                    Date</td>
                <td rowspan='2' width='350px'>
                    Particulars</td>
                <td rowspan='2'>
                    Voucher<br/> No.</td>
                <td colspan='4'>
                    Amount in Taka</td>
            </tr>
            <tr style='font-weight:bold;font-size:15px;'>
                <td>
                    Debit</td>
                <td>
                    Credit</td>
                <td>
                    Balance</td>
                <td>
                    Dr/Cr</td>
            </tr>";

            decimal totalDebit = 0;
            decimal totalCredit = 0;
            decimal totalDebitSubTotal = 0;
            decimal totalCreditSubTotal = 0;

            int lastJournalMasterID = 0;

            foreach (ACC_JournalDetail journalDetail in journalDetails)
            {

                if (lastJournalMasterID == 0)
                {
                    lastJournalMasterID = journalDetail.JournalMasterID;
                }
                else if (lastJournalMasterID != journalDetail.JournalMasterID)
                {
                    journalDetailsHTML += @"<tr>
                        <td>
                           </td>
                        <td style='text-align:right;'>
                             Sub Total</td>
                        <td>
                           </td>
                        <td style='text-align:right;'>
                            " + totalDebitSubTotal.ToString("0,0.00") + @"</td>
                        <td style='text-align:right;'>
                            " + totalCreditSubTotal.ToString("0,0.00") + @"</td>
                        <td>
                            &nbsp;</td>
                        <td></td>
                    </tr>";

                    totalDebitSubTotal = 0;
                    totalCreditSubTotal = 0;
                    lastJournalMasterID = journalDetail.JournalMasterID;
                }

                string voucherName = "";
                switch (journalDetail.JournalMasterName)
                {
                    case "1"://Receipt Voucher
                        voucherName = "RV";
                        break;
                    case "2"://Payment Voucher
                        voucherName = "PV";
                        break;
                    case "3"://Journal Voucher
                        voucherName = "JV";
                        break;
                    case "4"://Contra Voucher
                        voucherName = "CV";
                        break;
                    default:
                        break;
                }

                journalDetailsHTML += @"<tr>
                <td>
                    "+journalDetail.JournalDate.ToString("dd MMM yyyy")+@"</td>
                <td>
                     " + journalDetail.ACC_ChartOfAccountLabel3Text + " - " + journalDetail.ACC_ChartOfAccountLabel4Text + @"</td>
                <td>
                    <a style='color:blue;text-decoration:underline;' target='_blank' href='Voucherprint.aspx?JournalMasterID=" + journalDetail.JournalMasterID + "'>" + voucherName + "-" + journalDetail.JournalMasterID + @"</a></td>
                <td style='text-align:right;'>
                    " + journalDetail.Debit.ToString("0,0.00")+ @"</td>
                <td style='text-align:right;'>
                    " + journalDetail.Credit.ToString("0,0.00") + @"</td>
                <td>
                    &nbsp;</td>
                <td>
                    " + (journalDetail.Debit==0?"Cr":"Dr") + @"</td>
            </tr>";

                totalCredit += journalDetail.Credit;
                totalDebit += journalDetail.Debit;

                totalDebitSubTotal += journalDetail.Debit;
                totalCreditSubTotal += journalDetail.Credit;

            }

            journalDetailsHTML += @"<tr>
                        <td>
                           </td>
                        <td style='text-align:right;'>
                             Sub Total</td>
                        <td>
                           </td>
                        <td style='text-align:right;'>
                            " + totalDebitSubTotal.ToString("0,0.00") + @"</td>
                        <td style='text-align:right;'>
                            " + totalCreditSubTotal.ToString("0,0.00") + @"</td>
                        <td>
                            &nbsp;</td>
                        <td></td>
                    </tr>";

            if (totalCredit >= totalDebit)
            {
                lblJournalDetials.Text = journalDetailsHTML + "<tr><td></td><td>Total</td><td></td><td style='text-align:right;'>" + totalDebit.ToString("0,0.00") + "</td><td style='text-align:right;'>" + totalCredit.ToString("0,0.00") + "</td><td style='text-align:right;'>" + (totalCredit - totalDebit).ToString("0,0.00") + "</td><td>Cr</td></tr></table>";
            }
            else
            {
                lblJournalDetials.Text = journalDetailsHTML + "<tr><td></td><td>Total</td><td></td><td style='text-align:right;'>" + totalDebit.ToString("0,0.00") + "</td><td style='text-align:right;'>" + totalCredit.ToString("0,0.00") + "</td><td style='text-align:right;'>" + (totalDebit - totalCredit).ToString("0,0.00") + "</td><td>Dr</td></tr></table>";
            }


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
}