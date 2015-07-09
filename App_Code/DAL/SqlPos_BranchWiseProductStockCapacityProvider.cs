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

public class SqlPos_BranchWiseProductStockCapacityProvider:DataAccessObject
{
	public SqlPos_BranchWiseProductStockCapacityProvider()
    {
    }


    public bool DeletePos_BranchWiseProductStockCapacity(int pos_BranchWiseProductStockCapacityID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_BranchWiseProductStockCapacity", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_BranchWiseProductStockCapacityID", SqlDbType.Int).Value = pos_BranchWiseProductStockCapacityID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_BranchWiseProductStockCapacity> GetAllPos_BranchWiseProductStockCapacities()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_BranchWiseProductStockCapacities", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_BranchWiseProductStockCapacitiesFromReader(reader);
        }
    }
    public List<Pos_BranchWiseProductStockCapacity> GetPos_BranchWiseProductStockCapacitiesFromReader(IDataReader reader)
    {
        List<Pos_BranchWiseProductStockCapacity> pos_BranchWiseProductStockCapacities = new List<Pos_BranchWiseProductStockCapacity>();

        while (reader.Read())
        {
            pos_BranchWiseProductStockCapacities.Add(GetPos_BranchWiseProductStockCapacityFromReader(reader));
        }
        return pos_BranchWiseProductStockCapacities;
    }

    public Pos_BranchWiseProductStockCapacity GetPos_BranchWiseProductStockCapacityFromReader(IDataReader reader)
    {
        try
        {
            Pos_BranchWiseProductStockCapacity pos_BranchWiseProductStockCapacity = new Pos_BranchWiseProductStockCapacity
                (
                    (int)reader["Pos_BranchWiseProductStockCapacityID"],
                    (int)reader["ProductID"],
                    (int)reader["WorkStationID"],
                    (long)reader["StockAmount"]
                );
             return pos_BranchWiseProductStockCapacity;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_BranchWiseProductStockCapacity GetPos_BranchWiseProductStockCapacityByID(int pos_BranchWiseProductStockCapacityID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_BranchWiseProductStockCapacityByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_BranchWiseProductStockCapacityID", SqlDbType.Int).Value = pos_BranchWiseProductStockCapacityID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_BranchWiseProductStockCapacityFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_BranchWiseProductStockCapacity(Pos_BranchWiseProductStockCapacity pos_BranchWiseProductStockCapacity)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_BranchWiseProductStockCapacity", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_BranchWiseProductStockCapacityID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = pos_BranchWiseProductStockCapacity.ProductID;
            cmd.Parameters.Add("@WorkStationID", SqlDbType.Int).Value = pos_BranchWiseProductStockCapacity.WorkStationID;
            cmd.Parameters.Add("@StockAmount", SqlDbType.BigInt).Value = pos_BranchWiseProductStockCapacity.StockAmount;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_BranchWiseProductStockCapacityID"].Value;
        }
    }

    public bool UpdatePos_BranchWiseProductStockCapacity(Pos_BranchWiseProductStockCapacity pos_BranchWiseProductStockCapacity)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_BranchWiseProductStockCapacity", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_BranchWiseProductStockCapacityID", SqlDbType.Int).Value = pos_BranchWiseProductStockCapacity.Pos_BranchWiseProductStockCapacityID;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = pos_BranchWiseProductStockCapacity.ProductID;
            cmd.Parameters.Add("@WorkStationID", SqlDbType.Int).Value = pos_BranchWiseProductStockCapacity.WorkStationID;
            cmd.Parameters.Add("@StockAmount", SqlDbType.BigInt).Value = pos_BranchWiseProductStockCapacity.StockAmount;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
