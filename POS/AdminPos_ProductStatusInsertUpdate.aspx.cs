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

public partial class AdminPos_ProductStatusInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["pos_ProductStatusID"] != null)
            {
                int pos_ProductStatusID = Int32.Parse(Request.QueryString["pos_ProductStatusID"]);
                if (pos_ProductStatusID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_ProductStatusData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_ProductStatus pos_ProductStatus = new Pos_ProductStatus();

        pos_ProductStatus.ProductStatusName = txtProductStatusName.Text;
        int resutl = Pos_ProductStatusManager.InsertPos_ProductStatus(pos_ProductStatus);
        Response.Redirect("AdminPos_ProductStatusDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_ProductStatus pos_ProductStatus = new Pos_ProductStatus();
        pos_ProductStatus = Pos_ProductStatusManager.GetPos_ProductStatusByID(Int32.Parse(Request.QueryString["pos_ProductStatusID"]));
        Pos_ProductStatus tempPos_ProductStatus = new Pos_ProductStatus();
        tempPos_ProductStatus.Pos_ProductStatusID = pos_ProductStatus.Pos_ProductStatusID;

        tempPos_ProductStatus.ProductStatusName = txtProductStatusName.Text;
        bool result = Pos_ProductStatusManager.UpdatePos_ProductStatus(tempPos_ProductStatus);
        Response.Redirect("AdminPos_ProductStatusDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtProductStatusName.Text = "";
    }
    private void showPos_ProductStatusData()
    {
        Pos_ProductStatus pos_ProductStatus = new Pos_ProductStatus();
        pos_ProductStatus = Pos_ProductStatusManager.GetPos_ProductStatusByID(Int32.Parse(Request.QueryString["pos_ProductStatusID"]));

        txtProductStatusName.Text = pos_ProductStatus.ProductStatusName;
    }
}
