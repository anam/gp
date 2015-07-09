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

public class SqlCompanyProvider:DataAccessObject
{
	public SqlCompanyProvider()
    {
    }


    public bool DeleteCompany(int companyID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteCompany", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = companyID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Company> GetAllCompanies()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllCompanies", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetCompaniesFromReader(reader);
        }
    }
    public List<Company> GetCompaniesFromReader(IDataReader reader)
    {
        List<Company> companies = new List<Company>();

        while (reader.Read())
        {
            companies.Add(GetCompanyFromReader(reader));
        }
        return companies;
    }

    public Company GetCompanyFromReader(IDataReader reader)
    {
        try
        {
            Company company = new Company
                (
                    (int)reader["CompanyID"],
                    reader["CompanyName"].ToString()
                );
             return company;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Company GetCompanyByID(int companyID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetCompanyByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CompanyID", SqlDbType.Int).Value = companyID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetCompanyFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertCompany(Company company)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertCompany", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = company.CompanyName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@CompanyID"].Value;
        }
    }

    public bool UpdateCompany(Company company)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateCompany", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = company.CompanyID;
            cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = company.CompanyName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
