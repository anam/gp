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

public class Property
{
    public Property()
    {
    }

    public Property
        (
        int propertyID, 
        string propertyName, 
        string address, 
        int countryID, 
        int stateID, 
        int cityID, 
        string extraField1, 
        string extraField2, 
        string extraField3, 
        string extraField4, 
        string extraField5, 
        string extraField6, 
        string extraField7, 
        string extraField8, 
        string extraField9, 
        string extraField10
        )
    {
        this.PropertyID = propertyID;
        this.PropertyName = propertyName;
        this.Address = address;
        this.CountryID = countryID;
        this.StateID = stateID;
        this.CityID = cityID;
        this.ExtraField1 = extraField1;
        this.ExtraField2 = extraField2;
        this.ExtraField3 = extraField3;
        this.ExtraField4 = extraField4;
        this.ExtraField5 = extraField5;
        this.ExtraField6 = extraField6;
        this.ExtraField7 = extraField7;
        this.ExtraField8 = extraField8;
        this.ExtraField9 = extraField9;
        this.ExtraField10 = extraField10;
    }


    private int _propertyID;
    public int PropertyID
    {
        get { return _propertyID; }
        set { _propertyID = value; }
    }

    private string _propertyName;
    public string PropertyName
    {
        get { return _propertyName; }
        set { _propertyName = value; }
    }

    private string _address;
    public string Address
    {
        get { return _address; }
        set { _address = value; }
    }

    private int _countryID;
    public int CountryID
    {
        get { return _countryID; }
        set { _countryID = value; }
    }

    private int _stateID;
    public int StateID
    {
        get { return _stateID; }
        set { _stateID = value; }
    }

    private int _cityID;
    public int CityID
    {
        get { return _cityID; }
        set { _cityID = value; }
    }

   
    private string _extraField1;
    /// <summary>
    /// City
    /// </summary>
    public string ExtraField1
    {
        get { return _extraField1; }
        set { _extraField1 = value; }
    }

    private string _extraField2;
    /// <summary>
    /// Zip
    /// </summary>
    public string ExtraField2
    {
        get { return _extraField2; }
        set { _extraField2 = value; }
    }
    
    private string _extraField3;
    /// <summary>
    /// State Name
    /// </summary>
    public string ExtraField3
    {
        get { return _extraField3; }
        set { _extraField3 = value; }
    }


   
    
    private string _extraField4;
    /// <summary>
    /// Customised Property ID
    /// </summary>
    public string ExtraField4
    {
        get { return _extraField4; }
        set { _extraField4 = value; }
    }

    private string _extraField5;
    /// <summary>
    /// Company ID
    /// </summary>
    public string ExtraField5
    {
        get { return _extraField5; }
        set { _extraField5 = value; }
    }

    private string _extraField6;
    /// <summary>
    /// Company Name at display time
    /// </summary>
    public string ExtraField6
    {
        get { return _extraField6; }
        set { _extraField6 = value; }
    }

    private string _extraField7;
    /// <summary>
    /// Status(Active/InActive)
    /// </summary>
    public string ExtraField7
    {
        get { return _extraField7; }
        set { _extraField7 = value; }
    }

    private string _extraField8;
    /// <summary>
    /// Contact No
    /// </summary>
    public string ExtraField8
    {
        get { return _extraField8; }
        set { _extraField8 = value; }
    }

    private string _extraField9;
    /// <summary>
    /// Contact person
    /// </summary>
    public string ExtraField9
    {
        get { return _extraField9; }
        set { _extraField9 = value; }
    }

    private string _extraField10;
    public string ExtraField10
    {
        get { return _extraField10; }
        set { _extraField10 = value; }
    }
}
