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

public partial class AdminPos_FabricsTypeInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["pos_FabricsTypeID"] != null)
            {
                int pos_FabricsTypeID = Int32.Parse(Request.QueryString["pos_FabricsTypeID"]);
                if (pos_FabricsTypeID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_FabricsTypeData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_FabricsType pos_FabricsType = new Pos_FabricsType();

        pos_FabricsType.FabricsTypeName = txtFabricsTypeName.Text;
        int resutl = Pos_FabricsTypeManager.InsertPos_FabricsType(pos_FabricsType);
        Response.Redirect("AdminPos_FabricsTypeDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_FabricsType pos_FabricsType = new Pos_FabricsType();
        pos_FabricsType = Pos_FabricsTypeManager.GetPos_FabricsTypeByID(Int32.Parse(Request.QueryString["pos_FabricsTypeID"]));
        Pos_FabricsType tempPos_FabricsType = new Pos_FabricsType();
        tempPos_FabricsType.Pos_FabricsTypeID = pos_FabricsType.Pos_FabricsTypeID;

        tempPos_FabricsType.FabricsTypeName = txtFabricsTypeName.Text;
        bool result = Pos_FabricsTypeManager.UpdatePos_FabricsType(tempPos_FabricsType);
        Response.Redirect("AdminPos_FabricsTypeDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtFabricsTypeName.Text = "";
    }
    private void showPos_FabricsTypeData()
    {
        Pos_FabricsType pos_FabricsType = new Pos_FabricsType();
        pos_FabricsType = Pos_FabricsTypeManager.GetPos_FabricsTypeByID(Int32.Parse(Request.QueryString["pos_FabricsTypeID"]));

        txtFabricsTypeName.Text = pos_FabricsType.FabricsTypeName;
    }
}
