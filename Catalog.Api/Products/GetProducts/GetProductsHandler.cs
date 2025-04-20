﻿namespace Catalog.Api.Products.GetProducts
{
    public record GetProductsQuery : IQuery<GetProductsResult>;

    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetProductsHandler(IDocumentSession session, ILogger<GetProductsHandler> logger)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler.Handle is called with {@Query}", query);
            var products = await session.Query<Product>().ToListAsync();
            return new GetProductsResult(products);
        }
    }
}
