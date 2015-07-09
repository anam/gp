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

public partial class AdminLoginInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadRowStatus();
            if (Request.QueryString["loginID"] != null)
            {
                int loginID = Int32.Parse(Request.QueryString["loginID"]);
                if (loginID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showLoginData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string loginID = "1";
        try
        {
            if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }
            
            loginID = ((Login)Session["Login"]).LoginID.ToString();
        }
        catch (Exception ex)
        { }

        Login login = new Login();

        login.LoginName = txtLoginName.Text;
        login.Password = txtPassword.Text;
        login.Email = txtEmail.Text;
        login.FirstName = txtFirstName.Text;
        login.MiddleName = txtMiddleName.Text;
        login.LastName = txtLastName.Text;
        login.CellPhone = txtCellPhone.Text;
        login.HomePhone = txtHomePhone.Text;
        login.WorkPhone = txtWorkPhone.Text;
        login.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        login.AddedBy = loginID;
        login.AddedDate = DateTime.Now;
        login.UpdatedBy = loginID;
        login.UpdatedDate = DateTime.Now;
        login.Details = txtDetails.Text;
        login.ExtraField1 = txtExtraField1.Text;
        login.ExtraField2 = txtExtraField2.Text;
        login.ExtraField3 = txtExtraField3.Text;
        login.ExtraField4 = txtExtraField4.Text;
        login.ExtraField5 = txtExtraField5.Text;
        login.ExtraField6 = txtExtraField6.Text;
        login.ExtraField7 = txtExtraField7.Text;
        login.ExtraField8 = txtExtraField8.Text;
        login.ExtraField9 = txtExtraField9.Text;
        login.ExtraField10 = txtExtraField10.Text;
        int resutl = LoginManager.InsertLogin(login);
        Response.Redirect("AdminLoginDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string loginID = "1";
        try
        {
            if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }
            
            loginID = ((Login)Session["Login"]).LoginID.ToString();
        }
        catch (Exception ex)
        { }

        Login login = new Login();
        login = LoginManager.GetLoginByID(Int32.Parse(Request.QueryString["loginID"]));
        Login tempLogin = new Login();
        tempLogin.LoginID = login.LoginID;

        tempLogin.LoginName = txtLoginName.Text;
        tempLogin.Password = txtPassword.Text;
        tempLogin.Email = txtEmail.Text;
        tempLogin.FirstName = txtFirstName.Text;
        tempLogin.MiddleName = txtMiddleName.Text;
        tempLogin.LastName = txtLastName.Text;
        tempLogin.CellPhone = txtCellPhone.Text;
        tempLogin.HomePhone = txtHomePhone.Text;
        tempLogin.WorkPhone = txtWorkPhone.Text;
        tempLogin.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        tempLogin.AddedBy = loginID;
        tempLogin.AddedDate = DateTime.Now;
        tempLogin.UpdatedBy = loginID;
        tempLogin.UpdatedDate = DateTime.Now;
        tempLogin.Details = txtDetails.Text;
        tempLogin.ExtraField1 = txtExtraField1.Text;
        tempLogin.ExtraField2 = txtExtraField2.Text;
        tempLogin.ExtraField3 = txtExtraField3.Text;
        tempLogin.ExtraField4 = txtExtraField4.Text;
        tempLogin.ExtraField5 = txtExtraField5.Text;
        tempLogin.ExtraField6 = txtExtraField6.Text;
        tempLogin.ExtraField7 = txtExtraField7.Text;
        tempLogin.ExtraField8 = txtExtraField8.Text;
        tempLogin.ExtraField9 = txtExtraField9.Text;
        tempLogin.ExtraField10 = txtExtraField10.Text;
        bool result = LoginManager.UpdateLogin(tempLogin);
        Response.Redirect("AdminLoginDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtLoginName.Text = "";
        txtPassword.Text = "";
        txtEmail.Text = "";
        txtFirstName.Text = "";
        txtMiddleName.Text = "";
        txtLastName.Text = "";
        txtCellPhone.Text = "";
        txtHomePhone.Text = "";
        txtWorkPhone.Text = "";
        ddlRowStatus.SelectedIndex = 0;
        txtAddedBy.Text = "";
        txtUpdatedBy.Text = "";
        txtUpdatedDate.Text = "";
        txtDetails.Text = "";
        txtExtraField1.Text = "";
        txtExtraField2.Text = "";
        txtExtraField3.Text = "";
        txtExtraField4.Text = "";
        txtExtraField5.Text = "";
        txtExtraField6.Text = "";
        txtExtraField7.Text = "";
        txtExtraField8.Text = "";
        txtExtraField9.Text = "";
        txtExtraField10.Text = "";
    }
    private void showLoginData()
    {
        Login login = new Login();
        login = LoginManager.GetLoginByID(Int32.Parse(Request.QueryString["loginID"]));

        txtLoginName.Text = login.LoginName;
        txtPassword.Text = login.Password;
        txtEmail.Text = login.Email;
        txtFirstName.Text = login.FirstName;
        txtMiddleName.Text = login.MiddleName;
        txtLastName.Text = login.LastName;
        txtCellPhone.Text = login.CellPhone;
        txtHomePhone.Text = login.HomePhone;
        txtWorkPhone.Text = login.WorkPhone;
        ddlRowStatus.SelectedValue = login.RowStatusID.ToString();
        txtAddedBy.Text = login.AddedBy;
        txtUpdatedBy.Text = login.UpdatedBy;
        txtUpdatedDate.Text = login.UpdatedDate;
        txtDetails.Text = login.Details;
        txtExtraField1.Text = login.ExtraField1;
        txtExtraField2.Text = login.ExtraField2;
        txtExtraField3.Text = login.ExtraField3;
        txtExtraField4.Text = login.ExtraField4;
        txtExtraField5.Text = login.ExtraField5;
        txtExtraField6.Text = login.ExtraField6;
        txtExtraField7.Text = login.ExtraField7;
        txtExtraField8.Text = login.ExtraField8;
        txtExtraField9.Text = login.ExtraField9;
        txtExtraField10.Text = login.ExtraField10;
    }
    private void loadRowStatus()
    {
        ListItem li = new ListItem("Select RowStatus...", "0");
        ddlRowStatus.Items.Add(li);

        List<RowStatus> rowStatuss = new List<RowStatus>();
        rowStatuss = RowStatusManager.GetAllRowStatuss();
        foreach (RowStatus rowStatus in rowStatuss)
        {
            ListItem item = new ListItem(rowStatus.RowStatusName.ToString(), rowStatus.RowStatusID.ToString());
            ddlRowStatus.Items.Add(item);
        }
    }
}
