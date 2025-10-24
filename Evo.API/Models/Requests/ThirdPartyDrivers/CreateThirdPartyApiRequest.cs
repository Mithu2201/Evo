using System.ComponentModel.DataAnnotations;

namespace Evo.API.Models.Requests.ThirdPartyDrivers
{
    public class CreateThirdPartyApiRequest
    {

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = default!;

        [MaxLength(100)]
        public string? LastName { get; set; }

        [Required, EmailAddress, MaxLength(200)]
        public string WorkEmail { get; set; } = default!;

        [Required, MaxLength(20)]
        public string PhoneE164 { get; set; } = default!;

        [MaxLength(20)]
        public string? NIC { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(500)]
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
    }
}
