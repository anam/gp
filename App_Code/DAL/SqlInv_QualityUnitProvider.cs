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

public class SqlInv_QualityUnitProvider:DataAccessObject
{
	public SqlInv_QualityUnitProvider()
    {
    }


    public bool DeleteInv_QualityUnit(int inv_QualityUnitID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_QualityUnit", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_QualityUnitID", SqlDbType.Int).Value = inv_QualityUnitID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_QualityUnit> GetAllInv_QualityUnits()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_QualityUnits", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_QualityUnitsFromReader(reader);
        }
    }
    public List<Inv_QualityUnit> GetInv_QualityUnitsFromReader(IDataReader reader)
    {
        List<Inv_QualityUnit> inv_QualityUnits = new List<Inv_QualityUnit>();

        while (reader.Read())
        {
            inv_QualityUnits.Add(GetInv_QualityUnitFromReader(reader));
        }
        return inv_QualityUnits;
    }

    public Inv_QualityUnit GetInv_QualityUnitFromReader(IDataReader reader)
    {
        try
        {
            Inv_QualityUnit inv_QualityUnit = new Inv_QualityUnit
                (
                    (int)reader["Inv_QualityUnitID"],
                    reader["QualityUnitName"].ToString(),
                    (int)reader["RowStatusID"]
                );
             return inv_QualityUnit;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_QualityUnit GetInv_QualityUnitByID(int inv_QualityUnitID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_QualityUnitByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_QualityUnitID", SqlDbType.Int).Value = inv_QualityUnitID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_QualityUnitFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_QualityUnit(Inv_QualityUnit inv_QualityUnit)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_QualityUnit", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_QualityUnitID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@QualityUnitName", SqlDbType.NVarChar).Value = inv_QualityUnit.QualityUnitName;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_QualityUnit.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_QualityUnitID"].Value;
        }
    }

    public bool UpdateInv_QualityUnit(Inv_QualityUnit inv_QualityUnit)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_QualityUnit", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_QualityUnitID", SqlDbType.Int).Value = inv_QualityUnit.Inv_QualityUnitID;
            cmd.Parameters.Add("@QualityUnitName", SqlDbType.NVarChar).Value = inv_QualityUnit.QualityUnitName;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_QualityUnit.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
