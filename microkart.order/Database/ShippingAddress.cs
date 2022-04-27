using microkart.shared.Database;

namespace microkart.order.Database
{
    public class ShippingAddress: BaseEntity
    {
        public Guid UserId { get; set; }
        public string Street { get; set; }
        public string AptOrUnit { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

    }
}