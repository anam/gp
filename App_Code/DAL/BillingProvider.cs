using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlBillingProvider:DataAccessObject
{
	public SqlBillingProvider()
    {
    }


    public DataSet  GetAllBillings()
    {
        DataSet Billings = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllBillings", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Billings);
            myadapter.Dispose();
            connection.Close();

            return Billings;
        }
    }
	public DataSet GetBillingPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetBillingPageWise", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize",  PageSize );
                command.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                command.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                connection.Open();
                SqlDataAdapter myadapter = new SqlDataAdapter(command);
                myadapter.Fill(ds);
                myadapter.Dispose();
                connection.Close();
                recordCount = Convert.ToInt32(command.Parameters["@RecordCount"].Value);
                return ds;
            }
        }
    }


    public DataSet GetBillingByClientID(int clientID)
    {
        DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetBillingByClientID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ClientID", SqlDbType.NVarChar).Value = clientID;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(ds);
            myadapter.Dispose();
            connection.Close();
          //  recordCount = Convert.ToInt32(command.Parameters["@RecordCount"].Value);
            return ds;
        }
    }

    public DataSet  GetDropDownLisAllBilling()
    {
        DataSet Billings = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllBilling", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Billings);
            myadapter.Dispose();
            connection.Close();

            return Billings;
        }
    }
    public List<Billing> GetBillingsFromReader(IDataReader reader)
    {
        List<Billing> billings = new List<Billing>();

        while (reader.Read())
        {
            billings.Add(GetBillingFromReader(reader));
        }
        return billings;
    }

    public Billing GetBillingFromReader(IDataReader reader)
    {
        try
        {
            Billing billing = new Billing
                (

                     DataAccessObject.IsNULL<int>(reader["BillingID"]),
                     DataAccessObject.IsNULL<int>(reader["ClientID"]),
                     DataAccessObject.IsNULL<DateTime>(reader["PaymnetDate"]),
                     DataAccessObject.IsNULL<int>(reader["PaymnetType"]),
                     DataAccessObject.IsNULL<decimal>(reader["AmountCharge"]),
                     DataAccessObject.IsNULL<decimal>(reader["AmountPaid"]),
                      DataAccessObject.IsNULL<int>(reader["TreatmentServiceID"]),
                     
                     DataAccessObject.IsNULL<string>(reader["EntryBy"].ToString()),
                     DataAccessObject.IsNULL<DateTime>(reader["EntryDate"])
                );
             return billing;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Billing  GetBillingByBillingID(int  billingID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetBillingByBillingID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@BillingID", SqlDbType.Int).Value = billingID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetBillingFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertBilling(Billing billing)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertBilling", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@BillingID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = billing.ClientID;
            cmd.Parameters.Add("@PaymnetDate", SqlDbType.DateTime).Value = billing.PaymnetDate;
            cmd.Parameters.Add("@PaymnetType", SqlDbType.Int).Value = billing.PaymnetType;
            cmd.Parameters.Add("@AmountCharge", SqlDbType.Decimal).Value = billing.AmountCharge;
            cmd.Parameters.Add("@AmountPaid", SqlDbType.Decimal).Value = billing.AmountPaid;
            cmd.Parameters.Add("@TreatmentServiceID", SqlDbType.Decimal).Value = billing.TreatmentServiceID;
            
            cmd.Parameters.Add("@EntryBy", SqlDbType.NVarChar).Value = billing.EntryBy;
            cmd.Parameters.Add("@EntryDate", SqlDbType.DateTime).Value = billing.EntryDate;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@BillingID"].Value;
        }
    }

    public bool UpdateBilling(Billing billing)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateBilling", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@BillingID", SqlDbType.Int).Value = billing.BillingID;
            cmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = billing.ClientID;
            cmd.Parameters.Add("@PaymnetDate", SqlDbType.DateTime).Value = billing.PaymnetDate;
            cmd.Parameters.Add("@PaymnetType", SqlDbType.Int).Value = billing.PaymnetType;
            cmd.Parameters.Add("@AmountCharge", SqlDbType.Decimal).Value = billing.AmountCharge;
            cmd.Parameters.Add("@AmountPaid", SqlDbType.Decimal).Value = billing.AmountPaid;
            cmd.Parameters.Add("@TreatmentServiceID", SqlDbType.Decimal).Value = billing.TreatmentServiceID;
            cmd.Parameters.Add("@EntryBy", SqlDbType.NVarChar).Value = billing.EntryBy;
            cmd.Parameters.Add("@EntryDate", SqlDbType.DateTime).Value = billing.EntryDate;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
    public bool UpdatePaymnetType(int BillingID, int PaymnetTypes, int TreatmentService)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateBillingPaymnetStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BillingID", SqlDbType.Int).Value = BillingID;

            cmd.Parameters.Add("@PaymnetType", SqlDbType.Int).Value = PaymnetTypes;
            cmd.Parameters.Add("@TreatmentService", SqlDbType.Int).Value = TreatmentService;
         
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
    
    public bool DeleteBilling(int billingID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteBilling", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@BillingID", SqlDbType.Int).Value = billingID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

