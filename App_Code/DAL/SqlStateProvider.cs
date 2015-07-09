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

public class SqlStateProvider:DataAccessObject
{
	public SqlStateProvider()
    {
    }


    public bool DeleteState(int stateID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteState", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = stateID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<State> GetAllStates()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllStates", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetStatesFromReader(reader);
        }
    }
    public List<State> GetStatesFromReader(IDataReader reader)
    {
        List<State> states = new List<State>();

        while (reader.Read())
        {
            states.Add(GetStateFromReader(reader));
        }
        return states;
    }

    public State GetStateFromReader(IDataReader reader)
    {
        try
        {
            State state = new State
                (
                    (int)reader["StateID"],
                    reader["StateName"].ToString(),
                    (int)reader["CountryID"]
                );
             return state;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public State GetStateByID(int stateID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetStateByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@StateID", SqlDbType.Int).Value = stateID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetStateFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertState(State state)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertState", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = state.StateName;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = state.CountryID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@StateID"].Value;
        }
    }

    public bool UpdateState(State state)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateState", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = state.StateID;
            cmd.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = state.StateName;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = state.CountryID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
