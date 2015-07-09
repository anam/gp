﻿using System;
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
                lblStatement.Text = "BALANCE SHEET";
                fromDateCurrentYear = DateTime.Parse(Request.QueryString["FromDate"]).ToString("yyyy-MM-dd") + " 12:00:00 AM";
            }
            else
            {
                lblDate.Text = "AS ON " + DateTime.Parse(Request.QueryString["Date"]).ToString("dd/MM/yyyy");
                lblStatement.Text = "BALANCE SHEET";
                fromDateCurrentYear = DateTime.Parse(Request.QueryString["Date"]).ToString("yyyy-") + "01-01 12:00:00 AM";
            }

            string toDate = DateTime.Parse(Request.QueryString["Date"]).ToString("yyyy-MM-dd") + " 11:59:59 PM";
            
            int L1 = 0;
            int L2 = 0;
            int L3 = 0;
            int L4 = 0;
            L1 = 11;

            List<ACC_JournalDetail> financilaStatement = new List<ACC_JournalDetail>();

            List<ACC_ChartOfAccountLabel1> aCC_ChartOfAccountLabel1s = new List<ACC_ChartOfAccountLabel1>();
            aCC_ChartOfAccountLabel1s = ACC_ChartOfAccountLabel1Manager.GetAllACC_ChartOfAccountLabel1s().FindAll(x => x.RowStatusID == 1);
            foreach (ACC_ChartOfAccountLabel1 aCC_ChartOfAccountLabel1 in aCC_ChartOfAccountLabel1s)
            {
                if (aCC_ChartOfAccountLabel1.ExtraField1 == "Asset"
                    ||
                    aCC_ChartOfAccountLabel1.ExtraField1 == "Liability & Equity"
                    )
                {

                    ACC_JournalDetail eachL1 = new ACC_JournalDetail();

                    List<ACC_JournalDetail> journalDetailsCurrentYear = ACC_JournalDetailManager.GetAllACC_JournalDetailForTrialBalanceLableWiseL2
                        (aCC_ChartOfAccountLabel1.ACC_ChartOfAccountLabel1ID, L2, L3, L4,
                           fromDateCurrentYear,
                          toDate
                        );

                    decimal balance = 0;
                    
                    foreach (ACC_JournalDetail journalDetail in journalDetailsCurrentYear)
                    {
                        balance += journalDetail.Debit - journalDetail.Credit;
                    }

                    eachL1.ACC_ChartOfAccountLabel3ID = aCC_ChartOfAccountLabel1.ACC_ChartOfAccountLabel1ID;
                    eachL1.ACC_ChartOfAccountLabel3Text = aCC_ChartOfAccountLabel1.ChartOfAccountLabel1Text;
                    eachL1.Debit = ((balance >= 0) ? balance : 0);
                    eachL1.Credit = ((balance < 0) ? (balance * (-1)) : 0);
                    financilaStatement.Add(eachL1);                    
                }
            }

            string html = "<table class='tableBorder' cellpadding='0' cellspacing='0'><tr><td colspan='3'></td><td>Debit</td><td>Credit</td><td></td><td></td></tr>";
            html += "<tr><td>Assets:</td><td colspan='6'></td></tr>";
            html += "<tr><td></td><td>Current Asset</td><td colspan='5'></td></tr>";
            decimal totalAsset = 0;
            decimal totalLiability_Equity = 0;
            decimal totalLiability = 0;
            decimal totalEquity = 0;
            decimal totalAssetCurrent_Asset = 0;
            decimal totalAssetNon_Current_Asset = 0;
            decimal totalLiability_Equity_Current_Liabilities = 0;
            decimal totalLiability_Equity_Non_Current_Liabilities = 0;
            decimal totalLiability_Equity_Reserves = 0;
            decimal totalLiability_Equity_Share_Capital = 0;
            
            bool isPrintedAssetNon_Current_Asset = false;
            bool isPrintedLiability_Equity_Current_Liabilities = false;
            bool isPrintedLiability_Equity_Non_Current_Liabilities = false;
            bool isPrintedLiability_Equity_Reserves = false;
            bool isPrintedLiability_Equity_Share_Capital = false;

            foreach (ACC_JournalDetail L1_Journal in financilaStatement)
            {
                
                switch (L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[0])
                {
                    case "(Current Asset":
                        totalAsset += (L1_Journal.Debit -L1_Journal.Credit);
                        totalAssetCurrent_Asset += (L1_Journal.Debit - L1_Journal.Credit);
                        break;
                    case "(Non-Current Asset":
                        if (!isPrintedAssetNon_Current_Asset)
                        {
                            html += "<tr><td colspan='2' style='text-align:right;'>Total Current Asset(A):</td><td colspan='3'></td><td>" + totalAssetCurrent_Asset.ToString("0,0.00") + "</td><td></td></tr>";
                            html += "<tr><td></td><td>Non-Current Asset</td><td colspan='5'></td></tr>";
                            isPrintedAssetNon_Current_Asset = true;
                        }
                        totalAsset += (L1_Journal.Debit - L1_Journal.Credit);
                        totalAssetNon_Current_Asset += (L1_Journal.Debit - L1_Journal.Credit);
                        break;
                    case "(Current Liabilities":
                        if (!isPrintedLiability_Equity_Current_Liabilities)
                        {
                            html += "<tr><td colspan='2' style='text-align:right;'>Total Non-Current Asset(B):</td><td colspan='3'></td><td>" + totalAssetNon_Current_Asset.ToString("0,0.00") + "</td><td></td></tr>";
                            html += "<tr><td colspan='1' style='text-align:right;'>Total Asset(A+B):</td><td colspan='5'></td><td>" + totalAsset.ToString("0,0.00") + "</td></tr>";
                            html += "<tr><td colspan='7'></td></tr>";
                            html += "<tr><td>Liability & Shareholders' Equity:</td><td colspan='6'></td></tr>";
                            html += "<tr><td>&nbsp;&nbsp;&nbsp;Liabilities:</td><td colspan='6'></td></tr>";
                            html += "<tr><td></td><td>Current Liabilities</td><td colspan='5'></td></tr>";
                            isPrintedLiability_Equity_Current_Liabilities = true;
                        }
                        totalLiability_Equity += (L1_Journal.Credit - L1_Journal.Debit);
                        totalLiability += (L1_Journal.Credit - L1_Journal.Debit);
                        totalLiability_Equity_Current_Liabilities += (L1_Journal.Credit - L1_Journal.Debit);
                        break;

                    case "(Non-Current Liabilities":
                        if (!isPrintedLiability_Equity_Non_Current_Liabilities)
                        {
                            html += "<tr><td></td><td colspan='2' style='text-align:right;'>Total Current Liabilities(i):</td><td colspan='2'></td><td>" + totalLiability_Equity_Current_Liabilities.ToString("0,0.00") + "</td><td></td></tr>";
                            html += "<tr><td></td><td>Non-Current Liabilities</td><td colspan='5'></td></tr>";
                            isPrintedLiability_Equity_Non_Current_Liabilities = true;
                        }
                        totalLiability_Equity += (L1_Journal.Credit - L1_Journal.Debit);
                        totalLiability += (L1_Journal.Credit - L1_Journal.Debit);
                        totalLiability_Equity_Non_Current_Liabilities += (L1_Journal.Credit - L1_Journal.Debit);
                        break;

                    case "(Reserves":
                        if (!isPrintedLiability_Equity_Reserves)
                        {
                            html += "<tr><td></td><td colspan='2' style='text-align:right;'>Total Non-Current Liabilities(ii):</td><td colspan='2'></td><td>" + totalLiability_Equity_Non_Current_Liabilities.ToString("0,0.00") + "</td><td></td></tr>";
                            html += "<tr><td></td><td colspan='2' style='text-align:right;'>Total  Liabilities[C=(i + ii)]:</td><td colspan='2'></td><td>" + totalLiability.ToString("0,0.00") + "</td><td></td></tr>";
                            html += "<tr><td>&nbsp;&nbsp;&nbsp;Shareholders' Equity:</td><td colspan='6'></td></tr>";
                            html += "<tr><td></td><td>Reserves</td><td colspan='5'></td></tr>";
                            isPrintedLiability_Equity_Reserves = true;
                        }
                        totalLiability_Equity += (L1_Journal.Credit - L1_Journal.Debit);
                        totalEquity += (L1_Journal.Credit - L1_Journal.Debit);
                        totalLiability_Equity_Reserves += (L1_Journal.Credit - L1_Journal.Debit);
                        break;


                    case "(Share Capital":
                        if (!isPrintedLiability_Equity_Share_Capital)
                        {
                            html += "<tr><td></td><td colspan='2' style='text-align:right;'>Total Reserves(iii):</td><td colspan='2'></td><td>" + totalLiability_Equity_Reserves.ToString("0,0.00") + "</td><td></td></tr>";
                            html += "<tr><td></td><td>Non-Current Liabilities</td><td colspan='5'></td></tr>";
                            isPrintedLiability_Equity_Share_Capital = true;
                        }
                        totalLiability_Equity += (L1_Journal.Credit - L1_Journal.Debit);
                        totalEquity += (L1_Journal.Credit - L1_Journal.Debit);
                        totalLiability_Equity_Share_Capital += (L1_Journal.Credit - L1_Journal.Debit);
                        break;
                    default:
                        break;
                }
                html += "<tr><td colspan='2'></td><td><a target='_blank' href='TrialBalancePrint.aspx?L4=0&L1=" + L1_Journal.ACC_ChartOfAccountLabel3ID + "&L2=0&L3=0&WorkStationID=0&FromDate=" + fromDateCurrentYear + "&Date=" + toDate + "'>" + L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[1].Substring(1, L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[1].Length - 1) + "</a></td><td class='rightAlign'>" + L1_Journal.Debit.ToString("0,0.00") + "</td><td class='rightAlign'>" + L1_Journal.Credit.ToString("0,0.00") + "</td><td colspan='2'></td></tr>";
            
            }

            html += "<tr><td></td><td colspan='2' style='text-align:right;'>Total Share Capital(iv):</td><td colspan='2'></td><td>" + totalLiability_Equity_Share_Capital.ToString("0,0.00") + "</td><td></td></tr>";
            html += "<tr><td></td><td colspan='2' style='text-align:right;'>Total  Shareholders' Equity[D=(iii + iv)]:</td><td colspan='2'></td><td>" + totalEquity.ToString("0,0.00") + "</td><td></td></tr>";
            html += "<tr><td></td><td colspan='2' style='text-align:right;'>Total Liability & Shareholders' Equity(C+D):</td><td colspan='3'></td><td>" + totalLiability_Equity.ToString("0,0.00") + "</td></tr>";
            decimal netProfit_amount = netProfit();
            html += "<tr><td><a href='IncomeStatementPrint.aspx?Date="+Request.QueryString["Date"]+@"' target='_blank'>Net Profit (E)</a></td><td colspan='4'></td><td>" + netProfit_amount.ToString("0,0.00") + "</td><td></td></tr>";
            html += "<tr><td></td><td colspan='2' style='text-align:right;'>Total Liability & Shareholders' Equity with Net-Profit (C+D+E):</td><td colspan='3'></td><td>" + (netProfit_amount+totalLiability_Equity).ToString("0,0.00") + "</td></tr>";
            html += "</table>";

            lblJournalDetials.Text = html;

        }
        catch (Exception ex)
        { }
    }
    private decimal netProfit()
    {
        try
        {
            string fromDateCurrentYear = "";
            if (Request.QueryString["FromDate"] != null)
            {
                lblDate.Text = "FOR THE PERIOD FROM " + DateTime.Parse(Request.QueryString["FromDate"]).ToString("dd/MM/yyyy") + " TO " + DateTime.Parse(Request.QueryString["Date"]).ToString("dd/MM/yyyy") + "";
                //lblStatement.Text = "INCOME STATEMENT";
                fromDateCurrentYear = DateTime.Parse(Request.QueryString["FromDate"]).ToString("yyyy-MM-dd") + " 12:00:00 AM";
            }
            else
            {
                lblDate.Text = "AS ON " + DateTime.Parse(Request.QueryString["Date"]).ToString("dd/MM/yyyy");
               // lblStatement.Text = "INCOME STATEMENT";
                fromDateCurrentYear = DateTime.Parse(Request.QueryString["Date"]).ToString("yyyy-") + "01-01 12:00:00 AM";
            }

            string toDate = DateTime.Parse(Request.QueryString["Date"]).ToString("yyyy-MM-dd") + " 11:59:59 PM";

            int L1 = 0;
            int L2 = 0;
            int L3 = 0;
            int L4 = 0;
            L1 = 11;

            List<ACC_JournalDetail> financilaStatement = new List<ACC_JournalDetail>();

            List<ACC_ChartOfAccountLabel1> aCC_ChartOfAccountLabel1s = new List<ACC_ChartOfAccountLabel1>();
            aCC_ChartOfAccountLabel1s = ACC_ChartOfAccountLabel1Manager.GetAllACC_ChartOfAccountLabel1s().FindAll(x => x.RowStatusID == 1);
            foreach (ACC_ChartOfAccountLabel1 aCC_ChartOfAccountLabel1 in aCC_ChartOfAccountLabel1s)
            {
                if (aCC_ChartOfAccountLabel1.ExtraField1 == "Expense"
                    ||
                    aCC_ChartOfAccountLabel1.ExtraField1 == "Income"
                    )
                {

                    ACC_JournalDetail eachL1 = new ACC_JournalDetail();

                    List<ACC_JournalDetail> journalDetailsCurrentYear = ACC_JournalDetailManager.GetAllACC_JournalDetailForTrialBalanceLableWiseL2
                        (aCC_ChartOfAccountLabel1.ACC_ChartOfAccountLabel1ID, L2, L3, L4,
                           fromDateCurrentYear,
                          toDate
                        );

                    decimal balance = 0;

                    foreach (ACC_JournalDetail journalDetail in journalDetailsCurrentYear)
                    {
                        balance += journalDetail.Debit - journalDetail.Credit;
                    }

                    eachL1.ACC_ChartOfAccountLabel3ID = aCC_ChartOfAccountLabel1.ACC_ChartOfAccountLabel1ID;
                    eachL1.ACC_ChartOfAccountLabel3Text = aCC_ChartOfAccountLabel1.ChartOfAccountLabel1Text;
                    eachL1.Debit = ((balance >= 0) ? balance : 0);
                    eachL1.Credit = ((balance < 0) ? (balance * (-1)) : 0);
                    financilaStatement.Add(eachL1);
                }
            }

            string html = "<table class='tableBorder' cellpadding='0' cellspacing='0'><tr><td colspan='3'></td><td>Debit</td><td>Credit</td><td></td><td></td></tr>";
            html += "<tr><td></td><td>Sales Revenue</td><td colspan='5'></td></tr>";
            decimal totalOperating_income = 0;
            decimal totalSales_Expence = 0;
            decimal totalGross_Profit = 0;
            decimal totalOperating_Expenses = 0;
            decimal totalOperating_Profit = 0;
            decimal totalNon_Operating_income = 0;
            decimal totalProfit_Before_Tax = 0;
            decimal totalIncome_Tax_Expense = 0;
            decimal totalNet_Profit = 0;

            foreach (ACC_JournalDetail L1_Journal in financilaStatement)
            {
                if (L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[0] == "(Operating income")
                {
                    totalOperating_income += (L1_Journal.Debit - L1_Journal.Credit);
                    html += "<tr><td colspan='2'></td><td><a target='_blank' href='TrialBalancePrint.aspx?L4=0&L1=" + L1_Journal.ACC_ChartOfAccountLabel3ID + "&L2=0&L3=0&WorkStationID=0&FromDate=" + fromDateCurrentYear + "&Date=" + toDate + "'>" + L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[1].Substring(1, L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[1].Length - 1) + "</a></td><td class='rightAlign'>" + L1_Journal.Debit.ToString("0,0.00") + "</td><td class='rightAlign'>" + L1_Journal.Credit.ToString("0,0.00") + "</td><td></td><td></td></tr>";
                }
            }

            foreach (ACC_JournalDetail L1_Journal in financilaStatement)
            {
                if (L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[0] == "(Sales Expence")
                {
                    totalSales_Expence += (L1_Journal.Debit - L1_Journal.Credit);
                    html += "<tr><td colspan='2'></td><td><a target='_blank' href='TrialBalancePrint.aspx?L4=0&L1=" + L1_Journal.ACC_ChartOfAccountLabel3ID + "&L2=0&L3=0&WorkStationID=0&FromDate=" + fromDateCurrentYear + "&Date=" + toDate + "'>" + L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[1].Substring(1, L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[1].Length - 1) + "</a></td><td class='rightAlign'>" + L1_Journal.Debit.ToString("0,0.00") + "</td><td class='rightAlign'>" + L1_Journal.Credit.ToString("0,0.00") + "</td><td></td><td></td></tr>";
                }
            }

            totalGross_Profit = ((-1) * totalOperating_income) - totalSales_Expence;
            html += "<tr><td style='text-align:right;'>Gross Profit(A)</td><td colspan='4'></td><td>" + totalGross_Profit.ToString("0,0.00") + "</td><td></td></tr>";
            html += "<tr><td></td><td>Operating Expenses</td><td colspan='5'></td></tr>";

            foreach (ACC_JournalDetail L1_Journal in financilaStatement)
            {
                if (L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[0] == "(Operating Expence")
                {
                    totalOperating_Expenses += (L1_Journal.Debit - L1_Journal.Credit);
                    html += "<tr><td colspan='2'></td><td><a target='_blank' href='TrialBalancePrint.aspx?L4=0&L1=" + L1_Journal.ACC_ChartOfAccountLabel3ID + "&L2=0&L3=0&WorkStationID=0&FromDate=" + fromDateCurrentYear + "&Date=" + toDate + "'>" + L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[1].Substring(1, L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[1].Length - 1) + "</a></td><td class='rightAlign'>" + L1_Journal.Debit.ToString("0,0.00") + "</td><td class='rightAlign'>" + L1_Journal.Credit.ToString("0,0.00") + "</td><td></td><td></td></tr>";
                }
            }
            html += "<tr><td colspan='2'></td><td  style='text-align:right;'>Total Operating Expenses(B)</td><td colspan='2'></td><td>" + totalOperating_Expenses.ToString("0,0.00") + "</td><td></td></tr>";
            totalOperating_Profit = totalGross_Profit - totalOperating_Expenses;
            html += "<tr><td style='text-align:right;'>Operating Profit[C=(A-B)]</td><td colspan='5'></td><td>" + totalOperating_Profit.ToString("0,0.00") + "</td></tr>";

            html += "<tr><td></td><td>Non-Operating income</td><td colspan='5'></td></tr>";

            foreach (ACC_JournalDetail L1_Journal in financilaStatement)
            {
                if (L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[0] == "(Non-Operating income")
                {
                    totalNon_Operating_income += (L1_Journal.Debit - L1_Journal.Credit);
                    html += "<tr><td colspan='2'></td><td><a target='_blank' href='TrialBalancePrint.aspx?L4=0&L1=" + L1_Journal.ACC_ChartOfAccountLabel3ID + "&L2=0&L3=0&WorkStationID=0&FromDate=" + fromDateCurrentYear + "&Date=" + toDate + "'>" + L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[1].Substring(1, L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[1].Length - 1) + "</a></td><td class='rightAlign'>" + L1_Journal.Debit.ToString("0,0.00") + "</td><td class='rightAlign'>" + L1_Journal.Credit.ToString("0,0.00") + "</td><td></td><td></td></tr>";
                }
            }
            html += "<tr><td colspan='2'></td><td style='text-align:right;'>Total Non-Operating income(D)</td><td colspan='2'></td><td>" + totalNon_Operating_income.ToString("0,0.00") + "</td><td></td></tr>";
            totalProfit_Before_Tax = totalOperating_Profit - totalNon_Operating_income;
            html += "<tr><td style='text-align:right;'>Profit Before Tax(C-D)</td><td colspan='5'></td><td>" + totalOperating_Profit.ToString("0,0.00") + "</td></tr>";
            foreach (ACC_JournalDetail L1_Journal in financilaStatement)
            {
                if (L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[0] == "(Income Tax Expence")
                {
                    totalIncome_Tax_Expense += (L1_Journal.Debit - L1_Journal.Credit);
                    html += "<tr><td colspan='2'></td><td><a target='_blank' href='TrialBalancePrint.aspx?L4=0&L1=" + L1_Journal.ACC_ChartOfAccountLabel3ID + "&L2=0&L3=0&WorkStationID=0&FromDate=" + fromDateCurrentYear + "&Date=" + toDate + "'>" + L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[1].Substring(1, L1_Journal.ACC_ChartOfAccountLabel3Text.Split(')')[1].Length - 1) + "</a></td><td class='rightAlign'>" + L1_Journal.Debit.ToString("0,0.00") + "</td><td class='rightAlign'>" + L1_Journal.Credit.ToString("0,0.00") + "</td><td>" + totalIncome_Tax_Expense.ToString("0,0.00") + "</td><td></td></tr>";
                }
            }
            totalNet_Profit = totalProfit_Before_Tax - totalIncome_Tax_Expense;
            html += "<tr><td style='text-align:right;'>Net Profit </td><td colspan='5'></td><td>" + totalNet_Profit.ToString("0,0.00") + "</td></tr>";

            html += "</table>";

            return totalNet_Profit;

        }
        catch (Exception ex)
        {
            return 0;
        }
        return 0;

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