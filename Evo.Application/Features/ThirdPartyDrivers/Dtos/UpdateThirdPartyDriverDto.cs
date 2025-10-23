using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.ThirdPartyDrivers.Dtos
{
    public class UpdateThirdPartyDriverDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? WorkEmail { get; set; }
        public string? PhoneE164 { get; set; }
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
        public bool? IsAvailable { get; set; }
    }
}
