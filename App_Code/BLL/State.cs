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

public class State
{
    public State()
    {
    }

    public State
        (
        int stateID, 
        string stateName, 
        int countryID
        )
    {
        this.StateID = stateID;
        this.StateName = stateName;
        this.CountryID = countryID;
    }


    private int _stateID;
    public int StateID
    {
        get { return _stateID; }
        set { _stateID = value; }
    }

    private string _stateName;
    public string StateName
    {
        get { return _stateName; }
        set { _stateName = value; }
    }

    private int _countryID;
    public int CountryID
    {
        get { return _countryID; }
        set { _countryID = value; }
    }
}
