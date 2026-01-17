using System.Collections.Generic;
using Npgsql;
using PIM.Data.Queries;

namespace PIM.Access.Queries;

public class GetAllPasswordsQuery : QueryBase
{
    public GetAllPasswordsQuery(Dictionary<string, object?> parameters) : base(parameters)
    {
        base.query = "SELECT password FROM users";
    }

    public override object MapFromReader(NpgsqlDataReader reader)
    {
        return reader.GetString(0);
    }
}