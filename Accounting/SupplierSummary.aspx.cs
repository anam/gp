using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Accounting_SupplierSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        loadData();
    }

    private void loadData()
    {
        string Sql = @"Select SUM(Debit-Credit) as Balance,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text as Supplier,ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
            into #Supplayerpayable
             from ACC_JournalDetail 
            inner join ACC_JournalMaster on ACC_JournalMaster.ACC_JournalMasterID=ACC_JournalDetail.JournalMasterID
            inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID=ACC_JournalDetail.ACC_ChartOfAccountLabel4ID
            where ACC_JournalMaster.RowStatusID=1 and ACC_JournalDetail.RowStatusID=1 and ACC_JournalDetail.ACC_ChartOfAccountLabel3ID=43
            and ACC_ChartOfAccountLabel4.ACC_HeadTypeID=6
            Group by ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
            order by ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text

            Select ACC_ChartOfAccountLabel4ID as ID,Supplier,Balance from #Supplayerpayable
            where Balance<>0
            drop table #Supplayerpayable";

        DataSet ds = CommonManager.SQLExec(Sql);

        decimal balance = 0;
        
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            balance += decimal.Parse(dr["Balance"].ToString());
            //http://www.gentlepark.net/Accounting/TrialBalancePrint.aspx?L4=969&L1=8&L2=20&L3=43&WorkStationID=0&FromDate=2013-07-25%2012:00:00%20AM&Date=2013-07-25%2011:59:59%20PM

            dr["Supplier"] = "<a href='GeneralLedgerPrint.aspx?ACC_ChartOfAccountLabel4ID=" + dr["ID"].ToString() + "&ACC_ChartOfAccountLabel3ID=43&WorkStationID=0&FromDate=2013-01-01%2012:00:00%20AM&ToDate=" + DateTime.Today.ToString("yyyy-MM-dd") + "%2011:59:59%20PM' target='_blank'>" + dr["Supplier"].ToString() + "</a>";
        }

        gvSupplier.DataSource = ds.Tables[0];
        gvSupplier.DataBind();

        lblTotal.Text = balance.ToString("0,0.00");
    }
}