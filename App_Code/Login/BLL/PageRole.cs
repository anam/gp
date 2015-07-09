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

public class PageRole
{
    public PageRole()
    {
    }

    public PageRole
        (
        int pageRoleID, 
        int pageID, 
        int roleID, 
        DateTime addedDate, 
        string addedBy, 
        DateTime modifyDate, 
        string modifyBy, 
        int rowStatusID
        )
    {
        this.PageRoleID = pageRoleID;
        this.PageID = pageID;
        this.RoleID = roleID;
        this.AddedDate = addedDate;
        this.AddedBy = addedBy;
        this.ModifyDate = ModifyDate;
        this.ModifyBy = modifyBy;
        this.RowStatusID = rowStatusID;
    }


    private int _pageRoleID;
    public int PageRoleID
    {
        get { return _pageRoleID; }
        set { _pageRoleID = value; }
    }

    private int _pageID;
    public int PageID
    {
        get { return _pageID; }
        set { _pageID = value; }
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
