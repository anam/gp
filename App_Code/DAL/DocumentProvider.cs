using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlDocumentProvider:DataAccessObject
{
	public SqlDocumentProvider()
    {
    }


    public DataSet  GetAllDocuments()
    {
        DataSet Documents = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllDocuments", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Documents);
            myadapter.Dispose();
            connection.Close();

            return Documents;
        }
    }
	public DataSet GetDocumentPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetDocumentPageWise", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize",  PageSize );
                command.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                command.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                connection.Open();
                SqlDataAdapter myadapter = new SqlDataAdapter(command);
                myadapter.Fill(ds);
                myadapter.Dispose();
                connection.Close();
                recordCount = Convert.ToInt32(command.Parameters["@RecordCount"].Value);
                return ds;
            }
        }
    }


    public DataSet GetDocumentByClientID(int clientID)
    {
        DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDocumentByClientID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ClientID", SqlDbType.NVarChar).Value = clientID;
            connection.Open();
             
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(ds);
            myadapter.Dispose();
            connection.Close();
           
            return ds;
        }
    }

    public DataSet  GetDropDownLisAllDocument()
    {
        DataSet Documents = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllDocument", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Documents);
            myadapter.Dispose();
            connection.Close();

            return Documents;
        }
    }

    public DataSet   GetAllDocumentsWithRelation()
    {
       DataSet ds = new DataSet();
       using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllDocumentsWithRelation", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(ds);
            myadapter.Dispose();
            connection.Close();

            return ds;
        }
    }
    public List<Document> GetDocumentsFromReader(IDataReader reader)
    {
        List<Document> documents = new List<Document>();

        while (reader.Read())
        {
            documents.Add(GetDocumentFromReader(reader));
        }
        return documents;
    }

    public Document GetDocumentFromReader(IDataReader reader)
    {
        try
        {
            Document document = new Document
                (

                     DataAccessObject.IsNULL<int>(reader["DocumentID"]),
                     DataAccessObject.IsNULL<int>(reader["ClientID"]),
                     DataAccessObject.IsNULL<string>(reader["Details"]),
                     DataAccessObject.IsNULL<string>(reader["FileName"])
                );
             return document;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Document  GetDocumentByDocumentID(int  documentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDocumentByDocumentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@DocumentID", SqlDbType.Int).Value = documentID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetDocumentFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertDocument(Document document)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertDocument", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DocumentID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = document.ClientID;
            cmd.Parameters.Add("@Details", SqlDbType.NVarChar).Value = document.Details;
            cmd.Parameters.Add("@FileName", SqlDbType.NVarChar).Value = document.FileName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@DocumentID"].Value;
        }
    }

    public bool UpdateDocument(Document document)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateDocument", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DocumentID", SqlDbType.Int).Value = document.DocumentID;
            cmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = document.ClientID;
            cmd.Parameters.Add("@Details", SqlDbType.NVarChar).Value = document.Details;
            cmd.Parameters.Add("@FileName", SqlDbType.NVarChar).Value = document.FileName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteDocument(int documentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteDocument", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DocumentID", SqlDbType.Int).Value = documentID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

