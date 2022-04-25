using microkart.shared.Database;

namespace microkart.basket.Database
{
    public record CheckoutCart(
        string UserEmail,
        string City,
        string Street,
        string State,
        string Country,
        string CardNumber,
        string CardHolderName,
        DateTime CardExpiration,
        string CardSecurityCode
    );
}
