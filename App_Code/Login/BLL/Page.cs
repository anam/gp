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

public class Page
{
    public Page()
    {
    }

    public Page
        (
        int pageID, 
        string pageTitle, 
        string pageURL, 
        int moduleID, 
        string addedBy, 
        DateTime addedDate, 
        string updatedBy, 
        DateTime updatedDate, 
        int rowStatusID
        )
    {
        this.PageID = pageID;
        this.PageTitle = pageTitle;
        this.PageURL = pageURL;
        this.ModuleID = moduleID;
        this.AddedBy = addedBy;
        this.AddedDate = addedDate;
        this.UpdatedBy = updatedBy;
        this.UpdatedDate = updatedDate;
        this.RowStatusID = rowStatusID;
    }


    private int _pageID;
    public int PageID
    {
        get { return _pageID; }
        set { _pageID = value; }
    }

    private string _pageTitle;
    public string PageTitle
    {
        get { return _pageTitle; }
        set { _pageTitle = value; }
    }

    private string _pageURL;
    public string PageURL
    {
        get { return _pageURL; }
        set { _pageURL = value; }
    }

    private int _moduleID;
    public int ModuleID
    {
        get { return _moduleID; }
        set { _moduleID = value; }
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
}
