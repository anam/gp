using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlIncomeProvider:DataAccessObject
{
	public SqlIncomeProvider()
    {
    }


    public DataSet  GetAllIncomes()
    {
        DataSet Incomes = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllIncomes", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Incomes);
            myadapter.Dispose();
            connection.Close();

            return Incomes;
        }
    }
	public DataSet GetIncomePageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetIncomePageWise", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize",  PageSize );
                command.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                command.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                connection.Open();
                SqlDataAdapter myadapter = new SqlDataAdapter(command);
                myadapter.Fill(ds);
                myadapter.Dispose();
                connection.Close();
                recordCount = Convert.ToInt32(command.Parameters["@RecordCount"].Value);
                return ds;
            }
        }
    }


    public DataSet  GetDropDownLisAllIncome()
    {
        DataSet Incomes = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllIncome", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Incomes);
            myadapter.Dispose();
            connection.Close();

            return Incomes;
        }
    }

    public DataSet   GetAllToursWithRelation()
    {
       DataSet ds = new DataSet();
       using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllToursWithRelation", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(ds);
            myadapter.Dispose();
            connection.Close();

            return ds;
        }
    }
    public List<Income> GetIncomesFromReader(IDataReader reader)
    {
        List<Income> incomes = new List<Income>();

        while (reader.Read())
        {
            incomes.Add(GetIncomeFromReader(reader));
        }
        return incomes;
    }

    public Income GetIncomeFromReader(IDataReader reader)
    {
        try
        {
            Income income = new Income
                (

                     DataAccessObject.IsNULL<int>(reader["IncomeID"]),
                     DataAccessObject.IsNULL<string>(reader["IncomeName"])
                );
             return income;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Income  GetIncomeByIncomeID(int  incomeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetIncomeByIncomeID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@IncomeID", SqlDbType.Int).Value = incomeID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetIncomeFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertIncome(Income income)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertIncome", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@IncomeID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@IncomeName", SqlDbType.NVarChar).Value = income.IncomeName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@IncomeID"].Value;
        }
    }

    public bool UpdateIncome(Income income)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateIncome", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@IncomeID", SqlDbType.Int).Value = income.IncomeID;
            cmd.Parameters.Add("@IncomeName", SqlDbType.NVarChar).Value = income.IncomeName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteIncome(int incomeID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteIncome", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@IncomeID", SqlDbType.Int).Value = incomeID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

