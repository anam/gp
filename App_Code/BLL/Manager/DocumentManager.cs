using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class DocumentManager
{
	public DocumentManager()
	{
	}

    public static DataSet  GetAllDocuments()
    {
       DataSet documents = new DataSet();
        SqlDocumentProvider sqlDocumentProvider = new SqlDocumentProvider();
        documents = sqlDocumentProvider.GetAllDocuments();
        return documents;
    }

	public static void documentsPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
		{
		double dblPageCount = (double)((decimal)recordCount / decimal.Parse(PageSize.ToString()));
		int pageCount = (int)Math.Ceiling(dblPageCount);
		List<ListItem> pages = new List<ListItem>();
		if (pageCount > 0)
		{
 		pages.Add(new ListItem("First", "1", currentPage > 1));
		for (int i = 1; i <= pageCount; i++)
		{
		 pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
		}
		pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
		}
		rptPager.DataSource = pages;
		rptPager.DataBind();
		}
 	public static void LoadDocumentPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlDocumentProvider sqlDocumentProvider = new SqlDocumentProvider();
		DataSet ds =  sqlDocumentProvider.GetDocumentPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 documentsPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllDocument()
    {
       DataSet documents = new DataSet();
        SqlDocumentProvider sqlDocumentProvider = new SqlDocumentProvider();
        documents = sqlDocumentProvider.GetDropDownLisAllDocument();
        return documents;
    }

    public static DataSet   GetAllDocumentsWithRelation()
    {
       DataSet documents = new DataSet();
        SqlDocumentProvider sqlDocumentProvider = new SqlDocumentProvider();
        documents = sqlDocumentProvider.GetAllDocuments();
        return documents;
    }


    public static DataSet GetDocumentByClientID(int ClientID)
    {
        DataSet documents = new DataSet();
        SqlDocumentProvider sqlDocumentProvider = new SqlDocumentProvider();
        documents = sqlDocumentProvider.GetDocumentByClientID(ClientID);
        return documents;
    }


    public static Document GetDocumentByDocumentID(int DocumentID)
    {
        Document document = new Document();
        SqlDocumentProvider sqlDocumentProvider = new SqlDocumentProvider();
        document = sqlDocumentProvider.GetDocumentByDocumentID(DocumentID);
        return document;
    }


    public static int InsertDocument(Document document)
    {
        SqlDocumentProvider sqlDocumentProvider = new SqlDocumentProvider();
        return sqlDocumentProvider.InsertDocument(document);
    }


    public static bool UpdateDocument(Document document)
    {
        SqlDocumentProvider sqlDocumentProvider = new SqlDocumentProvider();
        return sqlDocumentProvider.UpdateDocument(document);
    }

    public static bool DeleteDocument(int documentID)
    {
        SqlDocumentProvider sqlDocumentProvider = new SqlDocumentProvider();
        return sqlDocumentProvider.DeleteDocument(documentID);
    }
}

