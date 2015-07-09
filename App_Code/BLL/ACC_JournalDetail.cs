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

public class ACC_JournalDetail
{
    public ACC_JournalDetail()
    {
    }

    public ACC_JournalDetail
        (
        int aCC_JournalDetailID, 
        int journalMasterID, 
        int aCC_ChartOfAccountLabel4ID, 
        int aCC_ChartOfAccountLabel3ID, 
        int workStation, 
        decimal debit, 
        decimal credit, 
        string extraField3, 
        string extraField2, 
        string extraField1, 
        int addedBy, 
        DateTime addedDate, 
        int updatedBy, 
        DateTime updatedDate, 
        int rowStatusID
        )
    {
        this.ACC_JournalDetailID = aCC_JournalDetailID;
        this.JournalMasterID = journalMasterID;
        this.ACC_ChartOfAccountLabel4ID = aCC_ChartOfAccountLabel4ID;
        this.ACC_ChartOfAccountLabel3ID = aCC_ChartOfAccountLabel3ID;
        this.WorkStation = workStation;
        this.Debit = debit;
        this.Credit = credit;
        this.ExtraField3 = extraField3;
        this.ExtraField2 = extraField2;
        this.ExtraField1 = extraField1;
        this.AddedBy = addedBy;
        this.AddedDate = addedDate;
        this.UpdatedBy = updatedBy;
        this.UpdatedDate = updatedDate;
        this.RowStatusID = rowStatusID;
    }


    private int _aCC_JournalDetailID;
    public int ACC_JournalDetailID
    {
        get { return _aCC_JournalDetailID; }
        set { _aCC_JournalDetailID = value; }
    }

    private int _journalMasterID;
    public int JournalMasterID
    {
        get { return _journalMasterID; }
        set { _journalMasterID = value; }
    }

    private int _aCC_ChartOfAccountLabel4ID;
    public int ACC_ChartOfAccountLabel4ID
    {
        get { return _aCC_ChartOfAccountLabel4ID; }
        set { _aCC_ChartOfAccountLabel4ID = value; }
    }

    private string _aCC_ChartOfAccountLabel4Text;

    public string ACC_ChartOfAccountLabel4Text
    {
        get { return _aCC_ChartOfAccountLabel4Text; }
        set { _aCC_ChartOfAccountLabel4Text = value; }
    }

    private string _aCC_ChartOfAccountLabel4Code;

    public string ACC_ChartOfAccountLabel4Code
    {
        get { return _aCC_ChartOfAccountLabel4Code; }
        set { _aCC_ChartOfAccountLabel4Code = value; }
    }

    private string _aCC_ChartOfAccountLabel3Code;

    public string ACC_ChartOfAccountLabel3Code
    {
        get { return _aCC_ChartOfAccountLabel3Code; }
        set { _aCC_ChartOfAccountLabel3Code = value; }
    }

    private string _aCC_ChartOfAccountLabel3Text;

    public string ACC_ChartOfAccountLabel3Text
    {
        get { return _aCC_ChartOfAccountLabel3Text; }
        set { _aCC_ChartOfAccountLabel3Text = value; }
    }


    private int _aCC_ChartOfAccountLabel3ID;
    public int ACC_ChartOfAccountLabel3ID
    {
        get { return _aCC_ChartOfAccountLabel3ID; }
        set { _aCC_ChartOfAccountLabel3ID = value; }
    }

    private int _workStation;
    public int WorkStation
    {
        get { return _workStation; }
        set { _workStation = value; }
    }
    private string _workStationName;

    public string WorkStationName
    {
        get { return _workStationName; }
        set { _workStationName = value; }
    }

    private decimal _debit;
    public decimal Debit
    {
        get { return _debit; }
        set { _debit = value; }
    }

    private decimal _credit;
    public decimal Credit
    {
        get { return _credit; }
        set { _credit = value; }
    }

    private string _extraField3;
    /// <summary>
    /// check no  & TransactionID of Pos_Transaction table & Card Type
    /// </summary>
    public string ExtraField3
    {
        get { return _extraField3; }
        set { _extraField3 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// Bank and branch details  & For POS
    /// </summary>
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }

    private string _extraField1;
    /// <summary>
    /// Check Date / Inv_ItemID  / Card No
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
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

    private DateTime journalDate;

    public DateTime JournalDate
    {
        get { return journalDate; }
        set { journalDate = value; }
    }

    private string journalMasterName;
    /// <summary>
    /// Vourcher type
    /// </summary>
    public string JournalMasterName
    {
        get { return journalMasterName; }
        set { journalMasterName = value; }
    }
}
