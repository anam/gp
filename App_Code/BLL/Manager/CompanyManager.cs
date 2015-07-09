using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class CompanyManager
{
	public CompanyManager()
	{
	}

    public static List<Company> GetAllCompanies()
    {
        List<Company> companies = new List<Company>();
        SqlCompanyProvider sqlCompanyProvider = new SqlCompanyProvider();
        companies = sqlCompanyProvider.GetAllCompanies();
        return companies;
    }


    public static Company GetCompanyByID(int id)
    {
        Company company = new Company();
        SqlCompanyProvider sqlCompanyProvider = new SqlCompanyProvider();
        company = sqlCompanyProvider.GetCompanyByID(id);
        return company;
    }


    public static int InsertCompany(Company company)
    {
        SqlCompanyProvider sqlCompanyProvider = new SqlCompanyProvider();
        return sqlCompanyProvider.InsertCompany(company);
    }


    public static bool UpdateCompany(Company company)
    {
        SqlCompanyProvider sqlCompanyProvider = new SqlCompanyProvider();
        return sqlCompanyProvider.UpdateCompany(company);
    }

    public static bool DeleteCompany(int companyID)
    {
        SqlCompanyProvider sqlCompanyProvider = new SqlCompanyProvider();
        return sqlCompanyProvider.DeleteCompany(companyID);
    }
}
