using FluentValidation;

namespace Basket.Api.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string Username);
    public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null.");
            RuleFor(x => x.Cart.Username).NotEmpty().WithMessage("Username is required.");
        }
    }
    public class StoreBasketHandler 
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            // Simulate storing the basket
            ShoppingCart cart = command.Cart;
            
            //TODO: Store basket in database (use Marten upsert -> create/update)
            //TODO: Update cache
            return new StoreBasketResult("test");
        }
    }
}
