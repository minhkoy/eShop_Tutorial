namespace Catalog.Api.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException()
            : base("Product not found")
        {
        }
        public ProductNotFoundException(Guid productId)
            : base($"Product with id {productId} not found")
        {
        }
    }
}
