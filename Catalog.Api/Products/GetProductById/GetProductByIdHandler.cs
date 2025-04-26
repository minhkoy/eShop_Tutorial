namespace Catalog.Api.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);

    internal class GetProductByIdHandler
        (IDocumentSession session, ILogger<GetProductByIdHandler> logger)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQueryHandler called with {@Query}", query);
            var result = await session.LoadAsync<Product>(query.Id, cancellationToken);
            if (result is null)
            {
                throw new ProductNotFoundException($"Product with id {query.Id} not found");
            }
            return new GetProductByIdResult(result);
        }
    }
}
