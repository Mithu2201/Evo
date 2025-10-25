namespace Evo.Application.Features.Customers.Dtos
{
    // UpdateCustomerDto.cs



    public class UpdateCustomerDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Phone { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
