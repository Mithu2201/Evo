using AutoMapper;
using Evo.API.Extensions;
using Evo.API.Models.Requests.Accounts;
using Evo.Application.Features.Accounts.Commands.RegisterStaffUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Evo.API.Controllers
{
    public class StaffController(IMapper _mapper, IMediator _mediator) : BaseAPIController
    {
        [HttpPost("registerstaff")]
        public async Task<IActionResult> RegisterStaff(RegisterStaffApiRequest request)   //http://localhost:5077/api/Staff/registerstaff
        {
            var command = _mapper.Map<RegisterStaffUserCommand>(request);
            var userDto = await _mediator.Send(command);

            // Map UserDto to ApiResponse<UserDto> using extension
            return this.ToApiResponse(userDto, "User registered successfully");
        }
    }
}
