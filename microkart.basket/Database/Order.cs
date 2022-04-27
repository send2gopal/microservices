using microkart.shared.Database;
using System.ComponentModel.DataAnnotations;

namespace microkart.basket.Database
{
    public class Order : BaseEntity
    {
        [Required]
        public string OrderNumber { get; set; } = string.Empty;

        [Required]
        public string UserEmai { get; set; } = string.Empty;

        [Required]
        public string Street { get; set; } = string.Empty;

        [Required]
        public string State { get; set; } = string.Empty;

        [Required]
        public string Country { get; set; } = string.Empty;

        [Required]
        public string CardNumber { get; set; } = string.Empty;

        [Required]
        public string CardHolderName { get; set; } = string.Empty;

        [Required]
        public string CardExpirationMonth { get; set; } = string.Empty;

        [Required]
        public string CardExpirationYear { get; set; } = string.Empty;

        [Required]
        public string CardSecurityCode { get; set; } = string.Empty;

        public virtual Cart Cart { get; set; } = new Cart();
    }
}
