namespace microkart.order.Models
{
    public class ShippingAddressResponse
    {
        public Guid UserId { get; set; }
        public string Street { get; set; } = string.Empty;
        public string AptOrUnit { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
}