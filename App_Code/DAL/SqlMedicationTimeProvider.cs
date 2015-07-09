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

public class SqlMedicationTimeProvider:DataAccessObject
{
	public SqlMedicationTimeProvider()
    {
    }


    public bool DeleteMedicationTime(int medicationTimeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteMedicationTime", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MedicationTimeID", SqlDbType.Int).Value = medicationTimeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<MedicationTime> GetAllMedicationTimes()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllMedicationTimes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetMedicationTimesFromReader(reader);
        }
    }
    public DataSet GetAllMedicationTimesByResident(int residentID)
    {
        DataSet Appointmnets = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllMedicationTimesByResidentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = residentID;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Appointmnets);
            myadapter.Dispose();
            connection.Close();

            return Appointmnets;
        }
    }

    public DataSet GetAllMedicationTimesByResidentByStatus(int residentID,string status)
    {
        DataSet Appointmnets = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllMedicationTimesByResidentIDByStatus", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = residentID;
            command.Parameters.Add("@Status", SqlDbType.NVarChar).Value = status;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Appointmnets);
            myadapter.Dispose();
            connection.Close();

            return Appointmnets;
        }
    }

    


    public DataSet GetAllMedicationTimesByResidentForPrint(int residentID,int printOption)
    {
        DataSet Appointmnets = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllMedicationTimesByResidentIDForPrint", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = residentID;
            command.Parameters.Add("@PrintOption", SqlDbType.Int).Value = printOption;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Appointmnets);
            myadapter.Dispose();
            connection.Close();

            return Appointmnets;
        }
    }

    public List<MedicationTime> GetMedicationTimesFromReader(IDataReader reader)
    {
        List<MedicationTime> medicationTimes = new List<MedicationTime>();

        while (reader.Read())
        {
            medicationTimes.Add(GetMedicationTimeFromReader(reader));
        }
        return medicationTimes;
    }

    public MedicationTime GetMedicationTimeFromReader(IDataReader reader)
    {
        try
        {
            MedicationTime medicationTime = new MedicationTime
                (
                    (int)reader["MedicationTimeID"],
                    reader["TakingTime"].ToString(),
                    (int)reader["MedicineID"],
                    (int)reader["ResidentID"],
                    reader["Quantity"].ToString(),
                    reader["Quality"].ToString(),
                    reader["Frequency"].ToString(),
                    reader["ExtraField1"].ToString(),
                    reader["ExtraField2"].ToString(),
                    (int)reader["AddedBy"],
                    (DateTime)reader["AddedDate"],
                    (int)reader["UpdateBy"],
                    (DateTime)reader["UpdateDate"]
                );
            try
            {
                medicationTime.ExtraField3 = reader["ExtraField3"].ToString();
                medicationTime.ExtraField4 = reader["ExtraField4"].ToString();
                medicationTime.ExtraField5 = reader["ExtraField5"].ToString();
            }
            catch (Exception ex)
            { }

            try
            {
                medicationTime.ExtraField6 = reader["ExtraField6"].ToString();
            }
            catch (Exception ex)
            { }

            try
            {
                medicationTime.ExtraField7 = reader["ExtraField7"].ToString();
            }
            catch (Exception ex)
            { }

            try
            {
                medicationTime.ExtraField8 = reader["ExtraField8"].ToString();
            }
            catch (Exception ex)
            { }

            try
            {
                medicationTime.ExtraField9 = reader["ExtraField9"].ToString();
            }
            catch (Exception ex)
            { }

            try
            {
                medicationTime.ExtraField10 = reader["ExtraField10"].ToString();
            }
            catch (Exception ex)
            { }

            
            return medicationTime;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public MedicationTime GetMedicationTimeByID(int medicationTimeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetMedicationTimeByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MedicationTimeID", SqlDbType.Int).Value = medicationTimeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetMedicationTimeFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertMedicationTime(MedicationTime medicationTime)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertMedicationTime", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MedicationTimeID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@TakingTime", SqlDbType.NVarChar).Value = medicationTime.TakingTime;
            cmd.Parameters.Add("@MedicineID", SqlDbType.Int).Value = medicationTime.MedicineID;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = medicationTime.ResidentID;
            cmd.Parameters.Add("@Quantity", SqlDbType.NVarChar).Value = medicationTime.Quantity;
            cmd.Parameters.Add("@Quality", SqlDbType.NChar).Value = medicationTime.Quality;
            cmd.Parameters.Add("@Frequency", SqlDbType.NVarChar).Value = medicationTime.Frequency;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = medicationTime.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = medicationTime.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = medicationTime.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = medicationTime.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = medicationTime.ExtraField5;
            cmd.Parameters.Add("@ExtraField6", SqlDbType.NVarChar).Value = medicationTime.ExtraField6;
            cmd.Parameters.Add("@ExtraField7", SqlDbType.NVarChar).Value = medicationTime.ExtraField7;
            cmd.Parameters.Add("@ExtraField8", SqlDbType.NVarChar).Value = medicationTime.ExtraField8;
            cmd.Parameters.Add("@ExtraField9", SqlDbType.NVarChar).Value = medicationTime.ExtraField9;
            cmd.Parameters.Add("@ExtraField10", SqlDbType.NVarChar).Value = medicationTime.ExtraField10;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = medicationTime.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = medicationTime.AddedDate;
            cmd.Parameters.Add("@UpdateBy", SqlDbType.Int).Value = medicationTime.UpdateBy;
            cmd.Parameters.Add("@UpdateDate", SqlDbType.DateTime).Value = medicationTime.UpdateDate;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@MedicationTimeID"].Value;
        }
    }

    public bool UpdateMedicationTime(MedicationTime medicationTime)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateMedicationTime", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MedicationTimeID", SqlDbType.Int).Value = medicationTime.MedicationTimeID;
            cmd.Parameters.Add("@TakingTime", SqlDbType.NVarChar).Value = medicationTime.TakingTime;
            cmd.Parameters.Add("@MedicineID", SqlDbType.Int).Value = medicationTime.MedicineID;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = medicationTime.ResidentID;
            cmd.Parameters.Add("@Quantity", SqlDbType.NVarChar).Value = medicationTime.Quantity;
            cmd.Parameters.Add("@Quality", SqlDbType.NChar).Value = medicationTime.Quality;
            cmd.Parameters.Add("@Frequency", SqlDbType.NVarChar).Value = medicationTime.Frequency;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = medicationTime.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = medicationTime.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = medicationTime.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = medicationTime.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = medicationTime.ExtraField5;
            cmd.Parameters.Add("@ExtraField6", SqlDbType.NVarChar).Value = medicationTime.ExtraField6;
            cmd.Parameters.Add("@ExtraField7", SqlDbType.NVarChar).Value = medicationTime.ExtraField7;
            cmd.Parameters.Add("@ExtraField8", SqlDbType.NVarChar).Value = medicationTime.ExtraField8;
            cmd.Parameters.Add("@ExtraField9", SqlDbType.NVarChar).Value = medicationTime.ExtraField9;
            cmd.Parameters.Add("@ExtraField10", SqlDbType.NVarChar).Value = medicationTime.ExtraField10;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = medicationTime.AddedBy;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = medicationTime.AddedDate;
            cmd.Parameters.Add("@UpdateBy", SqlDbType.Int).Value = medicationTime.UpdateBy;
            cmd.Parameters.Add("@UpdateDate", SqlDbType.DateTime).Value = medicationTime.UpdateDate;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
