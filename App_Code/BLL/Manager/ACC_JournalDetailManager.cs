using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class ACC_JournalDetailManager
{
	public ACC_JournalDetailManager()
	{
	}

    public static List<ACC_JournalDetail> GetAllACC_JournalDetails()
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GetAllACC_JournalDetails();
        return aCC_JournalDetails;
    }


    public static List<ACC_JournalDetail> GetAllACC_JournalDetailByJournalMasterID(int journalMasterID)
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GetAllACC_JournalDetailByJournalMasterID(journalMasterID);
        return aCC_JournalDetails;
    }


    public static List<ACC_JournalDetail> GetAllACC_JournalDetailForGeneralLedger
        (
        int ACC_ChartOfAccountLabel4ID,
        int ACC_ChartOfAccountLabel3ID,
        int WorkStationID,
        string FromDate,
        string ToDate
        )
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GetAllACC_JournalDetailForGeneralLedger
            (
                ACC_ChartOfAccountLabel4ID,
                ACC_ChartOfAccountLabel3ID,
                WorkStationID,
                FromDate,
                ToDate
            );
        return aCC_JournalDetails;
    }


    public static List<ACC_JournalDetail> GP_GetAllACC_JournalDetailForTransactionSearch_L2
        (
        string ACC_ChartOfAccountLabel4ID,
        int ACC_ChartOfAccountLabel3ID,
        int WorkStationID,
        string FromDate,
        string ToDate,
        string JournalMasterName
        )
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GP_GetAllACC_JournalDetailForTransactionSearch_L2
            (
                ACC_ChartOfAccountLabel4ID,
                ACC_ChartOfAccountLabel3ID,
                WorkStationID,
                FromDate,
                ToDate,
                JournalMasterName
            );
        return aCC_JournalDetails;
    }



    public static List<ACC_JournalDetail> GetAllACC_JournalDetailForGeneralLedgerByWorkStationIDs
        (
        int ACC_ChartOfAccountLabel4ID,
        int ACC_ChartOfAccountLabel3ID,
        String WorkStationIDs,
        string FromDate,
        string ToDate
        )
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GetAllACC_JournalDetailForGeneralLedgerByWorkStationIDs
            (
                ACC_ChartOfAccountLabel4ID,
                ACC_ChartOfAccountLabel3ID,
                WorkStationIDs,
                FromDate,
                ToDate
            );
        return aCC_JournalDetails;
    }


    public static List<ACC_JournalDetail> GP_GetAllACC_JournalDetailForTransactionSearchByWorkStationIDs
        (
        string ACC_ChartOfAccountLabel4ID,
       int ACC_ChartOfAccountLabel3ID,
       string WorkStationIDs,
       string FromDate,
       string ToDate,
       string JournalMasterName
        )
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GP_GetAllACC_JournalDetailForTransactionSearchByWorkStationIDs
            (
                ACC_ChartOfAccountLabel4ID,
                ACC_ChartOfAccountLabel3ID,
                WorkStationIDs,
                FromDate,
                ToDate,
                JournalMasterName
            );
        return aCC_JournalDetails;
    }


    public static List<ACC_JournalDetail> GetAllACC_JournalDetailForGeneralLedgerByL2
       (
       int ACC_ChartOfAccountLabel4ID,
       int ACC_ChartOfAccountLabel2ID,
       int WorkStationID,
       string FromDate,
       string ToDate
       )
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GetAllACC_JournalDetailForGeneralLedgerByL2
            (
                ACC_ChartOfAccountLabel4ID,
                ACC_ChartOfAccountLabel2ID,
                WorkStationID,
                FromDate,
                ToDate
            );
        return aCC_JournalDetails;
    }


    public static List<ACC_JournalDetail> GP_GetAllACC_JournalDetailForTransactionSearchByL2
      (
        string ACC_ChartOfAccountLabel4ID,
        int ACC_ChartOfAccountLabel2ID,
        string JournalMasterName,
        int WorkStationID,
        string FromDate,
        string ToDate
      )
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GP_GetAllACC_JournalDetailForTransactionSearchByL2
            (
                ACC_ChartOfAccountLabel4ID,
                ACC_ChartOfAccountLabel2ID,
                JournalMasterName,
                WorkStationID,
                FromDate,
                ToDate
            );
        return aCC_JournalDetails;
    }

    public static List<ACC_JournalDetail> GetAllACC_JournalDetailForGeneralLedgerForSearch
       (
       int ACC_ChartOfAccountLabel4ID,
       int ACC_ChartOfAccountLabel3ID,
       int ACC_ChartOfAccountLabel2ID,
       int ACC_ChartOfAccountLabel1ID,
       int WorkStationID,
       string FromDate,
       string ToDate
       )
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GetAllACC_JournalDetailForGeneralLedgerForSearch
            (
                ACC_ChartOfAccountLabel4ID,
                ACC_ChartOfAccountLabel3ID,
                ACC_ChartOfAccountLabel2ID,
                ACC_ChartOfAccountLabel1ID,
                WorkStationID,
                FromDate,
                ToDate
            );
        return aCC_JournalDetails;
    }



    public static List<ACC_JournalDetail> GetAllACC_JournalDetailForTrialBalance
        (
        string FromDate,
        string ToDate
        )
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GetAllACC_JournalDetailForTrialBalance
            (
                FromDate,
                ToDate
            );
        return aCC_JournalDetails;
    }


    public static List<ACC_JournalDetail> GetAllACC_JournalDetailForTrialBalanceWithoutL4
       (
       string FromDate,
       string ToDate
       )
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GetAllACC_JournalDetailForTrialBalanceWithoutL4
            (
                FromDate,
                ToDate
            );
        return aCC_JournalDetails;
    }


    public static List<ACC_JournalDetail> GetAllACC_JournalDetailForTrialBalanceLableWise
       (
        int L1,
        int L2,
        int L3,
        int L4,
       string FromDate,
       string ToDate
       )
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GetAllACC_JournalDetailForTrialBalanceLabelWise
            (L1,L2,L3,L4,
                FromDate,
                ToDate
            );
        return aCC_JournalDetails;
    }

    public static List<ACC_JournalDetail> GP_GetAllACC_JournalDetailForTransactionSearch
      (
       string JournalMasterName,
        string L1,
        string L2,
        string L3, string L4,
        string FromDate,
        string ToDate
      )
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GP_GetAllACC_JournalDetailForTransactionSearch
            (JournalMasterName, L1, L2, L3, L4,
                FromDate,
                ToDate
            );
        return aCC_JournalDetails;
    }

    public static List<ACC_JournalDetail> GP_GetAllACC_JournalDetailForTransactionSearchForSearchByLabel
     (
      string JournalMasterName,
       string L1,
       string L2,
       string L3, string L4,string workStationID,
       string FromDate,
       string ToDate
     )
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GP_GetAllACC_JournalDetailForTransactionSearchForSearchByLabel
            (JournalMasterName, L1, L2, L3, L4,workStationID,
                FromDate,
                ToDate
            );
        return aCC_JournalDetails;
    }

    public static List<ACC_JournalDetail> GetAllACC_JournalDetailForTrialBalanceLableWiseWithWorkStationID
       (
        int L1,
        int L2,
        int L3,
        int L4,
        string workStationID,
       string FromDate,
       string ToDate
       )
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GetAllACC_JournalDetailForTrialBalanceLabelWiseWithWorkStationID
            (L1, L2, L3, L4,
            workStationID,
                FromDate,
                ToDate
            );
        return aCC_JournalDetails;
    }

    public static List<ACC_JournalDetail> GetAllACC_JournalDetailForTrialBalanceLableWiseWithWorkStationIDShowRoom
      (
       string L1,
       int L2,
       int L3,
       int L4,
       string workStationID,
      string FromDate,
      string JournalMasterName,
      string ToDate
      )
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GetAllACC_JournalDetailForTrialBalanceLabelWiseWithWorkStationIDShowRoom
            (L1, L2, L3, L4,
            workStationID,
                FromDate,JournalMasterName,
                ToDate
            );
        return aCC_JournalDetails;
    }


    public static List<ACC_JournalDetail> GetAllACC_JournalDetailForTrialBalanceLableWiseL2
       (
        int L1,
        int L2,
        int L3,
        int L4,
       string FromDate,
       string ToDate
       )
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetails = sqlACC_JournalDetailProvider.GetAllACC_JournalDetailForTrialBalanceLabelWiseL2
            (L1, L2, L3, L4,
                FromDate,
                ToDate
            );
        return aCC_JournalDetails;
    }


    public static ACC_JournalDetail GetACC_JournalDetailByID(int id)
    {
        ACC_JournalDetail aCC_JournalDetail = new ACC_JournalDetail();
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        aCC_JournalDetail = sqlACC_JournalDetailProvider.GetACC_JournalDetailByID(id);
        return aCC_JournalDetail;
    }


    public static int InsertACC_JournalDetail(ACC_JournalDetail aCC_JournalDetail)
    {
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        return sqlACC_JournalDetailProvider.InsertACC_JournalDetail(aCC_JournalDetail);
    }


    public static int InsertACC_JournalDetailTmp(ACC_JournalDetail aCC_JournalDetail)
    {
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        return sqlACC_JournalDetailProvider.InsertACC_JournalDetailTmp(aCC_JournalDetail);
    }

    public static bool UpdateACC_JournalDetail(ACC_JournalDetail aCC_JournalDetail)
    {
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        return sqlACC_JournalDetailProvider.UpdateACC_JournalDetail(aCC_JournalDetail);
    }

    public static bool DeleteACC_JournalDetail(int aCC_JournalDetailID)
    {
        SqlACC_JournalDetailProvider sqlACC_JournalDetailProvider = new SqlACC_JournalDetailProvider();
        return sqlACC_JournalDetailProvider.DeleteACC_JournalDetail(aCC_JournalDetailID);
    }
}
