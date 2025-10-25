namespace Evo.API.Models.Requests.Customers
{
    public class UpdateCustomerApiRequest
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Phone { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
    }

    //public class DeleteCustomerApiRequest
    //{
    //    public string Id { get; set; } = default!;
    //}
}
