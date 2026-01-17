using System.Collections.Generic;
using PIM.Data.Queries;

namespace PIM.Access.Queries;

public class DeleteUserQuery : QueryBase
{
    public DeleteUserQuery(Dictionary<string, object?> parameters) : base(parameters)
    {
        base.query = "DELETE FROM users WHERE username = @user";
    }
}