
namespace Catalog.Api.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID must not be empty");
        }
    }
    internal class DeleteProductHandler
        (IDocumentSession session, ILogger<DeleteProductHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.Id);
            if (product is null)
            {
                logger.LogError("Product with ID {Id} not found", request.Id);
                return new DeleteProductResult(false);
            }

            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Product with ID {Id} deleted successfully", request.Id);
            return new DeleteProductResult(true);
        }
    }
}
