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

public partial class AdminPos_SizeInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["pos_SizeID"] != null)
            {
                int pos_SizeID = Int32.Parse(Request.QueryString["pos_SizeID"]);
                if (pos_SizeID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_SizeData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_Size pos_Size = new Pos_Size();

        pos_Size.SizeName = txtSizeName.Text;
        pos_Size.Code = txtCode.Text;
        int resutl = Pos_SizeManager.InsertPos_Size(pos_Size);
        Response.Redirect("AdminPos_SizeDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_Size pos_Size = new Pos_Size();
        pos_Size = Pos_SizeManager.GetPos_SizeByID(Int32.Parse(Request.QueryString["pos_SizeID"]));
        Pos_Size tempPos_Size = new Pos_Size();
        tempPos_Size.Pos_SizeID = pos_Size.Pos_SizeID;

        tempPos_Size.SizeName = txtSizeName.Text;
        tempPos_Size.Code = txtCode.Text;
        bool result = Pos_SizeManager.UpdatePos_Size(tempPos_Size);
        Response.Redirect("AdminPos_SizeDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtSizeName.Text = "";
        txtCode.Text = "";
    }
    private void showPos_SizeData()
    {
        Pos_Size pos_Size = new Pos_Size();
        pos_Size = Pos_SizeManager.GetPos_SizeByID(Int32.Parse(Request.QueryString["pos_SizeID"]));

        txtSizeName.Text = pos_Size.SizeName;
        txtCode.Text = pos_Size.Code;
    }
}
