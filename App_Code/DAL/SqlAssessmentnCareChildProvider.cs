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

public class SqlAssessmentnCareChildProvider:DataAccessObject
{
	public SqlAssessmentnCareChildProvider()
    {
    }


    public bool DeleteAssessmentnCareChild(int assessmentnCareChildID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteAssessmentnCareChild", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareChildID", SqlDbType.Int).Value = assessmentnCareChildID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<AssessmentnCareChild> GetAllAssessmentnCareChilds()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllAssessmentnCareChilds", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetAssessmentnCareChildsFromReader(reader);
        }
    }


    public List<AssessmentnCareChild> GetAllAssessmentnCareChildsByAssessmentnCareDateIDID(int AssessmentnCareDateIDID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllAssessmentnCareChildsByAssessmentnCareDateIDID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@AssessmentnCareDateIDID", SqlDbType.Int).Value = AssessmentnCareDateIDID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetAssessmentnCareChildsFromReader(reader);
        }
    }

    public List<AssessmentnCareChild> GetAssessmentnCareChildsFromReader(IDataReader reader)
    {
        List<AssessmentnCareChild> assessmentnCareChilds = new List<AssessmentnCareChild>();

        while (reader.Read())
        {
            assessmentnCareChilds.Add(GetAssessmentnCareChildFromReader(reader));
        }
        return assessmentnCareChilds;
    }

    public AssessmentnCareChild GetAssessmentnCareChildFromReader(IDataReader reader)
    {
        try
        {
            AssessmentnCareChild assessmentnCareChild = new AssessmentnCareChild
                (
                    (int)reader["AssessmentnCareChildID"],
                    (int)reader["AssessmentnCareParentID"],
                    reader["AssessmentnCareChildName"].ToString()
                );
            try
            {
                assessmentnCareChild.AssessmentnCareParentName = reader["AssessmentnCareParentName"].ToString();

            }
            catch (Exception ex)
            {
            }

             return assessmentnCareChild;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public AssessmentnCareChild GetAssessmentnCareChildByID(int assessmentnCareChildID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAssessmentnCareChildByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@AssessmentnCareChildID", SqlDbType.Int).Value = assessmentnCareChildID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetAssessmentnCareChildFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertAssessmentnCareChild(AssessmentnCareChild assessmentnCareChild)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertAssessmentnCareChild", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareChildID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@AssessmentnCareParentID", SqlDbType.Int).Value = assessmentnCareChild.AssessmentnCareParentID;
            cmd.Parameters.Add("@AssessmentnCareChildName", SqlDbType.NVarChar).Value = assessmentnCareChild.AssessmentnCareChildName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@AssessmentnCareChildID"].Value;
        }
    }

    public bool UpdateAssessmentnCareChild(AssessmentnCareChild assessmentnCareChild)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateAssessmentnCareChild", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareChildID", SqlDbType.Int).Value = assessmentnCareChild.AssessmentnCareChildID;
            cmd.Parameters.Add("@AssessmentnCareParentID", SqlDbType.Int).Value = assessmentnCareChild.AssessmentnCareParentID;
            cmd.Parameters.Add("@AssessmentnCareChildName", SqlDbType.NVarChar).Value = assessmentnCareChild.AssessmentnCareChildName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
