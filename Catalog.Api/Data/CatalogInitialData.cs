using Marten.Schema;

namespace Catalog.Api.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            // Check if the database is exist
            if (await session.Query<Product>().AnyAsync(cancellation))
            {
                return;
            }

            session.Store<Product>(GetPreconfiguredProducts());

            await session.SaveChangesAsync(cancellation);
        }

        private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
        {
            new Product()
            {
                Name = "IPhone X",
                Description = "This phone is the company's biggest change",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Categories = new List<string> { "Smart Phone", "Apple" }
            },
            new Product()
            {
                Name = "Samsung Galaxy S10",
                Description = "The Samsung Galaxy S10 is a flagship smartphone",
                ImageFile = "product-2.png",
                Price = 840.00M,
                Categories = new List<string> { "Smart Phone", "Samsung" }
            },
            new Product()
            {
                Name = "Google Pixel 3",
                Description = "The Google Pixel 3 is a smartphone developed by Google",
                ImageFile = "product-3.png",
                Price = 750.00M,
                Categories = new List<string> { "Smart Phone", "Google" }
            },
        };
    }
}
