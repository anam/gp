using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class SqlACC_JournalDetailProvider:DataAccessObject
{
	public SqlACC_JournalDetailProvider()
    {
    }


    public bool DeleteACC_JournalDetail(int aCC_JournalDetailID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteACC_JournalDetail", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_JournalDetailID", SqlDbType.Int).Value = aCC_JournalDetailID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ACC_JournalDetail> GetAllACC_JournalDetails()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetails", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }


    public List<ACC_JournalDetail> GetAllACC_JournalDetailByJournalMasterID(int journalMasterID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailByJournalMasterID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@JournalMasterID", SqlDbType.Int).Value = journalMasterID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }

    public List<ACC_JournalDetail> GetAllACC_JournalDetailForGeneralLedger
        (
        int ACC_ChartOfAccountLabel4ID,
        int ACC_ChartOfAccountLabel3ID,
        int WorkStationID,
        string FromDate,
        string ToDate
        )
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailForGeneralLedger", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ACC_ChartOfAccountLabel4ID", SqlDbType.Int).Value = ACC_ChartOfAccountLabel4ID;
            command.Parameters.Add("@ACC_ChartOfAccountLabel3ID", SqlDbType.Int).Value = ACC_ChartOfAccountLabel3ID;
            command.Parameters.Add("@WorkStationID", SqlDbType.Int).Value = WorkStationID;
            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }

    public List<ACC_JournalDetail> GP_GetAllACC_JournalDetailForTransactionSearch_L2
         (
         string ACC_ChartOfAccountLabel4ID,
        int ACC_ChartOfAccountLabel3ID,
        int WorkStationID,
        string FromDate,
        string ToDate,
        string JournalMasterName
         )
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailForTransactionSearch_L2", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ACC_ChartOfAccountLabel4ID", SqlDbType.NVarChar).Value = ACC_ChartOfAccountLabel4ID;
            command.Parameters.Add("@ACC_ChartOfAccountLabel3ID", SqlDbType.Int).Value = ACC_ChartOfAccountLabel3ID;
            command.Parameters.Add("@WorkStationID", SqlDbType.Int).Value = WorkStationID;
            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            command.Parameters.Add("@JournalMasterName", SqlDbType.NVarChar).Value = JournalMasterName;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }



    public List<ACC_JournalDetail> GetAllACC_JournalDetailForGeneralLedgerByWorkStationIDs
       (
       int ACC_ChartOfAccountLabel4ID,
       int ACC_ChartOfAccountLabel3ID,
       string WorkStationIDs,
       string FromDate,
       string ToDate
       )
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailForGeneralLedgerByWorkStationIDs", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ACC_ChartOfAccountLabel4ID", SqlDbType.Int).Value = ACC_ChartOfAccountLabel4ID;
            command.Parameters.Add("@ACC_ChartOfAccountLabel3ID", SqlDbType.Int).Value = ACC_ChartOfAccountLabel3ID;
            command.Parameters.Add("@WorkStationIDs", SqlDbType.NVarChar).Value = WorkStationIDs;
            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }


    public List<ACC_JournalDetail> GP_GetAllACC_JournalDetailForTransactionSearchByWorkStationIDs
       (
       string ACC_ChartOfAccountLabel4ID,
       int ACC_ChartOfAccountLabel3ID,
       string  WorkStationIDs,
       string FromDate,
       string ToDate,
       string JournalMasterName
       )
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailForTransactionSearchByWorkStationIDs", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ACC_ChartOfAccountLabel4ID", SqlDbType.NVarChar).Value = ACC_ChartOfAccountLabel4ID;
            command.Parameters.Add("@ACC_ChartOfAccountLabel3ID", SqlDbType.Int).Value = ACC_ChartOfAccountLabel3ID;
            command.Parameters.Add("@WorkStationIDs", SqlDbType.NVarChar).Value = WorkStationIDs;
            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            command.Parameters.Add("@JournalMasterName", SqlDbType.NVarChar).Value = JournalMasterName;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }


    public List<ACC_JournalDetail> GetAllACC_JournalDetailForGeneralLedgerByL2
        (
        int ACC_ChartOfAccountLabel4ID,
        int ACC_ChartOfAccountLabel2ID,
        int WorkStationID,
        string FromDate,
        string ToDate
        )
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailForGeneralLedgerByL2", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ACC_ChartOfAccountLabel4ID", SqlDbType.Int).Value = ACC_ChartOfAccountLabel4ID;
            command.Parameters.Add("@ACC_ChartOfAccountLabel2ID", SqlDbType.Int).Value = ACC_ChartOfAccountLabel2ID;
            command.Parameters.Add("@WorkStationID", SqlDbType.Int).Value = WorkStationID;
            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }


    public List<ACC_JournalDetail> GP_GetAllACC_JournalDetailForTransactionSearchByL2
        (
        string ACC_ChartOfAccountLabel4ID,
        int ACC_ChartOfAccountLabel2ID,
        string JournalMasterName,
        int WorkStationID,
        string FromDate,
        string ToDate
        )
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailForTransactionSearchByL2", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ACC_ChartOfAccountLabel4ID", SqlDbType.NVarChar).Value = ACC_ChartOfAccountLabel4ID;
            command.Parameters.Add("@ACC_ChartOfAccountLabel2ID", SqlDbType.Int).Value = ACC_ChartOfAccountLabel2ID;
            command.Parameters.Add("@WorkStationID", SqlDbType.Int).Value = WorkStationID;
            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            command.Parameters.Add("@JournalMasterName", SqlDbType.NVarChar).Value = JournalMasterName;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }

    public List<ACC_JournalDetail> GetAllACC_JournalDetailForGeneralLedgerForSearch
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
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailForGeneralLedgerForSearch", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ACC_ChartOfAccountLabel4ID", SqlDbType.Int).Value = ACC_ChartOfAccountLabel4ID;
            command.Parameters.Add("@ACC_ChartOfAccountLabel3ID", SqlDbType.Int).Value = ACC_ChartOfAccountLabel3ID;
            command.Parameters.Add("@ACC_ChartOfAccountLabel2ID", SqlDbType.Int).Value = ACC_ChartOfAccountLabel2ID;
            command.Parameters.Add("@ACC_ChartOfAccountLabel1ID", SqlDbType.Int).Value = ACC_ChartOfAccountLabel1ID;
            command.Parameters.Add("@WorkStationID", SqlDbType.Int).Value = WorkStationID;
            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }

    public List<ACC_JournalDetail> GetAllACC_JournalDetailForTrialBalance
        (
        string FromDate,
        string ToDate
        )
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailForTrialBalance", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }


    public List<ACC_JournalDetail> GetAllACC_JournalDetailForTrialBalanceWithoutL4
        (
        string FromDate,
        string ToDate
        )
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailForTrialBalanceWithoutL4", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }

    public List<ACC_JournalDetail> GetAllACC_JournalDetailForTrialBalanceLabelWiseL2
        (
        int L1,
        int L2,
        int L3, int L4,
        string FromDate,
        string ToDate
        )
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailForTrialBalanceLabelWiseL2", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@L1", SqlDbType.Int).Value = L1;
            command.Parameters.Add("@L2", SqlDbType.Int).Value = L2;
            command.Parameters.Add("@L3", SqlDbType.Int).Value = L3;
            command.Parameters.Add("@L4", SqlDbType.Int).Value = L4;
            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }


    public List<ACC_JournalDetail> GetAllACC_JournalDetailForTrialBalanceLabelWise
        (
        int L1,
        int L2,
        int L3,int L4,
        string FromDate,
        string ToDate
        )
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailForTrialBalanceLabelWise", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@L1", SqlDbType.Int).Value = L1;
            command.Parameters.Add("@L2", SqlDbType.Int).Value = L2;
            command.Parameters.Add("@L3", SqlDbType.Int).Value = L3;
            command.Parameters.Add("@L4", SqlDbType.Int).Value = L4;
            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }


    public List<ACC_JournalDetail> GP_GetAllACC_JournalDetailForTransactionSearch
        (
        string JournalMasterName,
        string L1,
        string L2,
        string L3, string L4,
        string FromDate,
        string ToDate
        )
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailForTransactionSearch", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@JournalMasterName", SqlDbType.NVarChar).Value = JournalMasterName;
            command.Parameters.Add("@L1", SqlDbType.NVarChar).Value = L1;
            command.Parameters.Add("@L2", SqlDbType.NVarChar).Value = L2;
            command.Parameters.Add("@L3", SqlDbType.NVarChar).Value = L3;
            command.Parameters.Add("@L4", SqlDbType.NVarChar).Value = L4;
            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }

    public List<ACC_JournalDetail> GP_GetAllACC_JournalDetailForTransactionSearchForSearchByLabel
        (
        string JournalMasterName,
        string L1,
        string L2,
        string L3, string L4, string workStationID,
        string FromDate,
        string ToDate
        )
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailForTransactionSearchForSearchByLabel", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@JournalMasterName", SqlDbType.NVarChar).Value = JournalMasterName;
            command.Parameters.Add("@L1", SqlDbType.NVarChar).Value = L1;
            command.Parameters.Add("@L2", SqlDbType.NVarChar).Value = L2;
            command.Parameters.Add("@L3", SqlDbType.NVarChar).Value = L3;
            command.Parameters.Add("@L4", SqlDbType.NVarChar).Value = L4;
            command.Parameters.Add("@WorkStationID", SqlDbType.NVarChar).Value = workStationID;
            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }



    public List<ACC_JournalDetail> GetAllACC_JournalDetailForTrialBalanceLabelWiseWithWorkStationID
        (
        int L1,
        int L2,
        int L3, int L4,
        string workStationID,
        string FromDate,
        string ToDate
        )
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailForTrialBalanceLabelWiseWithWorkStationID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@L1", SqlDbType.Int).Value = L1;
            command.Parameters.Add("@L2", SqlDbType.Int).Value = L2;
            command.Parameters.Add("@L3", SqlDbType.Int).Value = L3;
            command.Parameters.Add("@L4", SqlDbType.Int).Value = L4;
            command.Parameters.Add("@WorkStationID", SqlDbType.NVarChar).Value = workStationID;
            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }


    public List<ACC_JournalDetail> GetAllACC_JournalDetailForTrialBalanceLabelWiseWithWorkStationIDShowRoom
        (
        string L1,
        int L2,
        int L3, int L4,
        string workStationID,
        string FromDate,
      string JournalMasterName,
        string ToDate
        )
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllACC_JournalDetailForTrialBalanceLabelWiseWithWorkStationIDShowRoom", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@L1", SqlDbType.NVarChar).Value = L1;
            command.Parameters.Add("@L2", SqlDbType.Int).Value = L2;
            command.Parameters.Add("@L3", SqlDbType.Int).Value = L3;
            command.Parameters.Add("@L4", SqlDbType.Int).Value = L4;
            command.Parameters.Add("@WorkStationID", SqlDbType.NVarChar).Value = workStationID;
            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FromDate;
            command.Parameters.Add("@JournalMasterName", SqlDbType.NVarChar).Value = JournalMasterName;
            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = ToDate;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetACC_JournalDetailsFromReader(reader);
        }
    }


    public List<ACC_JournalDetail> GetACC_JournalDetailsFromReader(IDataReader reader)
    {
        List<ACC_JournalDetail> aCC_JournalDetails = new List<ACC_JournalDetail>();

        while (reader.Read())
        {
            aCC_JournalDetails.Add(GetACC_JournalDetailFromReader(reader));
        }
        return aCC_JournalDetails;
    }

    public ACC_JournalDetail GetACC_JournalDetailFromReader(IDataReader reader)
    {
        try
        {
            ACC_JournalDetail aCC_JournalDetail = new ACC_JournalDetail();

              try
            { aCC_JournalDetail.ACC_JournalDetailID =  DataAccessObject.IsNULL<int>(reader["ACC_JournalDetailID"]);
              }
            catch (Exception ex) { aCC_JournalDetail.ACC_JournalDetailID = 0; }try
            { aCC_JournalDetail.JournalMasterID =       DataAccessObject.IsNULL<int>(reader["JournalMasterID"]);
            }
            catch (Exception ex) { aCC_JournalDetail.JournalMasterID = 0; } try
            { aCC_JournalDetail.ACC_ChartOfAccountLabel4ID =        DataAccessObject.IsNULL<int>(reader["ACC_ChartOfAccountLabel4ID"]);
                    }
            catch (Exception ex) { aCC_JournalDetail.ACC_ChartOfAccountLabel4ID = 0; }try
            { aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = DataAccessObject.IsNULL<int>(reader["ACC_ChartOfAccountLabel3ID"]);
                    }
            catch (Exception ex) { aCC_JournalDetail.ACC_ChartOfAccountLabel3ID = 0; }try
            { aCC_JournalDetail.WorkStation = DataAccessObject.IsNULL<int>(reader["WorkStation"]);
                    }
            catch (Exception ex) { aCC_JournalDetail.WorkStation = 0; }try
              {
                  aCC_JournalDetail.Debit = DataAccessObject.IsNULL<decimal>(reader["Debit"]);
                    }
              catch (Exception ex) { aCC_JournalDetail.Debit =0; } try
              {
                  aCC_JournalDetail.Credit = DataAccessObject.IsNULL<decimal>(reader["Credit"]);
                    }
              catch (Exception ex) { aCC_JournalDetail.Credit = 0; } try
              {
                  aCC_JournalDetail.ExtraField3 = DataAccessObject.IsNULL<string>(reader["ExtraField3"].ToString());
                    }
              catch (Exception ex) { aCC_JournalDetail.ExtraField3 = ""; } try
              {
                  aCC_JournalDetail.ExtraField2 = DataAccessObject.IsNULL<string>(reader["ExtraField2"].ToString());
                    }
              catch (Exception ex) { aCC_JournalDetail.ExtraField2 = ""; } try
              {
                  aCC_JournalDetail.ExtraField1 = DataAccessObject.IsNULL<string>(reader["ExtraField1"].ToString());
                    }
              catch (Exception ex) { aCC_JournalDetail.ExtraField1 = ""; } try
              {
                  aCC_JournalDetail.AddedBy = DataAccessObject.IsNULL<int>(reader["AddedBy"]);
                    }
              catch (Exception ex) { aCC_JournalDetail.AddedBy =0; } try
              {
                  aCC_JournalDetail.AddedDate = DataAccessObject.IsNULL<DateTime>(reader["AddedDate"]);
                    }
              catch (Exception ex) { aCC_JournalDetail.AddedDate = DateTime.Today; ; } try
            { aCC_JournalDetail.UpdatedBy = DataAccessObject.IsNULL<int>(reader["UpdatedBy"]);
                    }
              catch (Exception ex) { aCC_JournalDetail.UpdatedBy = 0; } try
              {
                  aCC_JournalDetail.UpdatedDate = DataAccessObject.IsNULL<DateTime>(reader["UpdatedDate"]);
                    }
              catch (Exception ex) { aCC_JournalDetail.UpdatedDate =DateTime.Today; } try
              {
                  aCC_JournalDetail.RowStatusID = DataAccessObject.IsNULL<int>(reader["RowStatusID"]);
                }
              catch (Exception ex) { aCC_JournalDetail.RowStatusID = 0; }
            try
            { aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = reader["ChartOfAccountLabel4Text"].ToString(); }
            catch (Exception ex) { aCC_JournalDetail.ACC_ChartOfAccountLabel4Text = ""; }

            try
            { aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = reader["ChartOfAccountLabel3Text"].ToString(); }
            catch (Exception ex) { aCC_JournalDetail.ACC_ChartOfAccountLabel3Text = ""; }

            try
            { aCC_JournalDetail.ACC_ChartOfAccountLabel3Code = reader["ChartOfAccountLabel3Code"].ToString(); }
            catch (Exception ex) { aCC_JournalDetail.ACC_ChartOfAccountLabel3Code = ""; }

            try
            { aCC_JournalDetail.WorkStationName = reader["WorkStationName"].ToString(); }
            catch (Exception ex) { }

            try
            { aCC_JournalDetail.JournalDate = (DateTime)reader["JournalDate"]; }
            catch (Exception ex) { }

            try
            { aCC_JournalDetail.JournalMasterName = reader["JournalMasterName"].ToString(); }
            catch (Exception ex) { }

             return aCC_JournalDetail;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ACC_JournalDetail GetACC_JournalDetailByID(int aCC_JournalDetailID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetACC_JournalDetailByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ACC_JournalDetailID", SqlDbType.Int).Value = aCC_JournalDetailID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetACC_JournalDetailFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertACC_JournalDetail(ACC_JournalDetail aCC_JournalDetail)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertACC_JournalDetail", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@ACC_JournalDetailID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@JournalMasterID", SqlDbType.Int).Value = aCC_JournalDetail.JournalMasterID;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel4ID", SqlDbType.Int).Value = aCC_JournalDetail.ACC_ChartOfAccountLabel4ID;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel3ID", SqlDbType.Int).Value = aCC_JournalDetail.ACC_ChartOfAccountLabel3ID;
            cmd.Parameters.Add("@WorkStation", SqlDbType.Int).Value = aCC_JournalDetail.WorkStation;
            cmd.Parameters.Add("@Debit", SqlDbType.Decimal).Value = aCC_JournalDetail.Debit;
            cmd.Parameters.Add("@Credit", SqlDbType.Decimal).Value = aCC_JournalDetail.Credit;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_JournalDetail.ExtraField3;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_JournalDetail.ExtraField2;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_JournalDetail.ExtraField1;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = aCC_JournalDetail.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = aCC_JournalDetail.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = aCC_JournalDetail.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = aCC_JournalDetail.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = aCC_JournalDetail.RowStatusID;
            connection.Open();

            cmd.ExecuteNonQuery();
            return 1;
        }
    }
    public int InsertACC_JournalDetailTmp(ACC_JournalDetail aCC_JournalDetail)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertACC_JournalDetailTmp", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@ACC_JournalDetailID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@JournalMasterID", SqlDbType.Int).Value = aCC_JournalDetail.JournalMasterID;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel4ID", SqlDbType.Int).Value = aCC_JournalDetail.ACC_ChartOfAccountLabel4ID;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel3ID", SqlDbType.Int).Value = aCC_JournalDetail.ACC_ChartOfAccountLabel3ID;
            cmd.Parameters.Add("@WorkStation", SqlDbType.Int).Value = aCC_JournalDetail.WorkStation;
            cmd.Parameters.Add("@Debit", SqlDbType.Decimal).Value = aCC_JournalDetail.Debit;
            cmd.Parameters.Add("@Credit", SqlDbType.Decimal).Value = aCC_JournalDetail.Credit;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_JournalDetail.ExtraField3;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_JournalDetail.ExtraField2;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_JournalDetail.ExtraField1;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = aCC_JournalDetail.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = aCC_JournalDetail.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = aCC_JournalDetail.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = aCC_JournalDetail.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = aCC_JournalDetail.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return 1;
        }
    }


    public bool UpdateACC_JournalDetail(ACC_JournalDetail aCC_JournalDetail)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateACC_JournalDetail", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACC_JournalDetailID", SqlDbType.Int).Value = aCC_JournalDetail.ACC_JournalDetailID;
            cmd.Parameters.Add("@JournalMasterID", SqlDbType.Int).Value = aCC_JournalDetail.JournalMasterID;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel4ID", SqlDbType.Int).Value = aCC_JournalDetail.ACC_ChartOfAccountLabel4ID;
            cmd.Parameters.Add("@ACC_ChartOfAccountLabel3ID", SqlDbType.Int).Value = aCC_JournalDetail.ACC_ChartOfAccountLabel3ID;
            cmd.Parameters.Add("@WorkStation", SqlDbType.Int).Value = aCC_JournalDetail.WorkStation;
            cmd.Parameters.Add("@Debit", SqlDbType.Decimal).Value = aCC_JournalDetail.Debit;
            cmd.Parameters.Add("@Credit", SqlDbType.Decimal).Value = aCC_JournalDetail.Credit;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = aCC_JournalDetail.ExtraField3;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = aCC_JournalDetail.ExtraField2;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = aCC_JournalDetail.ExtraField1;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = aCC_JournalDetail.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = aCC_JournalDetail.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = aCC_JournalDetail.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = aCC_JournalDetail.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = aCC_JournalDetail.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
