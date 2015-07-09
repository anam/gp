using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class AdminCityInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadState();
            if (Request.QueryString["cityID"] != null)
            {
                int cityID = Int32.Parse(Request.QueryString["cityID"]);
                if (cityID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showCityData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        City city = new City();

        city.CityName = txtCityName.Text;
        city.StateID = Int32.Parse(ddlState.SelectedValue);
        int resutl = CityManager.InsertCity(city);
        Response.Redirect("AdminCityDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        City city = new City();
        city = CityManager.GetCityByID(Int32.Parse(Request.QueryString["cityID"]));
        City tempCity = new City();
        tempCity.CityID = city.CityID;

        tempCity.CityName = txtCityName.Text;
        tempCity.StateID = Int32.Parse(ddlState.SelectedValue);
        bool result = CityManager.UpdateCity(tempCity);
        Response.Redirect("AdminCityDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtCityName.Text = "";
        ddlState.SelectedIndex = 0;
    }
    private void showCityData()
    {
        City city = new City();
        city = CityManager.GetCityByID(Int32.Parse(Request.QueryString["cityID"]));

        txtCityName.Text = city.CityName;
        ddlState.SelectedValue = city.StateID.ToString();
    }
    private void loadState()
    {
        ListItem li = new ListItem("Select State...", "0");
        ddlState.Items.Add(li);

        List<State> states = new List<State>();
        states = StateManager.GetAllStates();
        foreach (State state in states)
        {
            ListItem item = new ListItem(state.StateName.ToString(), state.StateID.ToString());
            ddlState.Items.Add(item);
        }
    }
}
