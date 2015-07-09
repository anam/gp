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

public partial class AdminACC_HeadTypeInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["aCC_HeadTypeID"] != null)
            {
                int aCC_HeadTypeID = Int32.Parse(Request.QueryString["aCC_HeadTypeID"]);
                if (aCC_HeadTypeID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showACC_HeadTypeData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ACC_HeadType aCC_HeadType = new ACC_HeadType();

        aCC_HeadType.HeadTypeName = txtHeadTypeName.Text;
        aCC_HeadType.ExtraField1 = txtExtraField1.Text;
        aCC_HeadType.ExtraField2 = txtExtraField2.Text;
        aCC_HeadType.ExtraField3 = txtExtraField3.Text;
        int resutl = ACC_HeadTypeManager.InsertACC_HeadType(aCC_HeadType);
        Response.Redirect("AdminACC_HeadTypeDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ACC_HeadType aCC_HeadType = new ACC_HeadType();
        aCC_HeadType = ACC_HeadTypeManager.GetACC_HeadTypeByID(Int32.Parse(Request.QueryString["aCC_HeadTypeID"]));
        ACC_HeadType tempACC_HeadType = new ACC_HeadType();
        tempACC_HeadType.ACC_HeadTypeID = aCC_HeadType.ACC_HeadTypeID;

        tempACC_HeadType.HeadTypeName = txtHeadTypeName.Text;
        tempACC_HeadType.ExtraField1 = txtExtraField1.Text;
        tempACC_HeadType.ExtraField2 = txtExtraField2.Text;
        tempACC_HeadType.ExtraField3 = txtExtraField3.Text;
        bool result = ACC_HeadTypeManager.UpdateACC_HeadType(tempACC_HeadType);
        Response.Redirect("AdminACC_HeadTypeDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtHeadTypeName.Text = "";
        txtExtraField1.Text = "";
        txtExtraField2.Text = "";
        txtExtraField3.Text = "";
    }
    private void showACC_HeadTypeData()
    {
        ACC_HeadType aCC_HeadType = new ACC_HeadType();
        aCC_HeadType = ACC_HeadTypeManager.GetACC_HeadTypeByID(Int32.Parse(Request.QueryString["aCC_HeadTypeID"]));

        txtHeadTypeName.Text = aCC_HeadType.HeadTypeName;
        txtExtraField1.Text = aCC_HeadType.ExtraField1;
        txtExtraField2.Text = aCC_HeadType.ExtraField2;
        txtExtraField3.Text = aCC_HeadType.ExtraField3;
    }
}
