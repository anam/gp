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

public class SqlADLDetailnStatusProvider:DataAccessObject
{
	public SqlADLDetailnStatusProvider()
    {
    }


    public bool DeleteADLDetailnStatus(int aDLDetailnStatusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteADLDetailnStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLDetailnStatusID", SqlDbType.Int).Value = aDLDetailnStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<ADLDetailnStatus> GetAllADLDetailnStatuss()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllADLDetailnStatuss", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetADLDetailnStatussFromReader(reader);
        }
    }

    public List<ADLDetailnStatus> GetAllADLDetailnStatussByADLDetailID(int ADLDetailID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllADLDetailnStatussByADLDetailID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ADLDetailID", SqlDbType.Int).Value = ADLDetailID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetADLDetailnStatussFromReader(reader);
        }
    }

    public List<ADLDetailnStatus> GetADLDetailnStatussFromReader(IDataReader reader)
    {
        List<ADLDetailnStatus> aDLDetailnStatuss = new List<ADLDetailnStatus>();

        while (reader.Read())
        {
            aDLDetailnStatuss.Add(GetADLDetailnStatusFromReader(reader));
        }
        return aDLDetailnStatuss;
    }

    public ADLDetailnStatus GetADLDetailnStatusFromReader(IDataReader reader)
    {
        try
        {
            ADLDetailnStatus aDLDetailnStatus = new ADLDetailnStatus
                (
                    (int)reader["ADLDetailnStatusID"],
                    (int)reader["ADLDetailID"],
                    (int)reader["ADLStatusID"]
                );
            try
            {
                aDLDetailnStatus.ADLStatusName = reader["ADLStatusName"].ToString();
            }
            catch(Exception ex)
            {
                
            }
             return aDLDetailnStatus;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public ADLDetailnStatus GetADLDetailnStatusByID(int aDLDetailnStatusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetADLDetailnStatusByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ADLDetailnStatusID", SqlDbType.Int).Value = aDLDetailnStatusID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetADLDetailnStatusFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertADLDetailnStatus(ADLDetailnStatus aDLDetailnStatus)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertADLDetailnStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLDetailnStatusID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ADLDetailID", SqlDbType.Int).Value = aDLDetailnStatus.ADLDetailID;
            cmd.Parameters.Add("@ADLStatusID", SqlDbType.Int).Value = aDLDetailnStatus.ADLStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ADLDetailnStatusID"].Value;
        }
    }

    public bool UpdateADLDetailnStatus(ADLDetailnStatus aDLDetailnStatus)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateADLDetailnStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ADLDetailnStatusID", SqlDbType.Int).Value = aDLDetailnStatus.ADLDetailnStatusID;
            cmd.Parameters.Add("@ADLDetailID", SqlDbType.Int).Value = aDLDetailnStatus.ADLDetailID;
            cmd.Parameters.Add("@ADLStatusID", SqlDbType.Int).Value = aDLDetailnStatus.ADLStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
