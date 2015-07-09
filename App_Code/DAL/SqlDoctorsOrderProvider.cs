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

public class SqlDoctorsOrderProvider:DataAccessObject
{
	public SqlDoctorsOrderProvider()
    {
    }


    public bool DeleteDoctorsOrder(int doctorsOrderID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteDoctorsOrder", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DoctorsOrderID", SqlDbType.Int).Value = doctorsOrderID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<DoctorsOrder> GetAllDoctorsOrders()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllDoctorsOrders", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetDoctorsOrdersFromReader(reader);
        }
    }
    public List<DoctorsOrder> GetAllDoctorsOrdersByResidentID(int residentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllDoctorsOrdersByResidentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = residentID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetDoctorsOrdersFromReader(reader);
        }
    }
    public List<DoctorsOrder> GetDoctorsOrdersFromReader(IDataReader reader)
    {
        List<DoctorsOrder> doctorsOrders = new List<DoctorsOrder>();

        while (reader.Read())
        {
            doctorsOrders.Add(GetDoctorsOrderFromReader(reader));
        }
        return doctorsOrders;
    }

    public DoctorsOrder GetDoctorsOrderFromReader(IDataReader reader)
    {
        try
        {
            DoctorsOrder doctorsOrder = new DoctorsOrder
                (
                    (int)reader["DoctorsOrderID"],
                    reader["ClinicalFindings"].ToString(),
                    reader["Orders"].ToString(),
                    (int)reader["ResidentID"],
                    (DateTime)reader["OrderDate"],
                    (int)reader["AddeBy"],
                    (DateTime)reader["AddedDate"],
                    (int)reader["UpdatedBy"],
                    (DateTime)reader["UpdatedDate"],
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    reader["ExtraField3"].ToString(),
                    reader["ExtraField4"].ToString(),
                    reader["ExtraField5"].ToString()
                );
             return doctorsOrder;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public DoctorsOrder GetDoctorsOrderByID(int doctorsOrderID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetDoctorsOrderByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@DoctorsOrderID", SqlDbType.Int).Value = doctorsOrderID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetDoctorsOrderFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertDoctorsOrder(DoctorsOrder doctorsOrder)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertDoctorsOrder", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DoctorsOrderID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ClinicalFindings", SqlDbType.NText).Value = doctorsOrder.ClinicalFindings;
            cmd.Parameters.Add("@Orders", SqlDbType.NText).Value = doctorsOrder.Orders;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = doctorsOrder.ResidentID;
            cmd.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = doctorsOrder.OrderDate;
            cmd.Parameters.Add("@AddeBy", SqlDbType.Int).Value = doctorsOrder.AddeBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = doctorsOrder.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = doctorsOrder.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = doctorsOrder.UpdatedDate;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = doctorsOrder.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = doctorsOrder.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = doctorsOrder.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = doctorsOrder.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = doctorsOrder.ExtraField5;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@DoctorsOrderID"].Value;
        }
    }

    public bool UpdateDoctorsOrder(DoctorsOrder doctorsOrder)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateDoctorsOrder", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DoctorsOrderID", SqlDbType.Int).Value = doctorsOrder.DoctorsOrderID;
            cmd.Parameters.Add("@ClinicalFindings", SqlDbType.NText).Value = doctorsOrder.ClinicalFindings;
            cmd.Parameters.Add("@Orders", SqlDbType.NText).Value = doctorsOrder.Orders;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = doctorsOrder.ResidentID;
            cmd.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = doctorsOrder.OrderDate;
            cmd.Parameters.Add("@AddeBy", SqlDbType.Int).Value = doctorsOrder.AddeBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = doctorsOrder.AddedDate;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = doctorsOrder.UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = doctorsOrder.UpdatedDate;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = doctorsOrder.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = doctorsOrder.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = doctorsOrder.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = doctorsOrder.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = doctorsOrder.ExtraField5;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
