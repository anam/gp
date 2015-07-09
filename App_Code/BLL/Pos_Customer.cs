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

public class Pos_Customer
{
    public Pos_Customer()
    {
    }

    public Pos_Customer
        (
        int pos_CustomerID, 
        string cardNo, 
        string signature, 
        string customerName, 
        int referenceID, 
        string address, 
        string mobile, 
        string phone, 
        decimal discountPersent, 
        string note, 
        string extraFiled1, 
        string extraFiled2, 
        string extraFiled3, 
        string extraFiled4, 
        string extraFiled5, 
        int addedBy, 
        DateTime addedDate, 
        int updatedBy, 
        DateTime updatedDate, 
        int rowSatatusID,
        DateTime dateofbirth,
        DateTime applicationdate,
        DateTime cardissuedate,
        DateTime expiredate,
        string cardtype,
        string approvedby
        )
    {
        this.Pos_CustomerID = pos_CustomerID;
        this.CardNo = cardNo;
        this.Signature = signature;
        this.CustomerName = customerName;
        this.ReferenceID = referenceID;
        this.Address = address;
        this.Mobile = mobile;
        this.Phone = phone;
        this.DiscountPersent = discountPersent;
        this.Note = note;
        this.ExtraFiled1 = extraFiled1;
        this.ExtraFiled2 = extraFiled2;
        this.ExtraFiled3 = extraFiled3;
        this.ExtraFiled4 = extraFiled4;
        this.ExtraFiled5 = extraFiled5;
        this.AddedBy = addedBy;
        this.AddedDate = addedDate;
        this.UpdatedBy = updatedBy;
        this.UpdatedDate = updatedDate;
        this.RowSatatusID = rowSatatusID;
        this.DateofBirth = dateofbirth;
        this.ApplicationDate = applicationdate;
        this.CardIssueDate = cardissuedate;
        this.ExpireDate = expiredate;
        this.CardType = cardtype;
        this.ApprovedBy = approvedby;
    }



    private int _pos_CustomerID;
    public int Pos_CustomerID
    {
        get { return _pos_CustomerID; }
        set { _pos_CustomerID = value; }
    }

    private string _cardNo;
    public string CardNo
    {
        get { return _cardNo; }
        set { _cardNo = value; }
    }

    private string _signature;
    public string Signature
    {
        get { return _signature; }
        set { _signature = value; }
    }

    private string _customerName;
    public string CustomerName
    {
        get { return _customerName; }
        set { _customerName = value; }
    }

    private int _referenceID;
    public int ReferenceID
    {
        get { return _referenceID; }
        set { _referenceID = value; }
    }

    private string _address;
    public string Address
    {
        get { return _address; }
        set { _address = value; }
    }

    private string _mobile;
    public string Mobile
    {
        get { return _mobile; }
        set { _mobile = value; }
    }

    private string _phone;
    public string Phone
    {
        get { return _phone; }
        set { _phone = value; }
    }

    private decimal _discountPersent;
    public decimal DiscountPersent
    {
        get { return _discountPersent; }
        set { _discountPersent = value; }
    }

    private string _note;
    public string Note
    {
        get { return _note; }
        set { _note = value; }
    }

    private string _extraFiled1;
    /// <summary>
    /// Branch no
    /// </summary>
    public string ExtraFiled1
    {
        get { return _extraFiled1; }
        set { _extraFiled1 = value; }
    }

    private string _extraFiled2;
    /// <summary>
    /// Company Name
    /// </summary>
    public string ExtraFiled2
    {
        get { return _extraFiled2; }
        set { _extraFiled2 = value; }
    }

    private string _extraFiled3;
    /// <summary>
    /// Office Address
    /// </summary>
    public string ExtraFiled3
    {
        get { return _extraFiled3; }
        set { _extraFiled3 = value; }
    }

    private string _extraFiled4;
    /// <summary>
    /// Designation
    /// </summary>
    public string ExtraFiled4
    {
        get { return _extraFiled4; }
        set { _extraFiled4 = value; }
    }

    private string _extraFiled5;
    /// <summary>
    /// Occupation
    /// </summary>
    public string ExtraFiled5
    {
        get { return _extraFiled5; }
        set { _extraFiled5 = value; }
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

    private int _rowSatatusID;
    public int RowSatatusID
    {
        get { return _rowSatatusID; }
        set { _rowSatatusID = value; }
    }

    private DateTime _dateofbirth;
    public DateTime DateofBirth
    {
        get { return _dateofbirth; }
        set { _dateofbirth = value; }
    }

    private DateTime _applicationdate;
    public DateTime ApplicationDate
    {
        get { return _applicationdate; }
        set { _applicationdate = value; }
    }

    private DateTime _cardissuedate;
    public DateTime CardIssueDate
    {
        get { return _cardissuedate; }
        set { _cardissuedate = value; }
    }

    private DateTime _expireDate;
    public DateTime ExpireDate
    {

        get { return _expireDate; }
        set { _expireDate = value; }
    }

    private string _cardtype;
    public string CardType
    {

        get { return _cardtype; }
        set { _cardtype = value; }
    }

    private string _approvedby;
    public string ApprovedBy
    {

        get { return _approvedby; }
        set { _approvedby = value; }
    } 
}
