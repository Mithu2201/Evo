using Evo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.ThirdPartyDrivers.Dtos
{
    public class ThirdPartyDriverDto
    {
        public Guid DriverId { get; set; }
        public string? UserId { get; set; }
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        public string WorkEmail { get; set; } = default!;
        public string PhoneE164 { get; set; } = default!;
        public string? NIC { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhotoUrl { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime? LicenseExpiryUtc { get; set; }
        public string? VehicleType { get; set; }
        public string? VehiclePlateNo { get; set; }
        public DriverStatus Status { get; set; }
        public DriverVerificationStatus VerificationStatus { get; set; }
        public decimal? Rating { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
