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

public class SqlPos_ProductTypeProvider:DataAccessObject
{
	public SqlPos_ProductTypeProvider()
    {
    }


    public bool DeletePos_ProductType(int pos_ProductTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_ProductType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductTypeID", SqlDbType.Int).Value = pos_ProductTypeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_ProductType> GetAllPos_ProductTypes()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_ProductTypes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_ProductTypesFromReader(reader);
        }
    }
    public List<Pos_ProductType> GetPos_ProductTypesFromReader(IDataReader reader)
    {
        List<Pos_ProductType> pos_ProductTypes = new List<Pos_ProductType>();

        while (reader.Read())
        {
            pos_ProductTypes.Add(GetPos_ProductTypeFromReader(reader));
        }
        return pos_ProductTypes;
    }

    public Pos_ProductType GetPos_ProductTypeFromReader(IDataReader reader)
    {
        try
        {
            Pos_ProductType pos_ProductType = new Pos_ProductType
                (
                    (int)reader["Pos_ProductTypeID"],
                    reader["ProductTypeName"].ToString()
                );
             return pos_ProductType;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_ProductType GetPos_ProductTypeByID(int pos_ProductTypeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_ProductTypeByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_ProductTypeID", SqlDbType.Int).Value = pos_ProductTypeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_ProductTypeFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_ProductType(Pos_ProductType pos_ProductType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_ProductType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductTypeID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ProductTypeName", SqlDbType.NVarChar).Value = pos_ProductType.ProductTypeName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_ProductTypeID"].Value;
        }
    }

    public bool UpdatePos_ProductType(Pos_ProductType pos_ProductType)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_ProductType", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductTypeID", SqlDbType.Int).Value = pos_ProductType.Pos_ProductTypeID;
            cmd.Parameters.Add("@ProductTypeName", SqlDbType.NVarChar).Value = pos_ProductType.ProductTypeName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
