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

public class Inv_IssueDetailManager
{
	public Inv_IssueDetailManager()
	{
	}

    public static List<Inv_IssueDetail> GetAllInv_IssueDetails()
    {
        List<Inv_IssueDetail> inv_IssueDetails = new List<Inv_IssueDetail>();
        SqlInv_IssueDetailProvider sqlInv_IssueDetailProvider = new SqlInv_IssueDetailProvider();
        inv_IssueDetails = sqlInv_IssueDetailProvider.GetAllInv_IssueDetails();
        return inv_IssueDetails;
    }


    public static List<Inv_IssueDetail> GetAllInv_IssueDetailsByIssueMasterID(string IssueMasterID)
    {
        List<Inv_IssueDetail> inv_IssueDetails = new List<Inv_IssueDetail>();
        SqlInv_IssueDetailProvider sqlInv_IssueDetailProvider = new SqlInv_IssueDetailProvider();
        inv_IssueDetails = sqlInv_IssueDetailProvider.GetAllInv_IssueDetailsByIssueMasterID(IssueMasterID);
        return inv_IssueDetails;
    }

    public static List<Inv_IssueDetail> GetAllInv_IssueDetailsByIssueMasterReturnID(string IssueMasterReturnID)
    {
        List<Inv_IssueDetail> inv_IssueDetails = new List<Inv_IssueDetail>();
        SqlInv_IssueDetailProvider sqlInv_IssueDetailProvider = new SqlInv_IssueDetailProvider();
        inv_IssueDetails = sqlInv_IssueDetailProvider.GetAllInv_IssueDetailsByIssueMasterReturnID(IssueMasterReturnID);
        return inv_IssueDetails;
    }


    public static List<Inv_IssueDetail> GetAllInv_IssueDetailsByEmpoyeeIDnWorkStationIDnProductID(int employeeID,int workStationID,int productID,string codeString,bool withAccessories,string finalProductID)
    {
        List<Inv_IssueDetail> inv_IssueDetails = new List<Inv_IssueDetail>();
        SqlInv_IssueDetailProvider sqlInv_IssueDetailProvider = new SqlInv_IssueDetailProvider();
        inv_IssueDetails = sqlInv_IssueDetailProvider.GetAllInv_IssueDetailsByEmpoyeeIDnWorkStationIDnProductID(withAccessories,employeeID, workStationID, productID, codeString,finalProductID);
        return inv_IssueDetails;
    }


    public static List<Inv_IssueDetail> GetAllInv_IssueDetailsRootIssue()
    {
        List<Inv_IssueDetail> inv_IssueDetails = new List<Inv_IssueDetail>();
        SqlInv_IssueDetailProvider sqlInv_IssueDetailProvider = new SqlInv_IssueDetailProvider();
        inv_IssueDetails = sqlInv_IssueDetailProvider.GetAllInv_IssueDetailsRootIssue();
        return inv_IssueDetails;
    }

    public static List<Inv_IssueDetail> GetAllInv_IssueDetailsRootIssueByEmployeeID(int employeeID)
    {
        List<Inv_IssueDetail> inv_IssueDetails = new List<Inv_IssueDetail>();
        SqlInv_IssueDetailProvider sqlInv_IssueDetailProvider = new SqlInv_IssueDetailProvider();
        inv_IssueDetails = sqlInv_IssueDetailProvider.GetAllInv_IssueDetailsRootIssueByEmployeeID(employeeID);
        return inv_IssueDetails;
    }

    public static List<Inv_IssueDetail> GetAllInv_IssueDetailsRootIssueByEmployeeIDnProductID(int employeeID,int productID)
    {
        List<Inv_IssueDetail> inv_IssueDetails = new List<Inv_IssueDetail>();
        SqlInv_IssueDetailProvider sqlInv_IssueDetailProvider = new SqlInv_IssueDetailProvider();
        inv_IssueDetails = sqlInv_IssueDetailProvider.GetAllInv_IssueDetailsRootIssueByEmployeeIDnProductID(employeeID,productID);
        return inv_IssueDetails;
    }


    public static Inv_IssueDetail GetInv_IssueDetailByID(int id)
    {
        Inv_IssueDetail inv_IssueDetail = new Inv_IssueDetail();
        SqlInv_IssueDetailProvider sqlInv_IssueDetailProvider = new SqlInv_IssueDetailProvider();
        inv_IssueDetail = sqlInv_IssueDetailProvider.GetInv_IssueDetailByID(id);
        return inv_IssueDetail;
    }


    public static int InsertInv_IssueDetail(Inv_IssueDetail inv_IssueDetail)
    {
        SqlInv_IssueDetailProvider sqlInv_IssueDetailProvider = new SqlInv_IssueDetailProvider();
        return sqlInv_IssueDetailProvider.InsertInv_IssueDetail(inv_IssueDetail);
    }


    public static bool UpdateInv_IssueDetail(Inv_IssueDetail inv_IssueDetail)
    {
        SqlInv_IssueDetailProvider sqlInv_IssueDetailProvider = new SqlInv_IssueDetailProvider();
        return sqlInv_IssueDetailProvider.UpdateInv_IssueDetail(inv_IssueDetail);
    }

    public static bool DeleteInv_IssueDetail(int inv_IssueDetailID)
    {
        SqlInv_IssueDetailProvider sqlInv_IssueDetailProvider = new SqlInv_IssueDetailProvider();
        return sqlInv_IssueDetailProvider.DeleteInv_IssueDetail(inv_IssueDetailID);
    }
}
