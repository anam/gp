using System;
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

public class Pos_BranchWiseProductStockCapacity
{
    public Pos_BranchWiseProductStockCapacity()
    {
    }

    public Pos_BranchWiseProductStockCapacity
        (
        int pos_BranchWiseProductStockCapacityID, 
        int productID, 
        int workStationID, 
        long stockAmount
        )
    {
        this.Pos_BranchWiseProductStockCapacityID = pos_BranchWiseProductStockCapacityID;
        this.ProductID = productID;
        this.WorkStationID = workStationID;
        this.StockAmount = stockAmount;
    }


    private int _pos_BranchWiseProductStockCapacityID;
    public int Pos_BranchWiseProductStockCapacityID
    {
        get { return _pos_BranchWiseProductStockCapacityID; }
        set { _pos_BranchWiseProductStockCapacityID = value; }
    }

    private int _productID;
    public int ProductID
    {
        get { return _productID; }
        set { _productID = value; }
    }

    private int _workStationID;
    public int WorkStationID
    {
        get { return _workStationID; }
        set { _workStationID = value; }
    }

    private long _stockAmount;
    public long StockAmount
    {
        get { return _stockAmount; }
        set { _stockAmount = value; }
    }
}
