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

public partial class AdminPos_TransactionTypeInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["pos_TransactionTypeID"] != null)
            {
                int pos_TransactionTypeID = Int32.Parse(Request.QueryString["pos_TransactionTypeID"]);
                if (pos_TransactionTypeID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_TransactionTypeData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_TransactionType pos_TransactionType = new Pos_TransactionType();

        pos_TransactionType.TransactionTypeName = txtTransactionTypeName.Text;
        pos_TransactionType.CentralStockFormula = txtCentralStockFormula.Text;
        pos_TransactionType.ShowRoomFormula = txtShowRoomFormula.Text;
        pos_TransactionType.ExtraField1 = txtExtraField1.Text;
        pos_TransactionType.ExtraField2 = txtExtraField2.Text;
        pos_TransactionType.ExtraField3 = txtExtraField3.Text;
        int resutl = Pos_TransactionTypeManager.InsertPos_TransactionType(pos_TransactionType);
        Response.Redirect("AdminPos_TransactionTypeDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_TransactionType pos_TransactionType = new Pos_TransactionType();
        pos_TransactionType = Pos_TransactionTypeManager.GetPos_TransactionTypeByID(Int32.Parse(Request.QueryString["pos_TransactionTypeID"]));
        Pos_TransactionType tempPos_TransactionType = new Pos_TransactionType();
        tempPos_TransactionType.Pos_TransactionTypeID = pos_TransactionType.Pos_TransactionTypeID;

        tempPos_TransactionType.TransactionTypeName = txtTransactionTypeName.Text;
        tempPos_TransactionType.CentralStockFormula = txtCentralStockFormula.Text;
        tempPos_TransactionType.ShowRoomFormula = txtShowRoomFormula.Text;
        tempPos_TransactionType.ExtraField1 = txtExtraField1.Text;
        tempPos_TransactionType.ExtraField2 = txtExtraField2.Text;
        tempPos_TransactionType.ExtraField3 = txtExtraField3.Text;
        bool result = Pos_TransactionTypeManager.UpdatePos_TransactionType(tempPos_TransactionType);
        Response.Redirect("AdminPos_TransactionTypeDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtTransactionTypeName.Text = "";
        txtCentralStockFormula.Text = "";
        txtShowRoomFormula.Text = "";
        txtExtraField1.Text = "";
        txtExtraField2.Text = "";
        txtExtraField3.Text = "";
    }
    private void showPos_TransactionTypeData()
    {
        Pos_TransactionType pos_TransactionType = new Pos_TransactionType();
        pos_TransactionType = Pos_TransactionTypeManager.GetPos_TransactionTypeByID(Int32.Parse(Request.QueryString["pos_TransactionTypeID"]));

        txtTransactionTypeName.Text = pos_TransactionType.TransactionTypeName;
        txtCentralStockFormula.Text = pos_TransactionType.CentralStockFormula;
        txtShowRoomFormula.Text = pos_TransactionType.ShowRoomFormula;
        txtExtraField1.Text = pos_TransactionType.ExtraField1;
        txtExtraField2.Text = pos_TransactionType.ExtraField2;
        txtExtraField3.Text = pos_TransactionType.ExtraField3;
    }
}
