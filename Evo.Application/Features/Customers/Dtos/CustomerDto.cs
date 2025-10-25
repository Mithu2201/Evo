// CustomerDto.cs
using System;

namespace Evo.Application.Features.Customers.Dtos
{
    public class CustomerDto
    {
        public string Id { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Phone { get; set; }
        public string Email { get; set; } = default!;
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsEmailVerified { get; set; }
        public bool? IsPhoneVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }


    // DeleteCustomerDto.cs

    //public class DeleteCustomerDto
    //{
    //    public string Id { get; set; } = default!;
    //}
}
