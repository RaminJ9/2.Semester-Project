using System.Collections.Generic;
using PIM.Data.Queries;

namespace PIM.Access.Queries;

public class InsertUserQuery : QueryBase
{
    public InsertUserQuery(Dictionary<string, object?> parameters) : base(parameters)
    {
        base.query = "INSERT INTO users (username, password) VALUES (@username, @password)";
    }
}