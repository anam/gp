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

public partial class AdminPos_CostTypeInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["pos_CostTypeID"] != null)
            {
                int pos_CostTypeID = Int32.Parse(Request.QueryString["pos_CostTypeID"]);
                if (pos_CostTypeID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_CostTypeData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_CostType pos_CostType = new Pos_CostType();

        pos_CostType.CostTypeName = txtCostTypeName.Text;
        int resutl = Pos_CostTypeManager.InsertPos_CostType(pos_CostType);
        Response.Redirect("AdminPos_CostTypeDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_CostType pos_CostType = new Pos_CostType();
        pos_CostType = Pos_CostTypeManager.GetPos_CostTypeByID(Int32.Parse(Request.QueryString["pos_CostTypeID"]));
        Pos_CostType tempPos_CostType = new Pos_CostType();
        tempPos_CostType.Pos_CostTypeID = pos_CostType.Pos_CostTypeID;

        tempPos_CostType.CostTypeName = txtCostTypeName.Text;
        bool result = Pos_CostTypeManager.UpdatePos_CostType(tempPos_CostType);
        Response.Redirect("AdminPos_CostTypeDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtCostTypeName.Text = "";
    }
    private void showPos_CostTypeData()
    {
        Pos_CostType pos_CostType = new Pos_CostType();
        pos_CostType = Pos_CostTypeManager.GetPos_CostTypeByID(Int32.Parse(Request.QueryString["pos_CostTypeID"]));

        txtCostTypeName.Text = pos_CostType.CostTypeName;
    }
}
