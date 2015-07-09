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

public class SqlAssessmentnCareDateIDProvider:DataAccessObject
{
	public SqlAssessmentnCareDateIDProvider()
    {
    }


    public bool DeleteAssessmentnCareDateID(int assessmentnCareDateIDID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteAssessmentnCareDateID", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareDateIDID", SqlDbType.Int).Value = assessmentnCareDateIDID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<AssessmentnCareDate> GetAllAssessmentnCareDateIDs()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllAssessmentnCareDateIDs", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetAssessmentnCareDateIDsFromReader(reader);
        }
    }

    public List<AssessmentnCareDate> GetAllAssessmentnCareDateIDsByResidentID(int ResidentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllAssessmentnCareDateIDsByResidentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = ResidentID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetAssessmentnCareDateIDsFromReader(reader);
        }
    }

    public List<AssessmentnCareDate> GetAssessmentnCareDateIDsFromReader(IDataReader reader)
    {
        List<AssessmentnCareDate> assessmentnCareDateIDs = new List<AssessmentnCareDate>();

        while (reader.Read())
        {
            assessmentnCareDateIDs.Add(GetAssessmentnCareDateIDFromReader(reader));
        }
        return assessmentnCareDateIDs;
    }

    public AssessmentnCareDate GetAssessmentnCareDateIDFromReader(IDataReader reader)
    {
        try
        {
            AssessmentnCareDate assessmentnCareDateID = new AssessmentnCareDate
                (
                    (int)reader["AssessmentnCareDateIDID"],
                    (DateTime)reader["AssessmentnCareDate"],
                    (int)reader["ResidentID"],
                    (int)reader["AddedBy"],
                    (DateTime)reader["AddedDate"],
                    (int)reader["UpdatedBy"],
                    (DateTime)reader["UpdatedDate"]
                );
             return assessmentnCareDateID;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public AssessmentnCareDate GetAssessmentnCareDateIDByID(int assessmentnCareDateIDID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAssessmentnCareDateIDByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@AssessmentnCareDateIDID", SqlDbType.Int).Value = assessmentnCareDateIDID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetAssessmentnCareDateIDFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertAssessmentnCareDateID(AssessmentnCareDate assessmentnCareDateID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertAssessmentnCareDateID", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareDateIDID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@AssessmentnCareDate", SqlDbType.DateTime).Value = assessmentnCareDateID.AssessmentnCareDateName;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = assessmentnCareDateID.ResidentID;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = assessmentnCareDateID.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = assessmentnCareDateID.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = assessmentnCareDateID.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = assessmentnCareDateID.UpdatedDate;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@AssessmentnCareDateIDID"].Value;
        }
    }

    public bool ProcessAssessmentnCareDateID(AssessmentnCareDate assessmentnCareDateID, string ChiledIds, string CommentsByParentIds)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_ProcessAssessmentnCareDateID", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareDateIDID", SqlDbType.Int).Value = assessmentnCareDateID.AssessmentnCareDateIDID;
            cmd.Parameters.Add("@AssessmentnCareDate", SqlDbType.DateTime).Value = assessmentnCareDateID.AssessmentnCareDateName;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = assessmentnCareDateID.ResidentID;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = assessmentnCareDateID.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = assessmentnCareDateID.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = assessmentnCareDateID.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = assessmentnCareDateID.UpdatedDate;
            cmd.Parameters.Add("@ChiledIds", SqlDbType.NVarChar).Value = ChiledIds;
            cmd.Parameters.Add("@CommentsByParentIds", SqlDbType.NVarChar).Value = CommentsByParentIds;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool UpdateAssessmentnCareDateID(AssessmentnCareDate assessmentnCareDateID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateAssessmentnCareDateID", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareDateIDID", SqlDbType.Int).Value = assessmentnCareDateID.AssessmentnCareDateIDID;
            cmd.Parameters.Add("@AssessmentnCareDate", SqlDbType.DateTime).Value = assessmentnCareDateID.AssessmentnCareDateName;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = assessmentnCareDateID.ResidentID;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = assessmentnCareDateID.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = assessmentnCareDateID.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = assessmentnCareDateID.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = assessmentnCareDateID.UpdatedDate;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
