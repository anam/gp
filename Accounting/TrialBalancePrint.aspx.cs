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
            string fromDateCurrentYear = "";
            if (Request.QueryString["FromDate"] != null)
            {
                lblDate.Text = "FOR THE PERIOD FROM " + DateTime.Parse(Request.QueryString["FromDate"]).ToString("dd/MM/yyyy") + " TO " + DateTime.Parse(Request.QueryString["Date"]).ToString("dd/MM/yyyy") + "";
                lblStatement.Text = "TRANSACTION SUMMARY";
                fromDateCurrentYear = DateTime.Parse(Request.QueryString["FromDate"]).ToString("yyyy-MM-dd") + " 12:00:00 AM";
            }
            else
            {
                lblDate.Text = "AS ON " + DateTime.Parse(Request.QueryString["Date"]).ToString("dd/MM/yyyy");
                lblStatement.Text = "TRIAL BALANCE";
                fromDateCurrentYear = DateTime.Parse(Request.QueryString["Date"]).ToString("yyyy-") + "01-01 12:00:00 AM";
            }
            
            string toDate = DateTime.Parse(Request.QueryString["Date"]).ToString("yyyy-MM-dd") + " 11:59:59 PM";
            
            //List<ACC_JournalDetail> journalDetailsCurrentYear = ACC_JournalDetailManager.GetAllACC_JournalDetailForTrialBalance
            //    (
            //       fromDateCurrentYear,
            //      toDate
            //    );
            int L1 = 0;
            try
            {
                L1 = Int32.Parse(Request.QueryString["L1"]);
            }
            catch (Exception ex)
            { }

            int L2 = 0;
            try
            {
                L2 = Int32.Parse(Request.QueryString["L2"]);
            }
            catch (Exception ex)
            { }


            int L3 = 0;
            try
            {
                L3 = Int32.Parse(Request.QueryString["L3"]);
            }
            catch (Exception ex)
            { }


            int L4 = 0;
            try
            {
                L4 = Int32.Parse(Request.QueryString["L4"]);
            }
            catch (Exception ex)
            { }

            List<ACC_JournalDetail> journalDetailsCurrentYear = ACC_JournalDetailManager.GetAllACC_JournalDetailForTrialBalanceLableWise
                (L1,L2,L3,L4,
                   fromDateCurrentYear,
                  toDate
                );

            

            string journalDetailsHTML = @"<table width='100%'  id='tblJournalDetails' class='tdBorder' border='0' cellspacing='0' cellpadding='0' style='margin:20px 0;'><tr style='font-weight:bold;font-size:15px;'>
            <tr  style='font-weight:bold;font-size:15px;'>
                <td rowspan='2'>
                    Account<br/>Code</td>
                <td rowspan='2'>
                    Head of Accounts</td>
                <td colspan='2'>
                    Amount</td>
            </tr>
            <tr  style='font-weight:bold;font-size:15px;'>
                <td>
                    Debit</td>
                <td>
                    Credit</td>
            </tr>
            ";
           
            decimal currentYearDebit = 0;
            decimal currentYearCredit = 0;
            decimal balance = 0;
            foreach (ACC_JournalDetail journalDetail in journalDetailsCurrentYear)
            {
                balance = journalDetail.Debit - journalDetail.Credit;

                if (balance >= 0)
                {
                    journalDetail.Debit = balance;
                    journalDetail.Credit = 0;
                }
                else
                {
                    journalDetail.Credit = balance*(-1);
                    journalDetail.Debit = 0;
                }

                currentYearDebit += journalDetail.Debit;
                currentYearCredit += journalDetail.Credit;

                balance = journalDetail.Debit - journalDetail.Credit;

                journalDetailsHTML += @"<tr>
                <td>
                    " + journalDetail.ACC_ChartOfAccountLabel3Code + @"</td>
                <td>
                  <a target='_blank'  href='GeneralLedgerPrint.aspx?ACC_ChartOfAccountLabel4ID="+L4.ToString()+@"&ACC_ChartOfAccountLabel3ID=" + journalDetail.ACC_ChartOfAccountLabel3ID
            + "&WorkStationID=0&FromDate=" + fromDateCurrentYear
            + "&ToDate=" + toDate + @"'>" + journalDetail.ACC_ChartOfAccountLabel3Text  + @"</a> </td>
                ";

                journalDetailsHTML += @"<td style='text-align:right;'>
                    " + journalDetail.Debit.ToString("0,0.00")+ @"</td>
                <td style='text-align:right;'>
                    " + journalDetail.Credit.ToString("0,0.00") + @"</td>
                </tr>";
            }

            
            balance = currentYearDebit - currentYearCredit;
            lblJournalDetials.Text = journalDetailsHTML + "<tr><td></td><td><b>Total:</b></td><td style='text-align:right;'>" + currentYearDebit.ToString("0,0.00") + @"</td><td style='text-align:right;'>" + currentYearCredit.ToString("0,0.00") + @"</td></tr>";

            if (lblStatement.Text == "TRANSACTION SUMMARY")
            {
                lblJournalDetials.Text = lblJournalDetials.Text + "<tr><td></td><td><b>Balance:</b></td><td style='text-align:right;'>" + (balance >= 0 ? balance.ToString("0,0.00") : "") + @"</td><td style='text-align:right;'>" + (balance < 0 ? ((-1) * balance).ToString("0,0.00") : "") + @"</td></tr>";
            }

            lblJournalDetials.Text += "</table>";
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