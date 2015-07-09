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

public class SqlPos_ProductProvider:DataAccessObject
{
	public SqlPos_ProductProvider()
    {
    }


    public bool DeletePos_Product(int pos_ProductID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeletePos_Product", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductID", SqlDbType.Int).Value = pos_ProductID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Pos_Product> GetAllPos_Products()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_Products", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_ProductsFromReader(reader);
        }
    }

    public List<Pos_Product> GetAllPos_ProductsByTrasactionMasterID(int TrasactionMasterID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_ProductByTransactionMasterID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@TrasactionMasterID", SqlDbType.Int).Value = TrasactionMasterID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_ProductsFromReader(reader);
        }
    }


    public List<Pos_Product> GetAllPos_ProductsByInventoryID(int InventoryID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllPos_ProductByInventoryID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@InventoryID", SqlDbType.Int).Value = InventoryID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetPos_ProductsFromReader(reader);
        }
    }


    public List<Pos_Product> GetPos_ProductsFromReader(IDataReader reader)
    {
        List<Pos_Product> pos_Products = new List<Pos_Product>();

        while (reader.Read())
        {
            pos_Products.Add(GetPos_ProductFromReader(reader));
        }
        return pos_Products;
    }

    public Pos_Product GetPos_ProductFromReader(IDataReader reader)
    {
        try
        {
            Pos_Product pos_Product = new Pos_Product
                (
                    (int)reader["Pos_ProductID"],
                    (int)reader["ProductID"],
                    (int)reader["ReferenceID"],
                    (int)reader["Pos_ProductTypeID"],
                    reader["Inv_UtilizationDetailsIDs"].ToString(),
                    (int)reader["ProductStatusID"],
                    reader["ProductName"].ToString(),
                    reader["DesignCode"].ToString(),
                    (int)reader["Pos_SizeID"],
                    (int)reader["Pos_BrandID"],
                    (int)reader["Inv_QuantityUnitID"],
                    (decimal)reader["FabricsCost"],
                    (decimal)reader["AccesoriesCost"],
                    (decimal)reader["Overhead"],
                    (decimal)reader["OthersCost"],
                    (decimal)reader["PurchasePrice"],
                    (decimal)reader["SalePrice"],
                    (decimal)reader["OldSalePrice"],
                    reader["Note"].ToString(),
                    reader["BarCode"].ToString(),
                    (int)reader["Pos_ColorID"],
                    (int)reader["Pos_FabricsTypeID"],
                    reader["StyleCode"].ToString(),
                    reader["Pic1"].ToString(),
                    reader["Pic2"].ToString(),
                    reader["Pic3"].ToString(),
                    (decimal)reader["VatPercentage"],
                    (bool)reader["IsVatExclusive"],
                    (decimal)reader["DiscountPercentage"],
                    (decimal)reader["DiscountAmount"],
                    reader["FabricsNo"].ToString(),
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
             return pos_Product;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Pos_Product GetPos_ProductByID(int pos_ProductID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetPos_ProductByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Pos_ProductID", SqlDbType.Int).Value = pos_ProductID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetPos_ProductFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertPos_Product(Pos_Product pos_Product)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertPos_Product", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = pos_Product.ProductID;
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = pos_Product.ReferenceID;
            cmd.Parameters.Add("@Pos_ProductTypeID", SqlDbType.Int).Value = pos_Product.Pos_ProductTypeID;
            cmd.Parameters.Add("@Inv_UtilizationDetailsIDs", SqlDbType.NVarChar).Value = pos_Product.Inv_UtilizationDetailsIDs;
            cmd.Parameters.Add("@ProductStatusID", SqlDbType.Int).Value = pos_Product.ProductStatusID;
            cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = pos_Product.ProductName;
            cmd.Parameters.Add("@DesignCode", SqlDbType.NVarChar).Value = pos_Product.DesignCode;
            cmd.Parameters.Add("@Pos_SizeID", SqlDbType.Int).Value = pos_Product.Pos_SizeID;
            cmd.Parameters.Add("@Pos_BrandID", SqlDbType.Int).Value = pos_Product.Pos_BrandID;
            cmd.Parameters.Add("@Inv_QuantityUnitID", SqlDbType.Int).Value = pos_Product.Inv_QuantityUnitID;
            cmd.Parameters.Add("@FabricsCost", SqlDbType.Decimal).Value = pos_Product.FabricsCost;
            cmd.Parameters.Add("@AccesoriesCost", SqlDbType.Decimal).Value = pos_Product.AccesoriesCost;
            cmd.Parameters.Add("@Overhead", SqlDbType.Decimal).Value = pos_Product.Overhead;
            cmd.Parameters.Add("@OthersCost", SqlDbType.Decimal).Value = pos_Product.OthersCost;
            cmd.Parameters.Add("@PurchasePrice", SqlDbType.Decimal).Value = pos_Product.PurchasePrice;
            cmd.Parameters.Add("@SalePrice", SqlDbType.Decimal).Value = pos_Product.SalePrice;
            cmd.Parameters.Add("@OldSalePrice", SqlDbType.Decimal).Value = pos_Product.OldSalePrice;
            cmd.Parameters.Add("@Note", SqlDbType.NText).Value = pos_Product.Note;
            cmd.Parameters.Add("@BarCode", SqlDbType.NVarChar).Value = pos_Product.BarCode;
            cmd.Parameters.Add("@Pos_ColorID", SqlDbType.Int).Value = pos_Product.Pos_ColorID;
            cmd.Parameters.Add("@Pos_FabricsTypeID", SqlDbType.Int).Value = pos_Product.Pos_FabricsTypeID;
            cmd.Parameters.Add("@StyleCode", SqlDbType.NVarChar).Value = pos_Product.StyleCode;
            cmd.Parameters.Add("@Pic1", SqlDbType.NText).Value = pos_Product.Pic1;
            cmd.Parameters.Add("@Pic2", SqlDbType.NVarChar).Value = pos_Product.Pic2;
            cmd.Parameters.Add("@Pic3", SqlDbType.NVarChar).Value = pos_Product.Pic3;
            cmd.Parameters.Add("@VatPercentage", SqlDbType.Decimal).Value = pos_Product.VatPercentage;
            cmd.Parameters.Add("@IsVatExclusive", SqlDbType.Bit).Value = pos_Product.IsVatExclusive;
            cmd.Parameters.Add("@DiscountPercentage", SqlDbType.Decimal).Value = pos_Product.DiscountPercentage;
            cmd.Parameters.Add("@DiscountAmount", SqlDbType.Decimal).Value = pos_Product.DiscountAmount;
            cmd.Parameters.Add("@FabricsNo", SqlDbType.NVarChar).Value = pos_Product.FabricsNo;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_Product.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_Product.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_Product.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = pos_Product.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = pos_Product.ExtraField5;
            cmd.Parameters.Add("@ExtraField6", SqlDbType.NVarChar).Value = pos_Product.ExtraField6;
            cmd.Parameters.Add("@ExtraField7", SqlDbType.NVarChar).Value = pos_Product.ExtraField7;
            cmd.Parameters.Add("@ExtraField8", SqlDbType.NVarChar).Value = pos_Product.ExtraField8;
            cmd.Parameters.Add("@ExtraField9", SqlDbType.NVarChar).Value = pos_Product.ExtraField9;
            cmd.Parameters.Add("@ExtraField10", SqlDbType.NVarChar).Value = pos_Product.ExtraField10;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = pos_Product.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pos_Product.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = pos_Product.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = pos_Product.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = pos_Product.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Pos_ProductID"].Value;
        }
    }

    public bool UpdatePos_Product(Pos_Product pos_Product)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdatePos_Product", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pos_ProductID", SqlDbType.Int).Value = pos_Product.Pos_ProductID;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = pos_Product.ProductID;
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = pos_Product.ReferenceID;
            cmd.Parameters.Add("@Pos_ProductTypeID", SqlDbType.Int).Value = pos_Product.Pos_ProductTypeID;
            cmd.Parameters.Add("@Inv_UtilizationDetailsIDs", SqlDbType.NVarChar).Value = pos_Product.Inv_UtilizationDetailsIDs;
            cmd.Parameters.Add("@ProductStatusID", SqlDbType.Int).Value = pos_Product.ProductStatusID;
            cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = pos_Product.ProductName;
            cmd.Parameters.Add("@DesignCode", SqlDbType.NVarChar).Value = pos_Product.DesignCode;
            cmd.Parameters.Add("@Pos_SizeID", SqlDbType.Int).Value = pos_Product.Pos_SizeID;
            cmd.Parameters.Add("@Pos_BrandID", SqlDbType.Int).Value = pos_Product.Pos_BrandID;
            cmd.Parameters.Add("@Inv_QuantityUnitID", SqlDbType.Int).Value = pos_Product.Inv_QuantityUnitID;
            cmd.Parameters.Add("@FabricsCost", SqlDbType.Decimal).Value = pos_Product.FabricsCost;
            cmd.Parameters.Add("@AccesoriesCost", SqlDbType.Decimal).Value = pos_Product.AccesoriesCost;
            cmd.Parameters.Add("@Overhead", SqlDbType.Decimal).Value = pos_Product.Overhead;
            cmd.Parameters.Add("@OthersCost", SqlDbType.Decimal).Value = pos_Product.OthersCost;
            cmd.Parameters.Add("@PurchasePrice", SqlDbType.Decimal).Value = pos_Product.PurchasePrice;
            cmd.Parameters.Add("@SalePrice", SqlDbType.Decimal).Value = pos_Product.SalePrice;
            cmd.Parameters.Add("@OldSalePrice", SqlDbType.Decimal).Value = pos_Product.OldSalePrice;
            cmd.Parameters.Add("@Note", SqlDbType.NText).Value = pos_Product.Note;
            cmd.Parameters.Add("@BarCode", SqlDbType.NVarChar).Value = pos_Product.BarCode;
            cmd.Parameters.Add("@Pos_ColorID", SqlDbType.Int).Value = pos_Product.Pos_ColorID;
            cmd.Parameters.Add("@Pos_FabricsTypeID", SqlDbType.Int).Value = pos_Product.Pos_FabricsTypeID;
            cmd.Parameters.Add("@StyleCode", SqlDbType.NVarChar).Value = pos_Product.StyleCode;
            cmd.Parameters.Add("@Pic1", SqlDbType.NVarChar).Value = pos_Product.Pic1;
            cmd.Parameters.Add("@Pic2", SqlDbType.NVarChar).Value = pos_Product.Pic2;
            cmd.Parameters.Add("@Pic3", SqlDbType.NVarChar).Value = pos_Product.Pic3;
            cmd.Parameters.Add("@VatPercentage", SqlDbType.Decimal).Value = pos_Product.VatPercentage;
            cmd.Parameters.Add("@IsVatExclusive", SqlDbType.Bit).Value = pos_Product.IsVatExclusive;
            cmd.Parameters.Add("@DiscountPercentage", SqlDbType.Decimal).Value = pos_Product.DiscountPercentage;
            cmd.Parameters.Add("@DiscountAmount", SqlDbType.Decimal).Value = pos_Product.DiscountAmount;
            cmd.Parameters.Add("@FabricsNo", SqlDbType.NVarChar).Value = pos_Product.FabricsNo;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = pos_Product.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = pos_Product.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = pos_Product.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = pos_Product.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = pos_Product.ExtraField5;
            cmd.Parameters.Add("@ExtraField6", SqlDbType.NVarChar).Value = pos_Product.ExtraField6;
            cmd.Parameters.Add("@ExtraField7", SqlDbType.NVarChar).Value = pos_Product.ExtraField7;
            cmd.Parameters.Add("@ExtraField8", SqlDbType.NVarChar).Value = pos_Product.ExtraField8;
            cmd.Parameters.Add("@ExtraField9", SqlDbType.NVarChar).Value = pos_Product.ExtraField9;
            cmd.Parameters.Add("@ExtraField10", SqlDbType.NVarChar).Value = pos_Product.ExtraField10;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = pos_Product.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = pos_Product.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = pos_Product.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = pos_Product.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = pos_Product.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
