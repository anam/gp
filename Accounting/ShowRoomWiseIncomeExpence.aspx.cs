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
            string workStationID=" in (0)";
            
            try
            {
                workStationID = Request.QueryString["WorkStationID"];
            }
            catch (Exception ex)
            { 
                workStationID = " in (0)"; 
            }
             decimal income = loadData("",42, 0, workStationID);
             decimal expence = loadData("4,13,26,27,28",0, 0, workStationID);

            lblJournalDetials.Text += "<table width='100%'  class='tblJournalDetails' class='tdBorder' border='0' cellspacing='0' cellpadding='0' style='margin:20px 0;'><tr><td style='width:577px; text-align:right;'><b>Balance:</b></td><td style='text-align:right;'>" + (income - expence).ToString("0,0.00") + @"</td></tr></table>";

        }
    }

    private decimal loadData(string L1, int L2, int L3, string workStationID)
    {
        try
        {
            string fromDateCurrentYear = "";
            if (Request.QueryString["FromDate"] != null)
            {
                lblDate.Text = "FOR THE PERIOD FROM " + DateTime.Parse(Request.QueryString["FromDate"]).ToString("dd/MM/yyyy") + " TO " + DateTime.Parse(Request.QueryString["Date"]).ToString("dd/MM/yyyy") + "";
                lblStatement.Text = "SHOW ROOM TRANSACTION SUMMARY";
                fromDateCurrentYear = DateTime.Parse(Request.QueryString["FromDate"]).ToString("yyyy-MM-dd") + " 12:00:00 AM";
            }
            else
            {
                lblDate.Text = "AS ON " + DateTime.Parse(Request.QueryString["Date"]).ToString("dd/MM/yyyy");
                lblStatement.Text = "TRIAL BALANCE";
                fromDateCurrentYear = DateTime.Parse(Request.QueryString["Date"]).ToString("yyyy-") + "01-01 12:00:00 AM";
            }

            lblDate.Text += "<br/>" + Request.QueryString["ShowRoomName"];

            string toDate = DateTime.Parse(Request.QueryString["Date"]).ToString("yyyy-MM-dd") + " 11:59:59 PM";
           
            int L4 = 0;
            try
            {
                L4 = Int32.Parse(Request.QueryString["L4"]);
            }
            catch (Exception ex)
            { }

           

            List<ACC_JournalDetail> journalDetailsCurrentYear = ACC_JournalDetailManager.GetAllACC_JournalDetailForTrialBalanceLableWiseWithWorkStationIDShowRoom
                (L1,L2,L3,L4,workStationID,
                   fromDateCurrentYear, Request.QueryString["JournalMasterName"],
                  toDate
                );

            string journalDetailsHTML = @"<table width='100%'  class='tblJournalDetails' class='tdBorder' border='0' cellspacing='0' cellpadding='0' style='margin:20px 0;'><tr style='font-weight:bold;font-size:15px;'>
            <tr  style='font-weight:bold;font-size:15px;'>
               
                <td>
                    " + (lblJournalDetials.Text == "" ? "Income" : "Expence") + @"</td>
                <td>
                    Amount</td>
            </tr>
            ";
           
            decimal currentYearDebit = 0;
            decimal currentYearCredit = 0;
            decimal balance = 0;
            decimal balance_Total = 0;
            decimal balanceFinal = 0;
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

                //if (!journalDetail.ACC_ChartOfAccountLabel3Text.Contains("Sales Revenue"))
                //{
                    currentYearDebit += journalDetail.Debit;
                    currentYearCredit += journalDetail.Credit;
                //}


                    balanceFinal += journalDetail.Debit - journalDetail.Credit;
                balance_Total += balance<0?((-1)*balance):balance;

                journalDetailsHTML += @"<tr>
                <td style='width:577px;'>
                  <a target='_blank'  href='GeneralLedgerPrint.aspx?ACC_ChartOfAccountLabel4ID="+L4.ToString()+@"&ACC_ChartOfAccountLabel3ID=" + journalDetail.ACC_ChartOfAccountLabel3ID
            + "&WorkStationID=0&&WorkStationIDs="+workStationID+@"&FromDate=" + fromDateCurrentYear
            + "&ToDate=" + toDate + @"'>" + journalDetail.ACC_ChartOfAccountLabel3Text  + @"</a> </td>
                ";

                journalDetailsHTML += @"<td style='text-align:right;'>
                    " + (journalDetail.Debit == 0 ? journalDetail.Credit : journalDetail.Debit).ToString("0,0.00") + @"</td>
                </tr>";
            }

            
            //balance = currentYearDebit - currentYearCredit;
            lblJournalDetials.Text += journalDetailsHTML + "<tr><td style='width:577px;'><b>Total:</b></td><td style='text-align:right;'>" + (balance_Total > 0 ? balance_Total : ((-1) * balance_Total)).ToString("0,0.00") + @"</td></tr></table>";
            return (balance_Total > 0 ? balance_Total : ((-1) * balance_Total));
            
        }
        catch (Exception ex)
        { return 0; }
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