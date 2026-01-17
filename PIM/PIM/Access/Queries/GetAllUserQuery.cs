using System.Collections.Generic;
using Npgsql;
using PIM.Data.Queries;
using PIM.Models;

namespace PIM.Access.Queries;

public class GetAllUserQuery : QueryBase
{
    public GetAllUserQuery(Dictionary<string, object?> parameters) : base(parameters)
    {
        base.query = "SELECT username FROM users";
    }

    public override object MapFromReader(NpgsqlDataReader reader)
    {
        return reader.GetString(0);
    }
}