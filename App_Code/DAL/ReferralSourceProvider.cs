using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlReferralSourceProvider:DataAccessObject
{
	public SqlReferralSourceProvider()
    {
    }


    public DataSet  GetAllReferralSources()
    {
        DataSet ReferralSources = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllReferralSources", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(ReferralSources);
            myadapter.Dispose();
            connection.Close();

            return ReferralSources;
        }
    }
	public DataSet GetReferralSourcePageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetReferralSourcePageWise", connection))
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


    public ReferralSource  GetReferralSourceByMetroLocationID(int  metroLocationID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetReferralSourceByMetroLocationID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MetroLocationID", SqlDbType.NVarChar).Value = metroLocationID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetReferralSourceFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public DataSet  GetDropDownLisAllReferralSource()
    {
        DataSet ReferralSources = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllReferralSource", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(ReferralSources);
            myadapter.Dispose();
            connection.Close();

            return ReferralSources;
        }
    }
    public DataSet GetDropDownLisAllReferralSourceByMetroLocationID(int MetroLocationID)
    {
        DataSet ReferralSources = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownLisAllReferralSourceByMetroLocationID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MetroLocationID", SqlDbType.NVarChar).Value = MetroLocationID;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(ReferralSources);
            myadapter.Dispose();
            connection.Close();

            return ReferralSources;
        }
    }

    public DataSet   GetAllReferralSourcesWithRelation()
    {
       DataSet ds = new DataSet();
       using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllReferralSourcesWithRelation", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(ds);
            myadapter.Dispose();
            connection.Close();

            return ds;
        }
    }
    public List<ReferralSource> GetReferralSourcesFromReader(IDataReader reader)
    {
        List<ReferralSource> referralSources = new List<ReferralSource>();

        while (reader.Read())
        {
            referralSources.Add(GetReferralSourceFromReader(reader));
        }
        return referralSources;
    }

    public ReferralSource GetReferralSourceFromReader(IDataReader reader)
    {
        try
        {
            ReferralSource referralSource = new ReferralSource
                (

                     DataAccessObject.IsNULL<int>(reader["ReferralSourceID"]),
                     DataAccessObject.IsNULL<string>(reader["ReferralSourceName"]),
                     DataAccessObject.IsNULL<string>(reader["ReferralType"]),
                     DataAccessObject.IsNULL<string>(reader["ReferralStyle"]),
                     DataAccessObject.IsNULL<string>(reader["ReferralStation"]),
                     DataAccessObject.IsNULL<int>(reader["MetroLocationID"])
                );
             return referralSource;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ReferralSource  GetReferralSourceByReferralSourceID(int  referralSourceID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetReferralSourceByReferralSourceID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ReferralSourceID", SqlDbType.Int).Value = referralSourceID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetReferralSourceFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertReferralSource(ReferralSource referralSource)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertReferralSource", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReferralSourceID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ReferralSourceName", SqlDbType.NVarChar).Value = referralSource.ReferralSourceName;
            cmd.Parameters.Add("@ReferralType", SqlDbType.NVarChar).Value = referralSource.ReferralType;
            cmd.Parameters.Add("@ReferralStyle", SqlDbType.NVarChar).Value = referralSource.ReferralStyle;
            cmd.Parameters.Add("@ReferralStation", SqlDbType.NVarChar).Value = referralSource.ReferralStation;
            cmd.Parameters.Add("@MetroLocationID", SqlDbType.Int).Value = referralSource.MetroLocationID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ReferralSourceID"].Value;
        }
    }

    public bool UpdateReferralSource(ReferralSource referralSource)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateReferralSource", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReferralSourceID", SqlDbType.Int).Value = referralSource.ReferralSourceID;
            cmd.Parameters.Add("@ReferralSourceName", SqlDbType.NVarChar).Value = referralSource.ReferralSourceName;
            cmd.Parameters.Add("@ReferralType", SqlDbType.NVarChar).Value = referralSource.ReferralType;
            cmd.Parameters.Add("@ReferralStyle", SqlDbType.NVarChar).Value = referralSource.ReferralStyle;
            cmd.Parameters.Add("@ReferralStation", SqlDbType.NVarChar).Value = referralSource.ReferralStation;
            cmd.Parameters.Add("@MetroLocationID", SqlDbType.Int).Value = referralSource.MetroLocationID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteReferralSource(int referralSourceID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteReferralSource", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReferralSourceID", SqlDbType.Int).Value = referralSourceID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

