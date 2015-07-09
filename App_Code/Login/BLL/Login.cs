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

public class Login
{
    public Login()
    {
    }

    public Login
        (
        int loginID, 
        string loginName, 
        string password, 
        string email, 
        string firstName, 
        string middleName, 
        string lastName, 
        string cellPhone, 
        string homePhone, 
        string workPhone, 
        int rowStatusID, 
        string addedBy, 
        DateTime addedDate, 
        string updatedBy, 
        DateTime updatedDate, 
        string details, 
        string extraField1, 
        string extraField2, 
        string extraField3, 
        string extraField4, 
        string extraField5, 
        string extraField6, 
        string extraField7, 
        string extraField8, 
        string extraField9, 
        string extraField10
        )
    {
        this.LoginID = loginID;
        this.LoginName = loginName;
        this.Password = password;
        this.Email = email;
        this.FirstName = firstName;
        this.MiddleName = middleName;
        this.LastName = lastName;
        this.CellPhone = cellPhone;
        this.HomePhone = homePhone;
        this.WorkPhone = workPhone;
        this.RowStatusID = rowStatusID;
        this.AddedBy = addedBy;
        this.AddedDate = addedDate;
        this.UpdatedBy = updatedBy;
        this.UpdatedDate = updatedDate;
        this.Details = details;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.ExtraField3 = extraField3;
        this.ExtraField4 = extraField4;
        this.ExtraField5 = extraField5;
        this.ExtraField6 = extraField6;
        this.ExtraField7 = extraField7;
        this.ExtraField8 = extraField8;
        this.ExtraField9 = extraField9;
        this.ExtraField10 = extraField10;
    }


    private int _loginID;
    public int LoginID
    {
        get { return _loginID; }
        set { _loginID = value; }
    }

    private string _loginName;
    public string LoginName
    {
        get { return _loginName; }
        set { _loginName = value; }
    }

    private string _password;
    public string Password
    {
        get { return _password; }
        set { _password = value; }
    }

    private string _email;
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    private string _firstName;
    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    private string _middleName;
    public string MiddleName
    {
        get { return _middleName; }
        set { _middleName = value; }
    }

    private string _lastName;
    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    private string _cellPhone;
    public string CellPhone
    {
        get { return _cellPhone; }
        set { _cellPhone = value; }
    }

    private string _homePhone;
    public string HomePhone
    {
        get { return _homePhone; }
        set { _homePhone = value; }
    }

    private string _workPhone;
    public string WorkPhone
    {
        get { return _workPhone; }
        set { _workPhone = value; }
    }

    private int _rowStatusID;
    public int RowStatusID
    {
        get { return _rowStatusID; }
        set { _rowStatusID = value; }
    }

    private string _addedBy;
    public string AddedBy
    {
        get { return _addedBy; }
        set { _addedBy = value; }
    }

    private DateTime _addedDate;
    public DateTime AddedDate
    {
        get { return _addedDate; }
        set { _addedDate = value; }
    }

    private string _updatedBy;
    public string UpdatedBy
    {
        get { return _updatedBy; }
        set { _updatedBy = value; }
    }

    private DateTime _updatedDate;
    public DateTime UpdatedDate
    {
        get { return _updatedDate; }
        set { _updatedDate = value; }
    }

    private string _details;
    public string Details
    {
        get { return _details; }
        set { _details = value; }
    }


    
    private string _extraField1;
    /// <summary>
    /// Role IDs (, separated)
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// Default Page
    /// </summary>
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }

    private string _extraField3;
    /// <summary>
    /// Property
    /// </summary>
    public string ExtraField3
    {
        get { return _extraField3; }
        set { _extraField3 = value; }
    }

    private string _extraField4;
    /// <summary>
    /// Initail
    /// </summary>
    public string ExtraField4
    {
        get { return _extraField4; }
        set { _extraField4 = value; }
    }

    private string _extraField5;
    /// <summary>
    /// WorkStation ID
    /// </summary>
    public string ExtraField5
    {
        get { return _extraField5; }
        set { _extraField5 = value; }
    }

    private string _extraField6;
    public string ExtraField6
    {
        get { return _extraField6; }
        set { _extraField6 = value; }
    }

    private string _extraField7;
    public string ExtraField7
    {
        get { return _extraField7; }
        set { _extraField7 = value; }
    }

    private string _extraField8;
    public string ExtraField8
    {
        get { return _extraField8; }
        set { _extraField8 = value; }
    }

    private string _extraField9;
    public string ExtraField9
    {
        get { return _extraField9; }
        set { _extraField9 = value; }
    }

    private string _extraField10;
    public string ExtraField10
    {
        get { return _extraField10; }
        set { _extraField10 = value; }
    }
}
