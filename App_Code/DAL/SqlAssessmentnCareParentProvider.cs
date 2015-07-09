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

public class SqlAssessmentnCareParentProvider:DataAccessObject
{
	public SqlAssessmentnCareParentProvider()
    {
    }


    public bool DeleteAssessmentnCareParent(int assessmentnCareParentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteAssessmentnCareParent", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareParentID", SqlDbType.Int).Value = assessmentnCareParentID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<AssessmentnCareParent> GetAllAssessmentnCareParents()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllAssessmentnCareParents", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetAssessmentnCareParentsFromReader(reader);
        }
    }

    public List<AssessmentnCareParent> GetAllAssessmentnCareParentsByAssessmentnCareDateIDID(int AssessmentnCareDateIDID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllAssessmentnCareParentsByAssessmentnCareDateIDID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@AssessmentnCareDateIDID", SqlDbType.Int).Value = AssessmentnCareDateIDID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetAssessmentnCareParentsFromReader(reader);
        }
    }

    public List<AssessmentnCareParent> GetAssessmentnCareParentsFromReader(IDataReader reader)
    {
        List<AssessmentnCareParent> assessmentnCareParents = new List<AssessmentnCareParent>();

        while (reader.Read())
        {
            assessmentnCareParents.Add(GetAssessmentnCareParentFromReader(reader));
        }
        return assessmentnCareParents;
    }

    public AssessmentnCareParent GetAssessmentnCareParentFromReader(IDataReader reader)
    {
        try
        {
            AssessmentnCareParent assessmentnCareParent = new AssessmentnCareParent
                (
                    (int)reader["AssessmentnCareParentID"],
                    reader["AssessmentnCareParentName"].ToString()
                );
            
             return assessmentnCareParent;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public AssessmentnCareParent GetAssessmentnCareParentByID(int assessmentnCareParentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAssessmentnCareParentByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@AssessmentnCareParentID", SqlDbType.Int).Value = assessmentnCareParentID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetAssessmentnCareParentFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertAssessmentnCareParent(AssessmentnCareParent assessmentnCareParent)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertAssessmentnCareParent", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareParentID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@AssessmentnCareParentName", SqlDbType.NVarChar).Value = assessmentnCareParent.AssessmentnCareParentName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@AssessmentnCareParentID"].Value;
        }
    }

    public bool UpdateAssessmentnCareParent(AssessmentnCareParent assessmentnCareParent)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateAssessmentnCareParent", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareParentID", SqlDbType.Int).Value = assessmentnCareParent.AssessmentnCareParentID;
            cmd.Parameters.Add("@AssessmentnCareParentName", SqlDbType.NVarChar).Value = assessmentnCareParent.AssessmentnCareParentName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
