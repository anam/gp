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

public class SqlAssessmentnCareChildnDateProvider:DataAccessObject
{
	public SqlAssessmentnCareChildnDateProvider()
    {
    }


    public bool DeleteAssessmentnCareChildnDate(int assessmentnCareChildnDateID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteAssessmentnCareChildnDate", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareChildnDateID", SqlDbType.Int).Value = assessmentnCareChildnDateID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<AssessmentnCareChildnDate> GetAllAssessmentnCareChildnDates()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllAssessmentnCareChildnDates", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetAssessmentnCareChildnDatesFromReader(reader);
        }
    }

    public List<AssessmentnCareChildnDate> GetAllAssessmentnCareChildnDatesByAssessmentnCareDateID(int AssessmentnCareDateID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllAssessmentnCareChildnDatesByAssessmentnCareDateID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@AssessmentnCareDateID", SqlDbType.Int).Value = AssessmentnCareDateID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetAssessmentnCareChildnDatesFromReader(reader);
        }
    }

    public List<AssessmentnCareChildnDate> GetAssessmentnCareChildnDatesFromReader(IDataReader reader)
    {
        List<AssessmentnCareChildnDate> assessmentnCareChildnDates = new List<AssessmentnCareChildnDate>();

        while (reader.Read())
        {
            assessmentnCareChildnDates.Add(GetAssessmentnCareChildnDateFromReader(reader));
        }
        return assessmentnCareChildnDates;
    }

    public AssessmentnCareChildnDate GetAssessmentnCareChildnDateFromReader(IDataReader reader)
    {
        try
        {
            AssessmentnCareChildnDate assessmentnCareChildnDate = new AssessmentnCareChildnDate
                (
                    (int)reader["AssessmentnCareChildnDateID"],
                    (int)reader["AssessmentnCareChildID"],
                    (int)reader["AssessmentnCareDateID"]
                );
             return assessmentnCareChildnDate;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public AssessmentnCareChildnDate GetAssessmentnCareChildnDateByID(int assessmentnCareChildnDateID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAssessmentnCareChildnDateByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@AssessmentnCareChildnDateID", SqlDbType.Int).Value = assessmentnCareChildnDateID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetAssessmentnCareChildnDateFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertAssessmentnCareChildnDate(AssessmentnCareChildnDate assessmentnCareChildnDate)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertAssessmentnCareChildnDate", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareChildnDateID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@AssessmentnCareChildID", SqlDbType.Int).Value = assessmentnCareChildnDate.AssessmentnCareChildID;
            cmd.Parameters.Add("@AssessmentnCareDateID", SqlDbType.Int).Value = assessmentnCareChildnDate.AssessmentnCareDateID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@AssessmentnCareChildnDateID"].Value;
        }
    }

    public bool UpdateAssessmentnCareChildnDate(AssessmentnCareChildnDate assessmentnCareChildnDate)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateAssessmentnCareChildnDate", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssessmentnCareChildnDateID", SqlDbType.Int).Value = assessmentnCareChildnDate.AssessmentnCareChildnDateID;
            cmd.Parameters.Add("@AssessmentnCareChildID", SqlDbType.Int).Value = assessmentnCareChildnDate.AssessmentnCareChildID;
            cmd.Parameters.Add("@AssessmentnCareDateID", SqlDbType.Int).Value = assessmentnCareChildnDate.AssessmentnCareDateID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
