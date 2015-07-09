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

public class SqlPos_CostTypeProvider:DataAccessObject
{
	public SqlPos_CostTypeProvider()
    {
    }


    public bool DeletePos_CostType(int pos_CostTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_CostType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_CostTypeID", SqlDbType.Int).Value = pos_CostTypeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_CostType> GetAllPos_CostTypes()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_CostTypes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_CostTypesFromReader(reader);
        }
    }
    public List<Pos_CostType> GetPos_CostTypesFromReader(IDataReader reader)
    {
        List<Pos_CostType> pos_CostTypes = new List<Pos_CostType>();

        while (reader.Read())
        {
            pos_CostTypes.Add(GetPos_CostTypeFromReader(reader));
        }
        return pos_CostTypes;
    }

    public Pos_CostType GetPos_CostTypeFromReader(IDataReader reader)
    {
        try
        {
            Pos_CostType pos_CostType = new Pos_CostType
                (
                    (int)reader["Pos_CostTypeID"],
                    reader["CostTypeName"].ToString()
                );
             return pos_CostType;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_CostType GetPos_CostTypeByID(int pos_CostTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_CostTypeByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_CostTypeID", SqlDbType.Int).Value = pos_CostTypeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_CostTypeFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_CostType(Pos_CostType pos_CostType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_CostType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_CostTypeID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@CostTypeName", SqlDbType.NVarChar).Value = pos_CostType.CostTypeName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_CostTypeID"].Value;
        }
    }

    public bool UpdatePos_CostType(Pos_CostType pos_CostType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_CostType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_CostTypeID", SqlDbType.Int).Value = pos_CostType.Pos_CostTypeID;
            cmd.Parameters.Add("@CostTypeName", SqlDbType.NVarChar).Value = pos_CostType.CostTypeName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
