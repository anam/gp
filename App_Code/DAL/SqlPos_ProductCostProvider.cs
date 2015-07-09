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

public class SqlPos_ProductCostProvider:DataAccessObject
{
	public SqlPos_ProductCostProvider()
    {
    }


    public bool DeletePos_ProductCost(int pos_ProductCostID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_ProductCost", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductCostID", SqlDbType.Int).Value = pos_ProductCostID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_ProductCost> GetAllPos_ProductCosts()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_ProductCosts", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_ProductCostsFromReader(reader);
        }
    }
    public List<Pos_ProductCost> GetPos_ProductCostsFromReader(IDataReader reader)
    {
        List<Pos_ProductCost> pos_ProductCosts = new List<Pos_ProductCost>();

        while (reader.Read())
        {
            pos_ProductCosts.Add(GetPos_ProductCostFromReader(reader));
        }
        return pos_ProductCosts;
    }

    public Pos_ProductCost GetPos_ProductCostFromReader(IDataReader reader)
    {
        try
        {
            Pos_ProductCost pos_ProductCost = new Pos_ProductCost
                (
                    (int)reader["Pos_ProductCostID"],
                    (int)reader["Pos_CostTypeID"],
                    (int)reader["ProductID"],
                    (decimal)reader["Amount"],
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString()
                );
             return pos_ProductCost;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_ProductCost GetPos_ProductCostByID(int pos_ProductCostID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_ProductCostByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_ProductCostID", SqlDbType.Int).Value = pos_ProductCostID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_ProductCostFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_ProductCost(Pos_ProductCost pos_ProductCost)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_ProductCost", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductCostID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Pos_CostTypeID", SqlDbType.Int).Value = pos_ProductCost.Pos_CostTypeID;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = pos_ProductCost.ProductID;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = pos_ProductCost.Amount;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_ProductCost.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_ProductCost.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_ProductCost.ExtraField3;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_ProductCostID"].Value;
        }
    }

    public bool UpdatePos_ProductCost(Pos_ProductCost pos_ProductCost)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_ProductCost", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductCostID", SqlDbType.Int).Value = pos_ProductCost.Pos_ProductCostID;
            cmd.Parameters.Add("@Pos_CostTypeID", SqlDbType.Int).Value = pos_ProductCost.Pos_CostTypeID;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = pos_ProductCost.ProductID;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = pos_ProductCost.Amount;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_ProductCost.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_ProductCost.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_ProductCost.ExtraField3;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
