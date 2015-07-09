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

public class Button
{
    public Button()
    {
    }

    public Button
        (
        int buttonID, 
        string buttonName, 
        string buttonText, 
        int pageID, 
        string addedBy, 
        DateTime addedDate, 
        string updatedBy, 
        DateTime updatedDate, 
        int rowStatusID
        )
    {
        this.ButtonID = buttonID;
        this.ButtonName = buttonName;
        this.ButtonText = buttonText;
        this.PageID = pageID;
        this.AddedBy = addedBy;
        this.AddedDate = addedDate;
        this.UpdatedBy = updatedBy;
        this.UpdatedDate = updatedDate;
        this.RowStatusID = rowStatusID;
    }


    private int _buttonID;
    public int ButtonID
    {
        get { return _buttonID; }
        set { _buttonID = value; }
    }

    private string _buttonName;
    public string ButtonName
    {
        get { return _buttonName; }
        set { _buttonName = value; }
    }

    private string _buttonText;
    public string ButtonText
    {
        get { return _buttonText; }
        set { _buttonText = value; }
    }

    private int _pageID;
    public int PageID
    {
        get { return _pageID; }
        set { _pageID = value; }
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
