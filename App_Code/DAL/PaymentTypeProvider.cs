using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlPaymentTypeProvider:DataAccessObject
{
	public SqlPaymentTypeProvider()
    {
    }


    public DataSet  GetAllPaymentTypes()
    {
        DataSet PaymentTypes = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllPaymentTypes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(PaymentTypes);
            myadapter.Dispose();
            connection.Close();

            return PaymentTypes;
        }
    }
	public DataSet GetPaymentTypePageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetPaymentTypePageWise", connection))
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


    public DataSet  GetDropDownLisAllPaymentType()
    {
        DataSet PaymentTypes = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllPaymentType", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(PaymentTypes);
            myadapter.Dispose();
            connection.Close();

            return PaymentTypes;
        }
    }
    public List<PaymentType> GetPaymentTypesFromReader(IDataReader reader)
    {
        List<PaymentType> paymentTypes = new List<PaymentType>();

        while (reader.Read())
        {
            paymentTypes.Add(GetPaymentTypeFromReader(reader));
        }
        return paymentTypes;
    }

    public PaymentType GetPaymentTypeFromReader(IDataReader reader)
    {
        try
        {
            PaymentType paymentType = new PaymentType
                (

                     DataAccessObject.IsNULL<int>(reader["PaymnentTypeID"]),
                     DataAccessObject.IsNULL<string>(reader["PaymnentTypeName"])
                );
             return paymentType;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public PaymentType  GetPaymentTypeByPymnentTypeID(int  pymnentTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetPaymentTypeByPymnentTypeID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PaymnentTypeID", SqlDbType.Int).Value = pymnentTypeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetPaymentTypeFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertPaymentType(PaymentType paymentType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertPaymentType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PaymnentTypeID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@PaymnentTypeName", SqlDbType.NVarChar).Value = paymentType.PaymnentTypeName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@PymnentTypeID"].Value;
        }
    }

    public bool UpdatePaymentType(PaymentType paymentType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdatePaymentType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PaymnentTypeID", SqlDbType.Int).Value = paymentType.PaymnentTypeID;
            cmd.Parameters.Add("@PaymnentTypeName", SqlDbType.NVarChar).Value = paymentType.PaymnentTypeName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeletePaymentType(int paymnentTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeletePaymentType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PaymnentTypeID", SqlDbType.Int).Value = paymnentTypeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

