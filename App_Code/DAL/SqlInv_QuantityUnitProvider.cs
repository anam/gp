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

public class SqlInv_QuantityUnitProvider:DataAccessObject
{
	public SqlInv_QuantityUnitProvider()
    {
    }


    public bool DeleteInv_QuantityUnit(int inv_QuantityUnitID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_QuantityUnit", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_QuantityUnitID", SqlDbType.Int).Value = inv_QuantityUnitID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_QuantityUnit> GetAllInv_QuantityUnits()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_QuantityUnits", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_QuantityUnitsFromReader(reader);
        }
    }
    public List<Inv_QuantityUnit> GetInv_QuantityUnitsFromReader(IDataReader reader)
    {
        List<Inv_QuantityUnit> inv_QuantityUnits = new List<Inv_QuantityUnit>();

        while (reader.Read())
        {
            inv_QuantityUnits.Add(GetInv_QuantityUnitFromReader(reader));
        }
        return inv_QuantityUnits;
    }

    public Inv_QuantityUnit GetInv_QuantityUnitFromReader(IDataReader reader)
    {
        try
        {
            Inv_QuantityUnit inv_QuantityUnit = new Inv_QuantityUnit
                (
                    (int)reader["Inv_QuantityUnitID"],
                    reader["QuantityUnitName"].ToString(),
                    (int)reader["RowStatusID"]
                );
             return inv_QuantityUnit;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_QuantityUnit GetInv_QuantityUnitByID(int inv_QuantityUnitID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_QuantityUnitByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_QuantityUnitID", SqlDbType.Int).Value = inv_QuantityUnitID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_QuantityUnitFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_QuantityUnit(Inv_QuantityUnit inv_QuantityUnit)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_QuantityUnit", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_QuantityUnitID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@QuantityUnitName", SqlDbType.NVarChar).Value = inv_QuantityUnit.QuantityUnitName;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_QuantityUnit.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_QuantityUnitID"].Value;
        }
    }

    public bool UpdateInv_QuantityUnit(Inv_QuantityUnit inv_QuantityUnit)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_QuantityUnit", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_QuantityUnitID", SqlDbType.Int).Value = inv_QuantityUnit.Inv_QuantityUnitID;
            cmd.Parameters.Add("@QuantityUnitName", SqlDbType.NVarChar).Value = inv_QuantityUnit.QuantityUnitName;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_QuantityUnit.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
