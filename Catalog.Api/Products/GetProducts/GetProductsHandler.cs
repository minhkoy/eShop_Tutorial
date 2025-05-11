﻿using Marten.Pagination;

namespace Catalog.Api.Products.GetProducts
{
    public record GetProductsQuery(int? PageIndex = 1, int? PageSize = 10) : IQuery<GetProductsResult>;

    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetProductsHandler(IDocumentSession session)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(query.PageIndex ?? 1, query.PageSize ?? 10, cancellationToken);
            return new GetProductsResult(products);
        }
    }
}
