using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlTreatmentServiceProvider:DataAccessObject
{
	public SqlTreatmentServiceProvider()
    {
    }


    public DataSet  GetAllTreatmentServices()
    {
        DataSet TreatmentServices = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllTreatmentServices", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(TreatmentServices);
            myadapter.Dispose();
            connection.Close();

            return TreatmentServices;
        }
    }
	public DataSet GetTreatmentServicePageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetTreatmentServicePageWise", connection))
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


    public DataSet  GetDropDownLisAllTreatmentService()
    {
        DataSet TreatmentServices = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllTreatmentService", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(TreatmentServices);
            myadapter.Dispose();
            connection.Close();

            return TreatmentServices;
        }
    }
    public List<TreatmentService> GetTreatmentServicesFromReader(IDataReader reader)
    {
        List<TreatmentService> treatmentServices = new List<TreatmentService>();

        while (reader.Read())
        {
            treatmentServices.Add(GetTreatmentServiceFromReader(reader));
        }
        return treatmentServices;
    }

    public TreatmentService GetTreatmentServiceFromReader(IDataReader reader)
    {
        try
        {
            TreatmentService treatmentService = new TreatmentService
                (

                     DataAccessObject.IsNULL<int>(reader["TreatmentServiceID"]),
                     DataAccessObject.IsNULL<string>(reader["TreatmentServiceName"])
                );
             return treatmentService;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public TreatmentService  GetTreatmentServiceByTreatmentServiceID(int  treatmentServiceID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetTreatmentServiceByTreatmentServiceID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@TreatmentServiceID", SqlDbType.Int).Value = treatmentServiceID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetTreatmentServiceFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertTreatmentService(TreatmentService treatmentService)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertTreatmentService", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TreatmentServiceID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@TreatmentServiceName", SqlDbType.NVarChar).Value = treatmentService.TreatmentServiceName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@TreatmentServiceID"].Value;
        }
    }

    public bool UpdateTreatmentService(TreatmentService treatmentService)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateTreatmentService", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TreatmentServiceID", SqlDbType.Int).Value = treatmentService.TreatmentServiceID;
            cmd.Parameters.Add("@TreatmentServiceName", SqlDbType.NVarChar).Value = treatmentService.TreatmentServiceName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteTreatmentService(int treatmentServiceID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteTreatmentService", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TreatmentServiceID", SqlDbType.Int).Value = treatmentServiceID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

