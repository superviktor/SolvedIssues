using GraphQL.Types;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Api.GraphQL
{
    public class ProductSchema : Schema
    {
        public ProductSchema(IServiceProvider provider): base(provider)
        {
            Query = provider.GetRequiredService<ProductQuery>();
            Mutation = provider.GetRequiredService<ProductMutation>();
            Subscription = provider.GetRequiredService<ProductSubscription>();
        }
    }
}