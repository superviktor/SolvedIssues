using System;

namespace GraphQL.ConsoleClient.Models
{
    public class GraphQlException: ApplicationException
    {
        public GraphQlException(string message): base(message)
        {
        }
    }
}
