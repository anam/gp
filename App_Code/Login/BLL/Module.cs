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

public class Module
{
    public Module()
    {
    }

    public Module
        (
        int moduleID, 
        string moduleName, 
        string defaultURL, 
        string addedBy, 
        DateTime addedDate, 
        string updatedBy, 
        DateTime updatedDate, 
        int rowStatusID
        )
    {
        this.ModuleID = moduleID;
        this.ModuleName = moduleName;
        this.DefaultURL = defaultURL;
        this.AddedBy = addedBy;
        this.AddedDate = addedDate;
        this.UpdatedBy = updatedBy;
        this.UpdatedDate = updatedDate;
        this.RowStatusID = rowStatusID;
    }


    private int _moduleID;
    public int ModuleID
    {
        get { return _moduleID; }
        set { _moduleID = value; }
    }

    private string _moduleName;
    public string ModuleName
    {
        get { return _moduleName; }
        set { _moduleName = value; }
    }

    public string FolderName
    { get; set; }

    private string _defaultURL;
    public string DefaultURL
    {
        get { return _defaultURL; }
        set { _defaultURL = value; }
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
