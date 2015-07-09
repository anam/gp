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

public partial class AdminPos_BrandInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["pos_BrandID"] != null)
            {
                int pos_BrandID = Int32.Parse(Request.QueryString["pos_BrandID"]);
                if (pos_BrandID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_BrandData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_Brand pos_Brand = new Pos_Brand();

        pos_Brand.BrandName = txtBrandName.Text;
        pos_Brand.Details = txtDetails.Text;
        int resutl = Pos_BrandManager.InsertPos_Brand(pos_Brand);
        Response.Redirect("AdminPos_BrandDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_Brand pos_Brand = new Pos_Brand();
        pos_Brand = Pos_BrandManager.GetPos_BrandByID(Int32.Parse(Request.QueryString["pos_BrandID"]));
        Pos_Brand tempPos_Brand = new Pos_Brand();
        tempPos_Brand.Pos_BrandID = pos_Brand.Pos_BrandID;

        tempPos_Brand.BrandName = txtBrandName.Text;
        tempPos_Brand.Details = txtDetails.Text;
        bool result = Pos_BrandManager.UpdatePos_Brand(tempPos_Brand);
        Response.Redirect("AdminPos_BrandDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtBrandName.Text = "";
        txtDetails.Text = "";
    }
    private void showPos_BrandData()
    {
        Pos_Brand pos_Brand = new Pos_Brand();
        pos_Brand = Pos_BrandManager.GetPos_BrandByID(Int32.Parse(Request.QueryString["pos_BrandID"]));

        txtBrandName.Text = pos_Brand.BrandName;
        txtDetails.Text = pos_Brand.Details;
    }
}
