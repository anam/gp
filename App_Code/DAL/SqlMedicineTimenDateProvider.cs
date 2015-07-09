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

public class SqlMedicineTimenDateProvider:DataAccessObject
{
	public SqlMedicineTimenDateProvider()
    {
    }


    public bool DeleteMedicineTimenDate(int medicineTimenDateID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteMedicineTimenDate", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MedicineTimenDateID", SqlDbType.Int).Value = medicineTimenDateID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public bool DeleteMedicineTimenDateByResidentID(int residentID,string DateRange)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteMedicineTimenDateByResidentID", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = residentID;
            cmd.Parameters.Add("@DateRange", SqlDbType.NVarChar).Value = DateRange;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<MedicineTimenDate> GetAllMedicineTimenDates()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllMedicineTimenDates", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetMedicineTimenDatesFromReader(reader);
        }
    }

    public DataSet  GetAllMedicineTimenDatesDistinctDateByResidentID(int ResidentID)
    {
        DataSet Appointmnets = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllMedicineTimenDatesDistinctDateByResidentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = ResidentID;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Appointmnets);
            myadapter.Dispose();
            connection.Close();

            return Appointmnets;
        }
    }

    public DataSet GetMedicationTimenDateByResidentIDWithDateRange(int ResidentID, DateTime startdate, DateTime endDate)
    {
        DataSet Appointmnets = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetMedicationTimenDateByResidentIDWithDateRange", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = ResidentID;
            command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startdate;
            command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Appointmnets);
            myadapter.Dispose();
            connection.Close();

            return Appointmnets;
        }
    }

    public DataSet GetMedicationTimenDateByResidentID(int ResidentID)
    {
        DataSet Appointmnets = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetMedicationTimenDateByResidentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = ResidentID;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Appointmnets);
            myadapter.Dispose();
            connection.Close();

            return Appointmnets;
        }
    }

    public List<MedicineTimenDate> GetAllMedicineTimenDatesDistinctDateByResidentIDnDate(int ResidentID,string SearchString)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllMedicineTimenDatesByResidentIDnDate", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = ResidentID;
            command.Parameters.Add("@SearchString", SqlDbType.NVarChar).Value = SearchString;

            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetMedicineTimenDatesFromReader(reader);
        }
    }

    public List<MedicineTimenDate> GetAllMedicineTimenDatesByResidentID(int ResidentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllMedicineTimenDatesByResidentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = ResidentID;

            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetMedicineTimenDatesFromReader(reader);
        }
    }

    public List<MedicineTimenDate> GetMedicineTimenDatesFromReader(IDataReader reader)
    {
        List<MedicineTimenDate> medicineTimenDates = new List<MedicineTimenDate>();

        while (reader.Read())
        {
            medicineTimenDates.Add(GetMedicineTimenDateFromReader(reader));
        }
        return medicineTimenDates;
    }

    public MedicineTimenDate GetMedicineTimenDateFromReader(IDataReader reader)
    {
        try
        {
            MedicineTimenDate medicineTimenDate = new MedicineTimenDate
                (
                    (int)reader["MedicineTimenDateID"],
                    (int)reader["MedicationTimeID"],
                    (DateTime)reader["MedicineDate"]
                );
            try { medicineTimenDate.ExtraField1 = reader["ExtraField1"].ToString(); }
            catch (Exception ex) { }
            try { medicineTimenDate.ExtraField2 = reader["ExtraField2"].ToString(); }
            catch (Exception ex) { }
            try { medicineTimenDate.ExtraField3 = reader["ExtraField3"].ToString(); }
            catch (Exception ex) { }
            
             return medicineTimenDate;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public MedicineTimenDate GetMedicineTimenDateByID(int medicineTimenDateID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetMedicineTimenDateByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MedicineTimenDateID", SqlDbType.Int).Value = medicineTimenDateID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetMedicineTimenDateFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertMedicineTimenDate(MedicineTimenDate medicineTimenDate)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertMedicineTimenDate", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MedicineTimenDateID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@MedicationTimeID", SqlDbType.Int).Value = medicineTimenDate.MedicationTimeID;
            cmd.Parameters.Add("@MedicineDate", SqlDbType.DateTime).Value = medicineTimenDate.MedicineDate;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = medicineTimenDate.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = medicineTimenDate.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = medicineTimenDate.ExtraField3;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@MedicineTimenDateID"].Value;
        }
    }

    public bool UpdateMedicineTimenDate(MedicineTimenDate medicineTimenDate)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateMedicineTimenDate", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MedicineTimenDateID", SqlDbType.Int).Value = medicineTimenDate.MedicineTimenDateID;
            cmd.Parameters.Add("@MedicationTimeID", SqlDbType.Int).Value = medicineTimenDate.MedicationTimeID;
            cmd.Parameters.Add("@MedicineDate", SqlDbType.DateTime).Value = medicineTimenDate.MedicineDate;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
