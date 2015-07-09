using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Control_TopMenu : System.Web.UI.UserControl
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loginCheck();
            loadMenu();
        }
    }

    private void loadMenu()
    {
        lblMenu.Text = "<ul class='menu' id='menu'>";
        if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }
            
        DataSet dsMenu = MenuManager.GetMenuByLoginID(((Login)Session["Login"]).LoginID);
        bool isPreviousMenuWasSingleMenuModule = false;  
        string lastModule = "";
        for (int i = 0; i < dsMenu.Tables[0].Rows.Count; i++)
        {
            
            if(lastModule == "")
            {
                lastModule =  dsMenu.Tables[0].Rows[i]["ModuleName"].ToString();
                lblMenu.Text += "<li><a href='../" + dsMenu.Tables[0].Rows[i]["FolderName"].ToString() + "/" + dsMenu.Tables[0].Rows[i]["DefaultURL"].ToString() + "' class='menulink' >" + lastModule + "</a>";

                if (dsMenu.Tables[0].Rows.Count == 1) //when the single module with no summenu
                {
                    lblMenu.Text += "</li></ul>";
                    return;
                }
                else
                {
                    if (lastModule != dsMenu.Tables[0].Rows[i + 1]["ModuleName"].ToString()) // 1st module with no submenu but has some other module
                    {
                        lblMenu.Text += "</li>";
                        isPreviousMenuWasSingleMenuModule = true;
                        dsMenu.Tables[0].Rows[i]["Usered"] = "Yes";
                        continue;
                    }
                    else
                    {
                        lblMenu.Text += "<ul>";
                        isPreviousMenuWasSingleMenuModule = false;
                    }
                }
            }
            

            if (lastModule != dsMenu.Tables[0].Rows[i]["ModuleName"].ToString())
            {
                bool isSingleModule = true;
                if (!isPreviousMenuWasSingleMenuModule)
                {
                    lblMenu.Text += "</ul></li>";
                }

                lastModule = dsMenu.Tables[0].Rows[i]["ModuleName"].ToString();
                lblMenu.Text += "<li><a href='../" + dsMenu.Tables[0].Rows[i]["FolderName"].ToString() + "/" + dsMenu.Tables[0].Rows[i]["DefaultURL"].ToString() + "' class='menulink' >" + lastModule + "</a>";

                if ((i == (dsMenu.Tables[0].Rows.Count - 1)) && dsMenu.Tables[0].Rows[i]["ModuleName"].ToString() != dsMenu.Tables[0].Rows[i - 1]["ModuleName"].ToString()) // when last module has no sub menu
                {
                    lblMenu.Text += "</li></ul>";
                    return;                    
                }
                else
                {
                    if (dsMenu.Tables[0].Rows[i]["ModuleName"].ToString() != dsMenu.Tables[0].Rows[i + 1]["ModuleName"].ToString()) //module with no sub menu is in middle
                    {
                        lblMenu.Text += "</li>";
                        isPreviousMenuWasSingleMenuModule = true;
                        dsMenu.Tables[0].Rows[i]["Usered"] = "Yes";
                        continue;
                    }
                    else
                    {
                        lblMenu.Text += "<ul>";
                        isPreviousMenuWasSingleMenuModule = false;                    
                    }
                }
                
            }
            
            if (dsMenu.Tables[0].Rows[i]["Usered"].ToString() != "Yes")
            {
                lblMenu.Text += "<li><a href='../" + dsMenu.Tables[0].Rows[i]["FolderName"].ToString() + "/" + dsMenu.Tables[0].Rows[i]["PageURL"].ToString() + "'";
                dsMenu.Tables[0].Rows[i]["Usered"] = "Yes";
                int count = 0;
                foreach (DataRow drSubMenu in dsMenu.Tables[0].Rows)
                {
                    if (lastModule == drSubMenu["ModuleName"].ToString()
                        && drSubMenu["Usered"].ToString() == ""
                        && drSubMenu["ParentMenuID"].ToString() == dsMenu.Tables[0].Rows[i]["MenuID"].ToString())
                    {
                        if (count++ == 0)
                        {
                            lblMenu.Text += " class='sub'>" + dsMenu.Tables[0].Rows[i]["MenuTitle"].ToString() + "</a>";
                            lblMenu.Text += "<ul>";
                        }

                        lblMenu.Text += "<li class='topline'><a href='" + "../" + drSubMenu["FolderName"].ToString() + "/" + drSubMenu["PageURL"].ToString() + "' >" + drSubMenu["MenuTitle"].ToString() + "</a></li>";

                        drSubMenu["Usered"] = "Yes";
                        lblMenu.Text += "";
                    }
                }

                if (count == 0)
                {
                    lblMenu.Text += ">" + dsMenu.Tables[0].Rows[i]["MenuTitle"].ToString() + "</a></li>";
                }
                else
                {
                    lblMenu.Text += "</ul></li>";
                }
            }
        }

        lblMenu.Text += "</ul></li></ul>";
    }
    private void loginCheck()
    {
        bool isloggedIN = false;

        if ((Session["Login"] != null))
        {
            try
            {
                Login login = LoginManager.GetLoginByLoginName(((Login)Session["Login"]).LoginName.ToString());
                if (login != null)
                {
                    Session["Login"] = login;
                    isloggedIN = true;
                }
                else
                {
                    isloggedIN = false;
                }

            }
            catch (Exception ex)
            {
                isloggedIN = false;
            }
        }
        if ((Request.Browser.Cookies) && (Request.Cookies["LoginName"] != null))
        {

            HttpCookie aCookie = Request.Cookies["LoginName"];

            Login login = LoginManager.GetLoginByLoginName(Convert.ToString(Server.HtmlEncode(aCookie.Value)));
            if (login != null)
            {
                Session["Login"] = login;
                isloggedIN = true;
            }
        }

        if (isloggedIN)
        {

        }
        else
        {
            Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect("../LoginPage.aspx");
        }
    }

}