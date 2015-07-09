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

public class LoginRole
{
    public LoginRole()
    {
    }

    public LoginRole
        (
        int loginRoleID, 
        int roleID, 
        int loginID, 
        int rowStatusID, 
        DateTime addedDate, 
        string addedBy, 
        DateTime modifyDate, 
        string modifyBy
        )
    {
        this.LoginRoleID = loginRoleID;
        this.RoleID = roleID;
        this.LoginID = loginID;
        this.RowStatusID = rowStatusID;
        this.AddedDate = addedDate;
        this.AddedBy = addedBy;
        this.ModifyDate = ModifyDate;
        this.ModifyBy = modifyBy;
    }


    private int _loginRoleID;
    public int LoginRoleID
    {
        get { return _loginRoleID; }
        set { _loginRoleID = value; }
    }

    private int _roleID;
    public int RoleID
    {
        get { return _roleID; }
        set { _roleID = value; }
    }

    private int _loginID;
    public int LoginID
    {
        get { return _loginID; }
        set { _loginID = value; }
    }

    private int _rowStatusID;
    public int RowStatusID
    {
        get { return _rowStatusID; }
        set { _rowStatusID = value; }
    }

    private DateTime _addedDate;
    public DateTime AddedDate
    {
        get { return _addedDate; }
        set { _addedDate = value; }
    }

    private string _addedBy;
    public string AddedBy
    {
        get { return _addedBy; }
        set { _addedBy = value; }
    }

    private DateTime _modifyDate;
    public DateTime ModifyDate
    {
        get { return _modifyDate; }
        set { _modifyDate = value; }
    }

    private string _modifyBy;
    public string ModifyBy
    {
        get { return _modifyBy; }
        set { _modifyBy = value; }
    }
}
