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

public class SqlMedicineProvider:DataAccessObject
{
	public SqlMedicineProvider()
    {
    }


    public bool DeleteMedicine(int medicineID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteMedicine", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MedicineID", SqlDbType.Int).Value = medicineID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Medicine> GetAllMedicines()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllMedicines", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetMedicinesFromReader(reader);
        }
    }
    public List<Medicine> GetMedicinesFromReader(IDataReader reader)
    {
        List<Medicine> medicines = new List<Medicine>();

        while (reader.Read())
        {
            medicines.Add(GetMedicineFromReader(reader));
        }
        return medicines;
    }

    public Medicine GetMedicineFromReader(IDataReader reader)
    {
        try
        {
            Medicine medicine = new Medicine
                (
                    (int)reader["MedicineID"],
                    reader["MedicineName"].ToString()
                );
             return medicine;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Medicine GetMedicineByID(int medicineID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetMedicineByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MedicineID", SqlDbType.Int).Value = medicineID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetMedicineFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertMedicine(Medicine medicine)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertMedicine", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MedicineID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@MedicineName", SqlDbType.NVarChar).Value = medicine.MedicineName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@MedicineID"].Value;
        }
    }

    public bool UpdateMedicine(Medicine medicine)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateMedicine", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MedicineID", SqlDbType.Int).Value = medicine.MedicineID;
            cmd.Parameters.Add("@MedicineName", SqlDbType.NVarChar).Value = medicine.MedicineName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
