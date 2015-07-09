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

public partial class AdminPos_SizeProductInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadPos_Size();
            loadProduct();
            if (Request.QueryString["pos_SizeProductID"] != null)
            {
                int pos_SizeProductID = Int32.Parse(Request.QueryString["pos_SizeProductID"]);
                if (pos_SizeProductID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPos_SizeProductData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pos_SizeProduct pos_SizeProduct = new Pos_SizeProduct();

        pos_SizeProduct.Pos_SizeID = Int32.Parse(ddlPos_Size.SelectedValue);
        pos_SizeProduct.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        int resutl = Pos_SizeProductManager.InsertPos_SizeProduct(pos_SizeProduct);
        Response.Redirect("AdminPos_SizeProductDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Pos_SizeProduct pos_SizeProduct = new Pos_SizeProduct();
        pos_SizeProduct = Pos_SizeProductManager.GetPos_SizeProductByID(Int32.Parse(Request.QueryString["pos_SizeProductID"]));
        Pos_SizeProduct tempPos_SizeProduct = new Pos_SizeProduct();
        tempPos_SizeProduct.Pos_SizeProductID = pos_SizeProduct.Pos_SizeProductID;

        tempPos_SizeProduct.Pos_SizeID = Int32.Parse(ddlPos_Size.SelectedValue);
        tempPos_SizeProduct.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        bool result = Pos_SizeProductManager.UpdatePos_SizeProduct(tempPos_SizeProduct);
        Response.Redirect("AdminPos_SizeProductDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlPos_Size.SelectedIndex = 0;
        ddlProduct.SelectedIndex = 0;
    }
    private void showPos_SizeProductData()
    {
        Pos_SizeProduct pos_SizeProduct = new Pos_SizeProduct();
        pos_SizeProduct = Pos_SizeProductManager.GetPos_SizeProductByID(Int32.Parse(Request.QueryString["pos_SizeProductID"]));

        ddlPos_Size.SelectedValue = pos_SizeProduct.Pos_SizeID.ToString();
        ddlProduct.SelectedValue = pos_SizeProduct.ProductID.ToString();
    }
    private void loadPos_Size()
    {
        ListItem li = new ListItem("Select Pos_Size...", "0");
        ddlPos_Size.Items.Add(li);

        List<Pos_Size> pos_Sizes = new List<Pos_Size>();
        pos_Sizes = Pos_SizeManager.GetAllPos_Sizes();
        foreach (Pos_Size pos_Size in pos_Sizes)
        {
            ListItem item = new ListItem(pos_Size.SizeName.ToString(), pos_Size.Pos_SizeID.ToString());
            ddlPos_Size.Items.Add(item);
        }
    }
    private void loadProduct()
    {
        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlProduct.Items.Add(new ListItem("Select Product", "0"));

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 3)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlProduct.Items.Add(item);
            }
        }
    }
}
