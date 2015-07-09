using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Inventory_PurchasePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initialLoad();
            loadData();
        }
    }

    private void loadData()
    {

        

        int WorkStationID = 0;
        try
        {
            WorkStationID = Int32.Parse(Request.QueryString["WorkStationID"]);
        }
        catch (Exception ex)
        {
            WorkStationID = 0;
        }

        int TransactionTypeID = 0;
        try
        {
            TransactionTypeID = Int32.Parse(Request.QueryString["TransactionTypeID"]);
        }
        catch (Exception ex)
        {
            TransactionTypeID = 0;
        }

        string fromDate = (Request.QueryString["FromDate"] != null ? Request.QueryString["FromDate"].ToString() : "");
        string toDate = (Request.QueryString["ToDate"] != null ? Request.QueryString["ToDate"].ToString() : "");
        lblStockDate.Text = DateTime.Parse(fromDate).ToString("dd/MM/yyyy") + " to " + DateTime.Parse(toDate).ToString("dd/MM/yyyy");

        //purchase info
        string sql = @"SELECT [Pos_TransactionMasterID]
      ,[TransactionDate]
      ,[Pos_TransactionType].TransactionTypeName
      ,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text as FromOrTo
      ,ByWorkStation.ChartOfAccountLabel4Text as ByWorkStatonName
      ,[TransactionID]
      ,Pos_TransactionType.Pos_TransactionTypeID
  FROM [Pos_TransactionMaster]
  inner join Pos_TransactionType on Pos_TransactionMaster.Pos_TransactionTypeID
  =Pos_TransactionType.Pos_TransactionTypeID
  inner  join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
  =Pos_TransactionMaster.ToOrFromID
  inner  join ACC_ChartOfAccountLabel4 as ByWorkStation on ByWorkStation.ACC_ChartOfAccountLabel4ID
  =Pos_TransactionMaster.WorkSatationID
  
  
where   Pos_TransactionMaster.Pos_TransactionTypeID = " + TransactionTypeID.ToString() + @" and  (Pos_TransactionMaster.TransactionDate between '" + fromDate + "' and '" + toDate + "')";

        if (TransactionTypeID != 0)
        {
            lblVoucherType.Text = Pos_TransactionTypeManager.GetPos_TransactionTypeByID(TransactionTypeID).TransactionTypeName;
        }


        if (WorkStationID != 0)
        {
            if(
                TransactionTypeID == 9
                //||
                //TransactionTypeID==12
                )
                sql += " and Pos_TransactionMaster.ToOrFromID =" + WorkStationID;
            else
                sql += " and Pos_TransactionMaster.WorkSatationID =" + WorkStationID;
        }
        sql += @"
order by TransactionDate,[Pos_TransactionType].TransactionTypeName,
  ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
";
        DataSet ds= CommonManager.SQLExec(sql);

        gvTransactionIDs.DataSource = ds.Tables[0];
        gvTransactionIDs.DataBind();

    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
}