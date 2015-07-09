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

public class MenuRole
{
    public MenuRole()
    {
    }

    public MenuRole
        (
        int menuRoleID, 
        int menuID, 
        int roleID, 
        DateTime addedDate, 
        string addedBy, 
        DateTime modifyDate, 
        string modifyBy, 
        int rowStatusID
        )
    {
        this.MenuRoleID = menuRoleID;
        this.MenuID = menuID;
        this.RoleID = roleID;
        this.AddedDate = addedDate;
        this.AddedBy = addedBy;
        this.ModifyDate = ModifyDate;
        this.ModifyBy = modifyBy;
        this.RowStatusID = rowStatusID;
    }


    private int _menuRoleID;
    public int MenuRoleID
    {
        get { return _menuRoleID; }
        set { _menuRoleID = value; }
    }

    private int _menuID;
    public int MenuID
    {
        get { return _menuID; }
        set { _menuID = value; }
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
