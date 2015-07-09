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

public class SqlAssessmentnCareCommentnDateProvider:DataAccessObject
{
	public SqlAssessmentnCareCommentnDateProvider()
    {
    }


    public bool DeleteAssessmentnCareCommentnDate(int assessmentnCareCommentnDateID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteAssessmentnCareCommentnDate", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareCommentnDateID", SqlDbType.Int).Value = assessmentnCareCommentnDateID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<AssessmentnCareCommentnDate> GetAllAssessmentnCareCommentnDates()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllAssessmentnCareCommentnDates", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetAssessmentnCareCommentnDatesFromReader(reader);
        }
    }


    public List<AssessmentnCareCommentnDate> GetAllAssessmentnCareCommentnDatesByAssessmentnCareDateID(int AssessmentnCareDateID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllAssessmentnCareCommentnDatesByAssessmentnCareDateID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@AssessmentnCareDateID", SqlDbType.Int).Value = AssessmentnCareDateID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetAssessmentnCareCommentnDatesFromReader(reader);
        }
    }

    public List<AssessmentnCareCommentnDate> GetAssessmentnCareCommentnDatesFromReader(IDataReader reader)
    {
        List<AssessmentnCareCommentnDate> assessmentnCareCommentnDates = new List<AssessmentnCareCommentnDate>();

        while (reader.Read())
        {
            assessmentnCareCommentnDates.Add(GetAssessmentnCareCommentnDateFromReader(reader));
        }
        return assessmentnCareCommentnDates;
    }

    public AssessmentnCareCommentnDate GetAssessmentnCareCommentnDateFromReader(IDataReader reader)
    {
        try
        {
            AssessmentnCareCommentnDate assessmentnCareCommentnDate = new AssessmentnCareCommentnDate
                (
                    (int)reader["AssessmentnCareCommentnDateID"],
                    (int)reader["AssessmentnCareDateID"],
                    (int)reader["AssessmentnCareParentID"],
                    reader["Comment"].ToString()
                );
             return assessmentnCareCommentnDate;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public AssessmentnCareCommentnDate GetAssessmentnCareCommentnDateByID(int assessmentnCareCommentnDateID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAssessmentnCareCommentnDateByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@AssessmentnCareCommentnDateID", SqlDbType.Int).Value = assessmentnCareCommentnDateID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetAssessmentnCareCommentnDateFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertAssessmentnCareCommentnDate(AssessmentnCareCommentnDate assessmentnCareCommentnDate)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertAssessmentnCareCommentnDate", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareCommentnDateID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@AssessmentnCareDateID", SqlDbType.Int).Value = assessmentnCareCommentnDate.AssessmentnCareDateID;
            cmd.Parameters.Add("@AssessmentnCareParentID", SqlDbType.Int).Value = assessmentnCareCommentnDate.AssessmentnCareParentID;
            cmd.Parameters.Add("@Comment", SqlDbType.NText).Value = assessmentnCareCommentnDate.Comment;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@AssessmentnCareCommentnDateID"].Value;
        }
    }

    public bool UpdateAssessmentnCareCommentnDate(AssessmentnCareCommentnDate assessmentnCareCommentnDate)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateAssessmentnCareCommentnDate", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareCommentnDateID", SqlDbType.Int).Value = assessmentnCareCommentnDate.AssessmentnCareCommentnDateID;
            cmd.Parameters.Add("@AssessmentnCareDateID", SqlDbType.Int).Value = assessmentnCareCommentnDate.AssessmentnCareDateID;
            cmd.Parameters.Add("@AssessmentnCareParentID", SqlDbType.Int).Value = assessmentnCareCommentnDate.AssessmentnCareParentID;
            cmd.Parameters.Add("@Comment", SqlDbType.NText).Value = assessmentnCareCommentnDate.Comment;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
