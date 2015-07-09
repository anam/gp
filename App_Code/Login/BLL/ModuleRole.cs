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

public class ModuleRole
{
    public ModuleRole()
    {
    }

    public ModuleRole
        (
        int moduleRoleID, 
        int moduleID, 
        int roleID, 
        DateTime addedDate, 
        string addedBy, 
        DateTime modifyDate, 
        string modifyBy, 
        int rowStatusID
        )
    {
        this.ModuleRoleID = moduleRoleID;
        this.ModuleID = moduleID;
        this.RoleID = roleID;
        this.AddedDate = addedDate;
        this.AddedBy = addedBy;
        this.ModifyDate = ModifyDate;
        this.ModifyBy = modifyBy;
        this.RowStatusID = rowStatusID;
    }


    private int _moduleRoleID;
    public int ModuleRoleID
    {
        get { return _moduleRoleID; }
        set { _moduleRoleID = value; }
    }

    private int _moduleID;
    public int ModuleID
    {
        get { return _moduleID; }
        set { _moduleID = value; }
    }

    private int _roleID;
    public int RoleID
    {
        get { return _roleID; }
        set { _roleID = value; }
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

    private int _rowStatusID;
    public int RowStatusID
    {
        get { return _rowStatusID; }
        set { _rowStatusID = value; }
    }
}
