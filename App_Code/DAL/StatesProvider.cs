using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlStatesProvider:DataAccessObject
{
	public SqlStatesProvider()
    {
    }


    public DataSet  GetAllStatess()
    {
        DataSet Statess = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllStatess", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Statess);
            myadapter.Dispose();
            connection.Close();

            return Statess;
        }
    }
	public DataSet GetStatesPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetStatesPageWise", connection))
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


    public DataSet  GetDropDownLisAllStates()
    {
        DataSet Statess = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllStates", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Statess);
            myadapter.Dispose();
            connection.Close();

            return Statess;
        }
    }
    public List<States> GetStatessFromReader(IDataReader reader)
    {
        List<States> statess = new List<States>();

        while (reader.Read())
        {
            statess.Add(GetStatesFromReader(reader));
        }
        return statess;
    }

    public States GetStatesFromReader(IDataReader reader)
    {
        try
        {
            States states = new States
                (

                     DataAccessObject.IsNULL<int>(reader["StateID"]),
                     DataAccessObject.IsNULL<string>(reader["StateNameFullName"]),
                     DataAccessObject.IsNULL<string>(reader["StateNameShortName"])
                );
             return states;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public States  GetStatesByStateID(int  stateID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetStatesByStateID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@StateID", SqlDbType.Int).Value = stateID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetStatesFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertStates(States states)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertStates", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@StateNameFullName", SqlDbType.NVarChar).Value = states.StateNameFullName;
            cmd.Parameters.Add("@StateNameShortName", SqlDbType.NChar).Value = states.StateNameShortName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@StateID"].Value;
        }
    }

    public bool UpdateStates(States states)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateStates", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = states.StateID;
            cmd.Parameters.Add("@StateNameFullName", SqlDbType.NVarChar).Value = states.StateNameFullName;
            cmd.Parameters.Add("@StateNameShortName", SqlDbType.NChar).Value = states.StateNameShortName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteStates(int stateID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteStates", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = stateID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

