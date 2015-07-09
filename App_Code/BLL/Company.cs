using System;
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

public class Company
{
    public Company()
    {
    }

    public Company
        (
        int companyID, 
        string companyName
        )
    {
        this.CompanyID = companyID;
        this.CompanyName = companyName;
    }


    private int _companyID;
    public int CompanyID
    {
        get { return _companyID; }
        set { _companyID = value; }
    }

    private string _companyName;
    public string CompanyName
    {
        get { return _companyName; }
        set { _companyName = value; }
    }
}
