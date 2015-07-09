using System;
using System.Collections.Generic;
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

public class StateManager
{
	public StateManager()
	{
	}

    public static List<State> GetAllStates()
    {
        List<State> states = new List<State>();
        SqlStateProvider sqlStateProvider = new SqlStateProvider();
        states = sqlStateProvider.GetAllStates();
        return states;
    }


    public static State GetStateByID(int id)
    {
        State state = new State();
        SqlStateProvider sqlStateProvider = new SqlStateProvider();
        state = sqlStateProvider.GetStateByID(id);
        return state;
    }


    public static int InsertState(State state)
    {
        SqlStateProvider sqlStateProvider = new SqlStateProvider();
        return sqlStateProvider.InsertState(state);
    }


    public static bool UpdateState(State state)
    {
        SqlStateProvider sqlStateProvider = new SqlStateProvider();
        return sqlStateProvider.UpdateState(state);
    }

    public static bool DeleteState(int stateID)
    {
        SqlStateProvider sqlStateProvider = new SqlStateProvider();
        return sqlStateProvider.DeleteState(stateID);
    }
}
