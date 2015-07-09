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

public partial class AdminPos_CardBankInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadCharOfAccountLabel4();
            if (Request.QueryString["pos_CardBankID"] != null)
            {
                int pos_CardBankID = Int32.Parse(Request.QueryString["pos_CardBankID"]);
                if (pos_CardBankID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_CardBankData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_CardBank pos_CardBank = new Pos_CardBank();

        pos_CardBank.BankName = txtBankName.Text;
        pos_CardBank.Details = txtDetails.Text;
        pos_CardBank.CharOfAccountLabel4ID = Int32.Parse(ddlCharOfAccountLabel4.SelectedValue);
        int resutl = Pos_CardBankManager.InsertPos_CardBank(pos_CardBank);
        Response.Redirect("AdminPos_CardBankDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_CardBank pos_CardBank = new Pos_CardBank();
        pos_CardBank = Pos_CardBankManager.GetPos_CardBankByID(Int32.Parse(Request.QueryString["pos_CardBankID"]));
        Pos_CardBank tempPos_CardBank = new Pos_CardBank();
        tempPos_CardBank.Pos_CardBankID = pos_CardBank.Pos_CardBankID;

        tempPos_CardBank.BankName = txtBankName.Text;
        tempPos_CardBank.Details = txtDetails.Text;
        tempPos_CardBank.CharOfAccountLabel4ID = Int32.Parse(ddlCharOfAccountLabel4.SelectedValue);
        bool result = Pos_CardBankManager.UpdatePos_CardBank(tempPos_CardBank);
        Response.Redirect("AdminPos_CardBankDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtBankName.Text = "";
        txtDetails.Text = "";
        ddlCharOfAccountLabel4.SelectedIndex = 0;
    }
    private void showPos_CardBankData()
    {
        Pos_CardBank pos_CardBank = new Pos_CardBank();
        pos_CardBank = Pos_CardBankManager.GetPos_CardBankByID(Int32.Parse(Request.QueryString["pos_CardBankID"]));

        txtBankName.Text = pos_CardBank.BankName;
        txtDetails.Text = pos_CardBank.Details;
        ddlCharOfAccountLabel4.SelectedValue = pos_CardBank.CharOfAccountLabel4ID.ToString();
    }
    private void loadCharOfAccountLabel4()
    {
        ListItem li = new ListItem("Select CharOfAccountLabel4...", "0");
        ddlCharOfAccountLabel4.Items.Add(li);

        List<CharOfAccountLabel4> charOfAccountLabel4s = new List<CharOfAccountLabel4>();
        charOfAccountLabel4s = CharOfAccountLabel4Manager.GetAllCharOfAccountLabel4s();
        foreach (CharOfAccountLabel4 charOfAccountLabel4 in charOfAccountLabel4s)
        {
            ListItem item = new ListItem(charOfAccountLabel4.CharOfAccountLabel4Name.ToString(), charOfAccountLabel4.CharOfAccountLabel4ID.ToString());
            ddlCharOfAccountLabel4.Items.Add(item);
        }
    }
}
