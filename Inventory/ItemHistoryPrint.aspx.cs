using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Inventory_PurchasePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initialLoad();
            loadData();
        }
    }

    private void loadData()
    {
        int itemID = 0;

        List<Inv_ItemTransaction> Inv_ItemTransactionHostory = new List<Inv_ItemTransaction>();
        if (Request.QueryString["ItemID"] != null)
        {
            itemID = int.Parse(Request.QueryString["ItemID"]);
            Inv_ItemTransactionHostory = Inv_ItemTransactionManager.GetAllInv_ItemTransactionsByItemID(int.Parse(Request.QueryString["ItemID"]));
            //gvInv_ItemTransaction.DataSource = Inv_ItemTransactionHostory;
        }
        else if (Request.QueryString["ItemCode"] != null)
        {
            Inv_ItemTransactionHostory = Inv_ItemTransactionManager.GetAllInv_ItemTransactionsByItemCode(Request.QueryString["ItemCode"]);
            //gvInv_ItemTransaction.DataSource = Inv_ItemTransactionHostory;
            itemID = Inv_ItemTransactionHostory[0].ItemID;
        }
        Inv_ItemTransactionHostory = assignWorkStationName(Inv_ItemTransactionHostory);
        gvInv_ItemTransaction.DataSource = Inv_ItemTransactionHostory;
        gvInv_ItemTransaction.DataBind();


        //item details
        List<Inv_Item> items = new List<Inv_Item>();
        items.Add(Inv_ItemManager.GetInv_ItemByID(itemID));

        gvInv_Item.DataSource = items;
        gvInv_Item.DataBind();
    }
    private List<Inv_ItemTransaction> assignWorkStationName(List<Inv_ItemTransaction> Inv_ItemTransactionHostory)
    {
        DataSet ds = CommonManager.SQLExec("Select ACC_ChartOfAccountLabel4ID,ChartOfAccountLabel4Text from ACC_ChartOfAccountLabel4 where ACC_HeadTypeID=1");

        foreach (Inv_ItemTransaction item in Inv_ItemTransactionHostory)
        {
            if (item.ItemTrasactionTypeID != 2)
            {
                item.ExtraField4 = "";
                continue;
            
            }
            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["ACC_ChartOfAccountLabel4ID"].ToString() == item.ExtraField4)
                    {
                        item.ExtraField4 = dr["ChartOfAccountLabel4Text"].ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        return Inv_ItemTransactionHostory;
    }
    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
}