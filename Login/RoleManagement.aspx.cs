using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Login_RoleManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadRole();
            loadAllModule();
            loadLogin();


            btnAddNewRole.Visible = true;
            btnUpdate.Visible = false;
        }
    }

    private void loadLogin()
    {
        ListItem li = new ListItem("Select Login...", "0");
        ddlLogin.Items.Add(li);

        List<Login> Logins = new List<Login>();
        Logins = LoginManager.GetAllLogins();
        foreach (Login login in Logins)
        {
            ListItem item = new ListItem(login.FirstName+" "+login.MiddleName+" "+login.LastName+" ("+ login.LoginName.ToString()+")", login.LoginID.ToString()+"-"+login.ExtraField1);
            ddlLogin.Items.Add(item);
        }
    }

    private void loadAllModule()
    {
        gvModule.DataSource = ModuleManager.GetAllModules();
        gvModule.DataBind();
    }

    private void loadRole()
    {
        ddlRole.Items.Clear();
        ListItem li = new ListItem("Select Role...", "0");
        ddlRole.Items.Add(li);

        List<Role> roles = new List<Role>();
        roles = RoleManager.GetAllRoles();
        foreach (Role role in roles)
        {
            ListItem item = new ListItem(role.RoleName.ToString(), role.RoleID.ToString());
            ddlRole.Items.Add(item);
        }

        gvRole.DataSource = roles;
        gvRole.DataBind();
    }
    protected void btnAddNewRole_Click(object sender, EventArgs e)
    {
        string loginID ="1";
        try {
            if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }
            loginID = ((Login)Session["Login"]).LoginID.ToString();
        }
        catch (Exception ex)
        { }

        Role role = new Role();

        role.RoleName = txtRoleName.Text;
        role.RoleDescription = txtRoleDescription.Text;
        role.AddedDate = DateTime.Now;
        
        role.AddedBy = loginID;
        role.ModifyDate = DateTime.Now;
        role.ModifyBy = loginID;
        role.RowStatusID = 1;
        int resutl = RoleManager.InsertRole(role);
        loadRole();

        txtRoleName.Text="";
    }

    protected void gvPagenMenu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataList dlButton = (DataList)e.Row.FindControl("dlButton");
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelectPageNMenu");

            dlButton.DataSource = ButtonManager.GetAllButtonsByPageID(int.Parse(chkSelect.ToolTip));
            dlButton.DataBind();
        }
    }

    protected void gvRole_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gvPagenMenu = (GridView)e.Row.FindControl("gvPagenMenu");
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
            DataSet ds = PageManager.GetAllPagenMenuByModuleID(int.Parse(chkSelect.ToolTip));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["MenuTitle"].ToString().Length == 0)
                {
                    dr["MenuTitle"] = "UnTitled";
                }

                try
                {
                    int menuID = int.Parse(dr["MenuID"].ToString());
                }
                catch(Exception ex)
                {
                    dr["MenuID"] = 0;
                }

                try
                {
                    decimal menuID = decimal.Parse(dr["MenuOrder"].ToString());
                }
                catch (Exception ex)
                {
                    dr["MenuOrder"] = 1.00;
                }
            }

            gvPagenMenu.DataSource = ds;
            gvPagenMenu.DataBind();
        }
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadModulePageNMenuByRoleID(ddlRole.SelectedValue);
    }

    private void loadModulePageNMenuByRoleID(string RoleID)
    {
        DataSet ds = RoleManager.GetModuleNMenuNPageNButtonByRoleID(int.Parse(ddlRole.SelectedValue));

        /*
         * table 0 = module
         * table 1 = Menu
         * table 2 = page
         * table 3 = button
         */

        foreach (GridViewRow grModule in gvModule.Rows)
        {
            CheckBox chkSelect = (CheckBox)grModule.FindControl("chkSelect");
            //Checking rolewise Module
            chkSelect.Checked = false;  
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (chkSelect.ToolTip == dr["ModuleID"].ToString())
                {
                    chkSelect.Checked = true;
                }
            }

            GridView gvPagenMenu = (GridView)grModule.FindControl("gvPagenMenu");
            foreach (GridViewRow grPageNMenu in gvPagenMenu.Rows)
            {
                CheckBox chkSelectPageNMenu = (CheckBox)grPageNMenu.FindControl("chkSelectPageNMenu");
                //Checking rolewise Module
                chkSelectPageNMenu.Checked = false;
                bool IsPageRole = false;
                bool IsMenuRole = false;
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    if (chkSelectPageNMenu.ValidationGroup == dr["MenuID"].ToString())
                    {
                        IsMenuRole = true;
                    }
                }
                
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    if (chkSelectPageNMenu.ToolTip == dr["PageID"].ToString())
                    {
                        IsPageRole = true;
                    }
                }

                if (IsPageRole && IsMenuRole)
                {
                    chkSelectPageNMenu.Checked = true;
                }

                DataList dlButton = (DataList)grPageNMenu.FindControl("dlButton");
                foreach (DataListItem dli in dlButton.Items)
                {
                    CheckBox chkSelectButton = (CheckBox)dli.FindControl("chkSelectButton");
                    chkSelectButton.Checked = false;
                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        if (chkSelectButton.ToolTip == dr["ButtonID"].ToString())
                        {
                            chkSelectButton.Checked = true;
                        }
                    }
                }
            }
        }
    }
    protected void ddlLogin_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLogin.SelectedIndex != 0)
        {
            //when Single role
            try
            {
                int roleID = int.Parse(ddlLogin.SelectedValue.Split('-')[1]);
                foreach (GridViewRow gr in gvRole.Rows)
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
            foreach (string id in ddlLogin.SelectedValue.Split('-')[1].Split(','))
            {
                try
                {
                    int roleID = int.Parse(id);
                    foreach (GridViewRow gr in gvRole.Rows)
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
        else
        {
            loadRole();
        }
    }
    protected void btnRoleAssignSave_Click(object sender, EventArgs e)
    {
        string loginID = "1";
        try
        {
            if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }
            loginID = ((Login)Session["Login"]).LoginID.ToString();
        }
        catch (Exception ex)
        { }
        try
        {
        LoginRole loginRole = new LoginRole();

        loginRole.LoginID = Int32.Parse(ddlLogin.SelectedValue.Split('-')[0]);
        loginRole.RowStatusID = 1;
        loginRole.AddedDate = DateTime.Now;
        loginRole.AddedBy = loginID;
        loginRole.ModifyDate = DateTime.Now;
        loginRole.ModifyBy = loginID;
        bool resutl = LoginRoleManager.UpdateLoginRoleByIDs(loginRole,getSelectedRoleIds());
        }
        catch (Exception ex)
        { }
    }   

    private string getSelectedRoleIds()
    {
        string roleIDs = "";
        foreach (GridViewRow gr in gvRole.Rows)
        {
            CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");

            if (chkSelect.Checked)
            {
                roleIDs += (roleIDs == "" ? "" : ",") + chkSelect.ToolTip;
            }
        }
        return roleIDs;
    }

    protected void btnRoleWisePageNMenuAccess_Click(object sender, EventArgs e)
    {
        string loginID = "1";
        try
        {
            if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }
            
            loginID = ((Login)Session["Login"]).LoginID.ToString();
        }
        catch (Exception ex)
        { }

        RoleManager.DeleteModuleMenuPageButtonRole(int.Parse(ddlRole.SelectedValue));
        int resutl = 0;
        foreach (GridViewRow grModule in gvModule.Rows)
        {
            CheckBox chkSelect = (CheckBox)grModule.FindControl("chkSelect");
            if (chkSelect.Checked)
            {
                ModuleRole moduleRole = new ModuleRole();

                moduleRole.ModuleID = Int32.Parse(chkSelect.ToolTip);
                moduleRole.RoleID = Int32.Parse(ddlRole.SelectedValue);
                moduleRole.AddedDate = DateTime.Now;
                moduleRole.AddedBy = loginID;
                moduleRole.ModifyDate = DateTime.Now;
                moduleRole.ModifyBy = loginID;
                moduleRole.RowStatusID = 1;
                resutl = ModuleRoleManager.InsertModuleRole(moduleRole);
            

                GridView gvPagenMenu = (GridView)grModule.FindControl("gvPagenMenu");
                foreach (GridViewRow grPageNMenu in gvPagenMenu.Rows)
                {
                    CheckBox chkSelectPageNMenu = (CheckBox)grPageNMenu.FindControl("chkSelectPageNMenu");

                    if (chkSelectPageNMenu.Checked)
                    {
                        PageRole pageRole = new PageRole();

                        pageRole.PageID = Int32.Parse(chkSelectPageNMenu.ToolTip);
                        pageRole.RoleID = Int32.Parse(ddlRole.SelectedValue);
                        pageRole.AddedDate = DateTime.Now;
                        pageRole.AddedBy = loginID;
                        pageRole.ModifyDate = DateTime.Now;
                        pageRole.ModifyBy = loginID;
                        pageRole.RowStatusID = 1;
                        resutl = PageRoleManager.InsertPageRole(pageRole);

                        if (chkSelectPageNMenu.ValidationGroup != "0")
                        {
                            MenuRole menuRole = new MenuRole();

                            menuRole.MenuID = Int32.Parse(chkSelectPageNMenu.ValidationGroup);
                            menuRole.RoleID = Int32.Parse(ddlRole.SelectedValue);
                            menuRole.AddedDate = DateTime.Now;
                            menuRole.AddedBy = loginID;
                            menuRole.ModifyDate = DateTime.Now;
                            menuRole.ModifyBy = loginID;
                            menuRole.RowStatusID = 1;
                            resutl = MenuRoleManager.InsertMenuRole(menuRole);
                        }


                        DataList dlButton = (DataList)grPageNMenu.FindControl("dlButton");
                        foreach (DataListItem dliButton in dlButton.Items)
                        {
                            CheckBox chkSelectButton = (CheckBox)dliButton.FindControl("chkSelectButton");

                            if (chkSelectButton.Checked)
                            {
                                ButtonRole buttonRole = new ButtonRole();

                                buttonRole.RoleID = Int32.Parse(ddlRole.SelectedValue);
                                buttonRole.ButtonID = Int32.Parse(chkSelectButton.ToolTip);
                                buttonRole.AddedDate = DateTime.Now;
                                buttonRole.AddedBy = loginID;
                                buttonRole.ModifyDate = DateTime.Now;
                                buttonRole.ModifyBy = loginID;
                                buttonRole.RowStatusID = 1;
                                resutl = ButtonRoleManager.InsertButtonRole(buttonRole);
                            }
                        }
                    }
                }
            }
           

            {
                GridView gvPagenMenu = (GridView)grModule.FindControl("gvPagenMenu");
                foreach (GridViewRow grPageNMenu in gvPagenMenu.Rows)
                {
                    CheckBox chkSelectPageNMenu = (CheckBox)grPageNMenu.FindControl("chkSelectPageNMenu");

                    if (!chkSelectPageNMenu.Checked)
                    {
                        DataList dlButton = (DataList)grPageNMenu.FindControl("dlButton");
                        foreach (DataListItem dliButton in dlButton.Items)
                        {
                            CheckBox chkSelectButton = (CheckBox)dliButton.FindControl("chkSelectButton");

                            if (chkSelectButton.Checked)
                            {
                                ButtonRole buttonRole = new ButtonRole();

                                buttonRole.RoleID = Int32.Parse(ddlRole.SelectedValue);
                                buttonRole.ButtonID = Int32.Parse(chkSelectButton.ToolTip);
                                buttonRole.AddedDate = DateTime.Now;
                                buttonRole.AddedBy = loginID;
                                buttonRole.ModifyDate = DateTime.Now;
                                buttonRole.ModifyBy = loginID;
                                buttonRole.RowStatusID = 1;
                                resutl = ButtonRoleManager.InsertButtonRole(buttonRole);
                            }
                        }
                    }
                }
            }
        }
        Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = RoleManager.DeleteRole(Convert.ToInt32(linkButton.CommandArgument));
        loadRole();
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
        Role role = new Role();
        role = RoleManager.GetRoleByID(Int32.Parse(hfRoleID.Value));
        Role tempRole = new Role();
        tempRole.RoleID = role.RoleID;
        tempRole.RoleName = txtRoleName.Text;
        tempRole.RoleDescription = txtRoleDescription.Text;
        tempRole.AddedDate = DateTime.Now;
        tempRole.AddedBy = loginID;
        tempRole.ModifyDate = DateTime.Now;
        tempRole.ModifyBy = loginID;
        tempRole.RowStatusID = 1;
        bool result = RoleManager.UpdateRole(tempRole);

        loadRole();
        btnUpdate.Visible = false;
        btnAddNewRole.Visible = true;
        txtRoleName.Text = "";
    }

    protected void lbSelect_Click(object sender, EventArgs e)
    {
        Role role = new Role();
        role = RoleManager.GetRoleByID(Int32.Parse(((LinkButton)sender).CommandArgument));

        txtRoleName.Text = role.RoleName;
        txtRoleDescription.Text = role.RoleDescription;
        hfRoleID.Value = role.RoleID.ToString();

        btnAddNewRole.Visible = false;
        btnUpdate.Visible = true;
    }
}