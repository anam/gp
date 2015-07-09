using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlSalesCenterProvider:DataAccessObject
{
	public SqlSalesCenterProvider()
    {
    }


    public DataSet  GetAllSalesCenters()
    {
        DataSet SalesCenters = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllSalesCenters", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(SalesCenters);
            myadapter.Dispose();
            connection.Close();

            return SalesCenters;
        }
    }
	public DataSet GetSalesCenterPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetSalesCenterPageWise", connection))
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


    public DataSet  GetDropDownLisAllSalesCenter()
    {
        DataSet SalesCenters = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllSalesCenter", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(SalesCenters);
            myadapter.Dispose();
            connection.Close();

            return SalesCenters;
        }
    }
    public List<SalesCenter> GetSalesCentersFromReader(IDataReader reader)
    {
        List<SalesCenter> salesCenters = new List<SalesCenter>();

        while (reader.Read())
        {
            salesCenters.Add(GetSalesCenterFromReader(reader));
        }
        return salesCenters;
    }

    public SalesCenter GetSalesCenterFromReader(IDataReader reader)
    {
        try
        {
            SalesCenter salesCenter = new SalesCenter
                (

                     DataAccessObject.IsNULL<int>(reader["SalesCenterId"]),
                     DataAccessObject.IsNULL<string>(reader["SalesCenterName"])
                );
             return salesCenter;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public SalesCenter  GetSalesCenterBySalesCenterId(int  salesCenterId)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetSalesCenterBySalesCenterId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SalesCenterId", SqlDbType.Int).Value = salesCenterId;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetSalesCenterFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertSalesCenter(SalesCenter salesCenter)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertSalesCenter", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SalesCenterId", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@SalesCenterName", SqlDbType.NVarChar).Value = salesCenter.SalesCenterName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@SalesCenterId"].Value;
        }
    }

    public bool UpdateSalesCenter(SalesCenter salesCenter)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateSalesCenter", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SalesCenterId", SqlDbType.Int).Value = salesCenter.SalesCenterId;
            cmd.Parameters.Add("@SalesCenterName", SqlDbType.NVarChar).Value = salesCenter.SalesCenterName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteSalesCenter(int salesCenterId)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteSalesCenter", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SalesCenterId", SqlDbType.Int).Value = salesCenterId;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

