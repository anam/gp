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

public partial class AdminPos_ProductTypeInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["pos_ProductTypeID"] != null)
            {
                int pos_ProductTypeID = Int32.Parse(Request.QueryString["pos_ProductTypeID"]);
                if (pos_ProductTypeID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_ProductTypeData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_ProductType pos_ProductType = new Pos_ProductType();

        pos_ProductType.ProductTypeName = txtProductTypeName.Text;
        int resutl = Pos_ProductTypeManager.InsertPos_ProductType(pos_ProductType);
        Response.Redirect("AdminPos_ProductTypeDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_ProductType pos_ProductType = new Pos_ProductType();
        pos_ProductType = Pos_ProductTypeManager.GetPos_ProductTypeByID(Int32.Parse(Request.QueryString["pos_ProductTypeID"]));
        Pos_ProductType tempPos_ProductType = new Pos_ProductType();
        tempPos_ProductType.Pos_ProductTypeID = pos_ProductType.Pos_ProductTypeID;

        tempPos_ProductType.ProductTypeName = txtProductTypeName.Text;
        bool result = Pos_ProductTypeManager.UpdatePos_ProductType(tempPos_ProductType);
        Response.Redirect("AdminPos_ProductTypeDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtProductTypeName.Text = "";
    }
    private void showPos_ProductTypeData()
    {
        Pos_ProductType pos_ProductType = new Pos_ProductType();
        pos_ProductType = Pos_ProductTypeManager.GetPos_ProductTypeByID(Int32.Parse(Request.QueryString["pos_ProductTypeID"]));

        txtProductTypeName.Text = pos_ProductType.ProductTypeName;
    }
}
