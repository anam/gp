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

public class SqlInv_IssueDetailProvider:DataAccessObject
{
	public SqlInv_IssueDetailProvider()
    {
    }


    public bool DeleteInv_IssueDetail(int inv_IssueDetailID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_DeleteInv_IssueDetail", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_IssueDetailID", SqlDbType.Int).Value = inv_IssueDetailID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Inv_IssueDetail> GetAllInv_IssueDetails()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_IssueDetails", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_IssueDetailsFromReader(reader);
        }
    }


    public List<Inv_IssueDetail> GetAllInv_IssueDetailsByIssueMasterID(string IssueMasterID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_IssueDetailsByIssueMasterID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = IssueMasterID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_IssueDetailsFromReader(reader);
        }
    }


    public List<Inv_IssueDetail> GetAllInv_IssueDetailsByIssueMasterReturnID(string IssueMasterReturnID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_IssueDetailsByIssueMasterReturnID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@IssueMasterReturnID", SqlDbType.Decimal).Value = decimal.Parse(IssueMasterReturnID);
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_IssueDetailsFromReader(reader);
        }
    }


    public List<Inv_IssueDetail> GetAllInv_IssueDetailsByEmpoyeeIDnWorkStationIDnProductID(bool withAccessories, int employeeID, int workStationID, int productID,string codeString,string finalProductID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_IssueDetailsByEmpoyeeIDnWorkStationIDnProductID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID;
            command.Parameters.Add("@WorkStationID", SqlDbType.Int).Value = workStationID;
            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID;
            command.Parameters.Add("@FinalProductID", SqlDbType.Int).Value = int.Parse(finalProductID);
            command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = codeString;
            command.Parameters.Add("@WithAccessories", SqlDbType.Bit).Value = withAccessories;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_IssueDetailsFromReader(reader);
        }
    }

    public List<Inv_IssueDetail> GetAllInv_IssueDetailsRootIssue()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_IssueDetailsRootIssue", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_IssueDetailsFromReader(reader);
        }
    }

    public List<Inv_IssueDetail> GetAllInv_IssueDetailsRootIssueByEmployeeID(int employeeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_IssueDetailsRootIssueByEmployeeID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_IssueDetailsFromReader(reader);
        }
    }


    public List<Inv_IssueDetail> GetAllInv_IssueDetailsRootIssueByEmployeeIDnProductID(int employeeID,int productID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetAllInv_IssueDetailsRootIssueByEmployeeIDnProductID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID;
            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetInv_IssueDetailsFromReader(reader);
        }
    }


    public List<Inv_IssueDetail> GetInv_IssueDetailsFromReader(IDataReader reader)
    {
        List<Inv_IssueDetail> inv_IssueDetails = new List<Inv_IssueDetail>();

        while (reader.Read())
        {
            inv_IssueDetails.Add(GetInv_IssueDetailFromReader(reader));
        }
        return inv_IssueDetails;
    }

    public Inv_IssueDetail GetInv_IssueDetailFromReader(IDataReader reader)
    {
        try
        {
            Inv_IssueDetail inv_IssueDetail = new Inv_IssueDetail
                (
                    (int)reader["Inv_IssueDetailID"],
                    (int)reader["ItemID"],
                    (decimal)reader["Quantity"],
                    (int)reader["ApproximateQuantity"],
                    (int)reader["ProductID"],
                    (int)reader["AdditionalWithIssueDetailID"],
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

            try{inv_IssueDetail.ItemCode= reader["ItemCode"].ToString();}
            catch(Exception ex){}
            try{inv_IssueDetail.ItemName= reader["ItemName"].ToString();}
            catch(Exception ex){}
            try { inv_IssueDetail.PricePerUnit = (decimal)reader["PricePerUnit"]; }
            catch (Exception ex) { }
            try { inv_IssueDetail.TotalPrice = inv_IssueDetail.PricePerUnit * inv_IssueDetail.Quantity; }
            catch (Exception ex) { }
            try { inv_IssueDetail.QuantityUnitName = reader["QuantityUnitName"].ToString(); }
            catch (Exception ex) { }
            try { inv_IssueDetail.QualityUnitName = reader["QualityUnitName"].ToString(); }
            catch (Exception ex) { }
            try { inv_IssueDetail.QualityUnitValue = (decimal)reader["QualityValue"]; }
            catch (Exception ex) { }
            try { inv_IssueDetail.ProductName = reader["ProductName"].ToString(); }
            catch (Exception ex) { inv_IssueDetail.ProductName = "N/A"; }

            try { inv_IssueDetail.ACC_HeadTypeID = (int)reader["ACC_HeadTypeID"]; }
            catch (Exception ex) { inv_IssueDetail.ACC_HeadTypeID = 0; }

            try { inv_IssueDetail.RawMaterialID = (int)reader["RawMaterialID"]; }
            catch (Exception ex) { inv_IssueDetail.RawMaterialID = 0; }
            
            return inv_IssueDetail;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Inv_IssueDetail GetInv_IssueDetailByID(int inv_IssueDetailID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GP_GetInv_IssueDetailByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@Inv_IssueDetailID", SqlDbType.Int).Value = inv_IssueDetailID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetInv_IssueDetailFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertInv_IssueDetail(Inv_IssueDetail inv_IssueDetail)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_InsertInv_IssueDetail", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_IssueDetailID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = inv_IssueDetail.ItemID;
            cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = inv_IssueDetail.Quantity;
            cmd.Parameters.Add("@ApproximateQuantity", SqlDbType.Int).Value = inv_IssueDetail.ApproximateQuantity;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = inv_IssueDetail.ProductID;
            cmd.Parameters.Add("@AdditionalWithIssueDetailID", SqlDbType.Int).Value = inv_IssueDetail.AdditionalWithIssueDetailID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_IssueDetail.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_IssueDetail.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_IssueDetail.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_IssueDetail.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_IssueDetail.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_IssueDetail.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_IssueDetail.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_IssueDetail.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_IssueDetail.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_IssueDetail.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@Inv_IssueDetailID"].Value;
        }
    }

    public bool UpdateInv_IssueDetail(Inv_IssueDetail inv_IssueDetail)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("GP_UpdateInv_IssueDetail", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Inv_IssueDetailID", SqlDbType.Int).Value = inv_IssueDetail.Inv_IssueDetailID;
            cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = inv_IssueDetail.ItemID;
            cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = inv_IssueDetail.Quantity;
            cmd.Parameters.Add("@ApproximateQuantity", SqlDbType.Int).Value = inv_IssueDetail.ApproximateQuantity;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = inv_IssueDetail.ProductID;
            cmd.Parameters.Add("@AdditionalWithIssueDetailID", SqlDbType.Int).Value = inv_IssueDetail.AdditionalWithIssueDetailID;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = inv_IssueDetail.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = inv_IssueDetail.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = inv_IssueDetail.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = inv_IssueDetail.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = inv_IssueDetail.ExtraField5;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = inv_IssueDetail.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = inv_IssueDetail.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = inv_IssueDetail.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = inv_IssueDetail.UpdatedDate;
            cmd.Parameters.Add("@RowStatusID", SqlDbType.Int).Value = inv_IssueDetail.RowStatusID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
