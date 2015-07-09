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

public class SqlInv_ProductionConfigurationProvider:DataAccessObject
{
	public SqlInv_ProductionConfigurationProvider()
    {
    }


    public bool DeleteInv_ProductionConfiguration(int inv_ProductionConfigurationID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_ProductionConfiguration", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ProductionConfigurationID", SqlDbType.Int).Value = inv_ProductionConfigurationID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_ProductionConfiguration> GetAllInv_ProductionConfigurations()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ProductionConfigurations", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ProductionConfigurationsFromReader(reader);
        }
    }
    public List<Inv_ProductionConfiguration> GetAllInv_ProductionConfigurationsByProductIDnRawmaterialID(int productID, int rawmaterialID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ProductionConfigurationsByProductIDnRawmaterialID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID;
            command.Parameters.Add("@RawmaterialID", SqlDbType.Int).Value = rawmaterialID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ProductionConfigurationsFromReader(reader);
        }
    }

    public List<Inv_ProductionConfiguration> GetAllInv_ProductionConfigurationsByProductIDnItemID(int productID, int itemID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ProductionConfigurationsByProductIDnItemID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID;
            command.Parameters.Add("@ItemID", SqlDbType.Int).Value = itemID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ProductionConfigurationsFromReader(reader);
        }
    }
    public List<Inv_ProductionConfiguration> GetInv_ProductionConfigurationsFromReader(IDataReader reader)
    {
        List<Inv_ProductionConfiguration> inv_ProductionConfigurations = new List<Inv_ProductionConfiguration>();

        while (reader.Read())
        {
            inv_ProductionConfigurations.Add(GetInv_ProductionConfigurationFromReader(reader));
        }
        return inv_ProductionConfigurations;
    }

    public Inv_ProductionConfiguration GetInv_ProductionConfigurationFromReader(IDataReader reader)
    {
        try
        {
            Inv_ProductionConfiguration inv_ProductionConfiguration = new Inv_ProductionConfiguration
                (
                    (int)reader["Inv_ProductionConfigurationID"],
                    (int)reader["ProductID"],
                    (decimal)reader["QualityValue"],
                    (int)reader["QualityUnitID"],
                    (decimal)reader["QuantityValue"],
                    (int)reader["QuantityUnitID"],
                    (int)reader["RawMaterialID"],
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    reader["ExtraField4"].ToString(),
                    reader["ExtraField5"].ToString(),
                    (int)reader["AddedBy"],
                    (DateTime)reader["AddedDate"],
                    (int)reader["UpdatedBy"],
                    (DateTime)reader["UpdatedDate"],
                    (int)reader["RowStatusID"]
                );
             return inv_ProductionConfiguration;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_ProductionConfiguration GetInv_ProductionConfigurationByID(int inv_ProductionConfigurationID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_ProductionConfigurationByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_ProductionConfigurationID", SqlDbType.Int).Value = inv_ProductionConfigurationID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_ProductionConfigurationFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_ProductionConfiguration(Inv_ProductionConfiguration inv_ProductionConfiguration)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_ProductionConfiguration", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ProductionConfigurationID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = inv_ProductionConfiguration.ProductID;
            cmd.Parameters.Add("@QualityValue", SqlDbType.Decimal).Value = inv_ProductionConfiguration.QualityValue;
            cmd.Parameters.Add("@QualityUnitID", SqlDbType.Int).Value = inv_ProductionConfiguration.QualityUnitID;
            cmd.Parameters.Add("@QuantityValue", SqlDbType.Decimal).Value = inv_ProductionConfiguration.QuantityValue;
            cmd.Parameters.Add("@QuantityUnitID", SqlDbType.Int).Value = inv_ProductionConfiguration.QuantityUnitID;
            cmd.Parameters.Add("@RawMaterialID", SqlDbType.Int).Value = inv_ProductionConfiguration.RawMaterialID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_ProductionConfiguration.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_ProductionConfiguration.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_ProductionConfiguration.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_ProductionConfiguration.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_ProductionConfiguration.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_ProductionConfiguration.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_ProductionConfiguration.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_ProductionConfiguration.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_ProductionConfiguration.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_ProductionConfiguration.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_ProductionConfigurationID"].Value;
        }
    }

    public bool UpdateInv_ProductionConfiguration(Inv_ProductionConfiguration inv_ProductionConfiguration)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_ProductionConfiguration", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ProductionConfigurationID", SqlDbType.Int).Value = inv_ProductionConfiguration.Inv_ProductionConfigurationID;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = inv_ProductionConfiguration.ProductID;
            cmd.Parameters.Add("@QualityValue", SqlDbType.Decimal).Value = inv_ProductionConfiguration.QualityValue;
            cmd.Parameters.Add("@QualityUnitID", SqlDbType.Int).Value = inv_ProductionConfiguration.QualityUnitID;
            cmd.Parameters.Add("@QuantityValue", SqlDbType.Decimal).Value = inv_ProductionConfiguration.QuantityValue;
            cmd.Parameters.Add("@QuantityUnitID", SqlDbType.Int).Value = inv_ProductionConfiguration.QuantityUnitID;
            cmd.Parameters.Add("@RawMaterialID", SqlDbType.Int).Value = inv_ProductionConfiguration.RawMaterialID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_ProductionConfiguration.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_ProductionConfiguration.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_ProductionConfiguration.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_ProductionConfiguration.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_ProductionConfiguration.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_ProductionConfiguration.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_ProductionConfiguration.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_ProductionConfiguration.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_ProductionConfiguration.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_ProductionConfiguration.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
