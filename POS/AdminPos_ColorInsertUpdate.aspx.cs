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

public partial class AdminPos_ColorInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["pos_ColorID"] != null)
            {
                int pos_ColorID = Int32.Parse(Request.QueryString["pos_ColorID"]);
                if (pos_ColorID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_ColorData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_Color pos_Color = new Pos_Color();

        pos_Color.ColorName = txtColorName.Text;
        pos_Color.ColorCode = txtColorCode.Text;
        int resutl = Pos_ColorManager.InsertPos_Color(pos_Color);
        Response.Redirect("AdminPos_ColorDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_Color pos_Color = new Pos_Color();
        pos_Color = Pos_ColorManager.GetPos_ColorByID(Int32.Parse(Request.QueryString["pos_ColorID"]));
        Pos_Color tempPos_Color = new Pos_Color();
        tempPos_Color.Pos_ColorID = pos_Color.Pos_ColorID;

        tempPos_Color.ColorName = txtColorName.Text;
        tempPos_Color.ColorCode = txtColorCode.Text;
        bool result = Pos_ColorManager.UpdatePos_Color(tempPos_Color);
        Response.Redirect("AdminPos_ColorDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtColorName.Text = "";
        txtColorCode.Text = "";
    }
    private void showPos_ColorData()
    {
        Pos_Color pos_Color = new Pos_Color();
        pos_Color = Pos_ColorManager.GetPos_ColorByID(Int32.Parse(Request.QueryString["pos_ColorID"]));

        txtColorName.Text = pos_Color.ColorName;
        txtColorCode.Text = pos_Color.ColorCode;
    }
}
