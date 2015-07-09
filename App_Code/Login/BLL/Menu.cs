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

public class Menu
{
    public Menu()
    {
    }

    public Menu
        (
        int menuID, 
        string menuTitle, 
        int moduleID, 
        int pageID, 
        int parentMenuID, 
        string addedBy, 
        DateTime addedDate, 
        string updatedBy, 
        DateTime updatedDate, 
        int rowStatusID
        )
    {
        this.MenuID = menuID;
        this.MenuTitle = menuTitle;
        this.ModuleID = moduleID;
        this.PageID = pageID;
        this.ParentMenuID = parentMenuID;
        this.AddedBy = addedBy;
        this.AddedDate = addedDate;
        this.UpdatedBy = updatedBy;
        this.UpdatedDate = updatedDate;
        this.RowStatusID = rowStatusID;
    }


    private int _menuID;
    public int MenuID
    {
        get { return _menuID; }
        set { _menuID = value; }
    }

    private string _menuTitle;
    public string MenuTitle
    {
        get { return _menuTitle; }
        set { _menuTitle = value; }
    }

    private int _moduleID;
    public int ModuleID
    {
        get { return _moduleID; }
        set { _moduleID = value; }
    }

    private int _pageID;
    public int PageID
    {
        get { return _pageID; }
        set { _pageID = value; }
    }

    private int _parentMenuID;
    public int ParentMenuID
    {
        get { return _parentMenuID; }
        set { _parentMenuID = value; }
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

    private int _rowStatusID;
    public int RowStatusID
    {
        get { return _rowStatusID; }
        set { _rowStatusID = value; }
    }
    public decimal MenuOrder
    { set; get; }
}
