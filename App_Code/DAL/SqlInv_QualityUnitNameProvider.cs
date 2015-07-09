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

public class SqlInv_QualityUnitNameProvider:DataAccessObject
{
	public SqlInv_QualityUnitNameProvider()
    {
    }


    public bool DeleteInv_QualityUnitName(int inv_QualityUnitNameID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_QualityUnitName", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_QualityUnitNameID", SqlDbType.Int).Value = inv_QualityUnitNameID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_QualityUnitName> GetAllInv_QualityUnitNames()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_QualityUnitNames", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_QualityUnitNamesFromReader(reader);
        }
    }
    public List<Inv_QualityUnitName> GetInv_QualityUnitNamesFromReader(IDataReader reader)
    {
        List<Inv_QualityUnitName> inv_QualityUnitNames = new List<Inv_QualityUnitName>();

        while (reader.Read())
        {
            inv_QualityUnitNames.Add(GetInv_QualityUnitNameFromReader(reader));
        }
        return inv_QualityUnitNames;
    }

    public Inv_QualityUnitName GetInv_QualityUnitNameFromReader(IDataReader reader)
    {
        try
        {
            Inv_QualityUnitName inv_QualityUnitName = new Inv_QualityUnitName
                (
                    (int)reader["Inv_QualityUnitNameID"],
                    reader["QualityUnitName"].ToString(),
                    (int)reader["RowStatusID"]
                );
             return inv_QualityUnitName;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_QualityUnitName GetInv_QualityUnitNameByID(int inv_QualityUnitNameID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_QualityUnitNameByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_QualityUnitNameID", SqlDbType.Int).Value = inv_QualityUnitNameID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_QualityUnitNameFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_QualityUnitName(Inv_QualityUnitName inv_QualityUnitName)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_QualityUnitName", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_QualityUnitNameID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@QualityUnitName", SqlDbType.NVarChar).Value = inv_QualityUnitName.QualityUnitName;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_QualityUnitName.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_QualityUnitNameID"].Value;
        }
    }

    public bool UpdateInv_QualityUnitName(Inv_QualityUnitName inv_QualityUnitName)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_QualityUnitName", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_QualityUnitNameID", SqlDbType.Int).Value = inv_QualityUnitName.Inv_QualityUnitNameID;
            cmd.Parameters.Add("@QualityUnitName", SqlDbType.NVarChar).Value = inv_QualityUnitName.QualityUnitName;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_QualityUnitName.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
