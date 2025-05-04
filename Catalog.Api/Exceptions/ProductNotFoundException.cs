using BuildingBlocks.Exceptions;

namespace Catalog.Api.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException()
            : base("Product not found")
        {
        }
        public ProductNotFoundException(Guid productId)
            : base("Product", productId)
        {
        }
    }
}
