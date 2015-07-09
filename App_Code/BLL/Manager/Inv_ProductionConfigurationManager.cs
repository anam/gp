using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class Inv_ProductionConfigurationManager
{
	public Inv_ProductionConfigurationManager()
	{
	}

    public static List<Inv_ProductionConfiguration> GetAllInv_ProductionConfigurations()
    {
        List<Inv_ProductionConfiguration> inv_ProductionConfigurations = new List<Inv_ProductionConfiguration>();
        SqlInv_ProductionConfigurationProvider sqlInv_ProductionConfigurationProvider = new SqlInv_ProductionConfigurationProvider();
        inv_ProductionConfigurations = sqlInv_ProductionConfigurationProvider.GetAllInv_ProductionConfigurations();
        return inv_ProductionConfigurations;
    }


    public static List<Inv_ProductionConfiguration> GetAllInv_ProductionConfigurationsByProductIDnRawmaterialID(int productID,int rawmaterialID)
    {
        List<Inv_ProductionConfiguration> inv_ProductionConfigurations = new List<Inv_ProductionConfiguration>();
        SqlInv_ProductionConfigurationProvider sqlInv_ProductionConfigurationProvider = new SqlInv_ProductionConfigurationProvider();
        inv_ProductionConfigurations = sqlInv_ProductionConfigurationProvider.GetAllInv_ProductionConfigurationsByProductIDnRawmaterialID(productID, rawmaterialID);
        return inv_ProductionConfigurations;
    }

    public static List<Inv_ProductionConfiguration> GetAllInv_ProductionConfigurationsByProductIDnItemID(int productID, int itemID)
    {
        List<Inv_ProductionConfiguration> inv_ProductionConfigurations = new List<Inv_ProductionConfiguration>();
        SqlInv_ProductionConfigurationProvider sqlInv_ProductionConfigurationProvider = new SqlInv_ProductionConfigurationProvider();
        inv_ProductionConfigurations = sqlInv_ProductionConfigurationProvider.GetAllInv_ProductionConfigurationsByProductIDnItemID(productID, itemID);
        return inv_ProductionConfigurations;
    }

    public static Inv_ProductionConfiguration GetInv_ProductionConfigurationByID(int id)
    {
        Inv_ProductionConfiguration inv_ProductionConfiguration = new Inv_ProductionConfiguration();
        SqlInv_ProductionConfigurationProvider sqlInv_ProductionConfigurationProvider = new SqlInv_ProductionConfigurationProvider();
        inv_ProductionConfiguration = sqlInv_ProductionConfigurationProvider.GetInv_ProductionConfigurationByID(id);
        return inv_ProductionConfiguration;
    }


    public static int InsertInv_ProductionConfiguration(Inv_ProductionConfiguration inv_ProductionConfiguration)
    {
        SqlInv_ProductionConfigurationProvider sqlInv_ProductionConfigurationProvider = new SqlInv_ProductionConfigurationProvider();
        return sqlInv_ProductionConfigurationProvider.InsertInv_ProductionConfiguration(inv_ProductionConfiguration);
    }


    public static bool UpdateInv_ProductionConfiguration(Inv_ProductionConfiguration inv_ProductionConfiguration)
    {
        SqlInv_ProductionConfigurationProvider sqlInv_ProductionConfigurationProvider = new SqlInv_ProductionConfigurationProvider();
        return sqlInv_ProductionConfigurationProvider.UpdateInv_ProductionConfiguration(inv_ProductionConfiguration);
    }

    public static bool DeleteInv_ProductionConfiguration(int inv_ProductionConfigurationID)
    {
        SqlInv_ProductionConfigurationProvider sqlInv_ProductionConfigurationProvider = new SqlInv_ProductionConfigurationProvider();
        return sqlInv_ProductionConfigurationProvider.DeleteInv_ProductionConfiguration(inv_ProductionConfigurationID);
    }
}
