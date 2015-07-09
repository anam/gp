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
            loadACC_ChartOfAccountLabel4();
            loadRowStatus();
            loadMenu();
            ddlRowStatus.SelectedIndex = 1;
            loadRoleGrid();
            loadPropertyGrid();
            btnUpdate.Visible = false;
            btnAdd.Visible = true;

            trOldPassword.Visible = false;
            trPasswordEmptyMessage.Visible = false;

            if (Request.QueryString["loginID"] != null)
            {
                int loginID = Int32.Parse(Request.QueryString["loginID"]);
                if (loginID != 0)
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showLoginData();
                    trOldPassword.Visible = true;
                    trPasswordEmptyMessage.Visible = true;
                }
            }
            else
            { 
            ddlMenuID.SelectedValue= "Resident/AdminResidentDisplay.aspx";
            }

            loadRoleWiseControl();
        }
    }

    private void loadACC_ChartOfAccountLabel4()
    {

        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlWorkSatation.Items.Add(new ListItem("Select WorkStation", "0"));

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlWorkSatation.Items.Add(item);
            }

        }


    }


    private void loadRoleWiseControl()
    {
        ddlMenuID.Enabled = ButtonManager.GetAllButtonsByPageURLnUserIDnButtonName("ddlMenuID", HttpContext.Current.Request.Url.AbsoluteUri, getLogin().LoginID.ToString());
        dlRole.Visible = ButtonManager.GetAllButtonsByPageURLnUserIDnButtonName("dlRole", HttpContext.Current.Request.Url.AbsoluteUri, getLogin().LoginID.ToString());
        ddlWorkSatation.Enabled = ButtonManager.GetAllButtonsByPageURLnUserIDnButtonName("ddlWorkSatation", HttpContext.Current.Request.Url.AbsoluteUri, getLogin().LoginID.ToString());
        if (dlRole.Visible)
        {
            foreach (DataListItem item in dlRole.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Text == "Admin")
                {
                    chkSelect.Visible = ButtonManager.GetAllButtonsByPageURLnUserIDnButtonName("dlRole_Admin", HttpContext.Current.Request.Url.AbsoluteUri, getLogin().LoginID.ToString());
                }

                if (chkSelect.Text == "Developer")
                {
                    chkSelect.Visible = ButtonManager.GetAllButtonsByPageURLnUserIDnButtonName("dlRole_Developer", HttpContext.Current.Request.Url.AbsoluteUri, getLogin().LoginID.ToString());
                }
            }
        }
    }

    private Login getLogin()
    {
        Login login = new Login();
        try
        {
            if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }

            login = (Login)Session["Login"];
        }
        catch (Exception ex)
        { }

        return login;
    }

    private void loadRoleGrid()
    {
        dlRole.DataSource = RoleManager.GetAllRoles();
        dlRole.DataBind();
    }

    private void loadPropertyGrid()
    {
        
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
        if (LoginManager.GetLoginByLoginName(txtLoginName.Text) != null)
        {
            lblMsg.Text = "Login Name dulplicate<br/>";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return;
        }
        login.LoginName = txtLoginName.Text;

        if(txtPassword.Text != txtPasswordConfirm.Text)
        {
            lblMsg.Text = "Password and Confrim Password Does not match<br/>";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return;
        }

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
        login.ExtraField1 = getSelectedRoleIds();
        login.ExtraField2 = ddlMenuID.SelectedValue;
        login.ExtraField3 = getSelectedPropertyIDs(); 
        login.ExtraField4 = txtInitial.Text;
        login.ExtraField5 = ddlWorkSatation.SelectedValue;
        login.ExtraField6 = "";
        login.ExtraField7 = "";
        login.ExtraField8 = "";
        login.ExtraField9 = "";
        login.ExtraField10 = "";
        int resutl = LoginManager.InsertLogin(login);

        lblMsg.Text = "Added Successfully<br/>";
        lblMsg.ForeColor = System.Drawing.Color.Green;
        //Response.Redirect("AdminLoginDisplay.aspx");
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

        if (hfLoginName.Value != txtLoginName.Text)
        {
            if (LoginManager.GetLoginByLoginName(txtLoginName.Text) != null)
            {
                lblMsg.Text = "Login Name dulplicate<br/>";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        tempLogin.LoginName = txtLoginName.Text;

        if (txtPassword.Text != "")
        {
            if (hfPassword.Value != txtOldPassword.Text)
            {
                lblMsg.Text = "Old Password Does not match<br/>";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            tempLogin.Password = txtPassword.Text;
        }
        else
        {
            tempLogin.Password = login.Password;
        }
        
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
        tempLogin.ExtraField1 = getSelectedRoleIds();
        tempLogin.ExtraField2 = ddlMenuID.SelectedValue;
        tempLogin.ExtraField3 = getSelectedPropertyIDs();
        tempLogin.ExtraField4 = txtInitial.Text;
        tempLogin.ExtraField5 = ddlWorkSatation.SelectedValue;
        tempLogin.ExtraField6 = "";
        tempLogin.ExtraField7 = "";
        tempLogin.ExtraField8 = "";
        tempLogin.ExtraField9 = "";
        tempLogin.ExtraField10 = ""; 
        bool result = LoginManager.UpdateLogin(tempLogin);

        lblMsg.Text = "Updated Successfully<br/>";
        lblMsg.ForeColor = System.Drawing.Color.Green;
        
        //Response.Redirect("AdminLoginDisplay.aspx");
    }

    private string getSelectedRoleIds()
    {
        string roleIDs = "";
        foreach (DataListItem gr in dlRole.Items)
        {
            CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");

            if (chkSelect.Checked)
            {
                roleIDs+= (roleIDs==""?"":",")+chkSelect.ToolTip;
            }
        }
        return roleIDs;
    }

    private string getSelectedPropertyIDs()
    {
        string propertyIDs = "";
        
        return propertyIDs;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtLoginName.Text = "";
        txtPassword.Text = "";
        txtEmail.Text = "";
        txtFirstName.Text = "";
        txtMiddleName.Text = "";
        txtLastName.Text = "";
        txtDetails.Text = "";
        txtCellPhone.Text = "";
        txtHomePhone.Text = "";
        txtWorkPhone.Text = "";
        ddlRowStatus.SelectedIndex = 0;
        txtInitial.Text = "";
    }
    private void showLoginData()
    {
        Login login = new Login();
        login = LoginManager.GetLoginByID(Int32.Parse(Request.QueryString["loginID"]));

        txtLoginName.Text = login.LoginName;
        hfLoginName.Value = login.LoginName;
        hfPassword.Value = login.Password;
        txtEmail.Text = login.Email;
        txtFirstName.Text = login.FirstName;
        txtMiddleName.Text = login.MiddleName;
        txtLastName.Text = login.LastName;
        txtDetails.Text = login.Details;
        txtCellPhone.Text = login.CellPhone;
        txtHomePhone.Text = login.HomePhone;
        txtWorkPhone.Text = login.WorkPhone;
        ddlRowStatus.SelectedValue = login.RowStatusID.ToString();
        ddlMenuID.SelectedValue = login.ExtraField2;
        ddlWorkSatation.SelectedValue = login.ExtraField5;
        txtInitial.Text = login.ExtraField4;
        if (login.ExtraField1 != "")
        {
            //when Single role
            try {
                int roleID = int.Parse(login.ExtraField1);
                foreach (DataListItem gr in dlRole.Items)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");

                    if (chkSelect.ToolTip == roleID.ToString())
                    {
                        chkSelect.Checked = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            { }
            
            //when multiple role
            foreach (string id in login.ExtraField1.Split(','))
            {
                try
                {
                    int roleID = int.Parse(id);
                    foreach (DataListItem gr in dlRole.Items)
                    {
                        CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");

                        if (chkSelect.ToolTip == roleID.ToString())
                        {
                            chkSelect.Checked = true;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                { }
            
            }
        }

        
    }
    private void loadRowStatus()
    {
        ListItem li = new ListItem("Select >>", "0");
        ddlRowStatus.Items.Add(li);

        List<RowStatus> rowStatuss = new List<RowStatus>();
        rowStatuss = RowStatusManager.GetAllRowStatuss();
        foreach (RowStatus rowStatus in rowStatuss)
        {
            ListItem item = new ListItem(rowStatus.RowStatusName.ToString(), rowStatus.RowStatusID.ToString());
            ddlRowStatus.Items.Add(item);
        }

        ddlRowStatus.SelectedValue = "1";
    }

    private void loadMenu()
    {
        ListItem li = new ListItem("Select >>", "");
        ddlMenuID.Items.Add(li);

        DataSet menus = new DataSet();
        menus = MenuManager.GetMenuddl();
        foreach (DataRow dr in menus.Tables[0].Rows)
        {
            ListItem item = new ListItem(dr["ModuleName"].ToString() + " - " + dr["MenuTitle"].ToString(), dr["FolderName"].ToString() + "/" + dr["PageURL"].ToString());
            ddlMenuID.Items.Add(item);
        }
    }
}
