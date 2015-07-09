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

public class SqlInv_ItemProvider:DataAccessObject
{
	public SqlInv_ItemProvider()
    {
    }


    public bool DeleteInv_Item(int inv_ItemID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_ItemSingle", connection);
            // SqlCommand cmd = new SqlCommand("GP_DeleteInv_Item", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ItemID", SqlDbType.Int).Value = inv_ItemID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }


    public List<Inv_Item> GetAllInv_ItemsByPurchaseID(int purchaseID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ItemsByPurchaseID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@PurchaseID", SqlDbType.Int).Value = purchaseID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ItemsFromReader(reader);
        }
    }



    public List<Inv_Item> GetAllInv_ItemsByPurchaseReturnID(int returnID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ItemsByPurchaseReturnID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ReturnID", SqlDbType.Decimal).Value = decimal.Parse(returnID.ToString());
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ItemsFromReader(reader);
        }
    }



    public List<Inv_Item> GetAllInv_ItemsByAdjustmentID(int adjustmentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ItemsByAdjustmentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@AdjustmentID", SqlDbType.Decimal).Value = decimal.Parse(adjustmentID.ToString());
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ItemsFromReader(reader);
        }
    }

    public List<Inv_Item> GetAllInv_ItemsByUtilizationID(int adjustmentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ItemsByUtilizationID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@UtilizationID", SqlDbType.Decimal).Value = decimal.Parse(adjustmentID.ToString());
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ItemsFromReader(reader);
        }
    }

    public List<Inv_Item> GetAllInv_ItemsByWastageID(int adjustmentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ItemsByWastageID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@WastageID", SqlDbType.Decimal).Value = decimal.Parse(adjustmentID.ToString());
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ItemsFromReader(reader);
        }
    }


    public List<Inv_Item> GetAllInv_Items()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_Items", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ItemsFromReader(reader);
        }
    }


    public List<Inv_Item> GetAllInv_ItemsInStock()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ItemsInStore", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ItemsFromReader(reader);
        }
    }


    public List<Inv_Item> GetAllInv_ItemsByIDs(string ids)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ItemsByIDs", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@IDs", SqlDbType.NVarChar).Value = ids;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ItemsFromReader(reader);
        }
    }


    public List<Inv_Item> GetAllInv_ItemsInStockSupplierID(int supplierID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_ItemsBySuppierID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = supplierID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_ItemsFromReader(reader);
        }
    }


    public List<Inv_Item> GetInv_ItemsFromReader(IDataReader reader)
    {
        List<Inv_Item> inv_Items = new List<Inv_Item>();

        while (reader.Read())
        {
            inv_Items.Add(GetInv_ItemFromReader(reader));
        }
        return inv_Items;
    }

    public Inv_Item GetInv_ItemFromReader(IDataReader reader)
    {
        try
        {
            Inv_Item inv_Item = new Inv_Item
                (
                    (int)reader["Inv_ItemID"],
                    reader["ItemName"].ToString(),
                    (int)reader["PurchaseID"],
                    reader["ItemCode"].ToString(),
                    (int)reader["RawMaterialID"],
                    (int)reader["StoreID"],
                    (int)reader["QualityUnitID"],
                    (decimal)reader["QualityValue"],
                    (int)reader["QuantityUnitID"],
                    (decimal)reader["PricePerUnit"],
                    (decimal)reader["PurchasedQuantity"],
                    (decimal)reader["IssueReturedQuantity"],
                    (decimal)reader["UtilizedQuantity"],
                    (decimal)reader["LostQuantity"],
                    (decimal)reader["ExtraFieldQuantity1"],
                    (decimal)reader["ExtraFieldQuantity2"],
                    (decimal)reader["ExtraFieldQuantity3"],
                    (decimal)reader["ExtraFieldQuantity4"],
                    (decimal)reader["ExtraFieldQuantity5"],
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    reader["ExtraField4"].ToString(),
                    reader["ExtraField5"].ToString(),
                    reader["ExtraField6"].ToString(),
                    reader["ExtraField7"].ToString(),
                    reader["ExtraField8"].ToString(),
                    reader["ExtraField9"].ToString(),
                    reader["ExtraField10"].ToString(),
                    (int)reader["AddedBy"],
                    (DateTime)reader["AddedDate"],
                    (int)reader["UpdatedBy"],
                    (DateTime)reader["UpdatedDate"],
                    (int)reader["RowStatusID"]
                );

            try { inv_Item.QualityUnitName = inv_Item.ExtraField1; }
            catch (Exception) { }

            try { inv_Item.PurchasedQuantityPrice =( inv_Item.PricePerUnit * inv_Item.PurchasedQuantity); }
            catch (Exception) { }

            try { inv_Item.QuantityUnitName = inv_Item.ExtraField2; }
            catch (Exception) { }

            try { inv_Item.RawMaterialName = reader["RawMaterialName"].ToString(); }
            catch (Exception) { }

            try { inv_Item.RawMaterialTypeID = (int)reader["ACC_HeadTypeID"]; }
            catch (Exception) { inv_Item.RawMaterialTypeID = 0; }

            try { inv_Item.RawMaterialTypeName = reader["HeadTypeName"].ToString(); }
            catch (Exception) { inv_Item.RawMaterialTypeName = ""; }

             return inv_Item;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_Item GetInv_ItemByID(int inv_ItemID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_ItemByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_ItemID", SqlDbType.Int).Value = inv_ItemID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_ItemFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_Item(Inv_Item inv_Item)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_Item", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ItemID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ItemName", SqlDbType.NVarChar).Value = inv_Item.ItemName;
            cmd.Parameters.Add("@PurchaseID", SqlDbType.Int).Value = inv_Item.PurchaseID;
            cmd.Parameters.Add("@ItemCode", SqlDbType.NChar).Value = inv_Item.ItemCode;
            cmd.Parameters.Add("@RawMaterialID", SqlDbType.Int).Value = inv_Item.RawMaterialID;
            cmd.Parameters.Add("@StoreID", SqlDbType.Int).Value = inv_Item.StoreID;
            cmd.Parameters.Add("@QualityUnitID", SqlDbType.Int).Value = inv_Item.QualityUnitID;
            cmd.Parameters.Add("@QualityValue", SqlDbType.Decimal).Value = inv_Item.QualityValue;
            cmd.Parameters.Add("@QuantityUnitID", SqlDbType.Int).Value = inv_Item.QuantityUnitID;
            cmd.Parameters.Add("@PricePerUnit", SqlDbType.Decimal).Value = inv_Item.PricePerUnit;
            cmd.Parameters.Add("@PurchasedQuantity", SqlDbType.Decimal).Value = inv_Item.PurchasedQuantity;
            cmd.Parameters.Add("@IssueReturedQuantity", SqlDbType.Decimal).Value = inv_Item.IssueReturedQuantity;
            cmd.Parameters.Add("@UtilizedQuantity", SqlDbType.Decimal).Value = inv_Item.UtilizedQuantity;
            cmd.Parameters.Add("@LostQuantity", SqlDbType.Decimal).Value = inv_Item.LostQuantity;
            cmd.Parameters.Add("@ExtraFieldQuantity1", SqlDbType.Decimal).Value = inv_Item.ExtraFieldQuantity1;
            cmd.Parameters.Add("@ExtraFieldQuantity2", SqlDbType.Decimal).Value = inv_Item.ExtraFieldQuantity2;
            cmd.Parameters.Add("@ExtraFieldQuantity3", SqlDbType.Decimal).Value = inv_Item.ExtraFieldQuantity3;
            cmd.Parameters.Add("@ExtraFieldQuantity4", SqlDbType.Decimal).Value = inv_Item.ExtraFieldQuantity4;
            cmd.Parameters.Add("@ExtraFieldQuantity5", SqlDbType.Decimal).Value = inv_Item.ExtraFieldQuantity5;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_Item.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_Item.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_Item.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_Item.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_Item.ExtraField5;
            cmd.Parameters.Add("@ExtraField6", SqlDbType.NVarChar).Value = inv_Item.ExtraField6;
            cmd.Parameters.Add("@ExtraField7", SqlDbType.NVarChar).Value = inv_Item.ExtraField7;
            cmd.Parameters.Add("@ExtraField8", SqlDbType.NVarChar).Value = inv_Item.ExtraField8;
            cmd.Parameters.Add("@ExtraField9", SqlDbType.NVarChar).Value = inv_Item.ExtraField9;
            cmd.Parameters.Add("@ExtraField10", SqlDbType.NVarChar).Value = inv_Item.ExtraField10;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_Item.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_Item.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_Item.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_Item.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_Item.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_ItemID"].Value;
        }
    }

    public bool UpdateInv_Item(Inv_Item inv_Item)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_Item", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_ItemID", SqlDbType.Int).Value = inv_Item.Inv_ItemID;
            cmd.Parameters.Add("@ItemName", SqlDbType.NVarChar).Value = inv_Item.ItemName;
            cmd.Parameters.Add("@PurchaseID", SqlDbType.Int).Value = inv_Item.PurchaseID;
            cmd.Parameters.Add("@ItemCode", SqlDbType.NChar).Value = inv_Item.ItemCode;
            cmd.Parameters.Add("@RawMaterialID", SqlDbType.Int).Value = inv_Item.RawMaterialID;
            cmd.Parameters.Add("@StoreID", SqlDbType.Int).Value = inv_Item.StoreID;
            cmd.Parameters.Add("@QualityUnitID", SqlDbType.Int).Value = inv_Item.QualityUnitID;
            cmd.Parameters.Add("@QualityValue", SqlDbType.Decimal).Value = inv_Item.QualityValue;
            cmd.Parameters.Add("@QuantityUnitID", SqlDbType.Int).Value = inv_Item.QuantityUnitID;
            cmd.Parameters.Add("@PricePerUnit", SqlDbType.Decimal).Value = inv_Item.PricePerUnit;
            cmd.Parameters.Add("@PurchasedQuantity", SqlDbType.Decimal).Value = inv_Item.PurchasedQuantity;
            cmd.Parameters.Add("@IssueReturedQuantity", SqlDbType.Decimal).Value = inv_Item.IssueReturedQuantity;
            cmd.Parameters.Add("@UtilizedQuantity", SqlDbType.Decimal).Value = inv_Item.UtilizedQuantity;
            cmd.Parameters.Add("@LostQuantity", SqlDbType.Decimal).Value = inv_Item.LostQuantity;
            cmd.Parameters.Add("@ExtraFieldQuantity1", SqlDbType.Decimal).Value = inv_Item.ExtraFieldQuantity1;
            cmd.Parameters.Add("@ExtraFieldQuantity2", SqlDbType.Decimal).Value = inv_Item.ExtraFieldQuantity2;
            cmd.Parameters.Add("@ExtraFieldQuantity3", SqlDbType.Decimal).Value = inv_Item.ExtraFieldQuantity3;
            cmd.Parameters.Add("@ExtraFieldQuantity4", SqlDbType.Decimal).Value = inv_Item.ExtraFieldQuantity4;
            cmd.Parameters.Add("@ExtraFieldQuantity5", SqlDbType.Decimal).Value = inv_Item.ExtraFieldQuantity5;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_Item.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_Item.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_Item.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_Item.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_Item.ExtraField5;
            cmd.Parameters.Add("@ExtraField6", SqlDbType.NVarChar).Value = inv_Item.ExtraField6;
            cmd.Parameters.Add("@ExtraField7", SqlDbType.NVarChar).Value = inv_Item.ExtraField7;
            cmd.Parameters.Add("@ExtraField8", SqlDbType.NVarChar).Value = inv_Item.ExtraField8;
            cmd.Parameters.Add("@ExtraField9", SqlDbType.NVarChar).Value = inv_Item.ExtraField9;
            cmd.Parameters.Add("@ExtraField10", SqlDbType.NVarChar).Value = inv_Item.ExtraField10;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_Item.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_Item.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_Item.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_Item.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_Item.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
