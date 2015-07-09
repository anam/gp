using System;
using System.Collections;
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
using System.Collections.Generic;

public partial class AdminInv_ProductionConfigurationDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadACC_ChartOfAccountLabel4();
            loadQualityUnit();
            loadQuantityUnit();
            showInv_ProductionConfigurationGrid();
            btnUpdate.Visible = false;
            btnAdd.Visible = true;

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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Inv_ProductionConfiguration inv_ProductionConfiguration = new Inv_ProductionConfiguration();

        inv_ProductionConfiguration.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        inv_ProductionConfiguration.QualityValue = Decimal.Parse(txtQualityValue.Text);
        inv_ProductionConfiguration.QualityUnitID = Int32.Parse(ddlQualityUnit.SelectedValue);
        inv_ProductionConfiguration.QuantityValue = Decimal.Parse(txtQuantityValue.Text);
        inv_ProductionConfiguration.QuantityUnitID = Int32.Parse(ddlQuantityUnit.SelectedValue);
        inv_ProductionConfiguration.RawMaterialID = Int32.Parse(ddlRawMaterial.SelectedValue);
        inv_ProductionConfiguration.ExtraField1 = "";
        inv_ProductionConfiguration.ExtraField2 = "";
        inv_ProductionConfiguration.ExtraField3 = "";
        inv_ProductionConfiguration.ExtraField4 = "";
        inv_ProductionConfiguration.ExtraField5 = "";
        inv_ProductionConfiguration.AddedBy = getLogin().LoginID;
        inv_ProductionConfiguration.AddedDate = DateTime.Now;
        inv_ProductionConfiguration.UpdatedBy = getLogin().LoginID;
        inv_ProductionConfiguration.UpdatedDate = DateTime.Now;
        inv_ProductionConfiguration.RowStatusID = 1;
        int resutl = Inv_ProductionConfigurationManager.InsertInv_ProductionConfiguration(inv_ProductionConfiguration);
        //Response.Redirect("AdminInv_ProductionConfigurationDisplay.aspx");
        showInv_ProductionConfigurationGrid();
        btnClear_Click(this, new EventArgs());
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        btnUpdate.Visible = false;
        btnAdd.Visible = true;

        Inv_ProductionConfiguration tempInv_ProductionConfiguration = new Inv_ProductionConfiguration();
        tempInv_ProductionConfiguration.Inv_ProductionConfigurationID = int.Parse(hfConfigID.Value);

        tempInv_ProductionConfiguration.ProductID = Int32.Parse(ddlProduct.SelectedValue);
        tempInv_ProductionConfiguration.QualityValue = Decimal.Parse(txtQualityValue.Text);
        tempInv_ProductionConfiguration.QualityUnitID = Int32.Parse(ddlQualityUnit.SelectedValue);
        tempInv_ProductionConfiguration.QuantityValue = Decimal.Parse(txtQuantityValue.Text);
        tempInv_ProductionConfiguration.QuantityUnitID = Int32.Parse(ddlQuantityUnit.SelectedValue);
        tempInv_ProductionConfiguration.RawMaterialID = Int32.Parse(ddlRawMaterial.SelectedValue);
        tempInv_ProductionConfiguration.ExtraField1 = "";
        tempInv_ProductionConfiguration.ExtraField2 = "";
        tempInv_ProductionConfiguration.ExtraField3 = "";
        tempInv_ProductionConfiguration.ExtraField4 = "";
        tempInv_ProductionConfiguration.ExtraField5 = "";
        tempInv_ProductionConfiguration.AddedBy = getLogin().LoginID;
        tempInv_ProductionConfiguration.AddedDate = DateTime.Now;
        tempInv_ProductionConfiguration.UpdatedBy = getLogin().LoginID;
        tempInv_ProductionConfiguration.UpdatedDate = DateTime.Now;
        tempInv_ProductionConfiguration.RowStatusID = 1;
        bool result = Inv_ProductionConfigurationManager.UpdateInv_ProductionConfiguration(tempInv_ProductionConfiguration);
        showInv_ProductionConfigurationGrid();
        btnClear_Click(this, new EventArgs());
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //ddlProduct.SelectedIndex = 0;
        txtQualityValue.Text = "0";
        //ddlQualityUnit.SelectedIndex = 0;
        txtQuantityValue.Text = "";
        //ddlQuantityUnit.SelectedIndex = 0;
        //ddlRawMaterial.SelectedIndex = 0;

    }
    private void showInv_ProductionConfigurationData(int id)
    {
        Inv_ProductionConfiguration inv_ProductionConfiguration = new Inv_ProductionConfiguration();
        inv_ProductionConfiguration = Inv_ProductionConfigurationManager.GetInv_ProductionConfigurationByID(id);

        ddlProduct.SelectedValue = inv_ProductionConfiguration.ProductID.ToString();
        txtQualityValue.Text = inv_ProductionConfiguration.QualityValue.ToString();
        ddlQualityUnit.SelectedValue = inv_ProductionConfiguration.QualityUnitID.ToString();
        txtQuantityValue.Text = inv_ProductionConfiguration.QuantityValue.ToString();
        ddlQuantityUnit.SelectedValue = inv_ProductionConfiguration.QuantityUnitID.ToString();
        ddlRawMaterial.SelectedValue = inv_ProductionConfiguration.RawMaterialID.ToString();

    }
    private void loadACC_ChartOfAccountLabel4()
    {

        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlProduct.Items.Add(new ListItem("Select Product", "0"));
        ddlRawMaterial.Items.Add(new ListItem("Select RawMaterial", "0"));

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());


            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 3)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlProduct.Items.Add(item);
            }


            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 2
               || aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 9
                )
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlRawMaterial.Items.Add(item);
            }
        }


    }
    private void loadQualityUnit()
    {
        ListItem li = new ListItem("Select QualityUnit...", "0");
        //ddlQualityUnit.Items.Add(li);

        List<Inv_QualityUnit> qualityUnits = new List<Inv_QualityUnit>();
        qualityUnits = Inv_QualityUnitManager.GetAllInv_QualityUnits();
        foreach (Inv_QualityUnit qualityUnit in qualityUnits)
        {
            ListItem item = new ListItem(qualityUnit.QualityUnitName.ToString(), qualityUnit.Inv_QualityUnitID.ToString());
            ddlQualityUnit.Items.Add(item);
        }
    }
    private void loadQuantityUnit()
    {
        ListItem li = new ListItem("Select QuantityUnit...", "0");
        ddlQuantityUnit.Items.Add(li);

        List<Inv_QuantityUnit> quantityUnits = new List<Inv_QuantityUnit>();
        quantityUnits = Inv_QuantityUnitManager.GetAllInv_QuantityUnits();
        foreach (Inv_QuantityUnit quantityUnit in quantityUnits)
        {
            ListItem item = new ListItem(quantityUnit.QuantityUnitName.ToString(), quantityUnit.Inv_QuantityUnitID.ToString());
            ddlQuantityUnit.Items.Add(item);
        }
    }


    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        
        btnAdd.Visible = false;
        btnUpdate.Visible = true;
        hfConfigID.Value = id.ToString();
        showInv_ProductionConfigurationData(id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = Inv_ProductionConfigurationManager.DeleteInv_ProductionConfiguration(Convert.ToInt32(linkButton.CommandArgument));
        showInv_ProductionConfigurationGrid();
    }

    private void showInv_ProductionConfigurationGrid()
    {
        gvInv_ProductionConfiguration.DataSource = Inv_ProductionConfigurationManager.GetAllInv_ProductionConfigurations();
        gvInv_ProductionConfiguration.DataBind();
    }
}
