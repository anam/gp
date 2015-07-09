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

public class SqlPos_FabricsTypeProvider:DataAccessObject
{
	public SqlPos_FabricsTypeProvider()
    {
    }


    public bool DeletePos_FabricsType(int pos_FabricsTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_FabricsType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_FabricsTypeID", SqlDbType.Int).Value = pos_FabricsTypeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_FabricsType> GetAllPos_FabricsTypes()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_FabricsTypes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_FabricsTypesFromReader(reader);
        }
    }
    public List<Pos_FabricsType> GetPos_FabricsTypesFromReader(IDataReader reader)
    {
        List<Pos_FabricsType> pos_FabricsTypes = new List<Pos_FabricsType>();

        while (reader.Read())
        {
            pos_FabricsTypes.Add(GetPos_FabricsTypeFromReader(reader));
        }
        return pos_FabricsTypes;
    }

    public Pos_FabricsType GetPos_FabricsTypeFromReader(IDataReader reader)
    {
        try
        {
            Pos_FabricsType pos_FabricsType = new Pos_FabricsType
                (
                    (int)reader["Pos_FabricsTypeID"],
                    reader["FabricsTypeName"].ToString()
                );
             return pos_FabricsType;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_FabricsType GetPos_FabricsTypeByID(int pos_FabricsTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_FabricsTypeByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_FabricsTypeID", SqlDbType.Int).Value = pos_FabricsTypeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_FabricsTypeFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_FabricsType(Pos_FabricsType pos_FabricsType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_FabricsType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_FabricsTypeID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@FabricsTypeName", SqlDbType.NVarChar).Value = pos_FabricsType.FabricsTypeName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_FabricsTypeID"].Value;
        }
    }

    public bool UpdatePos_FabricsType(Pos_FabricsType pos_FabricsType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_FabricsType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_FabricsTypeID", SqlDbType.Int).Value = pos_FabricsType.Pos_FabricsTypeID;
            cmd.Parameters.Add("@FabricsTypeName", SqlDbType.NVarChar).Value = pos_FabricsType.FabricsTypeName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
