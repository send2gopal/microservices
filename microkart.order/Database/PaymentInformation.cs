using microkart.shared.Database;

namespace microkart.order.Database
{
    public class PaymentInformation : BaseEntity
    {
        public Guid UserId { get; set; } 
        public string CardNumber { get; set; } = string.Empty;
        public string CardHolderName { get; set; } = string.Empty;
        public string CardExpiration { get; set; } = string.Empty;
        public string CardSecurityNumber { get; set; } = string.Empty;
        public string PaymentReferenceNumber { get; set; } = string.Empty;
        public decimal  Amount { get; set; }
    }
}
