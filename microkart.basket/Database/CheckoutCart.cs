using microkart.shared.Database;

namespace microkart.basket.Database
{
    public record CheckoutCart(
        string UserEmail,
        string City,
        string Street,
        string State,
        string ZipCode,
        string AptOrUnit,
        string Country,
        string CardNumber,
        string CardHolderName,
        string CardExpiration,
        string CardSecurityCode
    );
}
