using Npgsql;
using System;
using System.Collections.Generic;

namespace PIM.Data.Queries;

// Method acting as 'scafolding' for all other queries
public abstract class QueryBase
{
    protected string query; // The SQL query (If the format of the query string changes from query to query; see method GetQuery()
    Dictionary<string, object> parameters; // Dictionary holding paramaters for the SQL query. (See DatabaseConnection.cs for example)
    protected QueryBase(Dictionary<string, object> parameters)
    {
        this.parameters = parameters;
        // Variable 'query' is unset on purpose, since it will be different for each sub-class
    }

    // Method used to go from NpgsqlDataReaderObject(equivalent to one response-row) to any object
    // This method, and the return type, should be overriden be any sub-class
    public virtual object MapFromReader(NpgsqlDataReader reader)
    {
        throw new NotImplementedException("This method should only be called on sub-classes that override it with a custom implementation");
    }

    // This method should only be overriden when the variable 'query' is ever changing / not static
    // Eg: A query that inserts multiple categories.
    // ^That query cannot be written once in the sub-class constructor, since
    // one query might need two categories and anohter might need 100. In that
    // case, the query can be built in this at runtime. (Possibly based on other values 
    // added in the sub-class or on the paramaters dictionary)
    public virtual string GetQuery()
    {
        return query;
    }
    public virtual Dictionary<string, object> GetParamaters()
    {
        return parameters;
    }
}
