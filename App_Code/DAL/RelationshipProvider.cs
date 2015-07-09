using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

public class SqlRelationshipProvider:DataAccessObject
{
	public SqlRelationshipProvider()
    {
    }


    public DataSet  GetAllRelationships()
    {
        DataSet Relationships = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllRelationships", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Relationships);
            myadapter.Dispose();
            connection.Close();

            return Relationships;
        }
    }
	public DataSet GetRelationshipPageWise(int pageIndex, int PageSize, out int recordCount)
    {
    DataSet ds = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand("GetRelationshipPageWise", connection))
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


    public DataSet  GetDropDownLisAllRelationship()
    {
        DataSet Relationships = new DataSet();
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetDropDownListAllRelationship", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(Relationships);
            myadapter.Dispose();
            connection.Close();

            return Relationships;
        }
    }

    public DataSet   GetAllToursWithRelation()
    {
       DataSet ds = new DataSet();
       using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetAllToursWithRelation", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataAdapter myadapter = new SqlDataAdapter(command);
            myadapter.Fill(ds);
            myadapter.Dispose();
            connection.Close();

            return ds;
        }
    }
    public List<Relationship> GetRelationshipsFromReader(IDataReader reader)
    {
        List<Relationship> relationships = new List<Relationship>();

        while (reader.Read())
        {
            relationships.Add(GetRelationshipFromReader(reader));
        }
        return relationships;
    }

    public Relationship GetRelationshipFromReader(IDataReader reader)
    {
        try
        {
            Relationship relationship = new Relationship
                (

                     DataAccessObject.IsNULL<int>(reader["RelationshipID"]),
                     DataAccessObject.IsNULL<string>(reader["RelationshipName"])
                );
             return relationship;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Relationship  GetRelationshipByRelationshipID(int  relationshipID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("GetRelationshipByRelationshipID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@RelationshipID", SqlDbType.Int).Value = relationshipID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
            return GetRelationshipFromReader(reader);
             }
            else
            {
            return null;
            }
        }
    }

    public int InsertRelationship(Relationship relationship)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertRelationship", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RelationshipID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@RelationshipName", SqlDbType.NVarChar).Value = relationship.RelationshipName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@RelationshipID"].Value;
        }
    }

    public bool UpdateRelationship(Relationship relationship)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateRelationship", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RelationshipID", SqlDbType.Int).Value = relationship.RelationshipID;
            cmd.Parameters.Add("@RelationshipName", SqlDbType.NVarChar).Value = relationship.RelationshipName;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }

    public bool DeleteRelationship(int relationshipID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteRelationship", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RelationshipID", SqlDbType.Int).Value = relationshipID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }
}

