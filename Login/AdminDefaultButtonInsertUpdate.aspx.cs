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

public partial class AdminDefaultButtonInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            btnUpdate.Visible = false;
            btnAdd.Visible = true;
            if (Request.QueryString["defaultButtonID"] != null)
            {
                int defaultButtonID = Int32.Parse(Request.QueryString["defaultButtonID"]);
                if (defaultButtonID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showDefaultButtonData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DefaultButton defaultButton = new DefaultButton();

        defaultButton.DefaultButtonName = txtDefaultButtonName.Text;
        defaultButton.DefaultButtonText = txtDefaultButtonText.Text;
        int resutl = DefaultButtonManager.InsertDefaultButton(defaultButton);
        Response.Redirect("AdminDefaultButtonDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DefaultButton defaultButton = new DefaultButton();
        defaultButton = DefaultButtonManager.GetDefaultButtonByID(Int32.Parse(Request.QueryString["defaultButtonID"]));
        DefaultButton tempDefaultButton = new DefaultButton();
        tempDefaultButton.DefaultButtonID = defaultButton.DefaultButtonID;

        tempDefaultButton.DefaultButtonName = txtDefaultButtonName.Text;
        tempDefaultButton.DefaultButtonText = txtDefaultButtonText.Text;
        bool result = DefaultButtonManager.UpdateDefaultButton(tempDefaultButton);
        Response.Redirect("AdminDefaultButtonDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtDefaultButtonName.Text = "";
        txtDefaultButtonText.Text = "";
    }
    private void showDefaultButtonData()
    {
        DefaultButton defaultButton = new DefaultButton();
        defaultButton = DefaultButtonManager.GetDefaultButtonByID(Int32.Parse(Request.QueryString["defaultButtonID"]));

        txtDefaultButtonName.Text = defaultButton.DefaultButtonName;
        txtDefaultButtonText.Text = defaultButton.DefaultButtonText;
    }
}
