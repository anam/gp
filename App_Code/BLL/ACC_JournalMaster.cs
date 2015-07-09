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

public class ACC_JournalMaster
{
    public ACC_JournalMaster()
    {
    }

    public ACC_JournalMaster
        (
        int aCC_JournalMasterID, 
        string journalMasterName, 
        string extraField1, 
        string extraField2, 
        string extraField3, 
        string note, 
        DateTime journalDate, 
        int addedBy, 
        DateTime addedDate, 
        int updatedBy, 
        DateTime updatedDate, 
        int rowStatusID
        )
    {
        this.ACC_JournalMasterID = aCC_JournalMasterID;
        this.JournalMasterName = journalMasterName;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.ExtraField3 = extraField3;
        this.Note = note;
        this.JournalDate = journalDate;
        this.AddedBy = addedBy;
        this.AddedDate = addedDate;
        this.UpdatedBy = updatedBy;
        this.UpdatedDate = updatedDate;
        this.RowStatusID = rowStatusID;
    }


    private int _aCC_JournalMasterID;
    public int ACC_JournalMasterID
    {
        get { return _aCC_JournalMasterID; }
        set { _aCC_JournalMasterID = value; }
    }

    private string _journalMasterName;
    public string JournalMasterName
    {
        get { return _journalMasterName; }
        set { _journalMasterName = value; }
    }

    private string _extraField1;

    /// <summary>
    /// txtReceivedOrPayto 
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// Address
    /// </summary>
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }

    private string _extraField3;
    /// <summary>
    /// Referrence
    /// </summary>
    public string ExtraField3
    {
        get { return _extraField3; }
        set { _extraField3 = value; }
    }

    private string _note;
    public string Note
    {
        get { return _note; }
        set { _note = value; }
    }

    private DateTime _journalDate;
    public DateTime JournalDate
    {
        get { return _journalDate; }
        set { _journalDate = value; }
    }

    private int _addedBy;
    public int AddedBy
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

    private int _updatedBy;
    public int UpdatedBy
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
