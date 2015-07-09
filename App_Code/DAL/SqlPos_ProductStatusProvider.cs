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

public class SqlPos_ProductStatusProvider:DataAccessObject
{
	public SqlPos_ProductStatusProvider()
    {
    }


    public bool DeletePos_ProductStatus(int pos_ProductStatusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_ProductStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductStatusID", SqlDbType.Int).Value = pos_ProductStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_ProductStatus> GetAllPos_ProductStatuss()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_ProductStatuss", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_ProductStatussFromReader(reader);
        }
    }
    public List<Pos_ProductStatus> GetPos_ProductStatussFromReader(IDataReader reader)
    {
        List<Pos_ProductStatus> pos_ProductStatuss = new List<Pos_ProductStatus>();

        while (reader.Read())
        {
            pos_ProductStatuss.Add(GetPos_ProductStatusFromReader(reader));
        }
        return pos_ProductStatuss;
    }

    public Pos_ProductStatus GetPos_ProductStatusFromReader(IDataReader reader)
    {
        try
        {
            Pos_ProductStatus pos_ProductStatus = new Pos_ProductStatus
                (
                    (int)reader["Pos_ProductStatusID"],
                    reader["ProductStatusName"].ToString()
                );
             return pos_ProductStatus;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_ProductStatus GetPos_ProductStatusByID(int pos_ProductStatusID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_ProductStatusByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_ProductStatusID", SqlDbType.Int).Value = pos_ProductStatusID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_ProductStatusFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_ProductStatus(Pos_ProductStatus pos_ProductStatus)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_ProductStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductStatusID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ProductStatusName", SqlDbType.NVarChar).Value = pos_ProductStatus.ProductStatusName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_ProductStatusID"].Value;
        }
    }

    public bool UpdatePos_ProductStatus(Pos_ProductStatus pos_ProductStatus)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_ProductStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductStatusID", SqlDbType.Int).Value = pos_ProductStatus.Pos_ProductStatusID;
            cmd.Parameters.Add("@ProductStatusName", SqlDbType.NVarChar).Value = pos_ProductStatus.ProductStatusName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
